using System;
using System.Reflection;
using Towel;

namespace Example
{
	static class Program
	{
		const string MyTagA = "My Tag A";
		const string MyTagB = "My Tag B";

		static void Main()
		{
			Console.WriteLine("You are runnning the TagAttributes example.");
			Console.WriteLine("============================================");
			Console.WriteLine();
			Console.WriteLine(@"  Towel has value-based ""tag"" attributes. Rather than making");
			Console.WriteLine(@"  custom attribute types, you can ""tag"" code members with");
			Console.WriteLine(@"  constant values.");
			Console.WriteLine();

			// Reflection Code
			Type type = typeof(A);
			MethodInfo methodInfo = typeof(Program).GetMethod(nameof(Method),
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Static |
				BindingFlags.Instance);
			EventInfo eventInfo = typeof(Program).GetEvent(nameof(Event));
			ConstructorInfo constructorInfo = type.GetConstructor(Array.Empty<Type>());
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

			Console.WriteLine($@"  Lets look up the ""{nameof(MyTagA)}"" tag on each");
			Console.WriteLine($@"  type of code member:");
			Console.WriteLine();

			// Looking Up MyTagA
			Console.WriteLine($"  - type: {type.GetTag(MyTagA).Value}");
			Console.WriteLine($"  - method: {methodInfo.GetTag(MyTagA).Value}");
			Console.WriteLine($"  - event: {eventInfo.GetTag(MyTagA).Value}");
			Console.WriteLine($"  - constructor: {constructorInfo.GetTag(MyTagA).Value}");
			Console.WriteLine($"  - field: {fieldInfo.GetTag(MyTagA).Value}");
			Console.WriteLine($"  - property: {propertyInfo.GetTag(MyTagA).Value}");
			Console.WriteLine($"  - parameter: {parameterInfo.GetTag(MyTagA).Value}");

			Console.WriteLine();
			Console.WriteLine($@"  Of course you can use multiple tags per code member.");
			Console.WriteLine($@"  Lets look up the ""{nameof(MyTagB)}"" tag on");
			Console.WriteLine($@"  on the same code members:");
			Console.WriteLine();

			// Looking Up MyTagB
			Console.WriteLine($"  - type: {type.GetTag(MyTagB).Value}");
			Console.WriteLine($"  - method: {methodInfo.GetTag(MyTagB).Value}");
			Console.WriteLine($"  - event: {eventInfo.GetTag(MyTagB).Value}");
			Console.WriteLine($"  - constructor: {constructorInfo.GetTag(MyTagB).Value}");
			Console.WriteLine($"  - field: {fieldInfo.GetTag(MyTagB).Value}");
			Console.WriteLine($"  - property: {propertyInfo.GetTag(MyTagB).Value}");
			Console.WriteLine($"  - parameter: {parameterInfo.GetTag(MyTagB).Value}");

			Console.WriteLine();
			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}

		[Tag(MyTagA, "works :)")]
		[Tag(MyTagB, "A")]
		public class A
		{
			[Tag(MyTagA, "works :3")]
			[Tag(MyTagB, "E")]
			public object Field = new object();

			[Tag(MyTagA, "works :b")]
			[Tag(MyTagB, "F")]
			public object Property { get; set; }

			[Tag(MyTagA, "works :O")]
			[Tag(MyTagB, "D")]
			public A() { }
		}

		[Tag(MyTagA, "works :P")]
		[Tag(MyTagB, "B")]
		public static void Method(
			[Tag(MyTagA, "works ;)")]
			[Tag(MyTagB, "G")]
			object a) => a.ToString();

		[Tag(MyTagA, "works :D")]
		[Tag(MyTagB, "C")]
		public static event Action Event;
	}
}
