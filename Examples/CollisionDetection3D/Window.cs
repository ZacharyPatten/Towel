using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using Towel;
using Towel.DataStructures;
using static Towel.Mathematics.Compute;
using SVector3 = System.Numerics.Vector3;

namespace CollisionDetection3D
{
	#region Camera

	// This is the camera class as it could be set up after the tutorials on the website
	// It is important to note there are a few ways you could have set up this camera, for example
	// you could have also managed the player input inside the camera class, and a lot of the properties could have
	// been made into functions.

	// TL;DR: This is just one of many ways in which we could have set up the camera
	// Check out the web version if you don't know why we are doing a specific thing or want to know more about the code
	public class Camera
	{
		// We need quite the amount of vectors to define the camera
		// The position is simply the position of the camera
		// the other vectors are directions pointing outwards from the camera to define how it is rotated
		public Vector3 Position;
		private Vector3 front = -Vector3.UnitZ;
		public Vector3 Front => front;
		private Vector3 up = Vector3.UnitY;
		public Vector3 Up => up;
		private Vector3 right = Vector3.UnitX;
		public Vector3 Right => right;

		// Pitch is the rotation around the x axis, and it is explained more specifically in the tutorial how we can use this
		private float _pitch;
		public float Pitch
		{
			get => _pitch;
			set
			{
				// We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
				// of weird "bugs" when you are using euler angles for rotation. If you want to read more about this you can try researching a topic called gimbal lock
				if (value > 89.0f)
				{
					_pitch = 89.0f;
				}
				else if (value <= -89.0f)
				{
					_pitch = -89.0f;
				}
				else
				{
					_pitch = value;
				}
				UpdateVertices();
			}
		}
		// Yaw is the rotation around the y axis, and it is explained more specifically in the tutorial how we can use this
		private float yaw;
		public float Yaw
		{
			get => yaw;
			set
			{
				yaw = value;
				UpdateVertices();
			}
		}

		// The speed and the sensitivity are the speeds of respectively,
		// the movement of the camera and the rotation of the camera (mouse sensitivity)
		public float Speed = 10f;
		public float Sensitivity = 0.2f;

		// The fov (field of view) is how wide the camera is viewing, this has been discussed more in depth in a
		// previous tutorial, but in this tutorial you have also learned how we can use this to simulate a zoom feature.
		private float fov = 45.0f;
		public float Fov
		{
			get => fov;
			set
			{
				if (value >= 45.0f)
				{
					fov = 45.0f;
				}
				else if (value <= 1.0f)
				{
					fov = 1.0f;
				}
				else
				{
					fov = value;
				}
			}
		}
		public float AspectRatio { get; set; } // This is simply the aspect ratio of the viewport, used for the projection matrix

		// In the instructor we take in a position
		// We also set the yaw to -90, the code would work without this, but you would be started rotated 90 degrees away from the rectangle
		public Camera(Vector3 position)
		{
			Position = position;
			yaw = -90;
		}

