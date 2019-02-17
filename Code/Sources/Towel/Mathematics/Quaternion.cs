using System;

namespace Towel.Mathematics
{
	/// <summary>Standard 4-component quaternion [x, y, z, w]. W is the rotation ammount.</summary>
	/// <typeparam name="T">The numeric type of this Quaternion.</typeparam>
	[System.Serializable]
	public struct Quaternion<T>
	{
		#region delegates

		public class Delegates
		{

			/// <summary>Computes the length of quaternion.</summary>
			/// <param name="quaternion">The quaternion to compute the length of.</param>
			/// <returns>The length of the given quaternion.</returns>
			public delegate T Quaternion_Magnitude(Quaternion<T> quaternion);
			/// <summary>Computes the length of a quaternion, but doesn't square root it.</summary>
			/// <param name="quaternion">The quaternion to compute the length squared of.</param>
			/// <returns>The squared length of the given quaternion.</returns>
			public delegate T Quaternion_MagnitudeSquared(Quaternion<T> quaternion);
			/// <summary>Gets the conjugate of the quaternion.</summary>
			/// <param name="quaternion">The quaternion to conjugate.</param>
			/// <returns>The conjugate of teh given quaternion.</returns>
			public delegate Quaternion<T> Quaternion_Conjugate(Quaternion<T> quaternion);
			/// <summary>Adds two quaternions together.</summary>
			/// <param name="left">The first quaternion of the addition.</param>
			/// <param name="right">The second quaternion of the addition.</param>
			/// <returns>The result of the addition.</returns>
			public delegate Quaternion<T> Quaternion_Add(Quaternion<T> left, Quaternion<T> right);
			/// <summary>Subtracts two quaternions.</summary>
			/// <param name="left">The left quaternion of the subtraction.</param>
			/// <param name="right">The right quaternion of the subtraction.</param>
			/// <returns>The resulting quaternion after the subtraction.</returns>
			public delegate Quaternion<T> Quaternion_Subtract(Quaternion<T> left, Quaternion<T> right);
			/// <summary>Multiplies two quaternions together.</summary>
			/// <param name="left">The first quaternion of the multiplication.</param>
			/// <param name="right">The second quaternion of the multiplication.</param>
			/// <returns>The resulting quaternion after the multiplication.</returns>
			public delegate Quaternion<T> Quaternion_Multiply(Quaternion<T> left, Quaternion<T> right);
			/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
			/// <param name="left">The quaternion of the multiplication.</param>
			/// <param name="right">The scalar of the multiplication.</param>
			/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
			public delegate Quaternion<T> Quaternion_Multiply_scalar(Quaternion<T> left, T right);
			/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
			/// <param name="left">The quaternion to pre-multiply the vector by.</param>
			/// <param name="right">The vector to be multiplied.</param>
			/// <returns>The resulting quaternion of the multiplication.</returns>
			public delegate Quaternion<T> Quaternion_Multiply_Vector(Quaternion<T> left, Vector<T> right);
			/// <summary>Normalizes the quaternion.</summary>
			/// <param name="quaternion">The quaternion to normalize.</param>
			/// <returns>The normalization of the given quaternion.</returns>
			public delegate Quaternion<T> Quaternion_Normalize(Quaternion<T> quaternion);
			/// <summary>Inverts a quaternion.</summary>
			/// <param name="quaternion">The quaternion to find the inverse of.</param>
			/// <returns>The inverse of the given quaternion.</returns>
			public delegate Quaternion<T> Quaternion_Invert(Quaternion<T> quaternion);
			/// <summary>Lenearly interpolates between two quaternions.</summary>
			/// <param name="left">The starting point of the interpolation.</param>
			/// <param name="right">The ending point of the interpolation.</param>
			/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
			/// <returns>The result of the interpolation.</returns>
			public delegate Quaternion<T> Quaternion_Lerp(Quaternion<T> left, Quaternion<T> right, T blend);
			/// <summary>Sphereically interpolates between two quaternions.</summary>
			/// <param name="left">The starting point of the interpolation.</param>
			/// <param name="right">The ending point of the interpolation.</param>
			/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
			/// <returns>The result of the interpolation.</returns>
			public delegate Quaternion<T> Quaternion_Slerp(Quaternion<T> left, Quaternion<T> right, T blend);
			/// <summary>Rotates a vector by a quaternion [v' = qvq'].</summary>
			/// <param name="rotation">The quaternion to rotate the vector by.</param>
			/// <param name="vector">The vector to be rotated by.</param>
			/// <returns>The result of the rotation.</returns>
			public delegate Vector<T> Quaternion_Rotate(Quaternion<T> rotation, Vector<T> vector);
			/// <summary>Does a value equality check.</summary>
			/// <param name="left">The first quaternion to check for equality.</param>
			/// <param name="right">The second quaternion to check for equality.</param>
			/// <returns>True if values are equal, false if not.</returns>
			public delegate bool Quaternion_EqualsValue(Quaternion<T> left, Quaternion<T> right);
			/// <summary>Does a value equality check with leniency.</summary>
			/// <param name="left">The first quaternion to check for equality.</param>
			/// <param name="right">The second quaternion to check for equality.</param>
			/// <param name="leniency">How much the values can vary but still be considered equal.</param>
			/// <returns>True if values are equal, false if not.</returns>
			public delegate bool Quaternion_EqualsValue_leniency(Quaternion<T> left, Quaternion<T> right, T leniency);

		}

