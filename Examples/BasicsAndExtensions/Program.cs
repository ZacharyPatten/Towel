using System;
using Towel;
using Towel.DataStructures;
using Towel.Mathematics;
using System.Linq;
using static Towel.Statics;

namespace BasicsAndExtensions
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("You are runnning the BasicsAndExtensions example.");
			Console.WriteLine("============================================");
			Console.WriteLine();

			#region Range Syntax Helpers
			{
				Console.WriteLine("  Range Syntax Helpers--------------------------------");
				Console.WriteLine();

				Console.WriteLine(@$"    (0..5).ToArray() = {string.Join(", ", (0..5).ToArray())}");
				Console.WriteLine();
				Console.WriteLine(@$"    (5..0).ToArray() = {string.Join(", ", (5..0).ToArray())}");
				Console.WriteLine();
				Console.WriteLine(@$"    ('a'..'f').ToArray(i => (char)i) = {string.Join(", ", ('a'..'f').ToArray(i => (char)i))}");
				Console.WriteLine();
				Console.WriteLine(@$"    (1..6).Select(i => i + 100) = {string.Join(", ", (1..6).Select(i => i + 100))}");
				Console.WriteLine();
				Console.WriteLine(@$"    ArrayHelper.NewFromRanges(1..4, 9..6, 4..7) = {string.Join(", ", ArrayHelper.NewFromRanges(1..4, 9..6, 4..7))}");

				Pause();
			}
			#endregion

			#region Multi String Replace
			{
				Console.WriteLine("  Multi String Replace -----------------------");
				Console.WriteLine();

				string a = "a b c d";
				string a2 = a.Replace(("a", "aaa"), ("c", "ccc"));

				Console.WriteLine($@"    a = ""{a}""");
				Console.WriteLine($@"    a.Replace((""a"", ""aaa""), (""c"", ""ccc"")) -> ""{a2}""");

				Console.WriteLine();

				string b = "123";
				string b2 = b.Replace(("1", "2"), ("2", "3"), ("3", "4"));

				Console.WriteLine($@"    b = ""{b}""");
				Console.WriteLine($@"    b.Replace((""1"", ""2""), (""2"", ""3""), (""3"", ""4"")) -> ""{b2}""");

				Pause();
			}
			#endregion

			#region TryParse
			{
				var (_, a) = TryParse<double>("123.4");
				var (_, b) = TryParse<float>("12.3");
				var (_, c) = TryParse<byte>("1");
				var (_, d) = TryParse<int>("1234");
				var (_, e) = TryParse<Program>("1234");
				var (_, f) = TryParse<ConsoleColor>("Red");
				var (_, g) = TryParse<StringComparison>("Ordinal");

				Console.WriteLine("  TryParse------------------------------------");
				Console.WriteLine();
				Console.WriteLine($"    TryParse(\"123.4\", out double a) -> {a}d");
				Console.WriteLine($"    TryParse(\"12.3\", out float b) -> {b}f");
				Console.WriteLine($"    TryParse(\"1\", out byte c) -> {c}");
				Console.WriteLine($"    TryParse(\"1234\", out int d) -> {d}");
				Console.WriteLine($"    TryParse(\"1234\", out Program e) -> {e?.ToString() ?? "null"}");
				Console.WriteLine($"    TryParse(\"Red\", out ConsoleColor f) -> {f}");
				Console.WriteLine($"    TryParse(\"Ordinal\", out StringComparison g) -> {g}");
				Pause();
			}
			#endregion

			#region Convert
			{
				// Note: the main use case for this is converting types when using generics (not when the types are known at compile time).

				double a = Convert<int, double>(1234);
				float b = Convert<int, float>(123);
				int c = Convert<double, int>(123.4d);
				int d = Convert<float, int>(12.3f);

				Console.WriteLine("  Convert------------------------------------");
				Console.WriteLine();
				Console.WriteLine($"    Convert<int, double>(1234) -> {a}d");
				Console.WriteLine($"    Convert<int, float>(123) -> {b}f");
				Console.WriteLine($"    Convert<double, int>(123.4d) -> {c}");
				Console.WriteLine($"    Convert<float, int>(12.3f) -> {d}");
				Pause();
			}
			#endregion

			#region Decimal To Words
			{
				Console.WriteLine("  Converting Decimal To Words---------------------------");
				Console.WriteLine();

				decimal a = 12345.6789m;
				Console.WriteLine($"    {a} -> {a.ToEnglishWords()}");

				decimal b = 999.888m;
				Console.WriteLine($"    {b} -> {b.ToEnglishWords()}");

				decimal c = 1111111.2m;
				Console.WriteLine($"    {c} -> {c.ToEnglishWords()}");

				Pause();
			}
			#endregion

			#region To/From Roman Numeral
			{
				Console.WriteLine("  To/From Roman Numeral--------------------------------");
				Console.WriteLine();

				string a = "I";
				Console.WriteLine(@$"    {nameof(TryParseRomanNumeral)}(""{a}"") = {TryParseRomanNumeral(a)}");

				string b = "XLII";
				Console.WriteLine(@$"    {nameof(TryParseRomanNumeral)}(""{b}"") = {TryParseRomanNumeral(b)}");

				string c = "invalid";
				Console.WriteLine(@$"    {nameof(TryParseRomanNumeral)}(""{c}"") = {TryParseRomanNumeral(c)}");

				Console.WriteLine();

				int d = 42;
				Console.WriteLine(@$"    {nameof(TryToRomanNumeral)}({d}) = {TryToRomanNumeral(d)}");

				int e = 77;
				Console.WriteLine(@$"    {nameof(TryToRomanNumeral)}({e}) = {TryToRomanNumeral(e)}");

				int f = -1;
				Console.WriteLine(@$"    {nameof(TryToRomanNumeral)}({f}) = {TryToRomanNumeral(f)}");

				Pause();
			}
			#endregion

			#region Type To C# Source Code
			{
				Console.WriteLine("  Type To C# Source Code---------------------------");
				Console.WriteLine();
				Console.WriteLine("    Note: this can be useful for runtime compilation from strings");
				Console.WriteLine();

				Console.WriteLine(@$"    {typeof(IOmnitreePoints<Vector<double>, double, double, double>)}");
				Console.WriteLine(@$"    {typeof(IOmnitreePoints<Vector<double>, double, double, double>).ConvertToCSharpSource()}");
				Console.WriteLine();

				Console.WriteLine(@$"    {typeof(Symbolics.Add)}");
				Console.WriteLine(@$"    {typeof(Symbolics.Add).ConvertToCSharpSource()}");

				Pause();
			}
			#endregion

			#region Random Extensions
			{
				Console.WriteLine("  Random Extensions---------------------------");
				Console.WriteLine();
				Console.WriteLine("    Note: there are overloads of these methods");
				Console.WriteLine();

				Random random = new();

				Console.WriteLine($"    Random.NextLong(): {random.NextLong()}");
				Console.WriteLine($"    Random.NextDateTime(): {random.NextDateTime()}");
				Console.WriteLine($"    Random.NextAlphaNumericString(15): {random.NextEnglishAlphaNumericString(15)}");
				Console.WriteLine($"    Random.NextChar('a', 'z'): {random.NextChar('a', 'z')}");
				Console.WriteLine($"    Random.NextDecimal(): {random.NextDecimal()}");
				Console.WriteLine($"    Random.NextTimeSpan(): {random.NextTimeSpan()}");
				Console.WriteLine($"    Random.Next(5, 0, 100, excluded: {{ 50, 51, 52, 53 }}): {string.Join(", ", random.Next(5, 0, 100, excluded: new[] { 50, 51, 52, 53 }))}");
				Console.WriteLine($"    Random.NextUnique(5, 0, 100): {string.Join(", ", random.NextUnique(5, 0, 100))}");
				Console.WriteLine($"    Random.NextUnique(5, 0, 100, excluded: {{ 50, 51, 52, 53 }}): {string.Join(", ", random.NextUnique(5, 0, 100, excluded: new[] { 50, 51, 52, 53 }))}");

				(string Name, double Weight)[] weightedNames = new[]
				{
					("Dixie Normous ", 40d),
					("Harry Dick    ", 70d),
					("Ivana Humpalot", 40d),
					("Ben Dover     ", 80d),
					("Hue Mungus    ", 30d),
					("Mr. Bates     ", 20d),
				};
				Console.WriteLine("    Random.Next (weighted)... ");
				Console.WriteLine();
				Console.WriteLine("        | Name           | Weight |");
				Console.WriteLine("        |----------------|--------|");
				foreach (var (Name, Weight) in weightedNames)
					Console.WriteLine($"        | {Name} |   {Weight}   |");
				Console.WriteLine();
				Console.WriteLine($"        Random Weighted Selection: {random.Next(weightedNames)}");
				Pause();
			}
			#endregion

			#region XML Code Documentation Via Reflection
			{
				Console.WriteLine("  XML Code Documentation Extensions------------");
				Console.WriteLine();
				Console.WriteLine("    You can access XML on source code via reflection");
				Console.WriteLine("    using Towel's extension methods.");
				Console.WriteLine();

				/// You may need to call <see cref="Meta.LoadXmlDocumentation"/> first if the documentation can't be found via reflection.

				Console.Write("    XML Documentation On Towel.TagAttribute:");
				Console.WriteLine(typeof(TagAttribute).GetDocumentation());
				Console.Write("    XML Documentation On Towel.Constant<float>.Pi:");
				Console.WriteLine(typeof(Constant<float>).GetProperty(nameof(Constant<float>.Pi))!.GetDocumentation());
				Pause();
			}
			#endregion

			#region Sorting
			{
				// Note: these functions are not restricted to array types. You can use the
				// overloads with "Get" and "Assign" delegates to use them on any int-indexed
				// data structure.

				Console.WriteLine("  Sorting Algorithms----------------------");
				Console.WriteLine();

				int[] dataSet = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

				string DataSetToString() => string.Join(", ", dataSet.Select(x => x.ToString()));

				Console.WriteLine($"    Data Set:  {DataSetToString()}");

				Shuffle<int>(dataSet);
				Console.WriteLine($"    Shuffle:   {DataSetToString()}");

				SortBubble<int>(dataSet);
				Console.WriteLine($"    Bubble:    {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortSelection<int>(dataSet);
				Console.WriteLine($"    Selection: {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortInsertion<int>(dataSet);
				Console.WriteLine($"    Insertion: {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortQuick<int>(dataSet);
				Console.WriteLine($"    Quick:     {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortMerge<int>(dataSet);
				Console.WriteLine($"    Merge:     {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortHeap<int>(dataSet);
				Console.WriteLine($"    Heap:      {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortTim<int>(dataSet);
				Console.WriteLine($"    Tim:       {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortOddEven<int>(dataSet);
				Console.WriteLine($"    OddEven:   {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortSlow<int>(dataSet);
				Console.WriteLine($"    Slow:      {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortCocktail<int>(dataSet);
				Console.WriteLine($"    Cocktail:  {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortShell<int>(dataSet);
				Console.WriteLine($"    Shell:     {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortGnome<int>(dataSet);
				Console.WriteLine($"    Gnome:     {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortComb<int>(dataSet);
				Console.WriteLine($"    Comb:      {DataSetToString()}");

				Shuffle<int>(dataSet);
				SortCycle<int>(dataSet);
				Console.WriteLine($"    Cycle:     {DataSetToString()}");

				Shuffle<int>(dataSet);
				Console.WriteLine("    Bogo:      Disabled (usually very slow...)");
				//SortBogo<int>(dataSet);
				//Console.WriteLine($"    Bogo:      {DataSetToString()}");

				Pause();
			}
			#endregion

			#region Get X Least/Greatest
			{
				Console.WriteLine("  Get X Least/Greatest--------------------------");
				Console.WriteLine();

				int[] a = { 10, 2, 9, 1, 8, 3 };
				int count = 3;

				Console.WriteLine($"    GetLeast([{string.Join(", ", a)}], {count}) -> [{string.Join(", ", GetLeast<int, Int32Compare>(a.AsSpan(), count))}]");
				Console.WriteLine($"    GetGreatest([{string.Join(", ", a)}], {count}) -> [{string.Join(", ", GetGreatest<int, Int32Compare>(a.AsSpan(), count))}]");
				Pause();
			}
			#endregion

			#region IsOrdered
			{
				Console.WriteLine("  IsOrdered------------------------------------");
				Console.WriteLine();

				int[] a = { 1, 2, 3, 4, 5 }; // least to greatest
				Console.WriteLine($"    IsOrdered({string.Join(", ", a)}) -> {IsOrdered<int>(a)}");

				int[] b = { 5, 4, 3, 2, 1 }; // greatest to least
				Console.WriteLine($"    IsOrdered({string.Join(", ", b)}, (a, b) => Compare(b, a)) -> {IsOrdered<int>(b, (a, b) => Compare(b, a))}");

				int[] c = { 1, 5, 3, 4, 2 }; // unordered
				Console.WriteLine($"    IsOrdered({string.Join(", ", c)}) -> {IsOrdered<int>(c)}");

				string[] d = { "a", "ba", "cba", "dcba", }; // least to greatest (strings)
				Console.WriteLine($"    IsOrdered({string.Join(", ", d)}) -> {IsOrdered<string>(d)}");
				
				Pause();
			}
			#endregion

			#region FilterOrdered
			{
				Console.WriteLine("  FilterOrdered------------------------------------");
				Console.WriteLine();
				Console.WriteLine("    Filters out values that are not in order.");
				Console.WriteLine();

				int[] a = { 1, 2, 3 };
				Console.Write($"    FilterOrdered({string.Join(", ", a)}) ->");
				FilterOrdered<int>(a, i => Console.Write(" " + i));
				Console.WriteLine();

				int[] b = { 1, -1, 2, -2, 3, -3 };
				Console.Write($"    FilterOrdered({string.Join(", ", b)}) ->");
				FilterOrdered<int>(b, i => Console.Write(" " + i));
				Console.WriteLine();

				Pause();
			}
			#endregion

			#region Binary Search
			{
				Console.WriteLine("  Search.Binary----------------------");
				Console.WriteLine();

				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, };
				Console.WriteLine($"    {nameof(values)}: {{ {string.Join(", ", values)} }}");
				Console.WriteLine();

				int valueToSearchFor = values[^2];
				Console.WriteLine($"    Let's search for value {valueToSearchFor}...");

				var result = SearchBinary(values, valueToSearchFor);
				Console.WriteLine($"      Found: {result.Found}");
				Console.WriteLine($"      Index: {result.Index}");
				Console.WriteLine($"      Value: {result.Value}");

				Pause();
			}
			#endregion

			#region IsPalindrome
			{
				Console.WriteLine("  Is Palindrome ----------------------");
				Console.WriteLine();

				string kayak = "kayak";
				Console.WriteLine($@"      IsPalindrome(""{kayak}""): {IsPalindrome(kayak)}");

				int[] values = { 1, 2, 3, 4 };
				Console.WriteLine($@"      IsPalindrome({{ {string.Join(", ", values)} }}): {IsPalindrome<int>(values)}");

				Pause();
			}
			#endregion

			#region IsInterleaved
			{
				Console.WriteLine("  Is Interleaved ----------------------");
				Console.WriteLine();

				string abc = "abc";
				string xyz = "xyz";
				string axbycz = "axbycz";
				Console.WriteLine($@"      IsInterleavedRecursive(""{abc}"", ""{xyz}"", ""{axbycz}""): {IsInterleavedRecursive(abc, xyz, axbycz)}");
				Console.WriteLine($@"      IsInterleavedIterative(""{abc}"", ""{xyz}"", ""{axbycz}""): {IsInterleavedIterative(abc, xyz, axbycz)}");

				string a = "a";
				string b = "b";
				string c = "c";
				Console.WriteLine($@"      IsInterleavedRecursive(""{a}"", ""{b}"", ""{c}""): {IsInterleavedRecursive(a, b, c)}");
				Console.WriteLine($@"      IsInterleavedIterative(""{a}"", ""{b}"", ""{c}""): {IsInterleavedIterative(a, b, c)}");

				Pause();
			}
			#endregion

			#region IsReorderOf
			{
				Console.WriteLine("  Is IsReorderOf ----------------------");
				Console.WriteLine();
				Console.WriteLine(@"    This is commonly called ""anagrams"".");
				Console.WriteLine();

				string abcdef = "abcdef";
				string fedcba = "fedcba";
				Console.WriteLine($@"      IsReorderOf(""{abcdef}"", ""{fedcba}""): {IsReorderOf<char>(abcdef, fedcba)}");

				string aabbcc = "aabbcc";
				string abbbbc = "abbbbc";
				Console.WriteLine($@"      IsReorderOf(""{aabbcc}"", ""{abbbbc}""): {IsReorderOf<char>(aabbcc, abbbbc)}");

				Pause();
			}
			#endregion

			#region Hamming Distance
			{
				Console.WriteLine("  HammingDistance----------------");
				Console.WriteLine();
				{
					string a = "book";
					string b = "barf";
					Console.WriteLine($@"    HammingDistance(""{a}"", ""{b}""): {HammingDistance(a, b)}");
				}
				Pause();
			}
			#endregion

			#region Levenshtein Distance
			{
				Console.WriteLine("  LevenshteinDistance----------------");
				Console.WriteLine();
				{
					string a = "book";
					string b = "barf";
					Console.WriteLine($@"    Recursive(""{a}"", ""{b}""): {LevenshteinDistanceRecursive(a, b)}");
				}
				{
					string a = "hello";
					string b = "help";
					Console.WriteLine($@"    Iterative(""{a}"", ""{b}""): {LevenshteinDistanceIterative(a, b)}");
				}
				Pause();
			}
			#endregion

			#region CombineRanges
			{
				Console.WriteLine("  CombineRanges--------------------");
				Console.WriteLine();
				Console.WriteLine("    CombineRanges combines ranges that have no gaps");
				Console.WriteLine("    between them. So, if you have two ranges (2000-2006)");
				Console.WriteLine("    and (2004-2010) it would combine them to be (2000-2010).");
				Console.WriteLine("    But ranges (1-3) and (7-9) could not be combined unless");
				Console.WriteLine("    there were other ranges to fill in the (4-6) gap.");
				Console.WriteLine();

				// int ranges
				Console.WriteLine("    Int Range Example:");
				(int, int)[] intRanges = new[]
				{
					(1,   5),
					(4,   7),
					(15, 18),
					(3,  10),
				};
				foreach (var range in intRanges)
				{
					Console.WriteLine($"      {range}");
				}
				Console.WriteLine($"    CombineRanges:");
				foreach (var range in CombineRanges(intRanges))
				{
					Console.WriteLine($"      {range}");
				}
				Console.WriteLine();

				// DateTime ranges
				Console.WriteLine("    DateTime Range Example:");
				(DateTime, DateTime)[] dateTimeRanges = new[]
				{
					(new DateTime(2000, 1, 1), new DateTime(2002, 1, 1)),
					(new DateTime(2000, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2003, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2011, 1, 1), new DateTime(2016, 1, 1)),
				};
				foreach (var range in dateTimeRanges)
				{
					Console.WriteLine($"      {range}");
				}
				Console.WriteLine($"    CombineRanges:");
				foreach (var range in CombineRanges(dateTimeRanges))
				{
					Console.WriteLine($"      {range}");
				}
				Console.WriteLine();

				// string ranges
				Console.WriteLine("    String Range Example:");
				(string, string)[] stringRanges = new[]
				{
					("tux", "zebra"),
					("a",   "hippo"),
					("boy", "joust"),
					("car",   "dog"),
				};
				foreach (var range in stringRanges)
				{
					Console.WriteLine($"      {range}");
				}
				Console.WriteLine($"    CombineRanges:");
				foreach (var range in CombineRanges(stringRanges))
				{
					Console.WriteLine($"      {range}");
				}

				Pause();
			}
			#endregion

			#region Switch
			{
				Console.WriteLine("  Switch syntax----------------------");
				Console.WriteLine();
				Console.WriteLine("    I don't recommend using this Switch syntax.");
				Console.WriteLine("    I added it for fun. :D");
				Console.WriteLine();

				Console.Write("    With Parameter: ");
				for (int i = 1; i <= 4; i++)
				{
					// Parameter
					Switch (i)
					(
						(1,       () => Console.Write("1, ")),
						(2,       () => Console.Write("2, ")),
						(3,       () => Console.Write("3, ")),
						(Default, () => Console.Write("Default"))
					);
				}
				Console.WriteLine();

				Console.Write("    Without Parameter: ");
				for (int i = 1; i <= 4; i++)
				{
					// No Parameter
					Switch
					(
						(i == 1,  () => Console.Write("1, ")),
						(i == 2,  () => Console.Write("2, ")),
						(i == 3,  () => Console.Write("3, ")),
						(Default, () => Console.Write("Default"))
					);
				}
				Console.WriteLine();

				Console.Write("    Mixing Conditions & Values: ");
				for (int i = 1; i <= 4; i++)
				{
					// Parameter + Conditions
					Switch (i)
					(
						(1,          () => Console.Write("1, ")),
						(i == 2,     () => Console.Write("2, ")),
						(i % 3 == 0, () => Console.Write("3, ")),
						(Default,    () => Console.Write("Default"))
					);
				}
				Console.WriteLine();

				Pause();
			}
			#endregion

			#region Tuple GetEnumerator Extensions
			{
				Console.WriteLine("  Tuple GetEnumerator Extensions-----------------");
				Console.WriteLine();
				Console.WriteLine(@$"    ""GetEnumerator"" extensions for tuples so");
				Console.WriteLine(@$"    you can use ""foreach"" loop them.");
				Console.WriteLine();

				Console.Write("    Tuple 1 = (");
				foreach (int value in (5, 7, 9, 11))
				{
					Console.Write($" {value}");
				}
				Console.WriteLine(" )");

				Console.Write("    Tuple 2 = (");
				foreach (object value in ('a', "howdy", -10, 3.33m))
				{
					Console.Write($" {value}");
				}
				Console.WriteLine(" )");

				Console.Write("    Tuple 3 = (");
				Tuple<int, int, int> tuple3 = new(4, 6, 8);
				foreach (int value in tuple3)
				{
					Console.Write($" {value}");
				}
				Console.WriteLine(" )");

				Pause();
			}
			#endregion

			#region Permutations
			{
				Console.WriteLine("  Permutations---------------------------");
				Console.WriteLine();
				Console.WriteLine("    You can iterate all the permutations of an array with the");
				Console.WriteLine("    Permute methods: PermuteIterative and PermuteRecursive.");
				Console.WriteLine();

				int[] array = { 0, 1, 2, };
				void WriteArray() => Console.Write(string.Concat(array) + " ");

				Console.Write("    Recursive (array): ");
				PermuteRecursive<int>(array, WriteArray);
				Console.WriteLine();

				Console.Write("    Iterative (array): ");
				PermuteIterative<int>(array, WriteArray);
				Console.WriteLine();

				Console.WriteLine();
				Console.WriteLine("    This implementation may be used on any int-indexed collection.");
				Console.WriteLine("    It also supports cancellation (cut off) of the iteration. Here is");
				Console.WriteLine("    an example with a ListArray<int> that cancels after 3 permutations.");
				Console.WriteLine();

				int i = 0;
				ListArray<int> list = new() { 0, 1, 2, 3, 4 };
				void WriteList() => Console.Write(string.Concat(list) + " ");

				i = 0;
				Console.Write("    Recursive (list):  ");
				PermuteRecursive(0, list.Count - 1, WriteList, () => (++i >= 3 ? Break : Continue), i => list[i], (i, v) => list[i] = v);
				Console.WriteLine();

				i = 0;
				Console.Write("    Iterative (list):  ");
				PermuteIterative(0, list.Count - 1, WriteList, () => (++i >= 3 ? Break : Continue), i => list[i], (i, v) => list[i] = v);
				Console.WriteLine();
				Pause();
			}
			#endregion

			#region Combinations
			{
				Console.WriteLine("  Combinations---------------------------");
				Console.WriteLine();
				Console.WriteLine(@"    You can iterate all the combinations of a data set with");
				Console.WriteLine(@"    the ""Combinations"" method and overloads.");
				Console.WriteLine();

				static void ConsoleWrite(ReadOnlySpan<char> readOnlySpan)
				{
					for (int i = 0; i < readOnlySpan.Length; i++)
					{
						Console.Write(readOnlySpan[i]);
					}
					Console.Write(", ");
				}

				Console.WriteLine($@"    Iterate all possible length 3 strings with chars in ""AB""...");
				Console.Write($@"    Result: ");
				Combinations(3, ConsoleWrite, i => "AB".Length, (i, j) => "AB"[j]);
				Console.WriteLine();
				Console.WriteLine();

				Console.WriteLine($@"    Iterate all possible length 2 strings with chars in ""0123""...");
				Console.Write($@"    Result: ");
				Combinations(2, ConsoleWrite, i => "0123".Length, (i, j) => "0123"[j]);
				Console.WriteLine();
				Console.WriteLine();

				Console.WriteLine($@"    Iterate all possible length 3 strings where");
				Console.WriteLine($@"    - char 0 in ""AB""");
				Console.WriteLine($@"    - char 1 in ""12""");
				Console.WriteLine($@"    - char 2 in ""AB12""");
				Console.Write($@"    Result: ");
				Combinations(3, ConsoleWrite,
					i => i switch
					{
						0 => "AB".Length,
						1 => "12".Length,
						2 => "AB12".Length,
					},
					(i, j) => i switch
					{
						0 => "AB"[j],
						1 => "12"[j],
						2 => "AB12"[j],
					});
				Console.WriteLine();

				Pause();
			}
			#endregion

			#region Chance
			{
				Console.WriteLine("  Chance syntax----------------------");
				Console.WriteLine();
				Console.WriteLine($"    20% Chance: {20% Chance}");
				Console.WriteLine($"    50% Chance: {50% Chance}");
				Console.WriteLine($"    70% Chance: {70% Chance}");
				Pause();
			}
			#endregion

			#region Inequality
			{
				Console.WriteLine("  Inequality syntax------------------");
				Console.WriteLine();
				// valid syntax
				{
					Console.WriteLine($"    {(Inequality<float>) 1 < 2 < 3 < 4 <= 4 < 5 < 6}");
					Console.WriteLine($"    {(Inequality<float>) 6 > 5 > 4 >= 4 > 3 > 2 > 1}");
					Console.WriteLine($"    {(Inequality<float>) 3 < 2 < 1}");
					Console.WriteLine($"    {(Inequality<float>) 1 > 2 > 3}");
				}
				// invalid syntax
				{
					//// this will not compile (a good thing)
					//if ((Inequality<float>) 1)
					//{
					//
					//}

					try
					{
						Console.WriteLine($"    {(Inequality<float>) 1}");
					}
					catch (InequalitySyntaxException)
					{
						Console.WriteLine("    Inequality Syntax Error");
					}

					try
					{
						Inequality<float> a = default;
						Console.WriteLine($"    {a < 1}");
					}
					catch (InequalitySyntaxException)
					{
						Console.WriteLine("    Inequality Syntax Error");
					}
				}
				Pause();
			}
			#endregion

			#region Stepper
			{
				Console.WriteLine("  Stepper------------------------------------");
				Console.WriteLine();
				Console.WriteLine(@"    A Towel has a lot of methods called ""Stepper""");
				Console.WriteLine(@"    and ""StepperBreak"". These methods are essnetially");
				Console.WriteLine(@"    the same as an ""IEnumerable<T>"", but rather than");
				Console.WriteLine(@"    The methods returning the values, you instead pass");
				Console.WriteLine(@"    the ""step"" code that you want to run on every value.");
				Console.WriteLine(@"    There are pros and cons to both patterns. Here are some");
				Console.WriteLine(@"    examples.");
				Console.WriteLine();

				System.Collections.Generic.IEnumerable<int> iEnumerable = Ɐ(1, 2, 3);
				Console.Write("    iEnumerable values:");
				foreach (int value in iEnumerable)
				{
					Console.Write(" " + value);
				}
				Console.WriteLine();

				Action<Action<int>> stepper = Ɐ(1, 2, 3);
				Console.Write("    stepper values:");
				stepper(value => Console.Write(" " + value));
				Console.WriteLine();

				/// In order to break the traversal you can return a "StepStatus"
				/// from a "StepperBreak" method.
				Func<Func<int, StepStatus>, StepStatus> stepperBreak = Ɐ(1, 2, 3, 4, 5, 6);
				Console.Write("    stepperBreak values:");
				stepperBreak(value =>
				{
					Console.Write(" " + value);
					return value >= 3 ? Break : Continue;
				});
				Console.WriteLine();

				/// Steppers can be defined as functions without a backing data structure.
				static void stepperFunctional(Action<int> s) { s(1); s(2); s(3); }
				Console.Write("    stepperFunctional values:");
				stepperFunctional(value => Console.Write(" " + value));
				Console.WriteLine();

				Pause();
			}
			#endregion

			#region Universal Quantifier
			{
				Console.WriteLine("  Universal Quantifier---------------");
				Console.WriteLine();
				Console.WriteLine("    (debug source code for examples)");

				// Ever wish there was one syntax that unified all the data structure types
				// and interfaces? Well... try out the "universal quantifier" syntax:

				System.Collections.Generic.IEnumerable<int> a = Ɐ(1, 2, 3);
				System.Collections.Generic.IList<int>       b = Ɐ(1, 2, 3);
				int[]                                       c = Ɐ(1, 2, 3);
				System.Collections.Generic.List<int>        d = Ɐ(1, 2, 3);
				System.Collections.Generic.HashSet<int>     e = Ɐ(1, 2, 3);
				System.Collections.Generic.LinkedList<int>  f = Ɐ(1, 2, 3);
				System.Collections.Generic.Stack<int>       g = Ɐ(1, 2, 3);
				System.Collections.Generic.Queue<int>       h = Ɐ(1, 2, 3);
				System.Collections.Generic.SortedSet<int>   i = Ɐ(1, 2, 3);
				Action<Action<int>>                         j = Ɐ(1, 2, 3);
				Func<Func<int, StepStatus>, StepStatus>     l = Ɐ(1, 2, 3);
				Towel.DataStructures.Array<int>             n = Ɐ(1, 2, 3);
				Towel.DataStructures.ListArray<int>         o = Ɐ(1, 2, 3);

				Pause();
			}
			#endregion

			#region Serializing Static Delegates
			{
				Console.WriteLine("  Serializing Static Delegates----------------------");
				Console.WriteLine();
				Console.WriteLine("    This needs to be used with caution. Serializing delegates can");
				Console.WriteLine("    be unreliable, and the current versions of these methods are");
				Console.WriteLine("    dependent on assembly versions (although that may change in future).");
				Console.WriteLine();

				Console.WriteLine($"    StaticMethod(): {StaticMethod()}");
				Console.WriteLine();

				Func<string> func = StaticMethod;
				Console.WriteLine($"    Func<string> func = StaticMethod;");
				Console.WriteLine($"    func(): {func()}");
				Console.WriteLine();

				string xml = Serialization.StaticDelegateToXml(func);
				Console.WriteLine($"    Serialization.StaticDelegateToXml(func):");
				Console.WriteLine($"      {xml}");
				Console.WriteLine();

				Func<string> func_xml = Serialization.StaticDelegateFromXml<Func<string>>(xml)!;
				Console.WriteLine($"    Func<string> func_xml = Serialization.StaticDelegateFromXml<Func<string>>(xml)!;");
				Console.WriteLine($"    func_xml(): {func_xml()}");
				Console.WriteLine();

				string json = Serialization.StaticDelegateToJson(func);
				Console.WriteLine($"    Serialization.StaticDelegateToJson(json):");
				Console.WriteLine($"      {json}");
				Console.WriteLine();

				Func<string> func_json = Serialization.StaticDelegateFromJson<Func<string>>(json)!;
				Console.WriteLine($"    Func<string> func_json = Serialization.StaticDelegateFromJson<Func<string>>(json)!;");
				Console.WriteLine($"    func_json(): {func_json()}");
				Console.WriteLine();

				Pause();
			}
			#endregion

			Console.WriteLine();
			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}

		public static void Pause()
		{
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue($"    Press[{ConsoleKey.Enter}] to continue...");
			Console.WriteLine();
			Console.WriteLine();
		}

		public static string StaticMethod()
		{
			return "hello from " + nameof(StaticMethod);
		}
	}
}
