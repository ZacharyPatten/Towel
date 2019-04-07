using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Towel.Mathematics
{
    /// <summary>Standard 4-component quaternion [x, y, z, w]. W is the rotation ammount.</summary>
    /// <typeparam name="T">The numeric type of this Quaternion.</typeparam>
    [DebuggerDisplay("{" + nameof(DebuggerString) + "}")]
    [Serializable]
    public class Quaternion<T>
    {
        internal T _x;
        internal T _y;
        internal T _z;
        internal T _w;

        #region Static Properties

        /// <summary>Returns an identity quaternion (no rotation).</summary>
        public static Quaternion<T> Identity
        {
            get
            {
                return new Quaternion<T>(Constant<T>.Zero, Constant<T>.Zero, Constant<T>.Zero, Constant<T>.One);
            }
        }

        #endregion

        #region Properties

        /// <summary>The X component of the quaternion. (axis, NOT rotation ammount)</summary>
        public T X { get { return _x; } set { _x = value; } }
        /// <summary>The Y component of the quaternion. (axis, NOT rotation ammount)</summary>
        public T Y { get { return _y; } set { _y = value; } }
        /// <summary>The Z component of the quaternion. (axis, NOT rotation ammount)</summary>
        public T Z { get { return _z; } set { _z = value; } }
        /// <summary>The W component of the quaternion. (rotation ammount, NOT axis)</summary>
        public T W { get { return _w; } set { _w = value; } }

        #endregion

        #region Debugger Properties

        internal string DebuggerString
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("[ ");
                stringBuilder.Append(_x);
                stringBuilder.Append(", ");
                stringBuilder.Append(_y);
                stringBuilder.Append(", ");
                stringBuilder.Append(_z);
                stringBuilder.Append(", ");
                stringBuilder.Append(_w);
                stringBuilder.Append(" ]");
                return stringBuilder.ToString();
            }
        }

        #endregion

        #region Constructors

        /// <summary>Constructs a quaternion.</summary>
        /// <param name="x">The x component of the quaternion.</param>
        /// <param name="y">The y component of the quaternion.</param>
        /// <param name="z">The z component of the quaternion.</param>
        /// <param name="w">The w component of the quaternion.</param>
        public Quaternion(T x, T y, T z, T w) { _x = x; _y = y; _z = z; _w = w; }

        #endregion

        #region Factories

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
            // Note: this method needs optimization

            if (matrix.Rows != 3 || matrix.Columns != 3)
                throw new System.ArithmeticException("error converting matrix to quaternion. matrix is not 3x3.");

            T w = Compute.Subtract(Compute.Add(Constant<T>.One, matrix[0, 0], matrix[1, 1], matrix[2, 2]), Compute.FromInt32<T>(2));
            return new Quaternion<T>(
                Compute.Divide(Compute.Subtract(matrix[2, 1], matrix[1, 2]), Compute.Multiply(Compute.FromInt32<T>(4), w)),
                Compute.Divide(Compute.Subtract(matrix[0, 2], matrix[2, 0]), Compute.Multiply(Compute.FromInt32<T>(4), w)),
                Compute.Divide(Compute.Subtract(matrix[1, 0], matrix[0, 1]), Compute.Multiply(Compute.FromInt32<T>(4), w)),
                w);
        }

        public static Quaternion<T> Factory_Matrix4x4(Matrix<T> matrix)
        {
            // Note: this method needs optimization

            matrix = matrix.Transpose();
            T w, x, y, z;
            T diagonal = Compute.Add(matrix[0, 0], matrix[1, 1], matrix[2, 2]);
            if (Compute.GreaterThan(diagonal, Constant<T>.Zero))
            {
                T w4 = Compute.Multiply(Compute.SquareRoot(Compute.Add(diagonal, Constant<T>.One)), Compute.FromInt32<T>(2));
                w = Compute.Divide(w4, Compute.FromInt32<T>(4));
                x = Compute.Divide(Compute.Subtract(matrix[2, 1], matrix[1, 2]), w4);
                y = Compute.Divide(Compute.Subtract(matrix[0, 2], matrix[2, 0]), w4);
                z = Compute.Divide(Compute.Subtract(matrix[1, 0], matrix[0, 1]), w4);
            }
            else if (Compute.GreaterThan(matrix[0, 0], matrix[1, 1]) && Compute.GreaterThan(matrix[0, 0], matrix[2, 2]))
            {
                T x4 = Compute.Multiply(Compute.SquareRoot(Compute.Subtract(Compute.Subtract(Compute.Add(Constant<T>.One, matrix[0, 0]), matrix[1, 1]), matrix[2, 2])), Compute.FromInt32<T>(2));
                w = Compute.Divide(Compute.Subtract(matrix[2, 1], matrix[1, 2]), x4);
                x = Compute.Divide(x4, Compute.FromInt32<T>(4));
                y = Compute.Divide(Compute.Add(matrix[0, 1], matrix[1, 0]), x4);
                z = Compute.Divide(Compute.Add(matrix[0, 2], matrix[2, 0]), x4);
            }
            else if (Compute.GreaterThan(matrix[1, 1], matrix[2, 2]))
            {
                T y4 = Compute.Multiply(Compute.SquareRoot(Compute.Subtract(Compute.Subtract(Compute.Add(Constant<T>.One, matrix[1, 1]), matrix[0, 0]), matrix[2, 2])), Compute.FromInt32<T>(2));
                w = Compute.Divide(Compute.Subtract(matrix[0, 2], matrix[2, 0]), y4);
                x = Compute.Divide(Compute.Add(matrix[0, 1], matrix[1, 0]), y4);
                y = Compute.Divide(y4, Compute.FromInt32<T>(4));
                z = Compute.Divide(Compute.Add(matrix[1, 2], matrix[2, 1]), y4);
            }
            else
            {
                T z4 = Compute.Multiply(Compute.SquareRoot(Compute.Subtract(Compute.Subtract(Compute.Add(Constant<T>.One, matrix[2, 2]), matrix[0, 0]), matrix[1, 1])), Compute.FromInt32<T>(2));
                w = Compute.Divide(Compute.Subtract(matrix[1, 0], matrix[0, 1]), z4);
                x = Compute.Divide(Compute.Add(matrix[0, 2], matrix[2, 0]), z4);
                y = Compute.Divide(Compute.Add(matrix[1, 2], matrix[2, 1]), z4);
                z = Compute.Divide(z4, Compute.FromInt32<T>(4));
            }
            return new Quaternion<T>(x, y, z, w);
        }

        #endregion

        #region Mathematics

        #region HasZeroMagnitude

        /// <summary>Checks quaternion for zero magnitude.</summary>
        /// <param name="a">The quaternion to check for zero magnitude.</param>
        /// <returns>True if the quaternion has zero magnitude. False if not.</returns>
        public static bool GetHasZeroMagnitude(Quaternion<T> a)
        {
            return
                Compute.Equal(a._x, Constant<T>.Zero) &&
                Compute.Equal(a._y, Constant<T>.Zero) &&
                Compute.Equal(a._z, Constant<T>.Zero) &&
                Compute.Equal(a._w, Constant<T>.Zero);
        }

        /// <summary>Checks quaternion for zero magnitude.</summary>
        public bool HasZeroMagnitude
        {
            get
            {
                return GetHasZeroMagnitude(this);
            }
        }

        #endregion

        #region Magnitude

        /// <summary>Computes the magnitude of this quaternion.</summary>
        /// <returns>The magnitude of this quaternion.</returns>
        public static T GetMagnitude(Quaternion<T> a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return Compute.SquareRoot(GetMagnitudeSquared(a));
        }

        /// <summary>Computes the magnitude of this quaternion.</summary>
        public T Magnitude
        {
            get
            {
                return GetMagnitude(this);
            }
        }

        #endregion

        #region MagnitudeSquared

        /// <summary>Computes the magnitude of this quaternion, but doesn't square root it for 
        /// possible optimization purposes.</summary>
        /// <returns>The squared length of the quaternion.</returns>
        public static T GetMagnitudeSquared(Quaternion<T> a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return GetMagnitudeSquaredImplementation.Function(a._x, a._y, a._z, a._w);
        }

        internal static class GetMagnitudeSquaredImplementation
        {
            internal static Func<T, T, T, T, T> Function = (T x, T y, T z, T w) =>
            {
                ParameterExpression X = Expression.Parameter(typeof(T));
                ParameterExpression Y = Expression.Parameter(typeof(T));
                ParameterExpression Z = Expression.Parameter(typeof(T));
                ParameterExpression W = Expression.Parameter(typeof(T));
                Expression BODY =
                    Expression.Add(Expression.Multiply(X, X),
                        Expression.Add(Expression.Multiply(Y, Y),
                            Expression.Add(Expression.Multiply(Z, Z),
                                Expression.Multiply(W, W))));
                Function = Expression.Lambda<Func<T, T, T, T, T>>(BODY, X, Y, Z, W).Compile();
                return Function(x, y, z, w);
            };
        }

        /// <summary>Computes the magnitude of this quaternion, but doesn't square root it for 
        /// possible optimization purposes.</summary>
        public T MagnitudeSquared
        {
            get
            {
                return GetMagnitudeSquared(this);
            }
        }

        #endregion

        #region Add

        /// <summary>Adds two quaternions together.</summary>
        /// <param name="a">The first quaternion of the addition.</param>
        /// <param name="b">The second quaternion of the addiiton.</param>
        /// <param name="c">The result of the addition.</param>
        public static void Add(Quaternion<T> a, Quaternion<T> b, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            T x = Compute.Add(a._x, b._x);
            T y = Compute.Add(a._y, b._y);
            T z = Compute.Add(a._z, b._z);
            T w = Compute.Add(a._w, b._w);
            if (c is null)
            {
                c = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                c._x = x;
                c._y = y;
                c._z = z;
                c._w = w;
            }
        }

        /// <summary>Adds two quaternions together.</summary>
        /// <param name="a">The first vector of the addition.</param>
        /// <param name="b">The second vector of the addiiton.</param>
        /// <returns>The result of the addition.</returns>
        public static Quaternion<T> Add(Quaternion<T> a, Quaternion<T> b)
        {
            Quaternion<T> c = null;
            Add(a, b, ref c);
            return c;
        }

        /// <summary>Adds two quaternions together.</summary>
        /// <param name="a">The first quaternion of the addition.</param>
        /// <param name="b">The second quaternion of the addition.</param>
        /// <returns>The result of the addition.</returns>
        public static Quaternion<T> operator +(Quaternion<T> a, Quaternion<T> b)
        {
            return Add(a, b);
        }

        /// <summary>Adds two quaternions together.</summary>
        /// <param name="b">The second quaternion of the addititon.</param>
        /// <param name="c">The result of the addition.</param>
        public void Add(Quaternion<T> b, ref Quaternion<T> c)
        {
            Add(this, b, ref c);
        }

        /// <summary>Adds two quaternions together.</summary>
		/// <param name="b">The quaternion to add to this one.</param>
		/// <returns>The result of the addition.</returns>
		public Quaternion<T> Add(Quaternion<T> b)
        {
            return this + b;
        }

        #endregion

        #region Subtract

        /// <summary>Subtracts two quaternions.</summary>
        /// <param name="a">The first quaternion of the subtraction.</param>
        /// <param name="b">The second quaternion of the subtraction.</param>
        /// <param name="c">The result of the subtraction.</param>
        public static void Subtract(Quaternion<T> a, Quaternion<T> b, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            T x = Compute.Subtract(a._x, b._x);
            T y = Compute.Subtract(a._y, b._y);
            T z = Compute.Subtract(a._z, b._z);
            T w = Compute.Subtract(a._w, b._w);
            if (c is null)
            {
                c = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                c._x = x;
                c._y = y;
                c._z = z;
                c._w = w;
            }
        }

        /// <summary>Subtracts two quaternions.</summary>
        /// <param name="a">The first vector of the subtraction.</param>
        /// <param name="b">The second vector of the subtraction.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Quaternion<T> Subtract(Quaternion<T> a, Quaternion<T> b)
        {
            Quaternion<T> c = null;
            Subtract(a, b, ref c);
            return c;
        }

        /// <summary>Subtracts two quaternions.</summary>
        /// <param name="a">The first quaternion of the subtraction.</param>
        /// <param name="b">The second quaternion of the subtraction.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Quaternion<T> operator -(Quaternion<T> a, Quaternion<T> b)
        {
            return Subtract(a, b);
        }

        /// <summary>Subtracts two quaternions.</summary>
        /// <param name="b">The second quaternion of the subtraction.</param>
        /// <param name="c">The result of the subtraction.</param>
        public void Subtract(Quaternion<T> b, ref Quaternion<T> c)
        {
            Subtract(this, b, ref c);
        }

        /// <summary>Subtracts two quaternions together.</summary>
		/// <param name="b">The second quaternion of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public Quaternion<T> Subtract(Quaternion<T> b)
        {
            return this - b;
        }

        #endregion

        #region Multiply (Quaternion * Quaternion)

        /// <summary>Multiplies two quaternions.</summary>
        /// <param name="a">The first quaternion of the multiplication.</param>
        /// <param name="b">The second quaternion of the multiplication.</param>
        /// <param name="c">The resulting quaternion after the multiplication.</param>
        private static void Multiply(Quaternion<T> a, Quaternion<T> b, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            T x = QuaternionMiltiplyXYZComponentImplementation.Function(a.X, b.W, a.W, b.X, a.Y, b.Z, a.Z, b.Y);
            T y = QuaternionMiltiplyXYZComponentImplementation.Function(a.Y, b.W, a.W, b.Y, a.Z, b.X, a.X, b.Z);
            T z = QuaternionMiltiplyXYZComponentImplementation.Function(a.Z, b.W, a.W, b.Z, a.X, b.Y, a.Y, b.X);
            T w = QuaternionMiltiplyWComponentImplementation.Function(a.W, b.W, a.X, b.X, a.Y, b.Y, a.Z, b.Z);
            if (c is null)
            {
                c = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                c._x = x;
                c._y = y;
                c._z = z;
                c._w = w;
            }
        }

        internal static class QuaternionMiltiplyXYZComponentImplementation
        {
            internal static Func<T, T, T, T, T, T, T, T, T> Function = (T a, T b, T c, T d, T e, T f, T g, T h) =>
            {
                // parameters
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                ParameterExpression D = Expression.Parameter(typeof(T));
                ParameterExpression E = Expression.Parameter(typeof(T));
                ParameterExpression F = Expression.Parameter(typeof(T));
                ParameterExpression G = Expression.Parameter(typeof(T));
                ParameterExpression H = Expression.Parameter(typeof(T));
                // multiply
                Expression AB = Expression.Multiply(A, B);
                Expression CD = Expression.Multiply(C, D);
                Expression EF = Expression.Multiply(E, F);
                Expression GH = Expression.Multiply(G, H);
                // add
                Expression AB_add_CD = Expression.Add(AB, CD);
                Expression AB_add_CD_add_EF = Expression.Add(AB_add_CD, EF);
                // subtract
                Expression AB_add_CD_add_EF_subtract_GH = Expression.Subtract(AB_add_CD_add_EF, GH);
                // compile
                Expression BODY = AB_add_CD_add_EF_subtract_GH;
                Function = Expression.Lambda<Func<T, T, T, T, T, T, T, T, T>>(BODY, A, B, C, D, E, F, G, H).Compile();
                return Function(a, b, c, d, e, f, g, h);
            };
        }

        internal static class QuaternionMiltiplyWComponentImplementation
        {
            internal static Func<T, T, T, T, T, T, T, T, T> Function = (T a, T b, T c, T d, T e, T f, T g, T h) =>
            {
                // parameters
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                ParameterExpression D = Expression.Parameter(typeof(T));
                ParameterExpression E = Expression.Parameter(typeof(T));
                ParameterExpression F = Expression.Parameter(typeof(T));
                ParameterExpression G = Expression.Parameter(typeof(T));
                ParameterExpression H = Expression.Parameter(typeof(T));
                // multiply
                Expression AB = Expression.Multiply(A, B);
                Expression CD = Expression.Multiply(C, D);
                Expression EF = Expression.Multiply(E, F);
                Expression GH = Expression.Multiply(G, H);
                // subtract
                Expression AB_subtract_CD = Expression.Subtract(AB, CD);
                Expression AB_subtract_CD_subtract_EF = Expression.Subtract(AB_subtract_CD, EF);
                Expression AB_subtract_CD_subtract_EF_subtract_GH = Expression.Subtract(AB_subtract_CD_subtract_EF, GH);
                // compile
                Expression BODY = AB_subtract_CD_subtract_EF_subtract_GH;
                Function = Expression.Lambda<Func<T, T, T, T, T, T, T, T, T>>(BODY, A, B, C, D, E, F, G, H).Compile();
                return Function(a, b, c, d, e, f, g, h);
            };
        }

        /// <summary>Multiplies two quaternions.</summary>
        /// <param name="a">The first quaternion of the multiplication.</param>
        /// <param name="b">The second quaternion of the multiplication.</param>
        /// <returns>The resulting quaternion after the multiplication.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> a, Quaternion<T> b)
        {
            Quaternion<T> c = null;
            Multiply(a, b, ref c);
            return c;
        }

        /// <summary>Multiplies two quaternions.</summary>
        /// <param name="a">The first quaternion of the multiplication.</param>
        /// <param name="b">The second quaternion of the multiplication.</param>
        /// <returns>The resulting quaternion after the multiplication.</returns>
        public static Quaternion<T> operator *(Quaternion<T> a, Quaternion<T> b)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies two quaternions.</summary>
        /// <param name="b">The second quaternion of the multiplication.</param>
        /// <param name="c">The resulting quaternion after the multiplication.</param>
		public void Multiply(Quaternion<T> b, ref Quaternion<T> c)
        {
            Multiply(this, b, ref c);
        }

        /// <summary>Multiplies two quaternions.</summary>
        /// <param name="b">The second quaternion of the multiplication.</param>
        /// <returns>The resulting quaternion after the multiplication.</returns>
		public Quaternion<T> Multiply(Quaternion<T> b)
        {
            return this * b;
        }

        #endregion

        #region Multiply (Quaternion * Vector)

        /// <summary>Multiplies a quaternion and a vector.</summary>
        /// <param name="a">The quaternion of the multiplication.</param>
        /// <param name="b">The vector of the multiplication.</param>
        /// <param name="c">The resulting quaternion after the multiplication.</param>
        public static void Multiply(Quaternion<T> a, Vector<T> b, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (b.Dimensions != 3)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(b) + "." + nameof(b.Dimensions) + " == 3)");
            }
            T x = QuaternionMiltiplyVectorXYZComponentImplementation.Function(a.W, b.X, a.Y, b.Z, a.Z, b.Y);
            T y = QuaternionMiltiplyVectorXYZComponentImplementation.Function(a.W, b.Y, a.Z, b.X, a.X, b.Z);
            T z = QuaternionMiltiplyVectorXYZComponentImplementation.Function(a.W, b.Z, a.X, b.Y, a.Y, b.X);
            T w = QuaternionMiltiplyVectorWComponentImplementation.Function(a.X, b.X, a.Y, b.Y, a.Z, b.Z);
            if (c is null)
            {
                c = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                c._x = x;
                c._y = y;
                c._z = z;
                c._w = w;
            }
        }

        internal static class QuaternionMiltiplyVectorXYZComponentImplementation
        {
            internal static Func<T, T, T, T, T, T, T> Function = (T a, T b, T c, T d, T e, T f) =>
            {
                // parameters
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                ParameterExpression D = Expression.Parameter(typeof(T));
                ParameterExpression E = Expression.Parameter(typeof(T));
                ParameterExpression F = Expression.Parameter(typeof(T));
                // multiply
                Expression AB = Expression.Multiply(A, B);
                Expression CD = Expression.Multiply(C, D);
                Expression EF = Expression.Multiply(E, F);
                // add
                Expression AB_add_CD = Expression.Add(AB, CD);
                // subtract
                Expression AB_add_CD_subtract_EF = Expression.Subtract(AB_add_CD, EF);
                // compile
                Expression BODY = AB_add_CD_subtract_EF;
                Function = Expression.Lambda<Func<T, T, T, T, T, T, T>>(BODY, A, B, C, D, E, F).Compile();
                return Function(a, b, c, d, e, f);
            };
        }

        internal static class QuaternionMiltiplyVectorWComponentImplementation
        {
            internal static Func<T, T, T, T, T, T, T> Function = (T a, T b, T c, T d, T e, T f) =>
            {
                // parameters
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                ParameterExpression D = Expression.Parameter(typeof(T));
                ParameterExpression E = Expression.Parameter(typeof(T));
                ParameterExpression F = Expression.Parameter(typeof(T));
                // multiply
                Expression nAB = Expression.Multiply(Expression.Negate(A), B);
                Expression CD = Expression.Multiply(C, D);
                Expression EF = Expression.Multiply(E, F);
                // subtract
                Expression nAB_subtract_CD = Expression.Subtract(nAB, CD);
                Expression nAB_subtract_CD_subtract_EF = Expression.Subtract(nAB_subtract_CD, EF);
                // compile
                Expression BODY = nAB_subtract_CD_subtract_EF;
                Function = Expression.Lambda<Func<T, T, T, T, T, T, T>>(BODY, A, B, C, D, E, F).Compile();
                return Function(a, b, c, d, e, f);
            };
        }

        /// <summary>Multiplies a quaternion and a vector.</summary>
        /// <param name="a">The quaternion of the multiplication.</param>
        /// <param name="b">The vector of the multiplication.</param>
        /// <returns>The resulting quaternion after the multiplication.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> a, Vector<T> b)
        {
            Quaternion<T> c = null;
            Multiply(a, b, ref c);
            return c;
        }

        /// <summary>Multiplies a quaternion and a vector.</summary>
        /// <param name="a">The quaternion of the multiplication.</param>
        /// <param name="b">The vector of the multiplication.</param>
        /// <returns>The resulting quaternion after the multiplication.</returns>
        public static Quaternion<T> operator *(Quaternion<T> a, Vector<T> b)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies a quaternion and a vector.</summary>
        /// <param name="b">The vector of the multiplication.</param>
        /// <param name="c">The resulting quaternion after the multiplication.</param>
		public void Multiply(Vector<T> b, ref Quaternion<T> c)
        {
            Multiply(this, b, ref c);
        }

        /// <summary>Multiplies a quaternion and a vector.</summary>
        /// <param name="b">The vector of the multiplication.</param>
        /// <returns>The resulting quaternion after the multiplication.</returns>
		public Quaternion<T> Multiply(Vector<T> b)
        {
            return this * b;
        }

        #endregion

        #region Multiply (Quaternion * Scalar)

        /// <summary>Multiplies all the values in a quaternion by a scalar.</summary>
        /// <param name="a">The quaternion to have the values multiplied.</param>
        /// <param name="b">The scalar to multiply the values by.</param>
        /// <param name="c">The resulting quaternion after the multiplications.</param>
        private static void Multiply(Quaternion<T> a, T b, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            T x = Compute.Multiply(a._x, b);
            T y = Compute.Multiply(a._y, b);
            T z = Compute.Multiply(a._z, b);
            T w = Compute.Multiply(a._w, b);
            if (c is null)
            {
                c = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                c._x = x;
                c._y = y;
                c._z = z;
                c._w = w;
            }
        }

        /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The matrix to have the values multiplied.</param>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting quaternion after the multiplications.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> a, T b)
        {
            Quaternion<T> c = null;
            Multiply(a, b, ref c);
            return c;
        }

        /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
        /// <param name="b">The scalar to multiply the values by.</param>
		/// <param name="a">The quaternion to have the values multiplied.</param>
		/// <returns>The resulting quaternion after the multiplications.</returns>
		public static Quaternion<T> Multiply(T b, Quaternion<T> a)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="a">The quaternion to have the values multiplied.</param>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting quaternion after the multiplications.</returns>
        public static Quaternion<T> operator *(Quaternion<T> a, T b)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
        /// <param name="b">The scalar to multiply the values by.</param>
        /// <param name="a">The quaternion to have the values multiplied.</param>
        /// <returns>The resulting quaternion after the multiplications.</returns>
        public static Quaternion<T> operator *(T b, Quaternion<T> a)
        {
            return Multiply(b, a);
        }

        /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="b">The scalar to multiply the values by.</param>
        /// <param name="c">The resulting quaternion after the multiplications.</param>
		public void Multiply(T b, ref Quaternion<T> c)
        {
            Multiply(this, b, ref c);
        }

        /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="b">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public Quaternion<T> Multiply(T b)
        {
            return this * b;
        }

        #endregion

        #region Rotate

        /// <summary>Rotates a vector by a quaternion rotation [v' = qvq'].</summary>
        /// <param name="a">The quaternion rotation to rotate the vector by.</param>
        /// <param name="b">The vector to rotate.</param>
        /// <param name="c">The result of the rotation.</param>
        public static void Rotate(Quaternion<T> a, Vector<T> b, ref Vector<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (b.Dimensions != 3)
            {
                throw new MathematicsException("Argument invalid !(" + nameof(b) + "." + nameof(b.Dimensions) + " != 3 || " + nameof(b) + "." + nameof(b.Dimensions) + " != 4)");
            }
            if (c is null || c.Dimensions != 3)
            {
                c = new Vector<T>(3);
            }
            Quaternion<T> d = null;
            Multiply(a, b, ref d);
            Multiply(a.Conjugate(), d, ref d); // need to prevent this heep allocation on "a.Conjugate()" in future update
            c.X = d.X;
            c.Y = d.Y;
            c.X = d.W;
        }

        /// <summary>Rotates a vector by a quaternion rotation [v' = qvq'].</summary>
        /// <param name="a">The quaternion rotation to rotate the vector by.</param>
        /// <param name="b">The vector to rotate.</param>
        /// <returns>The result of the rotation.</returns>
        public static Vector<T> Rotate(Quaternion<T> a, Vector<T> b)
        {
            Vector<T> c = null;
            Rotate(a, b, ref c);
            return c;
        }

        /// <summary>Rotates a vector by a quaternion rotation [v' = qvq'].</summary>
        /// <param name="b">The vector to rotate.</param>
        /// <param name="c">The result of the rotation.</param>
        public void Rotate(Vector<T> b, ref Vector<T> c)
        {
            Rotate(this, b, ref c);
        }

        /// <summary>Rotates a vector by a quaternion rotation [v' = qvq'].</summary>
        /// <param name="b">The vector to rotate.</param>
        /// <returns>The result of the rotation.</returns>
        public Vector<T> Rotate(Vector<T> b)
        {
            Vector<T> c = null;
            Rotate(this, b, ref c);
            return c;
        }

        #endregion

        #region Conjugate

        /// <summary>Conjugates a quaternion.</summary>
        /// <param name="a">The quaternion to conjugate.</param>
        /// <param name="b">The result of the conjugation.</param>
        public static void Conjugate(Quaternion<T> a, ref Quaternion<T> b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            T x = Compute.Negate(a._x);
            T y = Compute.Negate(a._y);
            T z = Compute.Negate(a._z);
            T w = a._w;
            if (b is null)
            {
                b = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                b._x = x;
                b._y = y;
                b._z = z;
                b._w = w;
            }
        }

        /// <summary>Conjugates a quaternion.</summary>
        /// <param name="a">The quaternion to conjugate.</param>
        /// <returns>The result of the conjugation.</returns>
        public static Quaternion<T> Conjugate(Quaternion<T> a)
        {
            Quaternion<T> b = null;
            Conjugate(a, ref b);
            return b;
        }

        /// <summary>Conjugates a quaternion.</summary>
        /// <param name="b">The result of the conjugation.</param>
        public void Conjugate(ref Quaternion<T> b)
        {
            Conjugate(this, ref b);
        }

        /// <summary>Conjugates a quaternion.</summary>
        /// <returns>The result of the conjugation.</returns>
        public Quaternion<T> Conjugate()
        {
            Quaternion<T> b = null;
            Conjugate(this, ref b);
            return b;
        }

        #endregion

        #region Normalize

        /// <summary>Normalizes a quaternion.</summary>
        /// <param name="a">The quaternion to normalize.</param>
        /// <param name="b">The result of the normalization.</param>
        public static void Normalize(Quaternion<T> a, ref Quaternion<T> b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            T magnitude = a.Magnitude;
            T x = Compute.Divide(a._x, magnitude);
            T y = Compute.Divide(a._y, magnitude);
            T z = Compute.Divide(a._z, magnitude);
            T w = Compute.Divide(a._w, magnitude);
            if (b is null)
            {
                b = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                b._x = x;
                b._y = y;
                b._z = z;
                b._w = w;
            }
        }

        /// <summary>Normalizes a quaternion.</summary>
        /// <param name="a">The quaternion to normalize.</param>
        /// <returns>The result of the normalization.</returns>
        public static Quaternion<T> Normalize(Quaternion<T> a)
        {
            Quaternion<T> b = null;
            Normalize(a, ref b);
            return b;
        }

        /// <summary>Normalizes a quaternion.</summary>
        /// <param name="a">The quaternion to normalize.</param>
        /// <param name="b">The result of the normalization.</param>
        public void Normalize(ref Quaternion<T> b)
        {
            Normalize(this, ref b);
        }

        /// <summary>Normalizes a quaternion.</summary>
        /// <param name="a">The quaternion to normalize.</param>
        /// <returns>The result of the normalization.</returns>
        public Quaternion<T> Normalize()
        {
            Quaternion<T> b = null;
            Normalize(this, ref b);
            return b;
        }

        #endregion

        #region Invert

        /// <summary>Inverts a quaternion.</summary>
        /// <param name="a">The quaternion to invert.</param>
        /// <param name="b">The result of the inversion.</param>
        public static void Invert(Quaternion<T> a, ref Quaternion<T> b)
        {
            // Note: I think this function is incorrect. Need to research.

            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            T magnitudeSquared = a.MagnitudeSquared;
            T x;
            T y;
            T z;
            T w;
            if (Compute.Equal(magnitudeSquared, Constant<T>.Zero))
            {
                x = a._x;
                y = a._y;
                z = a._z;
                w = a._w;
            }
            else
            {
                x = Compute.Multiply(Compute.Negate(a.X), magnitudeSquared);
                y = Compute.Multiply(Compute.Negate(a.Y), magnitudeSquared);
                z = Compute.Multiply(Compute.Negate(a.Z), magnitudeSquared);
                w = Compute.Multiply(a.W, magnitudeSquared);
            }
            if (b is null)
            {
                b = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                b._x = x;
                b._y = y;
                b._z = z;
                b._w = w;
            }
        }

        /// <summary>Inverts a quaternion.</summary>
        /// <param name="a">The quaternion to invert.</param>
        /// <returns>The result of the inversion.</returns>
        public static Quaternion<T> Invert(Quaternion<T> a)
        {
            Quaternion<T> b = null;
            Invert(a, ref b);
            return b;
        }

        /// <summary>Inverts a quaternion.</summary>
        /// <param name="b">The result of the inversion.</param>
        public void Invert(ref Quaternion<T> b)
        {
            Invert(this, ref b);
        }

        /// <summary>Inverts a quaternion.</summary>
        /// <returns>The result of the inversion.</returns>
        public Quaternion<T> Invert()
        {
            Quaternion<T> b = null;
            Invert(this, ref b);
            return b;
        }

        #endregion

        #region LinearInterpolation

        /// <summary>Linear interpolation for quaternions.</summary>
        /// <param name="a">The min of the interpolation.</param>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <param name="c">The result of the linear interpolation.</param>
        public static void LinearInterpolation(Quaternion<T> a, Quaternion<T> b, T blend, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (Compute.LessThan(blend, Constant<T>.Zero) || Compute.GreaterThan(blend, Constant<T>.One))
            {
                throw new ArgumentOutOfRangeException(nameof(blend), blend, "!(0 <= " + nameof(blend) + " <= 1)");
            }
            T x;
            T y;
            T z;
            T w;
            if (GetHasZeroMagnitude(a))
            {
                if (GetHasZeroMagnitude(b))
                {
                    x = Constant<T>.Zero;
                    y = Constant<T>.Zero;
                    z = Constant<T>.Zero;
                    w = Constant<T>.One;
                }
                else
                {
                    x = b._x;
                    y = b._y;
                    z = b._z;
                    w = b._w;
                }
            }
            else if (GetHasZeroMagnitude(b))
            {
                x = a._x;
                y = a._y;
                z = a._z;
                w = a._w;
            }
            else
            {
                x = Compute.Add(Compute.Subtract(Constant<T>.One, Compute.Multiply(blend, a.X)), Compute.Multiply(blend, b.X));
                y = Compute.Add(Compute.Subtract(Constant<T>.One, Compute.Multiply(blend, a.Y)), Compute.Multiply(blend, b.Y));
                z = Compute.Add(Compute.Subtract(Constant<T>.One, Compute.Multiply(blend, a.Z)), Compute.Multiply(blend, b.Z));
                w = Compute.Add(Compute.Subtract(Constant<T>.One, Compute.Multiply(blend, a.W)), Compute.Multiply(blend, b.W));
            }
            if (c is null)
            {
                c = new Quaternion<T>(x, y, z, w);
            }
            else
            {
                c._x = x;
                c._y = y;
                c._z = z;
                c._w = w;
            }
            if (!GetHasZeroMagnitude(c))
            {
                Normalize(c, ref c);
            }
            else
            {
                c._x = Constant<T>.Zero;
                c._y = Constant<T>.Zero;
                c._z = Constant<T>.Zero;
                c._w = Constant<T>.One;
            }
        }

        /// <summary>Linear interpolation for quaternions.</summary>
        /// <param name="a">The min of the interpolation.</param>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <returns>The result of the linear interpolation.</returns>
        public static Quaternion<T> LinearInterpolation(Quaternion<T> a, Quaternion<T> b, T blend)
        {
            Quaternion<T> c = null;
            LinearInterpolation(a, b, blend, ref c);
            return c;
        }

        /// <summary>Linear interpolation for quaternions.</summary>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <param name="c">The result of the linear interpolation.</param>
        public void LinearInterpolation(Quaternion<T> b, T blend, ref Quaternion<T> c)
        {
            LinearInterpolation(this, b, blend, ref c);
        }

        /// <summary>Linear interpolation for quaternions.</summary>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <returns>The result of the linear interpolation.</returns>
        public Quaternion<T> LinearInterpolation(Quaternion<T> b, T blend)
        {
            Quaternion<T> c = null;
            LinearInterpolation(this, b, blend, ref c);
            return c;
        }

        #endregion

        #region SphericalInterpolation

        /// <summary>Spherical interpolation for quaternions.</summary>
        /// <param name="a">The min of the interpolation.</param>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <param name="c">The result of the spherical interpolation.</param>
        public static void SphericalInterpolation(Quaternion<T> a, Quaternion<T> b, T blend, ref Quaternion<T> c)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (Compute.LessThan(blend, Constant<T>.Zero) || Compute.GreaterThan(blend, Constant<T>.One))
            {
                throw new ArgumentOutOfRangeException(nameof(blend), blend, "!(0 <= " + nameof(blend) + " <= 1)");
            }
            throw new NotImplementedException();
        }

        /// <summary>Spherical interpolation for quaternions.</summary>
        /// <param name="a">The min of the interpolation.</param>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <returns>The result of the spherical interpolation.</returns>
        public static Quaternion<T> SphericalInterpolation(Quaternion<T> a, Quaternion<T> b, T blend)
        {
            Quaternion<T> c = null;
            LinearInterpolation(a, b, blend, ref c);
            return c;
        }

        /// <summary>Spherical interpolation for quaternions.</summary>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <param name="c">The result of the spherical interpolation.</param>
        public void SphericalInterpolation(Quaternion<T> b, T blend, ref Quaternion<T> c)
        {
            LinearInterpolation(this, b, blend, ref c);
        }

        /// <summary>Spherical interpolation for quaternions.</summary>
        /// <param name="b">The max of the interpolation.</param>
        /// <param name="blend">The blending point of the interpolation.</param>
        /// <returns>The result of the spherical interpolation.</returns>
        public Quaternion<T> SphericalInterpolation(Quaternion<T> b, T blend)
        {
            Quaternion<T> c = null;
            LinearInterpolation(this, b, blend, ref c);
            return c;
        }

        #endregion

        #region ToMatrix3x3

        /// <summary>Converts a quaternion into a 3x3 matrix.</summary>
        /// <param name="quaternion">The quaternion of the conversion.</param>
        /// <returns>The resulting 3x3 matrix.</returns>
        public static Matrix<T> ToMatrix3x3(Quaternion<T> quaternion)
        {
            // Note: this Method needs optimization...

            return new Matrix<T>(3, 3, (int x, int y) =>
            {
                switch (x)
                {
                    case 0:
                        switch (y)
                        {
                            case 0: return Compute.Subtract(Compute.Subtract(Compute.Add(Compute.Multiply(quaternion.W, quaternion.W), Compute.Multiply(quaternion.X, quaternion.X)), Compute.Multiply(quaternion.Y, quaternion.Y)), Compute.Multiply(quaternion.Z, quaternion.Z));
                            case 1: return Compute.Subtract(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Y), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Z));
                            case 2: return Compute.Add(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Y));
                            default: throw new MathematicsException("BUG");
                        }
                    case 1:
                        switch (y)
                        {
                            case 0: return Compute.Add(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Y), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Z));
                            case 1: return Compute.Subtract(Compute.Add(Compute.Subtract(Compute.Multiply(quaternion.W, quaternion.W), Compute.Multiply(quaternion.X, quaternion.X)), Compute.Multiply(quaternion.Y, quaternion.Y)), Compute.Multiply(quaternion.Z, quaternion.Z));
                            case 2: return Compute.Add(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.Y), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.X));
                            default: throw new MathematicsException("BUG");
                        }
                    case 2:
                        switch (y)
                        {
                            case 0: return Compute.Subtract(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.X), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.Y));
                            case 1: return Compute.Subtract(Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.Y), quaternion.Z), Compute.Multiply(Compute.Multiply(Compute.FromInt32<T>(2), quaternion.W), quaternion.X));
                            case 2: return Compute.Add(Compute.Subtract(Compute.Subtract(Compute.Multiply(quaternion.W, quaternion.W), Compute.Multiply(quaternion.X, quaternion.X)), Compute.Multiply(quaternion.Y, quaternion.Y)), Compute.Multiply(quaternion.Z, quaternion.Z));
                            default: throw new MathematicsException("BUG");
                        }
                    default:
                        throw new MathematicsException("BUG");
                }
            });
        }

        #endregion

        #region Equal

        /// <summary>Does a value equality check.</summary>
        /// <param name="a">The first quaternion to check for equality.</param>
        /// <param name="b">The second quaternion to check for equality.</param>
        /// <returns>True if values are equal, false if not.</returns>
        private static bool Equal(Quaternion<T> a, Quaternion<T> b)
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
            return 
                Compute.Equal(a._x, b._x) &&
                Compute.Equal(a._y, b._y) &&
                Compute.Equal(a._z, b._z) &&
                Compute.Equal(a._w, b._w);
        }

        /// <summary>Does a value equality check.</summary>
		/// <param name="a">The first quaternion to check for equality.</param>
		/// <param name="b">The second quaternion to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
        public static bool operator ==(Quaternion<T> a, Quaternion<T> b)
        {
            return Equal(a, b);
        }

        /// <summary>Does a value non-equality check.</summary>
		/// <param name="a">The first quaternion to check for non-equality.</param>
		/// <param name="b">The second quaternion to check for non-equality.</param>
		/// <returns>True if values are not equal, false if not.</returns>
		public static bool operator !=(Quaternion<T> a, Quaternion<T> b)
        {
            return !Equal(a, b);
        }

        /// <summary>Does a value equality check.</summary>
		/// <param name="b">The second quaternion to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool Equal(Quaternion<T> b)
        {
            return this == b;
        }

        #endregion

        #region Equal (+leniency)

        /// <summary>Does a value equality check with leniency.</summary>
		/// <param name="a">The first quaternion to check for equality.</param>
		/// <param name="b">The second quaternion to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
        private static bool Equal(Quaternion<T> a, Quaternion<T> b, T leniency)
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
            return
                Compute.EqualLeniency(a._x, b._x, leniency) &&
                Compute.EqualLeniency(a._y, b._y, leniency) &&
                Compute.EqualLeniency(a._z, b._z, leniency) &&
                Compute.EqualLeniency(a._w, b._w, leniency);
        }

        /// <summary>Does a value equality check with leniency.</summary>
		/// <param name="b">The second quaternion to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool Equal(Quaternion<T> b, T leniency)
        {
            return Equal(this, b, leniency);
        }

        #endregion

        #endregion

        #region Other Methods

        #region Clone

        /// <summary>Creates a copy of a quaternion.</summary>
        /// <param name="a">The quaternion to copy.</param>
        /// <returns>The copy of this quaternion.</returns>
        public static Quaternion<T> Clone(Quaternion<T> a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            return new Quaternion<T>(a._x, a._y, a._z, a._w);
        }

        /// <summary>Copies this matrix.</summary>
        /// <returns>The copy of this matrix.</returns>
        public Quaternion<T> Clone()
        {
            return Clone(this);
        }

        #endregion

        #endregion

        #region Overrides

        /// <summary>Converts the quaternion into a string.</summary>
        /// <returns>The resulting string after the conversion.</returns>
        public override string ToString()
        {
            return base.ToString();
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
            {
                return Quaternion<T>.Equal(this, (Quaternion<T>)other);
            }
            return false;
        }

        #endregion
    }
}
