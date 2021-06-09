using System;
using System.IO;
using Towel;
using static Towel.CommandLine;
using static Towel.Statics;

namespace Towel_Generating
{
	internal class Program
	{
		internal static void Main(string[] args) => HandleArguments(args);

		/// <summary>Generates the source code for "Omnitree.cs".</summary>
		/// <param name="output">The file path to output the result to.</param>
		/// <example>dotnet run Omnitree --output "..\..\Sources\Towel\DataStructures\Omnitree2.cs"</example>
		/// <example>dotnet Towel_Generating.dll Omnitree --output "..\..\..\..\..\Sources\Towel\DataStructures\Omnitree2.cs"</example>
		[Command]
		public static void Omnitree(string? output = null)
		{
			string code = OmnitreeGenerator.Run();
			if (output is null)
			{
				Console.WriteLine(code);
			}
			else
			{
				try
				{
					File.WriteAllText(output, code);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
				}
			}
		}

		/// <summary>Generates the source code for "Link.cs".</summary>
		/// <param name="output">The file path to output the result to.</param>
		/// <example>dotnet run Link --output "..\..\Sources\Towel\DataStructures\Link.cs"</example>
		/// <example>dotnet Towel_Generating.dll Link --output "..\..\..\..\..\Sources\Towel\DataStructures\Link.cs"</example>
		[Command]
		public static void Link(string? output = null)
		{
			string code = LinkGenerator.Run();
			if (output is null)
			{
				Console.WriteLine(code);
			}
			else
			{
				try
				{
					File.WriteAllText(output, code);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
				}
			}
		}

		/// <summary>Generates the source code.</summary>
		/// <example>dotnet run All --omnitreeOutput "..\..\Sources\Towel\DataStructures\Omnitree2.cs" --omnitreeOutput "..\..\Sources\Towel\DataStructures\Link.cs"</example>
		/// <example>dotnet Towel_Generating.dll All --omnitreeOutput "..\..\Sources\Towel\DataStructures\Omnitree2.cs" --omnitreeOutput "..\..\Sources\Towel\DataStructures\Link.cs"</example>
		[Command]
		public static void All(
			string omnitreeOutput,
			string linkOutput)
		{
			if (ContainsDuplicates<string>(Ɐ(omnitreeOutput, linkOutput)))
			{
				Console.WriteLine("Invalid arguments. Duplicate output files.");
			}
			Omnitree(omnitreeOutput);
			Link(linkOutput);
		}
	}
}
