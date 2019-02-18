using System;
using Towel.Measurements;

namespace Towel.Mathematics
{
    /// <summary>A matrix of arbitrary dimensions implemented as a flattened array.</summary>
    /// <typeparam name="T">The numeric type of this Matrix.</typeparam>
    [Serializable]
    public struct Matrix<T>
    {
        internal readonly T[] _matrix;
        internal int _rows;
        internal int _columns;

        #region Properties

        internal int Length { get { return this._matrix.Length; } }
        /// <summary>The number of rows in the matrix.</summary>
        public int Rows { get { return this._rows; } }
        /// <summary>The number of columns in the matrix.</summary>
        public int Columns { get { return this._columns; } }
        /// <summary>Determines if the matrix is square.</summary>
        public bool IsSquare { get { return this._rows == this._columns; } }
        /// <summary>Determines if the matrix is a vector.</summary>
        public bool IsVector { get { return this._columns == 1; } }
        /// <summary>Determines if the matrix is a 2 component vector.</summary>
        public bool Is2x1 { get { return this._rows == 2 && this._columns == 1; } }
        /// <summary>Determines if the matrix is a 3 component vector.</summary>
        public bool Is3x1 { get { return this._rows == 3 && this._columns == 1; } }
        /// <summary>Determines if the matrix is a 4 component vector.</summary>
        public bool Is4x1 { get { return this._rows == 4 && this._columns == 1; } }
        /// <summary>Determines if the matrix is a 2 square matrix.</summary>
        public bool Is2x2 { get { return this._rows == 2 && this._columns == 2; } }
        /// <summary>Determines if the matrix is a 3 square matrix.</summary>
        public bool Is3x3 { get { return this._rows == 3 && this._columns == 3; } }
        /// <summary>Determines if the matrix is a 4 square matrix.</summary>
        public bool Is4x4 { get { return this._rows == 4 && this._columns == 4; } }

        /// <summary>Standard row-major matrix indexing.</summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <returns>The value at the given indeces.</returns>
        public T this[int row, int column]
        {
            get
            {
                if (row < 0 || row > this.Rows)
                {
                    throw new ArgumentOutOfRangeException(nameof(row), row, "!(" + nameof(row) + " >= 0) || !(" + nameof(row) + " < " + nameof(Rows) + ")");
                }
                if (column < 0 || column > this.Columns)
                {
                    throw new ArgumentOutOfRangeException(nameof(column), row, "!(" + nameof(column) + " >= 0) || !(" + nameof(column) + " < " + nameof(Columns) + ")");
                }
                return Get(row, column);
            }
            set
            {
                if (row < 0 || row > this.Rows)
                {
                    throw new ArgumentOutOfRangeException(nameof(row), row, "!(" + nameof(row) + " >= 0) || !(" + nameof(row) + " < " + nameof(Rows) + ")");
                }
                if (column < 0 || column > this.Columns)
                {
                    throw new ArgumentOutOfRangeException(nameof(column), row, "!(" + nameof(column) + " >= 0) || !(" + nameof(column) + " < " + nameof(Columns) + ")");
                }
                this.Set(row, column, value);
            }
        }

        #endregion

        #region Constructors

        internal Matrix(int rows, int columns, int length)
        {
            this._matrix = new T[length];
            this._rows = rows;
            this._columns = columns;
        }

        /// <summary>Constructs a new zero-matrix of the given dimensions (using FlattenedArray format).</summary>
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
            this._matrix = new T[rows * columns];
            this._rows = rows;
            this._columns = columns;
        }

        /// <summary>Constructs a matrix from a T[,] (using FlattenedArray format).</summary>
        /// <param name="matrix">The float[,] to wrap in a matrix class.</param>
        public Matrix(T[,] matrix)
        {
            this._rows = matrix.GetLength(0);
            this._columns = matrix.GetLength(1);
            this._matrix = new T[this._rows * this._columns];
            T[] this_matrix_flat = this._matrix;
            int current = 0;
            for (int i = 0; i < this._rows; i++)
            {
                for (int j = 0; j < this._columns; j++)
                {
                    this_matrix_flat[current++] = matrix[i, j];
                }
            }
        }

        private Matrix(Matrix<T> matrix)
        {
            this._rows = matrix._rows;
            this._columns = matrix.Columns;
            this._matrix = (matrix._matrix).Clone() as T[];
        }

        internal Matrix(Vector<T> vector)
        {
            this._rows = vector.Dimensions;
            this._columns = 1;
            this._matrix = vector._vector.Clone() as T[];
        }

		#endregion

		#region Factories

		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed zero-matrix.</returns>
		public static Matrix<T> FactoryZero(int rows, int columns)
		{
			return Matrix<T>.Matrix_FactoryZero(rows, columns);
		}

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static Matrix<T> FactoryIdentity(int rows, int columns)
		{
			return Matrix<T>.Matrix_FactoryIdentity(rows, columns);
		}

		/// <summary>Constructs a new matrix where every entry is 1.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed matrix filled with 1's.</returns>
		public static Matrix<T> FactoryOne(int rows, int columns)
		{
			return Matrix<T>.Matrix_FactoryOne(rows, columns);
		}

