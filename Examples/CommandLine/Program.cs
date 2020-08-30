using System;
using Towel;
using static System.Console;
using static Towel.CommandLine;

namespace CommandLine
{
	class Program
	{
		// The "Towel.CommandLine.Argument" type helps handle command line arguments. If used as a static
		// field in the entry type of a console application, it will be automatically assigned from the
		// command line arguments. Examples Of Command Lines:
		//
		//     dotnet CommandLine.dll A: "hello world"
		//     dotnet CommandLine.dll B: 3
		//     dotnet CommandLine.dll A: 1 B: 2 C: 3 D: 4
		//     dotnet CommandLine.dll A: hello, D: world
		//     dotnet CommandLine.dll Help
		//     dotnet CommandLine.dll Version

		static Argument Version;
		static Argument Help;
		static Argument<string> A = "default";
		static Argument<int> B = -1;
		static Argument<float> C = -1.5f;
		static Argument<string> D;

		static void Main()
		{
			if (Version.Exists || Help.Exists)
			{
				WriteLine(DefaultInfoString);
				WriteLine("  Summary: TODO");
				WriteLine("  Documentation: TODO");
				WriteLine("  Contact(s): TODO");
				return;
			}
			WriteLine($"A: {A}");
			WriteLine($"B: {B}");
			WriteLine($"C: {C}");
			WriteLine($"D: {(D.HasValue ? D.Value : D.Status.ToString())}");

			WriteLine();
			WriteLine($@"Index Of Argument B: {(B.Index.HasValue ? B.Index.ToString() : "null")}");

			WriteLine();
			WriteLine($"Default Value Of C: {C.DefaultValue}");

			// The "DefaultValue" property gets the default value that the argument was constructed with.

			// The "Index" property is the location of the relative argument if it was found.

			// The "HasValue" property returns true if the Status is Default or ValueProvided.

			// The "Status" property gives you information about the argument.
			// - Default: The arguemnt was not provided but a default value exists.
			// - SyntaxError: The definition of the CommandLineArgument is invalid (not supported). The code needs to be updated.
			// - NotProvided: The argument was not provided by the command line arguments and no default exists.
			// - DuplicateProvided: The argument was provided multiple times. The command line arguments are invalid.
			// - ParseFailed: The argument was provided, but the relative value was not valid.
			// - ValueProvided: The argument was provided and successfully parsed.

			WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}
	}
}
