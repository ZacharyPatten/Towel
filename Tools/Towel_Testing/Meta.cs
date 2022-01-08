using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Towel_Testing
{
	[TestClass]
	public class Meta_Testing
	{
		#region Type Testing

#pragma warning disable SA1121 // Use built-in type alias

		[TestMethod]
		public void Type_ConvertToCsharpSource()
		{
			{ // showGenericParameters = false
				var tests = new (Type Type, string String)[]
				{
					(typeof(System.Int32), "System.Int32"),
					(typeof(Towel.Mathematics.Symbolics.Expression), "Towel.Mathematics.Symbolics.Expression"),
					(typeof(Towel.Mathematics.Symbolics.Constant<System.Int32>), "Towel.Mathematics.Symbolics.Constant<System.Int32>"),
					(typeof(Towel_Testing.A.B.C), "Towel_Testing.A.B.C"),
					(typeof(Towel_Testing.A.D<System.Int32>.E<System.Int32>), "Towel_Testing.A.D<System.Int32>.E<System.Int32>"),
					(typeof(Towel_Testing.A.D<>.E<>), "Towel_Testing.A.D<>.E<>"),
					(typeof(System.Collections.Generic.List<(System.String @event, System.Object @class)>), "System.Collections.Generic.List<System.ValueTuple<System.String, System.Object>>"),
					(typeof(System.Int32?), "System.Nullable<System.Int32>"),
					(typeof(Towel_Testing.A.D<System.Object>.E<System.String>), "Towel_Testing.A.D<System.Object>.E<System.String>"),
					(typeof(Towel.DataStructures.IOmnitreePoints<,,,>), "Towel.DataStructures.IOmnitreePoints<,,,>"),
					(typeof(Towel.DataStructures.IOmnitreePoints<System.Object, System.Int32, System.String, System.Double>), "Towel.DataStructures.IOmnitreePoints<System.Object, System.Int32, System.String, System.Double>"),
					(typeof(System.ValueTuple<System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.ValueTuple<System.Int32, System.Int32>>), "System.ValueTuple<System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.ValueTuple<System.Int32, System.Int32>>"),
					(typeof(Towel.StepStatus), "Towel.StepStatus"),
					(typeof(System.Int32*), "System.Int32*"),
					(typeof(System.Int32**), "System.Int32**"),
					(typeof(System.Span<System.Int32>), "System.Span<System.Int32>"),
					(typeof(System.Int32[]), "System.Int32[]"),
					(typeof(System.Int32[][]), "System.Int32[][]"),
					(typeof(System.Int32[][][]), "System.Int32[][][]"),
					(typeof(System.Int32[,]), "System.Int32[,]"),
					(typeof(System.Int32[,,]), "System.Int32[,,]"),
					(typeof(System.Int32[,,,]), "System.Int32[,,,]"),
					(typeof(System.Int32[][,,,][]), "System.Int32[][,,,][]"),
					(typeof(System.Int32[,,,][][,,,]), "System.Int32[,,,][][,,,]"),
					// special syntaxes
					(typeof((int, int, int, int, int, int, int, int, int)), "System.ValueTuple<System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.ValueTuple<System.Int32, System.Int32>>"),
					(typeof((int A, int B)), "System.ValueTuple<System.Int32, System.Int32>"),
					(typeof(int), "System.Int32"),
					(typeof(string), "System.String"),
					(typeof(short), "System.Int16"),
				};
				foreach (var test in tests)
				{
					string temp;
					try
					{
						temp = test.Type.ConvertToCSharpSource();
						Assert.IsTrue(temp.Equals(test.String), test.Type.ToString());
					}
					catch
					{
						Debugger.Break();
						temp = test.Type.ConvertToCSharpSource();
						Assert.IsTrue(temp.Equals(test.String), test.Type.ToString());
					}
				}
			}

			{ // showGenericParameters = true
				var tests = new (Type Type, string String)[]
				{
					(typeof(System.Collections.Generic.List<>), "System.Collections.Generic.List<T>"),
					(typeof(Towel.Mathematics.Symbolics.Constant<>), "Towel.Mathematics.Symbolics.Constant<T>"),
					(typeof(Towel_Testing.A.D<>.E<>), "Towel_Testing.A.D<TA>.E<TB>"),
					(typeof(Towel.DataStructures.IOmnitreePoints<,,,>), "Towel.DataStructures.IOmnitreePoints<T, Axis1, Axis2, Axis3>"),
					(typeof(System.Span<>), "System.Span<T>"),
				};
				foreach (var test in tests)
				{
					string temp;
					try
					{
						temp = test.Type.ConvertToCSharpSource(true);
						Assert.IsTrue(temp.Equals(test.String), test.Type.ToString());
					}
					catch
					{
						Debugger.Break();
						temp = test.Type.ConvertToCSharpSource(true);
						Assert.IsTrue(temp.Equals(test.String), test.Type.ToString());
					}
				}
			}

			{
				MethodInfo methodInfo = typeof(Meta_Testing).GetMethod(nameof(Type_ConvertToCsharpSource_Test1))!;
				Type parameterType = methodInfo.GetParameters()[0].ParameterType;
				{
					string actual = parameterType.ConvertToCSharpSource(true);
					string expected = "System.ValueTuple<System.ValueTuple<T>>";
					Assert.IsTrue(actual.Equals(expected), parameterType.ToString());
				}
				{
					string actual = parameterType.ConvertToCSharpSource(false);
					string expected = "System.ValueTuple<System.ValueTuple<>>";
					Assert.IsTrue(actual.Equals(expected), parameterType.ToString());
				}
			}
			{
				MethodInfo methodInfo = typeof(Meta_Testing).GetMethod(nameof(Type_ConvertToCsharpSource_Test2))!;
				Type parameterType = methodInfo.GetParameters()[0].ParameterType;
				{
					string actual = parameterType.ConvertToCSharpSource(true);
					string expected = "System.ValueTuple<System.ValueTuple<T, System.Int32>>";
					Assert.IsTrue(actual.Equals(expected), parameterType.ToString());
				}
				{
					#warning TODO: review this test case
					//string actual = parameterType.ConvertToCSharpSource(false);
					//string expected = "System.ValueTuple<System.ValueTuple<,>>";
					//Assert.IsTrue(actual.Equals(expected), parameterType.ToString());
				}
			}
		}

		public static void Type_ConvertToCsharpSource_Test1<T>(System.ValueTuple<System.ValueTuple<T>> tuple) { }

		public static void Type_ConvertToCsharpSource_Test2<T>(System.ValueTuple<System.ValueTuple<T, System.Int32>> tuple) { }

#pragma warning restore SA1121 // Use built-in type alias

		#endregion

		#region XML Documentation Testing

		internal static readonly object xmlDocumentationlock = new();

		[TestMethod]
		public void Meta_XmlCache_Tests()
		{
			lock (xmlDocumentationlock)
			{
				Meta.ClearXmlDocumentation();
				Assert.IsTrue(Meta.loadedXmlDocumentation.Count is 0);
				Meta.LoadXmlDocumentation(File.ReadAllText("Towel_Testing.xml"));
				Assert.IsTrue(Meta.loadedXmlDocumentation.Count > 0);
				Meta.ClearXmlDocumentation();
				Assert.IsTrue(Meta.loadedXmlDocumentation.Count is 0);
			}
		}

		[TestMethod]
		public void MemberInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(MemberInfo)!));
			}
		}

		[TestMethod]
		public void MethodInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(MethodInfo)!));

				Meta.ClearXmlDocumentation();

				#region GitHub Issue 93
				{
					BindingFlags bf = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
					foreach (MemberInfo memberInfo in typeof(XmlDocumentationFromMethod.GitHubIssue93Class<>).GetMembers(bf))
					{
						bool shouldHaveDocumentation = memberInfo.GetTag("Test") is (true, true);
						try
						{
							string? xmlDocumentation = memberInfo.GetDocumentation();
							Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation) == shouldHaveDocumentation);
						}
						catch
						{
							Debugger.Break();
							string? xmlDocumentation = memberInfo.GetDocumentation();
							Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation) == shouldHaveDocumentation);
						}
					}
				}
				#endregion

				#region GitHub Issue 52

				try
				{
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				try
				{
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method2))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(xmlDocumentation is null);
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method2))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(xmlDocumentation is null);
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				try
				{
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method3))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method3))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				try
				{
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method4))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(xmlDocumentation is null);
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class<int>).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class<int>.GitHubIssue52Method4))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(xmlDocumentation is null);
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				try
				{
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class.GitHubIssue52Method1))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class.GitHubIssue52Method1))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				try
				{
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class.GitHubIssue52Method2))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(xmlDocumentation is null);
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					MethodInfo methodInfo = typeof(XmlDocumentationFromMethod.GitHubIssue52Class).GetMethod(nameof(XmlDocumentationFromMethod.GitHubIssue52Class.GitHubIssue52Method2))!;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(xmlDocumentation is null);
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				#endregion

				#region Delegate

				try
				{
					Action<int> action = new XmlDocumentationFromMethod.GitHubIssue52Class<int>().GitHubIssue52Method;
					MethodInfo methodInfo = action.Method;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					Action<int> action = new XmlDocumentationFromMethod.GitHubIssue52Class<int>().GitHubIssue52Method;
					MethodInfo methodInfo = action.Method;
					string? xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}

				#endregion

				foreach (MethodInfo methodInfo in Assembly.GetExecutingAssembly().GetMethodInfosWithAttribute<XmlDocumentationFromMethodAttribute>())
				{
					try
					{
						string? xmlDocumentation = methodInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
					catch
					{
						Debugger.Break();
						string? xmlDocumentation = methodInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
				}
			}
		}

		[TestMethod]
		public void Type_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(Type)!));

				Meta.ClearXmlDocumentation();

				foreach (Type type in Assembly.GetExecutingAssembly().GetTypesWithAttribute<XmlDocumentationFromTypeAttribute>())
				{
					try
					{
						string? xmlDocumentation = type.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)type).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
					catch
					{
						Debugger.Break();
						string? xmlDocumentation = type.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)type).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
				}
			}
		}

		[TestMethod]
		public void FieldInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(FieldInfo)!));

				Meta.ClearXmlDocumentation();

				foreach (FieldInfo fieldInfo in Assembly.GetExecutingAssembly().GetFieldInfosWithAttribute<XmlDocumentationFromFieldAttribute>())
				{
					try
					{
						string? xmlDocumentation = fieldInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)fieldInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
					catch
					{
						Debugger.Break();
						string? xmlDocumentation = fieldInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)fieldInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
				}
			}
		}

		[TestMethod]
		public void ConstructorInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(ConstructorInfo)!));

				Meta.ClearXmlDocumentation();

				foreach (ConstructorInfo constructorInfo in Assembly.GetExecutingAssembly().GetConstructorInfosWithAttribute<XmlDocumentationFromConstructorAttribute>())
				{
					try
					{
						string? xmlDocumentation = constructorInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)constructorInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
					catch
					{
						Debugger.Break();
						string? xmlDocumentation = constructorInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)constructorInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
				}
			}
		}

		[TestMethod]
		public void PropertyInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(PropertyInfo)!));

				Meta.ClearXmlDocumentation();

				foreach (PropertyInfo propertyInfo in Assembly.GetExecutingAssembly().GetPropertyInfosWithAttribute<XmlDocumentationFromPropertyAttribute>())
				{
					try
					{
						string? xmlDocumentation = propertyInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)propertyInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
					catch
					{
						Debugger.Break();
						string? xmlDocumentation = propertyInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)propertyInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
				}
			}
		}

		[TestMethod]
		public void EventInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(EventInfo)!));

				Meta.ClearXmlDocumentation();

				foreach (EventInfo eventInfo in Assembly.GetExecutingAssembly().GetEventInfosWithAttribute<XmlDocumentationFromEventAttribute>())
				{
					try
					{
						string? xmlDocumentation = eventInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)eventInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
					catch
					{
						Debugger.Break();
						string? xmlDocumentation = eventInfo.GetDocumentation();
						Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
						string? xmlDocumentationMember = ((MemberInfo)eventInfo).GetDocumentation();
						Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
					}
				}
			}
		}

		[TestMethod]
		public void ParameterInfo_GetDocumentation()
		{
			lock (xmlDocumentationlock)
			{
				Assert.ThrowsException<ArgumentNullException>(() => Meta.GetDocumentation(default(ParameterInfo)!));

				Meta.ClearXmlDocumentation();

				MethodInfo? methodInfo = typeof(XmlDocumentationFromParameter).GetMethod("Test1");
				if (methodInfo is not null)
				{
					string? Test1_a = methodInfo.GetParameters()[0].GetDocumentation();
					Assert.IsTrue(Test1_a == "<param name=\"a\">TEST a</param>");
				}
				else
				{
					Assert.Fail($"XmlDocumentationFromParameter.Test1 MethodInfo is null");
				}
			}
		}

		#endregion

		#region MethodBase Testing

		[TestMethod]
		public void MethodBase_IsLocalFunction()
		{
#pragma warning disable SA1501 // Statement should not be on a single line

			void a() { }

			Assert.IsTrue(new Action(a).Method.IsLocalFunction());

			long b() { return 0; }
			Assert.IsTrue(new Func<long>(b).Method.IsLocalFunction());

			void c(int c1) { }
			Assert.IsTrue(new Action<int>(c).Method.IsLocalFunction());

			void d<T>() { }
			Assert.IsTrue(new Action(d<int>).Method.IsLocalFunction());

			static void e() { }
			Assert.IsTrue(new Action(e).Method.IsLocalFunction());

			MethodInfo f()
			{
				void g() { }
				return new Action(g).Method;
			}
			Assert.IsTrue(f().IsLocalFunction());

			MethodInfo h()
			{
				MethodInfo i()
				{
					void j() { }
					return new Action(j).Method;
				}
				return i();
			}
			Assert.IsTrue(h().IsLocalFunction());

			Assert.IsFalse(new Action(MethodBase_IsLocalFunction).Method.IsLocalFunction());

#pragma warning restore SA1501 // Statement should not be on a single line
		}

		#endregion

		#region HasImplicitCast

		[TestMethod]
		public void Meta_HasImplicitCast()
		{
			Assert.Inconclusive("This Test and the underlaying methods are still under consideration.");

			Assert.IsTrue(Meta.HasImplicitCast<int, float>());
			Assert.IsTrue(Meta.HasImplicitCast<int, double>());
			Assert.IsTrue(Meta.HasImplicitCast<int, decimal>());

			Assert.IsFalse(Meta.HasExplicitCast<int, float>());
			Assert.IsFalse(Meta.HasExplicitCast<int, double>());
			Assert.IsFalse(Meta.HasExplicitCast<int, decimal>());

			Assert.IsFalse(Meta.HasImplicitCast<float, int>());
			Assert.IsFalse(Meta.HasImplicitCast<double, int>());
			Assert.IsFalse(Meta.HasImplicitCast<decimal, int>());

			Assert.IsTrue(Meta.HasExplicitCast<float, int>());
			Assert.IsTrue(Meta.HasExplicitCast<double, int>());
			Assert.IsTrue(Meta.HasExplicitCast<decimal, int>());

			Assert.IsFalse(Meta.HasExplicitCast<int, string>());
			Assert.IsFalse(Meta.HasExplicitCast<int, string>());
			Assert.IsFalse(Meta.HasExplicitCast<int, string>());

			Assert.IsFalse(Meta.HasImplicitCast<int, string>());
			Assert.IsFalse(Meta.HasImplicitCast<int, string>());
			Assert.IsFalse(Meta.HasImplicitCast<int, string>());
		}

		#endregion
	}

	#region Testing Types

	public class A
	{
		public class B
		{
			public class C
			{

			}
		}

		public class D<TA>
		{
			public class E<TB>
			{

			}
		}
	}

	#endregion

	#region XML Documentation Types

