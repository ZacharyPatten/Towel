using System.Linq.Expressions;

namespace Towel.Mathematics
{
	/// <summary>Represents a vector with an arbitrary number of components of a generic type.</summary>
	/// <typeparam name="T">The numeric type of this Vector.</typeparam>
	[System.Serializable]
	public class Vector<T>
	{
		#region delegates

		public class Delegates
		{

			/// <summary>Adds two vectors together.</summary>
			/// <param name="left">The first vector of the addition.</param>
			/// <param name="right">The second vector of the addiiton.</param>
			/// <returns>The result of the addiion.</returns>
			public delegate Vector<T> Vector_Add(Vector<T> left, Vector<T> right);
			/// <summary>Negates all the values in a vector.</summary>
			/// <param name="vector">The vector to have its values negated.</param>
			/// <returns>The result of the negations.</returns>
			public delegate Vector<T> Vector_Negate(Vector<T> vector);
			/// <summary>Subtracts two vectors.</summary>
			/// <param name="left">The left vector of the subtraction.</param>
			/// <param name="right">The right vector of the subtraction.</param>
			/// <returns>The result of the vector subtracton.</returns>
			public delegate Vector<T> Vector_Subtract(Vector<T> left, Vector<T> right);
			/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
			/// <param name="left">The vector to have the components multiplied by.</param>
			/// <param name="right">The scalars to multiply the vector components by.</param>
			/// <returns>The result of the multiplications.</returns>
			public delegate Vector<T> Vector_Multiply(Vector<T> left, T right);
			/// <summary>Divides all the components of a vector by a scalar.</summary>
			/// <param name="left">The vector to have the components divided by.</param>
			/// <param name="right">The scalar to divide the vector components by.</param>
			/// <returns>The resulting vector after teh divisions.</returns>
			public delegate Vector<T> Vector_Divide(Vector<T> vector, T right);
			/// <summary>Computes the dot product between two vectors.</summary>
			/// <param name="left">The first vector of the dot product operation.</param>
			/// <param name="right">The second vector of the dot product operation.</param>
			/// <returns>The result of the dot product operation.</returns>
			public delegate T Vector_DotProduct(Vector<T> left, Vector<T> right);
			/// <summary>Computes teh cross product of two vectors.</summary>
			/// <param name="left">The first vector of the cross product operation.</param>
			/// <param name="right">The second vector of the cross product operation.</param>
			/// <returns>The result of the cross product operation.</returns>
			public delegate Vector<T> Vector_CrossProduct(Vector<T> left, Vector<T> right);
			/// <summary>Normalizes a vector.</summary>
			/// <param name="vector">The vector to normalize.</param>
			/// <returns>The result of the normalization.</returns>
			public delegate Vector<T> Vector_Normalize(Vector<T> vector);
			/// <summary>Computes the length of a vector.</summary>
			/// <param name="vector">The vector to calculate the length of.</param>
			/// <returns>The computed length of the vector.</returns>
			public delegate T Vector_Magnitude(Vector<T> vector);
			/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
			/// <param name="vector">The vector to compute the length squared of.</param>
			/// <returns>The computed length squared of the vector.</returns>
			public delegate T Vector_MagnitudeSquared(Vector<T> vector);
			/// <summary>Computes the angle between two vectors.</summary>
			/// <param name="first">The first vector to determine the angle between.</param>
			/// <param name="second">The second vector to determine the angle between.</param>
			/// <returns>The angle between the two vectors in radians.</returns>
			public delegate T Vector_Angle(Vector<T> first, Vector<T> second);
			/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
			/// <param name="vector">The vector to rotate.</param>
			/// <param name="angle">The angle of the rotation.</param>
			/// <param name="x">The x component of the axis vector to rotate about.</param>
			/// <param name="y">The y component of the axis vector to rotate about.</param>
			/// <param name="z">The z component of the axis vector to rotate about.</param>
			/// <returns>The result of the rotation.</returns>
			public delegate Vector<T> Vector_RotateBy(Vector<T> vector, T angle, T x, T y, T z);
			/// <summary>Computes the linear interpolation between two vectors.</summary>
			/// <param name="left">The starting vector of the interpolation.</param>
			/// <param name="right">The ending vector of the interpolation.</param>
			/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
			/// <returns>The result of the interpolation.</returns>
			public delegate Vector<T> Vector_Lerp(Vector<T> left, Vector<T> right, T blend);
			/// <summary>Sphereically interpolates between two vectors.</summary>
			/// <param name="left">The starting vector of the interpolation.</param>
			/// <param name="right">The ending vector of the interpolation.</param>
			/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
			/// <returns>The result of the slerp operation.</returns>
			public delegate Vector<T> Vector_Slerp(Vector<T> left, Vector<T> right, T blend);
			/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
			/// <param name="a">The first vector of the interpolation.</param>
			/// <param name="b">The second vector of the interpolation.</param>
			/// <param name="c">The thrid vector of the interpolation.</param>
			/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
			/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
			/// <returns>The resulting vector of the barycentric interpolation.</returns>
			public delegate Vector<T> Vector_Blerp(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v);
			/// <summary>Does a value equality check.</summary>
			/// <param name="left">The first vector to check for equality.</param>
			/// <param name="right">The second vector to check for equality.</param>
			/// <returns>True if values are equal, false if not.</returns>
			public delegate bool Vector_EqualsValue(Vector<T> left, Vector<T> right);
			/// <summary>Does a value equality check with leniency.</summary>
			/// <param name="left">The first vector to check for equality.</param>
			/// <param name="right">The second vector to check for equality.</param>
			/// <param name="leniency">How much the values can vary but still be considered equal.</param>
			/// <returns>True if values are equal, false if not.</returns>
			public delegate bool Vector_EqualsValue_leniency(Vector<T> left, Vector<T> right, T leniency);
			/// <summary>Rotates a vector by a quaternion.</summary>
			/// <param name="vector">The vector to rotate.</param>
			/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
			/// <returns>The result of the rotation.</returns>
			public delegate Vector<T> Vector_RotateBy_quaternion(Vector<T> vector, Quaternion<T> rotation);

		}

		#endregion

		/// <summary>The array of this vector.</summary>
		internal readonly T[] _vector;

		#region properties

		/// <summary>Sane as accessing index 0.</summary>
		public T X
		{
			get { return _vector[0]; }
			set { _vector[0] = value; }
		}

		/// <summary>Same as accessing index 1.</summary>
		public T Y
		{
			get { try { return _vector[1]; } catch { throw new System.ArgumentOutOfRangeException("vector does not contains a y component."); } }
			set { try { _vector[1] = value; } catch { throw new System.ArgumentOutOfRangeException("vector does not contains a y component."); } }
		}

		/// <summary>Same as accessing index 2.</summary>
		public T Z
		{
			get { try { return _vector[2]; } catch { throw new System.ArgumentOutOfRangeException("vector does not contains a z component."); } }
			set { try { _vector[2] = value; } catch { throw new System.ArgumentOutOfRangeException("vector does not contains a z component."); } }
		}