		// Get the view matrix using the amazing LookAt function described more in depth on the web version of the tutorial
		public Matrix4 GetViewMatrix() =>
			Matrix4.LookAt(Position, Position + Front, Up);
		// Get the projection matrix using the same method we have used up until this point
		public Matrix4 GetProjectionMatrix() =>
			Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), AspectRatio, 0.01f, 10000f);

		// This function is going to update the direction vertices using some of the math learned in the web tutorials
		private void UpdateVertices()
		{
			// First the front matrix is calculated using some basic trigonometry
			front.X = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Yaw));
			front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Pitch));
			front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Yaw));
			// We need to make sure the vectors are all normalized, as otherwise we would get some funky results
			front = Vector3.Normalize(front);

			// Calculate both the right and the up vector using the cross product
			// Note that we are calculating the right from the global up, this behaviour might
			// not be what you need for all cameras so keep this in mind if you do not want a FPS camera
			right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
			up = Vector3.Normalize(Vector3.Cross(Right, Front));
		}
	}

	#endregion

	#region IObject3D

	interface IObject3D
	{
		float MinX { get; }
		float MaxX { get; }

		float MinY { get; }
		float MaxY { get; }

		float MinZ { get; }
		float MaxZ { get; }

		SVector3 Velocity { get; set; }
		SVector3 Position { get; set; }
	}

	#endregion

	#region Cube

	class Cube : IObject3D
	{
		public float HalfLength;

		public Vector3 Color { get; set; }

		public SVector3 Position { get; set; }
		public SVector3 Velocity { get; set; }

		public float MinX { get => Position.X - HalfLength; }
		public float MaxX { get => Position.X + HalfLength; }
		public float MinY { get => Position.Y - HalfLength; }
		public float MaxY { get => Position.Y + HalfLength; }
		public float MinZ { get => Position.Z - HalfLength; }
		public float MaxZ { get => Position.Z + HalfLength; }
	}

	#endregion

	#region Sphere

	//class Sphere : IObject3D
	//{
	//	public float Radius;
	//
	//	public SVector3 Position { get; set; }
	//	public SVector3 Velocity { get; set; }
	//
	//	public float MinX { get => Position.X - Radius; }
	//	public float MaxX { get => Position.X + Radius; }
	//	public float MinY { get => Position.Y - Radius; }
	//	public float MaxY { get => Position.Y + Radius; }
	//	public float MinZ { get => Position.Z - Radius; }
	//	public float MaxZ { get => Position.Z + Radius; }
	//}

	#endregion

	class Window : GameWindow
	{
		#region Vertex Shader Source

		string _vertextShaderSource =
@"#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 color;

out vec3 colorPass;

void main(void)
{
    colorPass = color;
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
}
";

		#endregion

		#region Fragment Shader Source

		string _fragmentShaderSource =
@"#version 330

in vec3 colorPass;

out vec4 outputColor;

void main()
{
    outputColor = vec4(colorPass, 1.0);
}";

		#endregion

		#region Cube Geometry Data

		//        G  ______________________  H
		//          /|                    /|
		//         / |                   / |
		//        /  |                  /  |
		//       /   |                 /   |
		//    C /____|________________/ D  |
		//      |    |                |    |
		//      |    |                |    |
		//      |    |     origin     |    |
		//      |    |       +        |    |
		//      |  E |________________|____| F
		//      |    /                |    /
		//      |   /                 |   / 
		//      |  /                  |  /  
		//      | /                   | /   
		//    A |/____________________|/ B   

		private readonly float[] vertices =
		{
			-.5f, -.5f, -.5f, // A
             .5f, -.5f, -.5f, // B
            -.5f,  .5f, -.5f, // C
             .5f,  .5f, -.5f, // D
            -.5f, -.5f,  .5f, // E
             .5f, -.5f,  .5f, // F
            -.5f,  .5f,  .5f, // G
             .5f,  .5f,  .5f, // H
        };

		private readonly uint[] indices =
		{
			0, 1, 2, // A-B-C
            1, 2, 3, // B-C-D

            4, 5, 6, // E-F-G
            5, 6, 7, // F-G-H

            0, 1, 4, // A-B-E
            1, 4, 5, // B-E-F

            0, 2, 6, // A-C-G
            0, 4, 6, // A-E-G

            1, 3, 5, // B-D-F
            3, 5, 7, // D-F-H

            2, 3, 6, // C-D-G
            3, 6, 7, // D-G-H
        };

		#endregion

		IOmnitreePoints<IObject3D, float, float, float> _omnitree;
		List<IObject3D> _objects;

		int _cubeElementBufferId;
		int _cubeVertexBufferId;
		int _cubeVertexArrayId;

		int _cubeShaderProgramId;

		private Camera camera;
		private bool firstMove = true;
		private Vector2 lastPos;

		private double time;

		private Random random = new Random();

		public Window() : base() { }

		public Window(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

		protected override void OnLoad(EventArgs e)
		{
			#region Object Generation

			_objects = new List<IObject3D>();
			for (int i = -50; i < 50; i += 10)
			{
				for (int j = -50; j < 50; j += 10)
				{
					for (int k = -50; k < 50; k += 10)
					{
						if (i == 0 && j == 0 && k == 0)
						{
							continue;
						}

						SVector3 position = new SVector3(i, j, k);
						SVector3 velocity;
						Vector3 color;

						if (i == 0 && j == 0 && k == 0)
						{
							velocity = SVector3.Normalize(new SVector3(
								(float)random.NextDouble(),
								(float)random.NextDouble(),
								(float)random.NextDouble()));
						}
						else
						{
							velocity = SVector3.Normalize(-position);
						}
						color = new Vector3(
							(float)random.NextDouble(),
							(float)random.NextDouble(),
							(float)random.NextDouble());

						Cube cube = new Cube()
						{
							HalfLength = .5f,
							Position = position,
							Velocity = velocity,
							Color = color,
						};
						_objects.Add(cube);
					}
				}
			}

			#endregion

			#region Omnitree Initialization

			void Locate(IObject3D obj,
				out float x,
				out float y,
				out float z)
			{
				x = obj.Position.X;
				y = obj.Position.Y;
				z = obj.Position.Z;
			}

			//void GetBounds(IObject3D boundingBox3D,
			//	out float minX, out float maxX,
			//	out float minY, out float maxY,
			//	out float minZ, out float maxZ)
			//{
			//	minX = boundingBox3D.MinX; maxX = boundingBox3D.MaxX;
			//	minY = boundingBox3D.MinY; maxY = boundingBox3D.MaxY;
			//	minZ = boundingBox3D.MinZ; maxZ = boundingBox3D.MaxZ;
			//}

			bool Equate(IObject3D a, IObject3D b)
			{
				return
					a.MinX == b.MinX && a.MaxX == b.MaxX &&
					a.MinY == b.MinY && a.MaxY == b.MaxY &&
					a.MinZ == b.MinZ && a.MaxZ == b.MaxZ;
			}

			float MeanMinX(
				Omnitree.Bounds<float, float, float> parentBounds,
				Stepper<IObject3D> stepper)
			{
				return Mean<float>(step => stepper(boundingBox => step(boundingBox.MinX)));
			}

			float MeanMinY(
				Omnitree.Bounds<float, float, float> parentBounds,
				Stepper<IObject3D> stepper)
			{
				return Mean<float>(step => stepper(boundingBox => step(boundingBox.MinY)));
			}

			float MeanMinZ(
				Omnitree.Bounds<float, float, float> parentBounds,
				Stepper<IObject3D> stepper)
			{
				return Mean<float>(step => stepper(boundingBox => step(boundingBox.MinZ)));
			}

			// construction of the omnitree
			_omnitree = new OmnitreePointsLinked<IObject3D, float, float, float>(
				Locate,
				Equate,
				Towel.Equate.Default, Towel.Equate.Default, Towel.Equate.Default,
				Towel.Compare.Default, Towel.Compare.Default, Towel.Compare.Default,
				MeanMinX, MeanMinY, MeanMinZ);

			foreach (var obj in _objects)
			{
				_omnitree.Add(obj);
			}

			#endregion

			#region OpenGL Initialization

			GL.ClearColor(Color.Teal);
			GL.Enable(EnableCap.DepthTest);

			_cubeVertexBufferId = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _cubeVertexBufferId);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

			_cubeElementBufferId = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _cubeElementBufferId);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			_cubeVertexArrayId = GL.GenVertexArray();
			GL.BindVertexArray(_cubeVertexArrayId);

			GL.BindBuffer(BufferTarget.ArrayBuffer, _cubeVertexBufferId);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _cubeElementBufferId);

			#endregion

			#region Cube Shader Initialization

			// Cube Vertex Shader
			int cubeVertexShaderId = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(cubeVertexShaderId, _vertextShaderSource);
			GL.CompileShader(cubeVertexShaderId);
			GL.GetShader(cubeVertexShaderId, ShaderParameter.CompileStatus, out int vertexCode);
			if (vertexCode != (int)All.True)
			{
				throw new Exception("Error compiling vertex shader.");
			}

			// Cube Fragment Shader
			int cubeFragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(cubeFragmentShaderId, _fragmentShaderSource);
			GL.CompileShader(cubeFragmentShaderId);
			GL.GetShader(cubeFragmentShaderId, ShaderParameter.CompileStatus, out int fragementCode);
			if (fragementCode != (int)All.True)
			{
				throw new Exception("Error compiling fragment shader.");
			}

			// Cube Shader Program
			_cubeShaderProgramId = GL.CreateProgram();
			GL.AttachShader(_cubeShaderProgramId, cubeVertexShaderId);
			GL.AttachShader(_cubeShaderProgramId, cubeFragmentShaderId);
			GL.LinkProgram(_cubeShaderProgramId);
			GL.GetProgram(_cubeShaderProgramId, GetProgramParameterName.LinkStatus, out var shaderCode);
			if (shaderCode != (int)All.True)
			{
				throw new Exception("Error compiling shader program.");
			}
			GL.DetachShader(_cubeShaderProgramId, cubeVertexShaderId);
			GL.DetachShader(_cubeShaderProgramId, cubeFragmentShaderId);
			GL.DeleteShader(cubeVertexShaderId);
			GL.DeleteShader(cubeFragmentShaderId);
			GL.UseProgram(_cubeShaderProgramId);

			int vertexLocation = GL.GetAttribLocation(_cubeShaderProgramId, "aPosition");
			GL.EnableVertexAttribArray(vertexLocation);
			GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

			#endregion

			#region Camera Initialization

			camera = new Camera(new Vector3(0, 10, 50));
			camera.AspectRatio = Width / (float)Height;
			CursorVisible = false;

			#endregion

			base.OnLoad(e);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			#region Rendering Code

			time += 4.0 * e.Time;
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			Matrix4 projectionMatrix = camera.GetProjectionMatrix();
			Matrix4 cameraMatrix = camera.GetViewMatrix();
			foreach (IObject3D @object in _objects)
			{
				if (@object is Cube)
				{
					#region Render Cube

					Vector3 color = (@object as Cube).Color;
					Matrix4 modelMatrix = Matrix4.CreateTranslation(@object.Position.X, @object.Position.Y, @object.Position.Z);
					GL.UseProgram(_cubeShaderProgramId);
					GL.Uniform3(GL.GetUniformLocation(_cubeShaderProgramId, "color"), ref color);
					GL.UniformMatrix4(GL.GetUniformLocation(_cubeShaderProgramId, "model"), true, ref modelMatrix);
					GL.UniformMatrix4(GL.GetUniformLocation(_cubeShaderProgramId, "view"), true, ref cameraMatrix);
					GL.UniformMatrix4(GL.GetUniformLocation(_cubeShaderProgramId, "projection"), true, ref projectionMatrix);
					GL.BindVertexArray(_cubeVertexArrayId);
					GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

					#endregion
				}
				//else if (@object is Sphere)
				//{
				//	// I haven't added any spheres yet
				//}
			}

			#endregion

			SwapBuffers();
			base.OnRenderFrame(e);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			if (!Focused)
			{
				return;
			}

			//DateTime a = DateTime.Now;

			#region Controls

			var input = Keyboard.GetState();
			if (input.IsKeyDown(Key.Escape))
			{
				Exit();
			}
			if (input.IsKeyDown(Key.W))
				camera.Position += camera.Front * camera.Speed * (float)e.Time;
			if (input.IsKeyDown(Key.S))
				camera.Position -= camera.Front * camera.Speed * (float)e.Time;
			if (input.IsKeyDown(Key.A))
				camera.Position -= camera.Right * camera.Speed * (float)e.Time;
			if (input.IsKeyDown(Key.D))
				camera.Position += camera.Right * camera.Speed * (float)e.Time;
			if (input.IsKeyDown(Key.Space))
				camera.Position += camera.Up * camera.Speed * (float)e.Time;
			if (input.IsKeyDown(Key.LShift))
				camera.Position -= camera.Up * camera.Speed * (float)e.Time;
			var mouse = Mouse.GetState();
			if (firstMove)
			{
				lastPos = new Vector2(mouse.X, mouse.Y);
				firstMove = false;
			}
			else
			{
				var deltaX = mouse.X - lastPos.X;
				var deltaY = mouse.Y - lastPos.Y;
				lastPos = new Vector2(mouse.X, mouse.Y);
				camera.Yaw += deltaX * camera.Sensitivity;
				camera.Pitch -= deltaY * camera.Sensitivity;
			}

			#endregion

			#region Collision Detection

			int iteration = 0;
			foreach (IObject3D obj in _objects)
			{
				iteration++;
				const float radius = 1500f;

				if (obj.Position.LengthSquared() > radius)
				{
					obj.Velocity = SVector3.Normalize(-obj.Position);
				}
				else
				{
					_omnitree.Stepper(
						x =>
						{
							if (object.ReferenceEquals(x, obj))
							{
								return;
							}

							SVector3 temp = obj.Position - x.Position;
							if (temp.LengthSquared() == 0)
							{
								obj.Velocity = SVector3.Normalize(new SVector3(
									(float)random.NextDouble(),
									(float)random.NextDouble(),
									(float)random.NextDouble()));
							}
							else
							{
								obj.Velocity = SVector3.Normalize(temp);
							}
						},
						obj.Position.X - 1, obj.Position.X + 1,
						obj.Position.Y - 1, obj.Position.Y + 1,
						obj.Position.Z - 1, obj.Position.Z + 1);
				}

				obj.Position += obj.Velocity * (float)e.Time * 4;
			}

			// this is not ideal... we shouldn't have to rebuild the omnitree every frame,
			// but the "update" function has a bug at the moment
			_omnitree.Clear();
			foreach (var obj in _objects)
			{
				_omnitree.Add(obj);
			}
			//_omnitree.Update();

			#endregion

			base.OnUpdateFrame(e);
		}

		#region Other Necessary Overrides

		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			if (Focused)
			{
				Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			camera.Fov -= e.DeltaPrecise;
			base.OnMouseWheel(e);
		}

		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);
			camera.AspectRatio = Width / (float)Height;
			base.OnResize(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindVertexArray(0);
			GL.UseProgram(0);
			GL.DeleteBuffer(_cubeVertexBufferId);
			GL.DeleteVertexArray(_cubeVertexArrayId);
			GL.DeleteProgram(_cubeShaderProgramId);
			base.OnUnload(e);
		}

		#endregion
	}
}