#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS0067 // The event is never used
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1314 // Type parameter names should begin with T
#pragma warning disable SA1618 // Generic type parameters should be documented
#pragma warning disable SA1611 // Element parameters should be documented

	#region XML Documentation From MethodInfo

	[AttributeUsage(AttributeTargets.Method)]
	public class XmlDocumentationFromMethodAttribute : Attribute { }

	public class XmlDocumentationFromMethod
	{
		public class GitHubIssue93Class<T>
		{
			/// <summary>hello world</summary>
			[Tag("Test", true)]
			private int private_int_property { get; set; }
			/// <summary>hello world</summary>
			[Tag("Test", true)]
			private string? private_string_property { get; set; }

			/// <summary>hello world</summary>
			[Tag("Test", true)]
			public int public_int_property { get; set; }
			/// <summary>hello world</summary>
			[Tag("Test", true)]
			public string? public_string_property { get; set; }
		}

		public class GitHubIssue52Class<T1>
		{
			/// <summary>hello world</summary>
			public void GitHubIssue52Method(T1 x) { }

			public void GitHubIssue52Method2(T1 x) { }

			/// <summary>hello world</summary>
			public void GitHubIssue52Method3<T2>(T1 a, T2 b) { }

			public void GitHubIssue52Method4<T2>(T1 a, T2 b) { }
		}

		public class GitHubIssue52Class
		{
			/// <summary>hello world</summary>
			public void GitHubIssue52Method1<T2>(T2 b) { }

			public void GitHubIssue52Method2<T2>(T2 b) { }
		}

		/// <summary>Test A</summary>
		/// <returns>object</returns>
		[XmlDocumentationFromMethod]
		public object DocumentedMethodReturns() => throw new Exception();

		/// <summary>Test B</summary>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod() => throw new Exception();

		/// <summary>Test C</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(object a) => throw new Exception();

		/// <summary>Test D</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(object a, object b) => throw new Exception();

		/// <summary>Test E</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(object a, object b, object c) => throw new Exception();

		/// <summary>Test F</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(params object[] a) => throw new Exception();

		/// <summary>Test G</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethodOut(out object a) => throw new Exception();

		/// <summary>Test H</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethodRef(ref object a) => throw new Exception();

		/// <summary>Test I</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethodIn(in object a) => throw new Exception();

		/// <summary>Test J</summary>
		/// <typeparam name="A">A</typeparam>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>() => throw new Exception();

		/// <summary>Test K</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a) => throw new Exception();

		/// <summary>Test L</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a, object b) => throw new Exception();

		/// <summary>Test M</summary>
		/// <typeparam name="A">A</typeparam>
		/// <typeparam name="B">B</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A, B>(A a, B b) => throw new Exception();

		/// <summary>Test N</summary>
		/// <typeparam name="A">A</typeparam>
		/// <typeparam name="B">B</typeparam>
		/// <param name="b">b</param>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A, B>(B b, A a) => throw new Exception();

		/// <summary>Test U</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public unsafe void DocumentedMethod(int* a) => throw new Exception();

		public class NestedType
		{
			/// <summary>Test O</summary>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod() => throw new Exception();
		}

		public class NestedGenericType<A>
		{
			/// <summary>Test P</summary>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod() => throw new Exception();

			/// <summary>Test W</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod(A a) => throw new Exception();

			/// <summary>Test X</summary>
			/// <typeparam name="B">B</typeparam>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod<B>(A a, B b) => throw new Exception();
		}

		/// <summary>Test Q</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(NestedType a) => throw new Exception();

		/// <summary>Test R</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(NestedGenericType<A> a) => throw new Exception();

		/// <summary>Test S</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a, Action<Action<A>> b, A[] c) => throw new Exception();

		/// <summary>Test T</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a, Action<Action<A>> b, A[] c, A[,] d) => throw new Exception();

		/// <summary>Test V</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public unsafe void DocumentedMethod(ref int* a) => throw new Exception();

		public class NestedGenericType2<A, B, C>
		{
			/// <summary>Test W</summary>
			/// <typeparam name="D">D</typeparam>
			/// <typeparam name="E">E</typeparam>
			/// <typeparam name="F">F</typeparam>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			/// <param name="c">c</param>
			/// <param name="d">d</param>
			/// <param name="e">e</param>
			/// <param name="f">f</param>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod<D, E, F>(A a, B b, C c, D d, E e, F f) => throw new Exception();

			/// <summary>Test X</summary>
			/// <typeparam name="D">D</typeparam>
			/// <typeparam name="E">E</typeparam>
			/// <typeparam name="F">F</typeparam>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			/// <param name="c">c</param>
			/// <param name="d">d</param>
			/// <param name="e">e</param>
			/// <param name="f">f</param>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod<D, E, F>(A[] a, B[,] b, C[,,] c, D[] d, E[,] e, F[,,] f) => throw new Exception();

			/// <summary>Test DD.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static implicit operator bool(NestedGenericType2<A, B, C> a) => throw new Exception();

			/// <summary>Test EE.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static explicit operator int(NestedGenericType2<A, B, C> a) => throw new Exception();

			/// <summary>Test HH.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static implicit operator NestedGenericType2<A, B, C>(bool a) => throw new Exception();

			/// <summary>Test II.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static explicit operator NestedGenericType2<A, B, C>(int a) => throw new Exception();

			public class NestedNestedGenericType3<D, E, F>
			{
				/// <summary>Test Y</summary>
				/// <typeparam name="G">G</typeparam>
				/// <typeparam name="H">H</typeparam>
				/// <typeparam name="I">I</typeparam>
				/// <param name="a">a</param>
				/// <param name="b">b</param>
				/// <param name="c">c</param>
				/// <param name="d">d</param>
				/// <param name="e">e</param>
				/// <param name="f">f</param>
				/// <param name="g">g</param>
				/// <param name="h">h</param>
				/// <param name="i">i</param>
				[XmlDocumentationFromMethod]
				public void DocumentedMethod<G, H, I>(
					A[] a, B[,] b, C[,,] c,
					D[] d, E[,] e, F[,,] f,
					G[] g, H[,] h, I[,,] i)
				{ }
			}

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
			public class NestedNestedGenericTypeOverriding3<A, B, C>
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
			{
				/// <summary>Test Z</summary>
				/// <param name="a">a</param>
				/// <param name="b">b</param>
				/// <param name="c">c</param>
				[XmlDocumentationFromMethod]
				public void DocumentedMethod(
					A[] a, B[,] b, C[,,] c)
				{ }
			}
		}

		/// <summary>Test AA</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod_OptionalParameters(int a = 2) { }

		/// <summary>Test BB.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static implicit operator bool(XmlDocumentationFromMethod a) => throw new Exception();

		/// <summary>Test CC.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static explicit operator int(XmlDocumentationFromMethod a) => throw new Exception();

		/// <summary>Test FF.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static implicit operator XmlDocumentationFromMethod(bool a) => throw new Exception();

		/// <summary>Test GG.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static explicit operator XmlDocumentationFromMethod(int a) => throw new Exception();

		/// <summary>Test JJ.</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public static XmlDocumentationFromMethod operator +(XmlDocumentationFromMethod a, XmlDocumentationFromMethod b) => throw new Exception();

		/// <summary>Test KK.</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public static XmlDocumentationFromMethod operator +(XmlDocumentationFromMethod a, NestedGenericType2<bool, bool, bool> b) => throw new Exception();
	}

	#endregion

	#region XML Documentation From Type Types

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Delegate | AttributeTargets.Enum)]
	public class XmlDocumentationFromTypeAttribute : Attribute { }

	/// <summary>Test A</summary>
	[XmlDocumentationFromType]
	public class XmlDocumentationFromTypeA { }

	/// <summary>Test B</summary>
	/// <typeparam name="A">A</typeparam>
	[XmlDocumentationFromType]
	public class XmlDocumentationFromTypeB<A> { }

	/// <summary>Test C</summary>
	/// <typeparam name="A">A</typeparam>
	/// <typeparam name="B">B</typeparam>
	[XmlDocumentationFromType]
	public class XmlDocumentationFromTypeC<A, B> { }

	/// <summary>Test D</summary>
	[XmlDocumentationFromType]
	public struct XmlDocumentationFromTypeD { }

	/// <summary>Test E</summary>
	/// <typeparam name="A">A</typeparam>
	[XmlDocumentationFromType]
	public struct XmlDocumentationFromTypeE<A> { }

	/// <summary>Test F</summary>
	/// <typeparam name="A">A</typeparam>
	/// <typeparam name="B">B</typeparam>
	[XmlDocumentationFromType]
	public struct XmlDocumentationFromTypeF<A, B> { }

	/// <summary>Test G</summary>
	[XmlDocumentationFromType]
	public ref struct XmlDocumentationFromTypeG { }

	/// <summary>Test H</summary>
	/// <typeparam name="A">A</typeparam>
	[XmlDocumentationFromType]
	public ref struct XmlDocumentationFromTypeH<A> { }

	/// <summary>Test I</summary>
	/// <typeparam name="A">A</typeparam>
	/// <typeparam name="B">B</typeparam>
	[XmlDocumentationFromType]
	public ref struct XmlDocumentationFromTypeI<A, B> { }

	/// <summary>Test J</summary>
	[XmlDocumentationFromType]
	public delegate void XmlDocumentationFromTypeJ();

	/// <summary>Test K</summary>
	/// <typeparam name="A">A</typeparam>
	[XmlDocumentationFromType]
	public delegate void XmlDocumentationFromTypeK<A>();

	/// <summary>Test L</summary>
	/// <typeparam name="A">A</typeparam>
	/// <typeparam name="B">B</typeparam>
	[XmlDocumentationFromType]
	public delegate void XmlDocumentationFromTypeL<A, B>();

	/// <summary>Test M</summary>
	[XmlDocumentationFromType]
	public enum XmlDocumentationFromTypeM { }

	public class XmlDocumentationFromTypeBase
	{
		/// <summary>Test N</summary>
		[XmlDocumentationFromType]
		public class XmlDocumentationFromTypeN { }

		/// <summary>Test O</summary>
		/// <typeparam name="A">A</typeparam>
		[XmlDocumentationFromType]
		public class XmlDocumentationFromTypeO<A> { }

		/// <summary>Test P</summary>
		/// <typeparam name="A">A</typeparam>
		/// <typeparam name="B">B</typeparam>
		[XmlDocumentationFromType]
		public class XmlDocumentationFromTypeP<A, B> { }
	}

	public class XmlDocumentationFromTypeBase<A>
	{
		/// <summary>Test Q</summary>
		[XmlDocumentationFromType]
		public class XmlDocumentationFromTypeQ { }

		/// <summary>Test R</summary>
		/// <typeparam name="B">B</typeparam>
		[XmlDocumentationFromType]
		public class XmlDocumentationFromTypeR<B> { }

		/// <summary>Test S</summary>
		/// <typeparam name="B">B</typeparam>
		/// <typeparam name="C">C</typeparam>
		[XmlDocumentationFromType]
		public class XmlDocumentationFromTypeS<B, C> { }
	}

	/// <summary>Test T</summary>
	[XmlDocumentationFromType]
	public static class XmlDocumentationFromTypeT { }

	#endregion

	#region XML Documentation From FieldInfo

	[AttributeUsage(AttributeTargets.Field)]
	public class XmlDocumentationFromFieldAttribute : Attribute { }

	public class XmlDocumentationFromField
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromField]
		public int FieldA;

		/// <summary>Test B</summary>
		[XmlDocumentationFromField]
		public string? FieldB;

		/// <summary>Test C</summary>
		[XmlDocumentationFromField]
		public Action? FieldC;

		/// <summary>Test D</summary>
		[XmlDocumentationFromField]
		public static int FieldD;

		/// <summary>Test E</summary>
		[XmlDocumentationFromField]
		public static string? FieldE;

		/// <summary>Test F</summary>
		[XmlDocumentationFromField]
		public static Action? FieldF;

		/// <summary>Test S</summary>
		[XmlDocumentationFromField]
		public int? FieldS;

		public class NestedType
		{
			/// <summary>Test G</summary>
			[XmlDocumentationFromField]
			public int FieldG;

			/// <summary>Test H</summary>
			[XmlDocumentationFromField]
			public string? FieldH;

			/// <summary>Test I</summary>
			[XmlDocumentationFromField]
			public Action? FieldI;

			/// <summary>Test J</summary>
			[XmlDocumentationFromField]
			public static int FieldJ;

			/// <summary>Test K</summary>
			[XmlDocumentationFromField]
			public static string? FieldK;

			/// <summary>Test L</summary>
			[XmlDocumentationFromField]
			public static Action? FieldL;
		}

		public class NestedTypeGeneric<A>
		{
			/// <summary>Test M</summary>
			[XmlDocumentationFromField]
			public int FieldM;

			/// <summary>Test N</summary>
			[XmlDocumentationFromField]
			public string? FieldN;

			/// <summary>Test O</summary>
			[XmlDocumentationFromField]
			public Action? FieldO;

			/// <summary>Test P</summary>
			[XmlDocumentationFromField]
			public static int FieldP;

			/// <summary>Test Q</summary>
			[XmlDocumentationFromField]
			public static string? FieldQ;

			/// <summary>Test R</summary>
			[XmlDocumentationFromField]
			public static Action? FieldR;
		}
	}

	#endregion

	#region XML Documentation From PropertyInfo

	[AttributeUsage(AttributeTargets.Property)]
	public class XmlDocumentationFromPropertyAttribute : Attribute { }

	public class XmlDocumentationFromProperty
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromProperty]
		public int FieldA => throw new Exception();

		/// <summary>Test B</summary>
		[XmlDocumentationFromProperty]
		public string FieldB => throw new Exception();

		/// <summary>Test C</summary>
		[XmlDocumentationFromProperty]
		public Action FieldC => throw new Exception();

		/// <summary>Test D</summary>
		[XmlDocumentationFromProperty]
		public static int FieldD => throw new Exception();

		/// <summary>Test E</summary>
		[XmlDocumentationFromProperty]
		public static string FieldE => throw new Exception();

		/// <summary>Test F</summary>
		[XmlDocumentationFromProperty]
		public static Action FieldF => throw new Exception();

		public class NestedType
		{
			/// <summary>Test G</summary>
			[XmlDocumentationFromProperty]
			public int FieldG => throw new Exception();

			/// <summary>Test H</summary>
			[XmlDocumentationFromProperty]
			public string FieldH => throw new Exception();

			/// <summary>Test I</summary>
			[XmlDocumentationFromProperty]
			public Action FieldI => throw new Exception();

			/// <summary>Test J</summary>
			[XmlDocumentationFromProperty]
			public static int FieldJ => throw new Exception();

			/// <summary>Test K</summary>
			[XmlDocumentationFromProperty]
			public static string FieldK => throw new Exception();

			/// <summary>Test L</summary>
			[XmlDocumentationFromProperty]
			public static Action FieldL => throw new Exception();
		}

		public class NestedTypeGeneric<A>
		{
			/// <summary>Test M</summary>
			[XmlDocumentationFromProperty]
			public int FieldM => throw new Exception();

			/// <summary>Test N</summary>
			[XmlDocumentationFromProperty]
			public string FieldN => throw new Exception();

			/// <summary>Test O</summary>
			[XmlDocumentationFromProperty]
			public Action FieldO => throw new Exception();

			/// <summary>Test P</summary>
			[XmlDocumentationFromProperty]
			public static int FieldP => throw new Exception();

			/// <summary>Test Q</summary>
			[XmlDocumentationFromProperty]
			public static string FieldQ => throw new Exception();

			/// <summary>Test R</summary>
			[XmlDocumentationFromProperty]
			public static Action FieldR => throw new Exception();
		}
	}

	#endregion

	#region XML Documentation From ConstructorInfo

	[AttributeUsage(AttributeTargets.Constructor)]
	public class XmlDocumentationFromConstructorAttribute : Attribute { }

	public class XmlDocumentationFromConstructor
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor() => throw new Exception();

		/// <summary>Test B</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(object a) => throw new Exception();

		/// <summary>Test C</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(object a, object b) => throw new Exception();

		/// <summary>Test D</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(object a, object b, object c) => throw new Exception();

		/// <summary>Test E</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(params object[] a) => throw new Exception();

		/// <summary>Test F</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(out object a) => throw new Exception();

		/// <summary>Test G</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public unsafe XmlDocumentationFromConstructor(int* a) => throw new Exception();

		public class NestedType
		{
			/// <summary>Test H</summary>
			[XmlDocumentationFromConstructor]
			public NestedType() => throw new Exception();
		}

		public class NestedGenericType<A>
		{
			/// <summary>Test I</summary>
			[XmlDocumentationFromConstructor]
			public NestedGenericType() => throw new Exception();

			/// <summary>Test L</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromConstructor]
			public NestedGenericType(A a) => throw new Exception();
		}

		/// <summary>Test J</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(NestedType a) => throw new Exception();

		/// <summary>Test K</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public unsafe XmlDocumentationFromConstructor(ref int* a) => throw new Exception();

		public class NestedGenericType2<A, B, C>
		{
			/// <summary>Test L</summary>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			/// <param name="c">c</param>
			[XmlDocumentationFromConstructor]
			public NestedGenericType2(A a, B b, C c) => throw new Exception();

			/// <summary>Test M</summary>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			/// <param name="c">c</param>
			[XmlDocumentationFromConstructor]
			public NestedGenericType2(A[] a, B[,] b, C[,,] c) => throw new Exception();
		}
	}

	#endregion

	#region XML Documentation From EventInfo

	[AttributeUsage(AttributeTargets.Event)]
	public class XmlDocumentationFromEventAttribute : Attribute { }

	public class XmlDocumentationFromEvent
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromEvent]
		public event Action? EventA;

		/// <summary>Test B</summary>
		[XmlDocumentationFromEvent]
		public event Action<int>? EventB;

		/// <summary>Test C</summary>
		[XmlDocumentationFromEvent]
		public event Func<int>? EventC;

		public class NestedType
		{
			/// <summary>Test A</summary>
			[XmlDocumentationFromEvent]
			public event Action? EventA;

			/// <summary>Test B</summary>
			[XmlDocumentationFromEvent]
			public event Action<int>? EventB;

			/// <summary>Test C</summary>
			[XmlDocumentationFromEvent]
			public event Func<int>? EventC;
		}

		public class NestedTypeGeneric<A>
		{
			/// <summary>Test A</summary>
			[XmlDocumentationFromEvent]
			public event Action? EventA;

			/// <summary>Test B</summary>
			[XmlDocumentationFromEvent]
			public event Action<A>? EventB;

			/// <summary>Test C</summary>
			[XmlDocumentationFromEvent]
			public event Func<A>? EventC;
		}
	}

	#endregion

	#region XML Documentation From ParameterInfo

	[AttributeUsage(AttributeTargets.Parameter)]
	public class XmlDocumentationFromParameterAttribute : Attribute { }

	public class XmlDocumentationFromParameter
	{
		/// <summary>Test1</summary>
		/// <param name="a">TEST a</param>
		public void Test1(object a) => throw new Exception();
	}

	#endregion

#pragma warning restore SA1611 // Element parameters should be documented
#pragma warning restore SA1618 // Generic type parameters should be documented
#pragma warning restore SA1314 // Type parameter names should begin with T
#pragma warning restore CA2211 // Non-constant fields should not be visible
#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS0067 // The event is never used
#pragma warning restore IDE0060 // Remove unused parameter

	#endregion
}
