<p align="center">
	<a href="#"><img src="https://github.com/ZacharyPatten/Towel/blob/main/.github/Resources/Logo.svg?raw=true" height="150"></a>
</p>

<h1 align="center">
	Towel
</h1>

<p align="center">
	A .NET library intended to make coding a bit more towelerable: data structures, algorithms, mathematics, metadata, extensions, console, and more. :)
</p>

> "It's a tough galaxy. If you want to survive, you've gotta know... where your towel is." - Ford Prefect

<p align="center">
	<a href="https://github.com/ZacharyPatten/Towel"><img src="https://img.shields.io/badge/github-repo-%2324292e?logo=github" title="Go To Github Repo"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="https://img.shields.io/badge/language-C%23-%23178600" title="Go To C# Documentation"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2FTowel%2Fmain%2FSources%2FTowel%2FTowel.csproj&logo=.net" title="Go To .NET Download"></a>
	<a href="https://www.nuget.org/packages/Towel"><img src="https://img.shields.io/nuget/v/Towel.svg?logo=nuget" title="Go To Nuget Package" /></a>
	<a href="https://zacharypatten.github.io/Towel/index.html"><img src="https://github.com/ZacharyPatten/Towel/blob/main/.github/Resources/docfx-badge.svg?raw=true" title="Go To Docfx Documentation"></a>
	<a href="https://github.com/ZacharyPatten/Towel/actions/workflows/Continuous%20Integration.yml"><img src="https://github.com/ZacharyPatten/Towel/actions/workflows/Continuous%20Integration.yml/badge.svg" title="Go To Action"></a>
	<a href="https://zacharypatten.github.io/Towel/coveragereport/index.html"><img src="https://img.shields.io/badge/dynamic/xml?color=yellow&label=coverage&query=%2F%2Fcoverage%5B1%5D%2F%40line-rate-percentage&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2FTowel%2Fgh-pages%2Fcoveragereport%2Fcoverage.xml" title="Go Test Coverage Report" /></a>
	<a href="https://discord.gg/4XbQbwF"><img src="https://img.shields.io/discord/557244925712924684?logo=discord&logoColor=ffffff&color=7389D8" title="Go To Discord Server" /></a>
	<a href="https://github.com/ZacharyPatten/Towel/blob/main/License.md"><img src="https://img.shields.io/badge/license-MIT-green.svg" title="Go To License" /></a>
</p>

> _**Note** This project has a goal of keeping up-to-date on modern coding practices rather than maintaining backwards compatibility such as targetting the latest non-preview version of .NET and embracing favorable breaking changes ("Semantic Versioning" is not being respected at this time)._

## Getting Started

<details>
<summary>
:page_facing_up: Run The Included Examples <sub>[Expand]</sub>
</summary>
<p>

