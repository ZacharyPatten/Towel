using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Towel.Measurements;
using static Towel.Syntax;

namespace Towel.Mathematics
{
	/// <summary>A matrix of arbitrary dimensions implemented as a flattened array.</summary>
	/// <typeparam name="T">The numeric type of this Matrix.</typeparam>
	[DebuggerDisplay("{" + nameof(DebuggerString) + "}")]
	[Serializable]
	public class Matrix<T>
	{
		internal readonly T[] _matrix;
		internal int _rows;
		internal int _columns;

		#region Properties

		internal int Length { get { return _matrix.Length; } }
		/// <summary>The number of rows in the matrix.</summary>
		public int Rows { get { return _rows; } }
		/// <summary>The number of columns in the matrix.</summary>
		public int Columns { get { return _columns; } }
		/// <summary>Determines if the matrix is square.</summary>
		public bool IsSquare { get { return _rows == _columns; } }
		/// <summary>Determines if the matrix is a vector.</summary>
		public bool IsVector { get { return _columns == 1; } }
		/// <summary>Determines if the matrix is a 2 component vector.</summary>
		public bool Is2x1 { get { return _rows == 2 && _columns == 1; } }
		/// <summary>Determines if the matrix is a 3 component vector.</summary>
		public bool Is3x1 { get { return _rows == 3 && _columns == 1; } }
		/// <summary>Determines if the matrix is a 4 component vector.</summary>
		public bool Is4x1 { get { return _rows == 4 && _columns == 1; } }
		/// <summary>Determines if the matrix is a 2 square matrix.</summary>
		public bool Is2x2 { get { return _rows == 2 && _columns == 2; } }
		/// <summary>Determines if the matrix is a 3 square matrix.</summary>
		public bool Is3x3 { get { return _rows == 3 && _columns == 3; } }
		/// <summary>Determines if the matrix is a 4 square matrix.</summary>
		public bool Is4x4 { get { return _rows == 4 && _columns == 4; } }

		/// <summary>Standard row-major matrix indexing.</summary>
		/// <param name="row">The row index.</param>
		/// <param name="column">The column index.</param>
		/// <returns>The value at the given indeces.</returns>
		public T this[int row, int column]
		{
			get
			{
				if (row < 0 || row > Rows)
				{
					throw new ArgumentOutOfRangeException(nameof(row), row, "!(" + nameof(row) + " >= 0) || !(" + nameof(row) + " < " + nameof(Rows) + ")");
				}
				if (column < 0 || column > Columns)
				{
					throw new ArgumentOutOfRangeException(nameof(column), row, "!(" + nameof(column) + " >= 0) || !(" + nameof(column) + " < " + nameof(Columns) + ")");
				}
				return Get(row, column);
			}
			set
			{
				if (row < 0 || row > Rows)
				{
					throw new ArgumentOutOfRangeException(nameof(row), row, "!(" + nameof(row) + " >= 0) || !(" + nameof(row) + " < " + nameof(Rows) + ")");
				}
				if (column < 0 || column > Columns)
				{
					throw new ArgumentOutOfRangeException(nameof(column), row, "!(" + nameof(column) + " >= 0) || !(" + nameof(column) + " < " + nameof(Columns) + ")");
				}
				Set(row, column, value);
			}
		}

		/// <summary>Indexing of the flattened array representing the matrix.</summary>
		/// <param name="flatIndex">The flattened index of the matrix.</param>
		/// <returns>The value at the given flattened index.</returns>
		public T this[int flatIndex]
		{
			get
			{
				if (flatIndex < 0 || _matrix.Length < flatIndex)
				{
					throw new ArgumentOutOfRangeException(nameof(flatIndex), flatIndex, "!(" + nameof(flatIndex) + " >= 0) || !(" + nameof(flatIndex) + " < " + nameof(Rows) + " * " + nameof(Columns) + ")");
				}
				return _matrix[flatIndex];
			}
			set
			{
				if (flatIndex < 0 || _matrix.Length < flatIndex)
				{
					throw new ArgumentOutOfRangeException(nameof(flatIndex), flatIndex, "!(" + nameof(flatIndex) + " >= 0) || !(" + nameof(flatIndex) + " < " + nameof(Rows) + " * " + nameof(Columns) + ")");
				}
				_matrix[flatIndex] = value;
			}
		}

		#endregion

		#region Debugger Properties

		internal string DebuggerString
		{
			get
			{
				int ROWS = this.Rows;
				int COLUMNS = this.Columns;
				if (ROWS < 5 && COLUMNS < 5)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("[");
					for (int i = 0; i < ROWS; i++)
					{
						stringBuilder.Append("[");
						for (int j = 0; j < COLUMNS; j++)
						{
							stringBuilder.Append(Get(i, j));
							if (j < COLUMNS - 1)
							{
								stringBuilder.Append(",");
							}
						}
						stringBuilder.Append("]");
					}
					stringBuilder.Append("]");
					return stringBuilder.ToString();
				}
				return ToString();
			}
		}

		#endregion

		#region Constructors

		internal Matrix(int rows, int columns, int length)
		{
			_matrix = new T[length];
			_rows = rows;
			_columns = columns;
		}

		/// <summary>Constructs a new default matrix of the given dimensions.</summary>
		/// <param name="rows">The number of row dimensions.</param>
		/// <param name="columns">The number of column dimensions.</param>
		public Matrix(int rows, int columns)
		{
			if (rows < 1)
			{
				throw new ArgumentOutOfRangeException("rows", rows, "!(rows > 0)");
			}
			if (columns < 1)
			{
				throw new ArgumentOutOfRangeException("columns", columns, "!(columns > 0)");
			}
			_matrix = new T[rows * columns];
			_rows = rows;
			_columns = columns;
		}

		/// <summary>Constructs a new matrix and initializes it via function.</summary>
		/// <param name="rows">The number of rows to construct.</param>
		/// <param name="columns">The number of columns to construct.</param>
		/// <param name="function">The initialization function.</param>
		public Matrix(int rows, int columns, Func<int, int, T> function) : this(rows, columns)
		{
			Format(this, function);
		}

		/// <summary>Constructs a new matrix and initializes it via function.</summary>
		/// <param name="rows">The number of rows to construct.</param>
		/// <param name="columns">The number of columns to construct.</param>
		/// <param name="function">The initialization function.</param>
		public Matrix(int rows, int columns, Func<int, T> function) : this(rows, columns)
		{
			Format(this, function);
		}

		/// <summary>
		/// Creates a new matrix using ROW MAJOR ORDER. The data will be referenced to, so 
		/// changes to it will modify the constructed matrix.
		/// </summary>
		/// <param name="rows">The number of rows to construct.</param>
		/// <param name="columns">The number of columns to construct.</param>
		/// <param name="data">The data of the matrix in ROW MAJOR ORDER.</param>
		public Matrix(int rows, int columns, params T[] data) : this(rows, columns)
		{
			_ = data ?? throw new ArgumentNullException(nameof(data));
			if (data.Length != rows * columns)
			{
				throw new ArgumentException("!(" + nameof(rows) + " * " + nameof(columns) + " == " + nameof(data) + "." + nameof(data.Length) + ")");
			}
			_rows = rows;
			_columns = columns;
			_matrix = data;
		}

		internal Matrix(Matrix<T> matrix)
		{
			_rows = matrix._rows;
			_columns = matrix.Columns;
			_matrix = (matrix._matrix).Clone() as T[];
		}

		internal Matrix(Vector<T> vector)
		{
			_rows = vector.Dimensions;
			_columns = 1;
			_matrix = vector._vector.Clone() as T[];
		}

		#endregion

		#region Factories

		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed zero-matrix.</returns>
		public static Matrix<T> FactoryZero(int rows, int columns)
		{
			if (rows < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
			}
			if (columns < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
			}
			return FactoryZeroImplementation(rows, columns);
		}

		internal static Func<int, int, Matrix<T>> FactoryZeroImplementation = (rows, columns) =>
		{
			if (EqualTo(default, Constant<T>.Zero))
			{
				FactoryZeroImplementation = (ROWS, COLUMNS) => new Matrix<T>(ROWS, COLUMNS);
			}
			else
			{
				FactoryZeroImplementation = (ROWS, COLUMNS) =>
				{
					Matrix<T> matrix = new Matrix<T>(ROWS, COLUMNS);
					matrix._matrix.Format(Constant<T>.Zero);
					return matrix;
				};
			}
			return FactoryZeroImplementation(rows, columns);
		};

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static Matrix<T> FactoryIdentity(int rows, int columns)
		{
			if (rows < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
			}
			if (columns < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
			}
			return FactoryIdentityImplementation(rows, columns);
		}

		internal static Func<int, int, Matrix<T>> FactoryIdentityImplementation = (rows, columns) =>
		{
			if (EqualTo(default, Constant<T>.Zero))
			{
				FactoryIdentityImplementation = (ROWS, COLUMNS) =>
				{
					Matrix<T> matrix = new Matrix<T>(ROWS, COLUMNS);
					T[] MATRIX = matrix._matrix;
					int minimum = Minimum(ROWS, COLUMNS);
					for (int i = 0; i < minimum; i++)
					{
						MATRIX[i * COLUMNS + i] = Constant<T>.One;
					}
					return matrix;
				};
			}
			else
			{
				FactoryIdentityImplementation = (ROWS, COLUMNS) =>
				{
					Matrix<T> matrix = new Matrix<T>(ROWS, COLUMNS);
					Format(matrix, (x, y) => x == y ? Constant<T>.One : Constant<T>.Zero);
					return matrix;
				};
			}
			return FactoryIdentityImplementation(rows, columns);
		};

