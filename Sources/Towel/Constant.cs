using System;
using System.Linq.Expressions;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains generic static numerical constant values.</summary>
	/// <typeparam name="T">The generic numeric type of the constants.</typeparam>
	public static class Constant<T>
	{
		/// <summary>Zero [0]</summary>
		public static readonly T Zero = Convert<int, T>(0);
		/// <summary>One [1]</summary>
		public static readonly T One = Convert<int, T>(1);
		/// <summary>Two [2]</summary>
		public static readonly T Two = Convert<int, T>(2);
		/// <summary>Three [3]</summary>
		public static readonly T Three = Convert<int, T>(3);
		/// <summary>Four [4]</summary>
		public static readonly T Four = Convert<int, T>(4);
		/// <summary>Ten [10]</summary>
		public static readonly T Ten = Convert<int, T>(10);
		/// <summary>Negative One [-1]</summary>
		public static readonly T NegativeOne = Convert<int, T>(-1);

		/// <summary>π [3.14...]</summary>
		public static readonly T Pi = ComputePi();
		/// <summary>π [3.14...]</summary>
		public static readonly T π = Pi;
		/// <summary>2π [6.28...]</summary>
		public static readonly T Pi2 = Multiplication(Two, Pi);
		/// <summary>2π [6.28...]</summary>
		public static readonly T π2 = Pi2;
		/// <summary>π / 2</summary>
		public static readonly T PiOver2 = Division(Pi, Two);
		/// <summary>π / 2</summary>
		public static readonly T πOver2 = PiOver2;
		/// <summary>3π/2</summary>
		public static readonly T Pi3Over2 = Division(Multiplication(Three, Pi), Two);
		/// <summary>3π/2</summary>
		public static readonly T π3Over2 = Pi3Over2;
		/// <summary>4/(π^2)</summary>
		public static readonly T FourOverPiSquared = Division(Multiplication(Three, Pi), Two);
		/// <summary>4/(π^2)</summary>
		public static readonly T FourOverπSquared = FourOverPiSquared;
		/// <summary>-4/(π^2)</summary>
		public static readonly T Negative4OverPiSquared = Negation(FourOverPiSquared);
		/// <summary>-4/(π^2)</summary>
		public static readonly T Negative4OverπSquared = Negative4OverPiSquared;

		#region Pi

		/// <summary>Computes the value of pi for the provided generic type.</summary>
		/// <param name="predicate">The cancellation token for cutting off computation.</param>
		/// <returns>The computed value of pi.</returns>
		public static T ComputePi(Predicate<T> predicate = null)
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
			for (int i = 1; InequalTo(previous, pi) && predicate(pi); i++)
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

					pi = AddMultiplyDivideAddImplementation.Function(Convert<int, T>(j), pi);
				}
				pi = Multiplication(Constant<T>.Two, pi);
			}
			pi = Maximum(pi, Constant<T>.Three);
			return pi;
		}

		internal static class AddMultiplyDivideAddImplementation
		{
			internal static Func<T, T, T> Function = (j, pi) =>
			{
				ParameterExpression J = Expression.Parameter(typeof(T));
				ParameterExpression PI = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Add(
					Expression.Constant(One),
					Expression.Multiply(
						PI,
						Expression.Divide(
							J,
							Expression.Add(
								Expression.Multiply(
									Expression.Constant(Two),
									J),
								Expression.Constant(One)))));
				Function = Expression.Lambda<Func<T, T, T>>(BODY, J, PI).Compile();
				return Function(j, pi);
			};
		}

		#endregion

		#region Golden Ratio
#if false

		/// <summary>GoldenRatio [(1 + SquareRoot(5)) / 2]</summary>
		//public static readonly T GoldenRatio = Symbolics.ParseAndSimplifyToConstant<T>("(1 + SquareRoot(5)) / 2");

		/// <summary>Epsilon (1.192092896...e-012f)</summary>
		//public static readonly T Epsilon = Compute.ComputeEpsilon<T>();

#endif
		#endregion

		#region Epsilon
#if false
		// Note sure if this method will be necessary.

		//internal static T ComputeEpsilon<T>()
		//{
		//    if (typeof(T) == typeof(float))
		//    {
		//        return (T)(object)float.Epsilon;
		//    }
		//    
		//}
#endif
		#endregion
	}
}
