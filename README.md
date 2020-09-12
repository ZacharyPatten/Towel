<p align="center">
	<a href="#"><img src="https://github.com/ZacharyPatten/Towel/blob/master/.github/Resources/Logo.svg?raw=true" height="150"></a>
</p>

<h1 align="center">
	Towel
</h1>

<p align="center">
	A .NET library intended to add core functionality and make advanced topics as clean and simple as possible: data structures, algorithms, mathematics, metadata, extensions, console, and more. :)
</p>

> "It's a tough galaxy. If you want to survive, you've gotta know... where your towel is." - Ford Prefect

<p align="center">
	<a href="https://github.com/ZacharyPatten/Towel" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://dotnet.microsoft.com/download" alt="Target"><img alt="flat" src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2FTowel%2Fmaster%2FSources%2FTowel%2FTowel.csproj"></a>
	<a href="https://github.com/ZacharyPatten/Towel/actions?query=workflow%3A%22Towel+Build%22" alt="docfx documentation"><img src="https://github.com/ZacharyPatten/Towel/workflows/Towel%20Build/badge.svg" title="Go To Towel Build" alt="Towel Build"></a>
	<a href="https://www.nuget.org/packages/Towel" alt="nuget package"><img src="https://img.shields.io/nuget/v/Towel.svg" /></a>
	<a href="https://zacharypatten.github.io/Towel/api/index.html"><img src="https://gist.githubusercontent.com/ZacharyPatten/ce53ded67eef6f9563376172dbc0382c/raw/8df6b22faa4255c3f84826639bb3b2d58ebee72a/docfx_badge.svg" title="Go To Towel docfx Documentation" alt="Towel docfx Documentation"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/Towel/blob/master/License.md" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

> _**Note**: This project has a goal of keeping as up-to-date on modern coding practices rather than maintaining backwards compatibility._

## Getting Started

<details>
<summary>
:page_facing_up: <strong>Run The Included Examples <em>(Click To Expand)</em></strong>
</summary>
<br/>

