using System;
using System.Reflection;
using Towel;

BindingFlags bf =
	BindingFlags.Public |
	BindingFlags.NonPublic |
	BindingFlags.Static |
	BindingFlags.Instance;

Type            type            = typeof(A);
MethodInfo      methodInfo      = type.GetMethod(nameof(A.Method), bf)!;
EventInfo       eventInfo       = type.GetEvent(nameof(A.Event))!;
ConstructorInfo constructorInfo = type.GetConstructor(Array.Empty<Type>())!;
FieldInfo       fieldInfo       = type.GetField(nameof(A.Field), bf)!;
PropertyInfo    propertyInfo    = type.GetProperty(nameof(A.Property), bf)!;
ParameterInfo   parameterInfo   = methodInfo.GetParameters()[0];

Console.WriteLine("You are runnning the TagAttributes example.");
Console.WriteLine("============================================");
Console.WriteLine();
Console.WriteLine(@"  Towel has value-based ""tag"" attributes. Rather than making");
Console.WriteLine(@"  custom attribute types, you can ""tag"" code members with");
Console.WriteLine(@"  constant values.");
Console.WriteLine();
Console.WriteLine($@"  Lets look up the ""A"" tag on each");
Console.WriteLine($@"  type of code member:");
Console.WriteLine();
Console.WriteLine($"  - type: {type.GetTag("A").Value}");
Console.WriteLine($"  - method: {methodInfo.GetTag("A").Value}");
Console.WriteLine($"  - event: {eventInfo.GetTag("A").Value}");
Console.WriteLine($"  - constructor: {constructorInfo.GetTag("A").Value}");
Console.WriteLine($"  - field: {fieldInfo.GetTag("A").Value}");
Console.WriteLine($"  - property: {propertyInfo.GetTag("A").Value}");
Console.WriteLine($"  - parameter: {parameterInfo.GetTag("A").Value}");
Console.WriteLine();
Console.WriteLine($@"  Of course you can use multiple tags per code member.");
Console.WriteLine($@"  Lets look up the ""B"" tag on");
Console.WriteLine($@"  on the same code members:");
Console.WriteLine();
Console.WriteLine($"  - type: {type.GetTag("B").Value}");
Console.WriteLine($"  - method: {methodInfo.GetTag("B").Value}");
Console.WriteLine($"  - event: {eventInfo.GetTag("B").Value}");
Console.WriteLine($"  - constructor: {constructorInfo.GetTag("B").Value}");
Console.WriteLine($"  - field: {fieldInfo.GetTag("B").Value}");
Console.WriteLine($"  - property: {propertyInfo.GetTag("B").Value}");
Console.WriteLine($"  - parameter: {parameterInfo.GetTag("B").Value}");
Console.WriteLine();
Console.WriteLine("============================================");
Console.WriteLine("Example Complete...");
Console.WriteLine();
ConsoleHelper.PromptPressToContinue();

[Tag("A", "works :)")]
[Tag("B", "A")]
public class A
{
	[Tag("A", "works :3")]
	[Tag("B", "E")]
	public object? Field;

	[Tag("A", "works :b")]
	[Tag("B", "F")]
	public object? Property { get; set; }

	[Tag("A", "works :O")]
	[Tag("B", "D")]
	public A() { }

	[Tag("A", "works :P")]
	[Tag("B", "B")]
	public static void Method(
		[Tag("A", "works ;)")]
		[Tag("B", "G")]
		object a) => a.ToString();

	[Tag("A", "works :D")]
	[Tag("B", "C")]
	public static event Action? Event;
}
