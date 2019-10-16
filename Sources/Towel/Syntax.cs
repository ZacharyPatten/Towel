using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using Towel.DataStructures;
using Towel.Algorithms;
using Towel.Measurements;

namespace Towel
{
	/// <summary>Towel syntax.</summary>
	public static class Syntax
	{
		#region Internals

		// These are some shared internal optimizations that I don't want to expose because it might confuse people.
		// If you need to use it, just copy this code into your own project.

		/// <summary>a * b + c</summary>
		internal static class MultiplyAddImplementation<T>
		{
			internal static Func<T, T, T, T> Function = (a, b, c) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				ParameterExpression B = Expression.Parameter(typeof(T));
				ParameterExpression C = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Add(Expression.Multiply(A, B), C);
				Function = Expression.Lambda<Func<T, T, T, T>>(BODY, A, B, C).Compile();
				return Function(a, b, c);
			};
		}

		internal static T OperationOnStepper<T>(Stepper<T> stepper, Func<T, T, T> operation)
		{
			if (stepper is null)
			{
				throw new ArgumentNullException(nameof(stepper));
			}
			T result = default;
			bool assigned = false;
			stepper(a =>
			{
				if (assigned)
				{
					result = operation(result, a);
				}
				else
				{
					result = a;
					assigned = true;
				}
			});
			return assigned
				? result
				: throw new ArgumentException(nameof(stepper), nameof(stepper) + " is empty.");
		}

		#endregion

		#region TryParse

		/// <summary>Assumes a TryParse method exists for a generic type and calls it [<see cref="bool"/> TryParse(<see cref="string"/>, out <typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The generic type to make assumptions about.</typeparam>
		/// <param name="string">The string to be parsed.</param>
		/// <param name="Default">The default value if the parse fails.</param>
		/// <returns>The parsed value or the default value if the parse fails.</returns>
		public static A TryParse<A>(string @string, A Default = default) =>
			TryParse(@string, out A value)
			? value
			: Default;

		/// <summary>Assumes a TryParse method exists for a generic type and calls it [<see cref="bool"/> TryParse(<see cref="string"/>, out <typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The generic type to make assumptions about.</typeparam>
		/// <param name="string">The string to be parsed.</param>
		/// <param name="value">The parsed value if successful or default if not.</param>
		/// <returns>True if successful or false if not.</returns>
		public static bool TryParse<A>(string @string, out A value) =>
			TryParseImplementation<A>.Function(@string, out value);

		internal static class TryParseImplementation<A>
		{
			internal delegate bool TryParseDelegate(string @string, out A value);

			internal static TryParseDelegate Function = (string @string, out A value) =>
			{
				MethodInfo methodInfo = Meta.GetTryParseMethod<A>();
				Function = methodInfo is null
					? Default
					: (TryParseDelegate)methodInfo.CreateDelegate(typeof(TryParseDelegate));
				return Function(@string, out value);
			};

			internal static bool Default(string @string, out A value)
			{
				value = default;
				return false;
			}
		}

		#endregion

		#region Convert

		/// <summary>Converts <paramref name="a"/> from <typeparamref name="A"/> to <typeparamref name="B"/>.</summary>
		/// <typeparam name="A">The type of the value to convert.</typeparam>
		/// <typeparam name="B">The type to convert teh value to.</typeparam>
		/// <param name="a">The value to convert.</param>
		/// <returns>The <paramref name="a"/> value of <typeparamref name="B"/> type.</returns>
		public static B Convert<A, B>(A a) =>
			ConvertImplementation<A, B>.Function(a);

		internal static class ConvertImplementation<A, B>
		{
			internal static Func<A, B> Function = a =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				Expression BODY = Expression.Convert(A, typeof(B));
				Function = Expression.Lambda<Func<A, B>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region Continue & Break

		/// <summary>Stepper was not broken.</summary>
		public const StepStatus Continue = StepStatus.Continue;

		/// <summary>Stepper was broken.</summary>
		public const StepStatus Break = StepStatus.Break;

		#endregion

		#region Greater Less Equal

		/// <summary>The left operand is less than the right operand.</summary>
		public const CompareResult Less = CompareResult.Less;

		/// <summary>The left operand is equal to the right operand.</summary>
		public const CompareResult Equal = CompareResult.Equal;

		/// <summary>The left operand is greater than the right operand.</summary>
		public const CompareResult Greater = CompareResult.Greater;

		#endregion

		// Logic

		#region Equality

