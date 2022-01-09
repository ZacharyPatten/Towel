using System.Linq.Expressions;

#pragma warning disable IDE1006 // Naming Styles

namespace Towel;

/// <summary>Contains generic static numerical constant values.</summary>
/// <typeparam name="T">The generic numeric type of the constants.</typeparam>
public static class Constant<T>
{
	internal static bool _zero_assigned;
	internal static bool _one_assigned;
	internal static bool _two_assigned;
	internal static bool _three_assigned;
	internal static bool _four_assigned;
	internal static bool _ten_assigned;
	internal static bool _negativeOne_assigned;
	internal static bool _pi_assigned;
	internal static bool _pi2_assigned;
	internal static bool _piOver2_assigned;
	internal static bool _pi3Over2_assigned;
	internal static bool _fourOverPiSquared_assigned;
	internal static bool _fourOverπSquared_assigned;
	internal static bool _negative4OverPiSquared_assigned;
	internal static bool _negative4OverπSquared_assigned;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	internal static T _zero;
	internal static T _one;
	internal static T _two;
	internal static T _three;
	internal static T _four;
	internal static T _ten;
	internal static T _negativeOne;
	internal static T _pi;
	internal static T _pi2;
	internal static T _piOver2;
	internal static T _pi3Over2;
	internal static T _fourOverPiSquared;
	internal static T _fourOverπSquared;
	internal static T _negative4OverPiSquared;
	internal static T _negative4OverπSquared;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	/// <summary>Zero [0]</summary>
	public static T Zero
	{
		get
		{
			if (!_zero_assigned)
			{
				_zero = Convert<int, T>(0);
				_zero_assigned = true;
			}
			return _zero;
		}
		set
		{
			_zero = value;
			_zero_assigned = true;
		}
	}

	/// <summary>One [1]</summary>
	public static T One
	{
		get
		{
			if (!_one_assigned)
			{
				_one = Convert<int, T>(1);
				_one_assigned = true;
			}
			return _one;
		}
		set
		{
			_one = value;
			_one_assigned = true;
		}
	}

	/// <summary>Two [2]</summary>
	public static T Two
	{
		get
		{
			if (!_two_assigned)
			{
				_two = Convert<int, T>(2);
				_two_assigned = true;
			}
			return _two;
		}
		set
		{
			_two = value;
			_two_assigned = true;
		}
	}

	/// <summary>Three [3]</summary>
	public static T Three
	{
		get
		{
			if (!_three_assigned)
			{
				_three = Convert<int, T>(3);
				_three_assigned = true;
			}
			return _three;
		}
		set
		{
			_three = value;
			_three_assigned = true;
		}
	}

	/// <summary>Four [4]</summary>
	public static T Four
	{
		get
		{
			if (!_four_assigned)
			{
				_four = Convert<int, T>(4);
				_four_assigned = true;
			}
			return _four;
		}
		set
		{
			_four = value;
			_four_assigned = true;
		}
	}

	/// <summary>Ten [10]</summary>
	public static T Ten
	{
		get
		{
			if (!_ten_assigned)
			{
				_ten = Convert<int, T>(10);
				_ten_assigned = true;
			}
			return _ten;
		}
		set
		{
			_ten = value;
			_ten_assigned = true;
		}
	}

	/// <summary>Negative One [-1]</summary>
	public static T NegativeOne
	{
		get
		{
			if (!_negativeOne_assigned)
			{
				_negativeOne = Convert<int, T>(-1);
				_negativeOne_assigned = true;
			}
			return _negativeOne;
		}
		set
		{
			_negativeOne = value;
			_negativeOne_assigned = true;
		}
	}

	/// <summary>π [3.14...]</summary>
	public static T Pi
	{
		get
		{
			if (!_pi_assigned)
			{
				_pi = ComputePi();
				_pi_assigned = true;
			}
			return _pi;
		}
		set
		{
			_pi = value;
			_pi_assigned = true;
		}
	}

	/// <summary>π [3.14...]</summary>
	public static T π
	{
		get
		{
			if (!_pi_assigned)
			{
				_pi = ComputePi();
				_pi_assigned = true;
			}
			return _pi;
		}
		set
		{
			_pi = value;
			_pi_assigned = true;
		}
	}

	/// <summary>2π [6.28...]</summary>
	public static T Pi2
	{
		get
		{
			if (!_pi2_assigned)
			{
				_pi2 = Multiplication(Two, Pi);
				_pi2_assigned = true;
			}
			return _pi2;
		}
		set
		{
			_pi2 = value;
			_pi2_assigned = true;
		}
	}

	/// <summary>2π [6.28...]</summary>
	public static T π2
	{
		get
		{
			if (!_pi2_assigned)
			{
				_pi2 = Multiplication(Two, Pi);
				_pi2_assigned = true;
			}
			return _pi2;
		}
		set
		{
			_pi2 = value;
			_pi2_assigned = true;
		}
	}