		/// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <param name="value">The value to assign every spot in the matrix.</param>
		/// <returns>The newly constructed matrix filled with the uniform value.</returns>
		public static Matrix<T> FactoryUniform(int rows, int columns, T value)
		{
			if (rows < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
			}
			if (columns < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
			}
			Matrix<T> matrix = new Matrix<T>(rows, columns);
			matrix._matrix.Format(value);
			return matrix;
		}

		#endregion

		#region Mathematics

		#region RowMultiplication

		internal static void RowMultiplication(Matrix<T> matrix, int row, T scalar)
		{
			int columns = matrix.Columns;
			for (int i = 0; i < columns; i++)
			{
				matrix.Set(row, i, Multiplication(matrix.Get(row, i), scalar));
			}
		}

		#endregion

		#region RowAddition

		internal static void RowAddition(Matrix<T> matrix, int target, int second, T scalar)
		{
			int columns = matrix.Columns;
			for (int i = 0; i < columns; i++)
			{
				matrix.Set(target, i, Addition(matrix.Get(target, i), Multiplication(matrix.Get(second, i), scalar)));
			}
		}

		#endregion

		#region SwapRows

		internal static void SwapRows(Matrix<T> matrix, int row1, int row2) =>
			SwapRows(matrix, row1, row2, 0, matrix.Columns - 1);

		internal static void SwapRows(Matrix<T> matrix, int row1, int row2, int columnStart, int columnEnd)
		{
			for (int i = columnStart; i <= columnEnd; i++)
			{
				T temp = matrix.Get(row1, i);
				matrix.Set(row1, i, matrix.Get(row2, i));
				matrix.Set(row2, i, temp);
			}
		}

		#endregion

		#region GetCofactor

		internal static void GetCofactor(Matrix<T> a, Matrix<T> temp, int p, int q, int n)
		{
			int i = 0, j = 0;
			for (int row = 0; row < n; row++)
			{
				for (int col = 0; col < n; col++)
				{
					if (row != p && col != q)
					{
						temp.Set(i, j++, a.Get(row, col));
						if (j == n - 1)
						{
							j = 0;
							i++;
						}
					}
				}
			}
		}

		#endregion

		#region GetDeterminant Gaussian elimination

		#region MatrixElementFraction
		/// <summary>
		/// Used to avoid issues when 1/2 + 1/2 = 0 + 0 = 0 instead of 1 for types, where division results in precision loss
		/// </summary>
		/// <typeparam name="T"></typeparam>
		private sealed class MatrixElementFraction<T>
		{
			private T Numerator;
			private T Denominator;

			internal MatrixElementFraction(T value)
			{
				Numerator = value;
				Denominator = Constant<T>.One;
			}

			internal MatrixElementFraction(T num, T den)
			{
				Numerator = num;
				Denominator = den;
			}

			public T Value => Division(Numerator, Denominator);

