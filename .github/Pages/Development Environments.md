# Development Environments

If you are new to C# development, here are some notes on recommended development environments
to help get you started with the Towel project.

## Visual Studio 2019

[Visual Studio 2019](https://visualstudio.microsoft.com/) is the recommended development environment.
The *Community* edition is free. Just open the "Towel.sln" file in Visual Studio 2019 and the code should
build without any additional steps.

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

[Visual Studio Code](https://visualstudio.microsoft.com/) is _not_ the recommended development environment,
but it you can use it as well. Visual Studio Code is a free program. After installing Visual Studio Code, you
will need to add the following extensions to use it for the Towel project:

- **ms-vscode.csharp**
	- _Reason: support for C#_
- **aisoftware.tt-processor**
	- _Reason: support for T4 Templates_
	- _Optional: this is only necessary if you plan on editing the ".tt" generator files_
- **formulahendry.dotnet-test-explorer**
	- _Reason: support for MSTest unit testing_
	- _Optional: this is only necessary if you plan on running the unit tests_

Once the extensions are installed, open the root folder of the repository.

_Note: The Collision Detection 3D example is a .NET Framework project (not .NET Core) and therefore is not
supported for debugging in Visual Studio Code. You will have to run that example without debugging. All the
other examples are .NET Core and fully compatible for debugging in Visual Studio Code._

<details>
<summary><strong>Optional Settings</strong></summary>
<p>

These are some notes about settings I like to use when I code in Visual Studio Code. They are completely optional.
All settings can be modified in the ".vscode/settings.json" file.

- "editor.insertSpaces": false,
- "editor.renderWhitespace": "all",

</p>
</details>