	/// <summary>π / 2</summary>
	public static T PiOver2
	{
		get
		{
			if (!_piOver2_assigned)
			{
				_piOver2 = Division(Pi, Two);
				_piOver2_assigned = true;
			}
			return _piOver2;
		}
		set
		{
			_piOver2 = value;
			_piOver2_assigned = true;
		}
	}

	/// <summary>π / 2</summary>
	public static T πOver2
	{
		get
		{
			if (!_piOver2_assigned)
			{
				_piOver2 = Division(Pi, Two);
				_piOver2_assigned = true;
			}
			return _piOver2;
		}
		set
		{
			_piOver2 = value;
			_piOver2_assigned = true;
		}
	}

	/// <summary>3π/2</summary>
	public static T Pi3Over2
	{
		get
		{
			if (!_pi3Over2_assigned)
			{
				_pi3Over2 = Division(Multiplication(Three, Pi), Two);
				_pi3Over2_assigned = true;
			}
			return _pi3Over2;
		}
		set
		{
			_pi3Over2 = value;
			_pi3Over2_assigned = true;
		}
	}

	/// <summary>3π/2</summary>
	public static T π3Over2
	{
		get
		{
			if (!_pi3Over2_assigned)
			{
				_pi3Over2 = Division(Multiplication(Three, Pi), Two);
				_pi3Over2_assigned = true;
			}
			return _pi3Over2;
		}
		set
		{
			_pi3Over2 = value;
			_pi3Over2_assigned = true;
		}
	}

	/// <summary>4/(π^2)</summary>
	public static T FourOverPiSquared
	{
		get
		{
			if (!_fourOverPiSquared_assigned)
			{
				_fourOverPiSquared = Division(Multiplication(Three, Pi), Two);
				_fourOverPiSquared_assigned = true;
			}
			return _fourOverPiSquared;
		}
		set
		{
			_fourOverPiSquared = value;
			_fourOverPiSquared_assigned = true;
		}
	}

	/// <summary>4/(π^2)</summary>
	public static T FourOverπSquared
	{
		get
		{
			if (!_fourOverPiSquared_assigned)
			{
				_fourOverPiSquared = Division(Multiplication(Three, Pi), Two);
				_fourOverPiSquared_assigned = true;
			}
			return _fourOverPiSquared;
		}
		set
		{
			_fourOverPiSquared = value;
			_fourOverPiSquared_assigned = true;
		}
	}

	/// <summary>-4/(π^2)</summary>
	public static T Negative4OverPiSquared
	{
		get
		{
			if (!_negative4OverPiSquared_assigned)
			{
				_negative4OverPiSquared = Negation(FourOverPiSquared);
				_negative4OverPiSquared_assigned = true;
			}
			return _negative4OverPiSquared;
		}
		set
		{
			_negative4OverPiSquared = value;
			_negative4OverPiSquared_assigned = true;
		}
	}

	/// <summary>-4/(π^2)</summary>
	public static T Negative4OverπSquared
	{
		get
		{
			if (!_negative4OverPiSquared_assigned)
			{
				_negative4OverPiSquared = Negation(FourOverPiSquared);
				_negative4OverPiSquared_assigned = true;
			}
			return _negative4OverPiSquared;
		}
		set
		{
			_negative4OverPiSquared = value;
			_negative4OverPiSquared_assigned = true;
		}
	}

	#region Pi

	/// <summary>Computes the value of pi for the provided generic type.</summary>
	/// <param name="predicate">The cancellation token for cutting off computation.</param>
	/// <returns>The computed value of pi.</returns>
	public static T ComputePi(Predicate<T>? predicate = null)
	{
		// Series: PI = 2 * (1 + 1/3 * (1 + 2/5 * (1 + 3/7 * (...))))
		// more terms in computation inproves accuracy

		if (predicate is null)
		{
			int iterations = 0;
			predicate = PI => ++iterations < 100;
		}

		T pi = Constant<T>.One;
		T previous = Constant<T>.Zero;
		for (int i = 1; Inequate(previous, pi) && predicate(pi); i++)
		{
			previous = pi;
			pi = Constant<T>.One;
			for (int j = i; j >= 1; j--)
			{
				#region Without Custom Runtime Compilation

				// T J = FromInt32<T>(j);
				// T a = Add(Multiply(Constant<T>.Two, J), Constant<T>.One);
				// T b = Divide(J, a);
				// T c = Multiply(b, pi);
				// T d = Add(Constant<T>.One, c);
				// pi = d;

				#endregion

				pi = AddMultiplyDivideAddImplementation.Function(Convert<int, T>(j), pi);
			}
			pi = Multiplication(Constant<T>.Two, pi);
		}
		pi = MaximumValue(pi, Constant<T>.Three);
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

#pragma warning restore IDE1006 // Naming Styles