#region ALTERNATE VERSION

//using OpenTK;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL4;
//using OpenTK.Input;
//using Towel.Mathematics;
//using Towel.DataStructures;

//using static Towel.Mathematics.Compute;
//using Towel;
//using System.Drawing;

//namespace CollisionDetection3D
//{
//    #region Camera

//    // This is the camera class as it could be set up after the tutorials on the website
//    // It is important to note there are a few ways you could have set up this camera, for example
//    // you could have also managed the player input inside the camera class, and a lot of the properties could have
//    // been made into functions.

//    // TL;DR: This is just one of many ways in which we could have set up the camera
//    // Check out the web version if you don't know why we are doing a specific thing or want to know more about the code
//    public class Camera
//    {
//        // We need quite the amount of vectors to define the camera
//        // The position is simply the position of the camera
//        // the other vectors are directions pointing outwards from the camera to define how it is rotated
//        public Vector3 Position;
//        private Vector3 front = -Vector3.UnitZ;
//        public Vector3 Front => front;
//        private Vector3 up = Vector3.UnitY;
//        public Vector3 Up => up;
//        private Vector3 right = Vector3.UnitX;
//        public Vector3 Right => right;

//        // Pitch is the rotation around the x axis, and it is explained more specifically in the tutorial how we can use this
//        private float _pitch;
//        public float Pitch
//        {
//            get => _pitch;
//            set
//            {
//                // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
//                // of weird "bugs" when you are using euler angles for rotation. If you want to read more about this you can try researching a topic called gimbal lock
//                if (value > 89.0f)
//                {
//                    _pitch = 89.0f;
//                }
//                else if (value <= -89.0f)
//                {
//                    _pitch = -89.0f;
//                }
//                else
//                {
//                    _pitch = value;
//                }
//                UpdateVertices();
//            }
//        }
//        // Yaw is the rotation around the y axis, and it is explained more specifically in the tutorial how we can use this
//        private float yaw;
//        public float Yaw
//        {
//            get => yaw;
//            set
//            {
//                yaw = value;
//                UpdateVertices();
//            }
//        }

