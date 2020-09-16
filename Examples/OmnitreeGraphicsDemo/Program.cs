using Silk.NET.Input;
using Silk.NET.Input.Common;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Towel;
using Towel.DataStructures;
using Towel.Measurements;
using static Towel.Statics;
using static Towel.Measurements.MeasurementsSyntax;

namespace OmnitreeGraphicsDemo
{
	class Cube
	{
		public float HalfLength;

		public Vector3 Color { get; set; }

		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }

		public float MinX { get => Position.X - HalfLength; }
		public float MaxX { get => Position.X + HalfLength; }
		public float MinY { get => Position.Y - HalfLength; }
		public float MaxY { get => Position.Y + HalfLength; }
		public float MinZ { get => Position.Z - HalfLength; }
		public float MaxZ { get => Position.Z + HalfLength; }
	}

	class Program
	{
		#region From Silk.Net

		private static IWindow window;
		private static GL Gl;
		private static IKeyboard primaryKeyboard;

		private const int Width = 800;
		private const int Height = 700;

		private static Vector3 CameraPosition = new Vector3(0.0f, 0.0f, 3.0f);
		private static Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
		private static Vector3 CameraUp = Vector3.UnitY;
		private static Vector3 CameraDirection = Vector3.Zero;
		private static float CameraYaw = -90f;
		private static float CameraPitch = 0f;
		private static float CameraZoom = 45f;
		private static PointF LastMousePosition;

		#endregion

		#region Vertex Shader Source

		readonly static string _vertextShaderSource =
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

		readonly static string _fragmentShaderSource =
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

		private static readonly float[] vertices =
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

		private static readonly uint[] indices =
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

		static IOmnitreePoints<Cube, float, float, float> _omnitree;
		static ListArray<Cube> _objects;

		static uint _cubeElementBufferId;
		static uint _cubeVertexBufferId;
		static uint _cubeVertexArrayId;

		static uint _cubeShaderProgramId;

		private static readonly Random random = new Random();


		private static void Main()
		{
			Console.WriteLine("This example is still in heavy development.");
			ConsoleHelper.PromptPressToContinue();

			var options = WindowOptions.Default;
			options.Size = new Size(Width, Height);
			options.Title = "Omnitree Graphics Demo";
			window = Window.Create(options);

			window.Load += OnLoad;
			window.Update += OnUpdate;
			window.Render += OnRender;
			window.Closing += OnClose;

			window.Run();
		}

		private unsafe static void OnLoad()
		{
			IInputContext input = window.CreateInput();
			primaryKeyboard = input.Keyboards.FirstOrDefault();
			if (primaryKeyboard != null)
			{
				primaryKeyboard.KeyDown += KeyDown;
			}
			for (int i = 0; i < input.Mice.Count; i++)
			{
				input.Mice[i].Cursor.CursorMode = CursorMode.Raw;
				input.Mice[i].MouseMove += OnMouseMove;
				input.Mice[i].Scroll += OnMouseWheel;
			}

			Gl = GL.GetApi(window);

			#region Object Generation

			_objects = new ListArray<Cube>();
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

						Vector3 position = new Vector3(i, j, k);
						Vector3 velocity;
						Vector3 color;

						if (i == 0 && j == 0 && k == 0)
						{
							velocity = Vector3.Normalize(new Vector3(
								(float)random.NextDouble(),
								(float)random.NextDouble(),
								(float)random.NextDouble()));
						}
						else
						{
							velocity = Vector3.Normalize(-position);
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

			static void Locate(Cube obj,
				out float x,
				out float y,
				out float z)
			{
				x = obj.Position.X;
				y = obj.Position.Y;
				z = obj.Position.Z;
			}

			// construction of the omnitree
			_omnitree = new OmnitreePointsLinked<Cube, float, float, float>(
				Locate,
				// Override the subdivision algorithms to use mean instead of median. We can do this because we are 
				// working with numerical values. We could also speed things up even more in this case by dividing
				// the bounding space (using the "bounds" parameter below rather than "stepper"), but I wanted to
				// show the mean algorithm in this example.
				subdivisionOverride1: (bounds, stepper) => Mean<float>(step => stepper(boundingBox => step(boundingBox.MinX))),
				subdivisionOverride2: (bounds, stepper) => Mean<float>(step => stepper(boundingBox => step(boundingBox.MinY))),
				subdivisionOverride3: (bounds, stepper) => Mean<float>(step => stepper(boundingBox => step(boundingBox.MinZ))));

			foreach (var obj in _objects)
			{
				_omnitree.Add(obj);
			}

			#endregion

			#region OpenGL Initialization

			Gl.ClearColor(Color.Teal);
			Gl.Enable(EnableCap.DepthTest);

			_cubeVertexBufferId = Gl.GenBuffer();
			Gl.BindBuffer(BufferTargetARB.ArrayBuffer, _cubeVertexBufferId);
			fixed (void* p = vertices)
			{
				Gl.BufferData(GLEnum.ArrayBuffer, (UIntPtr)(vertices.Length * sizeof(float)), p, GLEnum.StaticDraw);
			}

			_cubeElementBufferId = Gl.GenBuffer();
			Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _cubeElementBufferId);
			fixed (void* p = indices)
			{
				Gl.BufferData(GLEnum.ElementArrayBuffer, (UIntPtr)(indices.Length * sizeof(uint)), p, GLEnum.StaticDraw);
			}

			_cubeVertexArrayId = Gl.GenVertexArray();
			Gl.BindVertexArray(_cubeVertexArrayId);

			Gl.BindBuffer(BufferTargetARB.ArrayBuffer, _cubeVertexBufferId);
			Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _cubeElementBufferId);

			#endregion

			#region Cube Shader Initialization

			// Cube Vertex Shader
			uint cubeVertexShaderId = Gl.CreateShader(ShaderType.VertexShader);
			Gl.ShaderSource(cubeVertexShaderId, _vertextShaderSource);
			Gl.CompileShader(cubeVertexShaderId);
			string cubeVertexShaderInfoLog = Gl.GetShaderInfoLog(cubeVertexShaderId);
			if (!string.IsNullOrWhiteSpace(cubeVertexShaderInfoLog))
			{
				throw new Exception($"Error compiling vertex shader. {Environment.NewLine}{cubeVertexShaderInfoLog}");
			}

			// Cube Fragment Shader
			uint cubeFragmentShaderId = Gl.CreateShader(ShaderType.FragmentShader);
			Gl.ShaderSource(cubeFragmentShaderId, _fragmentShaderSource);
			Gl.CompileShader(cubeFragmentShaderId);
			string cubeFragmentShaderInfoLog = Gl.GetShaderInfoLog(cubeFragmentShaderId);
			if (!string.IsNullOrWhiteSpace(cubeFragmentShaderInfoLog))
			{
				throw new Exception($"Error compiling fragment shader. {Environment.NewLine}{cubeFragmentShaderInfoLog}");
			}

			// Cube Shader Program
			_cubeShaderProgramId = Gl.CreateProgram();
			Gl.AttachShader(_cubeShaderProgramId, cubeVertexShaderId);
			Gl.AttachShader(_cubeShaderProgramId, cubeFragmentShaderId);
			Gl.LinkProgram(_cubeShaderProgramId);
			Gl.GetProgram(_cubeShaderProgramId, GLEnum.LinkStatus, out var status);
			if (status == 0)
			{
				throw new Exception("Error compiling shader program.");
			}
			Gl.DetachShader(_cubeShaderProgramId, cubeVertexShaderId);
			Gl.DetachShader(_cubeShaderProgramId, cubeFragmentShaderId);
			Gl.DeleteShader(cubeVertexShaderId);
			Gl.DeleteShader(cubeFragmentShaderId);
			Gl.UseProgram(_cubeShaderProgramId);

			int vertexLocation = Gl.GetAttribLocation(_cubeShaderProgramId, "aPosition");
			Gl.EnableVertexAttribArray((uint)vertexLocation);
			Gl.VertexAttribPointer((uint)vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

			#endregion
		}

		private static unsafe void OnUpdate(double deltaTime)
		{
			var moveSpeed = 2.5f * (float)deltaTime;

			if (primaryKeyboard.IsKeyPressed(Key.W))
			{
				CameraPosition += moveSpeed * CameraFront;
			}
			if (primaryKeyboard.IsKeyPressed(Key.S))
			{
				CameraPosition -= moveSpeed * CameraFront;
			}
			if (primaryKeyboard.IsKeyPressed(Key.A))
			{
				CameraPosition -= Vector3.Normalize(Vector3.Cross(CameraFront, CameraUp)) * moveSpeed;
			}
			if (primaryKeyboard.IsKeyPressed(Key.D))
			{
				CameraPosition += Vector3.Normalize(Vector3.Cross(CameraFront, CameraUp)) * moveSpeed;
			}

			#region Collision Detection

			int iteration = 0;
			foreach (Cube obj in _objects)
			{
				iteration++;
				const float radius = 1500f;

				if (obj.Position.LengthSquared() > radius)
				{
					obj.Velocity = Vector3.Normalize(-obj.Position);
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

							Vector3 temp = obj.Position - x.Position;
							if (temp.LengthSquared() == 0)
							{
								obj.Velocity = Vector3.Normalize(new Vector3(
									(float)random.NextDouble(),
									(float)random.NextDouble(),
									(float)random.NextDouble()));
							}
							else
							{
								obj.Velocity = Vector3.Normalize(temp);
							}
						},
						obj.Position.X - 1, obj.Position.X + 1,
						obj.Position.Y - 1, obj.Position.Y + 1,
						obj.Position.Z - 1, obj.Position.Z + 1);
				}

				obj.Position += obj.Velocity * (float)deltaTime * 4;
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

		}

		private static unsafe void OnRender(double deltaTime)
		{
			Gl.Enable(EnableCap.DepthTest);
			Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

			float fov = Measurement.Convert(CameraZoom, Degrees, Radians);
			Matrix4x4 projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fov, (float)Width / Height, 0.1f, 100.0f);
			Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(CameraPosition, CameraPosition + CameraFront, CameraUp);

			foreach (Cube cube in _objects)
			{
				Vector3 color = cube.Color;
				Matrix4x4 modelMatrix = Matrix4x4.CreateTranslation(cube.Position.X, cube.Position.Y, cube.Position.Z);
				Gl.UseProgram(_cubeShaderProgramId);
				Gl.Uniform3(Gl.GetUniformLocation(_cubeShaderProgramId, "color"), ref color);
				Gl.UniformMatrix4(Gl.GetUniformLocation(_cubeShaderProgramId, "model"), 1, true, (float*)&modelMatrix);
				Gl.UniformMatrix4(Gl.GetUniformLocation(_cubeShaderProgramId, "view"), 1, true, (float*)&viewMatrix);
				Gl.UniformMatrix4(Gl.GetUniformLocation(_cubeShaderProgramId, "projection"), 1, true, (float*)&projectionMatrix);
				Gl.BindVertexArray(_cubeVertexArrayId);
				Gl.DrawElements(GLEnum.Triangles, (uint)indices.Length, GLEnum.UnsignedInt, default);
			}
		}

		private static unsafe void OnMouseMove(IMouse mouse, PointF position)
		{
			var lookSensitivity = 0.1f;
			if (LastMousePosition == default) { LastMousePosition = position; }
			else
			{
				var xOffset = (position.X - LastMousePosition.X) * lookSensitivity;
				var yOffset = (position.Y - LastMousePosition.Y) * lookSensitivity;
				LastMousePosition = position;
				CameraYaw += xOffset;
				CameraPitch -= yOffset;
				CameraPitch = Math.Clamp(CameraPitch, -89.0f, 89.0f);
				CameraDirection.X = MathF.Cos(Measurement.Convert(CameraYaw, Degrees, Radians)) * MathF.Cos(Measurement.Convert(CameraPitch, Degrees, Radians));
				CameraDirection.Y = MathF.Sin(Measurement.Convert(CameraPitch, Degrees, Radians));
				CameraDirection.Z = MathF.Sin(Measurement.Convert(CameraYaw, Degrees, Radians)) * MathF.Cos(Measurement.Convert(CameraPitch, Degrees, Radians));
				CameraFront = Vector3.Normalize(CameraDirection);
			}
		}

		private static unsafe void OnMouseWheel(IMouse mouse, ScrollWheel scrollWheel)
		{
			//We don't want to be able to zoom in too close or too far away so clamp to these values
			CameraZoom = Math.Clamp(CameraZoom - scrollWheel.Y, 1.0f, 45f);
		}

		private static void OnClose()
		{
			Gl.BindBuffer(GLEnum.ArrayBuffer, 0);
			Gl.BindVertexArray(0);
			Gl.UseProgram(0);
			Gl.DeleteBuffer(_cubeVertexBufferId);
			Gl.DeleteVertexArray(_cubeVertexArrayId);
			Gl.DeleteProgram(_cubeShaderProgramId);
		}

		private static void KeyDown(IKeyboard keyboard, Key key, int arg3)
		{
			if (key == Key.Escape)
			{
				window.Close();
			}
		}
	}
}