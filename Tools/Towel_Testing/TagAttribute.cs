using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	[TestClass]
	public class TagAttribute_Testing
	{
		internal const string TestAttribute1 = nameof(TestAttribute1);
		internal const string TestValue1 = nameof(TestValue1);
		internal const string TestAttribute2 = nameof(TestAttribute2);
		internal const string TestValue2 = nameof(TestValue2);
		internal const string not_null = nameof(not_null);
		internal const string TestAttributeFail = nameof(TestAttributeFail);
		internal const string AmbiguousAttribute = nameof(AmbiguousAttribute);
		internal const int IntValueOne = 7;
		internal const int IntValueTwo = -7;

		[TestMethod]
		public void Test()
		{
			BindingFlags bf =
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Static |
				BindingFlags.Instance;

			Type type = typeof(A);
			MethodInfo methodInfo = typeof(TagAttribute_Testing).GetMethod(nameof(Method), bf)!;
			EventInfo eventInfo = typeof(TagAttribute_Testing).GetEvent(nameof(Event))!;
			ConstructorInfo constructorInfo = type.GetConstructor(Array.Empty<Type>())!;
			FieldInfo fieldInfo = typeof(A).GetField(nameof(A.Field), bf)!;
			PropertyInfo propertyInfo = typeof(A).GetProperty(nameof(A.Property), bf)!;
			ParameterInfo parameterInfo = methodInfo.GetParameters()[0];

			// TestAttribute1 -----------------------------------

			Assert.IsTrue(type.GetTag(TestAttribute1) is (true, TestValue1));
			Assert.IsTrue(methodInfo.GetTag(TestAttribute1) is (true, TestValue1));
			Assert.IsTrue(eventInfo.GetTag(TestAttribute1) is (true, TestValue1));
			Assert.IsTrue(constructorInfo.GetTag(TestAttribute1) is (true, TestValue1));
			Assert.IsTrue(fieldInfo.GetTag(TestAttribute1) is (true, TestValue1));
			Assert.IsTrue(propertyInfo.GetTag(TestAttribute1) is (true, TestValue1));
			Assert.IsTrue(parameterInfo.GetTag(TestAttribute1) is (true, TestValue1));

			// TestAttribute2 -----------------------------------

			Assert.IsTrue(type.GetTag(TestAttribute2) is (true, TestValue2));
			Assert.IsTrue(methodInfo.GetTag(TestAttribute2) is (true, TestValue2));
			Assert.IsTrue(eventInfo.GetTag(TestAttribute2) is (true, TestValue2));
			Assert.IsTrue(constructorInfo.GetTag(TestAttribute2) is (true, TestValue2));
			Assert.IsTrue(fieldInfo.GetTag(TestAttribute2) is (true, TestValue2));
			Assert.IsTrue(propertyInfo.GetTag(TestAttribute2) is (true, TestValue2));
			Assert.IsTrue(parameterInfo.GetTag(TestAttribute2) is (true, TestValue2));

			// Testing null -----------------------------------

			Assert.IsTrue(typeof(C).GetTag(null) is (true, null));
			Assert.IsTrue(typeof(D).GetTag(not_null) is (true, null));
			Assert.IsTrue(typeof(E).GetTag(null) is (true, not_null));

			// Test int ----------------------------------

			Assert.IsTrue(typeof(F).GetTag(IntValueOne) is (true, IntValueTwo));

			// Failure Not Found -----------------------------------

			Assert.IsTrue(type.GetTag(TestAttributeFail) is (false, null));
			Assert.IsTrue(methodInfo.GetTag(TestAttributeFail) is (false, null));
			Assert.IsTrue(eventInfo.GetTag(TestAttributeFail) is (false, null));
			Assert.IsTrue(constructorInfo.GetTag(TestAttributeFail) is (false, null));
			Assert.IsTrue(fieldInfo.GetTag(TestAttributeFail) is (false, null));
			Assert.IsTrue(propertyInfo.GetTag(TestAttributeFail) is (false, null));
			Assert.IsTrue(parameterInfo.GetTag(TestAttributeFail) is (false, null));

			// Failure Ambiguous -----------------------------------

			MethodInfo b_methodInfo = typeof(B).GetMethod(nameof(B.Method), bf)!;
			ParameterInfo b_parameterInfo = b_methodInfo.GetParameters()[0];

			Assert.IsTrue(typeof(B).GetTag(AmbiguousAttribute) is (false, null));
			Assert.IsTrue(b_parameterInfo.GetTag(AmbiguousAttribute) is (false, null));
		}

		[Tag(TestAttribute1, TestValue1)]
		[Tag(TestAttribute2, TestValue2)]
		public class A
		{
			[Tag(TestAttribute1, TestValue1)]
			[Tag(TestAttribute2, TestValue2)]
			public object Field;

			[Tag(TestAttribute1, TestValue1)]
			[Tag(TestAttribute2, TestValue2)]
			public object Property { get; set; }

			[Tag(TestAttribute1, TestValue1)]
			[Tag(TestAttribute2, TestValue2)]
			public A() => throw new Exception();
		}

		[Tag(TestAttribute1, TestValue1)]
		[Tag(TestAttribute2, TestValue2)]
		public static void Method(
			[Tag(TestAttribute1, TestValue1)]
			[Tag(TestAttribute2, TestValue2)]
			Exception a) => throw a;

		[Tag(TestAttribute1, TestValue1)]
		[Tag(TestAttribute2, TestValue2)]
		public static event Action? Event;

		[Tag(AmbiguousAttribute, 1)]
		[Tag(AmbiguousAttribute, 1)]
		public class B
		{
			public static void Method(
				[Tag(AmbiguousAttribute, 1)]
				[Tag(AmbiguousAttribute, 1)]
				Exception a) => throw a;
		}

		[Tag(null, null)]
		public class C { }

		[Tag(not_null, null)]
		public class D { }

		[Tag(null, not_null)]
		public class E { }

		[Tag(IntValueOne, IntValueTwo)]
		public class F { }
	}
}