//        // The speed and the sensitivity are the speeds of respectively,
//        // the movement of the camera and the rotation of the camera (mouse sensitivity)
//        public float Speed = 1.5f;
//        public float Sensitivity = 0.2f;

//        // The fov (field of view) is how wide the camera is viewing, this has been discussed more in depth in a
//        // previous tutorial, but in this tutorial you have also learned how we can use this to simulate a zoom feature.
//        private float fov = 45.0f;
//        public float Fov
//        {
//            get => fov;
//            set
//            {
//                if (value >= 45.0f)
//                {
//                    fov = 45.0f;
//                }
//                else if (value <= 1.0f)
//                {
//                    fov = 1.0f;
//                }
//                else
//                {
//                    fov = value;
//                }
//            }
//        }
//        public float AspectRatio { get; set; } // This is simply the aspect ratio of the viewport, used for the projection matrix

//        // In the instructor we take in a position
//        // We also set the yaw to -90, the code would work without this, but you would be started rotated 90 degrees away from the rectangle
//        public Camera(Vector3 position)
//        {
//            Position = position;
//            yaw = -90;
//        }

//        // Get the view matrix using the amazing LookAt function described more in depth on the web version of the tutorial
//        public Matrix4 GetViewMatrix() =>
//            Matrix4.LookAt(Position, Position + Front, Up);
//        // Get the projection matrix using the same method we have used up until this point
//        public Matrix4 GetProjectionMatrix() =>
//            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), AspectRatio, 0.01f, 100f);

