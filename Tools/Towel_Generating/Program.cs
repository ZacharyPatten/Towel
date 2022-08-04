using System.IO;
using static Towel.CommandLine;

namespace Towel_Generating;

internal class Program
{
	public const int DefaultGenerationCount = 7;

	internal static void Main(string[] args)
	{
		HandleArguments(args);
	}

	/// <summary>Generates the source code for "Omnitree.cs".</summary>
	/// <param name="output">The file path to output the result to.</param>
	/// <example>dotnet run Omnitree --output "..\..\Sources\Towel\DataStructures\Omnitree2.cs"</example>
	[Command]
	public static void Omnitree(string? output = null) => Output(OmnitreeGenerator.Run(), output);

	/// <summary>Generates the source code for "Link.cs".</summary>
	/// <param name="output">The file path to output the result to.</param>
	/// <example>dotnet run Link --output "..\..\Sources\Towel\DataStructures\Link.cs"</example>
	[Command]
	public static void Link(string? output = null) => Output(LinkGenerator.Run(), output);

	/// <summary>Generates the source code for "Functional.cs".</summary>
	/// <param name="output">The file path to output the result to.</param>
	/// <example>dotnet run Functional --output "..\..\Sources\Towel\Functional.cs"</example>
	[Command]
	public static void Functional(string? output = null) => Output(FunctionalGenerator.Run(), output);

	/// <summary>Generates the source code for "Extensions-Tuple.cs".</summary>
	/// <param name="output">The file path to output the result to.</param>
	/// <example>dotnet run TupleExtensions --output "..\..\Sources\Towel\Statics-Tuple.cs"</example>
	[Command]
	public static void TupleExtensions(string? output = null) => Output(TupleExtensionsGenerator.Run(), output);

	/// <summary>Generates the source code.</summary>
	/// <example>dotnet run All --omnitreeOutput "..\..\Sources\Towel\DataStructures\Omnitree2.cs" --linkOutput "..\..\Sources\Towel\DataStructures\Link.cs" --functionalOutput "..\..\Sources\Towel\Functional.cs" --tupleExtensionsOutput "..\..\Sources\Towel\Statics-Tuple.cs"</example>
	[Command]
	public static void All(
		string omnitreeOutput,
		string linkOutput,
		string functionalOutput,
		string tupleExtensionsOutput)
	{
		if (ContainsDuplicates<string>(Ɐ(omnitreeOutput, linkOutput, functionalOutput, tupleExtensionsOutput)))
		{
			Console.WriteLine("Invalid arguments. Duplicate output files.");
		}
		Omnitree(omnitreeOutput);
		Link(linkOutput);
		Functional(functionalOutput);
		TupleExtensions(tupleExtensionsOutput);
	}

	public static void Output(string code, string? filePath)
	{
		if (filePath is null)
		{
			Console.WriteLine(code);
		}
		else
		{
			try
			{
				File.WriteAllText(filePath, code);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}
	}
}