		#endregion

		#region fields

		// the values of the quaternion
		internal T _x, _y, _z, _w;

		#endregion

		#region property

		/// <summary>The X component of the quaternion. (axis, NOT rotation ammount)</summary>
		public T X { get { return _x; } set { _x = value; } }
		/// <summary>The Y component of the quaternion. (axis, NOT rotation ammount)</summary>
		public T Y { get { return _y; } set { _y = value; } }
		/// <summary>The Z component of the quaternion. (axis, NOT rotation ammount)</summary>
		public T Z { get { return _z; } set { _z = value; } }
		/// <summary>The W component of the quaternion. (rotation ammount, NOT axis)</summary>
		public T W { get { return _w; } set { _w = value; } }

        /// <summary>Computes the length of quaternion.</summary>
        /// <returns>The length of the given quaternion.</returns>
        public T Magnitude { get { return Quaternion<T>.Quaternion_Magnitude(this); } }
        /// <summary>Computes the length of a quaternion, but doesn't square root it for optimization possibilities.</summary>
        /// <returns>The squared length of the given quaternion.</returns>
        public T MagnitudeSquared { get { return Quaternion<T>.Quaternion_MagnitudeSquared(this); } }

        #endregion

        #region constructor

        /// <summary>Constructs a quaternion with the desired values.</summary>
        /// <param name="x">The x component of the quaternion.</param>
        /// <param name="y">The y component of the quaternion.</param>
        /// <param name="z">The z component of the quaternion.</param>
        /// <param name="w">The w component of the quaternion.</param>
        public Quaternion(T x, T y, T z, T w) { _x = x; _y = y; _z = z; _w = w; }

		#endregion

		#region factories
        
		///// <summary>Creates a quaternion from an axis and rotation.</summary>
		///// <param name="axis">The to create the quaternion from.</param>
		///// <param name="angle">The angle to create teh quaternion from.</param>
		///// <returns>The newly created quaternion.</returns>
		//public static Quaternion<T> FactoryFromAxisAngle(Vector axis, T angle)
		//{
		//	throw new System.NotImplementedException();
		//	//if (axis.LengthSquared() == 0.0f)
		//	//	return FactoryIdentity;
		//	//T sinAngleOverAxisLength = Calc.Sin(angle / 2) / axis.Length();
		//	//return Quaternion<T>.Normalize(new Quaternion<T>(
		//	//	_multiply(axis.X, sinAngleOverAxisLength),
		//	//	axis.Y * sinAngleOverAxisLength,
		//	//	axis.Z * sinAngleOverAxisLength,
		//	//	Calc.Cos(angle / 2)));
		//}

        public static Quaternion<T> Factory_Matrix3x3(Matrix<T> matrix)
        {
            if (matrix.Rows != 3 || matrix.Columns != 3)
                throw new System.ArithmeticException("error converting matrix to quaternion. matrix is not 3x3.");

            T w = Compute.Subtract(Compute.Add(Compute.Constant<T>.One, matrix[0, 0], matrix[1, 1], matrix[2, 2]), Compute.FromInt32<T>(2));
            return new Quaternion<T>(
                Compute.Divide(Compute.Subtract(matrix[2, 1], matrix[1, 2]), Compute.Multiply(Compute.FromInt32<T>(4), w)),
                Compute.Divide(Compute.Subtract(matrix[0, 2], matrix[2, 0]), Compute.Multiply(Compute.FromInt32<T>(4), w)),
                Compute.Divide(Compute.Subtract(matrix[1, 0], matrix[0, 1]), Compute.Multiply(Compute.FromInt32<T>(4), w)),
                w);
        }

