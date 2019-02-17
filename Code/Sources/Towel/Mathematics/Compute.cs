using Towel;
using Towel.Structures;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Towel.Measurements;

namespace Towel.Mathematics
{
	/// <summary>Primary class for generic mathematics in the Towel Framework.</summary>
	/// <typeparam name="T">The generic type to perform mathematics on (expected to be numeric).</typeparam>
	public static class Compute
	{
        #region Fields

        public class Constant<T>
        {
            /// <summary>Zero (0)</summary>
            public static readonly T Zero = Compute.FromInt32<T>(0);
            /// <summary>One (1)</summary>
            public static readonly T One = Compute.FromInt32<T>(1);
            /// <summary>Two (2)</summary>
            public static readonly T Two = Compute.FromInt32<T>(2);
            /// <summary>Two (2)</summary>
            public static readonly T Three = Compute.FromInt32<T>(3);
            /// <summary>Negative One (-1)</summary>
            public static readonly T NegativeOne = Compute.FromInt32<T>(-1);
            /// <summary>Pi (3.14...)</summary>
            public static readonly T Pi = Compute.ComputePi<T>();
            /// <summary>Epsilon (1.192092896...e-012f)</summary>
            public static readonly T Epsilon = Compute.ComputeEpsilon<T>();
        }
        #endregion

        #region Constant Computation

        #region Pi

        private static T ComputePi<T>()
        {
            throw new NotImplementedException();

//            const int pi_maximum_iterations = 100; // NOTE: decimal accuracy for pi requires pi_maximum_iterations = 92

//            string type = Meta.ConvertTypeToCsharpSource(typeof(T));

//            // Series: PI = 2 * (1 + 1/3 * (1 + 2/5 * (1 + 3/7 * (...))))
//            // more terms in computation inproves accuracy
//            var script = CSharpScript.Create<Func<T>>(string.Concat(@"() =>
//{
//	int j = 1, max = 1;
//	// the actual computation
//	Compute<", type, @">.Delegates.ComputePi function = null;
//	function = () =>
//		{
//			if (j > max)
//				return 1;
//			return (", type, @")(1 + (", type, @")j / (", type, @")(2 * (j++) + 1) * function());
//		};
//	// continually compute with higher accuracy
//	", type, @" pi = 1, previous = 0;
//	for (int i = 1; previous != pi && i < ", pi_maximum_iterations, @"; i++)
//	{
//		previous = pi;
//		j = 1;
//		max = i;
//		pi = ((", type, @")2) * function();
//	}
//	return (", type, @")pi;
//}"));
//            _pi = (await script.RunAsync()).ReturnValue();
        }
        #endregion

        #region Epsilon

        private static T ComputeEpsilon<T>()
        {
            if (typeof(T) == typeof(float))
            {
                return (T)(object)1.192092896e-012f;
            }
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion

        #region FromInt32

        public static T FromInt32<T>(int a)
        {
            return FromInt32Implementation<T>.Function(a);
        }
        
        internal static class FromInt32Implementation<T>
        {
            internal static Func<int, T> Function = (int a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(int));
                Expression BODY = Expression.Convert(A, typeof(T));
                Function = Expression.Lambda<Func<int, T>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region ToInt32

        internal static int ToInt32<T>(T a)
        {
            return ToInt32Implementation<T>.Function(a);
        }

        internal static class ToInt32Implementation<T>
        {
            internal static Func<T, int> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.Convert(A, typeof(int));
                Function = Expression.Lambda<Func<T, int>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region Negate

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

        public static T Add<T>(T a, T b)
        {
            return AddImplementation<T>.Function(a, b);
        }

        public static T Add<T>(params T[] operands)
        {
            return Add(operands.Stepper());
        }

        public static T Add<T>(Stepper<T> stepper)
        {
            T result = Constant<T>.Zero;
            stepper(a => result = Add(result, a));
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

        public static T Subtract<T>(T a, T b)
        {
            return SubtractImplementation<T>.Function(a, b);
        }

        public static T Subtract<T>(params T[] operands)
        {
            return Subtract(operands.Stepper());
        }

        public static T Subtract<T>(Stepper<T> stepper)
        {
            T result = Constant<T>.Zero;
            Step<T> step = (a) =>
            {
                result = a;
                step = b => Subtract(result, b);
            };
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

        public static T Multiply<T>(T a, T b)
        {
            return MultiplyImplementation<T>.Function(a, b);
        }

        public static T Multiply<T>(params T[] operands)
        {
            return Multiply(operands.Stepper());
        }

        public static T Multiply<T>(Stepper<T> stepper)
        {
            T result = Constant<T>.Zero;
            stepper(a => result = Multiply(result, a));
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

        public static T Divide<T>(T a, T b)
        {
            return DivideImplementation<T>.Function(a, b);
        }

        public static T Divide<T>(params T[] operands)
        {
            return Divide(operands.Stepper());
        }

        public static T Divide<T>(Stepper<T> stepper)
        {
            T result = Constant<T>.Zero;
            Step<T> step = (a) =>
            {
                result = a;
                step = b => Divide(result, b);
            };
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

		public static T Invert<T>(T a)
        {
            return InvertImplementation<T>.Function(a);
        }

        internal static class InvertImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                throw new NotImplementedException();

                //ParameterExpression A = Expression.Parameter(typeof(T));
                //Expression BODY = ;
                //Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                //return Function(a);
            };
        }

        #endregion

        #region Modulo

        public static T Modulo<T>(T a, T b)
        {
            return ModuloImplementation<T>.Function(a, b);
        }

        public static T Modulo<T>(params T[] operands)
        {
            return Modulo(operands.Stepper());
        }

        public static T Modulo<T>(Stepper<T> operands)
        {
            T result = Constant<T>.Zero;
            Step<T> step = (a) =>
            {
                result = a;
                step = b => Modulo(result, b);
            };
            operands(step);
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

        public static T Power<T>(T a, T b)
        {
            return PowerImplementation<T>.Function(a, b);
        }

        public static T Power<T>(params T[] operands)
        {
            return Power(operands.Stepper());
        }

        public static T Power<T>(Stepper<T> operands)
        {
            T result = Constant<T>.Zero;
            Step<T> step = (a) =>
            {
                result = a;
                step = b => Power(result, b);
            };
            operands(step);
            return result;
        }

        internal static class PowerImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    ParameterExpression B = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Pow)), A, B);
                    Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                    return Function(a, b);
                }
                else
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    ParameterExpression B = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Power(A, B);
                    Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                    return Function(a, b);
                }
            };
        }

