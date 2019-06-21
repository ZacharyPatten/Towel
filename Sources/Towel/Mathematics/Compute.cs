using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Towel.Algorithms;
using Towel.DataStructures;
using Towel.Measurements;

namespace Towel.Mathematics
{
    /// <summary>Static Generic Mathematics Computation.</summary>
    public static class Compute
    {
        #region Internal Optimizations

        // These are some shared internal optimizations that I don't want to expose because it might confuse people.
        // If you need to use it, just copy this code into your own project.

        /// <summary>a * b + c</summary>
        internal static class MultiplyAddImplementation<T>
        {
            internal static Func<T, T, T, T> Function = (T a, T b, T c) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Add(Expression.Multiply(A, B), C);
                Function = Expression.Lambda<Func<T, T, T, T>>(BODY, A, B, C).Compile();
                return Function(a, b, c);
            };
        }

        #endregion

        #region Pi

        /// <summary>Computes the value of pi for the provided generic type.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="predicate">The cancellation token for cutting off computation.</param>
        /// <returns>The computed value of pi.</returns>
        public static T Pi<T>(Predicate<T> predicate = null)
        {
            // Series: PI = 2 * (1 + 1/3 * (1 + 2/5 * (1 + 3/7 * (...))))
            // more terms in computation inproves accuracy

            if (predicate is null)
            {
                int iterations = 0;
                predicate = PI => iterations < 100;
            }

            T pi = Constant<T>.One;
            T previous = Constant<T>.Zero;
            for (int i = 1; NotEqual(previous, pi) && predicate(pi); i++)
            {
                previous = pi;
                pi = Constant<T>.One;
                for (int j = i; j >= 1; j--)
                {
                    #region Without Custom Runtime Compilation

                    //T J = FromInt32<T>(j);
                    //T a = Add(Multiply(Constant<T>.Two, J), Constant<T>.One);
                    //T b = Divide(J, a);
                    //T c = Multiply(b, pi);
                    //T d = Add(Constant<T>.One, c);
                    //pi = d;

                    #endregion

                    pi = AddMultiplyDivideAddImplementation<T>.Function(Convert<int, T>(j), pi);
                }
                pi = Multiply(Constant<T>.Two, pi);
            }
            pi = Maximum(pi, Constant<T>.Three);
            return pi;
        }

        internal static class AddMultiplyDivideAddImplementation<T>
        {
            internal static Func<T, T, T> Function = (T j, T pi) =>
            {
                ParameterExpression J = Expression.Parameter(typeof(T));
                ParameterExpression PI = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Add(
                    Expression.Constant(Constant<T>.One),
                    Expression.Multiply(
                        PI,
                        Expression.Divide(
                            J,
                            Expression.Add(
                                Expression.Multiply(
                                    Expression.Constant(Constant<T>.Two),
                                    J),
                                Expression.Constant(Constant<T>.One)))));
                Function = Expression.Lambda<Func<T, T, T>>(BODY, J, PI).Compile();
                return Function(j, pi);
            };
        }

        #endregion

        #region Epsilon

        // Note sure if this method will be necessary.

        //private static T ComputeEpsilon<T>()
        //{
        //    if (typeof(T) == typeof(float))
        //    {
        //        return (T)(object)float.Epsilon;
        //    }
        //    throw new NotImplementedException();
        //}

        #endregion

        #region Convert

        /// <summary>Converts a value from one type to another.</summary>
        /// <typeparam name="A">The generic type to convert from.</typeparam>
        /// <typeparam name="B">The generic type to convert to.</typeparam>
        /// <param name="a">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static B Convert<A, B>(A a)
        {
            return ConvertImplementation<A, B>.Function(a);
        }

        internal static class ConvertImplementation<A, B>
        {
            internal static Func<A, B> Function = (A a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(A));
                Expression BODY = Expression.Convert(A, typeof(B));
                Function = Expression.Lambda<Func<A, B>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region Negate

        /// <summary>Negates a numeric value [-a].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to negate.</param>
        /// <returns>The result of the negation.</returns>
        public static T Negate<T>(T a)
        {
            return NegateImplementation<T>.Function(a);
        }

        internal static class NegateImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Negate(A);
                Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region Add

        /// <summary>Adds two numeric values [a + b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the addition.</param>
        /// <param name="b">The second operand of the addition.</param>
        /// <returns>The result of the addition.</returns>
        public static T Add<T>(T a, T b)
        {
            return AddImplementation<T>.Function(a, b);
        }

