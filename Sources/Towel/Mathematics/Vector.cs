using System;
using System.Diagnostics;
using System.Text;
using Towel.Measurements;
using static Towel.Statics;

namespace Towel.Mathematics
{
	/// <summary>Represents a vector with an arbitrary number of components of a generic type.</summary>
	/// <typeparam name="T">The numeric type of this Vector.</typeparam>
	[DebuggerDisplay("{" + nameof(DebuggerString) + "}")]
	public class Vector<T>
	{
		internal readonly T[] _vector;

		#region Basic Properties

		/// <summary>Index 0</summary>
		public T X
		{
			get
			{
				if (Dimensions < 1)
				{
					throw new MathematicsException("This vector doesn't have an " + nameof(X) + " component.");
				}
				return _vector[0];
			}
			set
			{
				if (Dimensions < 1)
				{
					throw new MathematicsException("This vector doesn't have an " + nameof(X) + " component.");
				}
				_vector[0] = value;
			}
		}

		/// <summary>Index 1</summary>
		public T Y
		{
			get
			{
				if (Dimensions < 2)
				{
					throw new MathematicsException("This vector doesn't have an " + nameof(Y) + " component.");
				}
				return _vector[1];
			}
			set
			{
				if (Dimensions < 2)
				{
					throw new MathematicsException("This vector doesn't have an " + nameof(Y) + " component.");
				}
				_vector[1] = value;
			}
		}

		/// <summary>Index 2</summary>
		public T Z
		{
			get
			{
				if (Dimensions < 3)
				{
					throw new MathematicsException("This vector doesn't have an " + nameof(Z) + " component.");
				}
				return _vector[2];
			}
			set
			{
				if (Dimensions < 3)
				{
					throw new MathematicsException("This vector doesn't have an " + nameof(Z) + " component.");
				}
				_vector[2] = value;
			}
		}

		/// <summary>The number of components in this vector.</summary>
		public int Dimensions
		{
			get
			{
				return _vector is null ? 0 : _vector.Length;
			}
		}

