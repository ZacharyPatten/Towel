using System;
using System.Linq.Expressions;
using Towel.Measurements;

namespace Towel.Mathematics
{
	/// <summary>Represents a vector with an arbitrary number of components of a generic type.</summary>
	/// <typeparam name="T">The numeric type of this Vector.</typeparam>
	[System.Serializable]
	public class Vector<T>
	{
		internal readonly T[] _vector;

		#region Properties

		/// <summary>Sane as accessing index 0.</summary>
		public T X
		{
			get
            {
                if (this.Dimensions < 1)
                {
                    throw new MathematicsException("This vector doesn't have an " + nameof(X) + " component.");
                }
                return _vector[0];
            }
			set
            {
                if (this.Dimensions < 1)
                {
                    throw new MathematicsException("This vector doesn't have an " + nameof(X) + " component.");
                }
                this._vector[0] = value;
            }
		}

		/// <summary>Same as accessing index 1.</summary>
		public T Y
		{
			get
            {
                if (this.Dimensions < 2)
                {
                    throw new MathematicsException("This vector doesn't have an " + nameof(Y) + " component.");
                }
                return _vector[1];
            }
            set
            {
                if (this.Dimensions < 2)
                {
                    throw new MathematicsException("This vector doesn't have an " + nameof(Y) + " component.");
                }
                this._vector[1] = value;
            }
        }

		/// <summary>Same as accessing index 2.</summary>
		public T Z
		{
            get
            {
                if (this.Dimensions < 3)
                {
                    throw new MathematicsException("This vector doesn't have an " + nameof(Z) + " component.");
                }
                return _vector[2];
            }
            set
            {
                if (this.Dimensions < 3)
                {
                    throw new MathematicsException("This vector doesn't have an " + nameof(Z) + " component.");
                }
                this._vector[2] = value;
            }
        }