        /// <summary>Adds multiple numeric values [a + b + c...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the addition.</param>
        /// <param name="b">The second operand of the addition.</param>
        /// <param name="c">The third operand of the addition.</param>
        /// <param name="d">The remaining operands of the addition.</param>
        /// <returns>The result of the addition.</returns>
        public static T Add<T>(T a, T b, T c, params T[] d)
        {
            return Add((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Adds multiple numeric values [step_1 + step_2 + step_3...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The stepper containing the values.</param>
        /// <returns>The result of the addition.</returns>
        public static T Add<T>(Stepper<T> stepper)
        {
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Add(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            return result;
        }

        internal static class AddImplementation<T>
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

        #endregion

        #region Subtract

        /// <summary>Subtracts two numeric values [a - b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the subtraction.</param>
        /// <param name="b">The second operand of the subtraction.</param>
        /// <returns>The result of the subtraction.</returns>
        public static T Subtract<T>(T a, T b)
        {
            return SubtractImplementation<T>.Function(a, b);
        }

        /// <summary>Subtracts multiple numeric values [a - b - c...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the subtraction.</param>
        /// <param name="b">The first second of the subtraction.</param>
        /// <param name="c">The first third of the subtraction.</param>
        /// <param name="d">The remaining values of the subtraction.</param>
        /// <returns>The result of the subtraction.</returns>
        public static T Subtract<T>(T a, T b, T c, params T[] d)
        {
            return Subtract((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Subtracts multiple numeric values [step_1 - step_2 - step_3...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The stepper containing the values.</param>
        /// <returns>The result of the subtraction.</returns>
        public static T Subtract<T>(Stepper<T> stepper)
        {
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Subtract(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            return result;
        }

        internal static class SubtractImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Subtract(A, B);
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Multiply

        /// <summary>Multiplies two numeric values [a * b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the multiplication.</param>
        /// <param name="b">The second operand of the multiplication.</param>
        /// <returns>The result of the multiplication.</returns>
        public static T Multiply<T>(T a, T b)
        {
            return MultiplyImplementation<T>.Function(a, b);
        }

        /// <summary>Multiplies multiple numeric values [a * b * c...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the multiplication.</param>
        /// <param name="b">The second operand of the multiplication.</param>
        /// <param name="c">The third operand of the multiplication.</param>
        /// <param name="d">The remaining values of the multiplication.</param>
        /// <returns>The result of the multiplication.</returns>
        public static T Multiply<T>(T a, T b, T c, params T[] d)
        {
            return Multiply((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Multiplies multiple numeric values [step_1 * step_2 * step_3...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The stepper containing the values.</param>
        /// <returns>The result of the multiplication.</returns>
        public static T Multiply<T>(Stepper<T> stepper)
        {
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Multiply(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            return result;
        }

        internal static class MultiplyImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Multiply(A, B);
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Divide

        /// <summary>Divides two numeric values [a / b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the division.</param>
        /// <param name="b">The second operand of the division.</param>
        /// <returns>The result of the division.</returns>
        public static T Divide<T>(T a, T b)
        {
            return DivideImplementation<T>.Function(a, b);
        }

        /// <summary>Divides multiple numeric values [a / b / c...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the division.</param>
        /// <param name="b">The second operand of the division.</param>
        /// <param name="c">The third operand of the division.</param>
        /// <param name="d">The remaining values of the division.</param>
        /// <returns>The result of the division.</returns>
        public static T Divide<T>(T a, T b, T c, params T[] d)
        {
            return Divide((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Divides multiple numeric values [step_1 / step_2 / step_3...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The stepper containing the values.</param>
        /// <returns>The result of the division.</returns>
        public static T Divide<T>(Stepper<T> stepper)
        {
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Divide(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            return result;
        }

        internal static class DivideImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Divide(A, B);
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Invert

        /// <summary>Inverts a numeric value [1 / a].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The numeric value to invert.</param>
        /// <returns>The result of the inversion.</returns>
		public static T Invert<T>(T a)
        {
            return InvertImplementation<T>.Function(a);
        }

        internal static class InvertImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Divide(Expression.Constant(Constant<T>.One), A);
                Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region Modulo

        /// <summary>Modulos two numeric values [a % b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the modulation.</param>
        /// <param name="b">The second operand of the modulation.</param>
        /// <returns>The result of the modulation.</returns>
        public static T Modulo<T>(T a, T b)
        {
            return ModuloImplementation<T>.Function(a, b);
        }

        /// <summary>Modulos multiple numeric values [a % b % c...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the modulation.</param>
        /// <param name="b">The second operand of the modulation.</param>
        /// <param name="c">The third operand of the modulation.</param>
        /// <param name="d">The remaining values of the modulation.</param>
        /// <returns>The result of the modulation.</returns>
        public static T Modulo<T>(T a, T b, T c, params T[] d)
        {
            return Modulo((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Modulos multiple numeric values [step_1 % step_2 % step_3...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The stepper containing the values.</param>
        /// <returns>The result of the modulation.</returns>
        public static T Modulo<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Modulo(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return result;
        }

        internal static class ModuloImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Modulo(A, B);
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Power

        /// <summary>Powers two numeric values [a ^ b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the power.</param>
        /// <param name="b">The first operand of the power.</param>
        /// <returns>The result of the power.</returns>
        public static T Power<T>(T a, T b)
        {
            return PowerImplementation<T>.Function(a, b);
        }

        /// <summary>Powers multiple numeric values [a ^ b ^ c...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the power.</param>
        /// <param name="b">The second operand of the power.</param>
        /// <param name="c">The third operand of the power.</param>
        /// <param name="d">The remaining values of the power.</param>
        /// <returns>The result of the power.</returns>
        public static T Power<T>(T a, T b, T c, params T[] d)
        {
            return Power((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Powers multiple numeric values [step_1 ^ step_2 ^ step_3...].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The stepper containing the values.</param>
        /// <returns>The result of the power.</returns>
        public static T Power<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Power(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return result;
        }

        internal static class PowerImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                // Note: this code needs to die.. but this works until it gets a better version

                // optimization for specific known types
                if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    ParameterExpression B = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Pow)), Expression.Convert(A, typeof(double)), Expression.Convert(B, typeof(double))), typeof(T));
                    Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                }
                else
                {
                    Function = (T A, T B) =>
                    {
                        if (IsInteger(B) && IsPositive(B))
                        {
                            T result = A;
                            int power = Convert<T, int>(B);
                            for (int i = 0; i < power; i++)
                            {
                                result = Multiply(result, A);
                            }
                            return result;
                        }
                        else
                        {
                            throw new NotImplementedException("This feature is still in development.");
                        }
                    };
                }
                return Function(a, b);
            };
        }

        #endregion

        #region SquareRoot

        /// <summary>Square roots a numeric value [√a].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The numeric value to square root.</param>
        /// <returns>The result of the square root.</returns>
        public static T SquareRoot<T>(T a)
        {
            return SquareRootImplementation<T>.Function(a);
        }

        internal static class SquareRootImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                #region Optimization(int)

                if (typeof(T) == typeof(int))
                {
                    int SquareRoot(int x)
                    {
                        if (x == 0 || x == 1)
                        {
                            return x;
                        }
                        int start = 1, end = x, ans = 0;
                        while (start <= end)
                        {
                            int mid = (start + end) / 2;
                            if (mid * mid == x)
                            {
                                return mid;
                            }
                            if (mid * mid < x)
                            {
                                start = mid + 1;
                                ans = mid;
                            }
                            else
                            {
                                end = mid - 1;
                            }
                        }
                        return ans;
                    }
                    SquareRootImplementation<int>.Function = SquareRoot;
                    return Function(a);
                }

                #endregion

                return Root(a, Constant<T>.Two);
            };
        }

        #endregion

        #region Root

        /// <summary>Roots two numeric values [a ^ (1 / b)].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The base of the root.</param>
        /// <param name="b">The root of the operation.</param>
        /// <returns>The result of the root.</returns>
        public static T Root<T>(T a, T b)
        {
            return Power(a, Invert(b));
        }

        #endregion

        #region Logarithm

        public static T Logarithm<T>(T value, T @base)
        {
            return LogarithmImplementation<T>.Function(value, @base);
        }

        internal static class LogarithmImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                throw new NotImplementedException();

                //ParameterExpression A = Expression.Parameter(typeof(T));
                //ParameterExpression B = Expression.Parameter(typeof(T));
                //Expression BODY = ;
                //Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                //return Function(a, b);
            };
        }

        #endregion

        #region IsInteger

        /// <summary>Determines if a numerical value is an integer.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine integer status of.</param>
        /// <returns>Whether or not the value is an integer.</returns>
        public static bool IsInteger<T>(T a)
        {
            return IsIntegerImplementation<T>.Function(a);
        }

        internal static class IsIntegerImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Equal(
                    Expression.Modulo(A, Expression.Constant(Constant<T>.One)),
                    Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsNonNegative

        /// <summary>Determines if a numerical value is non-negative.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine non-negative status of.</param>
        /// <returns>Whether or not the value is non-negative.</returns>
        public static bool IsNonNegative<T>(T a)
        {
            return IsNonNegativeImplementation<T>.Function(a);
        }

        internal static class IsNonNegativeImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.GreaterThanOrEqual(A, Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsNegative

        /// <summary>Determines if a numerical value is negative.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine negative status of.</param>
        /// <returns>Whether or not the value is negative.</returns>
        public static bool IsNegative<T>(T a)
        {
            return IsNegativeImplementation<T>.Function(a);
        }

        internal static class IsNegativeImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(bool));
                Expression BODY = Expression.LessThan(A, Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsPositive

        /// <summary>Determines if a numerical value is positive.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine positive status of.</param>
        /// <returns>Whether or not the value is positive.</returns>
        public static bool IsPositive<T>(T a)
        {
            return IsPositiveImplementation<T>.Function(a);
        }

        internal static class IsPositiveImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.GreaterThan(A, Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsEven

        /// <summary>Determines if a numerical value is even.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine even status of.</param>
        /// <returns>Whether or not the value is even.</returns>
        public static bool IsEven<T>(T a)
        {
            return IsEvenImplementation<T>.Function(a);
        }

        internal static class IsEvenImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Equal(Expression.Modulo(A, Expression.Constant(Constant<T>.Two)), Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsOdd

        /// <summary>Determines if a numerical value is odd.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine odd status of.</param>
        /// <returns>Whether or not the value is odd.</returns>
        public static bool IsOdd<T>(T a)
        {
            return IsOddImplementation<T>.Function(a);
        }

        internal static class IsOddImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY =
                    Expression.Block(
                        Expression.IfThen(
                            Expression.LessThan(A, Expression.Constant(Constant<T>.Zero)),
                            Expression.Assign(A, Expression.Negate(A))),
                        Expression.Equal(Expression.Modulo(A, Expression.Constant(Constant<T>.Two)), Expression.Constant(Constant<T>.One)));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsPrime

        /// <summary>Determines if a numerical value is prime.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to determine prime status of.</param>
        /// <returns>Whether or not the value is prime.</returns>
        public static bool IsPrime<T>(T a)
        {
            if (IsInteger(a) && !LessThan(a, Constant<T>.Two))
            {
                if (Equal(a, Constant<T>.Two))
                {
                    return true;
                }
                if (IsEven(a))
                {
                    return false;
                }
                T squareRoot = SquareRoot(a);
                for (T divisor = Constant<T>.Three; LessThanOrEqual(divisor, squareRoot); divisor = Add(divisor, Constant<T>.Two))
                {
                    if (Equal(Modulo<T>(a, divisor), Constant<T>.Zero))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

            //return IsPrimeImplementation<T>.Function(value);
        }

        internal static class IsPrimeImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                if (IsInteger(a))
                {
                    if (Equal(a, Constant<T>.Two))
                    {
                        return true;
                    }
                    T squareRoot = SquareRoot(a);
                    for (T divisor = Constant<T>.Three; LessThanOrEqual(divisor, squareRoot); divisor = Add(divisor, Constant<T>.Two))
                    {
                        if (Equal(Modulo(a, divisor), Constant<T>.Zero))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            };
        }

        #endregion

        #region AbsoluteValue

        /// <summary>Gets the absolute value of a value.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to get the absolute value of.</param>
        /// <returns>The absolute value of the provided value.</returns>
        public static T AbsoluteValue<T>(T a)
        {
            return AbsoluteValueImplementation<T>.Function(a);
        }

        internal static class AbsoluteValueImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(T));
                Expression BODY = Expression.Block(
                    Expression.IfThenElse(
                        Expression.LessThan(A, Expression.Constant(Constant<T>.Zero)),
                        Expression.Return(RETURN, Expression.Negate(A)),
                        Expression.Return(RETURN, A)),
                    Expression.Label(RETURN, Expression.Constant(default(T))));
                Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region Maximum

        /// <summary>Computes the maximum of two numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the maximum operation.</param>
        /// <param name="b">The second operand of the maximum operation.</param>
        /// <returns>The computed maximum of the provided values.</returns>
        public static T Maximum<T>(T a, T b)
        {
            return MaximumImplementation<T>.Function(a, b);
        }

        /// <summary>Computes the maximum of multiple numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the maximum operation.</param>
        /// <param name="b">The second operand of the maximum operation.</param>
        /// <param name="c">The third operand of the maximum operation.</param>
        /// <param name="d">The remaining operands of the maximum operation.</param>
        /// <returns>The computed maximum of the provided values.</returns>
        public static T Maximum<T>(T a, T b, T c, params T[] d)
        {
            return Maximum((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Computes the maximum of multiple numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The set of data to compute the maximum of.</param>
        /// <returns>The computed maximum of the provided values.</returns>
        public static T Maximum<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Maximum(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return result;
        }

        internal static class MaximumImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(T));
                Expression BODY = Expression.Block(
                    Expression.IfThenElse(
                        Expression.LessThan(A, B),
                        Expression.Return(RETURN, B),
                        Expression.Return(RETURN, A)),
                    Expression.Label(RETURN, Expression.Constant(default(T))));
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Minimum

        /// <summary>Computes the minimum of two numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the minimum operation.</param>
        /// <param name="b">The second operand of the minimum operation.</param>
        /// <returns>The computed minimum of the provided values.</returns>
        public static T Minimum<T>(T a, T b)
        {
            return MinimumImplementation<T>.Function(a, b);
        }

        /// <summary>Computes the minimum of multiple numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the minimum operation.</param>
        /// <param name="b">The second operand of the minimum operation.</param>
        /// <param name="c">The third operand of the minimum operation.</param>
        /// <param name="d">The remaining operands of the minimum operation.</param>
        /// <returns>The computed minimum of the provided values.</returns>
        public static T Minimum<T>(T a, T b, T c, params T[] d)
        {
            return Minimum((Step<T> step) => { step(a); step(b); step(c); d.ToStepper()(step); });
        }

        /// <summary>Computes the minimum of multiple numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The set of data to compute the minimum of.</param>
        /// <returns>The computed minimum of the provided values.</returns>
        public static T Minimum<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            T result = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    result = Minimum(result, a);
                }
                else
                {
                    result = a;
                    assigned = true;
                }
            }
            stepper(step);
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return result;
        }

        internal static class MinimumImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(T));
                Expression BODY = Expression.Block(
                    Expression.IfThenElse(
                        Expression.GreaterThan(A, B),
                        Expression.Return(RETURN, B),
                        Expression.Return(RETURN, A)),
                    Expression.Label(RETURN, Expression.Constant(default(T))));
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Clamp

        /// <summary>Gets a value restricted to a minimum and maximum range.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minimum">The minimum of the range to clamp the value by.</param>
        /// <param name="maximum">The maximum of the range to clamp the value by.</param>
        /// <returns>The value restricted to the provided range.</returns>
        public static T Clamp<T>(T value, T minimum, T maximum)
        {
            return ClampImplementation<T>.Function(value, minimum, maximum);
        }

        internal static class ClampImplementation<T>
        {
            internal static Func<T, T, T, T> Function = (T value, T minimum, T maximum) =>
            {
                ParameterExpression VALUE = Expression.Parameter(typeof(T));
                ParameterExpression MINIMUM = Expression.Parameter(typeof(T));
                ParameterExpression MAXIMUM = Expression.Parameter(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(T));
                Expression BODY = Expression.Block(
                    Expression.IfThenElse(
                        Expression.LessThan(VALUE, MINIMUM),
                        Expression.Return(RETURN, MINIMUM),
                        Expression.IfThenElse(
                            Expression.GreaterThan(VALUE, MAXIMUM),
                            Expression.Return(RETURN, MAXIMUM),
                            Expression.Return(RETURN, VALUE))),
                    Expression.Label(RETURN, Expression.Constant(default(T))));
                Function = Expression.Lambda<Func<T, T, T, T>>(BODY, VALUE, MINIMUM, MAXIMUM).Compile();
                return Function(value, minimum, maximum);
            };
        }

        #endregion

        #region EqualLeniency

        /// <summary>Checks for equality between two numeric values with a range of possibly leniency.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the equality check.</param>
        /// <param name="b">The second operand of the equality check.</param>
        /// <param name="leniency">The allowed distance between the values to still be considered equal.</param>
        /// <returns>True if the values are within the allowed leniency of each other. False if not.</returns>
        public static bool EqualLeniency<T>(T a, T b, T leniency)
        {
            // TODO: add an ArgumentOutOfBounds check on leniency

            return EqualLeniencyImplementation<T>.Function(a, b, leniency);
        }

        internal static class EqualLeniencyImplementation<T>
        {
            internal static Func<T, T, T, bool> Function = (T a, T b, T c) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                ParameterExpression D = Expression.Variable(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(bool));
                Expression BODY = Expression.Block(new ParameterExpression[] { D },
                    Expression.Assign(D, Expression.Subtract(A, B)),
                    Expression.IfThenElse(
                        Expression.LessThan(D, Expression.Constant(Constant<T>.Zero)),
                        Expression.Assign(D, Expression.Negate(D)),
                        Expression.Assign(D, D)),
                    Expression.Return(RETURN, Expression.LessThanOrEqual(D, C), typeof(bool)),
                    Expression.Label(RETURN, Expression.Constant(default(bool))));
                Function = Expression.Lambda<Func<T, T, T, bool>>(BODY, A, B, C).Compile();
                return Function(a, b, c);
            };
        }

        #endregion

        #region Compare

        /// <summary>Compares two numeric values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the comparison.</param>
        /// <param name="b">The second operand of the comparison.</param>
        /// <returns>The result of the comparison.</returns>
        public static CompareResult Compare<T>(T a, T b)
        {
            return CompareImplementation<T>.Function(a, b);
        }

        internal static class CompareImplementation<T>
        {
            internal static Func<T, T, CompareResult> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                LabelTarget RETURN = Expression.Label(typeof(CompareResult));
                Expression BODY = Expression.Block(
                    Expression.IfThen(
                            Expression.LessThan(A, B),
                            Expression.Return(RETURN, Expression.Constant(CompareResult.Less))),
                        Expression.IfThen(
                            Expression.GreaterThan(A, B),
                            Expression.Return(RETURN, Expression.Constant(CompareResult.Greater))),
                        Expression.Return(RETURN, Expression.Constant(CompareResult.Equal)),
                        Expression.Label(RETURN, Expression.Constant(default(CompareResult), typeof(CompareResult))));
                //Expression.IfThenElse(
                //    Expression.LessThan(A, B),
                //    Expression.Return(RETURN, Expression.Constant(Comparison.Less)),
                //    Expression.IfThenElse(
                //        Expression.GreaterThan(A, B),
                //        Expression.Return(RETURN, Expression.Constant(Comparison.Greater)),
                //        Expression.Return(RETURN, Expression.Constant(Comparison.Equal)))),
                //Expression.Label(RETURN, Expression.Constant(default(Comparison), typeof(Comparison))));
                Function = Expression.Lambda<Func<T, T, CompareResult>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Equal

        /// <summary>Checks two numeric values for equality [a == b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the equality check.</param>
        /// <param name="b">The second operand of the equality check.</param>
        /// <returns>The result of the equality check.</returns>
        public static bool Equal<T>(T a, T b)
        {
            return EqualImplementation<T>.Function(a, b);
        }

        /// <summary>Checks for equality among multiple numeric operands.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the equality check.</param>
        /// <param name="b">The second operand of the equality check.</param>
        /// <param name="c">The remaining operands of the equality check.</param>
        /// <returns>True if all operand are equal. False if not.</returns>
        public static bool Equal<T>(T a, T b, params T[] c)
        {
            if (NotEqual(a, b))
            {
                return false;
            }
            if (c.Length > 0)
            {
                if (NotEqual(a, c[0]))
                {
                    return false;
                }
                for (int i = 1; i < c.Length; i++)
                {
                    if (NotEqual(c[i - 1], c[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>Checks for equality among multiple numeric operands.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The operands of the equality check.</param>
        /// <returns>True if all operand are equal. False if not.</returns>
        public static bool Equal<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            bool result = true;
            T value = default(T);
            bool assigned = false;
            void step(T a)
            {
                if (assigned)
                {
                    if (!Equal(value, a))
                    {
                        result = false;
                    }
                }
                else
                {
                    value = a;
                    assigned = true;
                }
            }
            stepper(step);
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return result;
        }

        internal static class EqualImplementation<T>
        {
            internal static Func<T, T, bool> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Equal(A, B);
                Function = Expression.Lambda<Func<T, T, bool>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region NotEqual

        /// <summary>Checks two numeric values for inequality [a != b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the inequality check.</param>
        /// <param name="b">The second operand of the inequality check.</param>
        /// <returns>The result of the inequality check.</returns>
        public static bool NotEqual<T>(T a, T b)
        {
            return NotEqualImplementation<T>.Function(a, b);
        }

        internal static class NotEqualImplementation<T>
        {
            internal static Func<T, T, bool> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.NotEqual(A, B);
                Function = Expression.Lambda<Func<T, T, bool>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region LessThan

        /// <summary>Checks that a numeric value is less than another.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the less than check.</param>
        /// <param name="b">The second operand of the less than check.</param>
        /// <returns>The result of the less than check.</returns>
        public static bool LessThan<T>(T a, T b)
        {
            return LessThanImplementation<T>.Function(a, b);
        }

        internal static class LessThanImplementation<T>
        {
            internal static Func<T, T, bool> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.LessThan(A, B);
                Function = Expression.Lambda<Func<T, T, bool>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region GreaterThan

        /// <summary>Checks that a numeric value is greater than another [a > b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the greater than check.</param>
        /// <param name="b">The second operand of the greater than check.</param>
        /// <returns>The result of the greater than check.</returns>
        public static bool GreaterThan<T>(T a, T b)
        {
            return GreaterThanImplementation<T>.Function(a, b);
        }

        internal static class GreaterThanImplementation<T>
        {
            internal static Func<T, T, bool> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.GreaterThan(A, B);
                Function = Expression.Lambda<Func<T, T, bool>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region LessThanOrEqual

        /// <summary>Checks that a numeric value is less than or equal to another.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the less than or equal to check.</param>
        /// <param name="b">The second operand of the less than or equal to check.</param>
        /// <returns>The result of the less than or equal to check.</returns>
        public static bool LessThanOrEqual<T>(T a, T b)
        {
            return LessThanOrEqualImplementation<T>.Function(a, b);
        }

        internal static class LessThanOrEqualImplementation<T>
        {
            internal static Func<T, T, bool> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.LessThanOrEqual(A, B);
                Function = Expression.Lambda<Func<T, T, bool>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region GreaterThanOrEqual

        /// <summary>Checks that a numeric value is less greater or equal to another [a >= b].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the greater than or equal to check.</param>
        /// <param name="b">The second operand of the greater than or equal to check.</param>
        /// <returns>The result of the greater than or equal to check.</returns>
        public static bool GreaterThanOrEqual<T>(T a, T b)
        {
            return GreaterThanOrEqualImplementation<T>.Function(a, b);
        }

        internal static class GreaterThanOrEqualImplementation<T>
        {
            internal static Func<T, T, bool> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.GreaterThanOrEqual(A, B);
                Function = Expression.Lambda<Func<T, T, bool>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region GreatestCommonFactor

        /// <summary>Computes the greatest common factor of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="a">The first operand of the greatest common factor computation.</param>
        /// <param name="b">The second operand of the greatest common factor computation.</param>
        /// <param name="c">The remaining operands of the greatest common factor computation.</param>
        /// <returns>The computed greatest common factor of the set of numbers.</returns>
        public static T GreatestCommonFactor<T>(T a, T b, params T[] c)
        {
            return GreatestCommonFactor<T>((Step<T> step) => { step(a); step(b); c.ToStepper()(step); });
        }

        /// <summary>Computes the greatest common factor of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="stepper">The set of numbers to compute the greatest common factor of.</param>
        /// <returns>The computed greatest common factor of the set of numbers.</returns>
        public static T GreatestCommonFactor<T>(Stepper<T> stepper)
        {
            if (stepper == null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            bool assigned = false;
            T answer = Constant<T>.Zero;
            stepper((T n) =>
            {
                if (n == null)
                {
                    throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " contains null value(s).");
                }
                else if (Equal(n, Constant<T>.Zero))
                {
                    throw new MathematicsException("Encountered Zero (0) while computing the " + nameof(GreatestCommonFactor));
                }
                else if (!IsInteger(n))
                {
                    throw new MathematicsException(nameof(stepper) + " contains non-integer value(s).");
                }
                if (!assigned)
                {
                    answer = AbsoluteValue(n);
                    assigned = true;
                }
                else
                {
                    if (GreaterThan(answer, Constant<T>.One))
                    {
                        T a = answer;
                        T b = n;
                        while (NotEqual(b, Constant<T>.Zero))
                        {
                            T remainder = Modulo(a, b);
                            a = b;
                            b = remainder;
                        }
                        answer = AbsoluteValue(a);
                    }
                }
            });
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return answer;
        }

        #endregion

        #region LeastCommonMultiple

        /// <summary>Computes the least common multiple of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first operand of the least common muiltiple computation.</param>
        /// <param name="b">The second operand of the least common muiltiple computation.</param>
        /// <param name="c">The remaining operands of the least common muiltiple computation.</param>
        /// <returns>The computed least common least common multiple of the set of numbers.</returns>
        public static T LeastCommonMultiple<T>(T a, T b, params T[] c)
        {
            return LeastCommonMultiple((Step<T> step) => { step(a); step(b); c.ToStepper()(step); });
        }

        /// <summary>Computes the least common multiple of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The set of numbers to compute the least common multiple of.</param>
        /// <returns>The computed least common least common multiple of the set of numbers.</returns>
        public static T LeastCommonMultiple<T>(Stepper<T> stepper)
        {
            if (stepper == null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            bool assigned = false;
            T answer = default(T);
            stepper(parameter =>
            {
                if (Equal(parameter, Constant<T>.Zero))
                {
                    throw new MathematicsException(nameof(stepper) + " contains 0 value(s).");
                }
                if (!IsInteger(parameter))
                {
                    throw new MathematicsException(nameof(stepper) + " contains non-integer value(s).");
                }
                parameter = AbsoluteValue(parameter);
                if (!assigned)
                {
                    answer = parameter;
                    assigned = true;
                }
                else
                {
                    answer = Divide(Multiply(answer, parameter), GreatestCommonFactor(answer, parameter));
                }
            });
            if (!assigned)
            {
                throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
            }
            return answer;
        }

        #endregion

        #region LinearInterpolation

        public static T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1)
        {
            if (GreaterThan(x0, x1) ||
                GreaterThan(x, x1) ||
                LessThan(x, x0))
            {
                throw new MathematicsException("Arguments out of range !(" + nameof(x0) + " <= " + nameof(x) + " <= " + nameof(x1) + ") [" + x0 + " <= " + x + " <= " + x1 + "].");
            }
            if (Equal(x0, x1))
            {
                if (NotEqual(y0, y1))
                {
                    throw new MathematicsException("Arguments out of range (" + nameof(x0) + " == " + nameof(x1) + ") but !(" + nameof(y0) + " != " + nameof(y1) + ") [" + y0 + " != " + y1 + "].");
                }
                else
                {
                    return y0;
                }
            }
            return Add(y0, Divide(Multiply(Subtract(x, x0), Subtract(y1, y0)), Subtract(x1, x0)));
        }

        #endregion

        #region Factorial

        /// <summary>Computes the factorial of a numeric value [a!] == [a * (a - 1) * (a - 2) * ... * 1].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The integer factorial to computer the value of.</param>
        /// <returns>The computed factorial value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the parameter is not an integer value.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the parameter is less than zero.</exception>
        public static T Factorial<T>(T a)
        {
            return FactorialImplementation<T>.Function(a);
        }

        internal static class FactorialImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                Function = A =>
                {
                    if (!IsInteger(A))
                    {
                        throw new ArgumentOutOfRangeException(nameof(A), A, "!" + nameof(A) + "." + nameof(IsInteger));
                    }
                    if (LessThan(A, Constant<T>.Zero))
                    {
                        throw new ArgumentOutOfRangeException(nameof(A), A, "!(" + nameof(A) + " >= 0)");
                    }
                    T result = Constant<T>.One;
                    for (; GreaterThan(A, Constant<T>.One); A = Subtract(A, Constant<T>.One))
                        result = Multiply(A, result);
                    return result;
                };
                return Function(a);
            };
        }

        #endregion

        #region Combinations

        public static T Combinations<T>(T N, T[] n)
        {
            if (!IsInteger(N))
            {
                throw new ArgumentOutOfRangeException(nameof(N), N, "!(" + nameof(N) + "." + nameof(IsInteger) + ")");
            }
            T result = Factorial(N);
            T sum = Constant<T>.Zero;
            for (int i = 0; i < n.Length; i++)
            {
                if (!IsInteger(n[i]))
                {
                    throw new ArgumentOutOfRangeException(nameof(n) + "[" + i + "]", n[i], "!(" + nameof(n) + "[" + i + "]." + nameof(IsInteger) + ")");
                }
                result = Divide(result, Factorial(n[i]));
                sum = Add(sum, n[i]);
            }
            if (GreaterThan(sum, N))
            {
                throw new MathematicsException("Aurguments out of range !(" + nameof(N) + " < Add(" + nameof(n) + ") [" + N + " < " + sum + "].");
            }
            return result;
        }

        #endregion

        #region BinomialCoefficient

        /// <summary>Computes the Binomial coefficient (N choose n).</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="N">The size of the entire set (N choose n).</param>
        /// <param name="n">The size of the subset (N choose n).</param>
        /// <returns>The computed binomial coefficient (N choose n).</returns>
        public static T BinomialCoefficient<T>(T N, T n)
        {
            if (LessThan(N, Constant<T>.Zero))
            {
                throw new ArgumentOutOfRangeException(nameof(N), N, "!(" + nameof(N) + " >= 0)");
            }
            if (!IsInteger(N))
            {
                throw new ArgumentOutOfRangeException(nameof(N), N, "!(" + nameof(N) + "." + nameof(IsInteger) + ")");
            }
            if (!IsInteger(n))
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "!(" + nameof(n) + "." + nameof(IsInteger) + ")");
            }
            if (LessThan(N, n))
            {
                throw new MathematicsException("Arguments out of range !(" + nameof(N) + " <= " + nameof(n) + ") [" + N + " <= " + n + "].");
            }
            return Divide(Factorial(N), Multiply(Factorial(n), Factorial(Subtract(N, n))));
        }

        #endregion

        #region Mode

        public static IHeap<Link<T, int>> Mode<T>(T a, params T[] b)
        {
            return Mode((Step<T> step) => { step(a); b.ToStepper()(step); });
        }

        public static IHeap<Link<T, int>> Mode<T>(Stepper<T> stepper)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Mean

        /// <summary>Computes the mean of a set of numerical values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The first value of the set of data to compute the mean of.</param>
        /// <param name="b">The remaining values in the data set to compute the mean of.</param>
        /// <returns>The computed mean of the set of data.</returns>
        public static T Mean<T>(T a, params T[] b)
        {
            return Mean((Step<T> step) => { step(a); b.ToStepper()(step); });
        }

        /// <summary>Computes the mean of a set of numerical values.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The set of data to compute the mean of.</param>
        /// <returns>The computed mean of the set of data.</returns>
        public static T Mean<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            T i = Constant<T>.Zero;
            T sum = Constant<T>.Zero;
            stepper(step =>
            {
                i = Add(i, Constant<T>.One);
                sum = Add(sum, step);
            });
            if (Equal(i, Constant<T>.Zero))
            {
                throw new ArgumentException("The argument is empty.", nameof(stepper));
            }
            return Divide(sum, i);
        }

        #endregion

        #region Median

        /// <summary>Computes the median of a set of data.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="compare">The comparison algorithm to sort the data by.</param>
        /// <param name="values">The set of data to compute the median of.</param>
        /// <returns>The computed median value of the set of data.</returns>
        public static T Median<T>(Compare<T> compare, params T[] values)
        {
            // standard algorithm (sort and grab middle value)
            Sort.Merge(compare, values);
            if (values.Length % 2 == 1) // odd... just grab middle value
            {
                return values[values.Length / 2];
            }
            else // even... must perform a mean of the middle two values
            {
                T leftMiddle = values[(values.Length / 2) - 1];
                T rightMiddle = values[values.Length / 2];
                return Divide(Add(leftMiddle, rightMiddle), Constant<T>.Two);
            }
        }

        /// <summary>Computes the median of a set of data.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="compare">The comparison algorithm to sort the data by.</param>
        /// <param name="stepper">The set of data to compute the median of.</param>
        /// <returns>The computed median value of the set of data.</returns>
        public static T Median<T>(Compare<T> compare, Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            return Median<T>(compare, stepper.ToArray());
        }

        /// <summary>Computes the median of a set of data.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="values">The set of data to compute the median of.</param>
        /// <returns>The computed median value of the set of data.</returns>
        public static T Median<T>(params T[] values)
        {
            return Median(Towel.Compare.Default, values);
        }

        /// <summary>Computes the median of a set of data.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="stepper">The set of data to compute the median of.</param>
        /// <returns>The computed median value of the set of data.</returns>
        public static T Median<T>(Stepper<T> stepper)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            return Median(Towel.Compare.Default, stepper.ToArray());
        }

        #region Possible Optimization (Still in Development)

        //public static T Median<T>(Compare<T> compare, Hash<T> hash, Equate<T> equate, params T[] values)
        //{
        //    // this is an optimized median algorithm, but it only works on odd sets without duplicates
        //    if (hash != null && equate != null && values.Length % 2 == 1 && !values.ToStepper().ContainsDuplicates(equate, hash))
        //    {
        //        int medianIndex = 0;
        //        OddNoDupesMedianImplementation(values, values.Length, ref medianIndex, compare);
        //        return values[medianIndex];
        //    }
        //    else
        //    {
        //        return Median(compare, values);
        //    }
        //}

        //public static T Median<T>(Compare<T> compare, Hash<T> hash, Equate<T> equate, Stepper<T> stepper)
        //{
        //    return Median(compare, hash, equate, stepper.ToArray());
        //}

        ///// <summary>Fast algorithm for median computation, but only works on data with an odd number of values without duplicates.</summary>
        //private static void OddNoDupesMedianImplementation<T>(T[] a, int n, ref int k, Compare<T> compare)
        //{
        //    int L = 0;
        //    int R = n - 1;
        //    k = n / 2;
        //    int i; int j;
        //    while (L < R)
        //    {
        //        T x = a[k];
        //        i = L; j = R;
        //        OddNoDupesMedianImplementation_Split(a, n, x, ref i, ref j, compare);
        //        if (j <= k) L = i;
        //        if (i >= k) R = j;
        //    }
        //}

        //private static void OddNoDupesMedianImplementation_Split<T>(T[] a, int n, T x, ref int i, ref int j, Compare<T> compare)
        //{
        //    do
        //    {
        //        while (compare(a[i], x) == Comparison.Less) i++;
        //        while (compare(a[j], x) == Comparison.Greater) j--;
        //        T t = a[i];
        //        a[i] = a[j];
        //        a[j] = t;
        //    } while (i < j);
        //}

        #endregion

        #endregion

        #region GeometricMean

        /// <summary>Computes the geometric mean of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="stepper">The set of numbres to compute the geometric mean of.</param>
        /// <returns>The computed geometric mean of the set of numbers.</returns>
        public static T GeometricMean<T>(Stepper<T> stepper)
        {
            T multiple = Constant<T>.One;
            T count = Constant<T>.Zero;
            stepper(i =>
            {
                count = Add(count, Constant<T>.One);
                multiple = Multiply(multiple, i);
            });
            return Root(multiple, count);
        }

        #endregion

        #region Variance

        /// <summary>Computes the variance of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="stepper">The set of numbers to compute the variance of.</param>
        /// <returns>The computed variance of the set of numbers.</returns>
        public static T Variance<T>(Stepper<T> stepper)
        {
            T mean = Mean(stepper);
            T variance = Constant<T>.Zero;
            T count = Constant<T>.Zero;
            stepper(i =>
            {
                T i_minus_mean = Subtract(i, mean);
                variance = Add(variance, Multiply(i_minus_mean, i_minus_mean));
                count = Add(count, Constant<T>.One);
            });
            return Divide(variance, count);
        }

        #endregion

        #region StandardDeviation

        /// <summary>Computes the standard deviation of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="stepper">The set of numbers to compute the standard deviation of.</param>
        /// <returns>The computed standard deviation of the set of numbers.</returns>
        public static T StandardDeviation<T>(Stepper<T> stepper)
        {
            return SquareRoot(Variance(stepper));
        }

        #endregion

        #region MeanDeviation

        /// <summary>The mean deviation of a set of numbers.</summary>
        /// <typeparam name="T">The numeric type of the computation.</typeparam>
        /// <param name="stepper">The set of numbers to compute the mean deviation of.</param>
        /// <returns>The computed mean deviation of the set of numbers.</returns>
        public static T MeanDeviation<T>(Stepper<T> stepper)
        {
            T mean = Mean(stepper);
            T temp = Constant<T>.Zero;
            T count = Constant<T>.Zero;
            stepper(i =>
            {
                temp = Add(temp, AbsoluteValue(Subtract(i, mean)));
                count = Add(count, Constant<T>.One);
            });
            return Divide(temp, count);
        }

        #endregion

        #region Range

        /// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// /// <param name="stepper">The set of data to get the range of.</param>
        /// <param name="minimum">The minimum of the set of data.</param>
        /// <param name="maximum">The maximum of the set of data.</param>
        /// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
        /// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
        public static void Range<T>(out T minimum, out T maximum, Stepper<T> stepper)
        {
            Range(stepper, out minimum, out maximum);
        }

        /// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// /// <param name="stepper">The set of data to get the range of.</param>
        /// <param name="minimum">The minimum of the set of data.</param>
        /// <param name="maximum">The maximum of the set of data.</param>
        /// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
        /// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
        public static void Range<T>(Stepper<T> stepper, out T minimum, out T maximum)
        {
            if (stepper is null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            T MINIMUM = default(T);
            T MAXIMUM = default(T);
            bool assigned = false;
            Step<T> step = i =>
            {
                if (assigned)
                {
                    MINIMUM = LessThan(i, MINIMUM) ? i : MINIMUM;
                    MAXIMUM = LessThan(MAXIMUM, i) ? i : MAXIMUM;
                }
                else
                {
                    MINIMUM = i;
                    MAXIMUM = i;
                    assigned = true;
                }
            };
            stepper(step);
            if (!assigned)
            {
                throw new ArgumentException("The argument is empty.", nameof(stepper));
            }
            minimum = MINIMUM;
            maximum = MAXIMUM;
        }

        #endregion

        #region Quantiles

        public static T[] Quantiles<T>(int quantiles, Stepper<T> stepper)
        {
            if (quantiles < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantiles), quantiles, "!(" + nameof(quantiles) + " >= 1)");
            }
            int count = stepper.Count();
            T[] ordered = new T[count];
            int a = 0;
            stepper(i => { ordered[a++] = i; });
            Sort.Quick(Compare, ordered);
            T[] resultingQuantiles = new T[quantiles + 1];
            resultingQuantiles[0] = ordered[0];
            resultingQuantiles[resultingQuantiles.Length - 1] = ordered[ordered.Length - 1];
            T QUANTILES_PLUS_1 = Convert<int, T>(quantiles + 1);
            T ORDERED_LENGTH = Convert<int, T>(ordered.Length);
            for (int i = 1; i < quantiles; i++)
            {
                T I = Convert<int, T>(i);
                T temp = Divide(ORDERED_LENGTH, Multiply<T>(QUANTILES_PLUS_1, I));
                if (IsInteger(temp))
                {
                    resultingQuantiles[i] = ordered[Convert<T, int>(temp)];
                }
                else
                {
                    resultingQuantiles[i] = Divide(Add(ordered[Convert<T, int>(temp)], ordered[Convert<T, int>(temp) + 1]), Constant<T>.Two);
                }
            }
            return resultingQuantiles;
        }

        #endregion

        #region Correlation

        //        /// <summary>Computes the median of a set of values.</summary>
        //        private static Compute.Delegates.Correlation Correlation_private = (Stepper<T> a, Stepper<T> b) =>
        //        {
        //            throw new System.NotImplementedException("I introduced an error here when I removed the stepref off of structure. will fix soon");

        //            Compute.Correlation_private =
        //        Meta.Compile<Compute.Delegates.Correlation>(
        //        string.Concat(
        //        @"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> _a, Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _b) =>
        //{
        //	", Meta.ConvertTypeToCsharpSource(typeof(T)), " a_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_a);
        //	", Meta.ConvertTypeToCsharpSource(typeof(T)), " b_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_b);
        //	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> a_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
        //	_a((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { a_temp.Add(i - b_mean); });
        //	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> b_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
        //	_b((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { b_temp.Add(i - a_mean); });
        //	", Meta.ConvertTypeToCsharpSource(typeof(T)), "[] a_cross_b = new ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[a_temp.Count * b_temp.Count];
        //	int count = 0;
        //	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_a) =>
        //	{
        //		b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_b) =>
        //		{
        //			a_cross_b[count++] = i_a * i_b;
        //		});
        //	});
        //	a_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
        //	b_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
        //	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_cross_b = 0;
        //	foreach (", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i in a_cross_b)
        //		sum_a_cross_b += i;
        //	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_temp = 0;
        //	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_a_temp += i; });
        //	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_b_temp = 0;
        //	b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_b_temp += i; });
        //	return sum_a_cross_b / Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.sqrt(sum_a_temp * sum_b_temp);
        //}"));

        //            return Compute.Correlation_private(a, b);
        //        };

        //        public static T Correlation(Stepper<T> a, Stepper<T> b)
        //        {
        //            return Correlation_private(a, b);
        //        }
        #endregion

        #region Exponential

        /// <summary>Computes: [ e ^ a ].</summary>
        public static T Exponential<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region NaturalLogarithm

        public static T NaturalLogarithm<T>(T a)
        {
            return NaturalLogarithmImplementation<T>.Function(a);
        }

        /// <summary>Computes (natrual log): [ ln(n) ].</summary>
        internal static class NaturalLogarithmImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                // optimization for specific known types
                if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod("Log"), A);
                    Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region Trigonometry Functions

        #region Sine

        /// <summary>Computes the sine ratio of an angle using the relative talor series. Accurate but slow.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="angle">The angle to compute the sine ratio of.</param>
        /// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
        /// <returns>The taylor series computed sine ratio of the provided angle.</returns>
        public static T SineTaylorSeries<T>(Angle<T> angle, Predicate<T> predicate = null)
        {
            // Series: sine(x) = x - (x^3 / 3!) + (x^5 / 5!) - (x^7 / 7!) + (x^9 / 9!) + ...
            // more terms in computation inproves accuracy

            // Note: there is room for optimization (custom runtime compilation)

            T x = angle[Angle.Units.Radians];
            T sine = x;
            T previous;
            bool isAddTerm = false;
            T i = Constant<T>.Three;
            T xSquared = Multiply(x, x);
            T xRunningPower = x;
            T xRunningFactorial = Constant<T>.One;
            do
            {
                xRunningPower = Multiply(xRunningPower, xSquared);
                xRunningFactorial = Multiply(xRunningFactorial, Multiply(i, Subtract(i, Constant<T>.One)));
                previous = sine;
                if (isAddTerm)
                {
                    sine = Add(sine, Divide(xRunningPower, xRunningFactorial));
                }
                else
                {
                    sine = Subtract(sine, Divide(xRunningPower, xRunningFactorial));
                }
                isAddTerm = !isAddTerm;
                i = Add(i, Constant<T>.Two);
            } while (NotEqual(sine, previous) && (predicate is null || !predicate(sine)));
            return sine;
        }

        /// <summary>Computes the sine ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the sine ratio of.</param>
        /// <returns>The sine ratio of the provided angle.</returns>
        /// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
        public static T SineSystem<T>(Angle<T> a)
        {
            return Convert<double, T>(Math.Sin(Convert<T, double>(a[Angle.Units.Radians])));
        }

        /// <summary>Estimates the sine ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the quadratic estimated sine ratio of.</param>
        /// <returns>The quadratic estimation of the sine ratio of the provided angle.</returns>
        public static T SineQuadratic<T>(Angle<T> a)
        {
            // Piecewise Functions:
            // y = (-4/π^2)(x - (π/2))^2 + 1
            // y = (4/π^2)(x - (3π/2))^2 - 1

            T adjusted = Modulo(a[Angle.Units.Radians], Constant<T>.Pi2);
            if (IsNegative(adjusted))
            {
                adjusted = Add(adjusted, Constant<T>.Pi2);
            }
            if (LessThan(adjusted, Constant<T>.Pi))
            {
                T xMinusPiOver2 = Subtract(adjusted, Constant<T>.PiOver2);
                T xMinusPiOver2Squared = Multiply(xMinusPiOver2, xMinusPiOver2);
                return Add(Multiply(Constant<T>.Negative4OverPiSquared, xMinusPiOver2Squared), Constant<T>.One);
            }
            else
            {
                T xMinus3PiOver2 = Subtract(adjusted, Constant<T>.Pi3Over2);
                T xMinus3PiOver2Squared = Multiply(xMinus3PiOver2, xMinus3PiOver2);
                return Subtract(Multiply(Constant<T>.FourOverPiSquared, xMinus3PiOver2Squared), Constant<T>.One);
            }
        }

        #endregion

        #region Cosine

        /// <summary>Computes the cosine ratio of an angle using the relative talor series. Accurate but slow.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="angle">The angle to compute the cosine ratio of.</param>
        /// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
        /// <returns>The taylor series computed cosine ratio of the provided angle.</returns>
        public static T CosineTaylorSeries<T>(Angle<T> angle, Predicate<T> predicate = null)
        {
            // Series: cosine(x) = 1 - (x^2 / 2!) + (x^4 / 4!) - (x^6 / 6!) + (x^8 / 8!) - ...
            // more terms in computation inproves accuracy

            // Note: there is room for optimization (custom runtime compilation)

            T x = angle[Angle.Units.Radians];
            T cosine = Constant<T>.One;
            T previous;
            T xSquared = Multiply(x, x);
            T xRunningPower = Constant<T>.One;
            T xRunningFactorial = Constant<T>.One;
            bool isAddTerm = false;
            T i = Constant<T>.Two;
            do
            {
                xRunningPower = Multiply(xRunningPower, xSquared);
                xRunningFactorial = Multiply(xRunningFactorial, Multiply(i, Subtract(i, Constant<T>.One)));
                previous = cosine;
                if (isAddTerm)
                {
                    cosine = Add(cosine, Divide(xRunningPower, xRunningFactorial));
                }
                else
                {
                    cosine = Subtract(cosine, Divide(xRunningPower, xRunningFactorial));
                }
                isAddTerm = !isAddTerm;
                i = Add(i, Constant<T>.Two);
            } while (NotEqual(cosine, previous) && (predicate is null || !predicate(cosine)));
            return cosine;
        }

        /// <summary>Computes the cosine ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the cosine ratio of.</param>
        /// <returns>The cosine ratio of the provided angle.</returns>
        /// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
        public static T CosineSystem<T>(Angle<T> a)
        {
            return Convert<double, T>(Math.Cos(Convert<T, double>(a[Angle.Units.Radians])));
        }

        /// <summary>Estimates the cosine ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the quadratic estimated cosine ratio of.</param>
        /// <returns>The quadratic estimation of the cosine ratio of the provided angle.</returns>
        public static T CosineQuadratic<T>(Angle<T> a)
        {
            Angle<T> piOver2Radians = new Angle<T>(Constant<T>.PiOver2, Angle.Units.Radians);
            return SineQuadratic(a - piOver2Radians);
        }

        #endregion

        #region Tangent

        /// <summary>Computes the tangent ratio of an angle using the relative talor series. Accurate but slow.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="angle">The angle to compute the tangent ratio of.</param>
        /// <returns>The taylor series computed tangent ratio of the provided angle.</returns>
        public static T TangentTaylorSeries<T>(Angle<T> a)
        {
            return Divide(SineTaylorSeries(a), CosineTaylorSeries(a));
        }

        /// <summary>Computes the tangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the tangent ratio of.</param>
        /// <returns>The tangent ratio of the provided angle.</returns>
        /// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
        public static T TangentSystem<T>(Angle<T> a)
        {
            return Convert<double, T>(Math.Tan(Convert<T, double>(a[Angle.Units.Radians])));
        }

        /// <summary>Estimates the tangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the quadratic estimated tangent ratio of.</param>
        /// <returns>The quadratic estimation of the tangent ratio of the provided angle.</returns>
        public static T TangentQuadratic<T>(Angle<T> a)
        {
            return Divide(SineQuadratic(a), CosineQuadratic(a));
        }

        #endregion

        #region Cosecant

        /// <summary>Computes the cosecant ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the cosecant ratio of.</param>
        /// <returns>The cosecant ratio of the provided angle.</returns>
        /// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
        public static T CosecantSystem<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, SineSystem(a));
        }

        /// <summary>Estimates the cosecant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the quadratic estimated cosecant ratio of.</param>
        /// <returns>The quadratic estimation of the cosecant ratio of the provided angle.</returns>
        public static T CosecantQuadratic<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, SineQuadratic(a));
        }

        #endregion

        #region Secant

        /// <summary>Computes the secant ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the secant ratio of.</param>
        /// <returns>The secant ratio of the provided angle.</returns>
        /// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
        public static T SecantSystem<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, CosineSystem(a));
        }

        /// <summary>Estimates the secant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the quadratic estimated secant ratio of.</param>
        /// <returns>The quadratic estimation of the secant ratio of the provided angle.</returns>
        public static T SecantQuadratic<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, CosineQuadratic(a));
        }

        #endregion

        #region Cotangent

        /// <summary>Computes the cotangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the cotangent ratio of.</param>
        /// <returns>The cotangent ratio of the provided angle.</returns>
        /// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
        public static T CotangentSystem<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, TangentSystem(a));
        }

        /// <summary>Estimates the cotangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The angle to compute the quadratic estimated cotangent ratio of.</param>
        /// <returns>The quadratic estimation of the cotangent ratio of the provided angle.</returns>
        public static T CotangentQuadratic<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, TangentQuadratic(a));
        }

        #endregion

        #region InverseSine

        //public static Angle<T> InverseSine<T>(T a)
        //{
        //    return InverseSineImplementation<T>.Function(a);
        //}

        //internal static class InverseSineImplementation<T>
        //{
        //    internal static Func<T, Angle<T>> Function = (T a) =>
        //    {
        //        // optimization for specific known types
        //        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
        //        {
        //            ParameterExpression A = Expression.Parameter(typeof(T));
        //            Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Asin)), A));
        //            Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
        //            return Function(a);
        //        }
        //        throw new NotImplementedException();
        //    };
        //}

        #endregion

        #region InverseCosine

        //public static Angle<T> InverseCosine<T>(T a)
        //{
        //    return InverseCosineImplementation<T>.Function(a);
        //}

        //internal static class InverseCosineImplementation<T>
        //{
        //    internal static Func<T, Angle<T>> Function = (T a) =>
        //    {
        //        // optimization for specific known types
        //        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
        //        {
        //            ParameterExpression A = Expression.Parameter(typeof(T));
        //            Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Acos)), A));
        //            Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
        //            return Function(a);
        //        }
        //        throw new NotImplementedException();
        //    };
        //}

        #endregion

        #region InverseTangent

        //public static Angle<T> InverseTangent<T>(T a)
        //{
        //    return InverseTangentImplementation<T>.Function(a);
        //}

        //internal static class InverseTangentImplementation<T>
        //{
        //    internal static Func<T, Angle<T>> Function = (T a) =>
        //    {
        //        // optimization for specific known types
        //        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
        //        {
        //            ParameterExpression A = Expression.Parameter(typeof(T));
        //            Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Atan)), A));
        //            Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
        //            return Function(a);
        //        }
        //        throw new NotImplementedException();
        //    };
        //}

        #endregion

        #region InverseCosecant

        //public static Angle<T> InverseCosecant<T>(T a)
        //{
        //    return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseSine(a).Radians));
        //}

        #endregion

        #region InverseSecant

        //public static Angle<T> InverseSecant<T>(T a)
        //{
        //    return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseCosine(a).Radians));
        //}

        #endregion

        #region InverseCotangent

        //public static Angle<T> InverseCotangent<T>(T a)
        //{
        //    return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseTangent(a).Radians));
        //}

        #endregion

        #region HyperbolicSine

        //public static T HyperbolicSine<T>(Angle<T> a)
        //{
        //    return HyperbolicSineImplementation<T>.Function(a);
        //}

        //internal static class HyperbolicSineImplementation<T>
        //{
        //    internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
        //    {
        //        // optimization for specific known types
        //        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
        //        {
        //            ParameterExpression A = Expression.Parameter(typeof(T));
        //            Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Sinh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
        //            Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
        //            return Function(a);
        //        }
        //        throw new NotImplementedException();
        //    };
        //}

        #endregion

        #region HyperbolicCosine

        //public static T HyperbolicCosine<T>(Angle<T> a)
        //{
        //    return HyperbolicCosineImplementation<T>.Function(a);
        //}

        //internal static class HyperbolicCosineImplementation<T>
        //{
        //    internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
        //    {
        //        // optimization for specific known types
        //        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
        //        {
        //            ParameterExpression A = Expression.Parameter(typeof(T));
        //            Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Cosh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
        //            Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
        //            return Function(a);
        //        }
        //        throw new NotImplementedException();
        //    };
        //}

        #endregion

        #region HyperbolicTangent

        //public static T HyperbolicTangent<T>(Angle<T> a)
        //{
        //    return HyperbolicTangentImplementation<T>.Function(a);
        //}

        //internal static class HyperbolicTangentImplementation<T>
        //{
        //    internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
        //    {
        //        // optimization for specific known types
        //        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
        //        {
        //            ParameterExpression A = Expression.Parameter(typeof(T));
        //            Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Tanh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
        //            Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
        //            return Function(a);
        //        }
        //        throw new NotImplementedException();
        //    };
        //}

        #endregion

        #region HyperbolicCosecant

        //public static T HyperbolicCosecant<T>(Angle<T> a)
        //{
        //    return Divide(Constant<T>.One, HyperbolicSine(a));
        //}

        #endregion

        #region HyperbolicSecant

        //public static T HyperbolicSecant<T>(Angle<T> a)
        //{
        //    return Divide(Constant<T>.One, HyperbolicCosine(a));
        //}

        #endregion

        #region HyperbolicCotangent

        //public static T HyperbolicCotangent<T>(Angle<T> a)
        //{
        //    return Divide(Constant<T>.One, HyperbolicTangent(a));
        //}

        #endregion

        #region InverseHyperbolicSine

        public static Angle<T> InverseHyperbolicSine<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region InverseHyperbolicCosine

        public static Angle<T> InverseHyperbolicCosine<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region InverseHyperbolicTangent

        public static Angle<T> InverseHyperbolicTangent<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region InverseHyperbolicCosecant

        public static Angle<T> InverseHyperbolicCosecant<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region InverseHyperbolicSecant

        public static Angle<T> InverseHyperbolicSecant<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region InverseHyperbolicCotangent

        public static Angle<T> InverseHyperbolicCotangent<T>(T a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region LinearRegression2D

        /// <summary>Computes the best fit line from a set of points in 2D space [y = slope * x + y_intercept].</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="points">The points to compute the best fit line of.</param>
        /// <param name="slope">The slope of the computed best fit line [y = slope * x + y_intercept].</param>
        /// <param name="y_intercept">The y intercept of the computed best fit line [y = slope * x + y_intercept].</param>
        public static void LinearRegression2D<T>(Stepper<T, T> points, out T slope, out T y_intercept)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }
            int count = 0;
            T SUMX = Constant<T>.Zero;
            T SUMY = Constant<T>.Zero;
            points((x, y) =>
            {
                SUMX = Add(SUMX, x);
                SUMY = Add(SUMY, y);
                count++;
            });
            if (count < 2)
            {
                throw new MathematicsException("Argument Invalid !(" + nameof(points) + ".Count >= 2)");
            }
            T COUNT = Convert<int, T>(count);
            T MEANX = Divide(SUMX, COUNT);
            T MEANY = Divide(SUMY, COUNT);
            T VARIANCEX = Constant<T>.Zero;
            T VARIANCEY = Constant<T>.Zero;
            points((x, y) =>
            {
                T offset = Subtract(x, MEANX);
                VARIANCEY = Add(VARIANCEY, Multiply(offset, Subtract(y, MEANY)));
                VARIANCEX = Add(VARIANCEX, Multiply(offset, offset));
            });
            slope = Divide(VARIANCEY, VARIANCEX);
            y_intercept = Subtract(MEANY, Multiply(slope, MEANX));
        }

        #endregion

        #region FactorPrimes

        /// <summary>Factors the primes numbers of a numeric integer value.</summary>
        /// <typeparam name="T">The numeric type of the operation.</typeparam>
        /// <param name="a">The value to factor the prime numbers of.</param>
        /// <returns>A stepper of all the prime factors.</returns>
        public static Stepper<T> FactorPrimes<T>(T a)
        {
            return FactorPrimesImplementation<T>.Function(a);
        }

        internal static class FactorPrimesImplementation<T>
        {
            internal static Func<T, Stepper<T>> Function = (T a) =>
            {
                Function = A =>
                {
                    if (!IsInteger(A))
                    {
                        throw new ArgumentOutOfRangeException(nameof(A), A, "!(" + nameof(A) + "." + nameof(IsInteger) + ")");
                    }
                    return (Step<T> step) =>
                    {
                        if (IsNegative(A))
                        {
                            A = AbsoluteValue(A);
                            step(Convert<int, T>(-1));
                        }
                        while (IsEven(A))
                        {
                            step(Constant<T>.Two);
                            A = Divide(A, Constant<T>.Two);
                        }
                        for (T i = Constant<T>.Three; LessThanOrEqual(i, SquareRoot(A)); i = Add(i, Constant<T>.Two))
                        {
                            while (Equal(Modulo(A, i), Constant<T>.Zero))
                            {
                                step(i);
                                A = Divide(A, i);
                            }
                        }
                        if (GreaterThan(A, Constant<T>.Two))
                        {
                            step(A);
                        }
                    };
                };
                return Function(a);
            };
        }

        #endregion
    }
}