		/// <summary>Checks for equality of two values [<paramref name="a"/> == <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the equality.</returns>
		public static C Equality<A, B, C>(A a, B b) =>
			EqualityImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks for equality of two values [<paramref name="a"/> == <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool Equality<T>(T a, T b) =>
			Equality<T, T, bool>(a, b);

		/// <summary>Checks for equality among multiple values [<paramref name="a"/> == <paramref name="b"/> == <paramref name="c"/> == ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the equality check.</param>
		/// <param name="b">The second operand of the equality check.</param>
		/// <param name="c">The remaining operands of the equality check.</param>
		/// <returns>True if all operands are equal or false if not.</returns>
		public static bool Equality<T>(T a, T b, params T[] c) =>
			c is null ? throw new ArgumentNullException(nameof(c)) :
			c.Length == 0 ? throw new ArgumentException("The array is empty.", nameof(c)) :
			Equality(a, b) && !c.Any(x => !Equality(a, x));

		#region Alternative

		//Equal((StepBreak<T> x) => x(a) == Break
		//	? Break
		//	: x(b) == Break
		//		? Break
		//		: c.ToStepperBreak()(x));

		#endregion

		#region System.Collections.Generic.IEnumerable<T>

		// I'm not sure it I want to add IEnumerable overloads or not...

		///// <summary>Checks for equality among multiple numeric operands.</summary>
		///// <typeparam name="T">The numeric type of the operation.</typeparam>
		///// <param name="iEnumerable">The operands.</param>
		///// <returns>True if all operands are equal or false if not.</returns>
		//public static bool Equal<T>(System.Collections.Generic.IEnumerable<T> iEnumerable)
		//{
		//	return Equal(iEnumerable.ToStepperBreak());

		//	#region Alternative

		//	//if (!iEnumerable.TryFirst(out T first))
		//	//{
		//	//	throw new ArgumentNullException(nameof(iEnumerable), nameof(iEnumerable) + " is empty.");
		//	//}
		//	//foreach (T value in iEnumerable)
		//	//{
		//	//	if (!Equal(first, value))
		//	//	{
		//	//		return false;
		//	//	}
		//	//}
		//	//return true;

		//	#endregion
		//}

		#endregion

		/// <summary>Checks for equality among multiple values [step1 == step2 == step3 == ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The operands of the equality check.</param>
		/// <returns>True if all operand are equal or false if not.</returns>
		public static bool Equality<T>(Stepper<T> stepper)
		{
			if (stepper is null)
			{
				throw new ArgumentNullException(nameof(stepper));
			}
			T value = default;
			bool assigned = false;
			bool result = stepper.Any(x =>
			{
				if (assigned)
				{
					return !Equality(value, x);
				}
				else
				{
					value = x;
					assigned = true;
					return false;
				}
			});
			if (!assigned)
			{
				throw new ArgumentNullException(nameof(stepper), nameof(stepper) + " is empty.");
			}
			return result;
		}

		/// <summary>Checks for equality among multiple values [step1 == step2 == step3 == ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The operands of the equality check.</param>
		/// <returns>True if all operand are equal or false if not.</returns>
		public static bool Equality<T>(StepperBreak<T> stepper)
		{
			if (stepper is null)
			{
				throw new ArgumentNullException(nameof(stepper));
			}
			T value = default;
			bool assigned = false;
			bool result = stepper.Any(x =>
			{
				if (assigned)
				{
					return !Equality(value, x);
				}
				else
				{
					value = x;
					assigned = true;
					return false;
				}
			});
			return assigned
				? result
				: throw new ArgumentException(nameof(stepper), nameof(stepper) + " is empty.");
		}

		internal static class EqualityImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Equal(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Inequality

		/// <summary>Checks for inequality of two values [<paramref name="a"/> != <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the inequality.</returns>
		public static C Inequality<A, B, C>(A a, B b) =>
			InequalityImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks for inequality of two values [<paramref name="a"/> != <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the inequality check.</param>
		/// <param name="b">The second operand of the inequality check.</param>
		/// <returns>The result of the inequality check.</returns>
		public static bool Inequality<T>(T a, T b) =>
			Inequality<T, T, bool>(a, b);

		#region Alternative

		//NotEqual((StepBreak<T> x) =>
		//	x(a) == Break
		//		? Break
		//		: x(b) == Break
		//			? Break
		//			: c.ToStepperBreak()(x));

		#endregion

		#region System.Collections.Generic.IEnumerable<T>

		// I'm not sure it I want to add IEnumerable overloads or not...

		///// <summary>Checks for equality among multiple numeric operands.</summary>
		///// <typeparam name="T">The numeric type of the operation.</typeparam>
		///// <param name="iEnumerable">The operands.</param>
		///// <returns>True if all operands are equal or false if not.</returns>
		//public static bool Equal<T>(System.Collections.Generic.IEnumerable<T> iEnumerable)
		//{
		//	return NotEqual(iEnumerable.ToStepperBreak());

		//	#region Alternative

		//	//if (!iEnumerable.TryFirst(out T first))
		//	//{
		//	//	throw new ArgumentNullException(nameof(iEnumerable), nameof(iEnumerable) + " is empty.");
		//	//}
		//	//foreach (T value in iEnumerable)
		//	//{
		//	//	if (!NotEqual(first, value))
		//	//	{
		//	//		return false;
		//	//	}
		//	//}
		//	//return true;

		//	#endregion
		//}

		#endregion

		internal static class InequalityImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.NotEqual(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region LessThan

		/// <summary>Checks if one value is less than another [<paramref name="a"/> &lt; <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the less than operation.</returns>
		public static C LessThan<A, B, C>(A a, B b) =>
			LessThanImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is less than another [<paramref name="a"/> &lt; <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the less than check.</param>
		/// <param name="b">The second operand of the less than check.</param>
		/// <returns>The result of the less than check.</returns>
		public static bool LessThan<T>(T a, T b) =>
			LessThan<T, T, bool>(a, b);

		internal static class LessThanImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.LessThan(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region GreaterThan

		/// <summary>Checks if one value is greater than another [<paramref name="a"/> &gt; <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the greater than operation.</returns>
		public static C GreaterThan<A, B, C>(A a, B b) =>
			GreaterThanImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is greater than another [<paramref name="a"/> &gt; <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the greater than check.</param>
		/// <param name="b">The second operand of the greater than check.</param>
		/// <returns>The result of the greater than check.</returns>
		public static bool GreaterThan<T>(T a, T b) =>
			GreaterThan<T, T, bool>(a, b);

		internal static class GreaterThanImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.GreaterThan(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region LessThanOrEqual

		/// <summary>Checks if one value is less than or equal to another [<paramref name="a"/> &lt;= <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the less than or equal to operation.</returns>
		public static C LessThanOrEqual<A, B, C>(A a, B b) =>
			LessThanOrEqualImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is less than or equal to another [<paramref name="a"/> &lt;= <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the less than or equal to check.</param>
		/// <param name="b">The second operand of the less than or equal to check.</param>
		/// <returns>The result of the less than or equal to check.</returns>
		public static bool LessThanOrEqual<T>(T a, T b) =>
			LessThanOrEqual<T, T, bool>(a, b);

		internal static class LessThanOrEqualImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.LessThanOrEqual(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region GreaterThanOrEqual

		/// <summary>Checks if one value is less greater or equal to another [<paramref name="a"/> &gt;= <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the greater than or equal to operation.</returns>
		public static C GreaterThanOrEqual<A, B, C>(A a, B b) =>
			GreaterThanOrEqualImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is greater than or equal to another [<paramref name="a"/> &gt;= <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the greater than or equal to check.</param>
		/// <param name="b">The second operand of the greater than or equal to check.</param>
		/// <returns>The result of the greater than or equal to check.</returns>
		public static bool GreaterThanOrEqual<T>(T a, T b) =>
			GreaterThanOrEqual<T, T, bool>(a, b);

		internal static class GreaterThanOrEqualImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.GreaterThanOrEqual(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		// Mathematics

		#region Negation

		/// <summary>Negates a value [-<paramref name="a"/>].</summary>
		/// <typeparam name="A">The type of the value to negate.</typeparam>
		/// <typeparam name="B">The resulting type of the negation.</typeparam>
		/// <param name="a">The value to negate.</param>
		/// <returns>The result of the negation [-<paramref name="a"/>].</returns>
		public static B Negation<A, B>(A a) =>
			NegationImplementation<A, B>.Function(a);

		/// <summary>Negates a value [-<paramref name="a"/>].</summary>
		/// <typeparam name="T">The type of the value to negate.</typeparam>
		/// <param name="a">The value to negate.</param>
		/// <returns>The result of the negation [-<paramref name="a"/>].</returns>
		public static T Negation<T>(T a) =>
			Negation<T, T>(a);

		internal static class NegationImplementation<A, B>
		{
			internal static Func<A, B> Function = a =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				Expression BODY = Expression.Negate(A);
				Function = Expression.Lambda<Func<A, B>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region Addition

		/// <summary>Adds two values [<paramref name="a"/> + <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/>].</returns>
		public static C Addition<A, B, C>(A a, B b) =>
			AdditionImplementation<A, B, C>.Function(a, b);

		/// <summary>Adds two values [<paramref name="a"/> + <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/>].</returns>
		public static T Addition<T>(T a, T b) =>
			Addition<T, T, T>(a, b);

		/// <summary>Adds multiple values [<paramref name="a"/> + <paramref name="b"/> + <paramref name="c"/> + ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand of the addition.</param>
		/// <param name="b">The second operand of the addition.</param>
		/// <param name="c">The third operand of the addition.</param>
		/// <param name="d">The remaining operands of the addition.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/> + <paramref name="c"/> + ...].</returns>
		public static T Addition<T>(T a, T b, T c, params T[] d) =>
			Addition<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Adds multiple values [step1 + step2 + step3 + ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper of the values to add.</param>
		/// <returns>The result of the addition [step1 + step2 + step3 + ...].</returns>
		public static T Addition<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Addition);

		internal static class AdditionImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Add(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Subtraction

		/// <summary>Subtracts two values [<paramref name="a"/> - <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction [<paramref name="a"/> - <paramref name="b"/>].</returns>
		public static C Subtraction<A, B, C>(A a, B b) =>
			SubtractionImplementation<A, B, C>.Function(a, b);

		/// <summary>Subtracts two values [<paramref name="a"/> - <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction [<paramref name="a"/> - <paramref name="b"/>].</returns>
		public static T Subtraction<T>(T a, T b) =>
			Subtraction<T, T, T>(a, b);

		/// <summary>Subtracts multiple values [<paramref name="a"/> - <paramref name="b"/> - <paramref name="c"/> - ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <param name="c">The third operand.</param>
		/// <param name="d">The remaining values.</param>
		/// <returns>The result of the subtraction [<paramref name="a"/> - <paramref name="b"/> - <paramref name="c"/> - ...].</returns>
		public static T Subtraction<T>(T a, T b, T c, params T[] d) =>
			Subtraction<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Subtracts multiple numeric values [step1 - step2 - step3 - ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the subtraction [step1 - step2 - step3 - ...].</returns>
		public static T Subtraction<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Subtraction);

		internal static class SubtractionImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Subtract(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Multiplication

		/// <summary>Multiplies two values [<paramref name="a"/> * <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/>].</returns>
		public static C Multiplication<A, B, C>(A a, B b) =>
			MultiplicationImplementation<A, B, C>.Function(a, b);

		/// <summary>Multiplies two values [<paramref name="a"/> * <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/>].</returns>
		public static T Multiplication<T>(T a, T b) =>
			Multiplication<T, T, T>(a, b);

		/// <summary>Multiplies multiple values [<paramref name="a"/> * <paramref name="b"/> * <paramref name="c"/> * ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <param name="c">The third operand.</param>
		/// <param name="d">The remaining values.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/> * <paramref name="c"/> * ...].</returns>
		public static T Multiplication<T>(T a, T b, T c, params T[] d) =>
			Multiplication<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Multiplies multiple values [step1 * step2 * step3 * ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the multiplication [step1 * step2 * step3 * ...].</returns>
		public static T Multiplication<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Multiplication);

		internal static class MultiplicationImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Multiply(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Division

		/// <summary>Divides two values [<paramref name="a"/> / <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division [<paramref name="a"/> / <paramref name="b"/>].</returns>
		public static C Division<A, B, C>(A a, B b) =>
			DivisionImplementation<A, B, C>.Function(a, b);

		/// <summary>Divides two values [<paramref name="a"/> / <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division [<paramref name="a"/> / <paramref name="b"/>].</returns>
		public static T Division<T>(T a, T b) =>
			Division<T, T, T>(a, b);

		/// <summary>Divides multiple values [<paramref name="a"/> / <paramref name="b"/> / <paramref name="c"/> / ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand of the division.</param>
		/// <param name="b">The second operand of the division.</param>
		/// <param name="c">The third operand of the division.</param>
		/// <param name="d">The remaining values of the division.</param>
		/// <returns>The result of the division [<paramref name="a"/> / <paramref name="b"/> / <paramref name="c"/> / ...].</returns>
		public static T Division<T>(T a, T b, T c, params T[] d) =>
			Division<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Divides multiple values [step1 / step2 / step3 / ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the division [step1 / step2 / step3 / ...].</returns>
		public static T Division<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Division);

		internal static class DivisionImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Divide(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Remainder

		/// <summary>Remainders two values [<paramref name="a"/> % <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the remainder operation [<paramref name="a"/> % <paramref name="b"/>].</returns>
		public static C Remainder<A, B, C>(A a, B b) =>
			RemainderImplementation<A, B, C>.Function(a, b);

		/// <summary>Modulos two numeric values [<paramref name="a"/> % <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the modulation.</param>
		/// <param name="b">The second operand of the modulation.</param>
		/// <returns>The result of the modulation.</returns>
		public static T Remainder<T>(T a, T b) =>
			Remainder<T, T, T>(a, b);

		/// <summary>Modulos multiple numeric values [<paramref name="a"/> % <paramref name="b"/> % <paramref name="c"/> % ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the modulation.</param>
		/// <param name="b">The second operand of the modulation.</param>
		/// <param name="c">The third operand of the modulation.</param>
		/// <param name="d">The remaining values of the modulation.</param>
		/// <returns>The result of the modulation.</returns>
		public static T Remainder<T>(T a, T b, T c, params T[] d) =>
			Remainder<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Modulos multiple numeric values [step_1 % step_2 % step_3...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the modulation.</returns>
		public static T Remainder<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Remainder);

		internal static class RemainderImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Modulo(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Inversion

		/// <summary>Inverts a numeric value [1 / a].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The numeric value to invert.</param>
		/// <returns>The result of the inversion.</returns>
		public static T Inversion<T>(T a) =>
			Division(Constant<T>.One, a);

		#endregion

		#region Power

		/// <summary>Powers two numeric values [a ^ b].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the power.</param>
		/// <param name="b">The first operand of the power.</param>
		/// <returns>The result of the power.</returns>
		public static T Power<T>(T a, T b) =>
			PowerImplementation<T>.Function(a, b);

		/// <summary>Powers multiple numeric values [a ^ b ^ c...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the power.</param>
		/// <param name="b">The second operand of the power.</param>
		/// <param name="c">The third operand of the power.</param>
		/// <param name="d">The remaining values of the power.</param>
		/// <returns>The result of the power.</returns>
		public static T Power<T>(T a, T b, T c, params T[] d) =>
			Power<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Powers multiple numeric values [step_1 ^ step_2 ^ step_3...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the power.</returns>
		public static T Power<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Power);

		internal static class PowerImplementation<T>
		{
			internal static Func<T, T, T> Function = (a, b) =>
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
					Function = (A, B) =>
					{
						if (IsInteger(B) && IsPositive(B))
						{
							T result = A;
							int power = Convert<T, int>(B);
							for (int i = 0; i < power; i++)
							{
								result = Multiplication(result, A);
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
		public static T SquareRoot<T>(T a) =>
			SquareRootImplementation<T>.Function(a);

		internal static class SquareRootImplementation<T>
		{
			internal static Func<T, T> Function = a =>
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
		public static T Root<T>(T a, T b) =>
			Power(a, Inversion(b));

		#endregion

		#region Logarithm

		/// <summary>Computes the logarithm of a value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="value">The value to compute the logarithm of.</param>
		/// <param name="base">The base of the logarithm to compute.</param>
		/// <returns>The computed logarithm value.</returns>
		public static T Logarithm<T>(T value, T @base) =>
			LogarithmImplementation<T>.Function(value, @base);

		internal static class LogarithmImplementation<T>
		{
			internal static Func<T, T, T> Function = (a, b) =>
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
		public static bool IsInteger<T>(T a) =>
			IsIntegerImplementation<T>.Function(a);

		internal static class IsIntegerImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsIntegerMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

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
		public static bool IsNonNegative<T>(T a) =>
			IsNonNegativeImplementation<T>.Function(a);

		internal static class IsNonNegativeImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsNonNegativeMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

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
		public static bool IsNegative<T>(T a) =>
			IsNegativeImplementation<T>.Function(a);

		internal static class IsNegativeImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsNegativeMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

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
		public static bool IsPositive<T>(T a) =>
			IsPositiveImplementation<T>.Function(a);

		internal static class IsPositiveImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsPositiveMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

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
		public static bool IsEven<T>(T a) =>
			IsEvenImplementation<T>.Function(a);

		internal static class IsEvenImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsEvenMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

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
		public static bool IsOdd<T>(T a) =>
			IsOddImplementation<T>.Function(a);

		internal static class IsOddImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsOddMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

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
		public static bool IsPrime<T>(T a) =>
			IsPrimeImplementation<T>.Function(a);

		internal static class IsPrimeImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetIsPrimeMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, bool>)methodInfo.CreateDelegate(typeof(Func<T, bool>));
					return Function(a);
				}

				// This can be optimized
				Function = A =>
				{
					if (IsInteger(A) && !LessThan(A, Constant<T>.Two))
					{
						if (Equality(A, Constant<T>.Two))
						{
							return true;
						}
						if (IsEven(A))
						{
							return false;
						}
						T squareRoot = SquareRoot(A);
						for (T divisor = Constant<T>.Three; LessThanOrEqual(divisor, squareRoot); divisor = Addition(divisor, Constant<T>.Two))
						{
							if (Equality(Remainder<T>(A, divisor), Constant<T>.Zero))
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
				};
				return Function(a);
			};
		}

		#endregion

		#region AbsoluteValue

		/// <summary>Gets the absolute value of a value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to get the absolute value of.</param>
		/// <returns>The absolute value of the provided value.</returns>
		public static T AbsoluteValue<T>(T a) =>
			AbsoluteValueImplementation<T>.Function(a);

		internal static class AbsoluteValueImplementation<T>
		{
			internal static Func<T, T> Function = a =>
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
		public static T Maximum<T>(T a, T b) =>
			GreaterThanOrEqual(a, b) ? a : b;

		/// <summary>Computes the maximum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the maximum operation.</param>
		/// <param name="b">The second operand of the maximum operation.</param>
		/// <param name="c">The third operand of the maximum operation.</param>
		/// <param name="d">The remaining operands of the maximum operation.</param>
		/// <returns>The computed maximum of the provided values.</returns>
		public static T Maximum<T>(T a, T b, T c, params T[] d) =>
			Maximum<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Computes the maximum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the maximum of.</param>
		/// <returns>The computed maximum of the provided values.</returns>
		public static T Maximum<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Maximum);

		#endregion

		#region Minimum

		/// <summary>Computes the minimum of two numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(T a, T b) =>
			LessThanOrEqual(a, b) ? a : b;

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <param name="c">The third operand of the minimum operation.</param>
		/// <param name="d">The remaining operands of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(T a, T b, T c, params T[] d) =>
			Minimum<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the minimum of.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(Stepper<T> stepper) =>
			OperationOnStepper(stepper, Minimum);

		#endregion

		#region Clamp

		/// <summary>Gets a value restricted to a minimum and maximum range.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="value">The value to clamp.</param>
		/// <param name="minimum">The minimum of the range to clamp the value by.</param>
		/// <param name="maximum">The maximum of the range to clamp the value by.</param>
		/// <returns>The value restricted to the provided range.</returns>
		public static T Clamp<T>(T value, T minimum, T maximum) =>
			LessThan(value, minimum)
			? minimum
			: GreaterThan(value, maximum)
				? maximum
				: value;

		#endregion

		#region EqualityLeniency

		/// <summary>Checks for equality between two numeric values with a range of possibly leniency.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the equality check.</param>
		/// <param name="b">The second operand of the equality check.</param>
		/// <param name="leniency">The allowed distance between the values to still be considered equal.</param>
		/// <returns>True if the values are within the allowed leniency of each other. False if not.</returns>
		public static bool EqualityLeniency<T>(T a, T b, T leniency) =>
			// TODO: add an ArgumentOutOfBounds check on leniency
			EqualityLeniencyImplementation<T>.Function(a, b, leniency);

		internal static class EqualityLeniencyImplementation<T>
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

		#region Comparison

		/// <summary>Compares two values.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the comparison.</returns>
		public static CompareResult Comparison<A, B>(A a, B b) =>
			CompareImplementation<A, B>.Function(a, b);

#if Hidden
		// This code is hidden currently because it collides with the
		// System.Comparison<T> delegate type. I may or may not add it
		// in the future.

		/// <summary>Compares two values.</summary>
		/// <typeparam name="T">The type of the operands.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the comparison.</returns>
		public static CompareResult Comparison<T>(T a, T b) =>
			Comparison<T, T>(a, b);
#endif

		internal static class CompareImplementation<A, B>
		{
			internal static Compare<A, B> Function = (a, b) =>
			{
				if (typeof(A) == typeof(B) && a is IComparable<B> &&
					!(typeof(A).IsPrimitive && typeof(B).IsPrimitive))
				{
					CompareImplementation<A, A>.Function =
						new Compare<A, A>(
							Compare.ToCompare(System.Collections.Generic.Comparer<A>.Default));
				}
				else
				{
					ParameterExpression A = Expression.Parameter(typeof(A));
					ParameterExpression B = Expression.Parameter(typeof(B));

					Expression lessThanPredicate =
						typeof(A).IsPrimitive && typeof(B).IsPrimitive
						? Expression.LessThan(A, B)
						: !(Meta.GetLessThanMethod<A, B, bool>() is null)
							? Expression.LessThan(A, B)
							: !(Meta.GetGreaterThanMethod<B, A, bool>() is null)
								? Expression.GreaterThan(B, A)
								: null;

					Expression greaterThanPredicate =
						typeof(A).IsPrimitive && typeof(B).IsPrimitive
						? Expression.GreaterThan(A, B)
						: !(Meta.GetGreaterThanMethod<A, B, bool>() is null)
							? Expression.GreaterThan(A, B)
							: !(Meta.GetLessThanMethod<B, A, bool>() is null)
								? Expression.LessThan(B, A)
								: null;

					if (lessThanPredicate is null || greaterThanPredicate is null)
					{
						throw new NotSupportedException("You attempted a comparison operation with unsupported types.");
					}

					LabelTarget RETURN = Expression.Label(typeof(CompareResult));
					Expression BODY = Expression.Block(
						Expression.IfThen(
								lessThanPredicate,
								Expression.Return(RETURN, Expression.Constant(Less, typeof(CompareResult)))),
							Expression.IfThen(
								greaterThanPredicate,
								Expression.Return(RETURN, Expression.Constant(Greater, typeof(CompareResult)))),
							Expression.Return(RETURN, Expression.Constant(Equal, typeof(CompareResult))),
							Expression.Label(RETURN, Expression.Constant(default(CompareResult), typeof(CompareResult))));
					Function = Expression.Lambda<Compare<A, B>>(BODY, A, B).Compile();
				}
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
		public static T GreatestCommonFactor<T>(T a, T b, params T[] c) =>
			GreatestCommonFactor<T>(step => { step(a); step(b); c.ToStepper()(step); });

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
				else if (Equality(n, Constant<T>.Zero))
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
						while (Inequality(b, Constant<T>.Zero))
						{
							T remainder = Remainder(a, b);
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
		public static T LeastCommonMultiple<T>(T a, T b, params T[] c) =>
			LeastCommonMultiple<T>(step => { step(a); step(b); c.ToStepper()(step); });

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
			T answer = default;
			stepper(parameter =>
			{
				if (Equality(parameter, Constant<T>.Zero))
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
					answer = Division(Multiplication(answer, parameter), GreatestCommonFactor(answer, parameter));
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

		/// <summary>Linearly interpolations a value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="x">The value along the first dimension to compute the linear interpolation for.</param>
		/// <param name="x0">A known starting point along the first dimension.</param>
		/// <param name="x1">A known ending point along the first dimension.</param>
		/// <param name="y0">A known starting point along the second dimension.</param>
		/// <param name="y1">A known ending point along the second dimension.</param>
		/// <returns>The linearly interpolated value.</returns>
		public static T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1)
		{
			if (GreaterThan(x0, x1) ||
				GreaterThan(x, x1) ||
				LessThan(x, x0))
			{
				throw new MathematicsException("Arguments out of range !(" + nameof(x0) + " <= " + nameof(x) + " <= " + nameof(x1) + ") [" + x0 + " <= " + x + " <= " + x1 + "].");
			}
			if (Equality(x0, x1))
			{
				if (Inequality(y0, y1))
				{
					throw new MathematicsException("Arguments out of range (" + nameof(x0) + " == " + nameof(x1) + ") but !(" + nameof(y0) + " != " + nameof(y1) + ") [" + y0 + " != " + y1 + "].");
				}
				else
				{
					return y0;
				}
			}
			return Addition(y0, Division(Multiplication(Subtraction(x, x0), Subtraction(y1, y0)), Subtraction(x1, x0)));
		}

		#endregion

		#region Factorial

		/// <summary>Computes the factorial of a numeric value [<paramref name="a"/>!] == [<paramref name="a"/> * (<paramref name="a"/> - 1) * (<paramref name="a"/> - 2) * ... * 1].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The integer value to compute the factorial of.</param>
		/// <returns>The computed factorial value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the parameter is not an integer value.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the parameter is less than zero.</exception>
		public static T Factorial<T>(T a) =>
			FactorialImplementation<T>.Function(a);

		internal static class FactorialImplementation<T>
		{
			internal static Func<T, T> Function = a =>
			{
				MethodInfo methodInfo = Meta.GetFactorialMethod<T>();
				if (!(methodInfo is null))
				{
					Function = (Func<T, T>)methodInfo.CreateDelegate(typeof(Func<T, T>));
					return Function(a);
				}

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
					for (; GreaterThan(A, Constant<T>.One); A = Subtraction(A, Constant<T>.One))
						result = Multiplication(A, result);
					return result;
				};
				return Function(a);
			};
		}

		#endregion

		#region Combinations

		/// <summary>Computes the combinations of <paramref name="N"/> values using the <paramref name="n"/> grouping definitions.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="N">The number of values to compute the combinations of.</param>
		/// <param name="n">The groups and how many values fall into each group.</param>
		/// <returns>The computed number of combinations.</returns>
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
				result = Division(result, Factorial(n[i]));
				sum = Addition(sum, n[i]);
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
			return Division(Factorial(N), Multiplication(Factorial(n), Factorial(Subtraction(N, n))));
		}

		#endregion

		#region Occurences

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); });

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Equate<T> equate, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, equate, null);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="hash">The hash code delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Hash<T> hash, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, null, hash);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Equate<T> equate, Hash<T> hash, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, equate, hash);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="stepper">The data to count the occurences of.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Stepper<T> stepper, Equate<T> equate = null, Hash<T> hash = null)
		{
			IMap<int, T> map = new MapHashLinked<int, T>(equate, hash);
			stepper(a =>
			{
				if (map.Contains(a))
				{
					map[a]++;
				}
				else
				{
					map[a] = 1;
				}
			});
			return map;
		}

		#endregion

		#region Mode

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Step<T> step, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Step<T> step, Equate<T> equate, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, equate, null);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Step<T> step, Hash<T> hash, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, null, hash);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Step<T> step, Equate<T> equate, Hash<T> hash, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, equate, hash);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The data set.</param>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <returns>The modes of the data set.</returns>
		public static void Mode<T>(Stepper<T> stepper, Step<T> step, Equate<T> equate = null, Hash<T> hash = null)
		{
			int maxOccurences = -1;
			IMap<int, T> map = new MapHashLinked<int, T>(equate, hash);
			stepper(a =>
			{
				if (map.Contains(a))
				{
					int occurences = ++map[a];
					maxOccurences = Math.Max(occurences, maxOccurences);
				}
				else
				{
					map[a] = 1;
					maxOccurences = Math.Max(1, maxOccurences);
				}
			});
			map.Stepper((value, key) =>
			{
				if (value == maxOccurences)
				{
					step(key);
				}
			});
		}

		#endregion

		#region Mean

		/// <summary>Computes the mean of a set of numerical values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first value of the set of data to compute the mean of.</param>
		/// <param name="b">The remaining values in the data set to compute the mean of.</param>
		/// <returns>The computed mean of the set of data.</returns>
		public static T Mean<T>(T a, params T[] b) =>
			Mean<T>(step => { step(a); b.ToStepper()(step); });

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
				i = Addition(i, Constant<T>.One);
				sum = Addition(sum, step);
			});
			if (Equality(i, Constant<T>.Zero))
			{
				throw new ArgumentException("The argument is empty.", nameof(stepper));
			}
			return Division(sum, i);
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
				return Division(Addition(leftMiddle, rightMiddle), Constant<T>.Two);
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
		//internal static void OddNoDupesMedianImplementation<T>(T[] a, int n, ref int k, Compare<T> compare)
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

		//internal static void OddNoDupesMedianImplementation_Split<T>(T[] a, int n, T x, ref int i, ref int j, Compare<T> compare)
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
				count = Addition(count, Constant<T>.One);
				multiple = Multiplication(multiple, i);
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
				T i_minus_mean = Subtraction(i, mean);
				variance = Addition(variance, Multiplication(i_minus_mean, i_minus_mean));
				count = Addition(count, Constant<T>.One);
			});
			return Division(variance, count);
		}

		#endregion

		#region StandardDeviation

		/// <summary>Computes the standard deviation of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the standard deviation of.</param>
		/// <returns>The computed standard deviation of the set of numbers.</returns>
		public static T StandardDeviation<T>(Stepper<T> stepper) =>
			SquareRoot(Variance(stepper));

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
				temp = Addition(temp, AbsoluteValue(Subtraction(i, mean)));
				count = Addition(count, Constant<T>.One);
			});
			return Division(temp, count);
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
		public static void Range<T>(out T minimum, out T maximum, Stepper<T> stepper) =>
			Range(stepper, out minimum, out maximum);

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
			// Note: can't use out parameters as capture variables
			T min = default;
			T max = default;
			bool assigned = false;
			stepper(a =>
			{
				if (assigned)
				{
					min = LessThan(a, min) ? a : min;
					max = LessThan(max, a) ? a : max;
				}
				else
				{
					min = a;
					max = a;
					assigned = true;
				}
			});
			if (!assigned)
			{
				throw new ArgumentException("The argument is empty.", nameof(stepper));
			}
			minimum = min;
			maximum = max;
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
			Sort.Quick(Comparison, ordered);
			T[] resultingQuantiles = new T[quantiles + 1];
			resultingQuantiles[0] = ordered[0];
			resultingQuantiles[resultingQuantiles.Length - 1] = ordered[ordered.Length - 1];
			T QUANTILES_PLUS_1 = Convert<int, T>(quantiles + 1);
			T ORDERED_LENGTH = Convert<int, T>(ordered.Length);
			for (int i = 1; i < quantiles; i++)
			{
				T I = Convert<int, T>(i);
				T temp = Division(ORDERED_LENGTH, Multiplication<T>(QUANTILES_PLUS_1, I));
				if (IsInteger(temp))
				{
					resultingQuantiles[i] = ordered[Convert<T, int>(temp)];
				}
				else
				{
					resultingQuantiles[i] = Division(Addition(ordered[Convert<T, int>(temp)], ordered[Convert<T, int>(temp) + 1]), Constant<T>.Two);
				}
			}
			return resultingQuantiles;
		}

		#endregion

		#region Correlation

		//        /// <summary>Computes the median of a set of values.</summary>
		//        internal static Compute.Delegates.Correlation Correlation_internal = (Stepper<T> a, Stepper<T> b) =>
		//        {
		//            throw new System.NotImplementedException("I introduced an error here when I removed the stepref off of structure. will fix soon");

		//            Compute.Correlation_internal =
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

		//            return Compute.Correlation_internal(a, b);
		//        };

		//        public static T Correlation(Stepper<T> a, Stepper<T> b)
		//        {
		//            return Correlation_internal(a, b);
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

		public static T NaturalLogarithm<T>(T a) =>
			NaturalLogarithmImplementation<T>.Function(a);

		/// <summary>Computes (natrual log): [ ln(n) ].</summary>
		internal static class NaturalLogarithmImplementation<T>
		{
			internal static Func<T, T> Function = a =>
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
		/// <param name="a">The angle to compute the sine ratio of.</param>
		/// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
		/// <returns>The taylor series computed sine ratio of the provided angle.</returns>
		public static T SineTaylorSeries<T>(Angle<T> a, Predicate<T> predicate = null)
		{
			// Series: sine(x) = x - (x^3 / 3!) + (x^5 / 5!) - (x^7 / 7!) + (x^9 / 9!) + ...
			// more terms in computation inproves accuracy

			// Note: there is room for optimization (custom runtime compilation)

			T x = a[Angle.Units.Radians];
			T sine = x;
			T previous;
			bool isAddTerm = false;
			T i = Constant<T>.Three;
			T xSquared = Multiplication(x, x);
			T xRunningPower = x;
			T xRunningFactorial = Constant<T>.One;
			do
			{
				xRunningPower = Multiplication(xRunningPower, xSquared);
				xRunningFactorial = Multiplication(xRunningFactorial, Multiplication(i, Subtraction(i, Constant<T>.One)));
				previous = sine;
				if (isAddTerm)
				{
					sine = Addition(sine, Division(xRunningPower, xRunningFactorial));
				}
				else
				{
					sine = Subtraction(sine, Division(xRunningPower, xRunningFactorial));
				}
				isAddTerm = !isAddTerm;
				i = Addition(i, Constant<T>.Two);
			} while (Inequality(sine, previous) && (predicate is null || !predicate(sine)));
			return sine;
		}

		/// <summary>Computes the sine ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the sine ratio of.</param>
		/// <returns>The sine ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T SineSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Sin(c);
			T e = Convert<double, T>(d);
			return e;
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

			T adjusted = Remainder(a[Angle.Units.Radians], Constant<T>.Pi2);
			if (IsNegative(adjusted))
			{
				adjusted = Addition(adjusted, Constant<T>.Pi2);
			}
			if (LessThan(adjusted, Constant<T>.Pi))
			{
				T xMinusPiOver2 = Subtraction(adjusted, Constant<T>.PiOver2);
				T xMinusPiOver2Squared = Multiplication(xMinusPiOver2, xMinusPiOver2);
				return Addition(Multiplication(Constant<T>.Negative4OverPiSquared, xMinusPiOver2Squared), Constant<T>.One);
			}
			else
			{
				T xMinus3PiOver2 = Subtraction(adjusted, Constant<T>.Pi3Over2);
				T xMinus3PiOver2Squared = Multiplication(xMinus3PiOver2, xMinus3PiOver2);
				return Subtraction(Multiplication(Constant<T>.FourOverPiSquared, xMinus3PiOver2Squared), Constant<T>.One);
			}
		}

		#endregion

		#region Cosine

		/// <summary>Computes the cosine ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosine ratio of.</param>
		/// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
		/// <returns>The taylor series computed cosine ratio of the provided angle.</returns>
		public static T CosineTaylorSeries<T>(Angle<T> a, Predicate<T> predicate = null)
		{
			// Series: cosine(x) = 1 - (x^2 / 2!) + (x^4 / 4!) - (x^6 / 6!) + (x^8 / 8!) - ...
			// more terms in computation inproves accuracy

			// Note: there is room for optimization (custom runtime compilation)

			T x = a[Angle.Units.Radians];
			T cosine = Constant<T>.One;
			T previous;
			T xSquared = Multiplication(x, x);
			T xRunningPower = Constant<T>.One;
			T xRunningFactorial = Constant<T>.One;
			bool isAddTerm = false;
			T i = Constant<T>.Two;
			do
			{
				xRunningPower = Multiplication(xRunningPower, xSquared);
				xRunningFactorial = Multiplication(xRunningFactorial, Multiplication(i, Subtraction(i, Constant<T>.One)));
				previous = cosine;
				if (isAddTerm)
				{
					cosine = Addition(cosine, Division(xRunningPower, xRunningFactorial));
				}
				else
				{
					cosine = Subtraction(cosine, Division(xRunningPower, xRunningFactorial));
				}
				isAddTerm = !isAddTerm;
				i = Addition(i, Constant<T>.Two);
			} while (Inequality(cosine, previous) && (predicate is null || !predicate(cosine)));
			return cosine;
		}

		/// <summary>Computes the cosine ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosine ratio of.</param>
		/// <returns>The cosine ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CosineSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Cos(c);
			T e = Convert<double, T>(d);
			return e;
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
		/// <param name="a">The angle to compute the tangent ratio of.</param>
		/// <returns>The taylor series computed tangent ratio of the provided angle.</returns>
		public static T TangentTaylorSeries<T>(Angle<T> a)
		{
			return Division(SineTaylorSeries(a), CosineTaylorSeries(a));
		}

		/// <summary>Computes the tangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the tangent ratio of.</param>
		/// <returns>The tangent ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T TangentSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Tan(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the tangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated tangent ratio of.</param>
		/// <returns>The quadratic estimation of the tangent ratio of the provided angle.</returns>
		public static T TangentQuadratic<T>(Angle<T> a)
		{
			return Division(SineQuadratic(a), CosineQuadratic(a));
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
			return Division(Constant<T>.One, SineSystem(a));
		}

		/// <summary>Estimates the cosecant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cosecant ratio of.</param>
		/// <returns>The quadratic estimation of the cosecant ratio of the provided angle.</returns>
		public static T CosecantQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, SineQuadratic(a));
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
			return Division(Constant<T>.One, CosineSystem(a));
		}

		/// <summary>Estimates the secant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated secant ratio of.</param>
		/// <returns>The quadratic estimation of the secant ratio of the provided angle.</returns>
		public static T SecantQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, CosineQuadratic(a));
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
			return Division(Constant<T>.One, TangentSystem(a));
		}

		/// <summary>Estimates the cotangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cotangent ratio of.</param>
		/// <returns>The quadratic estimation of the cotangent ratio of the provided angle.</returns>
		public static T CotangentQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, TangentQuadratic(a));
		}