//        // This function is going to update the direction vertices using some of the math learned in the web tutorials
//        private void UpdateVertices()
//        {
//            // First the front matrix is calculated using some basic trigonometry
//            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Yaw));
//            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Pitch));
//            front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Yaw));
//            // We need to make sure the vectors are all normalized, as otherwise we would get some funky results
//            front = Vector3.Normalize(front);

//            // Calculate both the right and the up vector using the cross product
//            // Note that we are calculating the right from the global up, this behaviour might
//            // not be what you need for all cameras so keep this in mind if you do not want a FPS camera
//            right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
//            up = Vector3.Normalize(Vector3.Cross(Right, Front));
//        }
//    }

//    #endregion

//    #region IObject3D<T>

//    interface IObject3D<T>
//    {
//        T MinX { get; }
//        T MaxX { get; }

//        T MinY { get; }
//        T MaxY { get; }

//        T MinZ { get; }
//        T MaxZ { get; }

//        Vector<T> Velocity { get; set; }
//        Vector<T> Position { get; set; }
//    }

//    #endregion

//    #region Cube<T>

//    class Cube<T> : IObject3D<T>
//    {
//        public T HalfLength;

//        public Vector<T> Position { get; set; }
//        public Vector<T> Velocity { get; set; }

//        public T MinX { get => Subtract(Position.X, HalfLength); }
//        public T MaxX { get => Add(Position.X, HalfLength); }
//        public T MinY { get => Subtract(Position.Y, HalfLength); }
//        public T MaxY { get => Add(Position.Y, HalfLength); }
//        public T MinZ { get => Subtract(Position.Z, HalfLength); }
//        public T MaxZ { get => Add(Position.Z, HalfLength); }
//    }

//    #endregion

//    #region Sphere<T>

//    class Sphere<T> : IObject3D<T>
//    {
//        T Radius;

//        public Vector<T> Position { get; set; }
//        public Vector<T> Velocity { get; set; }

//        public T MinX { get => Subtract(Position.X, Radius); }
//        public T MaxX { get => Add(Position.X, Radius); }
//        public T MinY { get => Subtract(Position.Y, Radius); }
//        public T MaxY { get => Add(Position.Y, Radius); }
//        public T MinZ { get => Subtract(Position.Z, Radius); }
//        public T MaxZ { get => Add(Position.Z, Radius); }
//    }

//    #endregion

//    class Window : GameWindow
//    {
//        #region Vertex Shader Source

//        string _vertextShaderSource =
//@"#version 330 core
//layout(location = 0) in vec3 aPosition;
//uniform mat4 model;
//uniform mat4 view;
//uniform mat4 projection;
//void main(void)
//{
//    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
//}
//";

//        #endregion

//        #region Fragment Shader Source

//        string _fragmentShaderSource =
//@"#version 330
//out vec4 outputColor;
//void main()
//{
//    outputColor = vec4(1.0, 1.0, 1.0, 1.0);
//}";

//        #endregion

//        #region Cube Geometry Data

