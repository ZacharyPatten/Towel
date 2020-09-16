using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Reflection;
using Towel;

namespace Towel_Testing
{
	[TestClass] public class Meta_Testing
	{
		#region Type Testing

		[TestMethod] public void Type_ConvertToCsharpSource()
		{
			{ // showGenericParameters = false
				(Type, string)[] testCases = new (Type, string)[]
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
				(typeof((int, int, int, int, int, int, int, int, int)), "System.ValueTuple<System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.ValueTuple<System.Int32, System.Int32>>"),
				(typeof(StepStatus), "Towel.StepStatus"),
				(typeof(int*), "System.Int32*"),
				(typeof(int**), "System.Int32**"),
				(typeof(Span<int>), "System.Span<System.Int32>"),
				(typeof(int[]), "System.Int32[]"),
				(typeof(int[][]), "System.Int32[][]"),
				(typeof(int[][][]), "System.Int32[][][]"),
				(typeof(int[,]), "System.Int32[,]"),
				(typeof(int[,,]), "System.Int32[,,]"),
				(typeof(int[,,,]), "System.Int32[,,,]"),
				(typeof(int[][,,,][]), "System.Int32[][,,,][]"),
				(typeof(int[,,,][][,,,]), "System.Int32[,,,][][,,,]"),
				};
				foreach ((Type, string) testCase in testCases)
				{
					string temp;
					try
					{
						temp = testCase.Item1.ConvertToCSharpSource();
						Assert.IsTrue(temp.Equals(testCase.Item2), testCase.Item1.ToString());
					}
					catch
					{
						Debugger.Break();
						temp = testCase.Item1.ConvertToCSharpSource();
						Assert.IsTrue(temp.Equals(testCase.Item2), testCase.Item1.ToString());
					}
				}
			}

			{ // showGenericParameters = true
				(Type, string)[] testCases = new (Type, string)[]
				{
				(typeof(System.Collections.Generic.List<>), "System.Collections.Generic.List<T>"),
				(typeof(Towel.Mathematics.Symbolics.Constant<>), "Towel.Mathematics.Symbolics.Constant<T>"),
				(typeof(Towel_Testing.A.D<>.E<>), "Towel_Testing.A.D<AA>.E<BB>"),
				(typeof(Towel.DataStructures.IOmnitreePoints<,,,>), "Towel.DataStructures.IOmnitreePoints<T, Axis1, Axis2, Axis3>"),
				(typeof(Span<>), "System.Span<T>"),
				};
				foreach ((Type, string) testCase in testCases)
				{
					string temp;
					try
					{
						temp = testCase.Item1.ConvertToCSharpSource(true);
						Assert.IsTrue(temp.Equals(testCase.Item2), testCase.Item1.ToString());
					}
					catch
					{
						Debugger.Break();
						temp = testCase.Item1.ConvertToCSharpSource(true);
						Assert.IsTrue(temp.Equals(testCase.Item2), testCase.Item1.ToString());
					}
				}
			}
		}

		#endregion

		#region XML Documentation Testing

		[TestMethod] public void MethodInfo_GetDocumentation()
		{
			foreach (MethodInfo methodInfo in Assembly.GetExecutingAssembly().GetMethodInfosWithAttribute<XmlDocumentationFromMethodAttribute>())
			{
				try
				{
					string xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					string xmlDocumentation = methodInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)methodInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
			}
		}

