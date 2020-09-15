using System;
using System.Reflection;
using Towel;

namespace Example
{
	static class Program
	{
		const string MyAttributeA = nameof(MyAttributeA);
		const string MyAttributeB = nameof(MyAttributeB);

		static void Main()
		{
			Console.WriteLine("You are runnning the ValueAttributes example.");
			Console.WriteLine("============================================");
			Console.WriteLine();
			Console.WriteLine("  Towel has value-based attributes. Rather than making");
			Console.WriteLine("  custom attribute types, you can use constant values.");
			Console.WriteLine();

			// Reflection Code
			Type type = typeof(A);
			MethodInfo methodInfo = typeof(Program).GetMethod(nameof(Method),
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Static |
				BindingFlags.Instance);
			EventInfo eventInfo = typeof(Program).GetEvent(nameof(Event));
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

			Console.WriteLine($@"  Lets look up the ""{nameof(MyAttributeA)}"" attribute on ");
			Console.WriteLine($@"  the common reflection types:");
			Console.WriteLine();

			// Looking Up MyAttributeA
			Console.WriteLine($"  - type: {type.GetValueAttribute(MyAttributeA).Value}");
			Console.WriteLine($"  - method: {methodInfo.GetValueAttribute(MyAttributeA).Value}");
			Console.WriteLine($"  - event: {eventInfo.GetValueAttribute(MyAttributeA).Value}");
			Console.WriteLine($"  - constructor: {constructorInfo.GetValueAttribute(MyAttributeA).Value}");
			Console.WriteLine($"  - field: {fieldInfo.GetValueAttribute(MyAttributeA).Value}");
			Console.WriteLine($"  - property: {propertyInfo.GetValueAttribute(MyAttributeA).Value}");
			Console.WriteLine($"  - parameter: {parameterInfo.GetValueAttribute(MyAttributeA).Value}");

			Console.WriteLine();
			Console.WriteLine("  Of course you can use multiple value-based attributes per");
			Console.WriteLine($@"  member. Lets look up the ""{nameof(MyAttributeB)}"" attribute on ");
			Console.WriteLine($@"  the common reflection types:");
			Console.WriteLine();

			// Looking Up MyAttributeB
			Console.WriteLine($"  - type: {type.GetValueAttribute(MyAttributeB).Value}");
			Console.WriteLine($"  - method: {methodInfo.GetValueAttribute(MyAttributeB).Value}");
			Console.WriteLine($"  - event: {eventInfo.GetValueAttribute(MyAttributeB).Value}");
			Console.WriteLine($"  - constructor: {constructorInfo.GetValueAttribute(MyAttributeB).Value}");
			Console.WriteLine($"  - field: {fieldInfo.GetValueAttribute(MyAttributeB).Value}");
			Console.WriteLine($"  - property: {propertyInfo.GetValueAttribute(MyAttributeB).Value}");
			Console.WriteLine($"  - parameter: {parameterInfo.GetValueAttribute(MyAttributeB).Value}");

			Console.WriteLine();
			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}

		[Value(MyAttributeA, "works :)")]
		[Value(MyAttributeB, "A")]
		public class A
		{
			[Value(MyAttributeA, "works :3")]
			[Value(MyAttributeB, "E")]
			public object Field;

			[Value(MyAttributeA, "works :b")]
			[Value(MyAttributeB, "F")]
			public object Property { get; set; }

			[Value(MyAttributeA, "works :O")]
			[Value(MyAttributeB, "D")]
			public A() { }
		}

		[Value(MyAttributeA, "works :P")]
		[Value(MyAttributeB, "B")]
		public static void Method(
			[Value(MyAttributeA, "works ;)")]
			[Value(MyAttributeB, "G")]
			object a) { }

		[Value(MyAttributeA, "works :D")]
		[Value(MyAttributeB, "C")]
		public static event Action Event;
	}
}