//        //        G  ______________________  H
//        //          /|                    /|
//        //         / |                   / |
//        //        /  |                  /  |
//        //       /   |                 /   |
//        //    C /____|________________/ D  |
//        //      |    |                |    |
//        //      |    |                |    |
//        //      |    |     origin     |    |
//        //      |    |       +        |    |
//        //      |  E |________________|____| F
//        //      |    /                |    /
//        //      |   /                 |   / 
//        //      |  /                  |  /  
//        //      | /                   | /   
//        //    A |/____________________|/ B   

//        private readonly float[] vertices =
//        {
//            -.5f, -.5f, -.5f, // A
//             .5f, -.5f, -.5f, // B
//            -.5f,  .5f, -.5f, // C
//             .5f,  .5f, -.5f, // D
//            -.5f, -.5f,  .5f, // E
//             .5f, -.5f,  .5f, // F
//            -.5f,  .5f,  .5f, // G
//             .5f,  .5f,  .5f, // H
//        };

//        private readonly uint[] indices =
//        {
//            0, 1, 2, // A-B-C
//            1, 2, 3, // B-C-D

//            4, 5, 6, // E-F-G
//            5, 6, 7, // F-G-H

//            0, 1, 4, // A-B-E
//            1, 4, 5, // B-E-F

//            0, 2, 6, // A-C-G
//            0, 4, 6, // A-E-G

//            1, 3, 5, // B-D-F
//            3, 5, 7, // D-F-H

//            2, 3, 6, // C-D-G
//            3, 6, 7, // D-G-H
//        };

//        #endregion

//        IOmnitreeBounds<IObject3D<float>, float, float, float> _omnitree;
//        List<IObject3D<float>> _objects;

//        int _cubeElementBufferId;
//        int _cubeVertexBufferId;
//        int _cubeVertexArrayId;

//        int _cubeShaderProgramId;

//        private Camera camera;
//        private bool firstMove = true;
//        private Vector2 lastPos;

//        private double time;

//        public Window() : base() { }

//        protected override void OnLoad(EventArgs e)
//        {
//            #region Object Generation

//            _objects = new List<IObject3D<float>>();
//            for (int i = -50; i < 50; i += 10)
//            {
//                for (int j = -50; j < 50; j += 10)
//                {
//                    for (int k = -50; k < 50; k += 10)
//                    {
//                        Vector<float> position = new Vector<float>(i, j, k);
//                        Vector<float> velocity;
//                        if (i == 0 && j == 0 && k == 0)
//                        {
//                            velocity = new Vector<float>(1, 0, 0);
//                        }
//                        else
//                        {
//                            velocity = position.Negate().Normalize();
//                        }

//                        Cube<float> cube = new Cube<float>()
//                        {
//                            HalfLength = .5f,
//                            Position = position,
//                            Velocity = velocity,
//                        };
//                        _objects.Add(cube);
//                    }
//                }
//            }

//            #endregion

//            #region Omnitree Initialization

//            void GetBounds(IObject3D<float> boundingBox3D,
//                out float minX, out float maxX,
//                out float minY, out float maxY,
//                out float minZ, out float maxZ)
//            {
//                minX = boundingBox3D.MinX; maxX = boundingBox3D.MaxX;
//                minY = boundingBox3D.MinY; maxY = boundingBox3D.MaxY;
//                minZ = boundingBox3D.MinZ; maxZ = boundingBox3D.MaxZ;
//            }

//            bool Equate(IObject3D<float> a, IObject3D<float> b)
//            {
//                return
//                    a.MinX == b.MinX && a.MaxX == b.MaxX &&
//                    a.MinY == b.MinY && a.MaxY == b.MaxY &&
//                    a.MinZ == b.MinZ && a.MaxZ == b.MaxZ;
//            }

//            float MeanMinX(
//                Omnitree.Bounds<float, float, float> parentBounds,
//                Stepper<IObject3D<float>> stepper)
//            {
//                return Mean<float>(step => stepper(boundingBox => step(boundingBox.MinX)));
//            }

//            float MeanMinY(
//                Omnitree.Bounds<float, float, float> parentBounds,
//                Stepper<IObject3D<float>> stepper)
//            {
//                return Mean<float>(step => stepper(boundingBox => step(boundingBox.MinY)));
//            }

//            float MeanMinZ(
//                Omnitree.Bounds<float, float, float> parentBounds,
//                Stepper<IObject3D<float>> stepper)
//            {
//                return Mean<float>(step => stepper(boundingBox => step(boundingBox.MinZ)));
//            }

//            // construction of the omnitree
//            //_omnitree = new OmnitreeBoundsLinked<IBoundingBox3D<float>, float, float, float>(
//            //    GetBounds,
//            //    Equate,
//            //    Towel.Equate.Default, Towel.Equate.Default, Towel.Equate.Default,
//            //    Towel.Compare.Default, Towel.Compare.Default, Towel.Compare.Default,
//            //    MeanMinX, MeanMinY, MeanMinZ);

//            // This is terrible. Using the default constructor for the omnitree will result in the
//            // usage of the median algorithm to find the point of subdivision along each dimension.
//            // since we are using numeric types we shoudl at least be using the mean algorithm, but
//            // if we know the data and/or the spacial bounds, we can have even better algorithms than
//            // the mean. :)
//            _omnitree = new OmnitreeBoundsLinked<IObject3D<float>, float, float, float>(GetBounds);

