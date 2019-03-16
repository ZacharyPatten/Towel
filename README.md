# Towel
If you wanna survive out here you've got to know where your towel is.

Towel is a C# .Net Standard libary intended to add much needed functionality that is missing in .Net Standard as well as redesign some aspects to bring things up to modern standards.

## Build Status

[![Build Status](https://dev.azure.com/ZacharyPatten/Towel/_apis/build/status/ZacharyPatten.Towel?branchName=master)](https://dev.azure.com/ZacharyPatten/Towel/_build/latest?definitionId=1&branchName=master)

## Developer(s)

 - Zachary Patten
   - sevenix.zp@gmail.com

Feel free to contact me and I will respond as soon as I am able. :)

## Mathematics

The Towel framework has fast generic mathematics functions.

### Fundamental Operations
```csharp
T Add<T>(T a, T b);
T Negate<T>(T a);
T Subtract<T>(T a, T b);
T Multiply<T>(T a, T b);
T Divide<T>(T a, T b);
T Modulo<T>(T a, T b);
```
### Vectors
```csharp
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
```
### Matrices
```csharp
Matrix<T> M1 = new Matrix<T>(int rows, int columns);
Matrix<T> M2 = new Matrix<T>(int rows, int columns);
Matrix<T> M3;
Vector<T> V2 = new Vector<T>(params T[] vector);
Vector<T> V3;
T scalar;

M3 = -M1;                               // Negate
M1 + M2;                                // Add
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
### Statistics
```csharp
T Mean<T>(T a, params T[] b);
T Median<T>(params T[] values);
Heap<Link<T, int>> Mode<T>(T a, params T[] b);
void Range<T>(out T minimum, out T maximum, Stepper<T> stepper);
T[] Quantiles<T>(int quantiles, Stepper<T> stepper);
T GeometricMean<T>(Stepper<T> stepper);
T Variance<T>(Stepper<T> stepper);
T StandardDeviation<T>(Stepper<T> stepper);
T MeanDeviation<T>(Stepper<T> stepper);
```
### Other Numeric Mathematics
```csharp
void FactorPrimes<T>(T a, Step<T> step);
T Factorial<T>(T a);
T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1);
T LeastCommonMultiple<T>(T a, T b, params T[] c);
T GreatestCommonFactor<T>(T a, T b, params T[] c);
```
### Symbolic Mathematics
```csharp
// From Linq Expression
Expression<Func<double, double>> exp1 = (x) => 2 * (x / 7);
Symbolics.Expression symExp1 = Symbolics.Parse(e1);
Symbolics.Expression simplified = symExp1.Simplify(); // Simplification
symExp1.Substitute("x", 5);                           // Variable Subsitition

// From String
Symbolics.Expression symExp2 = Symbolics.Parse("2 * (x / 7)");
Symbolics.Expression simplified = symExp2.Simplify(); // Simplification
symExp2.Substitute("x", 5);                           // Variable Subsitition
```
## Type Safe Measurement Mathematics (TSMM)

Towel has measurement classes to provide type safe mathematics with automatic unit conversion. :)

## Data Structures

Towel has many useful data structure. Especially the Omnitree, which is an SPT that work on any number of dimensions. It can be used to make a quadtree, octree, or SPT's with higher dimensions.
```csharp
Heap<T> heap;
AvlTree<T> avlTree;
RedBlackTree<T> redBlackTree;
Omnitree<T, A1, A2, ... AX> omnitree; // (Quadtree, Octree, ...)
Tree<T> tree;
Graph<T> graph;
```
## Algorithms
```csharp
// Sorting
Sort<T>.Shuffle(...);
Sort<T>.Bubble(...);
Sort<T>.Selection(...);
Sort<T>.Insertion(...);
Sort<T>.Quick(...);
Sort<T>.Merge(...);
Sort<T>.Heap(...);
Sort<T>.OddEven(...);

// Path Finding
Search<T>.Graph<Math>.Astar(...);
Search<T>.Graph<Math>.Greedy(...);
```
## Extensions
```csharp
// System.Random extensions to generate more random types
string NextString(this Random random, int length);
char NextChar(this Random random);
decimal NextDecimal(this Random random);
DateTime DateTime(this Random random);
TimeSpan TimeSpan(this Random random);
```