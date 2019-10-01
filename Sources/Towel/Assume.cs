using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Towel
{
	/// <summary>Breaks type safeness by making assumptions about types.</summary>
	public static class Assume
	{
		#region TryParse<T>

		/// <summary>Assumes a TryParse method exists for a generic type and calls it.</summary>
		/// <typeparam name="T">The generic type to make assumptions about.</typeparam>
		/// <param name="string">The string to be parsed.</param>
		/// <param name="Default">The default value if the parse fails.</param>
		/// <returns>The parsed value or the default value if the parse fails.</returns>
		public static T TryParse<T>(string @string, T Default = default) =>
			TryParse(@string, out T value)
			? value
			: Default;

		/// <summary>Assumes a TryParse method exists for a generic type and calls it.</summary>
		/// <typeparam name="T">The generic type to make assumptions about.</typeparam>
		/// <param name="string">The string to be parsed.</param>
		/// <param name="value">The parsed value if successful or default if not.</param>
		/// <returns>True if successful or false if not.</returns>
		public static bool TryParse<T>(string @string, out T value) =>
			TryParseImplementation<T>.Function(@string, out value);

		internal static class TryParseImplementation<T>
		{
			internal delegate bool TryParseDelegate(string @string, out T value);

			internal static TryParseDelegate Function = (string @string, out T value) =>
				{
					Type type = typeof(T);
					Type[] parameterTypes = new Type[] { typeof(string), type.MakeByRefType() };
					MethodInfo methodInfo =
						type.GetMethod("TryParse",
							BindingFlags.Static |
							BindingFlags.Public |
							BindingFlags.NonPublic,
							null,
							parameterTypes,
							null);
					Function =
						methodInfo is null
						?
						(string _string, out T _value) =>
						{
							_value = default;
							return false;
						}
						:
						(TryParseDelegate)methodInfo.CreateDelegate(typeof(TryParseDelegate));
					return Function(@string, out value);
				};
		}

		#endregion

		#region Convert<A, B>

		/// <summary>Assumes a conversion exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the conversion.</returns>
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

		#region Negation<A, B>

		/// <summary>Assumes the addition operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition.</returns>
		public static B Negation<A, B>(A a) =>
			NegationImplementation<A, B>.Function(a);

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

		#region Addition<A, B, C>

		/// <summary>Assumes the addition operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition.</returns>
		public static C Addition<A, B, C>(A a, B b) =>
			AdditionImplementation<A, B, C>.Function(a, b);

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

		#region Subtraction<A, B, C>

		/// <summary>Assumes the subtraction operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction.</returns>
		public static C Subtraction<A, B, C>(A a, B b) =>
			SubtractionImplementation<A, B, C>.Function(a, b);

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

		#region Multiplication<A, B, C>

		/// <summary>Assumes the multiplication operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication.</returns>
		public static C Multiplication<A, B, C>(A a, B b) =>
			MultiplicationImplementation<A, B, C>.Function(a, b);

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

		#region Division<A, B, C>

		/// <summary>Assumes the division operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division.</returns>
		public static C Division<A, B, C>(A a, B b) =>
			DivisionImplementation<A, B, C>.Function(a, b);

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

		#region Remainder<A, B, C>

		/// <summary>Assumes the remainder operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the remainder operation.</returns>
		public static C Remainder<A, B, C>(A a, B b) =>
			RemainderImplementation<A, B, C>.Function(a, b);

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

		#region Equality<A, B, C>

		/// <summary>Assumes the equality operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the equality.</returns>
		public static C Equality<A, B, C>(A a, B b) =>
			EqualityImplementation<A, B, C>.Function(a, b);

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

		#region Inequality<A, B, C>

		/// <summary>Assumes the inequality operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the inequality.</returns>
		public static C Inequality<A, B, C>(A a, B b) =>
			InequalityImplementation<A, B, C>.Function(a, b);

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

		#region LessThan<A, B, C>

		/// <summary>Assumes the less than operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the less than operation.</returns>
		public static C LessThan<A, B, C>(A a, B b) =>
			LessThanImplementation<A, B, C>.Function(a, b);

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

		#region GreaterThan<A, B, C>

		/// <summary>Assumes the greater than operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the greater than operation.</returns>
		public static C GreaterThan<A, B, C>(A a, B b) =>
			GreaterThanImplementation<A, B, C>.Function(a, b);

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

		#region LessThanOrEqual<A, B, C>

		/// <summary>Assumes the less than or equal to operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the less than or equal to operation.</returns>
		public static C LessThanOrEqual<A, B, C>(A a, B b) =>
			LessThanOrEqualImplementation<A, B, C>.Function(a, b);

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

		#region GreaterThanOrEqual<A, B, C>

		/// <summary>Assumes the greater than or equal to operator exists between types.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the greater than or equal to operation.</returns>
		public static C GreaterThanOrEqual<A, B, C>(A a, B b) =>
			GreaterThanOrEqualImplementation<A, B, C>.Function(a, b);

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
	}
}