		#endregion

		#region InverseSine

		//public static Angle<T> InverseSine<T>(T a)
		//{
		//    return InverseSineImplementation<T>.Function(a);
		//}

		//internal static class InverseSineImplementation<T>
		//{
		//    internal static Func<T, Angle<T>> Function = a =>
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
		//    internal static Func<T, Angle<T>> Function = a =>
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
		//    internal static Func<T, Angle<T>> Function = a =>
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
			T sumx = Constant<T>.Zero;
			T sumy = Constant<T>.Zero;
			points((x, y) =>
			{
				sumx = Addition(sumx, x);
				sumy = Addition(sumy, y);
				count++;
			});
			if (count < 2)
			{
				throw new MathematicsException("Argument Invalid !(" + nameof(points) + ".Count() >= 2)");
			}
			T tcount = Convert<int, T>(count);
			T meanx = Division(sumx, tcount);
			T meany = Division(sumy, tcount);
			T variancex = Constant<T>.Zero;
			T variancey = Constant<T>.Zero;
			points((x, y) =>
			{
				T offset = Subtraction(x, meanx);
				variancey = Addition(variancey, Multiplication(offset, Subtraction(y, meany)));
				variancex = Addition(variancex, Multiplication(offset, offset));
			});
			slope = Division(variancey, variancex);
			y_intercept = Subtraction(meany, Multiplication(slope, meanx));
		}

