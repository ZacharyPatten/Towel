using System;
using static Towel.Statics;

namespace Towel.Mathematics
{
	/// <summary>Contains static methods for mathematics syntax.</summary>
	public static class MathematicsSyntax
	{
		#region Summation: Σ

		/// <summary>Adds two values [<paramref name="a"/> + <paramref name="b"/>].</summary>
		/// <typeparam name="TA">The type of the left operand.</typeparam>
		/// <typeparam name="TB">The type of the right operand.</typeparam>
		/// <typeparam name="TC">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/>].</returns>
		public static TC Σ<TA, TB, TC>(TA a, TB b) =>
			Addition<TA, TB, TC>(a, b);

		/// <summary>Adds two values [<paramref name="a"/> + <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/>].</returns>
		public static T Σ<T>(T a, T b) =>
			Addition<T, T, T>(a, b);

		/// <summary>Adds multiple values [<paramref name="a"/> + <paramref name="b"/> + <paramref name="c"/> + ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand of the addition.</param>
		/// <param name="b">The second operand of the addition.</param>
		/// <param name="c">The third operand of the addition.</param>
		/// <param name="d">The remaining operands of the addition.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/> + <paramref name="c"/> + ...].</returns>
		public static T Σ<T>(T a, T b, T c, params T[] d) =>
			Addition<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Adds multiple values [step1 + step2 + step3 + ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper of the values to add.</param>
		/// <returns>The result of the addition [step1 + step2 + step3 + ...].</returns>
		public static T Σ<T>(Action<Action<T>> stepper) =>
			Addition(stepper);

		#endregion

		#region Product: Π

		/// <summary>Multiplies two values [<paramref name="a"/> * <paramref name="b"/>].</summary>
		/// <typeparam name="TA">The type of the left operand.</typeparam>
		/// <typeparam name="TB">The type of the right operand.</typeparam>
		/// <typeparam name="TC">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/>].</returns>
		public static TC Π<TA, TB, TC>(TA a, TB b) =>
			Multiplication<TA, TB, TC>(a, b);

		/// <summary>Multiplies two values [<paramref name="a"/> * <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/>].</returns>
		public static T Π<T>(T a, T b) =>
			Multiplication<T, T, T>(a, b);

		/// <summary>Multiplies multiple values [<paramref name="a"/> * <paramref name="b"/> * <paramref name="c"/> * ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <param name="c">The third operand.</param>
		/// <param name="d">The remaining values.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/> * <paramref name="c"/> * ...].</returns>
		public static T Π<T>(T a, T b, T c, params T[] d) =>
			Multiplication(a, b, c, d);

		/// <summary>Multiplies multiple numeric values [step1 * step2 * step3 * ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the multiplication [step1 * step2 * step3 * ...].</returns>
		public static T Π<T>(Action<Action<T>> stepper) =>
			Multiplication(stepper);

		#endregion
	}
}