        #endregion

        #region SquareRoot

        public static T SquareRoot<T>(T a)
        {
            return SquareRootImplementation<T>.Function(a);
        }
        
        internal static class SquareRootImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Sqrt)), A);
                    Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region Root

        public static T Root<T>(T @base, T root)
		{
            return Power(@base, Invert(root));
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

        public static bool IsNegative<T>(T a)
        {
            return IsNegativeImplementation<T>.Function(a);
        }

        internal static class IsNegativeImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.LessThan(A, Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsPositive

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

        public static bool IsOdd<T>(T a)
        {
            return IsOddImplementation<T>.Function(a);
        }

        internal static class IsOddImplementation<T>
        {
            internal static Func<T, bool> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.NotEqual(Expression.Modulo(A, Expression.Constant(Constant<T>.Two)), Expression.Constant(Constant<T>.Zero));
                Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region IsPrime

        public static bool IsPrime<T>(T value)
        {
            return IsPrimeImplementation<T>.Function(value);
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

        public static T AbsoluteValue<T>(T a)
        {
            return AbsoluteValueImplementation<T>.Function(a);
        }
        
        internal static class AbsoluteValueImplementation<T>
        {
            internal static Func<T, T> Function = (T a) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                Expression BODY = Expression.IfThenElse(
                    Expression.LessThan(A, Expression.Constant(Constant<T>.Zero)),
                    A,
                    Expression.Negate(A));
                Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
                return Function(a);
            };
        }

        #endregion

        #region Maximum

        public static T Maximum<T>(T a, T b)
        {
            return MaximumImplementation<T>.Function(a, b);
        }

        public static T Maximum<T>(params T[] operands)
        {
            return Maximum(operands.Stepper());
        }

        public static T Maximum<T>(Stepper<T> stepper)
        {
            T result = Constant<T>.Zero;
            Step<T> step = (a) =>
            {
                result = a;
                step = b => Maximum(result, a);
            };
            stepper(step);
            return result;
        }

        internal static class MaximumImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.IfThenElse(
                    Expression.GreaterThan(A, B),
                    A,
                    B);
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Minimum

        public static T Minimum<T>(T a, T b)
        {
            return MinimumImplementation<T>.Function(a, b);
        }

        public static T Minimum<T>(params T[] operands)
        {
            return Minimum(operands.Stepper());
        }

        public static T Minimum<T>(Stepper<T> stepper)
        {
            T result = Constant<T>.Zero;
            Step<T> step = (a) =>
            {
                result = a;
                step = b => Minimum(result, a);
            };
            stepper(step);
            return result;
        }

        internal static class MinimumImplementation<T>
        {
            internal static Func<T, T, T> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.IfThenElse(
                    Expression.LessThan(A, B),
                    A,
                    B);
                Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Clamp

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
                Expression BODY = Expression.IfThenElse(
                    Expression.LessThan(VALUE, MINIMUM),
                    MINIMUM,
                    Expression.IfThenElse(
                        Expression.GreaterThan(VALUE, MAXIMUM),
                        MAXIMUM,
                        VALUE));
                Function = Expression.Lambda<Func<T, T, T, T>>(BODY, VALUE, MINIMUM, MAXIMUM).Compile();
                return Function(value, minimum, maximum);
            };
        }

        #endregion

        #region EqualLeniency

        public static bool Equal<T>(T a, T b, T leniency)
        {
            return EqualLeniencyImplementation<T>.Function(a, b, leniency);
        }

        internal static class EqualLeniencyImplementation<T>
        {
            internal static Func<T, T, T, bool> Function = (T a, T b, T c) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                ParameterExpression C = Expression.Parameter(typeof(T));
                Expression BODY = Expression.And(
                    Expression.GreaterThan(A, Expression.Subtract(B, C)),
                    Expression.LessThan(A, Expression.Add(B, C)));
                Function = Expression.Lambda<Func<T, T, T, bool>>(BODY, A, B, C).Compile();
                return Function(a, b, c);
            };
        }

        #endregion

        #region Compare

        public static Comparison Compare<T>(T a, T b)
        {
            return CompareImplementation<T>.Function(a, b);
        }
        
        internal static class CompareImplementation<T>
        {
            internal static Func<T, T, Comparison> Function = (T a, T b) =>
            {
                ParameterExpression A = Expression.Parameter(typeof(T));
                ParameterExpression B = Expression.Parameter(typeof(T));
                Expression BODY = Expression.IfThenElse(
                    Expression.LessThan(A, B),
                    Expression.Constant(Comparison.Less),
                    Expression.IfThenElse(
                        Expression.GreaterThan(A, B),
                        Expression.Constant(Comparison.Greater),
                        Expression.Constant(Comparison.Equal)));
                Function = Expression.Lambda<Func<T, T, Comparison>>(BODY, A, B).Compile();
                return Function(a, b);
            };
        }

        #endregion

        #region Equal

        public static bool Equal<T>(T a, T b)
        {
            return EqualImplementation<T>.Function(a, b);
        }

        public static bool Equal<T>(params T[] operands)
        {
            for (int i = 1; i < operands.Length; i++)
            {
                if (!Equal(operands[i - 1], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Equal<T>(Stepper<T> stepper)
        {
            bool result = true;
            T a;
            Step<T> step = (b) =>
            {
                a = b;
                step = c =>
                {
                    if (!Equal(a, c))
                    {
                        result = false;
                    }
                    a = c;
                };
            };
            stepper(step);
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

        public static bool NotEqual<T>(T a, T b)
        {
            return NotEqualImplementation<T>.Function(a, b);
        }

        public static bool NotEqual<T>(params T[] operands)
        {
            for (int i = 1; i < operands.Length; i++)
            {
                if (!NotEqual(operands[i - 1], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool NotEqual<T>(Stepper<T> stepper)
        {
            bool result = true;
            T a;
            Step<T> step = (b) =>
            {
                a = b;
                step = c =>
                {
                    if (!NotEqual(a, c))
                    {
                        result = false;
                    }
                    a = c;
                };
            };
            stepper(step);
            return result;
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

        public static bool LessThan<T>(T a, T b)
        {
            return LessThanImplementation<T>.Function(a, b);
        }

        public static bool LessThan<T>(params T[] operands)
        {
            for (int i = 1; i < operands.Length; i++)
            {
                if (!LessThan(operands[i - 1], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool LessThan<T>(Stepper<T> stepper)
        {
            bool result = true;
            T a;
            Step<T> step = (b) =>
            {
                a = b;
                step = c =>
                {
                    if (!LessThan(a, c))
                    {
                        result = false;
                    }
                    a = c;
                };
            };
            stepper(step);
            return result;
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

        public static bool GreaterThan<T>(T a, T b)
        {
            return GreaterThanImplementation<T>.Function(a, b);
        }

        public static bool GreaterThan<T>(params T[] operands)
        {
            for (int i = 1; i < operands.Length; i++)
            {
                if (!GreaterThan(operands[i - 1], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool GreaterThan<T>(Stepper<T> stepper)
        {
            bool result = true;
            T a;
            Step<T> step = (b) =>
            {
                a = b;
                step = c =>
                {
                    if (!GreaterThan(a, c))
                    {
                        result = false;
                    }
                    a = c;
                };
            };
            stepper(step);
            return result;
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

        public static bool LessThanOrEqual<T>(T a, T b)
        {
            return LessThanOrEqualImplementation<T>.Function(a, b);
        }

        public static bool LessThanOrEqual<T>(params T[] operands)
        {
            for (int i = 1; i < operands.Length; i++)
            {
                if (!LessThanOrEqual(operands[i - 1], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool LessThanOrEqual<T>(Stepper<T> stepper)
        {
            bool result = true;
            T a;
            Step<T> step = (b) =>
            {
                a = b;
                step = c =>
                {
                    if (!LessThanOrEqual(a, c))
                    {
                        result = false;
                    }
                    a = c;
                };
            };
            stepper(step);
            return result;
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

        public static bool GreaterThanOrEqual<T>(T a, T b)
        {
            return GreaterThanOrEqualImplementation<T>.Function(a, b);
        }

        public static bool GreaterThanOrEqual<T>(params T[] operands)
        {
            for (int i = 1; i < operands.Length; i++)
            {
                if (!GreaterThanOrEqual(operands[i - 1], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool GreaterThanOrEqual<T>(Stepper<T> stepper)
        {
            bool result = true;
            T a;
            Step<T> step = (b) =>
            {
                a = b;
                step = c =>
                {
                    if (!GreaterThanOrEqual(a, c))
                    {
                        result = false;
                    }
                    a = c;
                };
            };
            stepper(step);
            return result;
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

        private static T GreatestCommonFactor<T>(params T[] operands)
        {
            return GreatestCommonFactor(operands.Stepper());
        }

        private static T GreatestCommonFactor<T>(Stepper<T> stepper)
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
                if (!IsInteger(n))
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
                    if (LessThan(answer, Constant<T>.One))
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

        private static T LeastCommonMultiple<T>(params T[] operands)
        {
            return LeastCommonMultiple(operands.Stepper());
        }

        private static T LeastCommonMultiple<T>(Stepper<T> stepper)
        {
            if (stepper == null)
            {
                throw new ArgumentNullException(nameof(stepper));
            }
            bool assigned = false;
            T answer = Constant<T>.Zero;
            stepper((T n) =>
            {
                if (Equal(n, Constant<T>.Zero))
                {
                    answer = Constant<T>.Zero;
                    return;
                }
                if (!IsInteger(n))
                {
                    throw new MathematicsException(nameof(stepper) + " contains non-integer value(s).");
                }
                if (!assigned)
		        {
			        answer = AbsoluteValue(n);
			        assigned = true;
		        }
                if (GreaterThan(answer, Constant<T>.One))
                {
                    answer = AbsoluteValue(Multiply(Divide(answer, GreatestCommonFactor((Step<T> step) => { step(answer); step(n); })), n));
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

        private static T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1)
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
                    throw new MathematicsException("Arguments out of range (" + nameof(x0) + " == " + nameof(x1) +") but !(" + nameof(y0) + " != " + nameof(y1) + ") [" + y0 + " != " + y1 + "].");
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

        private static T Factorial<T>(T a)
        {
            if (!IsInteger(a))
            {
                throw new ArgumentOutOfRangeException(nameof(a), a, "!" + nameof(a) + "." + nameof(IsInteger));
            }
            if (LessThan(a, Constant<T>.Zero))
            {
                throw new ArgumentOutOfRangeException(nameof(a), a, "!(" + nameof(a) + " >= 0)");
            }
            T result = Constant<T>.One;
            for (; GreaterThan(a, Constant<T>.One); a = Subtract(a, Constant<T>.One))
                result = Multiply(a, result);
            return result;
        }

        #endregion

        #region Combinations

        private static T Combinations<T>(T N, T[] n)
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

        #region Choose

        private static T Choose<T>(T N, T n)
        {
            if (LessThan(N, Compute.Constant<T>.Zero))
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
            if (GreaterThan(N, n))
            {
                throw new MathematicsException("Arguments out of range !(" + nameof(N) + " <= " + nameof(n) + ") [" + N + " <= " + n + "].");
            }
            return Divide(Factorial(N), Factorial(Subtract(N, n)));
        }

        #endregion

        #region Mode

        public static Heap<Link<T, int>> Mode<T>(params T[] data)
        {
            return Mode(data.Stepper());
        }

        public static Heap<Link<T, int>> Mode<T>(Stepper<T> stepper)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Mean

        public static T Mean<T>(params T[] data)
        {
            return Mean(data.Stepper());
        }

        public static T Mean<T>(Stepper<T> stepper)
        {
            T i = Constant<T>.Zero;
            T sum = Constant<T>.Zero;
            stepper((T step) => { i = Add(i, Constant<T>.One); sum = Add(sum, step); });
            return Divide(sum, i);
        }

        #endregion

        #region Median

        public static T Median<T>(Compare<T> compare, Hash<T> hash, Equate<T> equate, params T[] values)
        {
            // this is an optimized median algorithm, but it only works on odd sets without duplicates
            if (hash != null && equate != null && values.Length % 2 == 1 && !values.Stepper().ContainsDuplicates(equate, hash))
            {
                int medianIndex = 0;
                OddNoDupesMedianImplementation(values, values.Length, ref medianIndex, compare);
                return values[medianIndex];
            }

            // standard algorithm (sort and grab middle value)
            Algorithms.Sort<T>.Merge(compare, values);
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

        public static T Median<T>(Compare<T> compare, Hash<T> hash, Equate<T> equate, Stepper<T> stepper)
        {
            return Median(compare, hash, equate, stepper.ToArray());
        }

        public static T Median<T>(Compare<T> compare, params T[] values)
        {
            return Median(compare, Hash.Default, Equate.Default, values);
        }

        public static T Median<T>(Compare<T> compare, Stepper<T> stepper)
        {
            return Median<T>(compare, Hash.Default, Equate.Default, stepper.ToArray());
        }

        public static T Median<T>(params T[] values)
        {
            return Median(Towel.Compare.Default, Hash.Default, Equate.Default, values);
        }

        public static T Median<T>(Stepper<T> stepper)
        {
            return Median(Towel.Compare.Default, Hash.Default, Equate.Default, stepper.ToArray());
        }

        /// <summary>Fast algorithm for median computation, but only works on data with an odd number of values without duplicates.</summary>
        private static void OddNoDupesMedianImplementation<T>(T[] a, int n, ref int k, Compare<T> compare)
        {
            int L = 0;
            int R = n - 1;
            k = n / 2;
            int i; int j;
            while (L < R)
            {
                T x = a[k];
                i = L; j = R;
                OddNoDupesMedianImplementation_Split(a, n, x, ref i, ref j, compare);
                if (j <= k) L = i;
                if (i >= k) R = j;
            }
        }

        private static void OddNoDupesMedianImplementation_Split<T>(T[] a, int n, T x, ref int i, ref int j, Compare<T> compare)
        {
            do
            {
                while (compare(a[i], x) == Comparison.Less) i++;
                while (compare(a[j], x) == Comparison.Greater) j--;
                T t = a[i];
                a[i] = a[j];
                a[j] = t;
            } while (i < j);
        }

        #endregion

        #region GeometricMean

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
        
        public static T StandardDeviation<T>(Stepper<T> stepper)
        {
            return SquareRoot(Variance(stepper));
        }

        #endregion

        #region MeanDeviation

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

        public static Range<T> Range<T>(Stepper<T> stepper)
        {
            bool set = false;
            T min = default(T);
            T max = default(T);
            stepper(i =>
            {
                if (!set)
                {
                    min = i;
                    max = i;
                    set = true;
                }
                else
                {
                    min = LessThan(i, min) ? i : min;
                    max = GreaterThan(i, max) ? i : max;
                }
            });
            return new Range<T>(min, max);
        }

        #endregion

        #region Quantiles

        public static T[] Quantiles<T>(int quantiles, Stepper<T> stepper)
        {
            if (quantiles < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantiles), quantiles, "!(" + nameof(quantiles) + " < 1)");
            }
            int count = stepper.Count();
            T[] ordered = new T[count];
            int a = 0;
            stepper(i => { ordered[a++] = i; });
            Algorithms.Sort<T>.Quick(Compare, ordered);
            T[] resultingQuantiles = new T[quantiles + 1];
            resultingQuantiles[0] = ordered[0];
            resultingQuantiles[resultingQuantiles.Length - 1] = ordered[ordered.Length - 1];
            T QUANTILES_PLUS_1 = FromInt32<T>(quantiles + 1);
            T ORDERED_LENGTH = FromInt32<T>(ordered.Length);
            for (int i = 1; i < quantiles; i++)
            {
                T I = FromInt32<T>(i);
                T temp = Divide(ORDERED_LENGTH, Multiply<T>(QUANTILES_PLUS_1, I));
                if (IsInteger(temp))
                {
                    resultingQuantiles[i] = ordered[ToInt32(temp)];
                }
                else
                {
                    resultingQuantiles[i] = Divide(Add(ordered[ToInt32(temp)], ordered[ToInt32(temp) + 1]), Constant<T>.Two);
                }
            }
            return resultingQuantiles;
        }

        #endregion

        #region Correlation
        //        /// <summary>Computes the median of a set of values.</summary>
        //        private static Compute<T>.Delegates.Correlation Correlation_private = (Stepper<T> a, Stepper<T> b) =>
        //        {
        //            throw new System.NotImplementedException("I introduced an error here when I removed the stepref off of structure. will fix soon");

        //            Compute<T>.Correlation_private =
        //        Meta.Compile<Compute<T>.Delegates.Correlation>(
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

        //            return Compute<T>.Correlation_private(a, b);
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
                if (typeof(double).IsAssignableFrom(typeof(T)))
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
        
        #region Sine

        public static T Sine<T>(Angle<T> a)
        {
            return SineImplementation<T>.Function(a);
        }

        internal static class SineImplementation<T>
        {
            internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Sin)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
                    Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();

                //// get the angle into the positive unit circle
                //_angle = _angle % (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi * 2);
                //if (_angle < 0)
                //    _angle = (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi * 2) + _angle;
                //if (_angle <= Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi / 2)
                //    goto QuandrantSkip;
                //else if (_angle <= Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi)
                //    _angle = (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), " >.Pi / 2) - (_angle % (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi / 2));
                //else if (_angle <= (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi * 3) / 2)
                //    _angle = _angle % Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi;
                //else
                //    _angle = (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), " >.Pi / 2) - (_angle % (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi / 2));
                //QuandrantSkip:
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" three_factorial = 6;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" five_factorial = 120;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" seven_factorial = 5040;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleCubed = _angle * _angle * _angle;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheFifth = angleCubed * _angle * _angle;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheSeventh = angleToTheFifth * _angle * _angle;
                //return -(_angle
                //    - (angleCubed / three_factorial)
                //    + (angleToTheFifth / five_factorial)
                //    - (angleToTheSeventh / seven_factorial));

            };
        }

        #endregion

        #region Cosine

        public static T Cosine<T>(Angle<T> a)
        {
            return CosineImplementation<T>.Function(a);
        }

        internal static class CosineImplementation<T>
        {
            internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Cos)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
                    Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();

                //// get the angle into the positive unit circle
                //_angle = _angle % (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi * 2);
                //if (_angle < 0)
                //    _angle = (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi * 2) + _angle;
                //if (_angle <= Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi / 2)
                //    goto QuandrantSkip;
                //else if (_angle <= Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi)
                //    _angle = (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), " >.Pi / 2) - (_angle % (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi / 2));
                //else if (_angle <= (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi * 3) / 2)
                //    _angle = _angle % Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi;
                //else
                //    _angle = (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), " >.Pi / 2) - (_angle % (Compute < ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" >.Pi / 2));
                //QuandrantSkip:
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" one = 1;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" two_factorial = 2;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" four_factorial = 24;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" six_factorial = 720;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleSquared = _angle * _angle;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheFourth = angleSquared * _angle * _angle;
                //", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheSixth = angleToTheFourth * _angle * _angle;
                //return one
                //    - (angleSquared / two_factorial)
                //    + (angleToTheFourth / four_factorial)
                //    - (angleToTheSixth / six_factorial);

            };
        }

        #endregion

        #region Tangent

        public static T Tangent<T>(Angle<T> a)
        {
            return TangentImplementation<T>.Function(a);
        }

        internal static class TangentImplementation<T>
        {
            internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Tan)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
                    Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();

        //        "	// get the angle into the positive unit circle" +
        //"	_angle = _angle % (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi * 2);" +
        //"	if (_angle < 0)" +
        //"		_angle = (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi * 2) + _angle;" +
        //"	if (_angle <= Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2) // quadrant 1" +
        //"		goto QuandrantSkip;" +
        //"	else if (_angle <= Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi) // quadrant 2" +
        //"		_angle = (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2) - (_angle % (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2));" +
        //"	else if (_angle <= (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi * 3) / 2) // quadrant 3" +
        //"		_angle = _angle % Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi;" +
        //"	else // quadrant 4" +
        //"		_angle = (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2) - (_angle % (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2));" +
        //"QuandrantSkip:" +
        //"	// do the computation" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " two = 2;" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " three = 3;" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " fifteen = 15;" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " seventeen = 17;" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " threehundredfifteen = 315;" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " angleCubed = _angle * _angle * _angle; // angle ^ 3" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " angleToTheFifth = angleCubed * _angle * _angle; // angle ^ 5" +
        //"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " angleToTheSeventh = angleToTheFifth * _angle * _angle;  // angle ^ 7" +
        //"	return angle" +
        //"		+ (angleCubed / three)" +
        //"		+ (two * angleToTheFifth / fifteen)" +
        //"		+ (seventeen * angleToTheSeventh / threehundredfifteen);" +

            };
        }

        #endregion

        #region Cosecant

        public static T Cosecant<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, Sine(a));
        }

        #endregion

        #region Secant

        public static T Secant<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, Cosine(a));
        }

        #endregion

        #region Cotangent

        public static T Cotangent<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, Tangent(a));
        }

        #endregion

        #region InverseSine

        public static Angle<T> InverseSine<T>(T a)
        {
            return InverseSineImplementation<T>.Function(a);
        }

        internal static class InverseSineImplementation<T>
        {
            internal static Func<T, Angle<T>> Function = (T a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Asin)), A));
                    Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region InverseCosine

        public static Angle<T> InverseCosine<T>(T a)
        {
            return InverseCosineImplementation<T>.Function(a);
        }

        internal static class InverseCosineImplementation<T>
        {
            internal static Func<T, Angle<T>> Function = (T a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Acos)), A));
                    Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region InverseTangent

        public static Angle<T> InverseTangent<T>(T a)
        {
            return InverseTangentImplementation<T>.Function(a);
        }

        internal static class InverseTangentImplementation<T>
        {
            internal static Func<T, Angle<T>> Function = (T a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Atan)), A));
                    Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region InverseCosecant

        public static Angle<T> InverseCosecant<T>(T a)
        {
            return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseSine(a).Radians));
        }

        #endregion

        #region InverseSecant

        public static Angle<T> InverseSecant<T>(T a)
        {
            return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseCosine(a).Radians));
        }

        #endregion

        #region InverseCotangent

        public static Angle<T> InverseCotangent<T>(T a)
        {
            return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseTangent(a).Radians));
        }

        #endregion

        #region HyperbolicSine

        public static T HyperbolicSine<T>(Angle<T> a)
        {
            return HyperbolicSineImplementation<T>.Function(a);
        }

        internal static class HyperbolicSineImplementation<T>
        {
            internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Sinh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
                    Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region HyperbolicCosine

        public static T HyperbolicCosine<T>(Angle<T> a)
        {
            return HyperbolicCosineImplementation<T>.Function(a);
        }

        internal static class HyperbolicCosineImplementation<T>
        {
            internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Cosh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
                    Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region HyperbolicTangent

        public static T HyperbolicTangent<T>(Angle<T> a)
        {
            return HyperbolicTangentImplementation<T>.Function(a);
        }

        internal static class HyperbolicTangentImplementation<T>
        {
            internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
            {
                // optimization for specific known types
                if (typeof(double).IsAssignableFrom(typeof(T)))
                {
                    ParameterExpression A = Expression.Parameter(typeof(T));
                    Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Tanh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
                    Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
                    return Function(a);
                }
                throw new NotImplementedException();
            };
        }

        #endregion

        #region HyperbolicCosecant

        public static T HyperbolicCosecant<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, HyperbolicSine(a));
        }

        #endregion

        #region HyperbolicSecant

        public static T HyperbolicSecant<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, HyperbolicCosine(a));
        }

        #endregion

        #region HyperbolicCotangent

        public static T HyperbolicCotangent<T>(Angle<T> a)
        {
            return Divide(Constant<T>.One, HyperbolicTangent(a));
        }

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
        
        #region Regression

        ///// <summary>A stepper through individual points in a 2D data set.</summary>
        ///// <param name="stepPoint">A step function for an individual point in the 2D data set.</param>
        //public delegate void PointCollection2D(StepPoint2D stepPoint);
        ///// <summary>A step function for a point in 2D space.</summary>
        ///// <param name="x">The x coordinate of the point.</param>
        ///// <param name="y">The y coordinate of the point.</param>
        //public delegate void StepPoint2D(T x, T y);

        ///// <summary>Performs simple 2D linear regression to find a best fit line in the form: "y = m * x + b".</summary>
        ///// <typeparam name="T">The numeric type.</typeparam>
        ///// <param name="points">The points to estimate a line out of.</param>
        ///// <param name="slope">The resulting slope of the best fit line.</param>
        ///// <param name="y_intercept">The resulting y axis intercept of the best fit line.</param>
        ///// <exception cref="System.InvalidOperationException">Vertical lines are not supported. There must be >2 unique X valued points.</exception>
        //public static void LinearRegression2D(PointCollection2D points, out T slope, out T y_intercept)
        //{
        //    Code.AssertArgNonNull(points, "points");
        //    int count = 0;
        //    // first find the mean X value (so we can divide the data into two halfs)
        //    T mean_x = Towel.Mathematics.Compute<T>.Mean((Step<T> step) =>
        //    {
        //        points((T x, T y) =>
        //        {
        //            step(x);
        //            count++;
        //        });
        //    });
        //    if (count < 2)
        //        throw new System.InvalidOperationException("Attempting to perform 2D linear regression on an insufficient data set (<2 points provided).");
        //    // add up all the x & y values while keeping the data segregated 
        //    // based on the mean X value and keeping a running totals
        //    int count_1 = 0;
        //    int count_2 = 0;
        //    T x_sum_1 = Compute<T>.Zero; T y_sum_1 = Compute<T>.Zero; // represents point 1 of best fit line
        //    T x_sum_2 = Compute<T>.Zero; T y_sum_2 = Compute<T>.Zero; // represents point 2 of best fit line
        //    points((T x, T y) =>
        //    {
        //        if (Compute<T>.Compare(x, mean_x) == Comparison.Less)
        //        {
        //            count_1++;
        //            x_sum_1 = Compute<T>.Add(x_sum_1, x);
        //            y_sum_1 = Compute<T>.Add(y_sum_1, y);
        //        }
        //        else
        //        {
        //            count_2++;
        //            x_sum_2 = Compute<T>.Add(x_sum_2, x);
        //            y_sum_2 = Compute<T>.Add(y_sum_2, y);
        //        }
        //    });
        //    if (count_1 == 0 || count_2 == 0)
        //        throw new System.InvalidOperationException("Not enough unique X values for 2D linear regression (vertical lines are not supported).");
        //    // divide all summations by the count to calculate means
        //    T count_1_T = Compute<T>.FromInt32(count_1); // convert int to T
        //    T x_1 = Compute<T>.Divide(x_sum_1, count_1_T);
        //    T y_1 = Compute<T>.Divide(y_sum_1, count_1_T);
        //    T count_2_T = Compute<T>.FromInt32(count_2); // convert int to T
        //    T x_2 = Compute<T>.Divide(x_sum_2, count_2_T);
        //    T y_2 = Compute<T>.Divide(y_sum_2, count_2_T);
        //    // At this point we have two points on the best fit line: (x_1, y_1) & (x_2, y_2)
        //    // calculate the slope and the y-intercept
        //    slope = Compute<T>.Divide(Compute<T>.Subtract(y_2, y_1), Compute<T>.Subtract(x_2, x_1)); // m = (y2- y1) / (x2- x1)
        //    y_intercept = Compute<T>.Subtract(y_1, Compute<T>.Multiply(x_1, slope)); // b = y' - m * x' where (x', y') is a point on the line
        //}

        ///// <summary>Performs simple 2D linear regression to find a best fit line in the form: "y = m * x + b".</summary>
        ///// <typeparam name="T">The numeric type.</typeparam>
        ///// <param name="stepper">The vectors to make a line out of.</param>
        ///// <param name="slope">The resulting slope of the best fit line.</param>
        ///// <param name="y_intercept">The resulting y axis intercept of the best fit line.</param>
        ///// <exception cref="System.InvalidOperationException">Vertical lines are not supported. There must be >2 unique X valued points.</exception>
        //public static void LinearRegression2D(Stepper<Vector<T>> stepper, out T slope, out T y_intercept)
        //{
        //    Code.AssertArgNonNull(stepper, "stepper");
        //    LinearRegression2D((StepPoint2D stepPoint) =>
        //    {
        //        stepper((Vector<T> vector) =>
        //        {
        //            if (vector.Dimensions != 2)
        //                throw new System.InvalidOperationException("Attempting to perform 2D linear regression on vectors that are not 2D.");
        //            stepPoint(vector[0], vector[1]);
        //        });
        //    }, out slope, out y_intercept);
        //}

        ///// <summary>Performs simple 2D linear regression to find a best fit line in the form: "y = m * x + b".</summary>
        ///// <typeparam name="T">The numeric type.</typeparam>
        ///// <param name="x_values">The set of x values for the 2D data set.</param>
        ///// <param name="y_values">The set of y values for the 2D data set.</param>
        ///// <param name="slope">The resulting slope of the best fit line.</param>
        ///// <param name="y_intercept">The resulting y axis intercept of the best fit line.</param>
        ///// <exception cref="System.InvalidOperationException">Vertical lines are not supported. There must be >2 unique X valued points.</exception>
        //public static void LinearRegression2D(T[] x_values, T[] y_values, out T slope, out T y_intercept)
        //{
        //    Code.AssertArgNonNull(x_values, "x_values");
        //    Code.AssertArgNonNull(y_values, "y_values");
        //    if (x_values.Length != y_values.Length)
        //        throw new System.InvalidOperationException("Invalid 2D data set. (the number of x and y values are not equal)");
        //    LinearRegression2D((StepPoint2D stepPoint) =>
        //    {
        //        for (int i = 0; i < x_values.Length; i++)
        //        {
        //            stepPoint(x_values[0], y_values[1]);
        //        }
        //    }, out slope, out y_intercept);
        //}

        #endregion

        #region FactorPrimes

        public static void FactorPrimes<T>(T a, Step<T> step)
        {
            T A = a;
            if (!IsInteger(A))
            {
                throw new ArgumentOutOfRangeException(nameof(A), A, "!(" + nameof(A) + "." + nameof(IsInteger) + ")");
            }
            if (IsNegative(A))
            {
                A = AbsoluteValue(A);
                step(FromInt32<T>(-1));
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
        }

        #endregion
    }
}
