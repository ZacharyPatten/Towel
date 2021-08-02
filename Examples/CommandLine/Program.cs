using System;
using System.IO;
using Towel;
using static Towel.CommandLine;
using static Towel.Statics;

namespace CommandLine
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args is not null && args.Length > 0)
			{
				HandleArguments(args);
				return;
			}

			string[] example_args =
			{
				@"A --a helloworld",
				@"B --b 8",
				$@"C --a test1 --b 5 --c {sourcefilepath()}",
				@"C --a test1",
				@"",
				@"Help",
				@"Version",
				@"Help A",
				@"Help B",
				@"Help C",
			};

			Console.WriteLine("You are runnning the CommandLine example.");
			Console.WriteLine("============================================");
			Console.WriteLine();
			Console.WriteLine("  Towel can parse the command line arguments for you.");
			Console.WriteLine("  Just put the [Command] attribute on your static methods");
			Console.WriteLine("  and call HandleArguments(args) and you are good to go.");
			Console.WriteLine();

			foreach (string example in example_args)
			{
				Console.WriteLine("Example args: " + example);
				args = example.Split(' ');

				// This line of code and the attributes are the only
				// lines of code you would need to add to your code.
				HandleArguments(args);

				Console.WriteLine();
			}

			Console.WriteLine();
			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}

		/// <summary>This is a test method A.</summary>
		/// <param name="a">The parameter a.</param>
		[Command]
		public static void A(
			string? a = default)
		{
			Console.WriteLine($"Method A Called.");
			Console.WriteLine($"a: {a ?? "null"}");
		}

		/// <summary>This is a test method B.</summary>
		/// <param name="a">The parameter a.</param>
		/// <param name="b">The parameter b.</param>
		[Command]
		public static void B(
			string? a = default,
			int b = default)
		{
			Console.WriteLine($"Method B Called.");
			Console.WriteLine($"a: {a ?? "null"}");
			Console.WriteLine($"b: {b}");
		}

		/// <summary>This is a test method C.</summary>
		/// <param name="a">The parameter a.</param>
		/// <param name="b">The parameter b.</param>
		/// <param name="c">The parameter c.</param>
		[Command]
		public static void C(
			string a,
			int b,
			FileInfo c)
		{
			Console.WriteLine($"Method C Called.");
			Console.WriteLine($"a: {a ?? "null"}");
			Console.WriteLine($"b: {b}");
			Console.WriteLine($"c: {c.Name}");
		}
	}
}
