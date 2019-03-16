using System;

using Towel.Mathematics;

using OpenTK.Graphics.OpenGL;

namespace Towel.Graphics.OpenGL
{
    public static class Render
    {
        public abstract class Uniform {
            
            private static int NOT_FOUND = -1;
            
            private String name;
            private int location;
            
            protected Uniform(string name){
                this.name = name;
            }
            
            public virtual void storeUniformLocation(int programID){
                location = GL.GetUniformLocation(programID, name);
                if(location == NOT_FOUND){
                    Console.WriteLine("No uniform variable called " + name + " found!");
                }
            }
            
            protected int getLocation(){
                return location;
            }
        
        }

        public class UniformVec3 : Uniform {
            private float currentX;
            private float currentY;
            private float currentZ;
            private bool used = false;
        
            public UniformVec3(string name) : base(name) { }
        
            public void loadVec3(Vector<float> vector) {
                loadVec3(vector.X, vector.Y, vector.Z);
            }
        
            public void loadVec3(float x, float y, float z) {
                if (!used || x != currentX || y != currentY || z != currentZ) {
                    this.currentX = x;
                    this.currentY = y;
                    this.currentZ = z;
                    used = true;
                    GL.Uniform3(base.getLocation(), x, y, z);
                }
            }
        
        }

        public class UniformMatrix : Uniform{
    
            private static float[] matrixBuffer = new float[16];
        
            public UniformMatrix(string name) : base(name) { }
            
            public void loadMatrix(Matrix<float> matrix){
                matrixBuffer = matrix._matrix;
                //matrixBuffer.flip();

                //Array.Reverse(matrixBuffer);


                int location = base.getLocation();

                //foreach (float value in matrixBuffer)
                //{
                //    if (value == float.NaN)
                //    {
                //        matrixBuffer = Matrix<float>.FactoryIdentity(4, 4)._matrix;
                //    }
                //}

                int length = 1;

                GL.UniformMatrix4(location, length, false, matrixBuffer);
            }
        }

        public class UniformMat4Array : Uniform{
    
            private UniformMatrix[] matrixUniforms;
            
            public UniformMat4Array(string name, int size) : base(name) {
                matrixUniforms = new UniformMatrix[size];
                for(int i=0;i<size;i++){
                    matrixUniforms[i] = new UniformMatrix(name + "["+i+"]");
                }
            }
            
            public override void storeUniformLocation(int programID) {
                foreach (UniformMatrix matrixUniform in matrixUniforms){
                    matrixUniform.storeUniformLocation(programID);
                }
            }
        
            public void loadMatrixArray(Matrix<float>[] matrices){
                for(int i=0;i<matrices.Length;i++){
                    matrixUniforms[i].loadMatrix(matrices[i]);
                }
            }
        }

        public class UniformSampler : Uniform
        {
            private int currentValue;
            private bool used = false;

            public UniformSampler(string name) : base(name) { }

            public void loadTexUnit(int texUnit)
            {
                if (!used || currentValue != texUnit)
                {
                    GL.Uniform1(base.getLocation(), texUnit);
                    used = true;
                    currentValue = texUnit;
                }
            }
        }

        public class ShaderProgram {

            private int programID;
            public string vertexCompileInfo;
            public string fragementCompileInfo;
    
            public ShaderProgram(string vertexFile, string fragmentFile, params string[] inVariables) {
                int vertexShaderID = loadShader(vertexFile, ShaderType.VertexShader, out vertexCompileInfo);
                int fragmentShaderID = loadShader(fragmentFile, ShaderType.FragmentShader, out fragementCompileInfo);
                programID = GL.CreateProgram();
                GL.AttachShader(programID, vertexShaderID);
                GL.AttachShader(programID, fragmentShaderID);
                bindAttributes(inVariables);
                GL.LinkProgram(programID);
                GL.DetachShader(programID, vertexShaderID);
                GL.DetachShader(programID, fragmentShaderID);
                GL.DeleteShader(vertexShaderID);
                GL.DeleteShader(fragmentShaderID);
            }
            
            protected void storeAllUniformLocations(params Uniform[] uniforms){
                foreach (Uniform uniform in uniforms){
                    uniform.storeUniformLocation(programID);
                }
                GL.ValidateProgram(programID);
            }
    
            public void start() {
                GL.UseProgram(programID);
            }
    
            public void stop() {
                GL.UseProgram(0);
            }
    
            public void cleanUp() {
                stop();
                GL.DeleteProgram(programID);
            }
    
            private void bindAttributes(string[] inVariables){
                for(int i=0;i<inVariables.Length;i++){
                    GL.BindAttribLocation(programID, i, inVariables[i]);
                }
            }
            
