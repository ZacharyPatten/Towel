# Towel
If you wanna survive out here you've got to know where your towel is.

Towel is a .Net Standard framework written in C# intended to add much needed functionality that is missing in .Net Standard as well as redesign some aspects to bring things up to modern standards.

## Mathematics

The Towel framework has fast generic mathematics functions.

### Fundamental Operations

    T Add<T>(T a, T b)
    T Negate<T>(T a)
    T Subtract<T>(T a, T b)
    T Multiply<T>(T a, T b)
    T Divide<T>(T a, T b)
    T Modulo<T>(T a, T b)
  
### Vectors

    Vector<T> V1 = new Vector<T>(params T[] vector)
    Vector<T> V2 = new Vector<T>(params T[] vector)
    Vector<T> V3
    T scalar
  
    Negate: V3 = -V1
    Add: V3 = V1 + V2
    Subtract: V3 = V1 - V2
    Multiply: V3 = V1 * scalar
    Divide: V3 = V1 / scalar
    Dot Product: scalar = V1.DotProduct(V2)
    Cross Product: V3 = V1.CrossProduct(V2)
    Magnitude: V1.Magnitude
    Normalize: V3 = V1.Normalize()
    Equality: bool equal = V1 == V2

### Matrices

    Matrix<T> M1 = new Matrix<T>(int rows, int columns)
    Matrix<T> M2 = new Matrix<T>(int rows, int columns)
    Matrix<T> M3
    Vector<T> V2 = new Vector<T>(params T[] vector)
    Vector<T> V3
    T scalar

    Negate: M3 = -M1
    Add: M3 = M1 + M2
    Subtract: M3 = M1 - M2
    Multiply: M3 = M1 * M2
    Multiply (with vector): V3 = M1 * V2
    Multiply (with scalar): M3 = M1 * scalar
    Divide: M3 = M1 / scalar
    Power: M3 = M1 ^ 3
    Determinent: scalar = M1.Determinent()
    Minor: M3 = M1.Minor(int row, int column)
    Echelon (REF): M3 = M1.Echelon()
    Reduced Echelon (RREF): M3 = M1.ReducedEchelon()
    Inverse: M3 = M1.Inverse()
    Lower/Upper Decomposition: M1.DecomposeLowerUpper(ref M2, ref M3)
    Equality: bool equal = M1 == M2
    
### Statistics

    T Mean<T>(T a, params T[] b)
    T Median<T>(params T[] values)
    Heap<Link<T, int>> Mode<T>(T a, params T[] b)
    void Range<T>(out T minimum, out T maximum, Stepper<T> stepper)
    T[] Quantiles<T>(int quantiles, Stepper<T> stepper)
    T GeometricMean<T>(Stepper<T> stepper)
    T Variance<T>(Stepper<T> stepper)
    T StandardDeviation<T>(Stepper<T> stepper)
    T MeanDeviation<T>(Stepper<T> stepper)
    
### Other Mathematics

    void FactorPrimes<T>(T a, Step<T> step)
    T Factorial<T>(T a)
    T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1)
    T LeastCommonMultiple<T>(T a, T b, params T[] c)
    T GreatestCommonFactor<T>(T a, T b, params T[] c)
    
## Data Structures

Towel has many useful data structure. Especially the Omnitree, which is an SPT that work on any number of dimensions. It can be used to make a quadtree, octree, or SPT's with higher dimensions.

* Heap
* AVL Tree
* Red Black Tree
* Omnitree (Quadtree, Octree, ...)
* Graph

## Algorithms

Towel has many algorithms, such as generic A* and Greedy Path finding.

* Sorting
   * Shuffle
   * Bubble
   * Selection
   * Insertion
   * Quick
   * Merge
   * Heap
   * Odd Even
* Path Finding (Graph Search)
   * A*
   * Greedy

## Extensions

Towel has many useful extensions for base types in .Net Standard.

Here are extensions on "System.Random" to generate more random types:

    string NextString(this Random random, int length)
    char NextChar(this Random random)
    decimal NextDecimal(this Random random)
    DateTime DateTime(this Random random)
    TimeSpan TimeSpan(this Random random)
    
