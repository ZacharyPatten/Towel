using System;

namespace Towel_Generating
{
	internal class Program
	{
		internal static void Main()
		{
			Console.WriteLine("This is a code generation tool for Towel.");

			Console.WriteLine("Generating Omnitree.cs...");
			Omnitree.Generate();
			Console.WriteLine("Omnitree.cs Generation Complete. :)");
		}
	}
}