        public static Quaternion<T> Factory_Matrix4x4(Matrix<T> matrix)
        {
            matrix = matrix.Transpose();
            T w, x, y, z;
            T diagonal = Compute.Add(matrix[0, 0], matrix[1, 1], matrix[2, 2]);
            if (Compute.GreaterThan(diagonal, Compute.Constant<T>.Zero))
            {
                T w4 = Compute.Multiply(Compute.SquareRoot(Compute.Add(diagonal, Compute.Constant<T>.One)), Compute.FromInt32<T>(2));
                w = Compute.Divide(w4, Compute.FromInt32<T>(4));
                x = Compute.Divide(Compute.Subtract(matrix[2, 1], matrix[1, 2]), w4);
                y = Compute.Divide(Compute.Subtract(matrix[0, 2], matrix[2, 0]), w4);
                z = Compute.Divide(Compute.Subtract(matrix[1, 0], matrix[0, 1]), w4);
            }
            else if (Compute.GreaterThan(matrix[0, 0], matrix[1, 1]) && Compute.GreaterThan(matrix[0, 0], matrix[2, 2]))
            {
                T x4 = Compute.Multiply(Compute.SquareRoot(Compute.Subtract(Compute.Subtract(Compute.Add(Compute.Constant<T>.One, matrix[0, 0]), matrix[1, 1]), matrix[2, 2])), Compute.FromInt32<T>(2));
                w = Compute.Divide(Compute.Subtract(matrix[2, 1], matrix[1, 2]), x4);
                x = Compute.Divide(x4, Compute.FromInt32<T>(4));
                y = Compute.Divide(Compute.Add(matrix[0, 1], matrix[1, 0]), x4);
                z = Compute.Divide(Compute.Add(matrix[0, 2], matrix[2, 0]), x4);
            }
            else if (Compute.GreaterThan(matrix[1, 1], matrix[2, 2]))
            {
                T y4 = Compute.Multiply(Compute.SquareRoot(Compute.Subtract(Compute.Subtract(Compute.Add(Compute.Constant<T>.One, matrix[1, 1]), matrix[0, 0]), matrix[2, 2])), Compute.FromInt32<T>(2));
                w = Compute.Divide(Compute.Subtract(matrix[0, 2], matrix[2, 0]), y4);
                x = Compute.Divide(Compute.Add(matrix[0, 1], matrix[1, 0]), y4);
                y = Compute.Divide(y4, Compute.FromInt32<T>(4));
                z = Compute.Divide(Compute.Add(matrix[1, 2], matrix[2, 1]), y4);
            }
            else
            {
                T z4 = Compute.Multiply(Compute.SquareRoot(Compute.Subtract(Compute.Subtract(Compute.Add(Compute.Constant<T>.One, matrix[2, 2]), matrix[0, 0]), matrix[1, 1])), Compute.FromInt32<T>(2));
                w = Compute.Divide(Compute.Subtract(matrix[1, 0], matrix[0, 1]), z4);
                x = Compute.Divide(Compute.Add(matrix[0, 2], matrix[2, 0]), z4);
                y = Compute.Divide(Compute.Add(matrix[1, 2], matrix[2, 1]), z4);
                z = Compute.Divide(z4, Compute.FromInt32<T>(4));
            }
            return new Quaternion<T>(x, y, z, w);
        }

        #endregion

        #region operator

