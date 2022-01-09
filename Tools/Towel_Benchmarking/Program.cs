using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Running;
using static Towel.CommandLine;

namespace Towel_Benchmarking;

public class Program
{
	internal const string Name = nameof(Name);
	internal const string OutputFile = nameof(OutputFile);

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

	internal static readonly Type[] Benchmarks =
	{
		typeof(SortBenchmarks),
		typeof(DataStructuresBenchmarks),
		typeof(WeightedRandomBenchmarks),
		typeof(ToEnglishWordsBenchmarks),
		typeof(PermuteBenchmarks),
		typeof(MapVsDictionaryAddBenchmarks),
		typeof(MapVsDictionaryLookUpBenchmarks),
		typeof(SpanVsArraySortingBenchmarks),
		typeof(RandomWithExclusionsBenchmarks),
		typeof(HeapGenericsVsDelegatesBenchmarks),
		typeof(LazyInitializationBenchmarks),
		typeof(LazyCachingBenchmarks),
		typeof(LazyConstructionBenchmarks),
	};

	/// <summary>Runs the benchmarks.</summary>
	/// <param name="updateDocumentation">Whether or not to update the docfx documentation.</param>
	/// <param name="refreshToc">Whether or not to refresh "toc.yml".</param>
	/// <param name="tocPath">The path to the docfx documentation file.</param>
	/// <param name="includeRegex">Runs all the benchmarks that match a regex pattern.</param>
	/// <example>dotnet run --configuration Release run --updateDocumentation True --refreshToc True</example>
	/// <example>dotnet run --configuration Release run --updateDocumentation True --refreshToc True --includeRegex PATTERN</example>
	[Command]
	public static void run(
		bool updateDocumentation = false,
		bool refreshToc = false,
		string? tocPath = null,
		string? includeRegex = null)
	{
		string thisPath = Path.GetDirectoryName(sourcefilepath())!;
		if (refreshToc)
		{
			tocPath ??= Path.Combine(thisPath, "..", "docfx_project", "benchmarks", "toc.yml");
			string[] lines =
			{
				"- name: Introduction",
				"  href: index.md",
			};
			File.WriteAllLines(tocPath, lines);
		}
		string? benchmarks_mdPath = default;
		if (updateDocumentation)
		{
			benchmarks_mdPath = Path.Combine(thisPath, "..", "docfx_project", "benchmarks", "index.md");
			string[] lines =
			{
				"# Towel Benchmarks",
				"",
				@"<a href=""https://github.com/ZacharyPatten/Towel"" alt=""Github Repository""><img alt=""github repo"" src=""https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"" title=""Go To Github Repo"" alt=""Github Repository""></a>",
				"",
				"The source code for all becnhmarks are in [Tools/ Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).",
				"",
			};
			File.WriteAllLines(benchmarks_mdPath, lines);
		}
		foreach (Type type in Benchmarks)
		{
			if (includeRegex is null || Regex.Match(type.Name, includeRegex).Success)
			{
				StringBuilder stringBuilder = new();
				string output = RunBenchmarkAndGetMarkdownOutput(type);
				stringBuilder.AppendLine($"# {type.GetTag(Name).Value ?? type.Name}");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(@"<a href=""https://github.com/ZacharyPatten/Towel"" alt=""Github Repository""><img alt=""github repo"" src=""https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"" title=""Go To Github Repo"" alt=""Github Repository""></a>");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(output);
				if (updateDocumentation)
				{
					string documentationPath = Path.Combine(thisPath, "..", "docfx_project", "benchmarks", type.GetTag(OutputFile).Value + ".md");
					if (Directory.Exists(Path.GetDirectoryName(documentationPath)))
					{
						File.WriteAllText(documentationPath, stringBuilder.ToString());
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Error.WriteLine("-----------------------------------");
						Console.Error.WriteLine("ERROR: documentation path not found");
						Console.Error.WriteLine($"    documentation path: {documentationPath}");
						Console.Error.WriteLine("-----------------------------------");
						Console.ResetColor();
					}
				}
				else
				{
					Console.WriteLine(stringBuilder.ToString());
				}
			}
			if (refreshToc)
			{
				string[] lines =
				{
					$"- name: {type.GetTag(Name).Value}",
					$"  href: {type.GetTag(OutputFile).Value}.md",
				};
				File.AppendAllLines(tocPath!, lines);
			}
			if (updateDocumentation)
			{
				string[] lines =
				{
					$"- [{type.GetTag(Name).Value}]({type.GetTag(OutputFile).Value}.md)",
				};
				File.AppendAllLines(benchmarks_mdPath!, lines);
			}
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

#region Shared Random Data Generation

public class Person
{
	public Guid Id;
	public string? FirstName;
	public string? LastName;
	public DateTime DateOfBirth;
}

public struct EquatePerson : IFunc<Person, Person, bool>
{
	public bool Invoke(Person a, Person b) => a.Id == b.Id;
}

public struct HashPerson : IFunc<Person, int>
{
	public int Invoke(Person a) => a.Id.GetHashCode();
}

public struct ComparePersonFirstName : IFunc<Person, Person, CompareResult>
{
	public CompareResult Invoke(Person a, Person b) => Compare(a.FirstName, b.FirstName);
}

public static partial class RandomData
{
	public static class DataStructures
	{
		public static Person[][] RandomData => GenerateBenchmarkData();

		public static Person[][] GenerateBenchmarkData()
		{
			DateTime minimumBirthDate = new(1950, 1, 1);
			DateTime maximumBirthDate = new(2000, 1, 1);

			Random random = new(7);
			Person[] GenerateData(int count)
			{
				Person[] data = new Person[count];
				for (int i = 0; i < count; i++)
				{
					data[i] = new Person()
					{
						Id = Guid.NewGuid(),
						FirstName = random.NextEnglishAlphabeticString(random.Next(5, 11)),
						LastName = random.NextEnglishAlphabeticString(random.Next(5, 11)),
						DateOfBirth = random.NextDateTime(minimumBirthDate, maximumBirthDate)
					};
				}
				return data;
			}

			return new Person[][]
			{
					GenerateData(10),
					GenerateData(100),
					GenerateData(1000),
			};
		}
	}
}

	#endregion