		[TestMethod] public void Type_GetDocumentation()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypesWithAttribute<XmlDocumentationFromTypeAttribute>())
			{
				try
				{
					string xmlDocumentation = type.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)type).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					string xmlDocumentation = type.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)type).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
			}
		}

		[TestMethod] public void FieldInfo_GetDocumentation()
		{
			foreach (FieldInfo fieldInfo in Assembly.GetExecutingAssembly().GetFieldInfosWithAttribute<XmlDocumentationFromFieldAttribute>())
			{
				try
				{
					string xmlDocumentation = fieldInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)fieldInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					string xmlDocumentation = fieldInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)fieldInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
			}
		}

		[TestMethod] public void ConstructorInfo_GetDocumentation()
		{
			foreach (ConstructorInfo constructorInfo in Assembly.GetExecutingAssembly().GetConstructorInfosWithAttribute<XmlDocumentationFromConstructorAttribute>())
			{
				try
				{
					string xmlDocumentation = constructorInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)constructorInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					string xmlDocumentation = constructorInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)constructorInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
			}
		}

		[TestMethod] public void PropertyInfo_GetDocumentation()
		{
			foreach (PropertyInfo propertyInfo in Assembly.GetExecutingAssembly().GetPropertyInfosWithAttribute<XmlDocumentationFromPropertyAttribute>())
			{
				try
				{
					string xmlDocumentation = propertyInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)propertyInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					string xmlDocumentation = propertyInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)propertyInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
			}
		}

		[TestMethod] public void EventInfo_GetDocumentation()
		{
			foreach (EventInfo eventInfo in Assembly.GetExecutingAssembly().GetEventInfosWithAttribute<XmlDocumentationFromEventAttribute>())
			{
				try
				{
					string xmlDocumentation = eventInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)eventInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
				catch
				{
					Debugger.Break();
					string xmlDocumentation = eventInfo.GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));

					string xmlDocumentationMember = ((MemberInfo)eventInfo).GetDocumentation();
					Assert.IsTrue(!string.IsNullOrWhiteSpace(xmlDocumentation));
					Assert.IsTrue(xmlDocumentation == xmlDocumentationMember);
				}
			}
		}

		[TestMethod] public void ParameterInfo_GetDocumentation()
		{
			string Test1_a = typeof(XmlDocumentationFromParameter).GetMethod("Test1").GetParameters()[0].GetDocumentation();
			Assert.IsTrue(Test1_a.Equals("<param name=\"a\">TEST a</param>"));
		}

		#endregion

		#region MethodInfo Testing

#pragma warning disable IDE0062 // Make local function 'static'

		[TestMethod] public void MethodInfo_IsLocalFunction()
		{
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

			Assert.IsFalse(new Action(MethodInfo_IsLocalFunction).Method.IsLocalFunction());
		}

#pragma warning restore IDE0062 // Make local function 'static'

		#endregion

		#region HasImplicitCast

		[TestMethod] public void Meta_HasImplicitCast()
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

		public class D<AA>
		{
			public class E<BB>
			{

			}
		}
	}

	#endregion

	#region XML Documentation Types

