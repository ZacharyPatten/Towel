using System;
using static Towel.CommandLine;

namespace CommandLine
{
	static class Program
	{
		static void Main(string[] args)
		{
			string[] example_args =
			{
				@"A --a helloworld",
				@"B --b 8",
				@"C --a test1 --b 5 --c 7.7",
				@"C --a test1",
				@"Help",
				@"Version",
				@"Help A",
				@"Help B",
				@"Help C",
			};

			foreach (string example in example_args)
			{
				Console.WriteLine("Example args: " + example);
				args = example.Split(' ');

				// This line of code and the attributes are the only 
				// lines of code you would need to add to your code.
				HandleArguments(args);

				Console.WriteLine();
			}

			Console.WriteLine("Press [Enter] To Continue...");
			Console.ReadLine();
		}

		/// <summary>This is a test method A.</summary>
		/// <param name="a">The parameter a.</param>
		[Command] static void A(
			string a = default)
		{
			Console.WriteLine($"Method A Called.");
			Console.WriteLine($"a: {a ?? "null"}");
		}

		/// <summary>This is a test method B.</summary>
		/// <param name="a">The parameter a.</param>
		/// <param name="b">The parameter b.</param>
		[Command] static void B(
			string a = default,
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
		[Command] static void C(
			string a,
			int b,
			decimal c)
		{
			Console.WriteLine($"Method C Called.");
			Console.WriteLine($"a: {a ?? "null"}");
			Console.WriteLine($"b: {b}");
			Console.WriteLine($"c: {c}");
		}
	}
}