> Towel has [Examples](https://github.com/ZacharyPatten/Towel/tree/main/Examples) included in this repository.
> 
> [Download](https://github.com/ZacharyPatten/Towel/archive/main.zip) this repository and unzip the contents.
> 
> There are no custom build processes. Towel should build with any standard .NET build process, but one of the following is recommended:
> 
> <details>
> <summary>
> :page_facing_up: Visual Studio <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > 1. Install [Visual Studio](https://visualstudio.microsoft.com/) if not already installed.
> > 
> > 2. Open the <sub><a href="Towel.sln"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-16.svg" title="File"></a></sub> **`Towel.sln`** file in Visual Studio.
> > 
> > - _(optional) [Here are some settings you change in Visual Studio](https://gist.github.com/ZacharyPatten/693f35653f6c21fbe6c85444792e524b)._
>
> </p>
> </details>
>
> <details>
> <summary>
> :page_facing_up: Visual Studio Code <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > 1. Install the [.NET SDK](https://dotnet.microsoft.com/download) if not already installed.
> > 
> > 2. Install [Visual Studio Code](https://visualstudio.microsoft.com/) if not already installed.
> > 
> > 3. Open the <sub><a href="#"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> **`root folder`** of the repository in Visual Studio Code.
> > 
> > _The following files are included in the repository:_
> > - `.vscode/extensions.json` <sub>recommends Vistual Studio Code extension dependencies</sub>
> > - `.vscode/launch.json` <sub>includes the configurations for debugging the examples</sub>
> > - `.vscode/settings.json` <sub>automatically applies settings to the workspace</sub>
> > - `.vscode/tasks.json` <sub>includes the commands to build the projects</sub>
> > 
> > _Visual Studio Code Extensions (will be prompted to install these when you open the folder):_
> > - `ms-vscode.csharp` <sub>C# support</sub>
> > - `formulahendry.dotnet-test-explorer` _(optional)_ <sub>MSTest unit testing support</sub>
> > - `aisoftware.tt-processor` _(optional)_ <sub>T4 Template support</sub>
> > - `zbecknell.t4-support` _(optional)_ <sub>T4 Template syntax highlighting</sub>
>
> </p>
> </details>

</p>
</details>

<details>
<summary>
:page_facing_up: Use Towel In Your .NET Projects <sub>[Expand]</sub>
</summary>
<p>

> - Your project must target the same or newer version of .NET as Towel. [See this documentation on how to check the current target of your project](https://docs.microsoft.com/en-us/dotnet/standard/frameworks). Towel targets the following version of .NET: <sub><a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2FTowel%2Fmain%2FSources%2FTowel%2FTowel.csproj&logo=.net" title="Go To .NET Download"></a></sub>
>
> - Towel has a nuget package: <sub><a href="https://www.nuget.org/packages/Towel"><img src="https://img.shields.io/nuget/v/Towel.svg?logo=nuget" title="Go To Nuget Package"/></a></sub><br/>Instructions on how to reference the package are included on nuget.org _(click the badge)_.
>
> - If you use Towel and would be willing to show it, here is a badge you can copy-paste into your readme: <sub><a href="#"><img src="https://github.com/ZacharyPatten/Towel/blob/main/.github/Resources/UsingTowel.svg?raw=true" title="Go To Towel"></a></sub>
>   ```html
>   <a href="https://github.com/ZacharyPatten/Towel"><img src="https://github.com/ZacharyPatten/Towel/blob/main/.github/Resources/UsingTowel.svg?raw=true" title="Go To Towel"></a>
>   ```
>
> - Share your work. If you use Towel in one of your projects we want to hear about it. :)

</p>
</details>

<details>
<summary>
:page_facing_up: View Documentation <sub>[Expand]</sub>
</summary>
<p>

> - Change Log: https://github.com/ZacharyPatten/Towel/releases
> - [docfx](https://github.com/dotnet/docfx) generated API documentation reference: <sub><a href="https://zacharypatten.github.io/Towel/index.html"><img src="https://github.com/ZacharyPatten/Towel/blob/main/.github/Resources/docfx-badge.svg?raw=true" title="Go To Docfx Documentation"></a></sub>
> - [Coverlet](https://github.com/coverlet-coverage/coverlet) + [ReportGenerator](https://github.com/danielpalme/ReportGenerator) generated test coverage report: <sub><a href="https://zacharypatten.github.io/Towel/coveragereport/index.html"><img src="https://img.shields.io/badge/dynamic/xml?color=yellow&label=coverage&query=%2F%2Fcoverage%5B1%5D%2F%40line-rate-percentage&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2FTowel%2Fgh-pages%2Fcoveragereport%2Fcoverage.xml" title="Go Test Coverage Report"/></a></sub>
> - [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) generated benchmarking: https://zacharypatten.github.io/Towel/articles/benchmarks.html
> 
> Relevant Articles:
>
> - [MSDN Accessing XML Documentation Via Reflection](https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/october/csharp-accessing-xml-documentation-via-reflection)</br>
> - [Beginner's Guide To Console Input In C#](https://gist.github.com/ZacharyPatten/798ed612d692a560bdd529367b6a7dbd)</br>
> - [Generating Unique Random Data](https://gist.github.com/ZacharyPatten/c9b43a2c9e8a5a5523883e77410f742d)</br>
> - [Random Generation (with efficient exclusions)](https://gist.github.com/ZacharyPatten/8de188b2bd358ab5c3517cbb55e83632)</br>
> - [Omnitree](https://gist.github.com/ZacharyPatten/f21fc5c6835faea9be8ae4baab4e294e)</br>
> - [C# Generic Math](https://gist.github.com/ZacharyPatten/8e1395a94928f2c7715cf939b0d0389c)</br>
>
> File Structure Overview _(except for <sub><a href="https://github.com/ZacharyPatten/Towel/tree/gh-pages"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/git-branch-16.svg" title="Branch"></a></sub> `gh-pages`)_:
>
> - <sub><a href=".github"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `.github` <sub>content regarding the GitHub repoistory.</sub>
>   - <sub><a href=".github/ISSUE_TEMPLATE"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `ISSUE_TEMPLATE` <sub>templates for issue submissions to the GitHub repository</sub>
>   - <sub><a href=".github/Resources"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Resources` <sub>resources such as image files</sub>
>   - <sub><a href=".github/workflows"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `workflows` <sub>[GitHub Actions](https://github.com/ZacharyPatten/Towel/actions) workflows</sub>
>     - <sub><a href=".github/workflows/Continuous Integration.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/play-16.svg" title="Action"></a></sub> `Continuous Integration.yml` <sub>workflow for checking that code compiles and unit tests pass</sub>
>     - <sub><a href=".github/workflows/Towel Deployment.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/play-16.svg" title="Action"></a></sub> `Towel Deployment.yml` <sub>workflow to manage [releases](https://github.com/ZacharyPatten/Towel/releases) and deploy [nuget packages](https://www.nuget.org/packages/Towel)</sub>
>     - <sub><a href=".github/workflows/Documentation.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/play-16.svg" title="Action"></a></sub> `Documentation.yml` <sub>workflow that runs [docfx](https://github.com/dotnet/docfx) + [Coverlet](https://github.com/coverlet-coverage/coverlet) + [ReportGenerator](https://github.com/danielpalme/ReportGenerator) to output [GitHub Pages](https://pages.github.com/) to <sub><a href="https://github.com/ZacharyPatten/Towel/tree/gh-pages"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/git-branch-16.svg" title="Branch"></a></sub> `gh-pages`</sub>
>   - <sub><a href=".github/pull_request_template.md"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-16.svg" title="File"></a></sub> `pull_request_template.md` <sub>template for when pull requests are created</sub>
> - <sub><a href=".vscode"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `.vscode` <sub>confirguration files for if the code is opened in [Visual Studio Code](https://visualstudio.microsoft.com/)</sub>
> - <sub><a href="Examples"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Examples` <sub>root folder for all the example projects</sub>
> - <sub><a href="Sources"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Sources` <sub>root folder for the source code of released nuget packages</sub>
>   - <sub><a href="Sources/Towel"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> **`Towel` <sub>the root folder for all source code in the Towel nuget package</sub>**
> - <sub><a href="Tools"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Tools` <sub>root folder for all support projects (not included in nuget packages)</sub>
>   - <sub><a href="Tools/docfx_project"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `docfx_project` <sub>root folder for [docfx](https://github.com/dotnet/docfx) project (used in <sub><a href=".github/workflows/Documentation.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/play-16.svg" title="Action"></a></sub> `Documentation.yml`)</sub>
>     - <sub><a href="Tools/docfx_project/articles"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `articles` <sub>root folder for all articless of the [docfx](https://github.com/dotnet/docfx) generated [GitHub Pages](https://pages.github.com/) website</sub>
>     - <sub><a href="Tools/docfx_project/apidoc"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `apidoc` <sub>root folder for all api overrides of the [docfx](https://github.com/dotnet/docfx) generated [GitHub Pages](https://pages.github.com/) website</sub>
>     - <sub><a href="Tools/docfx_project/docfx.json"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-16.svg" title="File"></a></sub> `docfx.json` <sub>configuration file that controls [docfx](https://github.com/dotnet/docfx)</sub>
>     - <sub><a href="Tools/docfx_project/index.md"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-16.svg" title="File"></a></sub> `index.md` <sub>home page of the [docfx](https://github.com/dotnet/docfx) generated [GitHub Pages](https://pages.github.com/) website</sub>
>     - <sub><a href="Tools/docfx_project/toc.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-16.svg" title="File"></a></sub> `toc.yml` <sub>primary navigation for the [docfx](https://github.com/dotnet/docfx) generated [GitHub Pages](https://pages.github.com/) website</sub>
>   - <sub><a href="Tools/Towel_Benchmarking"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Towel_Benchmarking` <sub>project with all the benchmarking for the Towel project</sub>
>   - <sub><a href="Tools/Towel_Generating"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Towel_Generating` <sub>project with code generation for the Towel Project</sub>
>   - <sub><a href="Tools/Towel_Testing"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/file-directory-16.svg" title="Directory"></a></sub> `Towel_Testing` <sub>project with all unit tests for the Towel project (used in <sub><a href=".github/workflows/Continuous Integration.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/play-16.svg" title="Action"></a></sub> `Continuous Integration.yml` and <sub><a href=".github/workflows/Documentation.yml"><img src="https://raw.githubusercontent.com/ZacharyPatten/Towel/main/.github/Resources/play-16.svg" title="Action"></a></sub> `Documentation.yml`)</sub>

</p>
</details>

<details>
<summary>
:page_facing_up: Get Involved <sub>[Expand]</sub>
</summary>
<p>

> - The easiest way to support Towel is to star the github repository.
>
> - If you have any questions, you can [start a new discussion](https://github.com/ZacharyPatten/Towel/discussions/new).
>
> - If you notice anything in Towel that may be improved, please [create a new issue](https://github.com/ZacharyPatten/Towel/issues/new/choose).<br/>
>   Feature requests are welcome.
>
> - You can chat with the developer(s) on discord: <sub><a href="https://discord.gg/4XbQbwF"><img src="https://img.shields.io/discord/557244925712924684?logo=discord&logoColor=ffffff&color=7389D8" title="Go To Discord Server"/></a></sub>
>
> - If you want to contribute to Towel:<br/>&nbsp;&nbsp;1. Fork this repository<br/>&nbsp;&nbsp;2. Make some changes<br/>&nbsp;&nbsp;3. Open a pull request

</p>
</details>

## Overview

<details>
<summary>
:page_facing_up: Algorithms <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // supports System.Span<T> and any (non ref struct) int-indexed type
> IsPalindrome<...>(...);
> 
> // supports System.ReadOnlySpan<T>
> IsInterleavedRecursive<...>(...);
> IsInterleavedIterative<...>(...);
> 
> IsReorderOf<...>(...); // aka "anagrams"
> 
> // supports System.Span<T> and any (non ref struct) int-indexed type
> SortShuffle<T>(...);
> SortBubble<T>(...);
> SortSelection<T>(...);
> SortInsertion<T>(...);
> SortQuick<T>(...);
> SortMerge<T>(...);
> SortHeap<T>(...);
> SortOddEven<T>(...);
> SortCocktail<T>(...);
> SortComb<T>(...);
> SortGnome<T>(...);
> SortShell<T>(...);
> SortBogo<T>(...);
> SortSlow<T>(...);
> SortCycle<T>(...);
> SortPancake<T>(...);
> SortStooge<T>(...);
> SortTim<T>(...);
> SortCounting<T>(...); // uint-based (non-comparative sort)
> SortRadix<T>(...); // uint-based (non-comparative sort)
> SortPidgeonHole<T>(...); // int-based (non-comparative sort)
> 
> // supports System.ReadOnlySpan<T> and any (non ref struct) int-indexed type
> SearchBinary<T>(...);
> 
> // supports System.ReadOnlySpan<T> and any (non ref struct) int-indexed type
> int HammingDistanceIterative<...>(...);
> int LevenshteinDistanceRecursive<...>(...);
> int LevenshteinDistanceIterative<...>(...);
> 
> // Permutations of sequences
> // supports System.Span<T> and any (non ref struct) int-indexed type
> void PermuteRecursive<...>(...);
> void PermuteIterative<...>(...);
> 
> // Combinations of sequences
> void Combinations<...>(...);
> 
> // Path Finding (Graph Search)
> // overloads for A*, Dijkstra, and Breadth-First-Search algorithms
> SearchGraph<...>(...);
> 
> // Combines ranges without gaps between them
> IEnumerable<(T A, T B)> CombineRanges<T>(IEnumerable<(T A, T B)> ranges)
> ```
> 
> > [Sorting Algorithm Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/SortBenchmarks.html)<br/>
> > [Permute Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/PermuteBenchmarks.html)

</p>
</details>

<details>
<summary>
:page_facing_up: Extensions <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // System.Random extensions to generate more random types
> // there are overloads to specify possible ranges
> string NextString(this Random random, int length);
> char NextChar(this Random random);
> decimal NextDecimal(this Random random);
> DateTime DateTime(this Random random);
> TimeSpan TimeSpan(this Random random);
> long NextLong(this Random random);
> int[] Next(this Random random, int count, int minValue, int maxValue, Span<T> excluded); // with exclusions
> int[] NextUnique(this Random random, int count, int minValue, int maxValue); // unique values
> int[] NextUnique(this Random random, int count, int minValue, int maxValue, Span<T> excluded); // unique values with exclusions
> T Next<T>(this Random random, IEnumerable<(T Value, double Weight)> pool); // weighted values
> void Shuffle<T>(this Random random, T[] array); // randomize arrays
> 
> // Type conversion to string definition as appears in C# source code
> string ConvertToCSharpSourceDefinition(this Type type);
> // Example: typeof(List<int>) -> "System.Collections.Generic.List<int>"
> 
> string ToEnglishWords(this decimal @decimal);
> // Example: 42 -> "Forty-Two"
> (bool Success, decimal Value) TryParseEnglishWordsToDecimal(string words);
> // Example: "Forty-Two" -> 42
> 
> int TryParseRomanNumeral(string @string);
> // Example: "XLII" -> 42
> int TryToRomanNumeral(int value);
> // Example: 42 -> "XLII"
> 
> // Reflection Extensions To Access XML Documentation
> string GetDocumentation(this Type type);
> string GetDocumentation(this FieldInfo fieldInfo);
> string GetDocumentation(this PropertyInfo propertyInfo);
> string GetDocumentation(this EventInfo eventInfo);
> string GetDocumentation(this ConstructorInfo constructorInfo);
> string GetDocumentation(this MethodInfo methodInfo);
> string GetDocumentation(this MemberInfo memberInfo);
> string GetDocumentation(this ParameterInfo parameterInfo);
> ```
> 
> > [Weighted Random Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/WeightedRandomBenchmarks.html)<br/>
> > [Random With Exclusions Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/RandomWithExclusionsBenchmarks.html)<br/>
> > [decimal To English Words Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/ToEnglishWordsBenchmarks.html)

</p>
</details>

<details>
<summary>
:page_facing_up: Data Structures <sub>[Expand]</sub>
</summary>
<p>

> <details>
> <summary>
> :page_facing_up: Heap <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > // A heap is a binary tree that is sorted vertically using comparison methods. This is different
> > // from AVL Trees or Red-Black Trees that keep their contents stored horizontally. The rule
> > // of a heap is that no parent can be less than either of its children. A Heap using "sifting up"
> > // and "sifting down" algorithms to move values vertically through the tree to keep items sorted.
> > 
> > IHeap<T> heap = HeapArray.New<T>();
> > 
> > // Visualization:
> > //
> > //    Binary Tree
> > //
> > //                      -7
> > //                      / \
> > //                     /   \
> > //                    /     \
> > //                   /       \
> > //                  /         \
> > //                 /           \
> > //                /             \
> > //               /               \
> > //             -4                 1
> > //             / \               / \     
> > //            /   \             /   \    
> > //           /     \           /     \   
> > //         -1       3         6       4
> > //         / \     / \       / \     / \ 
> > //        30  10  17  51    45  22  19  7
> > //
> > //    Flattened into an Array
> > //
> > //        Root = 1
> > //        Left Child = 2 * Index
> > //        Right Child = 2* Index + 1
> > //         __________________________________________________________________________
> > //        |0  |-7 |-4 |1  |-1 |3  |6  |4  |30 |10 |17 |51 |45 |22 |19 |7  |0  |0  |0  ...
> > //         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
> > //         0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18
> > ```
> 
> </p>
> </details>
> 
> <details>
> <summary>
> :page_facing_up: AVL Tree <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > // An AVL tree is a binary tree that is sorted using comparison methods and automatically balances
> > // itself by tracking the heights of nodes and performing one of four specific algorithms: rotate
> > // right, rotate left, double rotate right, or double rotate left. Any parent in an AVL Tree must
> > // be greater than its left child but less than its right child (if the children exist). An AVL
> > // tree is sorted in the same manor as a Red-Black Tree, but uses different algorithms to maintain
> > // the balance of the tree.
> > 
> > IAvlTree<T> avlTree = AvlTreeLinked.New<T>();
> > 
> > // Visualization:
> > //
> > //    Binary Tree
> > //
> > //        Depth 0 ------------------>    7
> > //                                      / \
> > //                                     /   \
> > //                                    /     \
> > //                                   /       \
> > //                                  /         \
> > //                                 /           \
> > //                                /             \
> > //                               /               \
> > //        Depth 1 --------->    1                 22
> > //                             / \               / \
> > //                            /   \             /   \
> > //                           /     \           /     \
> > //        Depth 2 ---->    -4       4         17      45
> > //                         / \     / \       / \     / \
> > //        Depth 3 --->   -7  -1   3   6     10  19  30  51
> > //
> > //    Flattened into an Array
> > //
> > //        Root = 1
> > //        Left Child = 2 * Index
> > //        Right Child = 2* Index + 1
> > //         __________________________________________________________________________
> > //        |0  |7  |1  |22 |-4 |4  |17 |45 |-7 |-1 |3  |6  |10 |19 |30 |51 |0  |0  |0  ...
> > //         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
> > //         0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18
> > ```
> 
> </p>
> </details>
> 
> <details>
> <summary>
> :page_facing_up: Red Black Tree <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > // A Red-Black treeis a binary tree that is sorted using comparison methods and automatically 
> > // balances itself. Any parent in an Red-Black Tree must be greater than its left child but less
> > // than its right child (if the children exist). A Red-Black tree is sorted in the same manor as
> > // an AVL Tree, but uses different algorithms to maintain the balance of the tree.
> > 
> > IRedBlackTree<T> redBlackTree = RedBlackTreeLinked.New<T>();
> > 
> > // Visualization:
> > //
> > //    Binary Tree
> > //
> > //        Color Black ---------------->    7
> > //                                        / \
> > //                                       /   \
> > //                                      /     \
> > //                                     /       \
> > //                                    /         \
> > //                                   /           \
> > //                                  /             \
> > //                                 /               \
> > //        Color Red --------->    1                 22
> > //                               / \               / \
> > //                              /   \             /   \
> > //                             /     \           /     \
> > //        Color Black --->   -4       4         17      45
> > //                           / \     / \       / \     / \
> > //        Color Red --->   -7  -1   3   6     10  19  30  51
> > //
> > //    Flattened into an Array
> > //
> > //        Root = 1
> > //        Left Child = 2 * Index
> > //        Right Child = 2* Index + 1
> > //         __________________________________________________________________________
> > //        |0  |7  |1  |22 |-4 |4  |17 |45 |-7 |-1 |3  |6  |10 |19 |30 |51 |0  |0  |0  ...
> > //         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
> > //         0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18
> > ```
> 
> </p>
> </details>
> 
> <details>
> <summary>
> :page_facing_up: Omnitree <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > // An Omnitree is a Spacial Partitioning Tree (SPT) that works on an arbitrary number of dimensions.
> > // It stores items sorted along multiple dimensions by dividing spaces into sub-spaces. A 3D
> > // version of an SPT is often called an "Octree" and a 2D version of an SPT is often called a
> > // "Quadtree." There are two versions of the Omnitree: Points and Bounds. The Points version stores
> > // vectors while the Bounds version stores spaces with a minimum and maximum vector.
> > 
> > IOmnitreePoints<T, A1, A2, A3...> omnitreePoints =
> >     new OmnitreePointsLinked<T, A1, A2, A3...>(
> >         (T value, out A1 a1, out A2 a2, out A3 a3...) => { ... });
> >         
> > IOmnitreeBounds<T, A1, A2, A3...> omnitreeBounds =
> >     new OmnitreeBoundsLinked<T, A1, A2, A3...>(
> >         (T value,
> >         out A1 min1, out A1 max1,
> >         out A2 min2, out A2 max2,
> >         out A3 min3, out A3 max3...) => { ... });
> > 
> > // The maximum number of children any node can have is 2 ^ N where N is the number
> > // of dimensions of the tree.
> > //
> > //    -------------------------------
> > //    | Dimensions | Max # Children |
> > //    |============|================|
> > //    |     1      |   2 ^ 1 = 2    |
> > //    |     2      |   2 ^ 2 = 4    |
> > //    |     3      |   2 ^ 3 = 8    |
> > //    |     4      |   2 ^ 4 = 16   |
> > //    |    ...     |      ...       |
> > //    -------------------------------
> > //
> > // Visualizations
> > //
> > // 1 Dimensional:
> > //
> > //  -1D |-----------|-----------| +1D        Children Indexes:
> > //                                           -1D: 0
> > //       <--- 0 ---> <--- 1 --->             +1D: 1
> > //
> > // 2 Dimensional:
> > //       _____________________
> > //      |          |          |  +2D
> > //      |          |          |   ^
> > //      |     2    |     3    |   |        Children Indexes:
> > //      |          |          |   |        -2D -1D: 0
> > //      |----------|----------|   |        -2D +1D: 1
> > //      |          |          |   |        +2D -1D: 2
> > //      |          |          |   |        +2D +1D: 3
> > //      |     0    |     1    |   |
> > //      |          |          |   v
> > //      |__________|__________|  -2D
> > //
> > //       -1D <-----------> +1D 
> > //
> > // 3 Dimensional:
> > //
> > //            +3D     _____________________
> > //           7       /         /          /|
> > //          /       /    6    /     7    / |
> > //         /       /---------/----------/  |                     Children Indexes:
> > //        /       /    2    /     3    /|  |                     -3D -2D -1D: 0
> > //       L       /_________/__________/ |  |                     -3D -2D +1D: 1
> > //    -3D       |          |          | | /|          +2D        -3D +2D -1D: 2
> > //              |          |          | |/ |           ^         -3D +2D +1D: 3
> > //              |     2    |     3    | /  |           |         +3D -2D -1D: 4
> > //              |          |          |/|  | <-- 5     |         +3D -2D +1D: 5
> > //              |----------|----------| |  |           |         +3D +2D -1D: 6
> > //              |          |          | |  /           |         +3D +2D +1D: 7
> > //              |          |          | | /            |
> > //              |     0    |     1    | |/             |
> > //              |          |          | /              v
> > //              |__________|__________|/              -2D
> > //             
> > //                   ^
> > //                   |
> > //                   4 (behind 0)
> > //
> > //               -1D <-----------> +1D
> > //
> > // 4 Dimensional:
> > //
> > //     +1D         +2D         +3D         +4D       Children Indexes:
> > //      ^           ^           ^           ^
> > //      |           |           |           |        -4D -3D -2D -1D: 0   +4D -3D -2D -1D: 8
> > //      |           |           |           |        -4D -3D -2D +1D: 1   +4D -3D -2D +1D: 9
> > //      |           |           |           |        -4D -3D +2D -1D: 2   +4D -3D +2D -1D: 10
> > //      |           |           |           |        -4D -3D +2D +1D: 3   +4D -3D +2D +1D: 11
> > //      |           |           |           |        -4D +3D -2D -1D: 4   +4D +3D -2D -1D: 12
> > //     ---         ---         ---         ---       -4D +3D -2D +1D: 5   +4D +3D -2D +1D: 13
> > //      |           |           |           |        -4D +3D +2D -1D: 6   +4D +3D +2D -1D: 14
> > //      |           |           |           |        -4D +3D +2D +1D: 7   +4D +3D +2D +1D: 15
> > //      |           |           |           |
> > //      |           |           |           |
> > //      |           |           |           |
> > //      v           v           v           v
> > //     -1D         -2D         -3D         -4D
> > //
> > //     With a value that is in the (+1D, -2D, -3D, +4D)[Index 9] child:
> > //
> > //     +1D         +2D         +3D         +4D
> > //      ^           ^           ^           ^
> > //      |           |           |           |
> > //      |           |           |           |
> > //      O---        |           |        ---O
> > //      |   \       |           |       /   |
> > //      |    \      |           |      /    |
> > //     ---    \    ---         ---    /    ---
> > //      |      \    |           |    /      |
> > //      |       \   |           |   /       |
> > //      |        ---O-----------O---        |
> > //      |           |           |           |
> > //      |           |           |           |
> > //      v           v           v           v
> > //     -1D         -2D         -3D         -4D
> > 
> > // By default, the omnitree will sort items along each axis and use the median algorithm to determine
> > // the point of divisions. However, you can override the subdivision algorithm. For numerical values,
> > // the mean algorithm can be used (and is much faster than median). If you know the data set will be
> > // relatively evenly distributed within a sub-space, you can even set the subdivision algorithm to
> > // calculate the subdivision from parent spaces rather than looking at the current contents of the
> > // space.
> > 
> > // The depth of the omnitree is bounded by "ln(count)" the natural log of the current count. When adding
> > // and item to the tree, if the number of items in the respective child is greater than ln(count) and 
> > // the depth bounding has not been reached, then the child will be subdivided. The goal is to achieve 
> > // Ω(ln(count)) runtime complexity when looking up values.
> > ```
> 
> </p>
> </details>
> 
> <details>
> <summary>
> :page_facing_up: Tree <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > ITree<T> treeMap = TreeMap.New<T>(...);
> > ```
> 
> </p>
> </details>
> 
> <details>
> <summary>
> :page_facing_up: Graph <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > // A graph is a data structure that contains nodes and edges. They are useful
> > // when you need to model real world scenarios. They also are generally used
> > // for particular algorithms such as path finding. The GraphSetOmnitree is a
> > // graph that stores nodes in a hashed set and the edges in a 2D omnitree (aka
> > // quadtree).
> > 
> > IGraph<int> graph = GraphSetOmnitree.New<int>();
> > // add nodes
> > graph.Add(0);
> > graph.Add(1);
> > graph.Add(2);
> > graph.Add(3);
> > // add edges
> > graph.Add(0, 1);
> > graph.Add(1, 2);
> > graph.Add(2, 3);
> > graph.Add(0, 3);
> > // visualization
> > //
> > //     0 --------> 1
> > //     |           |
> > //     |           |
> > //     |           |
> > //     v           v
> > //     3 <-------- 2
> > ```
> 
> </p>
> </details>
> 
> <details>
> <summary>
> :page_facing_up: Trie <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > // A trie is a tree that stores values in a way that partial keys may be shared
> > // amongst values to reduce redundant memory usage. They are generally used with
> > // large data sets such as storing all the words in the English language. For
> > // example, the words "farm" and "fart" both have the letters "far" in common.
> > // A trie takes advantage of that and only stores the necessary letters for
> > // those words ['f'->'a'->'r'->('t'||'m')]. A trie is not limited to string
> > // values though. Any key type that can be broken into pieces (and shared),
> > // could be used in a trie.
> > //
> > // There are two versions. One that only stores the values of the trie (ITrie<T>)
> > // and one that stores the values of the trie plus an additional generic value
> > // on the leaves (ITrie<T, D>).
> >
> > ITrie<T> trie = TrieLinkedHashLinked.New<T>();
> >
> > ITrie<T, D> trie = TrieLinkedHashLinked.New<T, D>();
> > ```
> 
> </p>
> </details>

</p>
</details>

<details>
<summary>
:page_facing_up: Generic Mathematics & Logic <sub>[Expand]</sub>
</summary>
<p>

> <details>
> <summary>
> :page_facing_up: How It Works <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > ```cs
> > public static T Addition<T>(T a, T b)
> > {
> > 	return AdditionImplementation<T>.Function(a, b);
> > }
> > 
> > internal static class AdditionImplementation<T>
> > {
> > 	internal static Func<T, T, T> Function = (T a, T b) =>
> > 	{
> > 		var A = Expression.Parameter(typeof(T));
> > 		var B = Expression.Parameter(typeof(T));
> > 		var BODY = Expression.Add(A, B);
> > 		Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
> > 		return Function(a, b);
> > 	};
> > }
> > ```
> > 
> > You can break type safe-ness using generic types and runtime compilation, and you can store the runtime compilation in a delegate so the only overhead is the invocation of the delegate.
> 
> </p>
> </details>
> 
> ```cs
> // Logic Fundamentals
> bool Equate<T>(T a , T b);
> bool LessThan<T>(T a, T b);
> bool GreaterThan<T>(T a, T b);
> CompareResult Compare<T>(T a, T b);
> 
> // Mathematics Fundamentals
> T Negation<T>(T a);
> T Addition<T>(T a, T b);
> T Subtraction<T>(T a, T b);
> T Multiplication<T>(T a, T b);
> T Division<T>(T a, T b);
> T Remainder<T>(T a, T b);
> 
> // More Logic
> bool IsPrime<T>(T a);
> bool IsEven<T>(T a);
> bool IsOdd<T>(T a);
> T Minimum<T>(T a, T b);
> T Maximum<T>(T a, T b);
> T Clamp<T>(T value, T floor, T ceiling);
> T AbsoluteValue<T>(T a);
> bool EqualityLeniency<T>(T a, T b, T leniency);
> 
> // More Numerics
> void FactorPrimes<T>(T a, ...);
> T Factorial<T>(T a);
> T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1);
> T LeastCommonMultiple<T>(T a, T b, params T[] c);
> T GreatestCommonFactor<T>(T a, T b, params T[] c);
> LinearRegression2D<T>(..., out T slope, out T y_intercept);
> 
> // Statistics
> T Mean<T>(T a, params T[] b);
> T Median<T>(params T[] values);
> Heap<Link<T, int>> Mode<T>(T a, params T[] b);
> void Range<T>(out T minimum, out T maximum, ...);
> T[] Quantiles<T>(int quantiles, ...);
> T GeometricMean<T>(...);
> T Variance<T>(...);
> T StandardDeviation<T>(...);
> T MeanDeviation<T>(...);
> 
> // Vectors
> Vector<T> V1 = new Vector<T>(params T[] vector);
> Vector<T> V2 = new Vector<T>(params T[] vector);
> Vector<T> V3;
> T scalar;
> V3 = -V1;                   // Negate
> V3 = V1 + V2;               // Add
> V3 = V1 - V2;               // Subtract
> V3 = V1 * scalar;           // Multiply
> V3 = V1 / scalar;           // Divide
> scalar = V1.DotProduct(V2); // Dot Product
> V3 = V1.CrossProduct(V2);   // Cross Product
> V1.Magnitude;               // Magnitude
> V3 = V1.Normalize();        // Normalize
> bool equal = V1 == V2;      // Equal
> 
> // Matrices
> Matrix<T> M1 = new Matrix<T>(int rows, int columns);
> Matrix<T> M2 = new Matrix<T>(int rows, int columns);
> Matrix<T> M3;
> Vector<T> V2 = new Vector<T>(params T[] vector);
> Vector<T> V3;
> T scalar;
> M3 = -M1;                               // Negate
> M3 = M1 + M2;                           // Add
> M3 = M1 - M2;                           // Subtract
> M3 = M1 * M2;                           // Multiply
> V3 = M1 * V2;                           // Multiply (vector)
> M3 = M1 * scalar;                       // Multiply (scalar)
> M3 = M1 / scalar;                       // Divide
> M3 = M1 ^ 3;                            // Power
> scalar = M1.Determinent();              // Determinent
> M3 = M1.Minor(int row, int column);     // Minor
> M3 = M1.Echelon();                      // Echelon Form (REF)
> M3 = M1.ReducedEchelon();               // Reduced Echelon Form (RREF)
> M3 = M1.Inverse();                      // Inverse
> M1.DecomposeLowerUpper(ref M2, ref M3); // Lower Upper Decomposition
> bool equal = M1 == M2;                  // Equal
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: Symbolic Mathematics <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // Parsing From Linq Expression
> Expression<Func<double, double>> exp1 = (x) => 2 * (x / 7);
> Symbolics.Expression symExp1 = Symbolics.Parse(exp1);
> 
> // Parsing From String
> Symbolics.Expression symExp2 = Symbolics.Parse("2 * ([x] / 7)");
> 
> // Mathematical Simplification
> Symbolics.Expression simplified = symExp1.Simplify();
> 
> // Variable Substitution
> symExp1.Substitute("x", 5);
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: Measurement Mathematics <sub>[Expand]</sub>
</summary>
<p>

> <details>
> <summary>
> :page_facing_up: Supported Measurements <sub>[Expand]</sub>
> </summary>
> <p>
> 
> > Here are the currently supported measurement types:
> > 
> > ```cs
> > //    Acceleration: Length/Time/Time
> > //    AngularAcceleration: Angle/Time/Time
> > //    Angle: Angle
> > //    AngularSpeed: Angle/Time
> > //    Area: Length*Length
> > //    AreaDensity: Mass/Length/Length
> > //    Density: Mass/Length/Length/Length
> > //    ElectricCharge: ElectricCharge
> > //    ElectricCurrent: ElectricCharge/Time
> > //    Energy: Mass*Length*Length/Time/Time
> > //    Force: Mass*Length/Time/Time
> > //    Length: Length
> > //    LinearDensity: Mass/Length
> > //    LinearMass: Mass*Length
> > //    LinearMassFlow: Mass*Length/Time
> > //    Mass: Mass
> > //    MassRate: Mass/Time
> > //    Power: Mass*Length*Length/Time/Time/Time
> > //    Pressure: Mass/Length/Time/Time
> > //    Speed: Length/Time
> > //    Tempurature: Tempurature
> > //    Time: Time
> > //    TimeArea: Time*Time
> > //    Volume: Length*Length*Length
> > //    VolumeRate: Length*Length*Length/Time
> > ```
> > 
> > The measurement types are generated in the *Towel/Measurements/MeasurementTypes.tt* T4 text template file. The unit (enum) definitions are in the *Towel/Measurements/MeasurementUnitDefinitions.cs* file. Both measurment types and unit definitions can be easily added. If you think a measurement type or unit type should be added, please [submit an enhancement issue](https://github.com/ZacharyPatten/Towel/issues/new/choose).
> 
> </p>
> </details>
> 
> ```cs
> // Towel has measurement types to help write scientific code: Acceleration<T>, Angle<T>, Area<T>, 
> // Density<T>, Length<T>, Mass<T>, Speed<T>, Time<T>, Volume<T>, etc.
> 
> // Automatic Unit Conversion
> // When you perform mathematical operations on measurements, any necessary unit conversions will
> // be automatically performed by the relative measurement type (in this case "Angle<T>").
> Angle<double> angle1 = (90d, Degrees);
> Angle<double> angle2 = (.5d, Turns);
> Angle<double> result1 = angle1 + angle2; // 270° 
> 
> // Type Safeness
> // The type safe-ness of the measurement types prevents the miss-use of the measurements. You cannot
> // add "Length<T>" to "Angle<T>" because that is mathematically invalid (no operator exists).
> Length<double> length1 = (2d, Yards);
> object result2 = angle1 + length1; // WILL NOT COMPILE!!!
> 
> // Simplify The Syntax Even Further
> // You can use alias to remove the generic type if you want to simplify the syntax even further.
> using Speedf = Towel.Measurements.Speed<float>; // at top of file
> Speedf speed1 = (5, Meters / Seconds);
> 
> // Vector + Measurements
> // You can use the measurement types inside Towel Vectors.
> Vector<Speed<float>> velocity1 = new Vector<Speed<float>>(
> 	(1f, Meters / Seconds),
> 	(2f, Meters / Seconds),
> 	(3f, Meters / Seconds));
> Vector<Speedf> velocity2 = new Vector<Speedf>(
> 	(1f, Centimeters / Seconds),
> 	(2f, Centimeters / Seconds),
> 	(3f, Centimeters / Seconds));
> Vector<Speed<float>> velocity3 = velocity1 + velocity2;
> 
> // Manual Unit Conversions
> // 1. Index Operator On Measurement Type
> double angle1_inRadians = angle1[Radians];
> float speed1_inMilesPerHour = speed1[Miles / Hours];
> // 2. Static Conversion Methods
> double angle3 = Angle<double>.Convert(7d,
> 	Radians,  // from
> 	Degrees); // to
> double speed2 = Speed<double>.Convert(8d,
> 	Meters / Seconds, // from
> 	Miles / Hours);   // to
> double force1 = Force<double>.Convert(9d,
> 	Kilograms * Meters / Seconds / Seconds, // from
> 	Grams * Miles / Hours / Hours);         // to
> double angle4 = Measurement.Convert(10d,
> 	Radians,  // from
> 	Degrees); // to
> // The unit conversion on the Measurement class
> // is still compile-time-safe.
> 
> // Measurement Parsing
> Speed<float>.TryParse("20.5 Meters / Seconds",
> 	out Speed<float> parsedSpeed);
> Force<decimal>.TryParse(".1234 Kilograms * Meters / Seconds / Seconds",
> 	out Force<decimal> parsedForce);
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: Console Helpers <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // Just some helper methods for console applications...
> 
> // wait for keypress to continue an intercept input
> ConsoleHelper.PromptPressToContinue(...);
> // generic method for retrieving validated console input
> ConsoleHelper.GetInput<T>(...);
> // animated ellipsis character to show processing
> ConsoleHelper.AnimatedEllipsis(...);
> // render progress bar in console
> ConsoleHelper.ProgressBar(...);
> // Console.ReadLine() with hidden input characters
> ConsoleHelper.HiddenReadLine();
> // easily manage int-based console menus
> ConsoleHelper.IntMenu(...);
> // preventing console input
> ConsoleHelper.FlushInputBuffer();
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: TagAttribute <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // With TagAttribute's you can make value-based attributes so
> // you don't always have to make your own custom attribute types.
> // Just "tag" a code member with constant values.
> 
> using System;
> using Towel;
> 
> var (Found, Value) = typeof(MyClass).GetTag("My Tag");
> Console.WriteLine("My Tag...");
> Console.WriteLine("Found: " + Found);
> Console.WriteLine("Value: " + Value);
> 
> [Tag("My Tag", "hello world")]
> public class MyClass { }
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: SLazy&lt;T&gt; + ValueLazy&lt;T&gt; <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // SLazy<T> is a faster Lazy<T> when using the default
> // LazyThreadSafetyMode.ExecutionAndPublication setting.
> 
> SLazy<string> slazy = new(() => "hello world");
> Console.WriteLine(slazy.IsValueCreated); // False
> Console.WriteLine(slazy.Value);          // hello world
> Console.WriteLine(slazy.IsValueCreated); // True
> 
> // ValueLazy<T> is even faster than SLazy<T> but it 
> // is unsafe as it will potentially call the factory
> // delegate multiple times if the struct is copied.
> // So please use ValueLazy<T> with caution.
>
> // There are various types for supporting no multithread lock,
> // no exception caching, and publication only locks.
> ```
> 
> > [Initialization Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/LazyInitializationBenchmarks.html)<br/>
> > [Caching Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/LazyCachingBenchmarks.html)<br/>
> > [Construction Benchmarks](https://zacharypatten.github.io/Towel/benchmarks/LazyConstructionBenchmarks.html)

</p>
</details>

<details>
<summary>
:page_facing_up: SpanBuilder&lt;T&gt; + SStringBuilder <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // SpanBuilder<char> is a small helper for initializing
> // stack allocated spans.
> SpanBuilder<char> span = stackalloc char[10];
> span.AppendLine("ab");
>
> // SStringBuilder is a small helper for initializing strings.
> // It will append to the span until the capacity is reached
> // and then it will revert to a StringBuilder if necessary
> // rather than throwing like SpanBuilder<T> does.
> SStringBuilder<char> span = stackalloc char[10];
> span.AppendLine("abcdefghijklmnopqrztuvwxyz");
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: Command Line Parser <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // Just put the [Command] attribute on any method you want to call
> // from the command line, and call the "HandleArguments" method.
>
> using System;
> using static Towel.CommandLine;
>
> public static class Program
> {
> 	public static void Main(string[] args)
> 	{
> 		HandleArguments(args);
> 	}
>
> 	[Command]
> 	public static void A(int a)
> 	{
> 		Console.WriteLine(nameof(A) + " called");
> 		Console.WriteLine(nameof(a) + ": " + a);
> 	}
> }
>
> // output:
> // dotnet run A --a 7
> // A called
> // a: 7
> ```

</p>
</details>

<details>
<summary>
:page_facing_up: XML docs via Reflection <sub>[Expand]</sub>
</summary>
<p>

> ```cs
> // Get the XML docs on code members via reflection.
>
> using System;
> using Towel;
>
> string xmlDocs = typeof(MyClass).GetDocumentation();
> Console.WriteLine(xmlDocs.Trim());
>
> /// <summary>hello world</summary>
> public class MyClass { }
>
> // Write "<summary>hello world</summary>" to standard output.
> ```

</p>
</details>
