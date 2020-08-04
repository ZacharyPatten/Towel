# Contributing to Towel

#### Open An Issue

If you notice anything in Towel that may be improved, please [create a new issue](https://github.com/ZacharyPatten/Towel/issues/new/choose) on the GitHub repository. This could be a bug fix, an enhancement, or anything else regarding the project. Creating an issue is just a public post that anyone can see and comment on.

#### Reference Your Content

If you create any content relavent to Towel (blog posts, articles, tutorials, videos, a project that uses Towel, etc.), message the [project developer(s)](https://github.com/ZacharyPatten/Towel#developers) and a link to your content may be added.

#### Development

If you want to help out with the development of the code, it will involve the standard GitHub processes of forking the code and creating a pull request. Any issues marked as [Good First Issue](https://github.com/ZacharyPatten/Towel/contribute)s will generally be the best starting points for helping out. You can chat with the [project developer(s)](https://github.com/ZacharyPatten/Towel#developers) on [Discord](https://discord.gg/4XbQbwF) for more information. See the [Development Environments](https://github.com/ZacharyPatten/Towel/blob/master/.github/Pages/Development%20Environments.md) page for information on the recommended development environemnts.

# Recommended Development Environments

## Visual Studio 2019

[Visual Studio 2019](https://visualstudio.microsoft.com/) is the recommended development environment. The *Community* edition is free. Just open the "Towel.sln" file in Visual Studio 2019 and the code should build without any additional steps.

<details>
<summary><strong>Optional Settings</strong></summary>
<p>

These are some notes about settings I like to use when I code in Visual Studio. They are completely optional.

#### Dark Theme
`Tools -> Options -> Environment -> General`

#### Control Click
`Tools -> Options -> Text Editor -> General`

Enable mouse click to perform Go to Definition = false

#### Shift Key Overrides
`Tools -> Options -> Environment -> Keyboard`

Window.CloseToolWindow = *Remove*

#### Show White Space Characters
`Tools -> Options -> Text Editor -> General`

View whitespace = true

#### Tabs
`Tools -> Options -> Text Editor -> All Languages -> Tabs`

Tab size  = 4

Keep tabs = true

#### Fonts and Colors
| Setting | Value |
| :---    | :--- |
| User Members - Constants         | RBG(220, 220, 220) |
| User Members - Enum Members      | RBG(220, 220, 220) |
| User Members - Constants         | RBG(220, 220, 220) |
| User Members - Events            | RBG(220, 220, 220) |
| User Members - Extension Methods | RBG(203, 133, 155) |
| User Members - Fields            | RBG(220, 220, 220) |
| User Members - Labels            | RBG(220, 220, 220) |
| User Members - Locals            | RBG(156, 220, 254) |
| User Members - Methods           | RBG(189,  99, 128) |
| User Members - Namespaces        | RBG(220, 220, 220) |
| User Members - Parameters        | RBG(156, 220, 254) |
| User Members - Properties        | RBG(220, 220, 220) |
| User Types - Classes             | RBG( 78, 201, 176) |
| User Types - Delegates           | RBG(189,  99, 197) |
| User Types - Enums               | RBG(255, 127,  39) |
| User Types - Interfaces          | RBG(184, 215, 163) |
| User Types - Structures          | RBG(255, 255, 128) |
| User Types - Type Parameters     | RBG(128, 128,   0) |

</p>
</details>

## Visual Studio Code

[Visual Studio Code](https://visualstudio.microsoft.com/) is _not_ the recommended development environment, but it you can use it as well. Visual Studio Code is a free program. After installing Visual Studio Code, you will need to add the following extensions to use it for the Towel project:

- **ms-vscode.csharp**
	- _Reason: support for C#_
- **aisoftware.tt-processor**
	- _Reason: support for T4 Templates_
	- _Optional: this is only necessary if you plan on editing the ".tt" generator files_
- **zbecknell.t4-support**
	- _Reason: support for syntax highlighting in T4 templates_
	- _Optional: this is not necessary, but it makes T4 templates easier to read_
- **formulahendry.dotnet-test-explorer**
	- _Reason: support for MSTest unit testing_
	- _Optional: this is only necessary if you plan on running the unit tests_

Open the root folder of the Towel repository in Visual Studio Code.

_Note: The Collision Detection 3D example is a .NET Framework project (not .NET Core) and therefore is not supported for debugging in Visual Studio Code. You will have to run that example without debugging. All the other examples are .NET Core and fully compatible for debugging in Visual Studio Code._

_Note: The Towel repository includes a `.vscode/settings.json` file that will automatically apply recommended settings. You can overwrite these settings._

# Code Standards

This is just some syntax preference notes for Towel. They are not strict rules and will not be enforced at this time. Just follow them if it makes sense. :)

### Runtime Algorithmic Complexity

One of the goals of Towel is to have members documented with algoritmic complexity using the `runtime` XML tag. Here is the notation in use:

- O(...): upper bound
- Ω(...): lower bound
- Θ(...): upper and lower bound
- ε(...): expected average

Examples:
- `/// <runtime>O(1)</runtime>` the member has constant time
- `/// <runtime>O(n), Ω(1), ε(1)</runtime>` the member may have to iterate n items, but it will generally run in constant time
- `/// <runtime>Θ(n)</runtime>` the member iterates n items
- `/// <runtime>Θ(n^2)</runtime>` the member iterates n items and has a nested loop

### Tabs

Please use tabs rather than spaces.

### Fields First

When declaring types, all fields (instance + static + const) should come before other members and be outside any "#region" or other code groupings. This makes it easy for people to understand the structure and purpose of a type.

### Internal Over Private

In general, prefer `internal` over `private`. If people want to access `internal` members for whatever reason, they can just download the code and add an `InternalsVisibleTo` attribute to the code so they can access internal members.

### Expression Bodied Members Are Prefered

In general, prefer expression body definition syntax `member => expression;` over traditional C# syntax `member { expression; }` where applicable.
