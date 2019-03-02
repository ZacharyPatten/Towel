using System;

using Towel.Mathematics;
using Towel.Measurements;

namespace Towel.Graphics
{
    /// <summary>Represents a camera to assist a game by generating a view matrix transformation.</summary>
    public class Camera
    {
        private static Vector<float> Y_AXIS = new Vector<float>(0, 1, 0);

        private Vector<float> _position;
        private Vector<float> _forward;
        private Vector<float> _up;

        public Vector<float> Position { get { return _position; } set { _position = value; } }
        public Vector<float> Forward { get { return _forward; } set { _forward = value; } }
        public Vector<float> Up { get { return _up; } set { _up = value; } }

        public Vector<float> Backward { get { return -_forward; } }
        public Vector<float> Right { get { return _forward.CrossProduct(_up).Normalize(); } }
        public Vector<float> Left { get { return _up.CrossProduct(_forward).Normalize(); } }
        public Vector<float> Down { get { return -_up; } }

        public Camera()
        {
            this._position = new Vector<float>(0, 0, 0);
            this._forward = new Vector<float>(0, 0, 1);
            this._up = new Vector<float>(0, 1, 0);
        }

        public Camera(Vector<float> pos, Vector<float> forward, Vector<float> up)
        {
            _position = pos;
            _forward = forward.Normalize();
            _up = up.Normalize();
        }

        public void Move(float x, float y, float z, float ammount)
        {
            _position = _position + (new Vector<float>(x, y, z) * ammount);
        }

        public void Move(Vector<float> direction, float ammount)
        {
            _position = _position + (direction * ammount);
        }

        public void RotateY(float angle)
        {
            Vector<float> Haxis = Y_AXIS.CrossProduct(_forward.Normalize());

            _forward = (Matrix<float>.Rotate4x4(
                Matrix<float>.FactoryIdentity(4, 4),
                Angle<float>.Factory_Degrees(angle),
                new Vector<float>(0, 1, 0)).Minor(3, 3) * _forward).Normalize();
            //_forward = _forward.RotateBy(angle, 0, 1, 0).Normalize();

            _up = _forward.CrossProduct(Haxis.Normalize());
        }

        public void RotateX(float angle)
        {
            Vector<float> Haxis = Y_AXIS.CrossProduct(_forward.Normalize());

            _forward = (Matrix<float>.Rotate4x4(
                Matrix<float>.FactoryIdentity(4, 4),
                Angle<float>.Factory_Degrees(angle),
                new Vector<float>(1, 0, 0)).Minor(3, 3) * _forward).Normalize();
            //_forward = _forward.RotateBy(angle, Haxis.X, Haxis.Y, Haxis.Z).Normalize();

            _up = _forward.CrossProduct(Haxis.Normalize());
        }

        public Matrix<float> GetViewMatrix()
        {
            Matrix<float> camera = ComputeViewMatrix(
              _position.X, _position.Y, _position.Z,
              _position.X + _forward.X, _position.Y + _forward.Y, _position.Z + _forward.Z,
              _up.X, _up.Y, _up.Z);

            return camera;


            //float FOV = 70;
            //float NEAR_PLANE = 0.2f;
            //float FAR_PLANE = 400;

            //Matrix<float> projectionMatrix = new Matrix<float>(4, 4);
            //float aspectRatio = (float)800 / (float)600;
            //float y_scale = (float)((1f / Math.Tan(Angle.DegreesToRadians(FOV / 2f))));
            //float x_scale = y_scale / aspectRatio;
            //float frustum_length = FAR_PLANE - NEAR_PLANE;

            //projectionMatrix[0, 0] = x_scale;
            //projectionMatrix[1, 1] = y_scale;
            //projectionMatrix[2, 2] = -((FAR_PLANE + NEAR_PLANE) / frustum_length);
            //projectionMatrix[2, 3] = -1;
            //projectionMatrix[3, 2] = -((2 * NEAR_PLANE * FAR_PLANE) / frustum_length);
            //projectionMatrix[3, 3] = 0;
            //return projectionMatrix;
        }

        private static Matrix<float> ComputeViewMatrix(Vector<float> eye, Vector<float> target, Vector<float> up)
        {
            if (eye == null)
            {
                throw new ArgumentNullException(nameof(eye));
            }
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (up == null)
            {
                throw new ArgumentNullException(nameof(up));
            }

            if (eye.Dimensions != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(eye), eye, "!(" + nameof(eye) + "." + nameof(eye.Dimensions) + " == 3)");
            }
            if (target.Dimensions != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(target), target, "!(" + nameof(target) + "." + nameof(target.Dimensions) + " == 3)");
            }
            if (up.Dimensions != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(up), up, "!(" + nameof(up) + "." + nameof(up.Dimensions) + " == 3)");
            }

            Vector<float> z = Vector<float>.Normalize(eye - target);
            Vector<float> x = Vector<float>.Normalize(Vector<float>.CrossProduct(up, z));
            Vector<float> y = Vector<float>.Normalize(Vector<float>.CrossProduct(z, x));

            float[,] floatData = new float[,]
            {
                { x.X, y.X, z.X, 0 },
                { x.Y, y.Y, z.Y, 0 },
                { x.Z, y.Z, z.Z, 0 },
                { -((x.X * eye.X) + (x.Y * eye.Y) + (x.Z * eye.Z)), -((y.X * eye.X) + (y.Y * eye.Y) + (y.Z * eye.Z)), -((z.X * eye.X) + (z.Y * eye.Y) + (z.Z * eye.Z)), 1 },
            };

            Matrix<float> result = new Matrix<float>(4, 4, (row, column) => floatData[row, column]);

            return result;
        }

        private static Matrix<float> ComputeViewMatrix(
            float eyeX, float eyeY, float eyeZ,
            float targetX, float targetY, float targetZ,
            float upX, float upY, float upZ)
        {
            return ComputeViewMatrix(
                new Vector<float>(eyeX, eyeY, eyeZ),
                new Vector<float>(targetX, targetY, targetZ),
                new Vector<float>(upX, upY, upZ));
        }
    }
}