		#endregion

		#region FactorPrimes

		/// <summary>Factors the primes numbers of a numeric integer value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to factor the prime numbers of.</param>
		/// <param name="step">The action to perform on all found prime factors.</param>
		public static void FactorPrimes<T>(T a, Step<T> step) =>
			FactorPrimesImplementation<T>.Function(a, step);

		internal static class FactorPrimesImplementation<T>
		{
			internal static Action<T, Step<T>> Function = (a, x) =>
			{
				Function = (A, step) =>
				{
					if (!IsInteger(A))
					{
						throw new ArgumentOutOfRangeException(nameof(A), A, "!(" + nameof(A) + "." + nameof(IsInteger) + ")");
					}
					if (IsNegative(A))
					{
						A = AbsoluteValue(A);
						step(Convert<int, T>(-1));
					}
					while (IsEven(A))
					{
						step(Constant<T>.Two);
						A = Division(A, Constant<T>.Two);
					}
					for (T i = Constant<T>.Three; LessThanOrEqual(i, SquareRoot(A)); i = Addition(i, Constant<T>.Two))
					{
						while (Equality(Remainder(A, i), Constant<T>.Zero))
						{
							step(i);
							A = Division(A, i);
						}
					}
					if (GreaterThan(A, Constant<T>.Two))
					{
						step(A);
					}
				};
				Function(a, x);
			};
		}

		#endregion
	}
}