		/// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <param name="uniform">The value to assign every spot in the matrix.</param>
		/// <returns>The newly constructed matrix filled with the uniform value.</returns>
		public static Matrix<T> FactoryUniform(int rows, int columns, T uniform)
		{
            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException("rows", rows, "!(rows > 0)");
            }
            if (columns < 1)
            {
                throw new ArgumentOutOfRangeException("columns", columns, "!(columns > 0)");
            }
            Matrix<T> matrix = new Matrix<T>(rows, columns);
            matrix._matrix.Fill(uniform);
			return matrix;
		}

		/// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
		/// <returns>The constructed 2-component vector matrix.</returns>
		public static Matrix<T> Factory2x1() { return new Matrix<T>(2, 1); }
		/// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
		/// <returns>The constructed 3-component vector matrix.</returns>
		public static Matrix<T> Factory3x1() { return new Matrix<T>(3, 1); }
		/// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
		/// <returns>The constructed 4-component vector matrix.</returns>
		public static Matrix<T> Factory4x1() { return new Matrix<T>(4, 1); }

		/// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
		/// <returns>The constructed 2x2 matrix.</returns>
		public static Matrix<T> Factory2x2() { return new Matrix<T>(2, 2); }
		/// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
		/// <returns>The constructed 3x3 matrix.</returns>
		public static Matrix<T> Factory3x3() { return new Matrix<T>(3, 3); }
		/// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
		/// <returns>The constructed 4x4 matrix.</returns>
		public static Matrix<T> Factory4x4() { return new Matrix<T>(4, 4); }

		///// <param name="angle">Angle of rotation in radians.</param>
		//public static Matrix<T> Factory3x3RotationX(T angle)
		//{
		//	T cos = _cos(angle);
		//	T sin = _sin(angle);
		//	return new Matrix<T>(new T[,] {
		//		{ _one, _zero, _zero },
		//		{ _zero, cos, sin },
		//		{ _zero, _negate(sin), cos }});
		//}

		///// <param name="angle">Angle of rotation in radians.</param>
		//public static Matrix<T> Factory3x3RotationY(T angle)
		//{
		//	T cos = _cos(angle);
		//	T sin = _sin(angle);
		//	return new Matrix<T>(new T[,] {
		//		{ cos, _zero, _negate(sin) },
		//		{ _zero, _one, _zero },
		//		{ sin, _zero, cos }});
		//}

		///// <param name="angle">Angle of rotation in radians.</param>
		//public static Matrix<T> Factory3x3RotationZ(T angle)
		//{
		//	T cos = _cos(angle);
		//	T sin = _sin(angle);
		//	return new Matrix<T>(new T[,] {
		//		{ cos, _negate(sin), _zero },
		//		{ sin, cos, _zero },
		//		{ _zero, _zero, _zero }});
		//}

		///// <param name="angleX">Angle about the X-axis in radians.</param>
		///// <param name="angleY">Angle about the Y-axis in radians.</param>
		///// <param name="angleZ">Angle about the Z-axis in radians.</param>
		//public static Matrix<T> Factory3x3RotationXthenYthenZ(T angleX, T angleY, T angleZ)
		//{
		//	T xCos = _cos(angleX), xSin = _sin(angleX),
		//		yCos = _cos(angleY), ySin = _sin(angleY),
		//		zCos = _cos(angleZ), zSin = _sin(angleZ);
		//	return new Matrix<T>(new T[,] {
		//		{ _multiply(yCos, zCos), _negate(_multiply(yCos, zSin)), ySin },
		//		{ _add(_multiply(xCos, zSin), _multiply(_multiply(xSin, ySin), zCos)), _add(_multiply(xCos, zCos), _multiply(_multiply(xSin, ySin), zSin)), _negate(_multiply(xSin, yCos)) },
		//		{ _subtract(_multiply(xSin, zSin), _multiply(_multiply(xCos, ySin), zCos)), _add(_multiply(xSin, zCos), _multiply(_multiply(xCos, ySin), zSin)), _multiply(xCos, yCos) }});
		//}

		///// <param name="angleX">Angle about the X-axis in radians.</param>
		///// <param name="angleY">Angle about the Y-axis in radians.</param>
		///// <param name="angleZ">Angle about the Z-axis in radians.</param>
		//public static Matrix<T> Factory3x3RotationZthenYthenX(T angleX, T angleY, T angleZ)
		//{
		//	T xCos = _cos(angleX), xSin = _sin(angleX),
		//		yCos = _cos(angleY), ySin = _sin(angleY),
		//		zCos = _cos(angleZ), zSin = _sin(angleZ);
		//	return new Matrix<T>(new T[,] {
		//		{ _multiply(yCos, zCos), _subtract(_multiply(_multiply(zCos, xSin), ySin), _multiply(xCos, zSin)), _add(_multiply(_multiply(xCos, zCos), ySin), _multiply(xSin, zSin)) },
		//		{ _multiply(yCos, zSin), _add(_multiply(xCos, zCos), _multiply(_multiply(xSin, ySin), zSin)), _add(_multiply(_negate(zCos), xSin), _multiply(_multiply(xCos, ySin), zSin)) },
		//		{ _negate(ySin), _multiply(yCos, xSin), _multiply(xCos, yCos) }});
		//}

		///// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
		///// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
		///// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
		///// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
		///// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
		///// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
		///// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
		///// <returns>The constructed shearing matrix.</returns>
		//public static Matrix<T> Factory3x3Shear(
		//	T shearXbyY, T shearXbyZ, T shearYbyX,
		//	T shearYbyZ, T shearZbyX, T shearZbyY)
		//{
		//	return new Matrix<T>(new T[,] {
		//		{ _one, shearYbyX, shearZbyX },
		//		{ shearXbyY, _one, shearYbyZ },
		//		{ shearXbyZ, shearYbyZ, _one }});
		//}

		#endregion

		#region Operators

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="a">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negations.</returns>
		public static Matrix<T> operator -(Matrix<T> a)
		{ return Negate(a); }
		/// <summary>Does a standard matrix addition.</summary>
		/// <param name="a">The left matrix of the addition.</param>
		/// <param name="b">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after teh addition.</returns>
		public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
		{ return Add(a, b); }
		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="a">The left matrix of the subtraction.</param>
		/// <param name="b">The right matrix of the subtraction.</param>
		/// <returns>The result of the matrix subtraction.</returns>
		public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
		{ return Subtract(a, b); }
		/// <summary>Multiplies a vector by a matrix.</summary>
		/// <param name="a">The matrix of the multiplication.</param>
		/// <param name="b">The vector of the multiplication.</param>
		/// <returns>The resulting vector after the multiplication.</returns>
		public static Vector<T> operator *(Matrix<T> a, Vector<T> b)
		{ return Multiply(a, b); }
		/// <summary>Does a standard matrix multiplication.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix after the multiplication.</returns>
		public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
		{ return Multiply(a, b); }
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The matrix to have its values multiplied.</param>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> operator *(Matrix<T> a, T b)
		{ return Multiply(a, b); }
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The scalar to multiply the values by.</param>
		/// <param name="b">The matrix to have its values multiplied.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> operator *(T a, Matrix<T> b)
		{ return Multiply(b, a); }
		/// <summary>Divides all the values in a matrix by a scalar.</summary>
		/// <param name="a">The matrix to have its values divided.</param>
		/// <param name="b">The scalar to divide the values by.</param>
		/// <returns>The resulting matrix after the divisions.</returns>
		public static Matrix<T> operator /(Matrix<T> a, T b)
		{ return Divide(a, b); }
		/// <summary>Applies a power to a matrix.</summary>
		/// <param name="a">The matrix to apply a power to.</param>
		/// <param name="b">The power to apply to the matrix.</param>
		/// <returns>The result of the power operation.</returns>
		public static Matrix<T> operator ^(Matrix<T> a, int b)
		{ return Power(a, b); }
		/// <summary>Checks for equality by value.</summary>
		/// <param name="a">The left matrix of the equality check.</param>
		/// <param name="b">The right matrix of the equality check.</param>
		/// <returns>True if the values of the matrices are equal, false if not.</returns>
		public static bool operator ==(Matrix<T> a, Matrix<T> b)
		{ return EqualsByValue(a, b); }
		/// <summary>Checks for false-equality by value.</summary>
		/// <param name="a">The left matrix of the false-equality check.</param>
		/// <param name="b">The right matrix of the false-equality check.</param>
		/// <returns>True if the values of the matrices are not equal, false if they are.</returns>
		public static bool operator !=(Matrix<T> a, Matrix<T> b)
		{ return !Matrix_EqualsByValue(a, b); }
		/// <summary>Automatically converts a float[,] into a matrix if necessary.</summary>
		/// <param name="a">The float[,] to convert to a matrix.</param>
		/// <returns>The reference to the matrix representing the T[,].</returns>
		public static explicit operator Matrix<T>(T[,] a)
		{ return new Matrix<T>(a); }
		///// <summary>Automatically converts a matrix into a T[,] if necessary.</summary>
		///// <param name="matrix">The matrix to convert to a T[,].</param>
		///// <returns>The reference to the T[,] representing the matrix.</returns>
		//public static explicit operator T[,](Matrix<T> matrix)
		//{ 
		//	T[,] array = new T[matrix.Rows, matrix.Columns];
		//	for (int i = 0; i < i )
		//	return matrix; }
		#endregion

		#region Instance

		/// <summary>Negates all the values in this matrix.</summary>
		/// <returns>The resulting matrix after the negations.</returns>
		public Matrix<T> Negate()
		{ return Matrix<T>.Negate(this); }
		/// <summary>Does a standard matrix addition.</summary>
		/// <param name="right">The matrix to add to this matrix.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public Matrix<T> Add(Matrix<T> right)
		{ return Matrix<T>.Add(this, right); }
		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
		/// <param name="right">The matrix to multiply this matrix by.</param>
		/// <returns>The resulting matrix after the multiplication.</returns>
		public Matrix<T> Multiply(Matrix<T> right)
		{ return Matrix<T>.Multiply(this, right); }
		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
		/// <param name="right">The scalar to multiply all the matrix values by.</param>
		/// <returns>The retulting matrix after the multiplications.</returns>
		public Matrix<T> Multiply(T right)
		{ return Matrix<T>.Multiply(this, right); }
		/// <summary>Divides all the values in this matrix by a scalar.</summary>
		/// <param name="right">The scalar to divide the matrix values by.</param>
		/// <returns>The resulting matrix after teh divisions.</returns>
		public Matrix<T> Divide(T right)
		{ return Matrix<T>.Divide(this, right); }
		/// <summary>Gets the minor of a matrix.</summary>
		/// <param name="row">The restricted row of the minor.</param>
		/// <param name="column">The restricted column of the minor.</param>
		/// <returns>The minor from the row/column restrictions.</returns>
		public Matrix<T> Minor(int row, int column)
		{ return Matrix<T>.Minor(this, row, column); }
		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="right">The matrix to combine with on the right side.</param>
		/// <returns>The resulting row-wise concatination.</returns>
		public Matrix<T> ConcatenateRowWise(Matrix<T> right)
		{ return Matrix<T>.ConcatenateRowWise(this, right); }
		/// <summary>Matrixs the determinent if this matrix is square.</summary>
		/// <returns>The computed determinent if this matrix is square.</returns>
		public T Determinent()
		{ return Matrix<T>.Matrix_Determinent(this); }
		/// <summary>Matrixs the echelon form of this matrix (aka REF).</summary>
		/// <returns>The computed echelon form of this matrix (aka REF).</returns>
		public Matrix<T> Echelon()
		{ return Matrix<T>.Echelon(this); }
		/// <summary>Matrixs the reduced echelon form of this matrix (aka RREF).</summary>
		/// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
		public Matrix<T> ReducedEchelon()
		{ return Matrix<T>.ReducedEchelon(this); }
		/// <summary>Matrixs the inverse of this matrix.</summary>
		/// <returns>The inverse of this matrix.</returns>
		public Matrix<T> Inverse()
		{ return Matrix<T>.Inverse(this); }
		/// <summary>Gets the adjoint of this matrix.</summary>
		/// <returns>The adjoint of this matrix.</returns>
		public Matrix<T> Adjoint()
		{ return Matrix<T>.Adjoint(this); }
		/// <summary>Transposes this matrix.</summary>
		/// <returns>The transpose of this matrix.</returns>
		public Matrix<T> Transpose()
		{ return Matrix<T>.Transpose(this); }
		/// <summary>Copies this matrix.</summary>
		/// <returns>The copy of this matrix.</returns>
		public Matrix<T> Clone()
		{ return Matrix<T>.Clone(this); }

		#endregion

		#region Static

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="a">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negations.</returns>
		public static Matrix<T> Negate(Matrix<T> a)
		{
            Matrix<T> b = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Negate(a, ref b);
            return b;
        }
		/// <summary>Does standard addition of two matrices.</summary>
		/// <param name="a">The left matrix of the addition.</param>
		/// <param name="b">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public static Matrix<T> Add(Matrix<T> a, Matrix<T> b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Add(a, b, ref c);
            return c;
        }
		/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
		/// <param name="a">The matrix to have the values subtracted from.</param>
		/// <param name="b">The scalar to subtract from all the matrix values.</param>
		/// <returns>The resulting matrix after the subtractions.</returns>
		public static Matrix<T> Subtract(Matrix<T> a, Matrix<T> b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Subtract(a, b, ref c);
            return c;
        }
		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public static Matrix<T> Multiply(Matrix<T> a, Matrix<T> b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, b.Columns);
            Matrix_Multiply(a, b, ref c);
            return c;
        }
		/// <summary>Does a matrix-vector multiplication.</summary>
		/// <param name="a">The left matrix of the multiplication.</param>
		/// <param name="b">The right vector of the multiplication.</param>
		/// <returns>The resulting matrix-vector of the multiplication.</returns>
		public static Vector<T> Multiply(Matrix<T> a, Vector<T> b)
		{
            Vector<T> c = new Vector<T>(b.Dimensions);
            Matrix_MultiplyVector(a, b, ref c);
            return c;
        }
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="left">The matrix to have the values multiplied.</param>
		/// <param name="right">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> Multiply(Matrix<T> a, T b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_MultiplyScalar(a, b, ref c);
            return c;
        }
		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="a">The matrix to be powered by.</param>
		/// <param name="b">The power to apply to the matrix.</param>
		/// <returns>The resulting matrix of the power operation.</returns>
		public static Matrix<T> Power(Matrix<T> a, int b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Power(a, b, ref c);
            return c;
        }
		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="a">The matrix to divide the values of.</param>
		/// <param name="b">The scalar to divide all the matrix values by.</param>
		/// <returns>The resulting matrix with the divided values.</returns>
		public static Matrix<T> Divide(Matrix<T> a, T b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_DivideScalar(a, b, ref c);
            return c;
        }
		/// <summary>Gets the minor of a matrix.</summary>
		/// <param name="a">The matrix to get the minor of.</param>
		/// <param name="row">The restricted row to form the minor.</param>
		/// <param name="column">The restricted column to form the minor.</param>
		/// <returns>The minor of the matrix.</returns>
		public static Matrix<T> Minor(Matrix<T> a, int row, int column)
		{
            Matrix<T> c = new Matrix<T>(a.Rows - 1, a.Columns - 1);
            Matrix_Minor(a, row, column, ref c);
            return c;
        }
		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="a">The left matrix of the concatenation.</param>
		/// <param name="b">The right matrix of the concatenation.</param>
		/// <returns>The resulting matrix of the concatenation.</returns>
		public static Matrix<T> ConcatenateRowWise(Matrix<T> a, Matrix<T> b)
		{
            Matrix<T> c = new Matrix<T>(a.Rows, a.Columns + b.Columns);
            Matrix_ConcatenateRowWise(a, b, ref c);
            return c;
        }
		/// <summary>Calculates the determinent of a square matrix.</summary>
		/// <param name="matrix">The matrix to calculate the determinent of.</param>
		/// <returns>The determinent of the matrix.</returns>
		public static T Determinent(Matrix<T> matrix)
		{
            return Matrix_Determinent(matrix);
        }
		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <param name="a">The matrix to calculate the echelon of (aka REF).</param>
		/// <returns>The echelon of the matrix (aka REF).</returns>
		public static Matrix<T> Echelon(Matrix<T> a)
		{
            Matrix<T> b = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Echelon(a, ref b);
            return b;
        }
		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		/// <param name="a">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
		/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
		public static Matrix<T> ReducedEchelon(Matrix<T> a)
		{
            Matrix<T> b = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_ReducedEchelon(a, ref b);
            return b;
        }
		/// <summary>Calculates the inverse of a matrix.</summary>
		/// <param name="a">The matrix to calculate the inverse of.</param>
		/// <returns>The inverse of the matrix.</returns>
		public static Matrix<T> Inverse(Matrix<T> a)
		{
            Matrix<T> b = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Inverse(a, ref b);
            return b;
        }
		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <param name="a">The matrix to calculate the adjoint of.</param>
		/// <returns>The adjoint of the matrix.</returns>
		public static Matrix<T> Adjoint(Matrix<T> a)
		{
            Matrix<T> b = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Adjoint(a, ref b);
            return b;
        }
		/// <summary>Returns the transpose of a matrix.</summary>
		/// <param name="a">The matrix to transpose.</param>
		/// <returns>The transpose of the matrix.</returns>
		public static Matrix<T> Transpose(Matrix<T> a)
		{
            Matrix<T> b = new Matrix<T>(a.Rows, a.Columns, a.Length);
            Matrix_Transpose(a, ref b);
            return b;
        }
		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="matrix">The matrix to decompose.</param>
		/// <param name="lower">The computed lower triangular matrix.</param>
		/// <param name="upper">The computed upper triangular matrix.</param>
		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="matrix">The matrix to decompose.</param>
		/// <param name="lower">The computed lower triangular matrix.</param>
		/// <param name="upper">The computed upper triangular matrix.</param>
		public static void DecomposeLU(Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper)
		{
            Matrix<T>.Matrix_DecomposeLU(matrix, out lower, out upper);
        }
		/// <summary>Creates a copy of a matrix.</summary>
		/// <param name="matrix">The matrix to copy.</param>
		/// <returns>A copy of the matrix.</returns>
		public static Matrix<T> Clone(Matrix<T> matrix)
		{
            return new Matrix<T>(matrix);
        }
		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first matrix to check for equality.</param>
		/// <param name="right">The second matrix to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsByValue(Matrix<T> left, Matrix<T> right)
		{
            return Matrix_EqualsByValue(left, right);
        }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first matrix to check for equality.</param>
		/// <param name="right">The second matrix to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsByValue(Matrix<T> left, Matrix<T> right, T leniency)
		{
            return Matrix_EqualsByValue_leniency(left, right, leniency);
        }

        #endregion

        #region Implementations

        #region Get

        internal T Get(int row, int column)
        {
            return this._matrix[row * this.Columns + column];
        }

        #endregion

        #region Set

        internal void Set(int row, int column, T value)
        {
            this._matrix[row * this.Columns + column] = value;
        }

        #endregion

        #region RowMultiplication

        private static void RowMultiplication(Matrix<T> matrix, int row, T scalar)
		{
            int columns = matrix.Columns;
            for (int i = 0; i < columns; i++)
			{
                matrix.Set(row, i, Compute.Multiply(matrix.Get(row, i), scalar));
			}
		}

		#endregion

		#region RowAddition

		private static void RowAddition(Matrix<T> matrix, int target, int second, T scalar)
		{
            int columns = matrix.Columns;
			for (int i = 0; i < columns; i++)
			{
                matrix.Set(target, i, Compute.Add(matrix.Get(target, i), Compute.Multiply(matrix.Get(second, i), scalar)));
				matrix[target, i] = Compute.Add(matrix.Get(target, i), Compute.Multiply(matrix.Get(second, i), scalar));
			}
		}

        #endregion

        #region SwapRows

        private static void SwapRows(Matrix<T> matrix, int row1, int row2)
        {
            int columns = matrix.Columns;
            for (int i = 0; i < columns; i++)
            {
                T temp = matrix.Get(row1, i);
                matrix.Set(row1, i, matrix.Get(row2, i));
                matrix.Set(row2, i, temp);
            }
        }

        #endregion

        #region DiagonalOnes

        private static Action<Matrix<T>> DiagonalOnes = (Matrix<T> a) =>
        {
            int a_rows = a._rows;
            int a_columns = a._columns;
            for (int i = 0; i < a_rows; i++)
            {
                for (int j = 0; j < a_columns; j++)
                {
                    a.Set(i, j, Compute.Constant<T>.One);
                }
            }
        };

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
            int Columns = a.Columns;
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

        #region FactoryZero

        internal static Func<int, int, Matrix<T>> Matrix_FactoryZero = (int rows, int columns) =>
		{
			if (Compute.Equal(default(T), Compute.Constant<T>.Zero))
			{
				Matrix_FactoryZero = (int ROWS, int COLUMNS) =>
				{
                    if (ROWS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
                    }
                    if (COLUMNS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
                    }
                    Matrix<T> result = new Matrix<T>(ROWS, COLUMNS);
					return result;
				};
			}
			else
			{
				Matrix_FactoryZero = (int ROWS, int COLUMNS) =>
				{
                    if (ROWS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
                    }
                    if (COLUMNS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
                    }
                    Matrix<T> result = new Matrix<T>(ROWS, COLUMNS);
                    result._matrix.Fill(Compute.Constant<T>.Zero);
                    return result;
				};
			}
			return Matrix_FactoryZero(rows, columns);
		};

		#endregion

		#region FactoryOne
		
		private static Func<int, int, Matrix<T>> Matrix_FactoryOne = (int rows, int columns) =>
		{
            if (Compute.Equal(default(T), Compute.Constant<T>.One))
            {
                Matrix_FactoryOne = (int ROWS, int COLUMNS) =>
                {
                    if (ROWS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
                    }
                    if (COLUMNS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
                    }
                    Matrix<T> result = new Matrix<T>(ROWS, COLUMNS);
                    return result;
                };
            }
            else
            {
                Matrix_FactoryOne = (int ROWS, int COLUMNS) =>
                {
                    if (ROWS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
                    }
                    if (COLUMNS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
                    }
                    Matrix<T> result = new Matrix<T>(ROWS, COLUMNS);
                    result._matrix.Fill(Compute.Constant<T>.One);
                    return result;
                };
            }
            return Matrix_FactoryOne(rows, columns);
        };

		#endregion

		#region FactoryIdentity
		
		private static Func<int, int, Matrix<T>> Matrix_FactoryIdentity = (int rows, int columns) =>
		{
            if (Compute.Equal(default(T), Compute.Constant<T>.Zero))
            {
                Matrix_FactoryIdentity = (int ROWS, int COLUMNS) =>
                {
                    if (ROWS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
                    }
                    if (COLUMNS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
                    }
                    Matrix<T> result = new Matrix<T>(ROWS, COLUMNS);
                    T[] result_flat = result._matrix;
                    DiagonalOnes(result);
                    return result;
                };
            }
            else
            {
                Matrix_FactoryIdentity = (int ROWS, int COLUMNS) =>
                {
                    if (ROWS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(rows), rows, "!(" + nameof(rows) + " > 0)");
                    }
                    if (COLUMNS < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(columns), columns, "!(" + nameof(columns) + " > 0)");
                    }
                    Matrix<T> result = new Matrix<T>(ROWS, COLUMNS);
                    result._matrix.Fill(Compute.Constant<T>.Zero);
                    DiagonalOnes(result);
                    return result;
                };
            }
            return Matrix_FactoryIdentity(rows, columns);
        };

		#endregion

		#region IsSymetric
		
		private static Func<Matrix<T>, bool> Matrix_IsSymetric = (Matrix<T> a) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (a._rows != a._columns)
            {
                return false;
            }
            int side_length = a._rows;
            T[] a_flat = a._matrix;
            for (int row = 0; row < side_length; row++)
            {
                for (int column = row + 1; column < side_length; column++)
                {
                    if (Compute.NotEqual(a_flat[row * side_length + column], a_flat[column * side_length + row]))
                    {
                        return false;
                    }
                }
            }
            return true;
		};

        #endregion

        #region Negate

        private delegate void NegateSignature(Matrix<T> a, ref Matrix<T> c);
        private static NegateSignature Matrix_Negate = (Matrix<T> a, ref Matrix<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            int length = a._matrix.Length;
            T[] a_flat = a._matrix;
            T[] b_flat;
            if (b != null && b._matrix.Length == length)
            {
                b_flat = b._matrix;
                b._rows = a._rows;
                b._columns = a._columns;
            }
            else
            {
                b = new Matrix<T>(a._rows, a._columns, length);
                b_flat = b._matrix;
            }
            for (int i = 0; i < length; i++)
            {
                b_flat[i] = Compute.Negate(a_flat[i]);
            }
		};

        #endregion

        #region Add

        private delegate void AddSignature(Matrix<T> a, Matrix<T> b, ref Matrix<T> c);
        private static AddSignature Matrix_Add = (Matrix<T> a, Matrix<T> b, ref Matrix<T> c) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a._rows != b._rows && a._columns != b._columns)
            {
                throw new MathematicsException("Invalid multiplication (size miss-match).");
            }
            int length = a._matrix.Length;
            T[] a_flat = a._matrix;
            T[] b_flat = b._matrix;
            T[] c_flat;
            if (c != null && c._matrix.Length == length)
            {
                c_flat = c._matrix;
                c._rows = a._rows;
                c._columns = a._columns;
            }
            else
            {
                c = new Matrix<T>(a._rows, a._columns, length);
                c_flat = c._matrix;
            }
            for (int i = 0; i < length; i++)
            {
                c_flat[i] = Compute.Add(a_flat[i], b_flat[i]);
            }
        };

        #endregion

        #region Subtract

        private delegate void SubtractSignature(Matrix<T> a, Matrix<T> b, ref Matrix<T> c);
        private static SubtractSignature Matrix_Subtract = (Matrix<T> a, Matrix<T> b, ref Matrix<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a._rows != b._rows && a._columns != b._columns)
            {
                throw new MathematicsException("Invalid multiplication (size miss-match).");
            }
            int length = a._matrix.Length;
            T[] a_flat = a._matrix;
            T[] b_flat = b._matrix;
            T[] c_flat;
            if (c != null && c._matrix.Length == length)
            {
                c_flat = c._matrix;
                c._rows = a._rows;
                c._columns = a._columns;
            }
            else
            {
                c = new Matrix<T>(a._rows, a._columns, length);
                c_flat = c._matrix;
            }
            for (int i = 0; i < length; i++)
            {
                c_flat[i] = Compute.Subtract(a_flat[i], b_flat[i]);
            }
        };

        #endregion

        #region Multiply

        private delegate void MultiplySignature(Matrix<T> a, Matrix<T> b, ref Matrix<T> c);
		private static MultiplySignature Matrix_Multiply = (Matrix<T> a, Matrix<T> b, ref Matrix<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a._columns != b._rows)
            {
                throw new MathematicsException("Invalid multiplication (size miss-match).");
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
            T[] a_flat = a._matrix;
            T[] b_flat = b._matrix;
            T[] c_flat;
            T sum;
            if (c != null && c._matrix.Length == c_Rows * c_Columns)
            {
                c_flat = c._matrix;
                c._rows = c_Rows;
                c._columns = c_Columns;
            }
            else
            {
                c = new Matrix<T>(c_Rows, c_Columns);
                c_flat = c._matrix;
            }
            for (int i = 0; i < c_Rows; i++)
            {
                for (int j = 0; j < c_Columns; j++)
                {
                    sum = Compute.Constant<T>.Zero;
                    for (int k = 0; k < a_Columns; k++)
                    {
                        sum = Compute.Add(sum, Compute.Multiply(a_flat[i * a_Columns + k], b_flat[k * c_Columns + j]));
                    }
                    c_flat[i * c_Columns + j] = sum;
                }
            }
		};

        #endregion

        #region MultiplyVector

        private delegate void MultiplyVectorSignature(Matrix<T> a, Vector<T> b, ref Vector<T> c);
        private static MultiplyVectorSignature Matrix_MultiplyVector = (Matrix<T> a, Vector<T> b, ref Vector<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
	        if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
		    if (a._columns != b._vector.Length)
            { 
		        throw new MathematicsException("Invalid multiplication (size miss-match).");
            }
	        int a_rows = a._rows;
	        int a_columns = a._columns;
            T[] a_flat = a._matrix;
            T[] b_flat = b._vector;
            T[] c_flat;
            if (c == null && c._vector.Length == b._vector.Length)
            {
                c_flat = c._vector;
            }
            else
            {
                c = new Vector<T>(b._vector.Length);
                c_flat = c._vector;
            }
            for (int i = 0; i < a_rows; i++)
            {
                c_flat[i] = Compute.Constant<T>.Zero;
                for (int j = 0; j < a_columns; j++)
                {
                    c_flat[i] = Compute.Add(c_flat[i], Compute.Multiply(a_flat[i * a_columns + j], b_flat[j]));
                }
            }
		};

        #endregion

        #region MultiplyScalar
        
        private delegate void MultiplyScalarSignature(Matrix<T> a, T b, ref Matrix<T> c);
        private static MultiplyScalarSignature Matrix_MultiplyScalar = (Matrix<T> a, T b, ref Matrix<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            int length = a._matrix.Length;
            T[] a_flat = a._matrix;
            T[] c_flat;
            if (c != null && c._matrix.Length == length)
            {
                c_flat = c._matrix;
                c._rows = a._rows;
                c._columns = a._columns;
            }
            else
            {
                c = new Matrix<T>(a._rows, a._columns, length);
                c_flat = c._matrix;
            }
            for (int i = 0; i < length; i++)
            {
                c_flat[i] = Compute.Add(a_flat[i], b);
            }
		};

        #endregion

        #region DivideScalar

        private delegate void DivideScalarSignature(Matrix<T> a, T b, ref Matrix<T> c);
        private static DivideScalarSignature Matrix_DivideScalar = (Matrix<T> a, T b, ref Matrix<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            int length = a._matrix.Length;
            T[] a_flat = a._matrix;
            T[] c_flat;
            if (c != null && c._matrix.Length == length)
            {
                c_flat = c._matrix;
                c._rows = a._rows;
                c._columns = a._columns;
            }
            else
            {
                c = new Matrix<T>(a._rows, a._columns, length);
                c_flat = c._matrix;
            }
            for (int i = 0; i < length; i++)
            {
                c_flat[i] = Compute.Divide(a_flat[i], b);
            }
        };

        #endregion

        #region Power

        private delegate void PowerSignature(Matrix<T> a, int b, ref Matrix<T> c);
        private static PowerSignature Matrix_Power = (Matrix<T> a, int b, ref Matrix<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (!a.IsSquare)
            {
                throw new MathematicsException("Invalid power (!" + nameof(a) + ".IsSquare)");
            }
            if (!(b >= 0))
            {
                throw new ArgumentOutOfRangeException(nameof(b), b, "Invalid power !(" + nameof(b) + " > -1).");
            }
            if (b == 0)
            {
                if (c != null && c._matrix.Length == a._matrix.Length)
                {
                    c._rows = a._rows;
                    c._columns = a._columns;
                    c._matrix.Fill(Compute.Constant<T>.Zero);
                    DiagonalOnes(c);
                }
                else
                {
                    c = Matrix<T>.FactoryIdentity(a._rows, a._columns);
                }
                return;
            }
            if (c != null && c._matrix.Length == a._matrix.Length)
            {
                c._rows = a._rows;
                c._columns = a._columns;
                T[] a_flat = a._matrix;
                T[] c_flat = c._matrix;
                for (int i = 0; i < a._matrix.Length; i++)
                {
                    c_flat[i] = a_flat[i];
                }
            }
            else
            {
                c = a.Clone();
            }
            Matrix<T> d = new Matrix<T>(a._rows, a._columns, a._matrix.Length);
            for (int i = 0; i < b; i++)
            {
                Matrix_Multiply(c, c, ref d);
                Matrix<T> temp = d;
                d = c;
                c = d;
            }
		};

		#endregion

        #region Rotate

        /// <summary>Rotates a 4x4 matrix around an 3D axis by a specified angle.</summary>
        /// <param name="angle">The angle of rotation around the axis.</param>
        /// <param name="axis">The 3D axis to rotate the matrix around.</param>
        /// <param name="matrix">The 4x4 matrix to rotate.</param>
        /// <returns>The rotated matrix.</returns>
        public static Matrix<T> Rotate4x4(Angle<T> angle, Vector<T> axis, Matrix<T> matrix)
        {
            if (axis == null)
            {
                throw new ArgumentNullException(nameof(axis));
            }
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (axis._vector.Length != 3)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(axis) + "." + nameof(axis.Dimensions) + " == 3)");
            }
            if (matrix._rows != 4 || matrix._columns != 4)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(matrix) + "." + nameof(matrix.Rows) + " == 4 && " + nameof(matrix) + "." + nameof(matrix.Columns) + " == 4)");
            }

            // if the angle is zero, no rotation is required
            if (Compute.Equal(angle._measurement, Compute.Constant<T>.Zero))
            {
                return matrix.Clone();
            }

            T cosine = Compute.Cosine(angle);
            T sine = Compute.Sine(angle);
            T oneMinusCosine = Compute.Subtract(Compute.Constant<T>.One, cosine);
            T xy = Compute.Multiply(axis.X, axis.Y);
            T yz = Compute.Multiply(axis.Y, axis.Z);
            T xz = Compute.Multiply(axis.X, axis.Z);
            T xs = Compute.Multiply(axis.X, sine);
            T ys = Compute.Multiply(axis.Y, sine);
            T zs = Compute.Multiply(axis.Z, sine);

            T f00 = Compute.Add(Compute.Multiply(Compute.Multiply(axis.X, axis.X), oneMinusCosine), cosine);
            T f01 = Compute.Add(Compute.Multiply(xy, oneMinusCosine), zs);
            T f02 = Compute.Subtract(Compute.Multiply(xz, oneMinusCosine), ys);
            // n[3] not used
            T f10 = Compute.Subtract(Compute.Multiply(xy, oneMinusCosine), zs);
            T f11 = Compute.Add(Compute.Multiply(Compute.Multiply(axis.Y, axis.Y), oneMinusCosine), cosine);
            T f12 = Compute.Add(Compute.Multiply(yz, oneMinusCosine), xs);
            // n[7] not used
            T f20 = Compute.Add(Compute.Multiply(xz, oneMinusCosine), ys);
            T f21 = Compute.Subtract(Compute.Multiply(yz, oneMinusCosine), xs);
            T f22 = Compute.Add(Compute.Multiply(Compute.Multiply(axis.Z, axis.Z), oneMinusCosine), cosine);

            // Row 1
            T _0_0 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 0], f00), Compute.Multiply(matrix[1, 0], f01)), Compute.Multiply(matrix[2, 0], f02));
            T _0_1 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 1], f00), Compute.Multiply(matrix[1, 1], f01)), Compute.Multiply(matrix[2, 1], f02));
            T _0_2 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 2], f00), Compute.Multiply(matrix[1, 2], f01)), Compute.Multiply(matrix[2, 2], f02));
            T _0_3 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 3], f00), Compute.Multiply(matrix[1, 3], f01)), Compute.Multiply(matrix[2, 3], f02));
            // Row 2
            T _1_0 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 0], f10), Compute.Multiply(matrix[1, 0], f11)), Compute.Multiply(matrix[2, 0], f12));
            T _1_1 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 1], f10), Compute.Multiply(matrix[1, 1], f11)), Compute.Multiply(matrix[2, 1], f12));
            T _1_2 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 2], f10), Compute.Multiply(matrix[1, 2], f11)), Compute.Multiply(matrix[2, 2], f12));
            T _1_3 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 3], f10), Compute.Multiply(matrix[1, 3], f11)), Compute.Multiply(matrix[2, 3], f12));
            // Row 3
            T _2_0 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 0], f20), Compute.Multiply(matrix[1, 0], f21)), Compute.Multiply(matrix[2, 0], f22));
            T _2_1 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 1], f20), Compute.Multiply(matrix[1, 1], f21)), Compute.Multiply(matrix[2, 1], f22));
            T _2_2 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 2], f20), Compute.Multiply(matrix[1, 2], f21)), Compute.Multiply(matrix[2, 2], f22));
            T _2_3 = Compute.Add(Compute.Add(Compute.Multiply(matrix[0, 3], f20), Compute.Multiply(matrix[1, 3], f21)), Compute.Multiply(matrix[2, 3], f22));
            // Row 4
            T _3_0 = Compute.Constant<T>.Zero;
            T _3_1 = Compute.Constant<T>.Zero;
            T _3_2 = Compute.Constant<T>.Zero;
            T _3_3 = Compute.Constant<T>.One;

            return new Matrix<T>(new T[,]
            {
                { _0_0, _0_1, _0_2, _0_3 },
                { _1_0, _1_1, _1_2, _1_3 },
                { _2_0, _2_1, _2_2, _2_3 },
                { _3_0, _3_1, _3_2, _3_3 }
            });
        }

        #endregion

        #region Minor

        private static void Matrix_Minor(Matrix<T> a, int row, int column, ref Matrix<T> b)
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (a._rows < 2 || a._columns < 2)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.Rows) + " >= 2 && " + nameof(a) + "." + nameof(a.Columns) + " >= 2)");
            }
            if (row < 0 || row >= a._rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row), row, "!(" + nameof(row) + " > 0)");
            }
            if (column < 0 || column >= a._columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column), column, "!(" + nameof(column) + " > 0)");
            }
            if (object.ReferenceEquals(a, b))
            {
                a = a.Clone();
            }
            int a_rows = a._rows;
            int a_columns = a._columns;
            int b_rows = a_rows - 1;
            int b_columns = a_columns - 1;
            int b_length = (b_rows - 1) * (b_columns - 1);
            if (b == null || b._matrix.Length != b_length)
            {
                b = new Matrix<T>(b_rows, b_columns, b_length);
            }
            else
            {
                b._rows = b_rows;
                b._columns = b_columns;
            }
            T[] b_flat = b._matrix;
            T[] a_flat = a._matrix;
	        int m = 0, n = 0;
	        for (int i = 0; i < a_rows; i++)
	        {
                if (i == row)
                {
                    continue;
                }
			    n = 0;
			    for (int j = 0; j < b_columns; j++)
			    {
                    if (j == column)
                    {
                        continue;
                    }
                    b_flat[m * b_columns + n] = a_flat[(i * a_columns) + j];
			    	n++;
			    }
			    m++;
	        }
		}

		#endregion

		#region ConcatenateRowWise

		private static void Matrix_ConcatenateRowWise(Matrix<T> a, Matrix<T> b, ref Matrix<T> c)
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (object.ReferenceEquals(a, c))
            {
                throw new MathematicsException("Arguments invalid (object.ReferenceEquals(" + nameof(a) + ", " + nameof(c) + "))");
            }
            if (object.ReferenceEquals(b, c))
            {
                throw new MathematicsException("Arguments invalid (object.ReferenceEquals(" + nameof(b) + ", " + nameof(c) + "))");
            }
            if (a._rows != b._rows)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Rows) + " == " + nameof(b) + "." + nameof(b.Rows) + ")");
            }
            int a_columns = a._columns;
            int b_columns = b._columns;
            int c_rows = a._rows;
            int c_columns = a._columns + b._columns;
            int c_length = c_rows * c_columns;
            if (c == null || c._matrix.Length != c_length)
            {
                c = new Matrix<T>(c_rows, c_columns, c_length);
            }
            else
            {
                c._rows = c_rows;
                c._columns = c_columns;
            }
            T[] a_flat = a._matrix;
            T[] b_flat = b._matrix;
            T[] c_flat = c._matrix;
            for (int i = 0; i < c_rows; i++)
            {
                for (int j = 0; j < c_columns; j++)
                {
                    if (j < a_columns)
                    {
                        c_flat[(i * c_columns) + j] = a_flat[(i * a_columns) + j];
                    }
                    else
                    {
                        c_flat[(i * c_columns) + j] = b_flat[(i * b_columns) + (j - a_columns)];
                    }
                }
            }
		}

        #endregion

        #region Determinent

        private static Func<Matrix<T>, T> Matrix_Determinent = (Matrix<T> a) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (!a.IsSquare)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
            }
            T determinant = Compute.Constant<T>.One;
            Matrix<T> rref = a.Clone();
            int a_rows = a._rows;
            for (int i = 0; i < a_rows; i++)
            {
                if (Compute.Equal(rref[i, i], Compute.Constant<T>.Zero))
                {
                    for (int j = i + 1; j < rref.Rows; j++)
                    {
                        if (Compute.NotEqual(rref.Get(j, i), Compute.Constant<T>.Zero))
                        {
                            SwapRows(rref, i, j);
                            determinant = Compute.Multiply(determinant, Compute.Constant<T>.NegativeOne);
                        }
                    }
                }
                determinant = Compute.Multiply(determinant, rref.Get(i, i));
                T temp_rowMultiplication = Compute.Divide(Compute.Constant<T>.One, rref.Get(i, i));
                RowMultiplication(rref, i, temp_rowMultiplication);
                for (int j = i + 1; j < rref.Rows; j++)
                {
                    T scalar = Compute.Negate(rref.Get(j, i));
                    RowAddition(rref, j, i, scalar);

                }
                for (int j = i - 1; j >= 0; j--)
                {
                    T scalar = Compute.Negate(rref.Get(j, i));
                    RowAddition(rref, j, i, scalar);

                }
            }
            return determinant;
        };

        #endregion

        #region Echelon

        private delegate void EchelonSignature(Matrix<T> a, ref Matrix<T> b);
        private static EchelonSignature Matrix_Echelon = (Matrix<T> a, ref Matrix<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (object.ReferenceEquals(a, b))
            {
                a = a.Clone();
            }
            int Rows = a.Rows;
            if (b != null && b._matrix.Length == a._matrix.Length)
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
                if (Compute.Equal(b.Get(i, i), Compute.Constant<T>.Zero))
                {
                    for (int j = i + 1; j < Rows; j++)
                    {
                        if (Compute.NotEqual(b.Get(j, i), Compute.Constant<T>.Zero))
                        {
                            SwapRows(b, i, j);
                        }
                    }
                }
                if (Compute.Equal(b.Get(i, i), Compute.Constant<T>.Zero))
                {
                    continue;
                }
                if (Compute.NotEqual(b.Get(i, i), Compute.Constant < T > .One))
                {
                    for (int j = i + 1; j < Rows; j++)
                    {
                        if (Compute.Equal(b.Get(j, i), Compute.Constant<T>.One))
                        {
                            SwapRows(b, i, j);
                        }
                    }
                }
                T rowMultipier = Compute.Divide(Compute.Constant<T>.One, b.Get(i, i));
                RowMultiplication(b, i, rowMultipier);
                for (int j = i + 1; j < Rows; j++)
                {
                    T rowAddend = Compute.Negate(b.Get(j, i));
                    RowAddition(b, j, i, rowAddend);
                }
            }
		};

        #endregion

        #region ReducedEchelon

        private delegate void ReducedEchelonSignature(Matrix<T> a, ref Matrix<T> b);
        private static ReducedEchelonSignature Matrix_ReducedEchelon = (Matrix<T> a, ref Matrix<T> b) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (object.ReferenceEquals(a, b))
            {
                a = a.Clone();
            }
            int Rows = a.Rows;
            if (b != null && b._matrix.Length == a._matrix.Length)
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
                if (Compute.Equal(b.Get(i, i), Compute.Constant<T>.Zero))
                {
                    for (int j = i + 1; j < Rows; j++)
                    {
                        if (Compute.NotEqual(b.Get(j, i), Compute.Constant<T>.Zero))
                        {
                            SwapRows(b, i, j);
                        }
                    }
                }
                if (Compute.Equal(b.Get(i, i), Compute.Constant<T>.Zero))
                {
                    continue;
                }
                if (Compute.NotEqual(b.Get(i, i), Compute.Constant<T>.One))
                {
                    for (int j = i + 1; j < Rows; j++)
                    {
                        if (Compute.Equal(b.Get(j, i), Compute.Constant<T>.One))
                        {
                            SwapRows(b, i, j);
                        }
                    }
                }
                T rowMiltiplier = Compute.Divide(Compute.Constant<T>.One, b.Get(i, i));
                RowMultiplication(b, i, rowMiltiplier);
                for (int j = i + 1; j < Rows; j++)
                {
                    T rowAddend = Compute.Negate(b.Get(j, i));
                    RowAddition(b, j, i, rowAddend);
                }
                for (int j = i - 1; j >= 0; j--)
                {
                    T rowAddend = Compute.Negate(b.Get(j, i));
                    RowAddition(b, j, i, rowAddend);
                }
            }
        };

        #endregion

        #region Inverse

        internal delegate void InverseSignature(Matrix<T> a, ref Matrix<T> b);
        private static InverseSignature Matrix_Inverse = (Matrix<T> a, ref Matrix<T> b) =>
		{
            throw new NotImplementedException();

//			Matrix<T>.Matrix_Inverse =
//				Meta.Compile<Matrix<T>.Delegates.Matrix_Inverse>(
//					string.Concat(
//@"(Matrix<", T_Source, @">.Delegates.Matrix_Inverse)(
//(Matrix<", T_Source, @"> _matrix) =>
//{
//	if (object.ReferenceEquals(_matrix, null))
//		throw new System.ArgumentNullException(", "\"matrix\"", @");
//	if (Matrix<", T_Source, @">.Determinent(_matrix) == 0)
//		throw new System.ArithmeticException(", "\"inverse calculation failed.\"", @");
//	Matrix<", T_Source, @"> identity = Matrix<", T_Source, @">.FactoryIdentity(_matrix.Rows, _matrix.Columns);
//	Matrix<", T_Source, @"> rref = _matrix.Clone();
//	for (int i = 0; i < _matrix.Rows; i++)
//	{
//		if (rref[i, i] == 0)
//			for (int j = i + 1; j < rref.Rows; j++)
//				if (rref[j, i] != 0)
//				{
//					", SwapRows("temp", "k", "rref", "i", "j"), @"
//					", SwapRows("temp", "k", "identity", "i", "j"), @"
//				}
//		", T_Source, @" temp_rowMultiplication1 = 1 / rref[i, i];
//		", RowMultiplication("j", "identity", "i", "temp_rowMultiplication1"), @"
//		", T_Source, @" temp_rowMultiplication2 = 1 / rref[i, i];
//		", RowMultiplication("j", "rref", "i", "temp_rowMultiplication2"), @"
//		for (int j = i + 1; j < rref.Rows; j++)
//		{
//			", T_Source, @" scalar1 = -rref[j, i];
//			", RowAddition("k", "identity", "j", "i", "scalar1"), @"
//			", T_Source, @" scalar2 = -rref[j, i];
//			", RowAddition("k", "rref", "j", "i", "scalar2"), @"
//		}
//		for (int j = i - 1; j >= 0; j--)
//		{
//			", T_Source, @" scalar1 = -rref[j, i];
//			", RowAddition("k", "identity", "j", "i", "scalar1"), @"
//			", T_Source, @" scalar2 = -rref[j, i];
//			", RowAddition("k", "rref", "j", "i", "scalar2"), @"
//		}
//	}
//	return identity;
//})"));

//			return Matrix<T>.Matrix_Inverse(matrix);

			#region Alternate Version
			//Matrix<T> identity = Matrix<T>.FactoryIdentity(matrix.Rows, matrix.Columns);
			//Matrix<T> rref = matrix.Clone();
			//for (int i = 0; i < matrix.Rows; i++)
			//{
			//	if (Compute<T>.Equate(rref[i, i], Compute<T>.FromInt32(0)))
			//		for (int j = i + 1; j < rref.Rows; j++)
			//			if (!Compute<T>.Equate(rref[j, i], Compute<T>.FromInt32(0)))
			//			{
			//				Matrix<T>.SwapRows(rref, i, j);
			//				Matrix<T>.SwapRows(identity, i, j);
			//			}
			//	Matrix<T>.RowMultiplication(identity, i, Compute<T>.Divide(Compute<T>.FromInt32(1), rref[i, i]));
			//	Matrix<T>.RowMultiplication(rref, i, Compute<T>.Divide(Compute<T>.FromInt32(1), rref[i, i]));
			//	for (int j = i + 1; j < rref.Rows; j++)
			//	{
			//		Matrix<T>.RowAddition(identity, j, i, Compute<T>.Negate(rref[j, i]));
			//		Matrix<T>.RowAddition(rref, j, i, Compute<T>.Negate(rref[j, i]));
			//	}
			//	for (int j = i - 1; j >= 0; j--)
			//	{
			//		Matrix<T>.RowAddition(identity, j, i, Compute<T>.Negate(rref[j, i]));
			//		Matrix<T>.RowAddition(rref, j, i, Compute<T>.Negate(rref[j, i]));
			//	}
			//}
			//return identity;
			#endregion
		};
        #endregion

        #region Adjoint

        private delegate void AdjointSignature(Matrix<T> a, ref Matrix<T> c);
        private static AdjointSignature Matrix_Adjoint = (Matrix<T> a, ref Matrix<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (!a.IsSquare)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(a) + "." + nameof(a.IsSquare) + ")");
            }
            if (object.ReferenceEquals(a, b))
            {
                a = a.Clone();
            }
            int Length = a.Length;
            int Rows = a.Rows;
            int Columns = a.Columns;
            if (b != null && b.Length == Length)
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
                    if (Compute.IsEven(a.Get(i, j)))
                    {
                        b[i, j] = Determinent(Minor(a, i, j));
                    }
                    else
                    {
                        b[i, j] = Compute.Negate(Determinent(Minor(a, i, j)));
                    }
                }
            }
		};

		#endregion

		#region Transpose

		private static void Matrix_Transpose(Matrix<T> a, ref Matrix<T> b)
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (object.ReferenceEquals(a, b))
            {
                TransposeContents(b);
                return;
            }
            int Length = a.Length;
            int Rows = a.Rows;
            int Columns = a.Columns;
            if (b != null && b.Length == a.Length)
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

        #endregion

        #region DecomposeLU

        internal delegate void DecomposeLUSignature(Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper);
        private static DecomposeLUSignature Matrix_DecomposeLU = (Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper) =>
		{
            throw new NotImplementedException();

//			Matrix<T>.Matrix_DecomposeLU =
//				Meta.Compile<Matrix<T>.Delegates.Matrix_DecomposeLU>(
//					string.Concat(
//"(Matrix<", T_Source, "> _matrix, out Matrix<", T_Source, "> _Lower, out Matrix<", T_Source, @"> _Upper) =>
//{
//	if (object.ReferenceEquals(_matrix, null))
//		throw new System.Exception(", "\"null reference: _matrix\"", @");
//	if (_matrix.Rows != _matrix.Columns)
//		throw new System.Exception(", "\"non-square _matrix during DecomposeLU function\"", @");
//	_Lower = Matrix<", T_Source, @">.FactoryIdentity(_matrix.Rows, _matrix.Columns);
//	_Upper = _matrix.Clone();
//	int[] permutation = new int[_matrix.Rows];
//	for (int i = 0; i < _matrix.Rows; i++) permutation[i] = i;
//	", T_Source, @" p = 0, pom2, detOfP = 1;
//	int k0 = 0, pom1 = 0;
//	for (int k = 0; k < _matrix.Columns - 1; k++)
//	{
//		p = 0;
//		for (int i = k; i < _matrix.Rows; i++)
//				if ((_Upper[i, k] > 0 ? _Upper[i, k] : -_Upper[i, k]) > p)
//				{
//						p = _Upper[i, k] > 0 ? _Upper[i, k] : -_Upper[i, k];
//						k0 = i;
//				}
//		if (p == 0)
//				throw new System.Exception(", "\"The _matrix is singular!\"", @");
//		pom1 = permutation[k];
//		permutation[k] = permutation[k0];
//		permutation[k0] = pom1;
//		for (int i = 0; i < k; i++)
//		{
//				pom2 = _Lower[k, i];
//				_Lower[k, i] = _Lower[k0, i];
//				_Lower[k0, i] = pom2;
//		}
//		if (k != k0)
//				detOfP *= -1;
//		for (int i = 0; i < _matrix.Columns; i++)
//		{
//				pom2 = _Upper[k, i];
//				_Upper[k, i] = _Upper[k0, i];
//				_Upper[k0, i] = pom2;
//		}
//		for (int i = k + 1; i < _matrix.Rows; i++)
//		{
//				_Lower[i, k] = _Upper[i, k] / _Upper[k, k];
//				for (int j = k; j < _matrix.Columns; j++)
//					_Upper[i, j] = _Upper[i, j] - _Lower[i, k] * _Upper[k, j];
//		}
//	}
//}"));

//			Matrix<T>.Matrix_DecomposeLU(matrix, out lower, out upper);

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
		};
		#endregion

		#region EqualsByValue
		
		private static Func<Matrix<T>, Matrix<T>, bool> Matrix_EqualsByValue = (Matrix<T> a, Matrix<T> b) =>
		{
            if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
                return true;
            if (object.ReferenceEquals(a, null))
                return false;
            if (object.ReferenceEquals(b, null))
                return false;
            int Rows = a.Rows;
            int Columns = a.Columns;
            if (Rows != b.Rows || Columns != b.Columns)
                return false;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Compute.NotEqual(a.Get(i, j), b.Get(i, j)))
                    {
                        return false;
                    }
                }
            }
            return true;
		};

		#endregion

		#region EqualsByValue_leniency
		
		private static Func<Matrix<T>, Matrix<T>, T, bool> Matrix_EqualsByValue_leniency = (Matrix<T> a, Matrix<T> b, T leniency) =>
		{
            if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
                return true;
            if (object.ReferenceEquals(a, null))
                return false;
            if (object.ReferenceEquals(b, null))
                return false;
            int Rows = a.Rows;
            int Columns = a.Columns;
            if (Rows != b.Rows || Columns != b.Columns)
                return false;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Compute.EqualLeniency(a.Get(i, j), b.Get(i, j), leniency))
                    {
                        return false;
                    }
                }
            }
            return true;
        };

		#endregion

		#endregion

		#region Steppers

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				step_function(this._matrix[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				step_function(ref this._matrix[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				if (step_function(this._matrix[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				if (step_function(ref this._matrix[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		#endregion

		#region Overrides

		/// <summary>Prints out a string representation of this matrix.</summary>
		/// <returns>A string representing this matrix.</returns>
		public override string ToString()
		{
            return base.ToString();
        }

		/// <summary>Matrixs a hash code from the values of this matrix.</summary>
		/// <returns>A hash code for the matrix.</returns>
		public override int GetHashCode()
		{
            return this._matrix.GetHashCode();
        }

		/// <summary>Does an equality check by value.</summary>
		/// <param name="right">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object right)
		{
			if (!(right is Matrix<T>))
				return Matrix<T>.EqualsByValue(this, (Matrix<T>)right);
			return false;
		}

		#endregion
	}
}