		/// <summary>Allows indexed access to this vector.</summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The value of the given index.</returns>
		public T this[int index]
		{
			get
			{
				if (0 > index || index > Dimensions)
				{
					throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Dimensions) + ")");
				}
				return _vector[index];
			}
			set
			{
				if (0 > index || index > Dimensions)
				{
					throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Dimensions) + ")");
				}
				_vector[index] = value;
			}
		}

		#endregion

		#region Debugger Properties

		internal string DebuggerString
		{
			get
			{
				StringBuilder stringBuilder = new();
				stringBuilder.Append('[');
				stringBuilder.Append(_vector[0]);
				for (int i = 1; i < _vector.Length; i++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append(_vector[i]);
				}
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}
		}

		#endregion

		#region Constructors

		/// <summary>Creates a new vector with the given number of components.</summary>
		/// <param name="dimensions">The number of dimensions this vector will have.</param>
		public Vector(int dimensions)
		{
			if (dimensions < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(dimensions), dimensions, "!(" + nameof(dimensions) + " >= 0)");
			}
			_vector = new T[dimensions];
		}

		/// <summary>Creates a vector out of the given values.</summary>
		/// <param name="vector">The values to initialize the vector to.</param>
		public Vector(params T[] vector)
		{
			_vector = vector;
		}

		/// <summary>Creates a new vector and initializes it via function.</summary>
		/// <param name="dimensions">The number of dimensions of the vector to construct.</param>
		/// <param name="function">The function to initialize the values of the vector.</param>
		public Vector(int dimensions, Func<int, T> function) : this(dimensions)
		{
			for (int i = 0; i < dimensions; i++)
			{
				_vector[i] = function(i);
			}
		}

		internal Vector(Vector<T> vector)
		{
			_vector = (T[])vector._vector.Clone();
		}

		#endregion

		#region Factories

		/// <summary>Creates a vector with the given number of components with the values initialized to zeroes.</summary>
		/// <param name="dimensions">The number of components in the vector.</param>
		/// <returns>The newly constructed vector.</returns>
		public static Vector<T> FactoryZero(int dimensions)
		{
			return FactoryZeroImplementation(dimensions);
		}

		internal static Func<int, Vector<T>> FactoryZeroImplementation = dimensions =>
		{
			if (Equate(default, Constant<T>.Zero))
			{
				FactoryZeroImplementation = DIMENSIONS => new Vector<T>(DIMENSIONS);
			}
			else
			{
				FactoryZeroImplementation = DIMENSIONS =>
				{
					T[] vector = new T[DIMENSIONS];
					vector.Format(Constant<T>.Zero);
					return new Vector<T>(vector);
				};
			}
			return FactoryZeroImplementation(dimensions);
		};

		/// <summary>Creates a vector with the given number of components with the values initialized to ones.</summary>
		/// <param name="dimensions">The number of components in the vector.</param>
		/// <returns>The newly constructed vector.</returns>
		public static Vector<T> FactoryOne(int dimensions)
		{
			return FactoryOneImplementation(dimensions);
		}

		internal static Func<int, Vector<T>> FactoryOneImplementation = dimensions =>
		{
			if (Equate(default, Constant<T>.One))
			{
				FactoryZeroImplementation = DIMENSIONS => new Vector<T>(DIMENSIONS);
			}
			else
			{
				FactoryZeroImplementation = DIMENSIONS =>
				{
					T[] vector = new T[DIMENSIONS];
					vector.Format(Constant<T>.One);
					return new Vector<T>(vector);
				};
			}
			return FactoryZeroImplementation(dimensions);
		};

		#endregion

		#region Mathematics

		#region Magnitude

		/// <summary>Computes the length of this vector.</summary>
		/// <returns>The length of this vector.</returns>
		public static T GetMagnitude(Vector<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			return Statics.SquareRoot(GetMagnitudeSquared(a));
		}

		/// <summary>Computes the length of this vector.</summary>
		public T Magnitude
		{
			get
			{
				return GetMagnitude(this);
			}
		}

		#endregion

		#region MagnitudeSquared

		/// <summary>Computes the length of this vector, but doesn't square root it for 
		/// possible optimization purposes.</summary>
		/// <returns>The squared length of the vector.</returns>
		public static T GetMagnitudeSquared(Vector<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			int Length = a.Dimensions;
			T result = Constant<T>.Zero;
			T[] A = a._vector;
			for (int i = 0; i < Length; i++)
			{
				result = MultiplyAddImplementation<T>.Function(A[i], A[i], result);
			}
			return result;
		}

		/// <summary>Computes the length of this vector, but doesn't square root it for 
		/// possible optimization purposes.</summary>
		public T MagnitudeSquared
		{
			get
			{
				return GetMagnitudeSquared(this);
			}
		}

		#endregion

		#region Negate

		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="a">The vector to have its values negated.</param>
		/// <param name="b">The result of the negations.</param>
		public static void Negate(Vector<T> a, ref Vector<T>? b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			T[] A = a._vector;
			int Length = A.Length;
			T[] B;
			if (b is null || b.Dimensions != Length)
			{
				b = new Vector<T>(Length);
				B = b._vector;
			}
			else
			{
				B = b._vector;
				if (B.Length != Length)
				{
					b = new Vector<T>(Length);
					B = b._vector;
				}
			}
			for (int i = 0; i < Length; i++)
			{
				B[i] = Statics.Negation(A[i]);
			}
		}

		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="a">The vector to have its values negated.</param>
		/// <returns>The result of the negations.</returns>
		public static Vector<T> Negate(Vector<T> a)
		{
			Vector<T>? b = null;
			Negate(a, ref b);
			return b!;
		}

		/// <summary>Negates a vector.</summary>
		/// <param name="vector">The vector to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Vector<T> operator -(Vector<T> vector)
		{
			return Negate(vector);
		}

		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="b">The result of the negations.</param>
		public void Negate(ref Vector<T>? b)
		{
			Negate(this, ref b);
		}

		/// <summary>Negates this vector.</summary>
		/// <returns>The result of the negation.</returns>
		public Vector<T> Negate()
		{
			return -this;
		}

		#endregion

		#region Add

		/// <summary>Adds two vectors together.</summary>
		/// <param name="a">The first vector of the addition.</param>
		/// <param name="b">The second vector of the addiiton.</param>
		/// <param name="c">The result of the addition.</param>
		public static void Add(Vector<T> a, Vector<T> b, ref Vector<T>? c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			T[] A = a._vector;
			T[] B = b._vector;
			int Length = A.Length;
			if (Length != B.Length)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
			}
			T[] C;
			if (c is null)
			{
				c = new Vector<T>(Length);
				C = c._vector;
			}
			else
			{
				C = c._vector;
				if (C.Length != Length)
				{
					c = new Vector<T>(Length);
					C = c._vector;
				}
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Statics.Addition(A[i], B[i]);
			}
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="a">The first vector of the addition.</param>
		/// <param name="b">The second vector of the addiiton.</param>
		/// <returns>The result of the addition.</returns>
		public static Vector<T> Add(Vector<T> a, Vector<T> b)
		{
			Vector<T>? c = null;
			Add(a, b, ref c);
			return c!;
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="a">The first vector of the addition.</param>
		/// <param name="b">The second vector of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Vector<T> operator +(Vector<T> a, Vector<T> b)
		{
			return Add(a, b);
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="b">The second vector of the addition.</param>
		/// <param name="c">The result of the addition.</param>
		public void Add(Vector<T> b, ref Vector<T>? c)
		{
			Add(this, b, ref c);
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="b">The vector to add to this one.</param>
		/// <returns>The result of the addition.</returns>
		public Vector<T> Add(Vector<T> b)
		{
			return this + b;
		}

		#endregion

		#region Subtract

		/// <summary>Subtracts two vectors.</summary>
		/// <param name="a">The left vector of the subtraction.</param>
		/// <param name="b">The right vector of the subtraction.</param>
		/// <param name="c">The result of the vector subtracton.</param>
		public static void Subtract(Vector<T> a, Vector<T> b, ref Vector<T>? c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			T[] A = a._vector;
			T[] B = b._vector;
			int Length = A.Length;
			if (Length != B.Length)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
			}
			T[] C;
			if (c is null)
			{
				c = new Vector<T>(Length);
				C = c._vector;
			}
			else
			{
				C = c._vector;
				if (C.Length != Length)
				{
					c = new Vector<T>(Length);
					C = c._vector;
				}
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Statics.Subtraction(A[i], B[i]);
			}
		}

		/// <summary>Subtracts two vectors.</summary>
		/// <param name="a">The left vector of the subtraction.</param>
		/// <param name="b">The right vector of the subtraction.</param>
		/// <returns>The result of the vector subtracton.</returns>
		public static Vector<T> Subtract(Vector<T> a, Vector<T> b)
		{
			Vector<T>? c = null;
			Subtract(a, b, ref c);
			return c!;
		}

		/// <summary>Subtracts two vectors.</summary>
		/// <param name="a">The left operand of the subtraction.</param>
		/// <param name="b">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Vector<T> operator -(Vector<T> a, Vector<T> b)
		{
			return Subtract(a, b);
		}

		/// <summary>Subtracts two vectors.</summary>
		/// <param name="b">The right vector of the subtraction.</param>
		/// <param name="c">The result of the vector subtracton.</param>
		public void Subtract(Vector<T> b, ref Vector<T>? c)
		{
			Subtract(this, b, ref c);
		}

		/// <summary>Subtracts another vector from this one.</summary>
		/// <param name="b">The vector to subtract from this one.</param>
		/// <returns>The result of the subtraction.</returns>
		public Vector<T> Subtract(Vector<T> b)
		{
			return this - b;
		}

		#endregion

		#region Multiply

		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="a">The vector to have all its values multiplied.</param>
		/// <param name="b">The scalar to multiply all the vector values by.</param>
		/// <param name="c">The result of the multiplication.</param>
		public static void Multiply(Vector<T> a, T b, ref Vector<T>? c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			T[] A = a._vector;
			int Length = A.Length;
			T[] C;
			if (c is null)
			{
				c = new Vector<T>(Length);
				C = c._vector;
			}
			else
			{
				C = c._vector;
				if (C.Length != Length)
				{
					c = new Vector<T>(Length);
					C = c._vector;
				}
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Statics.Multiplication(A[i], b);
			}
		}

		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="a">The vector to have all its values multiplied.</param>
		/// <param name="b">The scalar to multiply all the vector values by.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> Multiply(Vector<T> a, T b)
		{
			Vector<T>? c = null;
			Multiply(a, b, ref c);
			return c!;
		}

		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="a">The vector to have all its values multiplied.</param>
		/// <param name="b">The scalar to multiply all the vector values by.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(Vector<T> a, T b)
		{
			return Multiply(a, b);
		}

		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="a">The scalar to multiply all the vector values by.</param>
		/// <param name="b">The vector to have all its values multiplied.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(T a, Vector<T> b)
		{
			return Multiply(b, a);
		}

		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="b">The scalar to multiply all the vector values by.</param>
		/// <param name="c">The result of the multiplication.</param>
		public void Multiply(T b, ref Vector<T>? c)
		{
			Multiply(this, b, ref c);
		}

		/// <summary>Multiplies the values in this vector by a scalar.</summary>
		/// <param name="b">The scalar to multiply these values by.</param>
		/// <returns>The result of the multiplications</returns>
		public Vector<T> Multiply(T b)
		{
			return this * b;
		}

		#endregion

		#region Divide

		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="a">The vector to have the components divided by.</param>
		/// <param name="b">The scalar to divide the vector components by.</param>
		/// <param name="c">The resulting vector after the divisions.</param>
		public static void Divide(Vector<T> a, T b, ref Vector<T>? c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			T[] A = a._vector;
			int Length = A.Length;
			T[] C;
			if (c is null)
			{
				c = new Vector<T>(Length);
				C = c._vector;
			}
			else
			{
				C = c._vector;
				if (C.Length != Length)
				{
					c = new Vector<T>(Length);
					C = c._vector;
				}
			}
			for (int i = 0; i < Length; i++)
			{
				C[i] = Statics.Division(A[i], b);
			}
		}


		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="a">The vector to have the components divided by.</param>
		/// <param name="b">The scalar to divide the vector components by.</param>
		/// <returns>The resulting vector after the divisions.</returns>
		public static Vector<T> Divide(Vector<T> a, T b)
		{
			Vector<T>? c = null;
			Divide(a, b, ref c);
			return c!;
		}

		/// <summary>Divides all the values in the vector by a scalar.</summary>
		/// <param name="a">The vector to have its values divided.</param>
		/// <param name="b">The scalar to divide all the vectors values by.</param>
		/// <returns>The vector after the divisions.</returns>
		public static Vector<T> operator /(Vector<T> a, T b)
		{
			return Divide(a, b);
		}

		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="b">The scalar to divide the vector components by.</param>
		/// <param name="c">The resulting vector after the divisions.</param>
		public void Divide(T b, ref Vector<T>? c)
		{
			Divide(this, b, ref c);
		}

		/// <summary>Divides all the values in this vector by a scalar.</summary>
		/// <param name="b">The scalar to divide the values of the vector by.</param>
		/// <returns>The resulting vector after the divisions.</returns>
		public Vector<T> Divide(T b)
		{
			return this / b;
		}

		#endregion

		#region DotProduct

		/// <summary>Computes the dot product between two vectors.</summary>
		/// <param name="a">The first vector of the dot product operation.</param>
		/// <param name="b">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public static T DotProduct(Vector<T> a, Vector<T> b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			int Length = a.Dimensions;
			if (Length != b.Dimensions)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
			}
			T result = Constant<T>.Zero;
			T[] A = a._vector;
			T[] B = b._vector;
			for (int i = 0; i < Length; i++)
			{
				result = MultiplyAddImplementation<T>.Function(A[i], B[i], result);
			}
			return result;
		}

		/// <summary>Computes the dot product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product.</returns>
		public T DotProduct(Vector<T> right)
		{
			return DotProduct(this, right);
		}

		#endregion

		#region CrossProduct

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="a">The first vector of the cross product operation.</param>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <param name="c">The result of the cross product operation.</param>
		public static void CrossProduct(Vector<T> a, Vector<T> b, ref Vector<T>? c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			T[] A = a._vector;
			T[] B = b._vector;
			if (A.Length != 3)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == 3)");
			}
			if (B.Length != 3)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(b) + "." + nameof(b.Dimensions) + " == 3)");
			}
			if (c is null || c.Dimensions != 3)
			{
				c = new Vector<T>(3);
			}
			T[] C = c._vector;
			C[0] = Statics.Subtraction(Statics.Multiplication(A[1], B[2]), Statics.Multiplication(A[2], B[1]));
			C[1] = Statics.Subtraction(Statics.Multiplication(A[2], B[0]), Statics.Multiplication(A[0], B[2]));
			C[2] = Statics.Subtraction(Statics.Multiplication(A[0], B[1]), Statics.Multiplication(A[1], B[0]));
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="a">The first vector of the cross product operation.</param>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static Vector<T> CrossProduct(Vector<T> a, Vector<T> b)
		{
			Vector<T>? c = null;
			CrossProduct(a, b, ref c);
			return c!;
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <param name="c">The result of the cross product operation.</param>
		public void CrossProduct(Vector<T> b, ref Vector<T>? c)
		{
			CrossProduct(this, b, ref c);
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="b">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public Vector<T> CrossProduct(Vector<T> b)
		{
			return CrossProduct(this, b);
		}

		#endregion

		#region Normalize

		/// <summary>Normalizes a vector.</summary>
		/// <param name="a">The vector to normalize.</param>
		/// <param name="b">The result of the normalization.</param>
		public static void Normalize(Vector<T> a, ref Vector<T>? b)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			int Dimensions = a.Dimensions;
			if (Dimensions < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(a), a, "!(" + nameof(a) + "." + nameof(a.Dimensions) + " > 0)");
			}
			T magnitude = a.Magnitude;
			if (Statics.Equate(magnitude, Constant<T>.Zero))
			{
				throw new ArgumentOutOfRangeException(nameof(a), a, "!(" + nameof(a) + "." + nameof(a.Magnitude) + " > 0)");
			}
			if (b is null ||
				b.Dimensions != Dimensions)
			{
				b = new Vector<T>(Dimensions);
			}
			T[] B = b._vector;
			for (int i = 0; i < Dimensions; i++)
			{
				B[i] = Statics.Division(a[i], magnitude);
			}
		}

		/// <summary>Normalizes a vector.</summary>
		/// <param name="a">The vector to normalize.</param>
		/// <returns>The result of the normalization.</returns>
		public static Vector<T> Normalize(Vector<T> a)
		{
			Vector<T>? b = null;
			Normalize(a, ref b);
			return b!;
		}

		/// <summary>Normalizes a vector.</summary>
		/// <param name="b">The result of the normalization.</param>
		public void Normalize(ref Vector<T>? b)
		{
			Normalize(this, ref b);
		}

		/// <summary>Normalizes this vector.</summary>
		/// <returns>The result of the normalization.</returns>
		public Vector<T> Normalize()
		{
			return Normalize(this);
		}

		#endregion

		#region Angle

		/// <summary>Computes the angle between two vectors.</summary>
		/// <typeparam name="TArcCos">A function for how to compute the inverse of a cosine ratio.</typeparam>
		/// <param name="a">The first vector to determine the angle between.</param>
		/// <param name="b">The second vector to determine the angle between.</param>
		/// <param name="arccos">A function for how to compute the inverse of a cosine ratio.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static Angle<T> Angle<TArcCos>(Vector<T> a, Vector<T> b, TArcCos arccos = default)
			where TArcCos : struct, IFunc<T, Angle<T>>
		{
			// a ⋅ b = |a| * |b| * cosθ

			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			T dotProduct = a.DotProduct(b);
			T aMagTimesbMag = Statics.Multiplication(a.Magnitude, b.Magnitude);
			T divided = Statics.Division(dotProduct, aMagTimesbMag);
			return arccos.Invoke(divided);
		}

		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="a">The first vector to determine the angle between.</param>
		/// <param name="b">The second vector to determine the angle between.</param>
		/// <param name="arccos">A delegate for how to compute the inverse of a cosine ratio.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static Angle<T> Angle(Vector<T> a, Vector<T> b, Func<T, Angle<T>> arccos) =>
			Angle<SFunc<T, Angle<T>>>(a, b, arccos);

		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="b">The second vector to determine the angle between.</param>
		/// <param name="arccos">A function for how to compute the inverse of a cosine ratio.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public Angle<T> Angle(Vector<T> b, Func<T, Angle<T>> arccos) => Angle(this, b, arccos);

		/// <summary>Computes the angle between two vectors.</summary>
		/// <typeparam name="TArcCos">A function for how to compute the inverse of a cosine ratio.</typeparam>
		/// <param name="b">The second vector to determine the angle between.</param>
		/// <param name="arccos">A function for how to compute the inverse of a cosine ratio.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public Angle<T> Angle<TArcCos>(Vector<T> b, TArcCos arccos = default)
			where TArcCos : struct, IFunc<T, Angle<T>> =>
			Angle(this, b, arccos);

		#endregion

		#region Projection

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="a">The first vector of the cross product operation.</param>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <param name="c">The result of the cross product operation.</param>
		public static void Projection(Vector<T> a, Vector<T> b, ref Vector<T>? c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			if (a.Dimensions != b.Dimensions)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
			}
			int Dimensions = a.Dimensions;
			if (c is null || c.Dimensions != Dimensions)
			{
				c = new Vector<T>(Dimensions);
			}
			T magSquared = a.MagnitudeSquared;
			if (Statics.Equate(magSquared, Constant<T>.Zero))
			{
				throw new ArgumentOutOfRangeException(nameof(a), a, "!(" + nameof(a) + "." + nameof(a.Magnitude) + " > 0)");
			}
			T dot = a.DotProduct(b);
			T divided = Statics.Division(dot, magSquared);
			a.Multiply(divided, ref c);
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="a">The first vector of the cross product operation.</param>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static Vector<T> Projection(Vector<T> a, Vector<T> b)
		{
			Vector<T>? c = null;
			Projection(a, b, ref c);
			return c!;
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <param name="c">The result of the cross product operation.</param>
		public void Projection(Vector<T> b, ref Vector<T>? c)
		{
			Projection(this, b, ref c);
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="b">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public Vector<T> Projection(Vector<T> b)
		{
			return Projection(this, b);
		}

		#endregion

		#region RotateBy

		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of the rotation.</param>
		/// <param name="x">The x component of the axis vector to rotate about.</param>
		/// <param name="y">The y component of the axis vector to rotate about.</param>
		/// <param name="z">The z component of the axis vector to rotate about.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, Angle<T> angle, T x, T y, T z)
		{
			throw new NotImplementedException();
		}

		/// <summary>Rotates this vector by quaternon values.</summary>
		/// <param name="angle">The amount of rotation about the axis.</param>
		/// <param name="x">The x component deterniming the axis of rotation.</param>
		/// <param name="y">The y component determining the axis of rotation.</param>
		/// <param name="z">The z component determining the axis of rotation.</param>
		/// <returns>The resulting vector after the rotation.</returns>
		public Vector<T> RotateBy(Angle<T> angle, T x, T y, T z)
		{
			return RotateBy(this, angle, x, y, z);
		}

		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="a">The vector to rotate.</param>
		/// <param name="b">The quaternion to rotate the 3-component vector by.</param>
		/// <param name="c">The result of the rotation.</param>
		public static void RotateBy(Vector<T> a, Quaternion<T> b, ref Vector<T>? c)
		{
			Quaternion<T>.Rotate(b, a, ref c);
		}

		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="a">The vector to rotate.</param>
		/// <param name="b">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> a, Quaternion<T> b)
		{
			Vector<T>? c = null;
			Quaternion<T>.Rotate(b, a, ref c);
			return c!;
		}

		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="b">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public Vector<T> RotateBy(Quaternion<T> b)
		{
			return RotateBy(this, b);
		}

		#endregion

		#region LinearInterpolation

		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="a">The starting vector of the interpolation.</param>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <param name="c">The result of the interpolation.</param>
		public static void LinearInterpolation(Vector<T> a, Vector<T> b, T blend, ref Vector<T>? c)
		{
			if (Statics.LessThan(blend, Constant<T>.Zero) || Statics.GreaterThan(blend, Constant<T>.One))
			{
				throw new ArgumentOutOfRangeException(nameof(blend), blend, "!(0 <= " + nameof(blend) + " <= 1)");
			}
			int Length = a.Dimensions;
			if (Length != b.Dimensions)
			{
				throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " ==" + nameof(b) + "." + nameof(b.Dimensions) + ")");
			}
			if (c is null || c.Dimensions != Length)
			{
				c = new Vector<T>(Length);
			}
			T[] A = a._vector;
			T[] B = b._vector;
			T[] C = c._vector;
			for (int i = 0; i < Length; i++)
			{
				C[i] = Statics.Addition(A[i], Statics.Multiplication(blend, Statics.Subtraction(B[i], A[i])));
			}
		}

		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="a">The starting vector of the interpolation.</param>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Vector<T> LinearInterpolation(Vector<T> a, Vector<T> b, T blend)
		{
			Vector<T>? c = null;
			LinearInterpolation(a, b, blend, ref c);
			return c!;
		}

		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <param name="c">The result of the interpolation.</param>
		public void LinearInterpolation(Vector<T> b, T blend, ref Vector<T>? c)
		{
			LinearInterpolation(this, b, blend, ref c);
		}

		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public Vector<T> LinearInterpolation(Vector<T> b, T blend)
		{
			return LinearInterpolation(this, b, blend);
		}

		#endregion

		#region SphericalInterpolation

		/// <summary>Spherically interpolates between two vectors.</summary>
		/// <param name="a">The starting vector of the interpolation.</param>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <param name="c">The result of the slerp operation.</param>
		public static Vector<T> SphericalInterpolation(Vector<T> a, Vector<T> b, T blend, ref Vector<T>? c)
		{
			throw new NotImplementedException();
		}

		/// <summary>Spherically interpolates between two vectors.</summary>
		/// <param name="a">The starting vector of the interpolation.</param>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public static Vector<T> SphericalInterpolation(Vector<T> a, Vector<T> b, T blend)
		{
			Vector<T>? c = null;
			SphericalInterpolation(a, b, blend, ref c);
			return c!;
		}

		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public Vector<T> SphericalInterpolation(Vector<T> b, T blend)
		{
			return SphericalInterpolation(this, b, blend);
		}

		#endregion

		#region BarycentricInterpolation

		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <param name="d">The result of the interpolation.</param>
		public static void BarycentricInterpolation(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v, ref Vector<T>? d)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			_ = c ?? throw new ArgumentNullException(nameof(c));
			if (Statics.Equate(a.Dimensions, b.Dimensions, c.Dimensions))
			{
				throw new MathematicsException("Arguments invalid !(" +
					nameof(a) + "." + nameof(a.Dimensions) + " == " +
					nameof(b) + "." + nameof(b.Dimensions) + " == " +
					nameof(c) + "." + nameof(c.Dimensions) + ")");
			}

			// Note: needs optimization (call the "ref" methods)
			d = a + (u * (b - a)) + (v * (c - a));
		}

		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public static Vector<T> BarycentricInterpolation(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v)
		{
			Vector<T>? d = null;
			BarycentricInterpolation(a._vector, b._vector, c._vector, u, v, ref d);
			return d!;
		}

		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public Vector<T> BarycentricInterpolation(Vector<T> b, Vector<T> c, T u, T v)
		{
			return BarycentricInterpolation(this, b._vector, c._vector, u, v);
		}

		#endregion

		#region Equal

		/// <summary>Does a value equality check.</summary>
		/// <param name="a">The first vector to check for equality.</param>
		/// <param name="b">The second vector	to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool Equal(Vector<T> a, Vector<T> b)
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
			else
			{
				int Length = a.Dimensions;
				if (Length != b.Dimensions)
				{
					return false;
				}
				T[] A = a._vector;
				T[] B = b._vector;
				for (int i = 0; i < Length; i++)
				{
					if (Statics.Inequate(A[i], B[i]))
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Does a value non-equality check.</summary>
		/// <param name="a">The first vector to check for non-equality.</param>
		/// <param name="b">The second vector	to check for non-equality.</param>
		/// <returns>True if values are not equal, false if not.</returns>
		public static bool NotEqual(Vector<T> a, Vector<T> b)
		{
			return !Equal(a, b);
		}

		/// <summary>Does an equality check by value. (warning for float errors)</summary>
		/// <param name="a">The first vector of the equality check.</param>
		/// <param name="b">The second vector of the equality check.</param>
		/// <returns>true if the values are equal, false if not.</returns>
		public static bool operator ==(Vector<T> a, Vector<T> b)
		{
			return Equal(a, b);
		}

		/// <summary>Does an anti-equality check by value. (warning for float errors)</summary>
		/// <param name="a">The first vector of the anit-equality check.</param>
		/// <param name="b">The second vector of the anti-equality check.</param>
		/// <returns>true if the values are not equal, false if they are.</returns>
		public static bool operator !=(Vector<T> a, Vector<T> b)
		{
			return !Equal(a, b);
		}

		/// <summary>Check for equality by value.</summary>
		/// <param name="b">The other vector of the equality check.</param>
		/// <returns>true if the values were equal, false if not.</returns>
		public bool Equal(Vector<T> b)
		{
			return this == b;
		}

		/// <summary>Check for non-equality by value.</summary>
		/// <param name="b">The other vector of the non-equality check.</param>
		/// <returns>true if the values were not equal, false if not.</returns>
		public bool NotEqual(Vector<T> b)
		{
			return this != b;
		}

		#endregion

		#region Equal (+leniency)

		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="a">The first vector to check for equality.</param>
		/// <param name="b">The second vector to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool Equal(Vector<T> a, Vector<T> b, T leniency)
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
			int Length = a.Dimensions;
			if (Length != b.Dimensions)
				return false;
			T[] A = a._vector;
			T[] B = b._vector;
			for (int i = 0; i < Length; i++)
				if (!Statics.EqualToLeniency(A[i], B[i], leniency))
					return false;
			return true;
		}

		/// <summary>Checks for equality by value with some leniency.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <param name="leniency">The ammount the values can differ but still be considered equal.</param>
		/// <returns>true if the values were cinsidered equal, false if not.</returns>
		public bool Equal(Vector<T> right, T leniency)
		{
			return Equal(this, right, leniency);
		}

		#endregion

		#endregion

		#region Other Methods

		#region Clone

		/// <summary>Creates a copy of a vector.</summary>
		/// <param name="a">The vector to copy.</param>
		/// <returns>The copy of this vector.</returns>
		public static Vector<T> Clone(Vector<T> a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			return new Vector<T>(a);
		}

		/// <summary>Copies this vector.</summary>
		/// <returns>The copy of this vector.</returns>
		public Vector<T> Clone()
		{
			return Clone(this);
		}

		#endregion

		#endregion

		#region Casting Operators

		/// <summary>Implicit conversions from Vector to T[].</summary>
		/// <param name="vector">The Vector to be converted to a T[].</param>
		/// <returns>The T[] of the vector.</returns>
		public static implicit operator T[](Vector<T> vector)
		{
			return vector._vector;
		}

		/// <summary>Implicit conversions from Vector to T[].</summary>
		/// <param name="array">The Vector to be converted to a T[].</param>
		/// <returns>The T[] of the vector.</returns>
		public static implicit operator Vector<T>(T[] array)
		{
			return new Vector<T>(array);
		}

		/// <summary>Converts a vector into a matrix.</summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>The resulting matrix.</returns>
		public static explicit operator Matrix<T>(Vector<T> vector)
		{
			return new Matrix<T>(vector);
		}

		/// <summary>Implicitly converts a scalar into a one dimensional vector.</summary>
		/// <param name="scalar">The scalar value.</param>
		/// <returns>The one dimensional vector </returns>
		public static explicit operator Vector<T>(T scalar)
		{
			return new Vector<T>(scalar);
		}

		#endregion

		#region Overrides

		/// <summary>Computes a hash code from the values of this matrix.</summary>
		/// <returns>A hash code for the matrix.</returns>
		public override int GetHashCode()
		{
			int hashCode = default;
			for (int i = 1; i < _vector.Length; i++)
			{
				hashCode = HashCode.Combine(hashCode, Hash(_vector[i]));
			}
			return hashCode;
		}

		/// <summary>Does an equality check by reference.</summary>
		/// <param name="right">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object? right) => right is Vector<T> vector && Equal(this, vector);

		#endregion
	}
}