		/// <summary>The number of components in this vector.</summary>
		public int Dimensions
        {
            get
            {
                return this._vector == null ? 0 : this._vector.Length;
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
                return this._vector[index];
            }
			set
            {
                if (0 > index || index > Dimensions)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Dimensions) + ")");
                }
                this._vector[index] = value;
            }
		}

        /// <summary>Computes the length of this vector.</summary>
		/// <returns>The length of this vector.</returns>
		public T Magnitude
        {
            get
            {
                return Vector_Magnitude(this);
            }
        }
        /// <summary>Computes the length of this vector, but doesn't square root it for 
        /// possible optimization purposes.</summary>
        /// <returns>The squared length of the vector.</returns>
        public T MagnitudeSquared
        {
            get
            {
                return Vector_MagnitudeSquared(this);
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

		#endregion

		#region Factories

		/// <summary>Creates a vector with the given number of components with the values initialized to zeroes.</summary>
		/// <param name="dimensions">The number of components in the vector.</param>
		/// <returns>The newly constructed vector.</returns>
		public static Vector<T> FactoryZero(int dimensions) { return new Vector<T>(dimensions); }

		/// <summary>Creates a vector with the given number of components with the values initialized to ones.</summary>
		/// <param name="dimensions">The number of components in the vector.</param>
		/// <returns>The newly constructed vector.</returns>
		public static Vector<T> FactoryOne(int dimensions) { return new Vector<T>(new T[dimensions]); }

		///// <summary>Returns a 3-component vector representing the x-axis.</summary>
		//public static readonly Vector<T> Factory_XAxis = new Vector<T>(_one, _zero, _zero);
		///// <summary>Returns a 3-component vector representing the y-axis.</summary>
		//public static readonly Vector<T> Factory_YAxis = new Vector<T>(_zero, _one, _zero);
		///// <summary>Returns a 3-component vector representing the z-axis.</summary>
		//public static readonly Vector<T> Factory_ZAxis = new Vector<T>(_zero, _zero, _one);
		///// <summary>Returns a 3-component vector representing the negative x-axis.</summary>
		//public static readonly Vector<T> Factory_NegXAxis = new Vector<T>(_one, _zero, _zero);
		///// <summary>Returns a 3-component vector representing the negative y-axis.</summary>
		//public static readonly Vector<T> Factory_NegYAxis = new Vector<T>(_zero, _one, _zero);
		///// <summary>Returns a 3-component vector representing the negative z-axis.</summary>
		//public static readonly Vector<T> Factory_NegZAxis = new Vector<T>(_zero, _zero, _one);

		#endregion

		#region Operators

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector of the addition.</param>
		/// <param name="right">The second vector of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Vector<T> operator +(Vector<T> left, Vector<T> right)
		{ return Add(left, right); }
		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left operand of the subtraction.</param>
		/// <param name="right">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Vector<T> operator -(Vector<T> left, Vector<T> right)
		{ return Subtract(left, right); }
		/// <summary>Negates a vector.</summary>
		/// <param name="vector">The vector to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Vector<T> operator -(Vector<T> vector)
		{ return Negate(vector); }
		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="left">The vector to have all its values multiplied.</param>
		/// <param name="right">The scalar to multiply all the vector values by.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(Vector<T> left, T right)
		{ return Multiply(left, right); }
		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="left">The scalar to multiply all the vector values by.</param>
		/// <param name="right">The vector to have all its values multiplied.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(T left, Vector<T> right)
		{ return Multiply(right, left); }
		/// <summary>Divides all the values in the vector by a scalar.</summary>
		/// <param name="left">The vector to have its values divided.</param>
		/// <param name="right">The scalar to divide all the vectors values by.</param>
		/// <returns>The vector after the divisions.</returns>
		public static Vector<T> operator /(Vector<T> left, T right)
		{ return Divide(left, right); }
		/// <summary>Does an equality check by value. (warning for float errors)</summary>
		/// <param name="left">The first vector of the equality check.</param>
		/// <param name="right">The second vector of the equality check.</param>
		/// <returns>true if the values are equal, false if not.</returns>
		public static bool operator ==(Vector<T> left, Vector<T> right)
		{ return EqualsValue(left, right); }
		/// <summary>Does an anti-equality check by value. (warning for float errors)</summary>
		/// <param name="left">The first vector of the anit-equality check.</param>
		/// <param name="right">The second vector of the anti-equality check.</param>
		/// <returns>true if the values are not equal, false if they are.</returns>
		public static bool operator !=(Vector<T> left, Vector<T> right)
		{ return !EqualsValue(left, right); }
		/// <summary>Implicit conversions from Vector to T[].</summary>
		/// <param name="vector">The Vector to be converted to a T[].</param>
		/// <returns>The T[] of the vector.</returns>
		public static implicit operator T[](Vector<T> vector)
		{ return vector._vector; }
		/// <summary>Implicit conversions from Vector to T[].</summary>
		/// <param name="array">The Vector to be converted to a T[].</param>
		/// <returns>The T[] of the vector.</returns>
		public static implicit operator Vector<T>(T[] array)
		{ return new Vector<T>(array); }
		/// <summary>Converts a vector into a matrix.</summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>The resulting matrix.</returns>
		public static explicit operator Matrix<T>(Vector<T> vector)
		{ return new Matrix<T>(vector); }
		/// <summary>Implicitly converts a scalar into a one dimensional vector.</summary>
		/// <param name="scalar">The scalar value.</param>
		/// <returns>The one dimensional vector </returns>
		public static implicit operator Vector<T>(T scalar)
		{ return new Vector<T>(scalar); }

		#endregion

		#region Instance Methods

		/// <summary>Adds two vectors together.</summary>
		/// <param name="right">The vector to add to this one.</param>
		/// <returns>The result of the vector.</returns>
		public Vector<T> Add(Vector<T> right)
		{ return Add(this, right); }
		/// <summary>Negates this vector.</summary>
		/// <returns>The result of the negation.</returns>
		public Vector<T> Negate()
		{ return Negate(this); }
		/// <summary>Subtracts another vector from this one.</summary>
		/// <param name="right">The vector to subtract from this one.</param>
		/// <returns>The result of the subtraction.</returns>
		public Vector<T> Subtract(Vector<T> right)
		{ return Subtract(this, right); }
		/// <summary>Multiplies the values in this vector by a scalar.</summary>
		/// <param name="right">The scalar to multiply these values by.</param>
		/// <returns>The result of the multiplications</returns>
		public Vector<T> Multiply(T right)
		{ return Multiply(this, right); }
		/// <summary>Divides all the values in this vector by a scalar.</summary>
		/// <param name="right">The scalar to divide the values of the vector by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public Vector<T> Divide(T right)
		{ return Divide(this, right); }
		/// <summary>Computes the dot product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product.</returns>
		public T DotProduct(Vector<T> right)
		{ return DotProduct(this, right); }
		/// <summary>Computes the cross product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public Vector<T> CrossProduct(Vector<T> right)
		{ return CrossProduct(this, right); }
		/// <summary>Normalizes this vector.</summary>
		/// <returns>The result of the normalization.</returns>
		public Vector<T> Normalize()
		{ return Normalize(this); }
		/// <summary>Check for equality by value.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <returns>true if the values were equal, false if not.</returns>
		public bool EqualsValue(Vector<T> right)
		{ return EqualsValue(this, right); }
		/// <summary>Checks for equality by value with some leniency.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <param name="leniency">The ammount the values can differ but still be considered equal.</param>
		/// <returns>true if the values were cinsidered equal, false if not.</returns>
		public bool EqualsValue(Vector<T> right, T leniency)
		{ return EqualsValue(this, right, leniency); }
		/// <summary>Rotates this vector by quaternon values.</summary>
		/// <param name="angle">The amount of rotation about the axis.</param>
		/// <param name="x">The x component deterniming the axis of rotation.</param>
		/// <param name="y">The y component determining the axis of rotation.</param>
		/// <param name="z">The z component determining the axis of rotation.</param>
		/// <returns>The resulting vector after the rotation.</returns>
		public Vector<T> RotateBy(Angle<T> angle, T x, T y, T z)
		{ return RotateBy(this, angle, x, y, z); }
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public Vector<T> Lerp(Vector<T> right, T blend)
		{ return Lerp(this, right, blend); }
		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public Vector<T> Slerp(Vector<T> right, T blend)
		{ return Slerp(this, right, blend); }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public Vector<T> RotateBy(Quaternion<T> rotation)
		{ return RotateBy(this, rotation); }

		#endregion

		#region Statics

		/// <summary>Adds two vectors together.</summary>
		/// <param name="a">The first vector of the addition.</param>
		/// <param name="b">The second vector of the addiiton.</param>
		/// <returns>The result of the addiion.</returns>
		public static Vector<T> Add(Vector<T> a, Vector<T> b)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_Add(a, b, ref c);
            return c;
        }
		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="a">The vector to have its values negated.</param>
		/// <returns>The result of the negations.</returns>
		public static Vector<T> Negate(Vector<T> a)
		{
            Vector<T> b = new Vector<T>(a.Dimensions);
            Vector_Negate(a, ref b);
            return b;
        }
		/// <summary>Subtracts two vectors.</summary>
		/// <param name="a">The left vector of the subtraction.</param>
		/// <param name="b">The right vector of the subtraction.</param>
		/// <returns>The result of the vector subtracton.</returns>
		public static Vector<T> Subtract(Vector<T> a, Vector<T> b)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_Subtract(a, b, ref c);
            return c;
        }
		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
		/// <param name="a">The vector to have the components multiplied by.</param>
		/// <param name="b">The scalars to multiply the vector components by.</param>
		/// <returns>The result of the multiplications.</returns>
		public static Vector<T> Multiply(Vector<T> a, T b)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_Multiply(a, b, ref c);
            return c;
        }
		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="a">The vector to have the components divided by.</param>
		/// <param name="b">The scalar to divide the vector components by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public static Vector<T> Divide(Vector<T> a, T b)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_Divide(a, b, ref c);
            return c;
        }
		/// <summary>Computes the dot product between two vectors.</summary>
		/// <param name="left">The first vector of the dot product operation.</param>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public static T DotProduct(Vector<T> left, Vector<T> right)
		{
            return Vector<T>.Vector_DotProduct(left._vector, right._vector);
        }
		/// <summary>Computes teh cross product of two vectors.</summary>
		/// <param name="a">The first vector of the cross product operation.</param>
		/// <param name="b">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static Vector<T> CrossProduct(Vector<T> a, Vector<T> b)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_CrossProduct(a, b, ref c);
            return c;
        }
		/// <summary>Normalizes a vector.</summary>
		/// <param name="a">The vector to normalize.</param>
		/// <returns>The result of the normalization.</returns>
		public static Vector<T> Normalize(Vector<T> a)
		{
            Vector<T> b = new Vector<T>(a.Dimensions);
            Vector_Normalize(a, ref b);
            return b;
        }
		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="first">The first vector to determine the angle between.</param>
		/// <param name="second">The second vector to determine the angle between.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static Angle<T> Angle(Vector<T> first, Vector<T> second)
		{
            return Vector<T>.Vector_Angle(first._vector, second._vector);
        }
		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of the rotation.</param>
		/// <param name="x">The x component of the axis vector to rotate about.</param>
		/// <param name="y">The y component of the axis vector to rotate about.</param>
		/// <param name="z">The z component of the axis vector to rotate about.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, Angle<T> angle, T x, T y, T z)
		{
            Vector<T> result = new Vector<T>();
            Vector_RotateBy(vector, angle, x, y, z, ref result);
            return result;
        }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, Quaternion<T> rotation)
		{
            Vector<T> result = new Vector<T>(vector.Dimensions);
            Vector_RotateByQuaternion(vector, rotation, ref result);
            return result;
        }
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="a">The starting vector of the interpolation.</param>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Vector<T> Lerp(Vector<T> a, Vector<T> b, T blend)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_Lerp(a, b, blend, ref c);
            return c;
        }
		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="a">The starting vector of the interpolation.</param>
		/// <param name="b">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public static Vector<T> Slerp(Vector<T> a, Vector<T> b, T blend)
		{
            Vector<T> c = new Vector<T>(a.Dimensions);
            Vector_Slerp(a, b, blend, ref c);
            return c;
        }
		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public static Vector<T> Blerp(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v)
		{
            Vector<T> d = new Vector<T>(a.Dimensions);
            Vector_Blerp(a._vector, b._vector, c._vector, u, v, ref d);
            return d;
        }
		/// <summary>Does a value equality check.</summary>
		/// <param name="a">The first vector to check for equality.</param>
		/// <param name="b">The second vector	to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Vector<T> a, Vector<T> b)
		{
            return Vector<T>.Vector_EqualsValue(a, b);
        }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="a">The first vector to check for equality.</param>
		/// <param name="b">The second vector to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Vector<T> a, Vector<T> b, T leniency)
		{
            return Vector<T>.Vector_EqualsValue_leniency(a, b, leniency);
        }

		#endregion

		#region Implementations

        #region Add

        internal delegate void AddSignature(Vector<T> a, Vector<T> b, ref Vector<T> c);
        internal static AddSignature Vector_Add = (Vector<T> a, Vector<T> b, ref Vector<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            int Length = a.Dimensions;
            if (Length != b.Dimensions)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
            }
            if (c == null || c.Dimensions != Length)
            {
                c = new Vector<T>(Length);
            }
            T[] A = a._vector;
            T[] B = b._vector;
            T[] C = c._vector;
            for (int i = 0; i < Length; i++)
            {
                C[i] = Compute.Add(A[i], B[i]);
            }
		};

        #endregion

        #region Negate

        internal delegate void NegateSignature(Vector<T> a, ref Vector<T> b);
        internal static NegateSignature Vector_Negate = (Vector<T> a, ref Vector<T> b) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            int Length = a.Dimensions;
            if (b == null || b.Dimensions != Length)
            {
                b = new Vector<T>(Length);
            }
            T[] A = a._vector;
            T[] B = b._vector;
            for (int i = 0; i < Length; i++)
            {
                B[i] = Compute.Negate(A[i]);
            }
        };

        #endregion

        #region Subtract

        internal delegate void SubtractSignature(Vector<T> a, Vector<T> b, ref Vector<T> c);
        internal static SubtractSignature Vector_Subtract = (Vector<T> a, Vector<T> b, ref Vector<T> c) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            int Length = a.Dimensions;
            if (Length != b.Dimensions)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
            }
            if (c == null || c.Dimensions != Length)
            {
                c = new Vector<T>(Length);
            }
            T[] A = a._vector;
            T[] B = b._vector;
            T[] C = c._vector;
            for (int i = 0; i < Length; i++)
            {
                C[i] = Compute.Subtract(A[i], B[i]);
            }
        };

        #endregion

        #region Multiply

        internal delegate void MultiplySignature(Vector<T> a, T b, ref Vector<T> c);
        internal static MultiplySignature Vector_Multiply = (Vector<T> a, T b, ref Vector<T> c) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            int Length = a.Dimensions;
            if (c == null || c.Dimensions != Length)
            {
                c = new Vector<T>(Length);
            }
            T[] A = a._vector;
            T[] C = c._vector;
            for (int i = 0; i < Length; i++)
            {
                C[i] = Compute.Multiply(A[i], b);
            }
        };

        #endregion

        #region Divide

        internal delegate void DivideSignature(Vector<T> a, T b, ref Vector<T> c);
        internal static DivideSignature Vector_Divide = (Vector<T> a, T b, ref Vector<T> c) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            int Length = a.Dimensions;
            if (c == null || c.Dimensions != Length)
            {
                c = new Vector<T>(Length);
            }
            T[] A = a._vector;
            T[] C = c._vector;
            for (int i = 0; i < Length; i++)
            {
                C[i] = Compute.Divide(A[i], b);
            }
        };

        #endregion

        #region DotProduct

        internal static Func<Vector<T>, Vector<T>, T> Vector_DotProduct = (Vector<T> a, Vector<T> b) =>
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            int Length = a.Dimensions;
            if (Length != b.Dimensions)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == " + nameof(b) + "." + nameof(b.Dimensions) + ")");
            }
            T result = Compute.Constant<T>.Zero;
            T[] A = a._vector;
            T[] B = b._vector;
            for (int i = 0; i < Length; i++)
            {
                result = Compute.Add(result, Compute.Multiply(A[i], B[i]));
            }
            return result;
        };

        #endregion

        #region CrossProduct

        internal delegate void CrossProductSignature(Vector<T> a, Vector<T> b, ref Vector<T> c);
        internal static CrossProductSignature Vector_CrossProduct = (Vector<T> a, Vector<T> b, ref Vector<T> c) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Dimensions != 3)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " == 3)");
            }
            if (b.Dimensions != 3)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(b) + "." + nameof(b.Dimensions) + " == 3)");
            }
            if (c == null || c.Dimensions != 3)
            {
                c = new Vector<T>(3);
            }
            T[] A = a._vector;
            T[] B = b._vector;
            T[] C = c._vector;
            C[0] = Compute.Subtract(Compute.Multiply(A[1], B[2]), Compute.Multiply(A[2], B[1]));
            C[1] = Compute.Subtract(Compute.Multiply(A[2], B[0]), Compute.Multiply(A[0], B[2]));
            C[2] = Compute.Subtract(Compute.Multiply(A[0], B[1]), Compute.Multiply(A[1], B[0]));
		};

        #endregion

        #region Normalize

        internal delegate void NormalizeSignature(Vector<T> a, ref Vector<T> b);
        internal static NormalizeSignature Vector_Normalize = (Vector<T> a, ref Vector<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            int Dimensions = a.Dimensions;
            if (Dimensions < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(a), a, "!(" + nameof(a) + "." + nameof(a.Dimensions) + " > 0)");
            }
            T magnitude = a.Magnitude;
            if (Compute.Equal(magnitude, Compute.Constant<T>.Zero))
            {
                throw new ArgumentOutOfRangeException(nameof(a), a, "!(" + nameof(a) + "." + nameof(a.Magnitude) + " > 0)");
            }
            if (b.Dimensions != Dimensions)
            {
                b = new Vector<T>(Dimensions);
            }
            for (int i = 0; i < Dimensions; i++)
            {
                b[i] = Compute.Divide(a[i], magnitude);
            }
		};

		#endregion

		#region Magnitude
		
		internal static Func<Vector<T>, T> Vector_Magnitude = (Vector<T> a) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return Compute.SquareRoot(Vector_MagnitudeSquared(a));
		};

		#endregion

		#region MagnitudeSquared
		
		internal static Func<Vector<T>, T> Vector_MagnitudeSquared = (Vector<T> a) =>
		{
            if (a._vector == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            int Length = a.Dimensions;
            T result = Compute.Constant<T>.Zero;
            T[] A = a._vector;
            for (int i = 0; i < Length; i++)
            {
                result = Compute.Add(result, Compute.Multiply(A[i], A[i]));
            }
            return result;
		};

		#endregion

		#region Angle
		
		internal static Func<Vector<T>, Vector<T>, Angle<T>> Vector_Angle = (Vector<T> a, Vector<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            throw new NotImplementedException();
		};

        #endregion

        #region RotateBy

        internal delegate void RotateBySignature(Vector<T> a, Angle<T> angle, T x, T y, T z, ref Vector<T> c);
        internal static RotateBySignature Vector_RotateBy = (Vector<T> a, Angle<T> angle, T x, T y, T z, ref Vector<T> c) =>
		{
			throw new NotImplementedException();

			//#region pre-optimization

			//#endregion

			//Vector<generic>.Delegates.Vector_Angle compile_testing =
			//	#region code (compile testing)

			//	#endregion

			//string Vector_RotateBy_string =
			//	#region code (string)

			//	#endregion

			//Vector_RotateBy_string = Vector_RotateBy_string.Replace("generic", Generate.ToSourceString(typeof(T)));

			//Vector<T>.Vector_RotateBy =
			//	Generate.Object<Vector<T>.Delegates.Vector_RotateBy>(Vector_RotateBy_string);

			//return Vector<T>.Vector_RotateBy(vector, angle, x, y, z);
		};
        #endregion

        #region Lerp

        internal delegate void LerpSignature(Vector<T> a, Vector<T> b, T blend, ref Vector<T> c);
        internal static LerpSignature Vector_Lerp = (Vector<T> a, Vector<T> b, T blend, ref Vector<T> c) =>
		{
            if (Compute.LessThan(blend, Compute.Constant<T>.Zero) || Compute.GreaterThan(blend, Compute.Constant<T>.One))
            {
                throw new ArgumentOutOfRangeException(nameof(blend), blend, "!(0 <= " + nameof(blend) + " <= 1)");
            }
            int Length = a.Dimensions;
            if (Length != b.Dimensions)
            {
                throw new MathematicsException("Arguments invalid !(" + nameof(a) + "." + nameof(a.Dimensions) + " ==" + nameof(b) + "." + nameof(b.Dimensions) + ")");
            }
            if (c == null || c.Dimensions != Length)
            {
                c = new Vector<T>(Length);
            }
            T[] A = a._vector;
            T[] B = b._vector;
            T[] C = c._vector;
            for (int i = 0; i < Length; i++)
            {
                C[i] = Compute.Add(A[i], Compute.Multiply(blend, Compute.Subtract(B[i], A[i])));
            }
		};
        #endregion

        #region Slerp

        internal delegate void SlerpSignature(Vector<T> a, Vector<T> b, T blend, ref Vector<T> c);
        internal static SlerpSignature Vector_Slerp = (Vector<T> a, Vector<T> b, T blend, ref Vector<T> C) =>
		{
			throw new NotImplementedException();

			//#region pre-optimization

			//#endregion

			//Vector<generic>.Delegates.Vector_Angle compile_testing =
			//	#region code (compile testing)

			//	#endregion

			//string Vector_Slerp_string =
			//	#region code (string)

			//	#endregion

			//Vector_Slerp_string = Vector_Slerp_string.Replace("generic", Generate.ToSourceString(typeof(T)));

			//Vector<T>.Vector_Slerp =
			//	Generate.Object<Vector<T>.Delegates.Vector_Slerp>(Vector_Slerp_string);

			//return Vector<T>.Vector_Slerp(left, right, blend);
		};

        #endregion

        #region Blerp

        internal delegate void BlerpSignature(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v, ref Vector<T> d);
        internal static BlerpSignature Vector_Blerp = (Vector<T> a, Vector<T> b, Vector<T> c, T u, T v, ref Vector<T> d) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (c == null)
            {
                throw new ArgumentNullException(nameof(c));
            }
            d = a + (u * (b - a)) + (v * (c - a));
		};

		#endregion

		#region EqualsValue
		
		internal static Func<Vector<T>, Vector<T>, bool> Vector_EqualsValue = (Vector<T> a, Vector<T> b) =>
		{
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            else if (object.ReferenceEquals(a, null))
            {
                return false;
            }
            else if (object.ReferenceEquals(b, null))
            {
                return false;
            }
            else
            {
                int Length = a.Dimensions;
                if (Length != b.Dimensions)
                    return false;
                T[] A = a._vector;
                T[] B = b._vector;
                for (int i = 0; i < Length; i++)
                    if (Compute.Equal(A[i], B[i]))
                        return false;
                return true;
            }
        };

		#endregion

		#region EqualsValue_leniency
		
		internal static Func<Vector<T>, Vector<T>, T, bool> Vector_EqualsValue_leniency = (Vector<T> a, Vector<T> b, T leniency) =>
		{
            if (a == null && b == null)
                return true;
            if (a == null)
                return false;
            if (b == null)
                return false;
            int Length = a.Dimensions;
            if (Length != b.Dimensions)
                return false;
            T[] A = a._vector;
            T[] B = b._vector;
            for (int i = 0; i < Length; i++)
                if (Compute.EqualLeniency(A[i], B[i], leniency))
                    return false;
            return true;
		};

        #endregion

        #region RotateByQuaternion

        internal delegate void RotateByQuaternionSignature(Vector<T> a, Quaternion<T> b, ref Vector<T> c);
        internal static RotateByQuaternionSignature Vector_RotateByQuaternion = (Vector<T> a, Quaternion<T> b, ref Vector<T> c) =>
		{
			Quaternion<T>.Quaternion_Rotate(b, a, ref c);
		};

		#endregion

		#endregion

		#region Steppers

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step_function)
		{
			for (int i = 0; i < this._vector.Length; i++)
				step_function(this._vector[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step_function)
		{
			for (int i = 0; i < this._vector.Length; i++)
				step_function(ref this._vector[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step_function)
		{
			for (int i = 0; i < this._vector.Length; i++)
				if (step_function(this._vector[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step_function)
		{
			for (int i = 0; i < this._vector.Length; i++)
				if (step_function(ref this._vector[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		#endregion

		#region Overrides

		/// <summary>Prints out a string representation of this matrix.</summary>
		/// <returns>A string representing this matrix.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			str.Append("[");
			str.Append(this._vector[0]);
			for (int i = 1; i < this.Dimensions; i++)
			{
				str.Append(", ");
				str.Append(this._vector[i]);
			}
			str.Append("]");
			return str.ToString(); 
		}
		/// <summary>Computes a hash code from the values of this matrix.</summary>
		/// <returns>A hash code for the matrix.</returns>
		public override int GetHashCode()
		{
			int hash = this._vector[0].GetHashCode();
            for (int i = 1; i < this._vector.Length; i++)
            {
                hash ^= this._vector[i].GetHashCode();
            }
			return hash;
		}
		/// <summary>Does an equality check by reference.</summary>
		/// <param name="right">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object right)
		{
            if (!(right is Vector<T>))
            {
                return false;
            }
            else
            {
                return Vector<T>.EqualsValue(this, (Vector<T>)right);
            }
		}

		#endregion
	}
}
