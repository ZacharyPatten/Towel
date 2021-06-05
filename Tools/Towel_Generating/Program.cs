using System;
using System.IO;
using static Towel.CommandLine;

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
		public static void Omnitree(
			string output = null)
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
	}
}