		/// <summary>Same as accessing index 3.</summary>
		public T W
		{
			get { try { return _vector[3]; } catch { throw new System.ArgumentOutOfRangeException("vector does not contains a w component."); } }
			set { try { _vector[3] = value; } catch { throw new System.ArgumentOutOfRangeException("vector does not contains a w component."); } }
		}

		/// <summary>The number of components in this vector.</summary>
		public int Dimensions { get { return _vector.Length; } }

		/// <summary>Allows indexed access to this vector.</summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The value of the given index.</returns>
		public T this[int index]
		{
			get { try { return _vector[index]; } catch { throw new System.ArgumentOutOfRangeException("index out of bounds."); } }
			set { try { _vector[index] = value; } catch { throw new System.ArgumentOutOfRangeException("index out of bounds."); } }
		}

		#endregion

		#region constructors

		/// <summary>Creates a new vector with the given number of components.</summary>
		/// <param name="dimensions">The number of dimensions this vector will have.</param>
		public Vector(int dimensions)
		{
			if (dimensions < 1)
				throw new System.ArithmeticException("vectors must have 1 or greater dimensions");
			_vector = new T[dimensions];
		}

		/// <summary>Creates a vector out of the given values.</summary>
		/// <param name="vector">The values to initialize the vector to.</param>
		public Vector(params T[] vector)
		{
			if (vector.Length < 1)
				throw new System.ArithmeticException("vectors must have 1 or greater dimensions");
			_vector = vector;
		}

		#endregion

		#region factories

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

		#region operator

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector of the addition.</param>
		/// <param name="right">The second vector of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Vector<T> operator +(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_Add(left, right); }
		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left operand of the subtraction.</param>
		/// <param name="right">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Vector<T> operator -(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_Subtract(left, right); }
		/// <summary>Negates a vector.</summary>
		/// <param name="vector">The vector to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Vector<T> operator -(Vector<T> vector)
		{ return Vector<T>.Vector_Negate(vector); }
		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="left">The vector to have all its values multiplied.</param>
		/// <param name="right">The scalar to multiply all the vector values by.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(Vector<T> left, T right)
		{ return Vector<T>.Vector_Multiply(left, right); }
		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="left">The scalar to multiply all the vector values by.</param>
		/// <param name="right">The vector to have all its values multiplied.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(T left, Vector<T> right)
		{ return Vector<T>.Vector_Multiply(right, left); }
		/// <summary>Divides all the values in the vector by a scalar.</summary>
		/// <param name="left">The vector to have its values divided.</param>
		/// <param name="right">The scalar to divide all the vectors values by.</param>
		/// <returns>The vector after the divisions.</returns>
		public static Vector<T> operator /(Vector<T> left, T right)
		{ return Vector<T>.Vector_Divide(left, right); }
		/// <summary>Does an equality check by value. (warning for float errors)</summary>
		/// <param name="left">The first vector of the equality check.</param>
		/// <param name="right">The second vector of the equality check.</param>
		/// <returns>true if the values are equal, false if not.</returns>
		public static bool operator ==(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_EqualsValue(left, right); }
		/// <summary>Does an anti-equality check by value. (warning for float errors)</summary>
		/// <param name="left">The first vector of the anit-equality check.</param>
		/// <param name="right">The second vector of the anti-equality check.</param>
		/// <returns>true if the values are not equal, false if they are.</returns>
		public static bool operator !=(Vector<T> left, Vector<T> right)
		{ return !Vector<T>.Vector_EqualsValue(left, right); }
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

		#region instance

		/// <summary>Adds two vectors together.</summary>
		/// <param name="right">The vector to add to this one.</param>
		/// <returns>The result of the vector.</returns>
		public Vector<T> Add(Vector<T> right)
		{ return Vector<T>.Vector_Add(this, right); }
		/// <summary>Negates this vector.</summary>
		/// <returns>The result of the negation.</returns>
		public Vector<T> Negate()
		{ return Vector<T>.Vector_Negate(this); }
		/// <summary>Subtracts another vector from this one.</summary>
		/// <param name="right">The vector to subtract from this one.</param>
		/// <returns>The result of the subtraction.</returns>
		public Vector<T> Subtract(Vector<T> right)
		{ return Vector<T>.Vector_Subtract(this, right); }
		/// <summary>Multiplies the values in this vector by a scalar.</summary>
		/// <param name="right">The scalar to multiply these values by.</param>
		/// <returns>The result of the multiplications</returns>
		public Vector<T> Multiply(T right)
		{ return Vector<T>.Vector_Multiply(this, right); }
		/// <summary>Divides all the values in this vector by a scalar.</summary>
		/// <param name="right">The scalar to divide the values of the vector by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public Vector<T> Divide(T right)
		{ return Vector<T>.Vector_Divide(this, right); }
		/// <summary>Computes the dot product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product.</returns>
		public T DotProduct(Vector<T> right)
		{ return Vector<T>.Vector_DotProduct(this, right); }
		/// <summary>Computes the cross product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public Vector<T> CrossProduct(Vector<T> right)
		{ return Vector<T>.Vector_CrossProduct(this, right); }
		/// <summary>Normalizes this vector.</summary>
		/// <returns>The result of the normalization.</returns>
		public Vector<T> Normalize()
		{ return Vector<T>.Vector_Normalize(this); }
		/// <summary>Computes the length of this vector.</summary>
		/// <returns>The length of this vector.</returns>
		public T Magnitude()
		{ return Vector<T>.Vector_Magnitude(this); }
		/// <summary>Computes the length of this vector, but doesn't square root it for 
		/// possible optimization purposes.</summary>
		/// <returns>The squared length of the vector.</returns>
		public T MagnitudeSquared()
		{ return Vector<T>.Vector_MagnitudeSquared(this); }
		/// <summary>Check for equality by value.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <returns>true if the values were equal, false if not.</returns>
		public bool EqualsValue(Vector<T> right)
		{ return Vector<T>.Vector_EqualsValue(this, right); }
		/// <summary>Checks for equality by value with some leniency.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <param name="leniency">The ammount the values can differ but still be considered equal.</param>
		/// <returns>true if the values were cinsidered equal, false if not.</returns>
		public bool EqualsValue(Vector<T> right, T leniency)
		{ return Vector<T>.Vector_EqualsValue_leniency(this, right, leniency); }
		/// <summary>Checks for equality by reference.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <returns>true if the references are equal, false if not.</returns>
		public bool EqualsReference(Vector<T> right)
		{ return Vector<T>.EqualsReference(this, right); }
		/// <summary>Rotates this vector by quaternon values.</summary>
		/// <param name="angle">The amount of rotation about the axis.</param>
		/// <param name="x">The x component deterniming the axis of rotation.</param>
		/// <param name="y">The y component determining the axis of rotation.</param>
		/// <param name="z">The z component determining the axis of rotation.</param>
		/// <returns>The resulting vector after the rotation.</returns>
		public Vector<T> RotateBy(T angle, T x, T y, T z)
		{ return Vector<T>.Vector_RotateBy(this, angle, x, y, z); }
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public Vector<T> Lerp(Vector<T> right, T blend)
		{ return Vector<T>.Vector_Lerp(this, right, blend); }
		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public Vector<T> Slerp(Vector<T> right, T blend)
		{ return Vector<T>.Vector_Slerp(this, right, blend); }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public Vector<T> RotateBy(Quaternion<T> rotation)
		{ return Vector<T>.Vector_RotateBy_quaternion(this, rotation); }

