using System;
using System.Drawing;
using Towel.Mathematics;
using Towel.DataStructures;

namespace Towel.Graphics
{
    public class Model
    {
        #region Nested Definitons
        
        public class Joint
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Matrix<float> BindLocalTransform { get; set; }
        }

        public class Animation
        {
            public class KeyFrame
            {
                public float TimeSpan { get; set; }
                public IMap<Link<Vector<float>, Quaternion<float>>, Joint> JointTransformations { get; set; }
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public float Duration { get; set; }
            public KeyFrame[] KeyFrames { get; set; }
        }

        public enum ColorListFormat
        {
            NotAvailable,
            RGB,
            RGBA,
        }

        public enum IndexListFormat
        {
            Triangles,
            TriangleStrip,
            TriangleFan,
            Quads,
            QuadStrip,
        }

        #endregion

        public const int MaximumJoints = 50;

        // model data
        public string _name;
        public Vector<float> _position = Vector<float>.FactoryZero(3);
        public Quaternion<float> _orientation = Quaternion<float>.Identity;

        //public Bitmap _texture;

        // geometry and color/texturing data
        public float[] _positions;
        public float[] _textureCoordinates;
        public float[] _normals;
        public ColorListFormat _colorListformat;
        public float[] _colors;
        public int[] _jointIds;
        public float[] _jointWeights;
        public IndexListFormat _indexListFormat;
        public int[] _indices;

        // static animation data
        public ITree<Joint> _joints;
        public IMap<Animation, string> _animations;

        // running animation data
        public Animation _activeAnimation;
        public DateTime _animationStartTime;
        
        public Model() { }
        
        public string Name { get { return this._name; } }
        public Vector<float> Position { get { return this._position; } set { this._position = value; } }
        public Quaternion<float> Rotation { get { return this._orientation; } set { this._orientation = value; } }

        public bool HasTextureCoordinates { get { return this._textureCoordinates != null && this._textureCoordinates.Length > 0; } }
        public bool HasNormals { get { return this._normals != null && this._normals.Length > 0; } }
        public bool HasColors { get { return this._colors != null && this._colors.Length > 0; } }
        public bool HasAnimation
        {
            get
            {
                return
                    this._jointIds != null && this._jointIds.Length > 0 &&
                    this._joints != null && this._joints.Count > 0 &&
                    this._jointWeights != null && this._jointWeights.Length > 0 &&
                    this._animations != null && this._animations.Count > 0;
            }
        }

        public void ActivateAnimation(string animationName)
        {
            this.ActivateAnimation(animationName, DateTime.Now);
        }

        public void ActivateAnimation(string animationName, DateTime startTime)
        {
            if (!this._animations.Contains(animationName))
            {
                throw new System.ArgumentException("The requested animation does not exist.");
            }

            this._activeAnimation = this._animations[animationName];
            this._animationStartTime = startTime;
        }

        public Matrix<float>[] CalculateAnimatedJointMatrices()
        {
            // initialize the matrix array and set the base matrix of each joint
            Matrix<float>[] jointMatrices = new Matrix<float>[this._joints.Count];
            
            // if no animation is active, the base matrices wer all we needed
            if (this._activeAnimation == null)
            {
                this._joints.Stepper(x => jointMatrices[x.Id] = x.BindLocalTransform);
                return jointMatrices;
            }

            // get the elapsed animation time
            TimeSpan elapsedAnimationTimeSpan = DateTime.Now - this._animationStartTime;
            float elapsedAnimationFloat = (float)elapsedAnimationTimeSpan.TotalSeconds;
            elapsedAnimationFloat %= this._activeAnimation.Duration;

            // get the previous/next key frames
            Animation.KeyFrame previousFrame = this._activeAnimation.KeyFrames[0];
            Animation.KeyFrame nextFrame = null;
            // OPTIMIZATION NOTE: binary search is possible here
            for (int i = 1; i < this._activeAnimation.KeyFrames.Length; i++)
            {
                nextFrame = this._activeAnimation.KeyFrames[i];
                if (nextFrame.TimeSpan > elapsedAnimationFloat)
                {
                    break;
                }
                previousFrame = this._activeAnimation.KeyFrames[i];
            }

            // calculate the ratio between the two key frames
            float keyFrameTimeSpan = nextFrame.TimeSpan - previousFrame.TimeSpan;
            float currentKeyFrameTime = elapsedAnimationFloat - previousFrame.TimeSpan;
            float keyFrameRatio = currentKeyFrameTime / keyFrameTimeSpan;

            // interpolate the joint matrices
            IMap<Matrix<float>, string> currentPose = new MapHashLinked<Matrix<float>, string>();
            previousFrame.JointTransformations.Keys(joint =>
                {
                    Link<Vector<float>, Quaternion<float>> previousTransform = previousFrame.JointTransformations[joint];
                    Link<Vector<float>, Quaternion<float>> nextTransform = nextFrame.JointTransformations[joint];

                    Vector<float> currentTranslation = Vector<float>.LinearInterpolation(previousTransform._1, nextTransform._1, keyFrameRatio);
                    Quaternion<float> currentRotation = Quaternion<float>.LinearInterpolation(previousTransform._2, nextTransform._2, keyFrameRatio);

                    Matrix<float> rotation3x3 = Quaternion<float>.ToMatrix3x3(currentRotation);

                    float[,] floatData = new float[,]
                    {
                        { rotation3x3[0, 0], rotation3x3[0, 1], rotation3x3[0, 2], currentTranslation[0] },
                        { rotation3x3[1, 0], rotation3x3[1, 1], rotation3x3[1, 2], currentTranslation[1] },
                        { rotation3x3[2, 0], rotation3x3[2, 1], rotation3x3[2, 2], currentTranslation[2] },
                        { 0, 0, 0, 1 },
                    };

                    Matrix<float> currentJointMatrix = new Matrix<float>(4, 4, (row, column) => floatData[row, column]);

                    currentPose.Add(joint.Name, currentJointMatrix);
                });

            // multiply the current pose and the bind local transform
            this._joints.Stepper(x => jointMatrices[x.Id] = currentPose[x.Name] * x.BindLocalTransform.Inverse());
            
            return jointMatrices;

            //for (int i = 0; i < this._joints.Count; i++)
            //{
            //    jointMatrices[i] = Matrix<float>.FactoryIdentity(4, 4);
            //}
            //return jointMatrices;
        }
    }
}