        /// <summary>Adds two quaternions together.</summary>
        /// <param name="left">The first quaternion of the addition.</param>
        /// <param name="right">The second quaternion of the addition.</param>
        /// <returns>The result of the addition.</returns>
        public static Quaternion<T> operator +(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Add(left, right); }
		/// <summary>Subtracts two quaternions.</summary>
		/// <param name="left">The left quaternion of the subtraction.</param>
		/// <param name="right">The right quaternion of the subtraction.</param>
		/// <returns>The resulting quaternion after the subtraction.</returns>
		public static Quaternion<T> operator -(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Subtract(left, right); }
		/// <summary>Multiplies two quaternions together.</summary>
		/// <param name="left">The first quaternion of the multiplication.</param>
		/// <param name="right">The second quaternion of the multiplication.</param>
		/// <returns>The resulting quaternion after the multiplication.</returns>
		public static Quaternion<T> operator *(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Multiply(left, right); }
		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
		/// <param name="left">The quaternion to pre-multiply the vector by.</param>
		/// <param name="vector">The vector to be multiplied.</param>
		/// <returns>The resulting quaternion of the multiplication.</returns>
		//public static Quaternion<T> operator *(Quaternion<T> left, Vector right)
		//{ return Quaternion<T>.Multiply(left, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="left">The quaternion of the multiplication.</param>
		/// <param name="right">The scalar of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public static Quaternion<T> operator *(Quaternion<T> left, T right)
		{ return Quaternion<T>.Quaternion_MultiplyScalar(left, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="left">The scalar of the multiplication.</param>
		/// <param name="right">The quaternion of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public static Quaternion<T> operator *(T left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_MultiplyScalar(right, left); }
		/// <summary>Checks for equality by value. (beware float errors)</summary>
		/// <param name="left">The first quaternion of the equality check.</param>
		/// <param name="right">The second quaternion of the equality check.</param>
		/// <returns>true if the values were deemed equal, false if not.</returns>
		public static bool operator ==(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_EqualsValue(left, right); }
		/// <summary>Checks for anti-equality by value. (beware float errors)</summary>
		/// <param name="left">The first quaternion of the anti-equality check.</param>
		/// <param name="right">The second quaternion of the anti-equality check.</param>
		/// <returns>false if the values were deemed equal, true if not.</returns>
		public static bool operator !=(Quaternion<T> left, Quaternion<T> right)
		{ return !Quaternion<T>.Quaternion_EqualsValue(left, right); }

		#endregion

		#region instance

		/// <summary>Gets the conjugate of the quaternion.</summary>
		/// <returns>The conjugate of teh given quaternion.</returns>
		public Quaternion<T> Conjugate()
		{ return Quaternion<T>.Quaternion_Conjugate(this); }
		/// <summary>Adds two quaternions together.</summary>
		/// <param name="right">The second quaternion of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public Quaternion<T> Add(Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Add(this, right); }
		/// <summary>Subtracts two quaternions.</summary>
		/// <param name="right">The right quaternion of the subtraction.</param>
		/// <returns>The resulting quaternion after the subtraction.</returns>
		public Quaternion<T> Subtract(Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Subtract(this, right); }
		/// <summary>Multiplies two quaternions together.</summary>
		/// <param name="right">The second quaternion of the multiplication.</param>
		/// <returns>The resulting quaternion after the multiplication.</returns>
		public Quaternion<T> Multiply(Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Multiply(this, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="right">The scalar of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public Quaternion<T> Multiply(T right)
		{ return Quaternion<T>.Quaternion_MultiplyScalar(this, right); }
		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
		/// <param name="right">The vector to be multiplied.</param>
		/// <returns>The resulting quaternion of the multiplication.</returns>
		//public Quaternion<T> Multiply(Vector vector) { return Quaternion<T>.Multiply(this, vector); }
		/// <summary>Normalizes the quaternion.</summary>
		/// <returns>The normalization of the given quaternion.</returns>
		public Quaternion<T> Normalize()
		{ return Quaternion<T>.Quaternion_Normalize(this); }
		/// <summary>Inverts a quaternion.</summary>
		/// <returns>The inverse of the given quaternion.</returns>
		public Quaternion<T> Invert()
		{ return Quaternion<T>.Quaternion_Invert(this); }
		/// <summary>Lenearly interpolates between two quaternions.</summary>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public Quaternion<T> Lerp(Quaternion<T> right, T blend)
		{ return Quaternion<T>.Lerp(this, right, blend); }
		/// <summary>Sphereically interpolates between two quaternions.</summary>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public Quaternion<T> Slerp(Quaternion<T> right, T blend)
		{ return Quaternion<T>.Slerp(this, right, blend); }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="vector">The vector to be rotated by.</param>
		/// <returns>The result of the rotation.</returns>
		//public Vector Rotate(Vector vector) { return Quaternion<T>.Rotate(this, vector); }
		/// <summary>Does a value equality check.</summary>
		/// <param name="right">The second quaternion	to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool EqualsValue(Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_EqualsValue(this, right); }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="right">The second quaternion to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool EqualsValue(Quaternion<T> right, T leniency)
		{ return Quaternion<T>.Quaternion_EqualsValue_leniency(this, right, leniency); }
		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="right">The right quaternion of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public bool EqualsReference(Quaternion<T> right)
		{ return Quaternion<T>.EqualsReference(this, right); }

		#endregion

		#region statics

		/// <summary>Gets the conjugate of the quaternion.</summary>
		/// <param name="quaternion">The quaternion to conjugate.</param>
		/// <returns>The conjugate of teh given quaternion.</returns>
		public static Quaternion<T> Conjugate(Quaternion<T> quaternion)
		{ return Quaternion<T>.Quaternion_Conjugate(quaternion); }
		/// <summary>Adds two quaternions together.</summary>
		/// <param name="left">The first quaternion of the addition.</param>
		/// <param name="right">The second quaternion of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Quaternion<T> Add(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Add(left, right); }
		/// <summary>Subtracts two quaternions.</summary>
		/// <param name="left">The left quaternion of the subtraction.</param>
		/// <param name="right">The right quaternion of the subtraction.</param>
		/// <returns>The resulting quaternion after the subtraction.</returns>
		public static Quaternion<T> Subtract(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Subtract(left, right); }
		/// <summary>Multiplies two quaternions together.</summary>
		/// <param name="left">The first quaternion of the multiplication.</param>
		/// <param name="right">The second quaternion of the multiplication.</param>
		/// <returns>The resulting quaternion after the multiplication.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_Multiply(left, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="left">The quaternion of the multiplication.</param>
		/// <param name="right">The scalar of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> left, T right)
		{ return Quaternion<T>.Quaternion_MultiplyScalar(left, right); }
		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
		/// <param name="left">The quaternion to pre-multiply the vector by.</param>
		/// <param name="right">The vector to be multiplied.</param>
		/// <returns>The resulting quaternion of the multiplication.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> left, Vector<T> right)
		{ return Quaternion<T>.Quaternion_MultiplyVector(left, right); }
		/// <summary>Normalizes the quaternion.</summary>
		/// <param name="quaternion">The quaternion to normalize.</param>
		/// <returns>The normalization of the given quaternion.</returns>
		public static Quaternion<T> Normalize(Quaternion<T> quaternion)
		{ return Quaternion<T>.Quaternion_Normalize(quaternion); }
		/// <summary>Inverts a quaternion.</summary>
		/// <param name="quaternion">The quaternion to find the inverse of.</param>
		/// <returns>The inverse of the given quaternion.</returns>
		public static Quaternion<T> Invert(Quaternion<T> quaternion)
		{ return Quaternion<T>.Quaternion_Invert(quaternion); }
		/// <summary>Lenearly interpolates between two quaternions.</summary>
		/// <param name="left">The starting point of the interpolation.</param>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Quaternion<T> Lerp(Quaternion<T> left, Quaternion<T> right, T blend)
		{ return Quaternion<T>.Quaternion_Lerp(left, right, blend); }
		/// <summary>Sphereically interpolates between two quaternions.</summary>
		/// <param name="left">The starting point of the interpolation.</param>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Quaternion<T> Slerp(Quaternion<T> left, Quaternion<T> right, T blend)
		{ return Quaternion<T>.Quaternion_Slerp(left, right, blend); }
		/// <summary>Rotates a vector by a quaternion [v' = qvq'].</summary>
		/// <param name="rotation">The quaternion to rotate the vector by.</param>
		/// <param name="vector">The vector to be rotated by.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> Rotate(Quaternion<T> rotation, Vector<T> vector)
		{ return Quaternion<T>.Quaternion_Rotate(rotation, vector); }
		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first quaternion to check for equality.</param>
		/// <param name="right">The second quaternion	to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Quaternion<T> left, Quaternion<T> right)
		{ return Quaternion<T>.Quaternion_EqualsValue(left, right); }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first quaternion to check for equality.</param>
		/// <param name="right">The second quaternion to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Quaternion<T> left, Quaternion<T> right, T leniency)
		{ return Quaternion<T>.Quaternion_EqualsValue_leniency(left, right, leniency); }
		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left quaternion of the equality check.</param>
		/// <param name="right">The right quaternion of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(Quaternion<T> left, Quaternion<T> right)
		{ return object.ReferenceEquals(left, right); }

        /// <summary>Converts a quaternion into a 3x3 matrix.</summary>
        /// <param name="quaternion">The quaternion of the conversion.</param>
        /// <returns>The resulting 3x3 matrix.</returns>
        public static Matrix<T> ToMatrix3x3(Quaternion<T> quaternion)
        {
            return new Matrix<T>(new T[,]
            {
                { // row 1
                    Compute.Subtract(Compute.Subtract(Compute.Add(Compute.Multiply(quaternion.W, quaternion.W), Compute.Multiply(quaternion.X, quaternion.X)), Compute.Multiply(quaternion.Y, quaternion.Y)), Compute.Multiply(quaternion.Z, quaternion.Z)),
                    Compute.Subtract(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Y), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Z)),
                    Compute.Add(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Y)),
                },
                { // row 2
                    Compute.Add(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Y), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Z)),
                    Compute.Subtract(Compute.Add(Compute.Subtract(Compute.Multiply(quaternion.W, quaternion.W), Compute.Multiply(quaternion.X, quaternion.X)), Compute.Multiply(quaternion.Y, quaternion.Y)), Compute.Multiply(quaternion.Z, quaternion.Z)),
                    Compute.Add(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.Y), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.X)),
                },
                { // row 3
                    Compute.Subtract(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Y)),
                    Compute.Subtract(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.Y), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.X)),
                    Compute.Add(Compute.Subtract(Compute.Subtract(Compute.Multiply(quaternion.W, quaternion.W), Compute.Multiply(quaternion.X, quaternion.X)), Compute.Multiply(quaternion.Y, quaternion.Y)), Compute.Multiply(quaternion.Z, quaternion.Z)),
                }
            });
        }

        /// <summary>Returns an identity quaternion (no rotation).</summary>
        public static Quaternion<T> Identity
        {
            get
            {
                return new Quaternion<T>(Compute.Constant<T>.Zero, Compute.Constant<T>.Zero, Compute.Constant<T>.Zero, Compute.Constant<T>.One);
            }
        }

		#endregion

		#region implementations

		#region Magnitude

		private static Func<Quaternion<T>, T> Quaternion_Magnitude = (Quaternion<T> a) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return Compute.SquareRoot(Quaternion_MagnitudeSquared(a));
		};

		#endregion

		#region MagnitudeSquared

		private static Func<Quaternion<T>, T> Quaternion_MagnitudeSquared = (Quaternion<T> a) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return Compute.Add(
                Compute.Multiply(a.X, a.X),
                Compute.Multiply(a.Y, a.Y),
                Compute.Multiply(a.Z, a.Z),
                Compute.Multiply(a.W, a.W));
		};

		#endregion

		#region Conjugate

		private static Quaternion<T>.Delegates.Quaternion_Conjugate Quaternion_Conjugate = (Quaternion<T> a) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return new Quaternion<T>(
                Compute.Negate(a.X),
                Compute.Negate(a.Y),
                Compute.Negate(a.Z),
                a.W);
		};
		#endregion

		#region Add
		
		private static Func<Quaternion<T>, Quaternion<T>, Quaternion<T>> Quaternion_Add = (Quaternion<T> a, Quaternion<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            return new Quaternion<T>(
                Compute.Add(a.X, b.X),
                Compute.Add(a.Y, b.Y),
                Compute.Add(a.Z, b.Z),
                Compute.Add(a.W, b.W));
		};
		#endregion

		#region Subtract
		
		private static Func<Quaternion<T>, Quaternion<T>, Quaternion<T>> Quaternion_Subtract = (Quaternion<T> a, Quaternion<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            return new Quaternion<T>(
                Compute.Subtract(a.X, b.X),
                Compute.Subtract(a.Y, b.Y),
                Compute.Subtract(a.Z, b.Z),
                Compute.Subtract(a.W, b.W));
		};

		#endregion

		#region Multiply
		
		private static Func<Quaternion<T>, Quaternion<T>, Quaternion<T>> Quaternion_Multiply = (Quaternion<T> a, Quaternion<T> b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            return new Quaternion<T>(
                Compute.Subtract(Compute.Add(Compute.Add(Compute.Multiply(a.X, b.W), Compute.Multiply(a.W, b.X)), Compute.Multiply(a.Y, b.Z)), Compute.Multiply(a.Z, b.Y)),
                Compute.Subtract(Compute.Add(Compute.Add(Compute.Multiply(a.Y, b.W), Compute.Multiply(a.W, b.Y)), Compute.Multiply(a.Z, b.X)), Compute.Multiply(a.X, b.Z)),
                Compute.Subtract(Compute.Add(Compute.Add(Compute.Multiply(a.Z, b.W), Compute.Multiply(a.W, b.Z)), Compute.Multiply(a.X, b.Y)), Compute.Multiply(a.Y, b.X)),
                Compute.Subtract(Compute.Subtract(Compute.Subtract(Compute.Multiply(a.W, b.W), Compute.Multiply(a.X, b.X)), Compute.Multiply(a.Y, b.Y)), Compute.Multiply(a.Z, b.Z)));
		};
		#endregion

		#region MultiplyScalar
		
		private static Func<Quaternion<T>, T, Quaternion<T>> Quaternion_MultiplyScalar = (Quaternion<T> a, T b) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return new Quaternion<T>(
                Compute.Multiply(a.X, b),
                Compute.Multiply(a.Y, b),
                Compute.Multiply(a.Z, b),
                Compute.Multiply(a.W, b));
		};
		#endregion

		#region MultiplyVector
		
		private static Func<Quaternion<T>, Vector<T>, Quaternion<T>>  Quaternion_MultiplyVector = (Quaternion<T> a, Vector<T> b) =>
		{
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (b.Dimensions != 3)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(b) + "." + nameof(b.Dimensions) + " == 3)");
            }
            return new Quaternion<T>(
            	Compute.Subtract(Compute.Add(Compute.Multiply(a.W, b.X), Compute.Multiply(a.Y, b.Z)), Compute.Multiply(a.Z, b.Y)),
                Compute.Subtract(Compute.Add(Compute.Multiply(a.W, b.Y), Compute.Multiply(a.Z, b.X)), Compute.Multiply(a.X, b.Z)),
                Compute.Subtract(Compute.Add(Compute.Multiply(a.W, b.Z), Compute.Multiply(a.X, b.Y)), Compute.Multiply(a.Y, b.X)),
            	Compute.Subtract(Compute.Subtract(Compute.Multiply(Compute.Negate(a.X), b.X), Compute.Multiply(a.Y, b.Y)), Compute.Multiply(a.Z, b.Z)));
		};

		#endregion

		#region Normalize
		/// <summary>Normalizes the quaternion.</summary>
		private static Quaternion<T>.Delegates.Quaternion_Normalize Quaternion_Normalize = (Quaternion<T> a) =>
		{
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            T normalizer = a.Magnitude();
            if (normalizer != 0)
            {
                return a * (1 / normalizer);
            }
            else
            {
                return new Quaternion<" + T_Source + ">(0, 0, 0, 1);
            }

            string Quaternion_Normalize_string =
				"(Quaternion<" + T_Source + "> _quaternion) =>" +
				"{" +
				"	if (object.ReferenceEquals(_quaternion, null))" +
				"		throw new System.Exception(\"null reference: quaternion\");" +
				"	" + T_Source + " normalizer = Quaternion<" + T_Source + ">.Magnitude(_quaternion);" +
				"	if (normalizer != 0)" +
				"		return _quaternion * (1 / normalizer);" +
				"	else" +
				"		return new Quaternion<" + T_Source + ">(0, 0, 0, 1);" +
				"}";

			Quaternion<T>.Quaternion_Normalize =
				Meta.Compile<Quaternion<T>.Delegates.Quaternion_Normalize>(Quaternion_Normalize_string);

			return Quaternion<T>.Quaternion_Normalize(quaternion);
		};
		#endregion

		#region Invert
		/// <summary>Inverts a quaternion.</summary>
		private static Quaternion<T>.Delegates.Quaternion_Invert Quaternion_Invert = (Quaternion<T> quaternion) =>
		{
			string Quaternion_Invert_string =
				"(Quaternion<" + T_Source + "> _quaternion) =>" +
				"{" +
				"	if (object.ReferenceEquals(_quaternion, null))" +
				"		throw new System.Exception(\"null reference: quaternion\");" +
				"	" + T_Source + " normalizer = Quaternion<" + T_Source + ">.MagnitudeSquared(_quaternion);" +
				"	if (normalizer == 0)" +
				"		return new Quaternion<" + T_Source + ">(_quaternion.X, _quaternion.Y, _quaternion.Z, _quaternion.W);" +
				"	normalizer = 1 / normalizer;" +
				"	return new Quaternion<" + T_Source + ">(" +
				"		-_quaternion.X * normalizer," +
				"		-_quaternion.Y * normalizer," +
				"		-_quaternion.Z * normalizer," +
				"		_quaternion.W * normalizer);" +
				"}";

			Quaternion<T>.Quaternion_Invert =
				Meta.Compile<Quaternion<T>.Delegates.Quaternion_Invert>(Quaternion_Invert_string);

			return Quaternion<T>.Quaternion_Normalize(quaternion);
		};
		#endregion

		#region Lerp
		/// <summary>Lenearly interpolates between two quaternions.</summary>
		private static Quaternion<T>.Delegates.Quaternion_Lerp Quaternion_Lerp = (Quaternion<T> left, Quaternion<T> right, T blend) =>
		{
			string Quaternion_Lerp_string =
				"(Quaternion<" + T_Source + "> _left, Quaternion<" + T_Source + "> _right, " + T_Source + " _blend) =>" +
				"{" +
				"	if (object.ReferenceEquals(_left, null))" +
				"		throw new System.Exception(\"null reference: _left\");" +
				"	if (object.ReferenceEquals(_right, null))" +
				"		throw new System.Exception(\"null reference: _right\");" +
				"	if (_blend < 0 || _blend > 1)" +
				"		throw new System.Exception(\"invalid _blending value during lerp !(_blend < 0.0f || _blend > 1.0f).\");" +
				"	if (Quaternion<" + T_Source + ">.MagnitudeSquared(_left) == 0)" +
				"	{" +
				"		if (Quaternion<" + T_Source + ">.MagnitudeSquared(_right) == 0)" +
				"			return new Quaternion<" + T_Source + ">(0, 0, 0, 1);" +
				"		else" +
				"			return new Quaternion<" + T_Source + ">(_right.X, _right.Y, _right.Z, _right.W);" +
				"	}" +
				"	else if (Quaternion<" + T_Source + ">.MagnitudeSquared(_right) == 0)" +
				"		return new Quaternion<" + T_Source + ">(_left.X, _left.Y, _left.Z, _left.W);" +
				"	Quaternion<" + T_Source + "> result = new Quaternion<" + T_Source + ">(" +
				"		1 - _blend * _left.X + _blend * _right.X," +
				"		1 - _blend * _left.Y + _blend * _right.Y," +
				"		1 - _blend * _left.Z + _blend * _right.Z," +
				"		1 - _blend * _left.W + _blend * _right.W);" +
				"	if (Quaternion<" + T_Source + ">.MagnitudeSquared(result) > 0)" +
				"		return Quaternion<" + T_Source + ">.Normalize(result);" +
				"	else" +
				"		return new Quaternion<" + T_Source + ">(0, 0, 0, 1);" +
				"}";

			Quaternion<T>.Quaternion_Lerp =
				Meta.Compile<Quaternion<T>.Delegates.Quaternion_Lerp>(Quaternion_Lerp_string);

			return Quaternion<T>.Quaternion_Lerp(left, right, blend);
		};
		#endregion

		#region Slerp
		/// <summary>Sphereically interpolates between two quaternions.</summary>
		private static Quaternion<T>.Delegates.Quaternion_Slerp Quaternion_Slerp = (Quaternion<T> left, Quaternion<T> right, T blend) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region Rotate
		/// <summary>Rotates a vector by a quaternion [v' = qvq'].</summary>
		private static Quaternion<T>.Delegates.Quaternion_Rotate Quaternion_Rotate = (Quaternion<T> rotation, Vector<T> vector) =>
		{
			Quaternion<T>.Quaternion_Rotate =
				Meta.Compile<Quaternion<T>.Delegates.Quaternion_Rotate>(
					string.Concat(
						"(Quaternion<" + T_Source + "> _rotation, Vector<" + T_Source + "> _vector) =>" +
						"{" +
						"	if (object.ReferenceEquals(_rotation, null))" +
						"		throw new System.Exception(\"null reference: rotation\");" +
						"	if (object.ReferenceEquals(_vector, null))" +
						"		throw new System.Exception(\"null reference: vector\");" +
						"	if (_vector.Dimensions != 3 && _vector.Dimensions != 4)" +
						"		throw new System.Exception(\"my quaternion rotations are only defined for 3-component vectors.\");" +
						"	Quaternion<" + T_Source + "> answer =" +
						"		Quaternion<" + T_Source + ">.Multiply(" +
						"		Quaternion<" + T_Source + ">.Multiply(_rotation, _vector)," +
						"		Quaternion<" + T_Source + ">.Conjugate(_rotation));" +
						"	return new Vector<" + T_Source + ">(answer.X, answer.Y, answer.Z);" +
						"}"));

			return Quaternion<T>.Quaternion_Rotate(rotation, vector);
		};
		#endregion

		#region EqualsValue
		/// <summary>Does a value equality check.</summary>
		private static Quaternion<T>.Delegates.Quaternion_EqualsValue Quaternion_EqualsValue = (Quaternion<T> left, Quaternion<T> right) =>
		{
			string Quaternion_EqualsValue_string =
				"(Quaternion<" + T_Source + "> _left, Quaternion<" + T_Source + "> _right) =>" +
				"{" +
				"	if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))" +
				"		return true;" +
				"	if (object.ReferenceEquals(_left, null))" +
				"		return false;" +
				"	if (object.ReferenceEquals(_right, null))" +
				"		return false;" +
				"	return" +
				"		_left.X == _right.X &&" +
				"		_left.Y == _right.Y &&" +
				"		_left.Z == _right.Z &&" +
				"		_left.W == _right.W;" +
				"}";

			Quaternion<T>.Quaternion_EqualsValue =
				Meta.Compile<Quaternion<T>.Delegates.Quaternion_EqualsValue>(Quaternion_EqualsValue_string);

			return Quaternion<T>.Quaternion_EqualsValue(left, right);
		};
		#endregion

		#region EqualsValue_leniency
		/// <summary>Does a value equality check with leniency.</summary>
		private static Quaternion<T>.Delegates.Quaternion_EqualsValue_leniency Quaternion_EqualsValue_leniency = (Quaternion<T> left, Quaternion<T> right, T leniency) =>
		{
			string Quaternion_EqualsValue_leniency_string =
				"(Quaternion<" + T_Source + "> _left, Quaternion<" + T_Source + "> _right, " + T_Source + " _leniency) =>" +
				"{" +
				"	if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))" +
				"		return true;" +
				"	if (object.ReferenceEquals(_left, null))" +
				"		return false;" +
				"	if (object.ReferenceEquals(_right, null))" +
				"		return false;" +
				"	return" +
				"		Compute<" + T_Source + ">.AbsoluteValue(_left.X - _right.X) < _leniency &&" +
				"		Compute<" + T_Source + ">.AbsoluteValue(_left.Y - _right.Y) < _leniency &&" +
				"		Compute<" + T_Source + ">.AbsoluteValue(_left.Z - _right.Z) < _leniency &&" +
				"		Compute<" + T_Source + ">.AbsoluteValue(_left.W - _right.W) < _leniency;" +
				"}";

			Quaternion<T>.Quaternion_EqualsValue_leniency =
				Meta.Compile<Quaternion<T>.Delegates.Quaternion_EqualsValue_leniency>(Quaternion_EqualsValue_leniency_string);

			return Quaternion<T>.Quaternion_EqualsValue_leniency(left, right, leniency);
		};
		#endregion

		#endregion

		#region overrides

		/// <summary>Converts the quaternion into a string.</summary>
		/// <returns>The resulting string after the conversion.</returns>
		public override string ToString()
		{
			// Chane this method to format it how you want...
			return base.ToString();
			//return "{ " + _x + ", " + _y + ", " + _z + ", " + _w + " }";
		}

		/// <summary>Computes a hash code from the values in this quaternion.</summary>
		/// <returns>The computed hash code.</returns>
		public override int GetHashCode()
		{
			return
				_x.GetHashCode() ^
				_y.GetHashCode() ^
				_z.GetHashCode() ^
				_w.GetHashCode();
		}

		/// <summary>Does a reference equality check.</summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if (other is Quaternion<T>)
				return Quaternion<T>.EqualsReference(this, (Quaternion<T>)other);
			return false;
		}

		#endregion
	}
}