			// a / b + c / d = (a * d + b * c) / (b * d)
			public static MatrixElementFraction<T> operator +(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> new MatrixElementFraction<T>(Addition(
					Multiplication(a.Numerator, b.Denominator),
					Multiplication(a.Denominator, b.Numerator)
				), Multiplication(a.Denominator, b.Denominator));

			public static MatrixElementFraction<T> operator *(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> new MatrixElementFraction<T>(Multiplication(a.Numerator, b.Numerator), Multiplication(a.Denominator, b.Denominator));

			public static MatrixElementFraction<T> operator -(MatrixElementFraction<T> a)
				=> new MatrixElementFraction<T>(Multiplication(a.Numerator, Constant<T>.NegativeOne), a.Denominator);

			public static MatrixElementFraction<T> operator -(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> a + (-b);

			// a / b / (d / c) = (a * c) / (b * d)
			public static MatrixElementFraction<T> operator /(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> new MatrixElementFraction<T>(Multiplication(a.Numerator, b.Denominator), Multiplication(a.Denominator, b.Numerator));

			public static bool operator <(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
			{
				var c = Multiplication(a.Numerator, b.Denominator);
				var d = Multiplication(b.Numerator, a.Denominator);
				if (IsPositive(Multiplication(a.Denominator, b.Denominator)))
					return LessThan(c, d);
				else
					return !LessThan(c, d);
			}

			public static bool operator ==(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> Compare.Default(a.Numerator, b.Numerator) == CompareResult.Equal &&
				   Compare.Default(a.Denominator, b.Denominator) == CompareResult.Equal;
			public static bool operator !=(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> !(a == b);

			public static bool operator >(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> a >= b && !(a == b);

			public static bool operator >=(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> !(a < b);

			public static bool operator <=(MatrixElementFraction<T> a, MatrixElementFraction<T> b)
				=> a < b || a == b;

			public static explicit operator MatrixElementFraction<T>(int val)
				=> new MatrixElementFraction<T>(Convert<int, T>(val));

			public MatrixElementFraction<T> Abs()
				=> new MatrixElementFraction<T>(AbsoluteValue(Numerator), AbsoluteValue(Denominator));

			public bool IsDividedByZero => Compare.Default(Denominator, Constant<T>.Zero) == CompareResult.Equal;
		}
#endregion

		/// <summary>
		/// Reference: https://codereview.stackexchange.com/questions/204135/determinant-using-gauss-elimination
		/// </summary>
		internal static T GetDeterminantGaussian(Matrix<T> matrix, int n)
		{
			// Note: the reasoning behind using MatrixElementFraction is to account for
			// rounding errors such as 2.00...01 instead of 2

			if (n == 1)
			{
				return matrix.Get(0, 0);
			}
			MatrixElementFraction<T> determinant = new MatrixElementFraction<T>(Constant<T>.One);
			Matrix<MatrixElementFraction<T>> fractioned = new Matrix<MatrixElementFraction<T>>(matrix.Rows, matrix.Columns, (r, c) => new MatrixElementFraction<T>(matrix.Get(r, c)));
			for (int i = 0; i < n; i++)
			{
				var pivotElement = fractioned.Get(i, i);
				var pivotRow = i;
				for (int row = i + 1; row < n; ++row)
				{
					if (fractioned[row, i].Abs() > pivotElement.Abs())
					{
						pivotElement = fractioned.Get(row, i);
						pivotRow = row;
					}
				}
				if (pivotElement.Equals(Constant<T>.Zero))
				{
					return Constant<T>.Zero;
				}
				if (pivotRow != i)
				{
					Matrix<MatrixElementFraction<T>>.SwapRows(fractioned, i, pivotRow, 0, n - 1);
					determinant = Negation(determinant);
				}
				determinant *= pivotElement;
				for (int row = i + 1; row < n; row++)
				{
					for (int column = i + 1; column < n; column++)
					{
						// reference: matrix[row][column] -= matrix[row][i] * matrix[i][column] / pivotElement;
						fractioned.Set(row, column,
							// D - A * B / C
							D_subtract_A_multiply_B_divide_C<MatrixElementFraction<T>>.Function(
								fractioned.Get(row, i), /*     A */
								fractioned.Get(i, column), /*     B */
								pivotElement, /*               C */
								fractioned.Get(row, column))); /* D */
					}
				}
			}
			// TODO: should we return zero if determinant's denominator is zero?
			return determinant.IsDividedByZero ? Constant<T>.Zero : determinant.Value;
		}


		#endregion

		#region GetDeterminant Laplace method

		internal static T GetDeterminantLaplace(Matrix<T> a, int n)
		{
			T determinant = Constant<T>.Zero;
			if (n == 1)
			{
				return a.Get(0, 0);
			}
			Matrix<T> temp = new Matrix<T>(n, n);
			T sign = Constant<T>.One;
			for (int f = 0; f < n; f++)
			{
				GetCofactor(a, temp, 0, f, n);
				determinant =
					Addition(determinant,
						Multiplication(sign,
							Multiplication(a.Get(0, f),
								GetDeterminantLaplace(temp, n - 1))));
				sign = Negation(sign);
			}
			return determinant;
		}

		#endregion

		#region IsSymetric

		/// <summary>Determines if the matrix is symetric.</summary>
		/// <param name="a">The matrix to determine if symetric.</param>
		/// <returns>True if the matrix is symetric; false if not.</returns>
		public static bool GetIsSymetric(Matrix<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			int rows = a._rows;
			if (rows != a._columns)
			{
				return false;
			}
			T[] A = a._matrix;
			for (int row = 0; row < rows; row++)
			{
				for (int column = row + 1; column < rows; column++)
				{
					if (InequalTo(A[row * rows + column], A[column * rows + row]))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Determines if the matrix is symetric.</summary>
		/// <returns>True if the matrix is symetric; false if not.</returns>
		public bool IsSymetric
		{
			get
			{
				return GetIsSymetric(this);
			}
		}

		#endregion

		#region Negate

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="a">The matrix to have its values negated.</param>
		/// <param name="b">The resulting matrix after the negation.</param>
		public static void Negate(Matrix<T> a, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			int Length = a._matrix.Length;
			T[] A = a._matrix;
			T[] B;
			if (!(b is null) && b._matrix.Length == Length)
			{
				B = b._matrix;
				b._rows = a._rows;
				b._columns = a._columns;
			}
			else
			{
				b = new Matrix<T>(a._rows, a._columns, Length);
				B = b._matrix;
			}
			for (int i = 0; i < Length; i++)
			{
				B[i] = Negation(A[i]);
			}
		}

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="a">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negation.</returns>
		public static Matrix<T> Negate(Matrix<T> a)
		{
			Matrix<T> b = null;
			Negate(a, ref b);
			return b;
		}

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="a">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negation.</returns>
		public static Matrix<T> operator -(Matrix<T> a)
		{
			return Negate(a);
		}

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="b">The resulting matrix after the negation.</param>
		public void Negate(ref Matrix<T> b)
		{
			Negate(this, ref b);
		}

		/// <summary>Negates all the values in this matrix.</summary>
		/// <returns>The resulting matrix after the negation.</returns>
		public Matrix<T> Negate()
		{
			return -this;
		}

		#endregion

		#region Add

		/// <summary>Does standard addition of two matrices.</summary>
		/// <param name="a">The left matrix of the addition.</param>
		/// <param name="b">The right matrix of the addition.</param>
		/// <param name="c">The resulting matrix after the addition.</param>
		public static void Add(Matrix<T> a, Matrix<T> b, ref Matrix<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			if (a._rows != b._rows || a._columns != b._columns)
			{
				throw new MathematicsException("Arguments invalid !(" +
					nameof(a) + "." + nameof(a.Rows) + " == " + nameof(b) + "." + nameof(b.Rows) + " && " +
					nameof(a) + "." + nameof(a.Columns) + " == " + nameof(b) + "." + nameof(b.Columns) + ")");
			}
			int Length = a._matrix.Length;
			T[] A = a._matrix;
			T[] B = b._matrix;
			T[] C;
			if (!(c is null) && c._matrix.Length == Length)
			{
				C = c._matrix;
				c._rows = a._rows;
				c._columns = a._columns;
			}
			else
			{
				c = new Matrix<T>(a._rows, a._columns, Length);
				C = c._matrix;
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Addition(A[i], B[i]);
			}
		}

		/// <summary>Does standard addition of two matrices.</summary>
		/// <param name="a">The left matrix of the addition.</param>
		/// <param name="b">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public static Matrix<T> Add(Matrix<T> a, Matrix<T> b)
		{
			Matrix<T> c = null;
			Add(a, b, ref c);
			return c;
		}

		/// <summary>Does a standard matrix addition.</summary>
		/// <param name="a">The left matrix of the addition.</param>
		/// <param name="b">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after teh addition.</returns>
		public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
		{
			return Add(a, b);
		}

		/// <summary>Does standard addition of two matrices.</summary>
		/// <param name="b">The right matrix of the addition.</param>
		/// <param name="c">The resulting matrix after the addition.</param>
		public void Add(Matrix<T> b, ref Matrix<T> c)
		{
			Add(this, b, ref c);
		}

		/// <summary>Does a standard matrix addition.</summary>
		/// <param name="b">The matrix to add to this matrix.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public Matrix<T> Add(Matrix<T> b)
		{
			return this + b;
		}

		#endregion

		#region Subtract

		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="a">The left matrix of the subtraction.</param>
		/// <param name="b">The right matrix of the subtraction.</param>
		/// <param name="c">The resulting matrix after the subtraction.</param>
		public static void Subtract(Matrix<T> a, Matrix<T> b, ref Matrix<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			if (a._rows != b._rows || a._columns != b._columns)
			{
				throw new MathematicsException("Arguments invalid !(" +
					nameof(a) + "." + nameof(a.Rows) + " == " + nameof(b) + "." + nameof(b.Rows) + " && " +
					nameof(a) + "." + nameof(a.Columns) + " == " + nameof(b) + "." + nameof(b.Columns) + ")");
			}
			T[] A = a._matrix;
			T[] B = b._matrix;
			int Length = A.Length;
			T[] C;
			if (!(c is null) && c._matrix.Length == Length)
			{
				C = c._matrix;
				c._rows = a._rows;
				c._columns = a._columns;
			}
			else
			{
				c = new Matrix<T>(a._rows, a._columns, Length);
				C = c._matrix;
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Subtraction(A[i], B[i]);
			}
		}

		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="a">The left matrix of the subtraction.</param>
		/// <param name="b">The right matrix of the subtraction.</param>
		/// <returns>The resulting matrix after the subtraction.</returns>
		public static Matrix<T> Subtract(Matrix<T> a, Matrix<T> b)
		{
			Matrix<T> c = null;
			Subtract(a, b, ref c);
			return c;
		}

		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="a">The left matrix of the subtraction.</param>
		/// <param name="b">The right matrix of the subtraction.</param>
		/// <returns>The resulting matrix after the subtraction.</returns>
		public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
		{
			return Subtract(a, b);
		}

		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="b">The right matrix of the subtraction.</param>
		/// <param name="c">The resulting matrix after the subtraction.</param>
		public void Subtract(Matrix<T> b, ref Matrix<T> c)
		{
			Subtract(this, b, ref c);
		}

		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="b">The right matrix of the subtraction.</param>
		/// <returns>The resulting matrix after the subtraction.</returns>
		public Matrix<T> Subtract(Matrix<T> b)
		{
			return this - b;
		}

		#endregion

		#region Multiply (Matrix * Matrix)

		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <param name="c">The resulting matrix of the multiplication.</param>
		public static void Multiply(Matrix<T> a, Matrix<T> b, ref Matrix<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			if (a._columns != b._rows)
			{
				throw new MathematicsException("Arguments invalid !(" +
					nameof(a) + "." + nameof(a.Columns) + " == " + nameof(b) + "." + nameof(b.Rows) + ")");
			}
			if (object.ReferenceEquals(a, b) && object.ReferenceEquals(a, c))
			{
				Matrix<T> clone = a.Clone();
				a = clone;
				b = clone;
			}
			else if (object.ReferenceEquals(a, c))
			{
				a = a.Clone();
			}
			else if (object.ReferenceEquals(b, c))
			{
				b = b.Clone();
			}
			int c_Rows = a._rows;
			int a_Columns = a._columns;
			int c_Columns = b._columns;
			T[] A = a._matrix;
			T[] B = b._matrix;
			T[] C;
			if (!(c is null) && c._matrix.Length == c_Rows * c_Columns)
			{
				C = c._matrix;
				c._rows = c_Rows;
				c._columns = c_Columns;
			}
			else
			{
				c = new Matrix<T>(c_Rows, c_Columns);
				C = c._matrix;
			}
			for (int i = 0; i < c_Rows; i++)
			{
				int i_times_a_Columns = i * a_Columns;
				int i_times_c_Columns = i * c_Columns;
				for (int j = 0; j < c_Columns; j++)
				{
					T sum = Constant<T>.Zero;
					for (int k = 0; k < a_Columns; k++)
					{
						sum = MultiplyAddImplementation<T>.Function(A[i_times_a_Columns + k], B[k * c_Columns + j], sum);
					}
					C[i_times_c_Columns + j] = sum;
				}
			}
		}

		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public static Matrix<T> Multiply(Matrix<T> a, Matrix<T> b)
		{
			Matrix<T> c = null;
			Multiply(a, b, ref c);
			return c;
		}

		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
		{
			return Multiply(a, b);
		}

		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <param name="c">The resulting matrix of the multiplication.</param>
		public void Multiply(Matrix<T> b, ref Matrix<T> c)
		{
			Multiply(this, b, ref c);
		}

		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public Matrix<T> Multiply(Matrix<T> b)
		{
			return this * b;
		}

		#endregion

		#region Multiply (Matrix * Vector)

		/// <summary>Does a matrix-vector multiplication.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right vector of the multiplication.</param>
		/// <param name="c">The resulting vector of the multiplication.</param>
		public static void Multiply(Matrix<T> a, Vector<T> b, ref Vector<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			int rows = a._rows;
			int columns = a._columns;
			if (columns != b.Dimensions)
			{
				throw new MathematicsException("Arguments invalid !(" +
					nameof(a) + "." + nameof(a.Columns) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
			}
			T[] A = a._matrix;
			T[] B = b._vector;
			T[] C;
			if (!(c is null) && c.Dimensions == columns)
			{
				C = c._vector;
			}
			else
			{
				c = new Vector<T>(columns);
				C = c._vector;
			}
			for (int i = 0; i < rows; i++)
			{
				int i_times_columns = i * columns;
				T sum = Constant<T>.Zero;
				for (int j = 0; j < columns; j++)
				{
					sum = Addition(sum, Multiplication(A[i_times_columns + j], B[j]));
				}
				C[i] = sum;
			}
		}

		/// <summary>Does a matrix-vector multiplication.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right vector of the multiplication.</param>
		/// <returns>The resulting vector of the multiplication.</returns>
		public static Vector<T> Multiply(Matrix<T> a, Vector<T> b)
		{
			Vector<T> c = null;
			Multiply(a, b, ref c);
			return c;
		}

		/// <summary>Does a matrix-vector multiplication.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right vector of the multiplication.</param>
		/// <returns>The resulting vector of the multiplication.</returns>
		public static Vector<T> operator *(Matrix<T> a, Vector<T> b)
		{
			return Multiply(a, b);
		}

		/// <summary>Does a matrix-vector multiplication.</summary>
		/// <param name="b">The right vector of the multiplication.</param>
		/// <param name="c">The resulting vector of the multiplication.</param>
		public void Multiply(Vector<T> b, ref Vector<T> c)
		{
			Multiply(this, b, ref c);
		}

		/// <summary>Does a matrix-vector multiplication.</summary>
		/// <param name="b">The right vector of the multiplication.</param>
		/// <returns>The resulting vector of the multiplication.</returns>
		public Vector<T> Multiply(Vector<T> b)
		{
			return this * b;
		}

		#endregion

		#region Multiply (Matrix * Scalar)

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The matrix to have the values multiplied.</param>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <param name="c">The resulting matrix after the multiplications.</param>
		public static void Multiply(Matrix<T> a, T b, ref Matrix<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			T[] A = a._matrix;
			int Length = A.Length;
			T[] C;
			if (!(c is null) && c._matrix.Length == Length)
			{
				C = c._matrix;
				c._rows = a._rows;
				c._columns = a._columns;
			}
			else
			{
				c = new Matrix<T>(a._rows, a._columns, Length);
				C = c._matrix;
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Multiplication(A[i], b);
			}
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The matrix to have the values multiplied.</param>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> Multiply(Matrix<T> a, T b)
		{
			Matrix<T> c = null;
			Multiply(a, b, ref c);
			return c;
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <param name="a">The matrix to have the values multiplied.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> Multiply(T b, Matrix<T> a)
		{
			return Multiply(a, b);
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The matrix to have the values multiplied.</param>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> operator *(Matrix<T> a, T b)
		{
			return Multiply(a, b);
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <param name="a">The matrix to have the values multiplied.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> operator *(T b, Matrix<T> a)
		{
			return Multiply(b, a);
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <param name="c">The resulting matrix after the multiplications.</param>
		public void Multiply(T b, ref Matrix<T> c)
		{
			Multiply(this, b, ref c);
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public Matrix<T> Multiply(T b)
		{
			return this * b;
		}

		#endregion

		#region Divide (Matrix / Scalar)

		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="a">The matrix to divide the values of.</param>
		/// <param name="b">The scalar to divide all the matrix values by.</param>
		/// <param name="c">The resulting matrix after the division.</param>
		public static void Divide(Matrix<T> a, T b, ref Matrix<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			T[] A = a._matrix;
			int Length = A.Length;
			T[] C;
			if (!(c is null) && c._matrix.Length == Length)
			{
				C = c._matrix;
				c._rows = a._rows;
				c._columns = a._columns;
			}
			else
			{
				c = new Matrix<T>(a._rows, a._columns, Length);
				C = c._matrix;
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Division(A[i], b);
			}
		}


		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="a">The matrix to divide the values of.</param>
		/// <param name="b">The scalar to divide all the matrix values by.</param>
		/// <returns>The resulting matrix after the division.</returns>
		public static Matrix<T> Divide(Matrix<T> a, T b)
		{
			Matrix<T> c = null;
			Divide(a, b, ref c);
			return c;
		}

		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="a">The matrix to divide the values of.</param>
		/// <param name="b">The scalar to divide all the matrix values by.</param>
		/// <returns>The resulting matrix after the division.</returns>
		public static Matrix<T> operator /(Matrix<T> a, T b)
		{
			return Divide(a, b);
		}

		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="b">The scalar to divide all the matrix values by.</param>
		/// <param name="c">The resulting matrix after the division.</param>
		public void Divide(T b, ref Matrix<T> c)
		{
			Divide(this, b, ref c);
		}

		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="b">The scalar to divide all the matrix values by.</param>
		/// <returns>The resulting matrix after the division.</returns>
		public Matrix<T> Divide(T b)
		{
			return this / b;
		}

		#endregion

		#region Power (Matrix ^ Scalar)

		private class SquareMatrixFactory
		{
			private List<Matrix<T>> cache = new List<Matrix<T>>();
			private int pointer = 0;
			public readonly int DiagonalLength;
			public SquareMatrixFactory(int diagonalLength)
			{
				this.DiagonalLength = diagonalLength;
			}

			public Matrix<T> Get()
			{
				if (pointer == cache.Count)
					cache.Add(new Matrix<T>(DiagonalLength, DiagonalLength));
				var res = cache[pointer];
				pointer++;
				return res;
			}

			public void Return()
			{
				pointer--;
			}
		}

		[ThreadStatic]
		private static SquareMatrixFactory squareMatrixFactory;


		// Approach to
		// needed: a ^ (-13)
		// b = a ^ -1
		// needed: b ^ 13
		// mp2 = b ^ 6 (goto beginning)
		// needed: mp2 * mp2 * b
		// that's it, works for O(log(power))
		internal static void PowerPositiveSafe(Matrix<T> a, int power, ref Matrix<T> destination)
		{
			var dest = squareMatrixFactory.Get();
			Power(a, power / 2, ref dest);
			var mp2 = squareMatrixFactory.Get();
			Multiply(dest, dest, ref mp2);
			if (power % 2 == 1)
			{
				var tmp = squareMatrixFactory.Get();
				Multiply(mp2, dest, ref tmp);
				mp2 = tmp;
			}
			destination = mp2;
			squareMatrixFactory.Return();
			squareMatrixFactory.Return();
			squareMatrixFactory.Return();
		}

		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="a">The matrix to be powered by.</param>
		/// <param name="power">The power to apply to the matrix.</param>
		/// <param name="destination">The resulting matrix of the power operation.</param>
		public static void Power(Matrix<T> a, int power, ref Matrix<T> destination)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (!a.IsSquare)
			{
				throw new MathematicsException("Invalid power (!" + nameof(a) + ".IsSquare)");
			}
			if (power < 0)
            {
                Power(a.Inverse(), -power, ref destination);
                return;
            }
			if (power == 0)
			{
				if (!(destination is null) && destination.IsSquare)
				{
					destination._rows = a._rows;
					destination._columns = a._columns;
					Format(destination, (x, y) => x == y ? Constant<T>.One : Constant<T>.Zero);
				}
				else
				{
					destination = Matrix<T>.FactoryIdentity(a._rows, a._columns);
				}
				return;
			}
			if (!(destination is null) && destination._matrix.Length == a._matrix.Length)
			{
				destination._rows = a._rows;
				destination._columns = a._columns;
				T[] A = a._matrix;
				T[] C = destination._matrix;
				for (int i = 0; i < a._matrix.Length; i++)
				{
					C[i] = A[i];
				}
			}
			else
			{
				destination = a.Clone();
			}
            if (power == 1)
                return;
			if (squareMatrixFactory is null || squareMatrixFactory.DiagonalLength != a._rows)
                squareMatrixFactory = new SquareMatrixFactory(a._rows);
            PowerPositiveSafe(a, power, ref destination);
        }

		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="a">The matrix to be powered by.</param>
		/// <param name="power">The power to apply to the matrix.</param>
		/// <returns>The resulting matrix of the power operation.</returns>
		public static Matrix<T> Power(Matrix<T> a, int power)
		{
			Matrix<T> c = null;
			Power(a, power, ref c);
			return c;
		}

		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="a">The matrix to be powered by.</param>
		/// <param name="b">The power to apply to the matrix.</param>
		/// <returns>The resulting matrix of the power operation.</returns>
		public static Matrix<T> operator ^(Matrix<T> a, int b)
		{
			return Power(a, b);
		}

		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="b">The power to apply to the matrix.</param>
		/// <param name="c">The resulting matrix of the power operation.</param>
		public void Power(int b, ref Matrix<T> c)
		{
			Power(this, b, ref c);
		}

		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="b">The power to apply to the matrix.</param>
		/// <returns>The resulting matrix of the power operation.</returns>
		public Matrix<T> Power(int b)
		{
			return this ^ b;
		}

		#endregion

		#region Determinant

		/// <summary>Computes the determinant of a square matrix.</summary>
		/// <param name="a">The matrix to compute the determinant of.</param>
		/// <returns>The computed determinant.</returns>
		public static T Determinant(Matrix<T> a)
		{
			return DeterminantGaussian(a);

			#region Old Version

			//if (a is null)
			//{
			//	throw new ArgumentNullException(nameof(a));
			//}
			//if (!a.IsSquare)
			//{
			//	throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			//}
			//T determinant = Constant<T>.One;
			//Matrix<T> rref = a.Clone();
			//int a_rows = a._rows;
			//for (int i = 0; i < a_rows; i++)
			//{
			//	if (Compute.Equal(rref[i, i], Constant<T>.Zero))
			//	{
			//		for (int j = i + 1; j < rref.Rows; j++)
			//		{
			//			if (Compute.NotEqual(rref.Get(j, i), Constant<T>.Zero))
			//			{
			//				SwapRows(rref, i, j);
			//				determinant = Compute.Multiply(determinant, Constant<T>.NegativeOne);
			//			}
			//		}
			//	}
			//	determinant = Compute.Multiply(determinant, rref.Get(i, i));
			//	T temp_rowMultiplication = Compute.Divide(Constant<T>.One, rref.Get(i, i));
			//	RowMultiplication(rref, i, temp_rowMultiplication);
			//	for (int j = i + 1; j < rref.Rows; j++)
			//	{
			//		T scalar = Compute.Negate(rref.Get(j, i));
			//		RowAddition(rref, j, i, scalar);

			//	}
			//	for (int j = i - 1; j >= 0; j--)
			//	{
			//		T scalar = Compute.Negate(rref.Get(j, i));
			//		RowAddition(rref, j, i, scalar);

			//	}
			//}
			//return determinant;

			#endregion

		}

		/// <summary>Computes the determinant of a square matrix via Gaussian elimination.</summary>
		/// <param name="a">The matrix to compute the determinant of.</param>
		/// <returns>The computed determinant.</returns>
		/// <runtime>O((n^3 + 2n^−3) / 3)</runtime>
		public static T DeterminantGaussian(Matrix<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (!a.IsSquare)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			}
			return GetDeterminantGaussian(a, a.Rows);
		}

		/// <summary>Computes the determinant of a square matrix via Laplace's method.</summary>
		/// <param name="a">The matrix to compute the determinant of.</param>
		/// <returns>The computed determinant.</returns>
		/// <runtime>O(n(2^(n − 1) − 1))</runtime>
		public static T DeterminantLaplace(Matrix<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (!a.IsSquare)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			}
			return GetDeterminantLaplace(a, a.Rows);
		}

		/// <summary>Computes the determinant of a square matrix.</summary>
		/// <returns>The computed determinant.</returns>
		public T Determinant()
		{
			return DeterminantGaussian(this);
		}

		/// <summary>Computes the determinant of a square matrix via Laplace's method.</summary>
		/// <returns>The computed determinant.</returns>
		public T DeterminantLaplace()
		{
			return DeterminantLaplace(this);
		}

		/// <summary>Computes the determinant of a square matrix via Gaussian elimination.</summary>
		/// <returns>The computed determinant.</returns>
		public T DeterminantGaussian()
		{
			return DeterminantGaussian(this);
		}

		#endregion

		#region Trace

		/// <summary>Computes the trace of a square matrix.</summary>
		/// <param name="a">The matrix to compute the trace of.</param>
		/// <returns>The computed trace.</returns>
		public static T Trace(Matrix<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (!a.IsSquare)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			}
			T trace = Addition((Step<T> step) =>
			{
				T[] A = a._matrix;
				int rows = a.Rows;
				for (int i = 0; i < rows; i++)
				{
					step(A[i * rows + i]);
				}
			});
			return trace;
		}

		/// <summary>Computes the trace of a square matrix.</summary>
		/// <returns>The computed trace.</returns>
		public T Trace()
		{
			return Trace(this);
		}

		#endregion

		#region Minor

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Gets the minor of a matrix.</summary>
		/// <param name="a">The matrix to get the minor of.</param>
		/// <param name="row">The restricted row to form the minor.</param>
		/// <param name="column">The restricted column to form the minor.</param>
		/// <param name="b">The minor of the matrix.</param>
		/// <returns>The minor of the matrix.</returns>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Minor_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Minor_XML"/>
		public Matrix<T> Minor(int row, int column) =>
			Minor(this, row, column);

		/// <inheritdoc cref="Minor_XML"/>
		public static Matrix<T> Minor(Matrix<T> a, int row, int column)
		{
			Matrix<T> b = null;
			Minor(a, row, column, ref b);
			return b;
		}

		/// <inheritdoc cref="Minor_XML"/>
		public void Minor(int row, int column, ref Matrix<T> b) =>
			Minor(this, row, column, ref b);

		/// <inheritdoc cref="Minor_XML"/>
		public static void Minor(Matrix<T> a, int row, int column, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (a._rows < 2 || a._columns < 2)
			{
				throw new MathematicsException($"{nameof(a)}.{nameof(a.Rows)} < 2 || {nameof(a)}.{nameof(a.Columns)} < 2");
			}
			if (!(0 <= row && row < a._rows))
			{
				throw new ArgumentOutOfRangeException(nameof(row), row, $"!(0 <= {nameof(row)} < {nameof(a)}.{nameof(a.Rows)})");
			}
			if (!(0 <= column && column < a._columns))
			{
				throw new ArgumentOutOfRangeException(nameof(column), column, $"!(0 <= {nameof(column)} < {nameof(a)}.{nameof(a.Columns)})");
			}
			if (ReferenceEquals(a, b))
			{
				a = a.Clone();
			}
			int a_rows = a._rows;
			int a_columns = a._columns;
			int b_rows = a_rows - 1;
			int b_columns = a_columns - 1;
			int b_length = b_rows * b_columns;
			if (b is null || b._matrix.Length != b_length)
			{
				b = new Matrix<T>(b_rows, b_columns, b_length);
			}
			else
			{
				b._rows = b_rows;
				b._columns = b_columns;
			}
			T[] B = b._matrix;
			T[] A = a._matrix;
			for (int i = 0, m = 0; i < a_rows; i++)
			{
				if (i == row)
				{
					continue;
				}
				int i_times_a_columns = i * a_columns;
				int m_times_b_columns = m * b_columns;
				for (int j = 0, n = 0; j < a_columns; j++)
				{
					if (j == column)
					{
						continue;
					}
					T temp = A[i_times_a_columns + j];
					B[m_times_b_columns + n] = temp;
					n++;
				}
				m++;
			}
		}

		#endregion

		#region ConcatenateRowWise

		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows AND result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="a">The left matrix of the concatenation.</param>
		/// <param name="b">The right matrix of the concatenation.</param>
		/// <param name="c">The resulting matrix of the concatenation.</param>
		public static void ConcatenateRowWise(Matrix<T> a, Matrix<T> b, ref Matrix<T> c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			if (a._rows != b._rows)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Rows) + " == " + nameof(b) + "." + nameof(b.Rows) + ")");
			}
			int a_columns = a._columns;
			int b_columns = b._columns;
			int c_rows = a._rows;
			int c_columns = a._columns + b._columns;
			int c_length = c_rows * c_columns;
			if (c is null ||
				c._matrix.Length != c_length ||
				object.ReferenceEquals(a, c) ||
				object.ReferenceEquals(b, c))
			{
				c = new Matrix<T>(c_rows, c_columns, c_length);
			}
			else
			{
				c._rows = c_rows;
				c._columns = c_columns;
			}
			T[] A = a._matrix;
			T[] B = b._matrix;
			T[] C = c._matrix;
			for (int i = 0; i < c_rows; i++)
			{
				int i_times_a_columns = i * a_columns;
				int i_times_b_columns = i * b_columns;
				int i_times_c_columns = i * c_columns;
				for (int j = 0; j < c_columns; j++)
				{
					if (j < a_columns)
					{
						C[i_times_c_columns + j] = A[i_times_a_columns + j];
					}
					else
					{
						C[i_times_c_columns + j] = B[i_times_b_columns + j - a_columns];
					}
				}
			}
		}

		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows AND result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="a">The left matrix of the concatenation.</param>
		/// <param name="b">The right matrix of the concatenation.</param>
		/// <returns>The resulting matrix of the concatenation.</returns>
		public static Matrix<T> ConcatenateRowWise(Matrix<T> a, Matrix<T> b)
		{
			Matrix<T> c = null;
			ConcatenateRowWise(a, b, ref c);
			return c;
		}

		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows AND result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="b">The right matrix of the concatenation.</param>
		/// <param name="c">The resulting matrix of the concatenation.</param>
		public void ConcatenateRowWise(Matrix<T> b, ref Matrix<T> c)
		{
			ConcatenateRowWise(this, b, ref c);
		}

		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows AND result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="b">The right matrix of the concatenation.</param>
		/// <returns>The resulting matrix of the concatenation.</returns>
		public Matrix<T> ConcatenateRowWise(Matrix<T> b)
		{
			return ConcatenateRowWise(this, b);
		}

		#endregion

		#region Echelon

#if false

		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <param name="a">The matrix to calculate the echelon of (aka REF).</param>
		/// <param name="b">The echelon of the matrix (aka REF).</param>
		/// <bug>Failing for non-floating point rational types due to zero how values are being compared.</bug>
		public static void Echelon(Matrix<T> a, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (ReferenceEquals(a, b))
			{
				a = a.Clone();
			}
			int Rows = a.Rows;
			if (!(b is null) && b._matrix.Length == a._matrix.Length)
			{
				b._rows = Rows;
				b._columns = a._columns;
				CloneContents(a, b);
			}
			else
			{
				b = a.Clone();
			}
			for (int i = 0; i < Rows; i++)
			{
				if (Equality(b.Get(i, i), Constant<T>.Zero))
				{
					for (int j = i + 1; j < Rows; j++)
					{
						if (Inequality(b.Get(j, i), Constant<T>.Zero))
						{
							SwapRows(b, i, j);
						}
					}
				}
				if (Equality(b.Get(i, i), Constant<T>.Zero))
				{
					continue;
				}
				if (Inequality(b.Get(i, i), Constant<T>.One))
				{
					for (int j = i + 1; j < Rows; j++)
					{
						if (Equality(b.Get(j, i), Constant<T>.One))
						{
							SwapRows(b, i, j);
						}
					}
				}
				T rowMultipier = Division(Constant<T>.One, b.Get(i, i));
				RowMultiplication(b, i, rowMultipier);
				for (int j = i + 1; j < Rows; j++)
				{
					T rowAddend = Negation(b.Get(j, i));
					RowAddition(b, j, i, rowAddend);
				}
			}
		}

		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <param name="a">The matrix to calculate the echelon of (aka REF).</param>
		/// <returns>The echelon of the matrix (aka REF).</returns>
		public static Matrix<T> Echelon(Matrix<T> a)
		{
			Matrix<T> b = null;
			Echelon(a, ref b);
			return b;
		}

		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <param name="b">The echelon of the matrix (aka REF).</param>
		public void Echelon(ref Matrix<T> b)
		{
			Echelon(this, ref b);
		}

		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <returns>The echelon of the matrix (aka REF).</returns>
		public Matrix<T> Echelon()
		{
			return Echelon(this);
		}

#endif

		#endregion

		#region ReducedEchelon

		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		/// <param name="a">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
		/// <param name="b">The reduced echelon of the matrix (aka RREF).</param>
		public static void ReducedEchelon(Matrix<T> a, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			int Rows = a.Rows;
			int Columns = a.Columns;
			if (object.ReferenceEquals(a, b))
			{
				b = a.Clone();
			}
			else if (!(b is null) && b._matrix.Length == a._matrix.Length)
			{
				b._rows = Rows;
				b._columns = a._columns;
				CloneContents(a, b);
			}
			else
			{
				b = a.Clone();
			}
			int lead = 0;
			for (int r = 0; r < Rows; r++)
			{
				if (Columns <= lead) break;
				int i = r;
				while (EqualTo(b.Get(i, lead), Constant<T>.Zero))
				{
					i++;
					if (i == Rows)
					{
						i = r;
						lead++;
						if (Columns == lead)
						{
							lead--;
							break;
						}
					}
				}
				for (int j = 0; j < Columns; j++)
				{
					T temp = b.Get(r, j);
					b.Set(r, j, b.Get(i, j));
					b.Set(i, j, temp);
				}
				T div = b.Get(r, lead);
				if (InequalTo(div, Constant<T>.Zero))
				{
					for (int j = 0; j < Columns; j++)
					{
						b.Set(r, j, Division(b.Get(r, j), div));
					}
				}
				for (int j = 0; j < Rows; j++)
				{
					if (j != r)
					{
						T sub = b.Get(j, lead);
						for (int k = 0; k < Columns; k++)
						{
							b.Set(j, k, Subtraction(b.Get(j, k), Multiplication(sub, b.Get(r, k))));
						}
					}
				}
				lead++;
			}

			#region Old Version

			//if (a is null)
			//{
			//    throw new ArgumentNullException(nameof(a));
			//}
			//if (object.ReferenceEquals(a, b))
			//{
			//    a = a.Clone();
			//}
			//int Rows = a.Rows;
			//if (!(b is null) && b._matrix.Length == a._matrix.Length)
			//{
			//    b._rows = Rows;
			//    b._columns = a._columns;
			//    CloneContents(a, b);
			//}
			//else
			//{
			//    b = a.Clone();
			//}
			//for (int i = 0; i < Rows; i++)
			//{
			//    if (Compute.Equal(b.Get(i, i), Constant<T>.Zero))
			//    {
			//        for (int j = i + 1; j < Rows; j++)
			//        {
			//            if (Compute.NotEqual(b.Get(j, i), Constant<T>.Zero))
			//            {
			//                SwapRows(b, i, j);
			//            }
			//        }
			//    }
			//    if (Compute.Equal(b.Get(i, i), Constant<T>.Zero))
			//    {
			//        continue;
			//    }
			//    if (Compute.NotEqual(b.Get(i, i), Constant<T>.One))
			//    {
			//        for (int j = i + 1; j < Rows; j++)
			//        {
			//            if (Compute.Equal(b.Get(j, i), Constant<T>.One))
			//            {
			//                SwapRows(b, i, j);
			//            }
			//        }
			//    }
			//    T rowMiltiplier = Compute.Divide(Constant<T>.One, b.Get(i, i));
			//    RowMultiplication(b, i, rowMiltiplier);
			//    for (int j = i + 1; j < Rows; j++)
			//    {
			//        T rowAddend = Compute.Negate(b.Get(j, i));
			//        RowAddition(b, j, i, rowAddend);
			//    }
			//    for (int j = i - 1; j >= 0; j--)
			//    {
			//        T rowAddend = Compute.Negate(b.Get(j, i));
			//        RowAddition(b, j, i, rowAddend);
			//    }
			//}

			#endregion
		}

		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		/// <param name="a">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
		/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
		public static Matrix<T> ReducedEchelon(Matrix<T> a)
		{
			Matrix<T> b = null;
			ReducedEchelon(a, ref b);
			return b;
		}

		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		/// <param name="b">The reduced echelon of the matrix (aka RREF).</param>
		public void ReducedEchelon(ref Matrix<T> b)
		{
			ReducedEchelon(this, ref b);
		}

		/// <summary>Matrixs the reduced echelon form of this matrix (aka RREF).</summary>
		/// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
		public Matrix<T> ReducedEchelon()
		{
			return ReducedEchelon(this);
		}

		#endregion

		#region Inverse

		/// <summary>Calculates the inverse of a matrix.</summary>
		/// <param name="a">The matrix to calculate the inverse of.</param>
		/// <param name="b">The inverse of the matrix.</param>
		public static void Inverse(Matrix<T> a, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (!a.IsSquare)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			}
			T determinant = Determinant(a);
			if (EqualTo(determinant, Constant<T>.Zero))
			{
				throw new MathematicsException("Singular matrix encountered during inverse caluculation (cannot be inversed).");
			}
			Matrix<T> adjoint = a.Adjoint();
			int dimension = a.Rows;
			int Length = a.Length;
			if (object.ReferenceEquals(a, b))
			{
				b = a.Clone();
			}
			else if (!(b is null) && b.Length == Length)
			{
				b._rows = dimension;
				b._columns = dimension;
			}
			else
			{
				b = new Matrix<T>(dimension, dimension, Length);
			}
			for (int i = 0; i < dimension; i++)
			{
				for (int j = 0; j < dimension; j++)
				{
					b.Set(i, j, Division(adjoint.Get(i, j), determinant));
				}
			}

			#region Old Version

			//// Note: this method can be optimized...

			//if (a is null)
			//{
			//    throw new ArgumentNullException(nameof(a));
			//}
			//if (Compute.Equal(Determinant(a), Constant<T>.Zero))
			//{
			//    throw new MathematicsException("inverse calculation failed.");
			//}
			//Matrix<T> identity = FactoryIdentity(a.Rows, a.Columns);
			//Matrix<T> rref = a.Clone();
			//for (int i = 0; i < a.Rows; i++)
			//{
			//    if (Compute.Equal(rref[i, i], Constant<T>.Zero))
			//    {
			//        for (int j = i + 1; j < rref.Rows; j++)
			//        {
			//            if (Compute.NotEqual(rref[j, i], Constant<T>.Zero))
			//            {
			//                SwapRows(rref, i, j);
			//                SwapRows(identity, i, j);
			//            }
			//        }
			//    }
			//    T identityRowMultiplier = Compute.Divide(Constant<T>.One, rref[i, i]);
			//    RowMultiplication(identity, i, identityRowMultiplier);
			//    RowMultiplication(rref, i, identityRowMultiplier);
			//    for (int j = i + 1; j < rref.Rows; j++)
			//    {
			//        T rowAdder = Compute.Negate(rref[j, i]);
			//        RowAddition(identity, j, i, rowAdder);
			//        RowAddition(rref, j, i, rowAdder);
			//    }
			//    for (int j = i - 1; j >= 0; j--)
			//    {
			//        T rowAdder = Compute.Negate(rref[j, i]);
			//        RowAddition(identity, j, i, rowAdder);
			//        RowAddition(rref, j, i, rowAdder);
			//    }
			//}
			//b = identity;

			#endregion

			#region Alternate Version
			//Matrix<T> identity = Matrix<T>.FactoryIdentity(matrix.Rows, matrix.Columns);
			//Matrix<T> rref = matrix.Clone();
			//for (int i = 0; i < matrix.Rows; i++)
			//{
			//	if (Compute.Equate(rref[i, i], Compute.FromInt32(0)))
			//		for (int j = i + 1; j < rref.Rows; j++)
			//			if (!Compute.Equate(rref[j, i], Compute.FromInt32(0)))
			//			{
			//				Matrix<T>.SwapRows(rref, i, j);
			//				Matrix<T>.SwapRows(identity, i, j);
			//			}
			//	Matrix<T>.RowMultiplication(identity, i, Compute.Divide(Compute.FromInt32(1), rref[i, i]));
			//	Matrix<T>.RowMultiplication(rref, i, Compute.Divide(Compute.FromInt32(1), rref[i, i]));
			//	for (int j = i + 1; j < rref.Rows; j++)
			//	{
			//		Matrix<T>.RowAddition(identity, j, i, Compute.Negate(rref[j, i]));
			//		Matrix<T>.RowAddition(rref, j, i, Compute.Negate(rref[j, i]));
			//	}
			//	for (int j = i - 1; j >= 0; j--)
			//	{
			//		Matrix<T>.RowAddition(identity, j, i, Compute.Negate(rref[j, i]));
			//		Matrix<T>.RowAddition(rref, j, i, Compute.Negate(rref[j, i]));
			//	}
			//}
			//return identity;
			#endregion
		}

		/// <summary>Calculates the inverse of a matrix.</summary>
		/// <param name="a">The matrix to calculate the inverse of.</param>
		/// <returns>The inverse of the matrix.</returns>
		public static Matrix<T> Inverse(Matrix<T> a)
		{
			Matrix<T> b = null;
			Inverse(a, ref b);
			return b;
		}

		/// <summary>Matrixs the inverse of this matrix.</summary>
		/// <returns>The inverse of this matrix.</returns>
		public Matrix<T> Inverse()
		{
			return Inverse(this);
		}

		#endregion

		#region Adjoint

		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <param name="a">The matrix to calculate the adjoint of.</param>
		/// <param name="b">The adjoint of the matrix.</param>
		public static void Adjoint(Matrix<T> a, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (!a.IsSquare)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			}
			int dimension = a.Rows;
			int Length = a.Length;
			if (object.ReferenceEquals(a, b))
			{
				b = a.Clone();
			}
			else if (!(b is null) && b.Length == Length)
			{
				b._rows = dimension;
				b._columns = dimension;
			}
			else
			{
				b = new Matrix<T>(dimension, dimension, Length);
			}
			if (dimension == 1)
			{
				b.Set(0, 0, Constant<T>.One);
				return;
			}
			Matrix<T> temp = new Matrix<T>(dimension, dimension, Length);
			for (int i = 0; i < dimension; i++)
			{
				for (int j = 0; j < dimension; j++)
				{
					GetCofactor(a, temp, i, j, dimension);
					T sign = (i + j) % 2 == 0 ? Constant<T>.One : Constant<T>.NegativeOne;
					b.Set(j, i, Multiplication(sign, GetDeterminantGaussian(temp, dimension - 1)));
				}
			}

			#region Old Version

			//if (a is null)
			//{
			//    throw new ArgumentNullException(nameof(a));
			//}
			//if (!a.IsSquare)
			//{
			//    throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
			//}
			//if (object.ReferenceEquals(a, b))
			//{
			//    a = a.Clone();
			//}
			//int Length = a.Length;
			//int Rows = a.Rows;
			//int Columns = a.Columns;
			//if (!(b is null) && b.Length == Length)
			//{
			//    b._rows = Rows;
			//    b._columns = Columns;
			//}
			//else
			//{
			//    b = new Matrix<T>(Rows, Columns, Length);
			//}
			//for (int i = 0; i < Rows; i++)
			//{
			//    for (int j = 0; j < Columns; j++)
			//    {
			//        if (Compute.IsEven(a.Get(i, j)))
			//        {
			//            b[i, j] = Determinant(Minor(a, i, j));
			//        }
			//        else
			//        {
			//            b[i, j] = Compute.Negate(Determinant(Minor(a, i, j)));
			//        }
			//    }
			//}

			#endregion
		}

		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <param name="a">The matrix to calculate the adjoint of.</param>
		/// <returns>The adjoint of the matrix.</returns>
		public static Matrix<T> Adjoint(Matrix<T> a)
		{
			Matrix<T> b = null;
			Adjoint(a, ref b);
			return b;
		}

		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <param name="b">The adjoint of the matrix.</param>
		public void Adjoint(ref Matrix<T> b)
		{
			Adjoint(this, ref b);
		}

		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <returns>The adjoint of the matrix.</returns>
		public Matrix<T> Adjoint()
		{
			return Adjoint(this);
		}

		#endregion

		#region Transpose

		/// <summary>Returns the transpose of a matrix.</summary>
		/// <param name="a">The matrix to transpose.</param>
		/// <param name="b">The transpose of the matrix.</param>
		public static void Transpose(Matrix<T> a, ref Matrix<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			if (ReferenceEquals(a, b))
			{
				if (b.IsSquare)
				{
					TransposeContents(b);
					return;
				}
			}
			int Length = a.Length;
			int Rows = a.Columns;
			int Columns = a.Rows;
			if (!(b is null) && b.Length == a.Length && !ReferenceEquals(a, b))
			{
				b._rows = Rows;
				b._columns = Columns;
			}
			else
			{
				b = new Matrix<T>(Rows, Columns, Length);
			}
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					b.Set(i, j, a.Get(j, i));
				}
			}
		}

		/// <summary>Returns the transpose of a matrix.</summary>
		/// <param name="a">The matrix to transpose.</param>
		/// <returns>The transpose of the matrix.</returns>
		public static Matrix<T> Transpose(Matrix<T> a)
		{
			Matrix<T> b = null;
			Transpose(a, ref b);
			return b;
		}

		/// <summary>Returns the transpose of a matrix.</summary>
		/// <param name="b">The transpose of the matrix.</param>
		public void Transpose(ref Matrix<T> b)
		{
			Transpose(this, ref b);
		}

		/// <summary>Returns the transpose of a matrix.</summary>
		/// <returns>The transpose of the matrix.</returns>
		public Matrix<T> Transpose()
		{
			return Transpose(this);
		}

		#endregion

		#region DecomposeLowerUpper

		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="matrix">The matrix to decompose.</param>
		/// <param name="lower">The computed lower triangular matrix.</param>
		/// <param name="upper">The computed upper triangular matrix.</param>
		public static void DecomposeLowerUpper(Matrix<T> matrix, ref Matrix<T> lower, ref Matrix<T> upper)
		{
			// Note: this method can be optimized...

			lower = Matrix<T>.FactoryIdentity(matrix.Rows, matrix.Columns);
			upper = matrix.Clone();
			int[] permutation = new int[matrix.Rows];
			for (int i = 0; i < matrix.Rows; i++)
			{
				permutation[i] = i;
			}
			T pom2, detOfP = Constant<T>.One;
			int k0 = 0;
			for (int k = 0; k < matrix.Columns - 1; k++)
			{
				T p = Constant<T>.Zero;
				for (int i = k; i < matrix.Rows; i++)
				{
					if (GreaterThan(GreaterThan(upper[i, k], Constant<T>.Zero) ? upper[i, k] : Negation(upper[i, k]), p))
					{
						p = GreaterThan(upper[i, k], Constant<T>.Zero) ? upper[i, k] : Negation(upper[i, k]);
						k0 = i;
					}
				}
				if (EqualTo(p, Constant<T>.Zero))
				{
					throw new MathematicsException("The matrix is singular!");
				}
				int pom1 = permutation[k];
				permutation[k] = permutation[k0];
				permutation[k0] = pom1;
				for (int i = 0; i < k; i++)
				{
					pom2 = lower[k, i];
					lower[k, i] = lower[k0, i];
					lower[k0, i] = pom2;
				}
				if (k != k0)
				{
					detOfP = Multiplication(detOfP, Constant<T>.NegativeOne);
				}
				for (int i = 0; i < matrix.Columns; i++)
				{
					pom2 = upper[k, i];
					upper[k, i] = upper[k0, i];
					upper[k0, i] = pom2;
				}
				for (int i = k + 1; i < matrix.Rows; i++)
				{
					lower[i, k] = Division(upper[i, k], upper[k, k]);
					for (int j = k; j < matrix.Columns; j++)
					{
						upper[i, j] = Subtraction(upper[i, j], Multiplication(lower[i, k], upper[k, j]));
					}
				}
			}

			#region Alternate Version
			//lower = Matrix<T>.FactoryIdentity(matrix.Rows, matrix.Columns);
			//upper = matrix.Clone();
			//int[] permutation = new int[matrix.Rows];
			//for (int i = 0; i < matrix.Rows; i++) permutation[i] = i;
			//T p = 0, pom2, detOfP = 1;
			//int k0 = 0, pom1 = 0;
			//for (int k = 0; k < matrix.Columns - 1; k++)
			//{
			//	p = 0;
			//	for (int i = k; i < matrix.Rows; i++)
			//		if ((upper[i, k] > 0 ? upper[i, k] : -upper[i, k]) > p)
			//		{
			//			p = upper[i, k] > 0 ? upper[i, k] : -upper[i, k];
			//			k0 = i;
			//		}
			//	if (p == 0)
			//		throw new System.Exception("The matrix is singular!");
			//	pom1 = permutation[k];
			//	permutation[k] = permutation[k0];
			//	permutation[k0] = pom1;
			//	for (int i = 0; i < k; i++)
			//	{
			//		pom2 = lower[k, i];
			//		lower[k, i] = lower[k0, i];
			//		lower[k0, i] = pom2;
			//	}
			//	if (k != k0)
			//		detOfP *= -1;
			//	for (int i = 0; i < matrix.Columns; i++)
			//	{
			//		pom2 = upper[k, i];
			//		upper[k, i] = upper[k0, i];
			//		upper[k0, i] = pom2;
			//	}
			//	for (int i = k + 1; i < matrix.Rows; i++)
			//	{
			//		lower[i, k] = upper[i, k] / upper[k, k];
			//		for (int j = k; j < matrix.Columns; j++)
			//			upper[i, j] = upper[i, j] - lower[i, k] * upper[k, j];
			//	}
			//}
			#endregion
		}

		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="lower">The computed lower triangular matrix.</param>
		/// <param name="upper">The computed upper triangular matrix.</param>
		public void DecomposeLowerUpper(ref Matrix<T> lower, ref Matrix<T> upper)
		{
			DecomposeLowerUpper(this, ref lower, ref upper);
		}

		#endregion

		#region Rotate

		/// <summary>Rotates a 4x4 matrix around an 3D axis by a specified angle.</summary>
		/// /// <param name="matrix">The 4x4 matrix to rotate.</param>
		/// <param name="angle">The angle of rotation around the axis.</param>
		/// <param name="axis">The 3D axis to rotate the matrix around.</param>
		/// <returns>The rotated matrix.</returns>
		public static Matrix<T> Rotate4x4(Matrix<T> matrix, Angle<T> angle, Vector<T> axis)
		{
			_ = axis ?? throw new ArgumentNullException(nameof(axis));
			_ = matrix ?? throw new ArgumentNullException(nameof(matrix));
			if (axis._vector.Length != 3)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(axis) + "." + nameof(axis.Dimensions) + " == 3)");
			}
			if (matrix._rows != 4 || matrix._columns != 4)
			{
				throw new MathematicsException("Argument invalid !(" + nameof(matrix) + "." + nameof(matrix.Rows) + " == 4 && " + nameof(matrix) + "." + nameof(matrix.Columns) + " == 4)");
			}

			// if the angle is zero, no rotation is required
			if (EqualTo(angle._measurement, Constant<T>.Zero))
			{
				return matrix.Clone();
			}

			T cosine = CosineSystem(angle);
			T sine = SineSystem(angle);
			T oneMinusCosine = Subtraction(Constant<T>.One, cosine);
			T xy = Multiplication(axis.X, axis.Y);
			T yz = Multiplication(axis.Y, axis.Z);
			T xz = Multiplication(axis.X, axis.Z);
			T xs = Multiplication(axis.X, sine);
			T ys = Multiplication(axis.Y, sine);
			T zs = Multiplication(axis.Z, sine);

			T f00 = Addition(Multiplication(Multiplication(axis.X, axis.X), oneMinusCosine), cosine);
			T f01 = Addition(Multiplication(xy, oneMinusCosine), zs);
			T f02 = Subtraction(Multiplication(xz, oneMinusCosine), ys);
			// n[3] not used
			T f10 = Subtraction(Multiplication(xy, oneMinusCosine), zs);
			T f11 = Addition(Multiplication(Multiplication(axis.Y, axis.Y), oneMinusCosine), cosine);
			T f12 = Addition(Multiplication(yz, oneMinusCosine), xs);
			// n[7] not used
			T f20 = Addition(Multiplication(xz, oneMinusCosine), ys);
			T f21 = Subtraction(Multiplication(yz, oneMinusCosine), xs);
			T f22 = Addition(Multiplication(Multiplication(axis.Z, axis.Z), oneMinusCosine), cosine);

			// Row 1
			T _0_0 = Addition(Addition(Multiplication(matrix[0, 0], f00), Multiplication(matrix[1, 0], f01)), Multiplication(matrix[2, 0], f02));
			T _0_1 = Addition(Addition(Multiplication(matrix[0, 1], f00), Multiplication(matrix[1, 1], f01)), Multiplication(matrix[2, 1], f02));
			T _0_2 = Addition(Addition(Multiplication(matrix[0, 2], f00), Multiplication(matrix[1, 2], f01)), Multiplication(matrix[2, 2], f02));
			T _0_3 = Addition(Addition(Multiplication(matrix[0, 3], f00), Multiplication(matrix[1, 3], f01)), Multiplication(matrix[2, 3], f02));
			// Row 2
			T _1_0 = Addition(Addition(Multiplication(matrix[0, 0], f10), Multiplication(matrix[1, 0], f11)), Multiplication(matrix[2, 0], f12));
			T _1_1 = Addition(Addition(Multiplication(matrix[0, 1], f10), Multiplication(matrix[1, 1], f11)), Multiplication(matrix[2, 1], f12));
			T _1_2 = Addition(Addition(Multiplication(matrix[0, 2], f10), Multiplication(matrix[1, 2], f11)), Multiplication(matrix[2, 2], f12));
			T _1_3 = Addition(Addition(Multiplication(matrix[0, 3], f10), Multiplication(matrix[1, 3], f11)), Multiplication(matrix[2, 3], f12));
			// Row 3
			T _2_0 = Addition(Addition(Multiplication(matrix[0, 0], f20), Multiplication(matrix[1, 0], f21)), Multiplication(matrix[2, 0], f22));
			T _2_1 = Addition(Addition(Multiplication(matrix[0, 1], f20), Multiplication(matrix[1, 1], f21)), Multiplication(matrix[2, 1], f22));
			T _2_2 = Addition(Addition(Multiplication(matrix[0, 2], f20), Multiplication(matrix[1, 2], f21)), Multiplication(matrix[2, 2], f22));
			T _2_3 = Addition(Addition(Multiplication(matrix[0, 3], f20), Multiplication(matrix[1, 3], f21)), Multiplication(matrix[2, 3], f22));
			// Row 4
			T _3_0 = Constant<T>.Zero;
			T _3_1 = Constant<T>.Zero;
			T _3_2 = Constant<T>.Zero;
			T _3_3 = Constant<T>.One;

			return new Matrix<T>(4, 4, (row, column) =>
				(row, column) switch
				{
					(0, 0) => _0_0,
					(0, 1) => _0_1,
					(0, 2) => _0_2,
					(0, 3) => _0_3,
					(1, 0) => _1_0,
					(1, 1) => _1_1,
					(1, 2) => _1_2,
					(1, 3) => _1_3,
					(2, 0) => _2_0,
					(2, 1) => _2_1,
					(2, 2) => _2_2,
					(2, 3) => _2_3,
					(3, 0) => _3_0,
					(3, 1) => _3_1,
					(3, 2) => _3_2,
					(3, 3) => _3_3,
					_ => throw new MathematicsException("BUG"),
				});
		}

		/// <summary>Converts an angle around an axis into a 4x4 rotational matrix.</summary>
		/// <param name="angle">The angle of the axis rotation.</param>
		/// <param name="axis">The axis of the axis rotation.</param>
		/// <returns>The rotation expressed as a 4x4 matrix.</returns>
		public Matrix<T> Rotate4x4(Angle<T> angle, Vector<T> axis) => Rotate4x4(this, angle, axis);

		#endregion

		#region Equal

		/// <summary>Does a value equality check.</summary>
		/// <param name="a">The first matrix to check for equality.</param>
		/// <param name="b">The second matrix to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool Equal(Matrix<T> a, Matrix<T> b)
		{
			if (a is null)
			{
				if (b is null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			if (b is null)
			{
				return false;
			}
			int Rows = a.Rows;
			int Columns = a.Columns;
			if (Rows != b.Rows || Columns != b.Columns)
			{
				return false;
			}
			T[] A = a._matrix;
			T[] B = b._matrix;
			int Length = A.Length;
			for (int i = 0; i < Length; i++)
			{
				if (!EqualTo(A[i], B[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Does a value equality check.</summary>
		/// <param name="a">The first matrix to check for equality.</param>
		/// <param name="b">The second matrix to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool operator ==(Matrix<T> a, Matrix<T> b)
		{
			return Equal(a, b);
		}

		/// <summary>Does a value non-equality check.</summary>
		/// <param name="a">The first matrix to check for non-equality.</param>
		/// <param name="b">The second matrix to check for non-equality.</param>
		/// <returns>True if values are not equal, false if not.</returns>
		public static bool operator !=(Matrix<T> a, Matrix<T> b)
		{
			return !Equal(a, b);
		}

		/// <summary>Does a value equality check.</summary>
		/// <param name="b">The second matrix to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool Equal(Matrix<T> b)
		{
			return this == b;
		}

        #endregion

		#region Equal (+leniency)

		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="a">The first matrix to check for equality.</param>
		/// <param name="b">The second matrix to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool Equal(Matrix<T> a, Matrix<T> b, T leniency)
		{
			if (a is null)
			{
				if (b is null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			if (b is null)
			{
				return false;
			}
			int Rows = a.Rows;
			int Columns = a.Columns;
			if (Rows != b.Rows || Columns != b.Columns)
			{
				return false;
			}
			T[] A = a._matrix;
			T[] B = b._matrix;
			int Length = A.Length;
			for (int i = 0; i < Length; i++)
			{
				if (!EqualToLeniency(A[i], B[i], leniency))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="b">The second matrix to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool Equal(Matrix<T> b, T leniency)
		{
			return Equal(this, b, leniency);
		}

		#endregion

		#endregion

		#region Other Methods

		#region Get/Set

		internal T Get(int row, int column)
		{
			return _matrix[row * Columns + column];
		}

		internal void Set(int row, int column, T value)
		{
			_matrix[row * Columns + column] = value;
		}

		#endregion

		#region Format

		/// <summary>Fills a matrix with values using a delegate.</summary>
		/// <param name="matrix">The matrix to fill the values of.</param>
		/// <param name="function">The function to set the values at the relative indeces.</param>
		public static void Format(Matrix<T> matrix, Func<int, int, T> function)
		{
			int Rows = matrix.Rows;
			int Columns = matrix.Columns;
			T[] MATRIX = matrix._matrix;
			int i = 0;
			for (int row = 0; row < Rows; row++)
			{
				for (int column = 0; column < Columns; column++)
				{
					MATRIX[i++] = function(row, column);
				}
			}
		}

		/// <summary>Fills a matrix with values using a delegate.</summary>
		/// <param name="matrix">The matrix to fill the values of.</param>
		/// <param name="func">The function to set the values at the relative indeces.</param>
		public static void Format(Matrix<T> matrix, Func<int, T> func)
		{
			matrix._matrix.Format(func);
		}

		#endregion

		#region CloneContents

		internal static void CloneContents(Matrix<T> a, Matrix<T> b)
		{
			T[] a_flat = a._matrix;
			T[] b_flat = b._matrix;
			int length = a_flat.Length;
			for (int i = 0; i < length; i++)
			{
				b_flat[i] = a_flat[i];
			}
		}

		#endregion

		#region TransposeContents

		internal static void TransposeContents(Matrix<T> a)
		{
			int Rows = a.Rows;
			for (int i = 0; i < Rows; i++)
			{
				int Rows_Minus_i = Rows - i;
				for (int j = 0; j < Rows_Minus_i; j++)
				{
					T temp = a.Get(i, j);
					a.Set(i, j, a.Get(j, i));
					a.Set(j, i, temp);
				}
			}
		}

		#endregion

		#region Clone

		/// <summary>Creates a copy of a matrix.</summary>
		/// <param name="a">The matrix to copy.</param>
		/// <returns>The copy of this matrix.</returns>
		public static Matrix<T> Clone(Matrix<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			return new Matrix<T>(a);
		}

		/// <summary>Copies this matrix.</summary>
		/// <returns>The copy of this matrix.</returns>
		public Matrix<T> Clone()
		{
			return Clone(this);
		}

		#endregion

		#endregion

		#region Casting Operators

		/// <summary>Converts a T[,] into a matrix.</summary>
		/// <param name="array">The T[,] to convert to a matrix.</param>
		/// <returns>The resulting matrix after conversion.</returns>
		public static implicit operator Matrix<T>(T[,] array) =>
			new Matrix<T>(array.GetLength(0), array.GetLength(1), (i, j) => array[i, j]);

		/// <summary>Converts a matrix into a T[,].</summary>
		/// <param name="matrix">The matrix toconvert to a T[,].</param>
		/// <returns>The resulting T[,] after conversion.</returns>
		public static explicit operator T[,](Matrix<T> matrix)
		{
			int rows = matrix._rows;
			int columns = matrix._columns;
			T[,] array = new T[rows, columns];
			T[] MATRIX = matrix._matrix;
			int k = 0;
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					array[i, j] = MATRIX[k++];
				}
			}
			return array;
		}

		#endregion

		#region Steppers

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step) => _matrix.Stepper(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step) => _matrix.Stepper(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step) => _matrix.Stepper(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step) => _matrix.Stepper(step);

		#endregion

		#region Overrides

		/// <summary>Prints out a string representation of this matrix.</summary>
		/// <returns>A string representing this matrix.</returns>
		public override string ToString() => base.ToString();

		/// <summary>Matrixs a hash code from the values of this matrix.</summary>
		/// <returns>A hash code for the matrix.</returns>
		public override int GetHashCode() => _matrix.GetHashCode() ^ _rows ^ _columns;

		/// <summary>Does an equality check by value.</summary>
		/// <param name="b">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object b) => b is Matrix<T> B
			? Equal(this, B)
			: false;

		#endregion
	}
}
