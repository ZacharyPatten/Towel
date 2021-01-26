using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using Towel;

namespace Towel_Testing
{
	[TestClass]	public class ValueAttribute_Testing
	{
		internal const string TestAttribute1 = nameof(TestAttribute1);
		internal const string TestValue1 = nameof(TestValue1);
		internal const string TestAttribute2 = nameof(TestAttribute2);
		internal const string TestValue2 = nameof(TestValue2);
		internal const string not_null = nameof(not_null);
		internal const string TestAttributeFail = nameof(TestAttributeFail);
		internal const int IntValueOne = 7;
		internal const int IntValueTwo = -7;

		[TestMethod] public void Test()
		{
			Type type = typeof(A);
			MethodInfo methodInfo = typeof(ValueAttribute_Testing).GetMethod(nameof(Method),
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Static |
				BindingFlags.Instance);
			EventInfo eventInfo = typeof(ValueAttribute_Testing).GetEvent(nameof(Event));
			ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
			FieldInfo fieldInfo = typeof(A).GetField(nameof(A.Field),
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Static |
				BindingFlags.Instance);
			PropertyInfo propertyInfo = typeof(A).GetProperty(nameof(A.Property),
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Static |
				BindingFlags.Instance);
			ParameterInfo parameterInfo = methodInfo.GetParameters()[0];

#pragma warning disable IDE0042 // Deconstruct variable declaration

			// TestAttribute1 -----------------------------------

			var typeAttribute1 = type.GetTag(TestAttribute1);
			Assert.IsTrue(typeAttribute1.Found);
			Assert.AreEqual(typeAttribute1.Value, TestValue1);

			var methodAttribute1 = methodInfo.GetTag(TestAttribute1);
			Assert.IsTrue(methodAttribute1.Found);
			Assert.AreEqual(methodAttribute1.Value, TestValue1);

			var eventAttribute1 = eventInfo.GetTag(TestAttribute1);
			Assert.IsTrue(eventAttribute1.Found);
			Assert.AreEqual(eventAttribute1.Value, TestValue1);

			var constructorAttribute1 = constructorInfo.GetTag(TestAttribute1);
			Assert.IsTrue(constructorAttribute1.Found);
			Assert.AreEqual(constructorAttribute1.Value, TestValue1);

			var fieldAttribute1 = fieldInfo.GetTag(TestAttribute1);
			Assert.IsTrue(fieldAttribute1.Found);
			Assert.AreEqual(fieldAttribute1.Value, TestValue1);

			var propertyAttribute1 = propertyInfo.GetTag(TestAttribute1);
			Assert.IsTrue(propertyAttribute1.Found);
			Assert.AreEqual(propertyAttribute1.Value, TestValue1);

			var parmeterAttribute1 = parameterInfo.GetTag(TestAttribute1);
			Assert.IsTrue(parmeterAttribute1.Found);
			Assert.AreEqual(parmeterAttribute1.Value, TestValue1);

			// TestAttribute2 -----------------------------------

			var typeAttribute2 = type.GetTag(TestAttribute2);
			Assert.IsTrue(typeAttribute2.Found);
			Assert.AreEqual(typeAttribute2.Value, TestValue2);

			var methodAttribute2 = methodInfo.GetTag(TestAttribute2);
			Assert.IsTrue(methodAttribute2.Found);
			Assert.AreEqual(methodAttribute2.Value, TestValue2);

			var eventAttribute2 = eventInfo.GetTag(TestAttribute2);
			Assert.IsTrue(eventAttribute2.Found);
			Assert.AreEqual(eventAttribute2.Value, TestValue2);

			var constructorAttribute2 = constructorInfo.GetTag(TestAttribute2);
			Assert.IsTrue(constructorAttribute2.Found);
			Assert.AreEqual(constructorAttribute2.Value, TestValue2);

			var fieldAttribute2 = fieldInfo.GetTag(TestAttribute2);
			Assert.IsTrue(fieldAttribute2.Found);
			Assert.AreEqual(fieldAttribute2.Value, TestValue2);

			var propertyAttribute2 = propertyInfo.GetTag(TestAttribute2);
			Assert.IsTrue(propertyAttribute2.Found);
			Assert.AreEqual(propertyAttribute2.Value, TestValue2);

			var parmeterAttribute2 = parameterInfo.GetTag(TestAttribute2);
			Assert.IsTrue(parmeterAttribute2.Found);
			Assert.AreEqual(parmeterAttribute2.Value, TestValue2);

			// Testing null -----------------------------------

			var null_null = typeof(C).GetTag(null);
			Assert.IsTrue(null_null.Found);
			Assert.AreEqual(null_null.Value, null);

			var not_null_null = typeof(D).GetTag(not_null);
			Assert.IsTrue(not_null_null.Found);
			Assert.AreEqual(not_null_null.Value, null);

			var null_not_null = typeof(E).GetTag(null);
			Assert.IsTrue(null_not_null.Found);
			Assert.AreEqual(null_not_null.Value, not_null);

			// Test int ----------------------------------

			var intAttribute = typeof(F).GetTag(IntValueOne);
			Assert.IsTrue(intAttribute.Found);
			Assert.AreEqual(intAttribute.Value, IntValueTwo);

			// Failure -----------------------------------

			var typeAttributeFail = type.GetTag(TestAttributeFail);
			Assert.IsFalse(typeAttributeFail.Found);
			Assert.AreEqual(typeAttributeFail.Value, null);

			var methodAttributeFail = methodInfo.GetTag(TestAttributeFail);
			Assert.IsFalse(methodAttributeFail.Found);
			Assert.AreEqual(methodAttributeFail.Value, null);

			var eventAttributeFail = eventInfo.GetTag(TestAttributeFail);
			Assert.IsFalse(eventAttributeFail.Found);
			Assert.AreEqual(eventAttributeFail.Value, null);

			var constructorAttributeFail = constructorInfo.GetTag(TestAttributeFail);
			Assert.IsFalse(constructorAttributeFail.Found);
			Assert.AreEqual(constructorAttributeFail.Value, null);

			var fieldAttributeFail = fieldInfo.GetTag(TestAttributeFail);
			Assert.IsFalse(fieldAttributeFail.Found);
			Assert.AreEqual(fieldAttributeFail.Value, null);

			var propertyAttributeFail = propertyInfo.GetTag(TestAttributeFail);
			Assert.IsFalse(propertyAttributeFail.Found);
			Assert.AreEqual(propertyAttributeFail.Value, null);

			var parmeterAttributeFail = parameterInfo.GetTag(TestAttributeFail);
			Assert.IsFalse(parmeterAttributeFail.Found);
			Assert.AreEqual(parmeterAttributeFail.Value, null);

			var ambiguousMatchFail = typeof(B).GetTag(TestAttribute1);
			Assert.IsFalse(ambiguousMatchFail.Found);
			Assert.AreEqual(ambiguousMatchFail.Value, null);

#pragma warning restore IDE0042 // Deconstruct variable declaration
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
			public A() { }
		}

		[Tag(TestAttribute1, TestValue1)]
		[Tag(TestAttribute2, TestValue2)]
		public static void Method(
			[Tag(TestAttribute1, TestValue1)]
			[Tag(TestAttribute2, TestValue2)]
			object a)
		{ }

		[Tag(TestAttribute1, TestValue1)]
		[Tag(TestAttribute2, TestValue2)]
		public static event Action Event;

		[Tag(TestAttribute1, TestValue1)]
		[Tag(TestAttribute1, TestValue1)]
		public class B { }

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