Towel has [Examples](https://github.com/ZacharyPatten/Towel/tree/master/Examples) included in this repository.

[Download](https://github.com/ZacharyPatten/Towel/archive/master.zip) this repository and unzip the contents.

There are no custom build processes. Towel should build with any standard .NET build process, but one of the following is recommended:

### Visual Studio 2019

Install [Visual Studio 2019](https://visualstudio.microsoft.com/) if not already installed.

Open the :page_facing_up: **`Towel.sln`** file in Visual Studio.

> _**Note** This is optional, but [here are some recommended settings you change in Visual Studio](https://gist.github.com/ZacharyPatten/693f35653f6c21fbe6c85444792e524b)._

### Visual Studio Code

Install the [.NET Core SDK](https://dotnet.microsoft.com/download) if not already installed.

Install [Visual Studio Code](https://visualstudio.microsoft.com/) if not already installed.

Open the :file_folder: **`root folder`** of the Towel repository in Visual Studio Code.

> _**Note** Towel includes the following:_
> - `.vscode/extensions.json`<sub>recommends Vistual Studio Code extension dependencies</sub>
> - `.vscode/launch.json`<sub>includes the configurations for debugging the examples</sub>
> - `.vscode/settings.json`<sub>automatically applies settings to the workspaces</sub>
> - `.vscode/tasks.json`<sub>includes the commands to build the projects</sub>

> _**Note** Visual Studio Code Extensions (will be prompted to install these when you open the folder):_
> - **ms-vscode.csharp** <sub>C# support</sub>
> - **aisoftware.tt-processor** (optional) <sub>T4 Template support</sub>
> - **zbecknell.t4-support** (optional) <sub>T4 Template syntax highlighting</sub>
> - **formulahendry.dotnet-test-explorer** (optional) <sub>MSTest unit testing support</sub>

</details>

<details>
<summary>
:page_facing_up: <strong>Use Towel In Your .NET Projects <em>(Click To Expand)</em></strong>
</summary>
<br/>

Towel has a nuget package:<br/>
<a href="https://www.nuget.org/packages/Towel" alt="nuget package"><img src="https://img.shields.io/nuget/v/Towel.svg" /></a>

You can install it with the `dotnet add package Towel --version XXXXX` command, or you can
manually add a reference to it in your `.csproj` files `<PackageReference Include="Towel" Version="XXXXX" />`
(where `XXXXX` is the version to install).

</details>

<details>
<summary>
:page_facing_up: <strong>View Documentation <em>(Click To Expand)</em></strong>
</summary>
<br/>

Towel has an API documentation reference that is generated with [docfx](https://github.com/dotnet/docfx). You can view the documentation here:</br>
https://zacharypatten.github.io/Towel/api/index.html</br>
<a href="https://zacharypatten.github.io/Towel/api/index.html"><img src="https://gist.githubusercontent.com/ZacharyPatten/ce53ded67eef6f9563376172dbc0382c/raw/8df6b22faa4255c3f84826639bb3b2d58ebee72a/docfx_badge.svg" title="Go To Towel docfx Documentation" alt="Towel docfx Documentation"></a>

Here is Towel's benchmarking documentation:</br>
https://github.com/ZacharyPatten/Towel/blob/master/.github/Pages/Benchmarks.md

Here are some other documentation references:
- [MSDN Accessing XML Documentation Via Reflection](https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/october/csharp-accessing-xml-documentation-via-reflection)</br>
- [Beginner's Guide To Console Input In C#](https://gist.github.com/ZacharyPatten/798ed612d692a560bdd529367b6a7dbd)</br>
- [Generating Unique Random Data](https://gist.github.com/ZacharyPatten/c9b43a2c9e8a5a5523883e77410f742d)</br>
- [Omnitree](https://gist.github.com/ZacharyPatten/f21fc5c6835faea9be8ae4baab4e294e)</br>
- [C# Generic Math](https://gist.github.com/ZacharyPatten/8e1395a94928f2c7715cf939b0d0389c)</br>

</details>

<details>
<summary>
:page_facing_up: <strong>Get Involved <em>(Click To Expand)</em></strong>
</summary>
<br/>

The easiest way to support Towel is to star the github repository.

If you notice anything in Towel that may be improved, please [create a new issue](https://github.com/ZacharyPatten/Towel/issues/new/choose).

You can chat with the developer(s) on [discord](https://discord.gg/4XbQbwF).

Share your work. If you use Towel in one of your projects we want to hear about it.

If you want to contribute to Towel, you need to fork the github repsoitory, make your desired changes, and open a pull request.
See [Contributing](https://github.com/ZacharyPatten/Towel/blob/master/CONTRIBUTING.md) for more detailed information.

</details>

## Generic Mathematics & Logic

<details>
<summary>
:page_facing_up: <strong>How It Works <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
public static T Addition<T>(T a, T b)
{
	return AdditionImplementation<T>.Function(a, b);
}

internal static class AdditionImplementation<T>
{
	internal static Func<T, T, T> Function = (T a, T b) =>
	{
		ParameterExpression A = Expression.Parameter(typeof(T));
		ParameterExpression B = Expression.Parameter(typeof(T));
		Expression BODY = Expression.Add(A, B);
		Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
		return Function(a, b);
	};
}
```

You can break type safe-ness using generic types and runtime compilation, and you can store the runtime compilation in a delegate so the only overhead is the invocation of the delegate.

</details>

```cs
// Logic Fundamentals
bool EqualTo<T>(T a , T b);
bool LessThan<T>(T a, T b);
bool GreaterThan<T>(T a, T b);
CompareResult Compare<T>(T a, T b);

// Mathematics Fundamentals
T Negation<T>(T a);
T Addition<T>(T a, T b);
T Subtraction<T>(T a, T b);
T Multiplication<T>(T a, T b);
T Division<T>(T a, T b);
T Remainder<T>(T a, T b);

// More Logic
bool IsPrime<T>(T a);
bool IsEven<T>(T a);
bool IsOdd<T>(T a);
T Minimum<T>(T a, T b);
T Maximum<T>(T a, T b);
T Clamp<T>(T value, T floor, T ceiling);
T AbsoluteValue<T>(T a);
bool EqualityLeniency<T>(T a, T b, T leniency);

// More Numerics
void FactorPrimes<T>(T a, Step<T> step);
T Factorial<T>(T a);
T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1);
T LeastCommonMultiple<T>(T a, T b, params T[] c);
T GreatestCommonFactor<T>(T a, T b, params T[] c);
LinearRegression2D<T>(Stepper<T, T> points, out T slope, out T y_intercept);

// Statistics
T Mean<T>(T a, params T[] b);
T Median<T>(params T[] values);
Heap<Link<T, int>> Mode<T>(T a, params T[] b);
void Range<T>(out T minimum, out T maximum, Stepper<T> stepper);
T[] Quantiles<T>(int quantiles, Stepper<T> stepper);
T GeometricMean<T>(Stepper<T> stepper);
T Variance<T>(Stepper<T> stepper);
T StandardDeviation<T>(Stepper<T> stepper);
T MeanDeviation<T>(Stepper<T> stepper);

// Vectors
Vector<T> V1 = new Vector<T>(params T[] vector);
Vector<T> V2 = new Vector<T>(params T[] vector);
Vector<T> V3;
T scalar;
V3 = -V1;                   // Negate
V3 = V1 + V2;               // Add
V3 = V1 - V2;               // Subtract
V3 = V1 * scalar;           // Multiply
V3 = V1 / scalar;           // Divide
scalar = V1.DotProduct(V2); // Dot Product
V3 = V1.CrossProduct(V2);   // Cross Product
V1.Magnitude;               // Magnitude
V3 = V1.Normalize();        // Normalize
bool equal = V1 == V2;      // Equal

// Matrices
Matrix<T> M1 = new Matrix<T>(int rows, int columns);
Matrix<T> M2 = new Matrix<T>(int rows, int columns);
Matrix<T> M3;
Vector<T> V2 = new Vector<T>(params T[] vector);
Vector<T> V3;
T scalar;
M3 = -M1;                               // Negate
M3 = M1 + M2;                           // Add
M3 = M1 - M2;                           // Subtract
M3 = M1 * M2;                           // Multiply
V3 = M1 * V2;                           // Multiply (vector)
M3 = M1 * scalar;                       // Multiply (scalar)
M3 = M1 / scalar;                       // Divide
M3 = M1 ^ 3;                            // Power
scalar = M1.Determinent();              // Determinent
M3 = M1.Minor(int row, int column);     // Minor
M3 = M1.Echelon();                      // Echelon Form (REF)
M3 = M1.ReducedEchelon();               // Reduced Echelon Form (RREF)
M3 = M1.Inverse();                      // Inverse
M1.DecomposeLowerUpper(ref M2, ref M3); // Lower Upper Decomposition
bool equal = M1 == M2;                  // Equal
```

## Symbolic Mathematics

```cs
// Parsing From Linq Expression
Expression<Func<double, double>> exp1 = (x) => 2 * (x / 7);
Symbolics.Expression symExp1 = Symbolics.Parse(exp1);

// Parsing From String
Symbolics.Expression symExp2 = Symbolics.Parse("2 * ([x] / 7)");

// Mathematical Simplification
Symbolics.Expression simplified = symExp1.Simplify();

// Variable Substitution
symExp1.Substitute("x", 5);
```

## Measurement Mathematics

<details>
<summary>
:page_facing_up: <strong>Supported Measurements <em>(Click To Expand)</em></strong>
</summary>
<br/>

Here are the currently supported measurement types:

```cs
//    Acceleration: Length/Time/Time
//    AngularAcceleration: Angle/Time/Time
//    Angle: Angle
//    AngularSpeed: Angle/Time
//    Area: Length*Length
//    AreaDensity: Mass/Length/Length
//    Density: Mass/Length/Length/Length
//    ElectricCharge: ElectricCharge
//    ElectricCurrent: ElectricCharge/Time
//    Energy: Mass*Length*Length/Time/Time
//    Force: Mass*Length/Time/Time
//    Length: Length
//    LinearDensity: Mass/Length
//    LinearMass: Mass*Length
//    LinearMassFlow: Mass*Length/Time
//    Mass: Mass
//    MassRate: Mass/Time
//    Power: Mass*Length*Length/Time/Time/Time
//    Pressure: Mass/Length/Time/Time
//    Speed: Length/Time
//    Tempurature: Tempurature
//    Time: Time
//    TimeArea: Time*Time
//    Volume: Length*Length*Length
//    VolumeRate: Length*Length*Length/Time
```

The measurement types are generated in the *Towel/Measurements/MeasurementTypes.tt* T4 text template file. The unit (enum) definitions are in the *Towel/Measurements/MeasurementUnitDefinitions.cs* file. Both measurment types and unit definitions can be easily added. If you think a measurement type or unit type should be added, please [submit an enhancement issue](https://github.com/ZacharyPatten/Towel/issues/new/choose).

</details>

```cs
// Towel has measurement types to help write scientific code: Acceleration<T>, Angle<T>, Area<T>, 
// Density<T>, Length<T>, Mass<T>, Speed<T>, Time<T>, Volume<T>, etc.

// Automatic Unit Conversion
// When you perform mathematical operations on measurements, any necessary unit conversions will
// be automatically performed by the relative measurement type (in this case "Angle<T>").
Angle<double> angle1 = (90d, Degrees);
Angle<double> angle2 = (.5d, Turns);
Angle<double> result1 = angle1 + angle2; // 270° 

// Type Safeness
// The type safe-ness of the measurement types prevents the miss-use of the measurements. You cannot
// add "Length<T>" to "Angle<T>" because that is mathematically invalid (no operator exists).
Length<double> length1 = (2d, Yards);
object result2 = angle1 + length1; // WILL NOT COMPILE!!!

// Simplify The Syntax Even Further
// You can use alias to remove the generic type if you want to simplify the syntax even further.
using Speedf = Towel.Measurements.Speed<float>; // at top of file
Speedf speed1 = (5, Meters / Seconds);

// Vector + Measurements
// You can use the measurement types inside Towel Vectors.
Vector<Speed<float>> velocity1 = new Vector<Speed<float>>(
	(1f, Meters / Seconds),
	(2f, Meters / Seconds),
	(3f, Meters / Seconds));
Vector<Speedf> velocity2 = new Vector<Speedf>(
	(1f, Centimeters / Seconds),
	(2f, Centimeters / Seconds),
	(3f, Centimeters / Seconds));
Vector<Speed<float>> velocity3 = velocity1 + velocity2;

// Manual Unit Conversions
// 1. Index Operator On Measurement Type
double angle1_inRadians = angle1[Radians];
float speed1_inMilesPerHour = speed1[Miles / Hours];
// 2. Static Conversion Methods
double angle3 = Angle<double>.Convert(7d,
	Radians,  // from
	Degrees); // to
double speed2 = Speed<double>.Convert(8d,
	Meters / Seconds, // from
	Miles / Hours);   // to
double force1 = Force<double>.Convert(9d,
	Kilograms * Meters / Seconds / Seconds, // from
	Grams * Miles / Hours / Hours);         // to
double angle4 = Measurement.Convert(10d,
	Radians,  // from
	Degrees); // to
// Note: The unit conversion on the Measurement class
// is still compile-time-safe.

// Measurement Parsing
Speed<float>.TryParse("20.5 Meters / Seconds",
	out Speed<float> parsedSpeed);
Force<decimal>.TryParse(".1234 Kilograms * Meters / Seconds / Seconds",
	out Force<decimal> parsedForce);
```

## Data Structures

<details>
<summary>
:page_facing_up: <strong>Heap <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
// A heap is a binary tree that is sorted vertically using comparison methods. This is different
// from AVL Trees or Red-Black Trees that keep their contents stored horizontally. The rule
// of a heap is that no parent can be less than either of its children. A Heap using "sifting up"
// and "sifting down" algorithms to move values vertically through the tree to keep items sorted.

IHeap<T> heapArray = new HeapArray<T>();

// Visualization:
//
//    Binary Tree
//
//                      -7
//                      / \
//                     /   \
//                    /     \
//                   /       \
//                  /         \
//                 /           \
//                /             \
//               /               \
//             -4                 1
//             / \               / \     
//            /   \             /   \    
//           /     \           /     \   
//         -1       3         6       4
//         / \     / \       / \     / \ 
//        30  10  17  51    45  22  19  7
//
//    Flattened into an Array
//
//        Root = 1
//        Left Child = 2 * Index
//        Right Child = 2* Index + 1
//         __________________________________________________________________________
//        |0  |-7 |-4 |1  |-1 |3  |6  |4  |30 |10 |17 |51 |45 |22 |19 |7  |0  |0  |0  ...
//         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
//         0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18
```

</details>

<details>
<summary>
:page_facing_up: <strong>AVL Tree <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
// An AVL tree is a binary tree that is sorted using comparison methods and automatically balances
// itself by tracking the heights of nodes and performing one of four specific algorithms: rotate
// right, rotate left, double rotate right, or double rotate left. Any parent in an AVL Tree must
// be greater than its left child but less than its right child (if the children exist). An AVL
// tree is sorted in the same manor as a Red-Black Tree, but uses different algorithms to maintain
// the balance of the tree.

IAvlTree<int> avlTree = new AvlTreeLinked<int>();

// Visualization:
//
//    Binary Tree
//
//        Depth 0 ------------------>    7
//                                      / \
//                                     /   \
//                                    /     \
//                                   /       \
//                                  /         \
//                                 /           \
//                                /             \
//                               /               \
//        Depth 1 --------->    1                 22
//                             / \               / \
//                            /   \             /   \
//                           /     \           /     \
//        Depth 2 ---->    -4       4         17      45
//                         / \     / \       / \     / \
//        Depth 3 --->   -7  -1   3   6     10  19  30  51
//
//    Flattened into an Array
//
//        Root = 1
//        Left Child = 2 * Index
//        Right Child = 2* Index + 1
//         __________________________________________________________________________
//        |0  |7  |1  |22 |-4 |4  |17 |45 |-7 |-1 |3  |6  |10 |19 |30 |51 |0  |0  |0  ...
//         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
//         0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18
```

</details>

<details>
<summary>
:page_facing_up: <strong>Red Black Tree <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
// A Red-Black treeis a binary tree that is sorted using comparison methods and automatically 
// balances itself. Any parent in an Red-Black Tree must be greater than its left child but less
// than its right child (if the children exist). A Red-Black tree is sorted in the same manor as
// an AVL Tree, but uses different algorithms to maintain the balance of the tree.

IRedBlackTree<int> redBlackTree = new RedBlackTreeLinked<int>();

// Visualization:
//
//    Binary Tree
//
//        Color Black ---------------->    7
//                                        / \
//                                       /   \
//                                      /     \
//                                     /       \
//                                    /         \
//                                   /           \
//                                  /             \
//                                 /               \
//        Color Red --------->    1                 22
//                               / \               / \
//                              /   \             /   \
//                             /     \           /     \
//        Color Black --->   -4       4         17      45
//                           / \     / \       / \     / \
//        Color Red --->   -7  -1   3   6     10  19  30  51
//
//    Flattened into an Array
//
//        Root = 1
//        Left Child = 2 * Index
//        Right Child = 2* Index + 1
//         __________________________________________________________________________
//        |0  |7  |1  |22 |-4 |4  |17 |45 |-7 |-1 |3  |6  |10 |19 |30 |51 |0  |0  |0  ...
//         ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
//         0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18
```

</details>

<details>
<summary>
:page_facing_up: <strong>Omnitree <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
// An Omnitree is a Spacial Partitioning Tree (SPT) that works on an arbitrary number of dimensions.
// It stores items sorted along multiple dimensions by dividing spaces into sub-spaces. A 3D
// version of an SPT is often called an "Octree" and a 2D version of an SPT is often called a
// "Quadtree." There are two versions of the Omnitree: Points and Bounds. The Points version stores
// vectors while the Bounds version stores spaces with a minimum and maximum vector.

IOmnitreePoints<T, A1, A2, A3...> omnitreePoints =
    new OmnitreePointsLinked<T, A1, A2, A3...>(
        (T value, out A1 a1, out A2 a2, out A3 a3...) => { ... });
        
IOmnitreeBounds<T, A1, A2, A3...> omnitreeBounds =
    new OmnitreeBoundsLinked<T, A1, A2, A3...>(
        (T value,
        out A1 min1, out A1 max1,
        out A2 min2, out A2 max2,
        out A3 min3, out A3 max3...) => { ... });

// The maximum number of children any node can have is 2 ^ N where N is the number
// of dimensions of the tree.
//
//    -------------------------------
//    | Dimensions | Max # Children |
//    |============|================|
//    |     1      |   2 ^ 1 = 2    |
//    |     2      |   2 ^ 2 = 4    |
//    |     3      |   2 ^ 3 = 8    |
//    |     4      |   2 ^ 4 = 16   |
//    |    ...     |      ...       |
//    -------------------------------
//
// Visualizations
//
// 1 Dimensional:
//
//  -1D |-----------|-----------| +1D        Children Indexes:
//                                           -1D: 0
//       <--- 0 ---> <--- 1 --->             +1D: 1
//
// 2 Dimensional:
//       _____________________
//      |          |          |  +2D
//      |          |          |   ^
//      |     2    |     3    |   |        Children Indexes:
//      |          |          |   |        -2D -1D: 0
//      |----------|----------|   |        -2D +1D: 1
//      |          |          |   |        +2D -1D: 2
//      |          |          |   |        +2D +1D: 3
//      |     0    |     1    |   |
//      |          |          |   v
//      |__________|__________|  -2D
//
//       -1D <-----------> +1D 
//
// 3 Dimensional:
//
//            +3D     _____________________
//           7       /         /          /|
//          /       /    6    /     7    / |
//         /       /---------/----------/  |                     Children Indexes:
//        /       /    2    /     3    /|  |                     -3D -2D -1D: 0
//       L       /_________/__________/ |  |                     -3D -2D +1D: 1
//    -3D       |          |          | | /|          +2D        -3D +2D -1D: 2
//              |          |          | |/ |           ^         -3D +2D +1D: 3
//              |     2    |     3    | /  |           |         +3D -2D -1D: 4
//              |          |          |/|  | <-- 5     |         +3D -2D +1D: 5
//              |----------|----------| |  |           |         +3D +2D -1D: 6
//              |          |          | |  /           |         +3D +2D +1D: 7
//              |          |          | | /            |
//              |     0    |     1    | |/             |
//              |          |          | /              v
//              |__________|__________|/              -2D
//             
//                   ^
//                   |
//                   4 (behind 0)
//
//               -1D <-----------> +1D
//
// 4 Dimensional:
//
//     +1D         +2D         +3D         +4D       Children Indexes:
//      ^           ^           ^           ^
//      |           |           |           |        -4D -3D -2D -1D: 0   +4D -3D -2D -1D: 8
//      |           |           |           |        -4D -3D -2D +1D: 1   +4D -3D -2D +1D: 9
//      |           |           |           |        -4D -3D +2D -1D: 2   +4D -3D +2D -1D: 10
//      |           |           |           |        -4D -3D +2D +1D: 3   +4D -3D +2D +1D: 11
//      |           |           |           |        -4D +3D -2D -1D: 4   +4D +3D -2D -1D: 12
//     ---         ---         ---         ---       -4D +3D -2D +1D: 5   +4D +3D -2D +1D: 13
//      |           |           |           |        -4D +3D +2D -1D: 6   +4D +3D +2D -1D: 14
//      |           |           |           |        -4D +3D +2D +1D: 7   +4D +3D +2D +1D: 15
//      |           |           |           |
//      |           |           |           |
//      |           |           |           |
//      v           v           v           v
//     -1D         -2D         -3D         -4D
//
//     With a value that is in the (+1D, -2D, -3D, +4D)[Index 9] child:
//
//     +1D         +2D         +3D         +4D
//      ^           ^           ^           ^
//      |           |           |           |
//      |           |           |           |
//      O---        |           |        ---O
//      |   \       |           |       /   |
//      |    \      |           |      /    |
//     ---    \    ---         ---    /    ---
//      |      \    |           |    /      |
//      |       \   |           |   /       |
//      |        ---O-----------O---        |
//      |           |           |           |
//      |           |           |           |
//      v           v           v           v
//     -1D         -2D         -3D         -4D

// By default, the omnitree will sort items along each axis and use the median algorithm to determine
// the point of divisions. However, you can override the subdivision algorithm. For numerical values,
// the mean algorithm can be used (and is much faster than median). If you know the data set will be
// relatively evenly distributed within a sub-space, you can even set the subdivision algorithm to
// calculate the subdivision from parent spaces rather than looking at the current contents of the
// space. Note: In a future enhancement I will automatically detect if the mean algorithm is possible
// for a given type, and then the default will depend on the type in use.

// The depth of the omnitree is bounded by "ln(count)" the natural log of the current count. When adding
// and item to the tree, if the number of items in the respective child is greater than ln(count) and 
// the depth bounding has not been reached, then the child will be subdivided. The goal is to achieve 
// Ω(ln(count)) runtime complexity when looking up values.
```

</details>

<details>
<summary>
:page_facing_up: <strong>Tree <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
Tree<T> treeMap = new TreeMap<T>(...);
```

</details>

<details>
<summary>
:page_facing_up: <strong>Graph <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
// A graph is a data structure that contains nodes and edges. They are useful
// when you need to model real world scenarios. They also are generally used
// for particular algorithms such as path finding. The GraphSetOmnitree is a
// graph that stores nodes in a hashed set and the edges in a 2D omnitree (aka
// quadtree).

IGraph<int> graph = new GraphSetOmnitree<int>()
{
	// add nodes
	0,
	1,
	2,
	3,
	// add edges
	{ 0, 1 },
	{ 1, 2 },
	{ 2, 3 },
	{ 0, 3 },
	// visualization
	//
	//     0 --------> 1
	//     |           |
	//     |           |
	//     |           |
	//     v           v
	//     3 <-------- 2
};
```

</details>

<details>
<summary>
:page_facing_up: <strong>Trie <em>(Click To Expand)</em></strong>
</summary>
<br/>

```cs
// A trie is a tree that stores values in a way that partial keys may be shared
// amongst values to reduce redundant memory usage. They are generally used with
// large data sets such as storing all the words in the English language. For
// example, the words "farm" and "fart" both have the letters "far" in common.
// A trie takes advantage of that and only stores the necessary letters for
// those words ['f'->'a'->'r'->('t'||'m')]. A trie is not limited to string
// values though. Any key type that can be broken into pieces (and shared),
// could be used in a trie.
//
// There are two versions. One that only stores the values of the trie (ITrie<T>)
// and one that stores the values of the trie plus an additional generic value
// on the leaves (ITrie<T, D>).

ITrie<T> trie = new TrieLinkedHashLinked<T>();

ITrie<T, D> trieWithAdditionalData = new TrieLinkedHashLinked<T, D>();
```

</details>

## Algorithms

```cs
// Sorting
Sort.Shuffle<T>(...);
Sort.Bubble<T>(...);
Sort.Selection<T>(...);
Sort.Insertion<T>(...);
Sort.Quick<T>(...);
Sort.Merge<T>(...);
Sort.Heap<T>(...);
Sort.OddEven<T>(...);
Sort.Cocktail<T>(...);
Sort.Comb<T>(...);
Sort.Gnome<T>(...);
Sort.Shell<T>(...);
Sort.Bogo<T>(...);
Sort.Slow<T>(...);

Search.Binary<T>(...);

// Path Finding (Graph Search)
// Note: overloads for A*, Dijkstra, and Breadth-First-Search algorithms
Search.Graph<Node, Numeric>(...);
```

_Note: [Benchmarks](https://github.com/ZacharyPatten/Towel/blob/master/.github/Pages/Benchmarks.md) are included for the sorting algorithms._

## Extensions

```cs
// System.Random extensions to generate more random types
// Note: there are overloads to specify possible ranges
string NextString(this Random random, int length);
char NextChar(this Random random);
decimal NextDecimal(this Random random);
DateTime DateTime(this Random random);
TimeSpan TimeSpan(this Random random);
long NextLong(this Random random);
IEnumerable<int> NextUnique(this Random random, int count, int minValue, int maxValue); // unique values
T Next<T>(this Random random, IEnumerable<(T Value, double Weight)> pool); // weighted values
void Shuffle<T>(this Random random, T[] array); // randomize arrays

// Type conversion to string definition as appears in C# source code
// Note: useful for runtime compilation from strings
string ConvertToCSharpSourceDefinition(this Type type);
// Example: typeof(List<int>) -> "System.Collections.Generic.List<int>"

string ToEnglishWords(this decimal @decimal);
// Example: 12 -> "Twelve"

// Reflection Extensions To Access XML Documentation
string GetDocumentation(this Type type);
string GetDocumentation(this FieldInfo fieldInfo);
string GetDocumentation(this PropertyInfo propertyInfo);
string GetDocumentation(this EventInfo eventInfo);
string GetDocumentation(this ConstructorInfo constructorInfo);
string GetDocumentation(this MethodInfo methodInfo);
string GetDocumentation(this MemberInfo memberInfo);
string GetDocumentation(this ParameterInfo parameterInfo);

// Iterating the permutations of an array
void PermuteRecursive<T>(this T[] array, Step<T[]> step);
void PermuteIterative<T>(this T[] array, Step<T[]> step);
```

## Console Helpers

```cs
// Just some helpe rmethods for console applications...

// command line argument parser/handler
CommandLine.HandleArguments();

// wait for keypress to continue an intercept input
ConsoleHelper.PromptPressToContinue(...);
// generic method for retrieving validated console input
ConsoleHelper.GetInput<T>(...);
// animated ellipsis character to show processing
ConsoleHelper.AnimatedEllipsis(...);
// render progress bar in console
ConsoleHelper.ProgressBar(...);
// Console.ReadLine() with hidden input characters
ConsoleHelper.HiddenReadLine();
// easily manage int-based console menus
ConsoleHelper.IntMenu(...);
// preventing console input
ConsoleHelper.FlushInputBuffer();
```

## Developer(s)

> [Zachary Patten](https://github.com/ZacharyPatten)</br>
> Howdy! I'm Zachary Patten. I like making code frameworks and teaching people how to code. I only work on Towel in my free time, but feel free to contact me if you have questions and I will respond when I am able. :)
