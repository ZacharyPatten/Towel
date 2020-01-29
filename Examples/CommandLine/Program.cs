using System;
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
		//     dotnet CommandLine.dll A: 1, B: 2, C: 3, D: 4
		//     dotnet CommandLine.dll D: 1, C: 2, B: 3, A: 4
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
				Console.WriteLine(DefaultInfoString);
				Console.WriteLine("  Summary: TODO");
				Console.WriteLine("  Documentation: TODO");
				Console.WriteLine("  Contact(s): TODO");
				return;
			}
			Console.WriteLine(A);
			Console.WriteLine(B - 1);
			Console.WriteLine(C - 1.5);
			Console.WriteLine(D.HasValue ? D.Value : nameof(D) + ": " + D.Status);

			// The "Status" property gives you information about the argument.
			// - Default: The arguemnt was not provided but a default value exists.
			// - SyntaxError: The definition of the CommandLineArgument is invalid (not supported). The code needs to be updated.
			// - NotProvided: The argument was not provided by the command line arguments and no default exists.
			// - DuplicateProvided: The argument was provided multiple times. The command line arguments are invalid.
			// - ParseFailed: The argument was provided, but the relative value was not valid.
			// - ValueProvided: The argument was provided and successfully parsed.

			// The "HasValue" property returns true if the Status is Default or ValueProvided.
		}
	}
}