            private int loadShader(string source, ShaderType type, out string info) {
                int shaderID = GL.CreateShader(type);
                GL.ShaderSource(shaderID, source);
                GL.CompileShader(shaderID);


                GL.GetShaderInfoLog(shaderID, out info);
                //if (GL.GetShaderInfoLog(shaderID, out info))
                //{
                //    //System.out.println(GL20.glGetShaderInfoLog(shaderID, 500));
                //    //System.err.println("Could not compile shader "+ file);
                //    Console.WriteLine("Could not compile shader");
                //    //System.exit(-1);
                //    Environment.Exit(-1);
                //}


                return shaderID;
            }
        }

        public class AnimatedModelShader : ShaderProgram {

            private static int MAX_JOINTS = 50;// max number of joints in a skeleton
            private static int DIFFUSE_TEX_UNIT = 0;
        
            //private static final MyFile VERTEX_SHADER = new MyFile("renderer", "animatedEntityVertex.glsl");
            //private static final MyFile FRAGMENT_SHADER = new MyFile("renderer", "animatedEntityFragment.glsl");
        
            public UniformMatrix projectionViewMatrix = new UniformMatrix("projectionViewMatrix");
            public UniformVec3 lightDirection = new UniformVec3("lightDirection");
            public UniformMat4Array jointTransforms = new UniformMat4Array("jointTransforms", MAX_JOINTS);
            public UniformSampler diffuseMap = new UniformSampler("diffuseMap");
        
            public AnimatedModelShader() : base(
                Shaders.AnimatedModelVertexShader,
                Shaders.AnimatedModelFragmentShader,
                "in_position",
                "in_textureCoords",
                "in_normal",
                "in_jointIndices",
                "in_weights")
            {
                base.storeAllUniformLocations(projectionViewMatrix, diffuseMap, lightDirection, jointTransforms);
                connectTextureUnits();
            }
        
            private void connectTextureUnits() {
                base.start();
                diffuseMap.loadTexUnit(DIFFUSE_TEX_UNIT);
                base.stop();
            }
        }

        public static void Model(AnimatedModelShader shaderProgram, LoadedModel loadedModel, Camera camera, Vector<float> lightDir)
        {
            shaderProgram.start();

            OpenTK.Matrix4 perspective = OpenTK.Matrix4.CreatePerspectiveFieldOfView(
                (float).5f,
                (float)600 / (float)800,
                (float)1,
                (float)65);

            Matrix<float> projection = new Matrix<float>(new float[,] {
                { perspective.M11, perspective.M12, perspective.M13, perspective.M14 },
                { perspective.M21, perspective.M22, perspective.M23, perspective.M24 },
                { perspective.M31, perspective.M32, perspective.M33, perspective.M34 },
                { perspective.M41, perspective.M42, perspective.M43, perspective.M44 },
            });

            Matrix<float> projectionView = projection * camera.GetViewMatrix();

            //projectionView = new Matrix<float>(new float[,] {
            //    { 1, 0, 0, 0 },
            //    { 0, 1, 0, 0 },
            //    { 0, 0, 1, -100 },
            //    { 0, 0, 0, 1 },
            //});

            GL.BindVertexArray(loadedModel.LoadedVertexArrayObject._id);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);
            GL.EnableVertexAttribArray(4);

            Matrix<float> projectionViewMatrix = new Matrix<float>(new float[,]
                {
                    { 0.8033333f, 0.0f, 0.0f, 0.0f, },
                    { 0.0f, 1.4064512f, -0.2479953f, -7.0322556f, },
                    { 0.0f, -0.17382191f, -0.98579305f, 10.478915f, },
                    { 0.0f, -0.17364818f, -0.9848077f, 10.868241f },
                }).Transpose();


            shaderProgram.projectionViewMatrix.loadMatrix(projectionViewMatrix);
            shaderProgram.lightDirection.loadVec3(lightDir);
            
            //OpenGlUtils.enableDepthTesting(true);
            
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, loadedModel.LoadedTextureId);
            
            //foreach (LoadedModel.VertexBufferObject vbo in loadedModel.LoadedVertexArrayObject._dataVbos)
            //{
            //    GL.EnableVertexAttribArray(vbo.Id);
            //}

            shaderProgram.jointTransforms.loadMatrixArray(loadedModel.CalculateAnimatedJointMatrices());
            GL.DrawElements(BeginMode.Triangles, loadedModel.LoadedVertexArrayObject.IndexCount, DrawElementsType.UnsignedInt, 0);

            //GL.BindVertexArray(loadedModel.LoadedVertexArrayObject._id);
            //GL.DisableVertexAttribArray(0);
            //GL.DisableVertexAttribArray(1);
            //GL.DisableVertexAttribArray(2);
            //GL.DisableVertexAttribArray(3);
            //GL.DisableVertexAttribArray(4);

            //foreach (LoadedModel.VertexBufferObject vbo in loadedModel.LoadedVertexArrayObject._dataVbos)
            //{
            //    GL.EnableVertexAttribArray(vbo.Id);
            //}

            GL.BindVertexArray(0);
            shaderProgram.stop();
        }
    }
}