		#endregion

		#region static

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector of the addition.</param>
		/// <param name="right">The second vector of the addiiton.</param>
		/// <returns>The result of the addiion.</returns>
		public static Vector<T> Add(T[] left, T[] right)
		{ return Vector<T>.Vector_Add(left, right); }
		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector of the addition.</param>
		/// <param name="right">The second vector of the addiiton.</param>
		/// <returns>The result of the addiion.</returns>
		public static Vector<T> Add(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_Add(left._vector, right._vector); }
		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="vector">The vector to have its values negated.</param>
		/// <returns>The result of the negations.</returns>
		public static T[] Negate(T[] vector)
		{ return Vector<T>.Vector_Negate(vector); }
		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="vector">The vector to have its values negated.</param>
		/// <returns>The result of the negations.</returns>
		public static Vector<T> Negate(Vector<T> vector)
		{ return Vector<T>.Vector_Negate(vector); }
		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left vector of the subtraction.</param>
		/// <param name="right">The right vector of the subtraction.</param>
		/// <returns>The result of the vector subtracton.</returns>
		public static T[] Subtract(T[] left, T[] right)
		{ return Vector<T>.Vector_Subtract(left, right); }
		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left vector of the subtraction.</param>
		/// <param name="right">The right vector of the subtraction.</param>
		/// <returns>The result of the vector subtracton.</returns>
		public static Vector<T> Subtract(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_Subtract(left._vector, right._vector); }
		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
		/// <param name="left">The vector to have the components multiplied by.</param>
		/// <param name="right">The scalars to multiply the vector components by.</param>
		/// <returns>The result of the multiplications.</returns>
		public static T[] Multiply(T[] left, T right)
		{ return Vector<T>.Vector_Multiply(left, right); }
		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
		/// <param name="left">The vector to have the components multiplied by.</param>
		/// <param name="right">The scalars to multiply the vector components by.</param>
		/// <returns>The result of the multiplications.</returns>
		public static Vector<T> Multiply(Vector<T> left, T right)
		{ return Vector<T>.Vector_Multiply(left._vector, right); }
		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="left">The vector to have the components divided by.</param>
		/// <param name="right">The scalar to divide the vector components by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public static T[] Divide(T[] left, T right)
		{ return Vector<T>.Vector_Divide(left, right); }
		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="left">The vector to have the components divided by.</param>
		/// <param name="right">The scalar to divide the vector components by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public static Vector<T> Divide(Vector<T> left, T right)
		{ return Vector<T>.Vector_Divide(left, right); }
		/// <summary>Computes the dot product between two vectors.</summary>
		/// <param name="left">The first vector of the dot product operation.</param>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public static T DotProduct(T[] left, T[] right)
		{ return Vector<T>.Vector_DotProduct(left, right); }
		/// <summary>Computes the dot product between two vectors.</summary>
		/// <param name="left">The first vector of the dot product operation.</param>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public static T DotProduct(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_DotProduct(left._vector, right._vector); }
		/// <summary>Computes teh cross product of two vectors.</summary>
		/// <param name="left">The first vector of the cross product operation.</param>
		/// <param name="right">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static T[] CrossProduct(T[] left, T[] right)
		{ return Vector<T>.Vector_CrossProduct(left, right); }
		/// <summary>Computes teh cross product of two vectors.</summary>
		/// <param name="left">The first vector of the cross product operation.</param>
		/// <param name="right">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static Vector<T> CrossProduct(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_CrossProduct(left, right); }
		/// <summary>Normalizes a vector.</summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The result of the normalization.</returns>
		public static T[] Normalize(T[] vector)
		{ return Vector<T>.Vector_Normalize(vector); }
		/// <summary>Normalizes a vector.</summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The result of the normalization.</returns>
		public static Vector<T> Normalize(Vector<T> vector)
		{ return Vector<T>.Vector_Normalize(vector._vector); }
		/// <summary>Computes the length of a vector.</summary>
		/// <param name="vector">The vector to calculate the length of.</param>
		/// <returns>The computed length of the vector.</returns>
		public static T Magnitude(T[] vector)
		{ return Vector<T>.Vector_Magnitude(vector); }
		/// <summary>Computes the length of a vector.</summary>
		/// <param name="vector">The vector to calculate the length of.</param>
		/// <returns>The computed length of the vector.</returns>
		public static T Magnitude(Vector<T> vector)
		{ return Vector<T>.Vector_Magnitude(vector); }
		/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
		/// <param name="vector">The vector to compute the length squared of.</param>
		/// <returns>The computed length squared of the vector.</returns>
		public static T MagnitudeSquared(T[] vector)
		{ return Vector<T>.Vector_MagnitudeSquared(vector); }
		/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
		/// <param name="vector">The vector to compute the length squared of.</param>
		/// <returns>The computed length squared of the vector.</returns>
		public static T MagnitudeSquared(Vector<T> vector)
		{ return Vector<T>.Vector_MagnitudeSquared(vector._vector); }
		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="first">The first vector to determine the angle between.</param>
		/// <param name="second">The second vector to determine the angle between.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static T Angle(T[] first, T[] second)
		{ return Vector<T>.Vector_Angle(first, second); }
		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="first">The first vector to determine the angle between.</param>
		/// <param name="second">The second vector to determine the angle between.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static T Angle(Vector<T> first, Vector<T> second)
		{ return Vector<T>.Vector_Angle(first._vector, second._vector); }
		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of the rotation.</param>
		/// <param name="x">The x component of the axis vector to rotate about.</param>
		/// <param name="y">The y component of the axis vector to rotate about.</param>
		/// <param name="z">The z component of the axis vector to rotate about.</param>
		/// <returns>The result of the rotation.</returns>
		public static T[] RotateBy(T[] vector, T angle, T x, T y, T z)
		{ return Vector<T>.Vector_RotateBy(vector, angle, x, y, z); }
		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of the rotation.</param>
		/// <param name="x">The x component of the axis vector to rotate about.</param>
		/// <param name="y">The y component of the axis vector to rotate about.</param>
		/// <param name="z">The z component of the axis vector to rotate about.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, T angle, T x, T y, T z)
		{ return Vector<T>.Vector_RotateBy(vector._vector, angle, x, y, z); }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, Quaternion<T> rotation)
		{ return Vector<T>.Vector_RotateBy_quaternion(vector._vector, rotation); }
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public static T[] Lerp(T[] left, T[] right, T blend)
		{ return Vector<T>.Vector_Lerp(left, right, blend); }
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Vector<T> Lerp(Vector<T> left, Vector<T> right, T blend)
		{ return Vector<T>.Vector_Lerp(left._vector, right._vector, blend); }
		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public static T[] Slerp(T[] left, T[] right, T blend)
		{ return Vector<T>.Vector_Slerp(left, right, blend); }
		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public static Vector<T> Slerp(Vector<T> left, Vector<T> right, T blend)
		{ return Vector<T>.Vector_Slerp(left._vector, right._vector, blend); }
		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public static T[] Blerp(T[] a, T[] b, T[] c, T u, T v)
		{ return Vector<T>.Vector_Blerp(a, b, c, u, v); }
		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public static Vector<T> Blerp(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v)
		{ return Vector<T>.Vector_Blerp(a._vector, b._vector, c._vector, u, v); }
		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector	to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(T[] left, T[] right)
		{ return Vector<T>.Vector_EqualsValue(left, right); }
		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector	to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Vector<T> left, Vector<T> right)
		{ return Vector<T>.Vector_EqualsValue(left._vector, right._vector); }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(T[] left, T[] right, T leniency)
		{ return Vector<T>.Vector_EqualsValue_leniency(left, right, leniency); }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Vector<T> left, Vector<T> right, T leniency)
		{ return Vector<T>.Vector_EqualsValue_leniency(left._vector, right._vector, leniency); }
		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left vector of the equality check.</param>
		/// <param name="right">The right vector of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(T[] left, T[] right)
		{ return object.ReferenceEquals(left, right); }
		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left vector of the equality check.</param>
		/// <param name="right">The right vector of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(Vector<T> left, Vector<T> right)
		{ return EqualsReference(left._vector, right._vector); }

		#endregion

		#region implementations

		#region T_Source

		private static string t_source = null;
		private static string T_Source
		{
			get
			{
				if (t_source != null)
					return t_source;
				return t_source = Meta.ConvertTypeToCsharpSource(typeof(T));
			}
		}

		#endregion

		#region Add
		/// <summary>Adds two vectors together.</summary>
		private static Vector<T>.Delegates.Vector_Add Vector_Add = (Vector<T> left, Vector<T> right) =>
		{
			// compilation
			Vector<T>.Vector_Add =
					Meta.Compile<Vector<T>.Delegates.Vector_Add>(
						string.Concat(
@"(Vector<", T_Source, "> _left, Vector<", T_Source, @"> _right) =>
{
 	if (object.ReferenceEquals(_left, null))
			throw new System.ArithmeticException(", "\"null reference: _left\"", @");
	if (object.ReferenceEquals(_right, null))
			throw new System.ArithmeticException(", "\"null reference: _right\"", @");
	if (_left.Dimensions != _right.Dimensions)
			throw new System.ArithmeticException(", "\"invalid dimensions on vector addition.\"", @");
 	int length = _left.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(_left.Dimensions);
	", T_Source, @"[] _left_flat = _left._vector;
	", T_Source, @"[] _right_flat = _right._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
		result_flat[i] = _left_flat[i] + _right_flat[i];
	return result;
}"));
			// recursive invocation
			return Vector<T>.Vector_Add(left, right);

			#region Alternat Version
			//if (object.ReferenceEquals(left, null))
			//	throw new System.Exception("null reference: left");
			//if (object.ReferenceEquals(right, null))
			//	throw new System.Exception("null reference: right");
			//if (left.Dimensions != right.Dimensions)
			//	throw new System.Exception("invalid dimensions on vector addition.");
			//T[] _left = left._vector;
			//T[] _right = right._vector;
			//int dimensions = left.Dimensions;
			//Vector<T> result =
			//	new Vector<T>(left.Dimensions);
			//T[] _result = result._vector;
			//for (int i = 0; i < dimensions; i++)
			//	_result[i] = Compute<T>.Add(_left[i], _right[i]);
			//return result;
			#endregion

			#region Alternate Version
			//// shared
			//ParameterExpression result = Expression.Variable(typeof(Vector<T>));
			//ParameterExpression i = Expression.Variable(typeof(int));
			//ParameterExpression end = Expression.Variable(typeof(int));
			//LabelTarget breakLabel = Expression.Label();
			//// build operaion
			//Vector<T>.Vector_Add3 =
			//	Meta.BinaryOperationHelper<Vector<T>.Delegates.Vector_Add, Vector<T>, Vector<T>, Vector<T>>(
			//		(Expression _left, Expression _right, LabelTarget returnLabel) =>
			//		{
			//			return Expression.Block(
			//				new ParameterExpression[] { result, i, end },
			//				Expression.IfThen(
			//					Expression.NotEqual(Expression.MakeMemberAccess(_left, typeof(Vector<T>).GetMember("Dimensions")[0]), Expression.MakeMemberAccess(_right, typeof(Vector<T>).GetMember("Dimensions")[0])),
			//					Expression.Throw(Expression.New(typeof(System.ArithmeticException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("invalid dimensions during vector addition")))),
			//				Expression.Assign(result, Expression.New(typeof(Vector<T>).GetConstructor(new System.Type[] { typeof(int) }), Expression.MakeMemberAccess(_left, typeof(Vector<T>).GetMember("Dimensions")[0]))),
			//				Expression.Assign(end, Expression.MakeMemberAccess(_left, typeof(Vector<T>).GetMember("Dimensions")[0])),
			//				Expression.Assign(i, Expression.Constant(0)),
			//				Expression.Loop(
			//					Expression.Block(
			//						Expression.IfThen(
			//							Expression.Equal(i, end),
			//							Expression.Goto(breakLabel)),
			//						Expression.Assign(
			//							Expression.Property(result, typeof(Vector<T>).GetProperty("Item"), i),
			//								Expression.Multiply(
			//									Expression.Property(_left, typeof(Vector<T>).GetProperty("Item"), i),
			//									Expression.Property(_right, typeof(Vector<T>).GetProperty("Item"), i))),
			//						Expression.Assign(i, Expression.Increment(i)))),
			//				Expression.Label(breakLabel),
			//				Expression.Return(returnLabel, result));
			//		});
			//// recursive invoke
			//return Vector<T>.Vector_Add3(left, right);
			#endregion
		};
		#endregion

		#region Negate
		/// <summary>Negates all the values in a vector.</summary>
		private static Vector<T>.Delegates.Vector_Negate Vector_Negate = (Vector<T> vector) =>
		{
			Vector<T>.Vector_Negate =
					Meta.Compile<Vector<T>.Delegates.Vector_Negate>(
						string.Concat(
@"(Vector<", T_Source, @"> _vector) =>
{
 	if (object.ReferenceEquals(_vector, null))
		throw new System.Exception(", "\"null reference: vector\"", @");
 	int length = _vector.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(length);
	", T_Source, @"[] vector_flat = _vector._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
		result_flat[i] = -vector_flat[i];
	return result;
}"));

			return Vector<T>.Vector_Negate(vector);

			#region Alternate Version
			//if (object.ReferenceEquals(vector, null))
			//	throw new System.Exception("null reference: vector");
			//Vector<T> result =
			//	new Vector<T>(vector.Dimensions);
			//for (int i = 0; i < vector.Dimensions; i++)
			//	result[i] = Vector<T>.Negate(vector[i]);
			//return result;
			#endregion
		};
		#endregion

		#region Subtract
		/// <summary>Subtracts two vectors.</summary>
		private static Vector<T>.Delegates.Vector_Subtract Vector_Subtract = (Vector<T> left, Vector<T> right) =>
		{
			Vector<T>.Vector_Subtract =
				Meta.Compile<Vector<T>.Delegates.Vector_Subtract>(
					string.Concat(
@"(Vector<", T_Source, "> _left, Vector<", T_Source, @"> _right) =>
{
	if (object.ReferenceEquals(_left, null))
		throw new System.Exception(", "\"null reference: _left\"", @");
	if (object.ReferenceEquals(_right, null))
		throw new System.Exception(", "\"null reference: _right\"", @");
	if (_left.Dimensions != _right.Dimensions)
		throw new System.Exception(", "\"invalid dimensions on vector subtraction.\"", @");
	int length = _left.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(length);
	", T_Source, @"[] _left_flat = _left._vector;
	", T_Source, @"[] _right_flat = _right._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
		result_flat[i] = _left_flat[i] - _right_flat[i];
	return result;
}"));

			return Vector<T>.Vector_Subtract(left, right);

			#region Alternate Version
			//if (object.ReferenceEquals(left, null))
			//	throw new System.Exception("null reference: left");
			//if (object.ReferenceEquals(right, null))
			//	throw new System.Exception("null reference: right");
			//if (left.Dimensions != right.Dimensions)
			//	throw new System.Exception("invalid dimensions on vector subtraction.");
			//Vector<T> result =
			//	new Vector<T>(left.Dimensions);
			//for (int i = 0; i < left.Dimensions; i++)
			//	result[i] = Vector<T>.Subtract(left[i], right[i]);
			//return result;
			#endregion
		};
		#endregion

		#region Multiply
		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
		private static Vector<T>.Delegates.Vector_Multiply Vector_Multiply = (Vector<T> left, T right) =>
		{
			Vector<T>.Vector_Multiply =
				Meta.Compile<Vector<T>.Delegates.Vector_Multiply>(
					string.Concat(
"(Vector<", T_Source, "> _left, ", T_Source, @" _right) =>
{
 	if (object.ReferenceEquals(_left, null))
			throw new System.Exception(", "\"null reference: _left\"", @");
	int length = _left.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(length);
	", T_Source, @"[] left_flat = _left._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
			result_flat[i] = left_flat[i] * _right;
	return result;
}"));

			return Vector<T>.Vector_Multiply(left, right);

			#region Alternate Version
			//if (left == null)
			//	throw new System.Exception("null reference: left");
			//if (right == null)
			//	throw new System.Exception("null reference: right");
			//Vector<T> result =
			//	new Vector<T>(left.Dimensions);
			//for (int i = 0; i < left.Dimensions; i++)
			//	result[i] = Vector<T>.Multiply(left[i], right);
			//return result;
			#endregion
		};
		#endregion

		#region Divide
		/// <summary>Divides all the components of a vector by a scalar.</summary>
		private static Vector<T>.Delegates.Vector_Divide Vector_Divide = (Vector<T> vector, T right) =>
		{
			Vector<T>.Vector_Divide =
				Meta.Compile<Vector<T>.Delegates.Vector_Divide>(
					string.Concat(
@"(Vector<", T_Source, "> _left, ", T_Source, @" _right) =>
{",
 @"	if (object.ReferenceEquals(_left, null))
		throw new System.ArgumentNullException(", "\"null reference: left\"", @");", System.Environment.NewLine,
 @"	int length = _left.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(length);
	", T_Source, @"[] _left_flat = _left._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
		result_flat[i] = _left_flat[i] / _right;
	return result;
}"));

			return Vector<T>.Vector_Divide(vector, right);

			#region Alternate Version
			//if (vector == null)
			//	throw new System.ArgumentNullException("left");
			//if (right == null)
			//	throw new System.ArgumentNullException("right");
			//Vector<T> result =
			//	new Vector<T>(vector.Dimensions);
			//for (int i = 0; i < vector.Dimensions; i++)
			//	result[i] = Vector<T>.Divide(vector[i], right);
			//return result;
			#endregion
		};
		#endregion

		#region DotProduct
		/// <summary>Computes the dot product between two vectors.</summary>
		private static Vector<T>.Delegates.Vector_DotProduct Vector_DotProduct = (Vector<T> left, Vector<T> right) =>
		{
			Vector<T>.Vector_DotProduct =
				Meta.Compile<Vector<T>.Delegates.Vector_DotProduct>(
					string.Concat(
"(Vector<", T_Source, "> _left, Vector<", T_Source, @"> _right) =>
{
 	if (object.ReferenceEquals(_left, null))
		throw new System.ArgumentNullException(", "\"left\"", @");
	if (object.ReferenceEquals(_right, null))
		throw new System.ArgumentNullException(", "\"right\"", @");
	if (_left.Dimensions != _right.Dimensions)
		throw new System.ArithmeticException(", "\"dimension mismatch on vector dot product.\"", @");
 	int length = _left.Dimensions;
	", T_Source, @" result = 0;
	", T_Source, @"[] _left_flat = _left._vector;
	", T_Source, @"[] _right_flat = _right._vector;
	for (int i = 0; i < length; i++)
		result += _left_flat[i] * _right_flat[i];
	return result;
}"));

			return Vector<T>.Vector_DotProduct(left, right);

			#region Alternate Version
			//generic result = 0;
			//for (int i = 0; i < left.Dimensions; i++)
			//	result += left[i] * right[i];
			//return result;
			#endregion
		};
		#endregion

		#region CrossProduct
		/// <summary>Computes teh cross product of two vectors.</summary>
		private static Vector<T>.Delegates.Vector_CrossProduct Vector_CrossProduct = (Vector<T> left, Vector<T> right) =>
		{
			Vector<T>.Vector_CrossProduct =
				Meta.Compile<Vector<T>.Delegates.Vector_CrossProduct>(
					string.Concat(
"(Vector<", T_Source, "> _left, Vector<", T_Source, @"> _right) =>
{
	if (object.ReferenceEquals(_left, null))
		throw new System.ArgumentNullException(", "\"null reference: _left\"", @");
	if (object.ReferenceEquals(_right, null))
		throw new System.ArgumentNullException(", "\"null reference: _right\"", @");
	if (_left.Dimensions != _right.Dimensions)
		throw new System.ArithmeticException(", "\"invalid cross product (_left.Dimensions != _right.Dimensions)\"", @");
	if (_left.Dimensions != 3)
		throw new System.ArithmeticException(", "\"my cross product function is only defined for 3-component vectors.\"", @");
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(3);
	", T_Source, @"[] _left_flat = _left._vector;
	", T_Source, @"[] _right_flat = _right._vector;
	", T_Source, @"[] result_flat = result._vector;
	result_flat[0] = _left_flat[1] * _right_flat[2] - _left_flat[2] * _right_flat[1];
	result_flat[1] = _left_flat[2] * _right_flat[0] - _left_flat[0] * _right_flat[2];
	result_flat[2] = _left_flat[0] * _right_flat[1] - _left_flat[1] * _right_flat[0];
	return result;
}"));

			return Vector<T>.Vector_CrossProduct(left, right);
		};
		#endregion

		#region Normalize
		/// <summary>Normalizes a vector.</summary>
		private static Vector<T>.Delegates.Vector_Normalize Vector_Normalize = (Vector<T> vector) =>
		{
			Vector<T>.Vector_Normalize =
				Meta.Compile<Vector<T>.Delegates.Vector_Normalize>(
					string.Concat(
"(Vector<", T_Source + @"> _vector) =>
{
	if (object.ReferenceEquals(_vector, null))
		throw new System.Exception(", "\"null reference: _vector\"", @");
	int length = _vector.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(_vector.Dimensions);
	", T_Source, " magnitude = Vector<", T_Source, @">.Magnitude(_vector);
	if (magnitude == 0)
		throw new System.Exception();
	", T_Source, @"[] _vector_flat = _vector._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
		result_flat[i] = _vector_flat[i] / magnitude;
	return result;
}"));

			return Vector<T>.Vector_Normalize(vector);

			#region Alternat Version
			//generic magnitude = Vector.Magnitude(vector);
			//if (magnitude != 0)
			//{
			//	Vector<generic> result = 
			//		new Vector<generic>(vector.Dimensions);
			//	for (int i = 0; i < vector.Dimensions; i++)
			//		result[i] = vector[i] / magnitude;
			//	return result;
			//}
			//else
			//	return new generic[vector.Dimensions];
			#endregion
		};
		#endregion

		#region Magnitude
		/// <summary>Computes the length of a vector.</summary>
		private static Vector<T>.Delegates.Vector_Magnitude Vector_Magnitude = (Vector<T> vector) =>
		{

			Vector<T>.Vector_Magnitude =
				Meta.Compile<Vector<T>.Delegates.Vector_Magnitude>(
					string.Concat(
"(Vector<", T_Source, @"> _vector) =>
{
	if (object.ReferenceEquals(_vector, null))
		throw new System.Exception(", "\"null reference: _vector\"", @");
	int length = _vector.Dimensions;
	", T_Source, @" result = 0;
	", T_Source, @"[] _vector_flat = _vector._vector;
	for (int i = 0; i < length; i++)
		result += _vector_flat[i] * _vector_flat[i];
	return Compute<", T_Source, @">.SquareRoot(result);
}"));

			return Vector<T>.Vector_Magnitude(vector);

			#region Alternate Version
			//generic result = 0;
			//for (int i = 0; i < vector.Dimensions; i++)
			//	result += vector[i] * vector[i];
			//return Algebra.sqrt(result);
			#endregion
		};
		#endregion

		#region MagnitudeSquared
		/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
		private static Vector<T>.Delegates.Vector_MagnitudeSquared Vector_MagnitudeSquared = (Vector<T> vector) =>
		{
			Vector<T>.Vector_MagnitudeSquared =
				Meta.Compile<Vector<T>.Delegates.Vector_MagnitudeSquared>(
					string.Concat(
"(Vector<", T_Source, @"> _vector) =>
{
	if (object.ReferenceEquals(_vector, null))
		throw new System.Exception(", "\"null reference: _vector\"", @");
	int length = _vector.Dimensions;
	", T_Source, @" result = 0;
	", T_Source, @"[] _vector_flat = _vector._vector;
	for (int i = 0; i < length; i++)
		result += _vector_flat[i] * _vector_flat[i];
	return result;
}"));

			return Vector<T>.Vector_MagnitudeSquared(vector);

			#region Alternate Version
			//generic result = 0;
			//for (int i = 0; i < vector.Dimensions; i++)
			//	result += vector[i] * vector[i];
			//return result;
			#endregion
		};
		#endregion

		#region Angle
		/// <summary>Computes the angle between two vectors.</summary>
		private static Vector<T>.Delegates.Vector_Angle Vector_Angle = (Vector<T> first, Vector<T> second) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region RotateBy
		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		private static Vector<T>.Delegates.Vector_RotateBy Vector_RotateBy = (Vector<T> vector, T angle, T x, T y, T z) =>
		{
			throw new System.NotImplementedException();

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
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		private static Vector<T>.Delegates.Vector_Lerp Vector_Lerp = (Vector<T> left, Vector<T> right, T blend) =>
		{
			Vector<T>.Vector_Lerp =
				Meta.Compile<Vector<T>.Delegates.Vector_Lerp>(
					string.Concat(
"(Vector<", T_Source, "> _left, Vector<", T_Source, "> _right, ", T_Source, @" _blend) =>
{
	if (_blend < 0 || _blend > 1)
		throw new System.Exception(", "\"invalid vector lerp _blend value: (_blend < 0.0f || _blend > 1.0f).\"", @");
	if (_left.Dimensions != _right.Dimensions)
		throw new System.Exception(", "\"invalid vector lerp length: (_left.Dimensions != _right.Dimensions)\"", @");
	int length = _left.Dimensions;
	Vector<", T_Source, @"> result =
		new Vector<", T_Source, @">(length);
	", T_Source, @"[] _left_flat = _left._vector;
	", T_Source, @"[] _right_flat = _right._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < length; i++)
		result_flat[i] = _left_flat[i] + _blend * (_right_flat[i] - _left_flat[i]);
	return result;
}"));

			return Vector<T>.Vector_Lerp(left, right, blend);

			#region Alternate Version
			//Vector<generic> result = new Vector<generic>(left.Dimensions);
			//for (int i = 0; i < left.Dimensions; i++)
			//	result[i] = left[i] + blend * (right[i] - left[i]);
			//return result;
			#endregion
		};
		#endregion

		#region Slerp
		/// <summary>Sphereically interpolates between two vectors.</summary>
		private static Vector<T>.Delegates.Vector_Slerp Vector_Slerp = (Vector<T> left, Vector<T> right, T blend) =>
		{
			throw new System.NotImplementedException();

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
		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		private static Vector<T>.Delegates.Vector_Blerp Vector_Blerp = (Vector<T> a, Vector<T> b, Vector<T> c, T u, T v) =>
		{
			Vector<T>.Vector_Blerp =
				Meta.Compile<Vector<T>.Delegates.Vector_Blerp>(
					string.Concat(
"(Vector<", T_Source, "> _a, Vector<", T_Source, "> _b, Vector<", T_Source, "> _c, ", T_Source, " _u, ", T_Source, @" _v) =>
{
	if (object.ReferenceEquals(a, null))
		throw new System.Exception(", "\"null reference: _a\"", @");
	if (object.ReferenceEquals(b, null))
		throw new System.Exception(", "\"null reference: _b\"", @");
	if (object.ReferenceEquals(c, null))
		throw new System.Exception(", "\"null reference: _c\"", @");
	return
		Vector<", T_Source, @">.Vector_Add(
			Vector<", T_Source, @">.Vector_Add(
				Vector<", T_Source, @">.Vector_Multiply(
					Vector<", T_Source, @">.Vector_Subtract(_b, _a), _u),
						Vector<", T_Source, @">.Vector_Multiply(
							Vector<", T_Source, @">.Vector_Subtract(_c, _a), _v)), _a);
}"));

			return Vector<T>.Vector_Blerp(a, b, c, u, v);

			#region Alternate Version
			//return 
			//	Vector.Add(
			//		Vector.Add(
			//			Vector.Multiply(
			//				Vector.Subtract(b, a), u),
			//					Vector.Multiply(
			//						Vector.Subtract(c, a), v)), a);
			#endregion
		};
		#endregion

		#region EqualsValue
		/// <summary>Does a value equality check.</summary>
		private static Vector<T>.Delegates.Vector_EqualsValue Vector_EqualsValue = (Vector<T> left, Vector<T> right) =>
		{
			Vector<T>.Vector_EqualsValue =
				Meta.Compile<Vector<T>.Delegates.Vector_EqualsValue>(
					string.Concat(
"(Vector<", T_Source, "> _left, Vector<", T_Source, @"> _right) =>
{
	if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))
		return true;
	if (object.ReferenceEquals(_left, null))
		return false;
	if (object.ReferenceEquals(_right, null))
		return false;
	if (_left.Dimensions != _right.Dimensions)
		return false;
	for (int i = 0; i < _left.Dimensions; i++)
		if (_left[i] != _right[i])
			return false;
	return true;
}"));

			return Vector<T>.Vector_EqualsValue(left, right);

			#region Alternate Version
			//if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
			//	return true;
			//if (object.ReferenceEquals(left, null))
			//	return false;
			//if (object.ReferenceEquals(right, null))
			//	return false;
			//if (left.Dimensions != right.Dimensions)
			//	return false;
			//for (int i = 0; i < left.Dimensions; i++)
			//	if (left[i] != right[i])
			//		return false;
			//return true;
			#endregion
		};
		#endregion

		#region EqualsValue_leniency
		/// <summary>Does a value equality check with leniency.</summary>
		private static Vector<T>.Delegates.Vector_EqualsValue_leniency Vector_EqualsValue_leniency = (Vector<T> left, Vector<T> right, T leniency) =>
		{
			Vector<T>.Vector_EqualsValue_leniency =
				Meta.Compile<Vector<T>.Delegates.Vector_EqualsValue_leniency>(
					string.Concat(
"(Vector<", T_Source + "> _left, Vector<", T_Source, "> _right, ", T_Source, @" _leniency) =>
{
	if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))
		return true;
	if (object.ReferenceEquals(_left, null))
		return false;
	if (object.ReferenceEquals(_right, null))
		return false;
	if (_left.Dimensions != _right.Dimensions)
		return false;
	for (int i = 0; i < _left.Dimensions; i++)
		if (Logic<", T_Source, @">.Abs(_left[i] - _right[i]) > _leniency)
			return false;
	return true;
}"));

			return Vector<T>.Vector_EqualsValue_leniency(left, right, leniency);

			#region Alternate Version
			//if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
			//	return true;
			//if (object.ReferenceEquals(left, null))
			//	return false;
			//if (object.ReferenceEquals(right, null))
			//	return false;
			//if (left.Dimensions != right.Dimensions)
			//	return false;
			//for (int i = 0; i < left.Dimensions; i++)
			//	if (Logic.Abs(left[i] - right[i]) > leniency)
			//		return false;
			//return true;
			#endregion
		};
		#endregion

		#region RotateBy_quaternion
		/// <summary>Rotates a vector by a quaternion.</summary>
		private static Vector<T>.Delegates.Vector_RotateBy_quaternion Vector_RotateBy_quaternion = (Vector<T> vector, Quaternion<T> rotation) =>
		{
			return Quaternion<T>.Rotate(rotation, vector);
		};
		#endregion

		#region generic

		///// <summary>Computes the angle between two vectors.</summary>
		///// <param name="first">The first vector to determine the angle between.</param>
		///// <param name="second">The second vector to determine the angle between.</param>
		///// <returns>The angle between the two vectors in radians.</returns>
		//private static generic Angle(Vector<generic> first, Vector<generic> second)
		//{
		//		throw new System.Exception("requires rational types");

		//		//			#region pre-optimization

		//		//			//return Trigonometry.arccos(
		//		//			//	Vector.DotProduct(first, second) / 
		//		//			//	(Vector.Magnitude(first) * 
		//		//			//	Vector.Magnitude(second)));

		//		//			#endregion

		//		//#if no_error_checking
		//		//			// nothing
		//		//#else
		//		//			if (object.ReferenceEquals(first, null))
		//		//				throw new System.Exception("null reference: first");
		//		//			if (object.ReferenceEquals(second, null))
		//		//				throw new System.Exception("null reference: second");
		//		//#endif

		//		//			return Trigonometry.arccos(
		//		//				Vector.DotProduct(first, second) /
		//		//				(Vector.Magnitude(first) *
		//		//				Vector.Magnitude(second)));
		//		//		}

		//		//		/// <summary>Rotates a 3-component vector by the specified axis and rotation values.</summary>
		//		//		/// <param name="vector">The vector to rotate.</param>
		//		//		/// <param name="angle">The angle of the rotation in radians.</param>
		//		//		/// <param name="x">The x component of the axis vector to rotate about.</param>
		//		//		/// <param name="y">The y component of the axis vector to rotate about.</param>
		//		//		/// <param name="z">The z component of the axis vector to rotate about.</param>
		//		//		/// <returns>The result of the rotation.</returns>
		//		//		private static Vector<generic> RotateBy(Vector<generic> vector, generic angle, generic x, generic y, generic z)
		//		//		{
		//		//			#region pre-optimization

		//		//			//generic sinHalfAngle = Trigonometry.sin(angle / 2);
		//		//			//generic cosHalfAngle = Trigonometry.cos(angle / 2);
		//		//			//x *= sinHalfAngle;
		//		//			//y *= sinHalfAngle;
		//		//			//z *= sinHalfAngle;
		//		//			//generic x2 = cosHalfAngle * vector[0] + y * vector[2] - z * vector[1];
		//		//			//generic y2 = cosHalfAngle * vector[1] + z * vector[0] - x * vector[2];
		//		//			//generic z2 = cosHalfAngle * vector[2] + x * vector[1] - y * vector[0];
		//		//			//generic w2 = -x * vector[0] - y * vector[1] - z * vector[2];
		//		//			//Vector<generic> result = new Vector<generic>();
		//		//			//result[0] = x * w2 + cosHalfAngle * x2 + y * z2 - z * y2;
		//		//			//result[1] = y * w2 + cosHalfAngle * y2 + z * x2 - x * z2;
		//		//			//result[2] = z * w2 + cosHalfAngle * z2 + x * y2 - y * x2;

		//		//			#endregion

		//		//#if no_error_checking
		//		//			// nothing
		//		//#else
		//		//			if (object.ReferenceEquals(vector, null))
		//		//				throw new System.Exception("null reference: vector");
		//		//			if (vector.Dimensions == 3)
		//		//				throw new System.Exception("my RotateBy() function is only defined for 3-component vectors.");
		//		//#endif

		//		//			Vector<generic> result = new Vector<generic>(3);
		//		//			generic sinHalfAngle = Trigonometry.sin(angle / 2);
		//		//			generic cosHalfAngle = Trigonometry.cos(angle / 2);
		//		//			x *= sinHalfAngle;
		//		//			y *= sinHalfAngle;
		//		//			z *= sinHalfAngle;

		//		//#if unsafe_code
		//		//			unsafe
		//		//			{
		//		//				fixed (generic*
		//		//					vector_flat = vector._vector,
		//		//					result_flat = result._vector)
		//		//				{
		//		//					generic x2 = cosHalfAngle * vector_flat[0] + y * vector_flat[2] - z * vector_flat[1];
		//		//					generic y2 = cosHalfAngle * vector_flat[1] + z * vector_flat[0] - x * vector_flat[2];
		//		//					generic z2 = cosHalfAngle * vector_flat[2] + x * vector_flat[1] - y * vector_flat[0];
		//		//					generic w2 = -x * vector_flat[0] - y * vector_flat[1] - z * vector_flat[2];
		//		//					result_flat[0] = x * w2 + cosHalfAngle * x2 + y * z2 - z * y2;
		//		//					result_flat[1] = y * w2 + cosHalfAngle * y2 + z * x2 - x * z2;
		//		//					result_flat[2] = z * w2 + cosHalfAngle * z2 + x * y2 - y * x2;
		//		//				}
		//		//			}
		//		//#else
		//		//			generic[] vector_flat = vector._vector;
		//		//			generic[] result_flat = result._vector;
		//		//			generic x2 = cosHalfAngle * vector_flat[0] + y * vector_flat[2] - z * vector_flat[1];
		//		//			generic y2 = cosHalfAngle * vector_flat[1] + z * vector_flat[0] - x * vector_flat[2];
		//		//			generic z2 = cosHalfAngle * vector_flat[2] + x * vector_flat[1] - y * vector_flat[0];
		//		//			generic w2 = -x * vector_flat[0] - y * vector_flat[1] - z * vector_flat[2];
		//		//			result_flat[0] = x * w2 + cosHalfAngle * x2 + y * z2 - z * y2;
		//		//			result_flat[1] = y * w2 + cosHalfAngle * y2 + z * x2 - x * z2;
		//		//			result_flat[2] = z * w2 + cosHalfAngle * z2 + x * y2 - y * x2;
		//		//#endif

		//		//			return result;
		//}

		///// <summary>Rotates a vector by a quaternion rotation.</summary>
		///// <param name="vector">The vector to be rotated.</param>
		///// <param name="quaternion">The rotation to be applied.</param>
		///// <returns>The vector after the rotation.</returns>
		//private static Vector<generic> RotateBy(Vector<generic> vector, Quaternion<generic> quaternion)
		//{
		//		return Rotate(quaternion, vector);
		//}

		///// <summary>Sphereically interpolates between two vectors.</summary>
		///// <param name="left">The starting vector of the interpolation.</param>
		///// <param name="right">The ending vector of the interpolation.</param>
		///// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		///// <returns>The result of the slerp operation.</returns>
		//private static Vector<generic> Slerp(Vector<generic> left, Vector<generic> right, generic blend)
		//{
		//		throw new System.Exception("requires rational rational types");

		//		//			#region pre-optimization

		//		//			//generic dot = Vector.DotProduct(left, right);
		//		//			//dot = dot < -1 ? -1 : dot;
		//		//			//dot = dot > 1 ? 1 : dot;
		//		//			//generic Towel = Trigonometry.arccos(dot) * blend;
		//		//			//return Vector.Multiply(Vector.Add(Vector.Multiply(left, Trigonometry.cos(Towel)),
		//		//			//	Vector.Normalize(Vector.Subtract(right, Vector.Multiply(left, dot)))), Trigonometry.sin(Towel));

		//		//			#endregion

		//		//#if no_error_checking
		//		//			// nothing
		//		//#else
		//		//			if (object.ReferenceEquals(left, null))
		//		//				throw new System.Exception("null reference: left");
		//		//			if (object.ReferenceEquals(right, null))
		//		//				throw new System.Exception("null reference: right");
		//		//			if (blend < 0 || blend > 1)
		//		//				throw new System.Exception("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
		//		//#endif

		//		//			generic dot = Vector.DotProduct(left, right);
		//		//			dot = dot < -1 ? -1 : dot;
		//		//			dot = dot > 1 ? 1 : dot;
		//		//			generic Towel = Trigonometry.arccos(dot) * blend;
		//		//			return Vector.Multiply(Vector.Add(Vector.Multiply(left, Trigonometry.cos(Towel)),
		//		//				Vector.Normalize(Vector.Subtract(right, Vector.Multiply(left, dot)))), Trigonometry.sin(Towel));
		//}

		///// <summary>Does a value equality check with leniency.</summary>
		///// <param name="left">The first matrix to check for equality.</param>
		///// <param name="right">The second matrix to check for equality.</param>
		///// <param name="leniency">How much the values can vary but still be considered equal.</param>
		///// <returns>True if values are equal, false if not.</returns>
		//private static bool EqualsValue(Vector<generic> left, Vector<generic> right, generic leniency)
		//{
		//		#region pre-optimization

		//		//if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
		//		//	return true;
		//		//if (object.ReferenceEquals(left, null))
		//		//	return false;
		//		//if (object.ReferenceEquals(right, null))
		//		//	return false;

		//		//if (left.Dimensions != right.Dimensions)
		//		//	return false;
		//		//for (int i = 0; i < left.Dimensions; i++)
		//		//	if (Logic.Abs(left[i] - right[i]) > leniency)
		//		//		return false;
		//		//return true;

		//		#endregion

		//		if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
		//				return true;
		//		if (object.ReferenceEquals(left, null))
		//				return false;
		//		if (object.ReferenceEquals(right, null))
		//				return false;

		//		if (left.Dimensions != right.Dimensions)
		//				return false;
		//		for (int i = 0; i < left.Dimensions; i++)
		//				if (Logic<generic>.Abs(left[i] - right[i]) > leniency)
		//						return false;
		//		return true;
		//}

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

		#region override

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
				hash ^= this._vector[i].GetHashCode();
			return hash;
		}
		/// <summary>Does an equality check by reference.</summary>
		/// <param name="right">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object right)
		{
			if (!(right is Vector<T>)) return false;
			return Vector<T>.EqualsValue(this, right as Vector<T>);
		}

		#endregion
	}
}