//            foreach (var obj in _objects)
//            {
//                _omnitree.Add(obj);
//            }

//            #endregion

//            #region OpenGL Initialization

//            GL.ClearColor(Color.Teal);
//            GL.Enable(EnableCap.DepthTest);

//            _cubeVertexBufferId = GL.GenBuffer();
//            GL.BindBuffer(BufferTarget.ArrayBuffer, _cubeVertexBufferId);
//            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

//            _cubeElementBufferId = GL.GenBuffer();
//            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _cubeElementBufferId);
//            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

//            _cubeVertexArrayId = GL.GenVertexArray();
//            GL.BindVertexArray(_cubeVertexArrayId);

//            GL.BindBuffer(BufferTarget.ArrayBuffer, _cubeVertexBufferId);
//            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _cubeElementBufferId);

//            #endregion

//            #region Cube Shader Initialization

//            // Cube Vertex Shader
//            int cubeVertexShaderId = GL.CreateShader(ShaderType.VertexShader);
//            GL.ShaderSource(cubeVertexShaderId, _vertextShaderSource);
//            GL.CompileShader(cubeVertexShaderId);
//            GL.GetShader(cubeVertexShaderId, ShaderParameter.CompileStatus, out int vertexCode);
//            if (vertexCode != (int)All.True)
//            {
//                throw new Exception("Error compiling vertex shader.");
//            }

//            // Cube Fragment Shader
//            int cubeFragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
//            GL.ShaderSource(cubeFragmentShaderId, _fragmentShaderSource);
//            GL.CompileShader(cubeFragmentShaderId);
//            GL.GetShader(cubeFragmentShaderId, ShaderParameter.CompileStatus, out int fragementCode);
//            if (fragementCode != (int)All.True)
//            {
//                throw new Exception("Error compiling fragment shader.");
//            }

//            // Cube Shader Program
//            _cubeShaderProgramId = GL.CreateProgram();
//            GL.AttachShader(_cubeShaderProgramId, cubeVertexShaderId);
//            GL.AttachShader(_cubeShaderProgramId, cubeFragmentShaderId);
//            GL.LinkProgram(_cubeShaderProgramId);
//            GL.GetProgram(_cubeShaderProgramId, GetProgramParameterName.LinkStatus, out var shaderCode);
//            if (shaderCode != (int)All.True)
//            {
//                throw new Exception("Error compiling shader program.");
//            }
//            GL.DetachShader(_cubeShaderProgramId, cubeVertexShaderId);
//            GL.DetachShader(_cubeShaderProgramId, cubeFragmentShaderId);
//            GL.DeleteShader(cubeVertexShaderId);
//            GL.DeleteShader(cubeFragmentShaderId);
//            GL.UseProgram(_cubeShaderProgramId);

//            int vertexLocation = GL.GetAttribLocation(_cubeShaderProgramId, "aPosition");
//            GL.EnableVertexAttribArray(vertexLocation);
//            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

//            #endregion

//            #region Camera Initialization

//            camera = new Camera(Vector3.UnitZ * 3);
//            camera.AspectRatio = Width / (float)Height;
//            CursorVisible = false;

//            #endregion

//            base.OnLoad(e);
//        }

//        protected override void OnRenderFrame(FrameEventArgs e)
//        {
//            #region Rendering Code

//            time += 4.0 * e.Time;
//            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
//            Matrix4 projectionMatrix = camera.GetProjectionMatrix();
//            Matrix4 cameraMatrix = camera.GetViewMatrix();
//            foreach (IObject3D<float> @object in _objects)
//            {
//                if (@object is Cube<float>)
//                {
//                    #region Render Cube

//                    Matrix4 modelMatrix = Matrix4.CreateTranslation(@object.Position.X, @object.Position.Y, @object.Position.Z);
//                    GL.UseProgram(_cubeShaderProgramId);
//                    GL.UniformMatrix4(GL.GetUniformLocation(_cubeShaderProgramId, "model"), true, ref modelMatrix);
//                    GL.UniformMatrix4(GL.GetUniformLocation(_cubeShaderProgramId, "view"), true, ref cameraMatrix);
//                    GL.UniformMatrix4(GL.GetUniformLocation(_cubeShaderProgramId, "projection"), true, ref projectionMatrix);
//                    GL.BindVertexArray(_cubeVertexArrayId);
//                    GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

//                    #endregion
//                }
//                else if (@object is Sphere<float>)
//                {
//                    #region Render Sphere

//                    // I haven't added any spheres yet

//                    #endregion
//                }
//            }

//            #endregion

//            SwapBuffers();
//            base.OnRenderFrame(e);
//        }

//        protected override void OnUpdateFrame(FrameEventArgs e)
//        {
//            if (!Focused)
//            {
//                return;
//            }

//            //DateTime a = DateTime.Now;

//            #region Controls