#pragma warning disable CS0067 // The event is never used
#pragma warning disable IDE0060 // Remove unused parameter

	#region XML Documentation From MethodInfo

	public class XmlDocumentationFromMethodAttribute : Attribute { }

	public class XmlDocumentationFromMethod
	{
		/// <summary>Test A</summary>
		/// <returns>object</returns>
		[XmlDocumentationFromMethod]
		public object DocumentedMethodReturns() { return null; }

		/// <summary>Test B</summary>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod() { }

		/// <summary>Test C</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(object a) { }

		/// <summary>Test D</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(object a, object b) { }

		/// <summary>Test E</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(object a, object b, object c) { }

		/// <summary>Test F</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(params object[] a) { }

		/// <summary>Test G</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethodOut(out object a) { a = null; }

		/// <summary>Test H</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethodRef(ref object a) { }

		/// <summary>Test I</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethodIn(in object a) { }

		/// <summary>Test J</summary>
		/// <typeparam name="A">A</typeparam>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>() { }

		/// <summary>Test K</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a) { }

		/// <summary>Test L</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a, object b) { }

		/// <summary>Test M</summary>
		/// <typeparam name="A">A</typeparam>
		/// <typeparam name="B">B</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A, B>(A a, B b) { }

		/// <summary>Test N</summary>
		/// <typeparam name="A">A</typeparam>
		/// <typeparam name="B">B</typeparam>
		/// <param name="b">b</param>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A, B>(B b, A a) { }

		/// <summary>Test U</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public unsafe void DocumentedMethod(int* a) { }

		public class NestedType
		{
			/// <summary>Test O</summary>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod() { }
		}

		public class NestedGenericType<A>
		{
			/// <summary>Test P</summary>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod() { }

			/// <summary>Test W</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod(A a) { }

			/// <summary>Test X</summary>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			[XmlDocumentationFromMethod]
			public void DocumentedMethod<B>(A a, B b) { }
		}

		/// <summary>Test Q</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod(NestedType a) { }

		/// <summary>Test R</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(NestedGenericType<A> a) { }

		/// <summary>Test S</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a, Action<Action<A>> b, A[] c) { }

		/// <summary>Test T</summary>
		/// <typeparam name="A">A</typeparam>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		[XmlDocumentationFromMethod]
		public void DocumentedMethod<A>(A a, Action<Action<A>> b, A[] c, A[,] d) { }

		/// <summary>Test V</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public unsafe void DocumentedMethod(ref int* a) { }

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
			public void DocumentedMethod<D, E, F>(A a, B b, C c, D d, E e, F f) { }

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
			public void DocumentedMethod<D, E, F>(A[] a, B[,] b, C[,,] c, D[] d, E[,] e, F[,,] f) { }

			/// <summary>Test DD.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static implicit operator bool(NestedGenericType2<A, B, C> a) { return false; }

			/// <summary>Test EE.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static explicit operator int(NestedGenericType2<A, B, C> a) { return 0; }

			/// <summary>Test HH.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static implicit operator NestedGenericType2<A, B, C>(bool a) { return null; }

			/// <summary>Test II.</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromMethod]
			public static explicit operator NestedGenericType2<A, B, C>(int a) { return null; }

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
		public static implicit operator bool(XmlDocumentationFromMethod a) { return false; }

		/// <summary>Test CC.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static explicit operator int(XmlDocumentationFromMethod a) { return 0; }

		/// <summary>Test FF.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static implicit operator XmlDocumentationFromMethod(bool a) { return null; }

		/// <summary>Test GG.</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromMethod]
		public static explicit operator XmlDocumentationFromMethod(int a) { return null; }

		/// <summary>Test JJ.</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public static XmlDocumentationFromMethod operator +(XmlDocumentationFromMethod a, XmlDocumentationFromMethod b) { return null; }

		/// <summary>Test KK.</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromMethod]
		public static XmlDocumentationFromMethod operator +(XmlDocumentationFromMethod a, NestedGenericType2<bool, bool, bool> b) { return null; }
	}

	#endregion

	#region XML Documentation From Type Types

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

	public class XmlDocumentationFromFieldAttribute : Attribute { }

	public class XmlDocumentationFromField
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromField]
		public int FieldA;

		/// <summary>Test B</summary>
		[XmlDocumentationFromField]
		public string FieldB;

		/// <summary>Test C</summary>
		[XmlDocumentationFromField]
		public Action FieldC;

		/// <summary>Test D</summary>
		[XmlDocumentationFromField]
		public static int FieldD;

		/// <summary>Test E</summary>
		[XmlDocumentationFromField]
		public static string FieldE;

		/// <summary>Test F</summary>
		[XmlDocumentationFromField]
		public static Action FieldF;

		public class NestedType
		{
			/// <summary>Test G</summary>
			[XmlDocumentationFromField]
			public int FieldG;

			/// <summary>Test H</summary>
			[XmlDocumentationFromField]
			public string FieldH;

			/// <summary>Test I</summary>
			[XmlDocumentationFromField]
			public Action FieldI;

			/// <summary>Test J</summary>
			[XmlDocumentationFromField]
			public static int FieldJ;

			/// <summary>Test K</summary>
			[XmlDocumentationFromField]
			public static string FieldK;

			/// <summary>Test L</summary>
			[XmlDocumentationFromField]
			public static Action FieldL;
		}

		public class NestedTypeGeneric<A>
		{
			/// <summary>Test M</summary>
			[XmlDocumentationFromField]
			public int FieldM;

			/// <summary>Test N</summary>
			[XmlDocumentationFromField]
			public string FieldN;

			/// <summary>Test O</summary>
			[XmlDocumentationFromField]
			public Action FieldO;

			/// <summary>Test P</summary>
			[XmlDocumentationFromField]
			public static int FieldP;

			/// <summary>Test Q</summary>
			[XmlDocumentationFromField]
			public static string FieldQ;

			/// <summary>Test R</summary>
			[XmlDocumentationFromField]
			public static Action FieldR;
		}
	}

	#endregion

	#region XML Documentation From PropertyInfo

	public class XmlDocumentationFromPropertyAttribute : Attribute { }

	public class XmlDocumentationFromProperty
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromProperty]
		public int FieldA => default;

		/// <summary>Test B</summary>
		[XmlDocumentationFromProperty]
		public string FieldB => default;

		/// <summary>Test C</summary>
		[XmlDocumentationFromProperty]
		public Action FieldC => default;

		/// <summary>Test D</summary>
		[XmlDocumentationFromProperty]
		public static int FieldD => default;

		/// <summary>Test E</summary>
		[XmlDocumentationFromProperty]
		public static string FieldE => default;

		/// <summary>Test F</summary>
		[XmlDocumentationFromProperty]
		public static Action FieldF => default;

		public class NestedType
		{
			/// <summary>Test G</summary>
			[XmlDocumentationFromProperty]
			public int FieldG => default;

			/// <summary>Test H</summary>
			[XmlDocumentationFromProperty]
			public string FieldH => default;

			/// <summary>Test I</summary>
			[XmlDocumentationFromProperty]
			public Action FieldI => default;

			/// <summary>Test J</summary>
			[XmlDocumentationFromProperty]
			public static int FieldJ => default;

			/// <summary>Test K</summary>
			[XmlDocumentationFromProperty]
			public static string FieldK => default;

			/// <summary>Test L</summary>
			[XmlDocumentationFromProperty]
			public static Action FieldL => default;
		}

		public class NestedTypeGeneric<A>
		{
			/// <summary>Test M</summary>
			[XmlDocumentationFromProperty]
			public int FieldM => default;

			/// <summary>Test N</summary>
			[XmlDocumentationFromProperty]
			public string FieldN => default;

			/// <summary>Test O</summary>
			[XmlDocumentationFromProperty]
			public Action FieldO => default;

			/// <summary>Test P</summary>
			[XmlDocumentationFromProperty]
			public static int FieldP => default;

			/// <summary>Test Q</summary>
			[XmlDocumentationFromProperty]
			public static string FieldQ => default;

			/// <summary>Test R</summary>
			[XmlDocumentationFromProperty]
			public static Action FieldR => default;
		}
	}

	#endregion

	#region XML Documentation From ConstructorInfo

	public class XmlDocumentationFromConstructorAttribute : Attribute { }

	public class XmlDocumentationFromConstructor
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor() { }

		/// <summary>Test B</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(object a) { }

		/// <summary>Test C</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(object a, object b) { }

		/// <summary>Test D</summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(object a, object b, object c) { }

		/// <summary>Test E</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(params object[] a) { }

		/// <summary>Test F</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(out object a) { a = null; }

		/// <summary>Test G</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public unsafe XmlDocumentationFromConstructor(int* a) { }

		public class NestedType
		{
			/// <summary>Test H</summary>
			[XmlDocumentationFromConstructor]
			public NestedType() { }
		}

		public class NestedGenericType<A>
		{
			/// <summary>Test I</summary>
			[XmlDocumentationFromConstructor]
			public NestedGenericType() { }

			/// <summary>Test L</summary>
			/// <param name="a">a</param>
			[XmlDocumentationFromConstructor]
			public NestedGenericType(A a) { }
		}

		/// <summary>Test J</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public XmlDocumentationFromConstructor(NestedType a) { }

		/// <summary>Test K</summary>
		/// <param name="a">a</param>
		[XmlDocumentationFromConstructor]
		public unsafe XmlDocumentationFromConstructor(ref int* a) { }

		public class NestedGenericType2<A, B, C>
		{
			/// <summary>Test L</summary>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			/// <param name="c">c</param>
			[XmlDocumentationFromConstructor]
			public NestedGenericType2(A a, B b, C c) { }

			/// <summary>Test M</summary>
			/// <param name="a">a</param>
			/// <param name="b">b</param>
			/// <param name="c">c</param>
			[XmlDocumentationFromConstructor]
			public NestedGenericType2(A[] a, B[,] b, C[,,] c) { }
		}
	}

	#endregion

	#region XML Documentation From EventInfo

	public class XmlDocumentationFromEventAttribute : Attribute { }

	public class XmlDocumentationFromEvent
	{
		/// <summary>Test A</summary>
		[XmlDocumentationFromEvent]
		public event Action EventA;

		/// <summary>Test B</summary>
		[XmlDocumentationFromEvent]
		public event Action<int> EventB;

		/// <summary>Test C</summary>
		[XmlDocumentationFromEvent]
		public event Func<int> EventC;

		public class NestedType
		{
			/// <summary>Test A</summary>
			[XmlDocumentationFromEvent]
			public event Action EventA;

			/// <summary>Test B</summary>
			[XmlDocumentationFromEvent]
			public event Action<int> EventB;

			/// <summary>Test C</summary>
			[XmlDocumentationFromEvent]
			public event Func<int> EventC;
		}

		public class NestedTypeGeneric<A>
		{
			/// <summary>Test A</summary>
			[XmlDocumentationFromEvent]
			public event Action EventA;

			/// <summary>Test B</summary>
			[XmlDocumentationFromEvent]
			public event Action<A> EventB;

			/// <summary>Test C</summary>
			[XmlDocumentationFromEvent]
			public event Func<A> EventC;
		}
	}

	#endregion

	#region XML Documentation From ParameterInfo

	public class XmlDocumentationFromParameterAttribute : Attribute { }

	public class XmlDocumentationFromParameter
	{
		/// <summary>Test1</summary>
		/// <param name="a">TEST a</param>
		public void Test1(object a) { }
	}

	#endregion

#pragma warning restore CS0067 // The event is never used
#pragma warning restore IDE0060 // Remove unused parameter

	#endregion
}
