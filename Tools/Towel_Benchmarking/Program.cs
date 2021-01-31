using Towel;
using BenchmarkDotNet.Running;
using static Towel.CommandLine;
using static Towel.Statics;
using System.IO;
using System.Text;
using System;

namespace Towel_Benchmarking
{
	public class Program
	{
		internal const string Name = "name";
		//internal const string Path = "path";

		public static void Main(string[] args)
		{
			if (args is null || args.Length == 0)
			{
				run();
			}
			else
			{
				HandleArguments(args);
			}
		}

		static readonly Type[] Benchmarks =
		{
			typeof(Sort_Benchmarks),
			typeof(DataStructures_Benchmarks),
			typeof(Random_Benchmarks),
			typeof(ToEnglishWords_Benchmarks),
			typeof(Permute_Benchmarks),
			typeof(MapVsDictionary_Add),
			typeof(MapVsDictionary_LookUp),
			typeof(SpanVsArraySorting),
			typeof(RandomWithExclusions),
		};

		/// <summary>Runs the benchmarks.</summary>
		/// <param name="updateDocumentation">Whether or not to update the docfx documentation.</param>
		/// <param name="documentationPath">The path to the docfx documentation file.</param>
		[Command] public static void run(
			bool updateDocumentation = false,
			string documentationPath = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("# Benchmarks");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(@"<a href=""https://github.com/ZacharyPatten/Towel"" alt=""Github Repository""><img alt=""github repo"" src=""https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"" title=""Go To Github Repo"" alt=""Github Repository""></a>");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).");
			stringBuilder.AppendLine();

			foreach (Type type in Benchmarks)
			{
				string output = RunBenchmarkAndGetMarkdownOutput(type);
				stringBuilder.AppendLine($"# {type.GetTag(Name).Value ?? type.Name}");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(output);
			}
			if (updateDocumentation)
			{
				string thisPath = Path.GetDirectoryName(sourcefilepath());
				documentationPath ??= Path.Combine(thisPath, "..", "docfx_project", "articles", "benchmarks.md");
				if (Directory.Exists(Path.GetDirectoryName(documentationPath)))
				{
					File.WriteAllText(documentationPath, stringBuilder.ToString());
				}
				else
				{
					Console.Error.WriteLine("ERROR: documentation path not found");
					Console.Error.WriteLine($"    documentation path: {documentationPath}");
				}
			}
			else
			{
				Console.WriteLine(stringBuilder.ToString());
			}
			Console.WriteLine("Benchmarking Complete. :)");
		}

		public static string RunBenchmarkAndGetMarkdownOutput(Type type)
		{
			var summary = BenchmarkRunner.Run(type);
			string markdownFile = type.FullName + "-report-github.md";
			string markdownPath = Path.Combine(summary.ResultsDirectoryPath, markdownFile);
			try
			{
				return File.Exists(markdownPath)
					? File.ReadAllText(markdownPath)
					: $"Error: markdown file output for {type.Name} not found.";
			}
			catch (Exception exception)
			{
				return $"Error: {Environment.NewLine}{exception}";
			}
		}
	}
}