//            var input = Keyboard.GetState();
//            if (input.IsKeyDown(Key.Escape))
//            {
//                Exit();
//            }
//            if (input.IsKeyDown(Key.W))
//                camera.Position += camera.Front * camera.Speed * (float)e.Time;
//            if (input.IsKeyDown(Key.S))
//                camera.Position -= camera.Front * camera.Speed * (float)e.Time;
//            if (input.IsKeyDown(Key.A))
//                camera.Position -= camera.Right * camera.Speed * (float)e.Time;
//            if (input.IsKeyDown(Key.D))
//                camera.Position += camera.Right * camera.Speed * (float)e.Time;
//            if (input.IsKeyDown(Key.Space))
//                camera.Position += camera.Up * camera.Speed * (float)e.Time;
//            if (input.IsKeyDown(Key.LShift))
//                camera.Position -= camera.Up * camera.Speed * (float)e.Time;
//            var mouse = Mouse.GetState();
//            if (firstMove)
//            {
//                lastPos = new Vector2(mouse.X, mouse.Y);
//                firstMove = false;
//            }
//            else
//            {
//                var deltaX = mouse.X - lastPos.X;
//                var deltaY = mouse.Y - lastPos.Y;
//                lastPos = new Vector2(mouse.X, mouse.Y);
//                camera.Yaw += deltaX * camera.Sensitivity;
//                camera.Pitch -= deltaY * camera.Sensitivity;
//            }

//            #endregion

//            #region Collision Detection

//            int iteration = 0;
//            foreach (IObject3D<float> obj in _objects)
//            {
//                iteration++;

//                Vector<float> position = obj.Position;
//                Vector<float> velocity = obj.Velocity;

//                Vector<float> temp = null;

//                const float radius = 3000f;

//                if (obj.Position.MagnitudeSquared > radius)
//                {
//                    obj.Position.Negate(ref velocity);
//                    velocity.Normalize(ref velocity);
//                }
//                else
//                {
//                    //foreach (var obj1 in _objects)
//                    //{
//                    //    foreach (var obj2 in _objects)
//                    //    {
//                    //        if (object.ReferenceEquals(obj1, obj2))
//                    //        {
//                    //            continue;
//                    //        }

//                    //        if (obj1.MinX > obj2.MaxX ||
//                    //            obj1.MaxX < obj2.MinX ||
//                    //            obj1.MinY > obj2.MaxY ||
//                    //            obj1.MaxY < obj2.MinY ||
//                    //            obj1.MinZ > obj2.MaxZ ||
//                    //            obj1.MaxZ < obj2.MinZ)
//                    //        {
//                    //            obj2.Position.Subtract(obj.Position, ref temp);
//                    //            if (temp.MagnitudeSquared == 0)
//                    //            {
//                    //                velocity.X = 1;
//                    //                velocity.Y = 0;
//                    //                velocity.Z = 0;
//                    //            }
//                    //            else
//                    //            {
//                    //                temp.Normalize(ref velocity);
//                    //            }
//                    //        }
//                    //    }
//                    //}

//                    _omnitree.StepperOverlapped(
//                        x =>
//                        {
//                            if (object.ReferenceEquals(x, obj))
//                            {
//                                return;
//                            }

//                            x.Position.Subtract(obj.Position, ref temp);
//                            if (temp.MagnitudeSquared == 0)
//                            {
//                                velocity.X = 1;
//                                velocity.Y = 0;
//                                velocity.Z = 0;
//                            }
//                            else
//                            {
//                                temp.Negate(ref temp);
//                                temp.Normalize(ref velocity);
//                            }
//                        },
//                        obj.MinX, obj.MaxX,
//                        obj.MinY, obj.MaxY,
//                        obj.MinZ, obj.MaxZ);
//                }

//                position.Add(velocity * (float)e.Time, ref position);
//            }

//            // this is not ideal... we shouldn't have to rebuild the omnitree every frame,
//            // but the "update" function has a bug at the moment
//            _omnitree.Clear();
//            foreach (var obj in _objects)
//            {
//                _omnitree.Add(obj);
//            }

//            #endregion

//            //System.Windows.Forms.MessageBox.Show((DateTime.Now - a).ToString());

//            base.OnUpdateFrame(e);
//        }

//        #region Other Necessary Overrides

//        protected override void OnMouseMove(MouseMoveEventArgs e)
//        {
//            if (Focused)
//            {
//                Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
//            }
//            base.OnMouseMove(e);
//        }

//        protected override void OnMouseWheel(MouseWheelEventArgs e)
//        {
//            camera.Fov -= e.DeltaPrecise;
//            base.OnMouseWheel(e);
//        }

//        protected override void OnResize(EventArgs e)
//        {
//            GL.Viewport(0, 0, Width, Height);
//            camera.AspectRatio = Width / (float)Height;
//            base.OnResize(e);
//        }

//        protected override void OnUnload(EventArgs e)
//        {
//            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
//            GL.BindVertexArray(0);
//            GL.UseProgram(0);
//            GL.DeleteBuffer(_cubeVertexBufferId);
//            GL.DeleteVertexArray(_cubeVertexArrayId);
//            GL.DeleteProgram(_cubeShaderProgramId);
//            base.OnUnload(e);
//        }

//        #endregion
//    }
//}

#endregion