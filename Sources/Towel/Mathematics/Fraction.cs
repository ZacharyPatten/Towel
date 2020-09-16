using System;
using static Towel.Syntax;

namespace Towel.Mathematics
{
	/// <summary>Represents a rational value with a numerator and a denominator.</summary>
	/// <typeparam name="T">The type of the numerator and denominator.</typeparam>
	public struct Fraction<T>
	{
		internal T _numerator;
		internal T _denominator;

		#region Properties

		/// <summary>The numerator of the fraction.</summary>
		public T Numerator
		{
			get => _numerator;
			set => _numerator = value;
		}

		/// <summary>The denominator of the fraction.</summary>
		public T Denominator
		{
			get => _denominator;
			set
			{
				if (Syntax.EqualTo(value, Constant<T>.Zero))
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, "!(" + nameof(value) + " != 0)");
				}
				_denominator = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new fraction [<paramref name="numerator"/> / 1].</summary>
		/// <param name="numerator">The numerator of the fraction.</param>
		public Fraction(T numerator)
		{
			_numerator = numerator;
			_denominator = Constant<T>.One;
		}

		/// <summary>Constructs a new fraction [<paramref name="numerator"/> / <paramref name="deniminator"/>].</summary>
		/// <param name="numerator">The numerator of the fraction.</param>
		/// <param name="deniminator">The denominator of the fraction.</param>
		public Fraction(T numerator, T deniminator)
		{
			ReduceInternal(numerator, deniminator, out T reducedNumerator, out T reducedDenominator);
			_numerator = reducedNumerator;
			_denominator = reducedDenominator;
		}

		#endregion

		#region Cast

		/// <summary>Implicitly converts a value into a fraction.</summary>
		/// <param name="value">The value to convert to a fraction.</param>
		public static implicit operator Fraction<T>(T value) =>
			new Fraction<T>(value);

		#endregion

		#region Negation

		/// <summary>Negates a value.</summary>
		/// <param name="a">The fraction to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Fraction<T> operator -(Fraction<T> a) => Fraction<T>.Negate(a);

		/// <summary>Negates a value.</summary>
		/// <param name="a">The value to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Fraction<T> Negate(Fraction<T> a) =>
			new Fraction<T>(Negation(a.Numerator), a.Denominator);

		#endregion

		#region Addition

		/// <summary>Adds two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition.</returns>
		public static Fraction<T> operator +(Fraction<T> a, Fraction<T> b) => Fraction<T>.Add(a, b);

		/// <summary>Adds two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition.</returns>
		public static Fraction<T> Add(Fraction<T> a, Fraction<T> b)
		{
			T c = Multiplication(a.Numerator, b.Denominator);
			T d = Multiplication(b.Numerator, a.Denominator);
			T e = Addition(c, d);
			T f = Multiplication(a.Denominator, b.Denominator);
			return new Fraction<T>(e, f);
		}

		#endregion

		#region Subtraction

		/// <summary>Subtracts two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fraction<T> operator -(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.Subtract(a, b);

		/// <summary>Subtracts two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fraction<T> Subtract(Fraction<T> a, Fraction<T> b)
		{
			T c = Multiplication(a.Numerator, b.Denominator);
			T d = Multiplication(b.Numerator, a.Denominator);
			T e = Subtraction(c, d);
			T f = Multiplication(a.Denominator, b.Denominator);
			return new Fraction<T>(e, f);
		}

		#endregion

		#region Multiplication

		/// <summary>Multiplies two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fraction<T> operator *(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.Multiply(a, b);

		/// <summary>Multiplies two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fraction<T> Multiply(Fraction<T> a, Fraction<T> b) =>
			new Fraction<T>(
				Multiplication(a.Numerator, b.Numerator),
				Multiplication(a.Denominator, b.Denominator));

		#endregion

		#region Division

		/// <summary>Divides two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division.</returns>
		public static Fraction<T> operator /(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.Divide(a, b);

		/// <summary>Divides two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division.</returns>
		public static Fraction<T> Divide(Fraction<T> a, Fraction<T> b) =>
			new Fraction<T>(
				Multiplication(a.Numerator, b.Denominator),
				Multiplication(a.Denominator, b.Numerator));

		#endregion

		#region Remainder

		/// <summary>Computes the remainder of two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the remainder operation.</returns>
		public static Fraction<T> operator %(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.Remainder(a, b);

		/// <summary>Computes the remainder of two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the remainder operation.</returns>
		public static Fraction<T> Remainder(Fraction<T> a, Fraction<T> b)
		{
			while (a > b)
			{
				a -= b;
			}
			return a;
		}

		/// <summary>Computes the remainder of two values.</summary>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the remainder operation.</returns>
		public Fraction<T> Remainder(Fraction<T> b) =>
			Fraction<T>.Remainder(this, b);

		#endregion

		#region Reduce

		internal static void ReduceInternal(T a, T b, out T c, out T d)
		{
			if (Syntax.EqualTo(a, Constant<T>.Zero))
			{
				c = a;
				d = Constant<T>.One;
				return;
			}
			T gcf = GreatestCommonFactor(a, b);
			c = Division(a, gcf);
			d = Division(b, gcf);
			if (IsNegative(b))
			{
				c = Multiplication(c, Constant<T>.NegativeOne);
				d = Multiplication(d, Constant<T>.NegativeOne);
			}
		}

		/// <summary>Reduces a fractional value if possible.</summary>
		/// <param name="a">The fractional value to reduce.</param>
		public static Fraction<T> Reduce(Fraction<T> a)
		{
			ReduceInternal(a.Numerator, a.Denominator, out T numerator, out T denominator);
			return new Fraction<T>(numerator, denominator);
		}

		/// <summary>Reduces a fractional value if possible.</summary>
		public Fraction<T> Reduce() =>
			Fraction<T>.Reduce(this);

		#endregion

		#region Equality

		/// <summary>Checks for equality between two values.</summary>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator ==(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.Equality(a, b);

		/// <summary>Checks for equality between two values.</summary>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool Equality(Fraction<T> a, Fraction<T> b) =>
			EqualTo(a._numerator, b._numerator) &&
			EqualTo(a._denominator, b._denominator);

		/// <summary>Checks for equality between two values.</summary>
		/// <param name="b">The second operand.</param>
		/// <returns>The result of the equality check.</returns>
		public bool Equality(Fraction<T> b) =>
			Fraction<T>.Equality(this, b);

		/// <summary>Checks for equality with another object.</summary>
		/// <param name="obj">The object to equate with this.</param>
		/// <returns>The result of the equate.</returns>
		public override bool Equals(object obj) => 
			obj is Fraction<T> fraction && Fraction<T>.Equality(this, fraction);

		#endregion

		#region Inequality

		/// <summary>Checks for inequality between two values.</summary>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <returns>The result of the inequality check.</returns>
		public static bool operator !=(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.NotEqual(a, b);

		/// <summary>Checks for inequality between two values.</summary>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <returns>The result of the inequality check.</returns>
		public static bool NotEqual(Fraction<T> a, Fraction<T> b) =>
			InequalTo(a._numerator, b._numerator) ||
			InequalTo(a._denominator, b._denominator);

		/// <summary>Checks for inequality between two values.</summary>
		/// <param name="b">The second operand.</param>
		/// <returns>The result of the inequality check.</returns>
		public bool NotEqual(Fraction<T> b) =>
			Fraction<T>.NotEqual(this, b);

		#endregion

		#region LessThan

		/// <summary>Determines if one value is less than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the less than operation.</returns>
		public static bool operator <(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.LessThan(a, b);

		/// <summary>Determines if one value is less than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the less than operation.</returns>
		public static bool LessThan(Fraction<T> a, Fraction<T> b)
		{
			T c = Multiplication(a.Numerator, b.Denominator);
			T d = Multiplication(b.Numerator, a.Denominator);
			return Syntax.LessThan(c, d);
		}

		/// <summary>Determines if one value is less than another.</summary>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the less than operation.</returns>
		public bool LessThan(Fraction<T> b) =>
			Fraction<T>.LessThan(this, b);

		#endregion

		#region GreaterThan

		/// <summary>Determines if one value is greater than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the greater than operation.</returns>
		public static bool operator >(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.GreaterThan(a, b);

		/// <summary>Determines if one value is greater than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the greater than operation.</returns>
		public static bool GreaterThan(Fraction<T> a, Fraction<T> b)
		{
			T c = Multiplication(a.Numerator, b.Denominator);
			T d = Multiplication(b.Numerator, a.Denominator);
			return Syntax.GreaterThan(c, d);
		}

		/// <summary>Determines if one value is greater than another.</summary>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the greater than operation.</returns>
		public bool GreaterThan(Fraction<T> b) =>
			Fraction<T>.GreaterThan(this, b);

		#endregion

		#region LessThanOrEqual

		/// <summary>Determines if one value is less than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the less than operation.</returns>
		public static bool operator <=(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.LessThanOrEqual(a, b);

		/// <summary>Determines if one value is less than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the less than operation.</returns>
		public static bool LessThanOrEqual(Fraction<T> a, Fraction<T> b)
		{
			T c = Multiplication(a.Numerator, b.Denominator);
			T d = Multiplication(b.Numerator, a.Denominator);
			return Syntax.LessThanOrEqual(c, d);
		}

		/// <summary>Determines if one value is less than another.</summary>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the less than operation.</returns>
		public bool LessThanOrEqual(Fraction<T> b) =>
			Fraction<T>.LessThanOrEqual(this, b);

		#endregion

		#region GreaterThanOrEqual

		/// <summary>Determines if one value is greater than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the greater than operation.</returns>
		public static bool operator >=(Fraction<T> a, Fraction<T> b) =>
			Fraction<T>.GreaterThanOrEqual(a, b);

		/// <summary>Determines if one value is greater than another.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the greater than operation.</returns>
		public static bool GreaterThanOrEqual(Fraction<T> a, Fraction<T> b)
		{
			T c = Multiplication(a.Numerator, b.Denominator);
			T d = Multiplication(b.Numerator, a.Denominator);
			return Syntax.GreaterThanOrEqual(c, d);
		}

		/// <summary>Determines if one value is greater than another.</summary>
		/// <param name="b">The right operand.</param>
		/// <returns>The value of the greater than operation.</returns>
		public bool GreaterThanOrEqual(Fraction<T> b) =>
			Fraction<T>.GreaterThanOrEqual(this, b);

		#endregion

		#region Compare

		/// <summary>Compares two values.</summary>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the comparison.</returns>
		public CompareResult Compare(Fraction<T> b) =>
			Fraction<T>.Compare(this, b);

		/// <summary>Compares two values.</summary>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the comparison.</returns>
		public static CompareResult Compare(Fraction<T> a, Fraction<T> b) =>
			a < b ? Less :
			a > b ? Greater :
			Equal;

		#endregion

		#region Hash

		/// <summary>Gets the default hash code for this instance.</summary>
		/// <returns>Teh computed hash code.</returns>
		public override int GetHashCode()
		{
			return (int)((Numerator.GetHashCode() ^ Denominator.GetHashCode()) & 0xFFFFFFFF);
		}

		#endregion

		#region TryParse + Parse

		/// <summary>Tries to parse a string into a fraction.</summary>
		/// <param name="string">The string to parse.</param>
		/// <param name="tryParse">The function to parse the numerator and denominator.</param>
		/// <param name="fraction">The parsed value if succeeded.</param>
		/// <param name="exception">The exception that occurred if failed.</param>
		/// <returns>True if the parse succeeded or false if not.</returns>
		public bool TryParse(string @string, TryParse<T> tryParse, out Fraction<T> fraction, out Exception exception)
		{
			if (!(tryParse is null) && tryParse(@string, out T value))
			{
				fraction = new Fraction<T>(value);
			}
			if (@string.Contains("/"))
			{
				int divideIndex = @string.IndexOf("/");
				string numeratorString = @string.Substring(0, divideIndex - 1);
				string denominatorString = @string.Substring(divideIndex + 1);

				// TODO: refactor this code so the try-catch is not necessary.

				try
				{
					if (!tryParse(numeratorString, out T numerator))
					{
						numerator = Symbolics.ParseAndSimplifyToConstant<T>(numeratorString);
					}
					if (!tryParse(denominatorString, out T denominator))
					{
						denominator = Symbolics.ParseAndSimplifyToConstant<T>(denominatorString);
					}
					fraction = new Fraction<T>(numerator, denominator);
					exception = null;
					return true;
				}
				catch { }
			}

			exception = new Exception("The value could not be parsed.");
			fraction = default;
			return false;
		}

		/// <summary>Tries to parse a string into a fraction.</summary>
		/// <param name="string">The string to parse.</param>
		/// <param name="tryParse">The function to parse the numerator and denominator.</param>
		/// <param name="fraction">The parsed value if succeeded.</param>
		/// <returns>True if the parse succeeded or false if not.</returns>
		public bool TryParse(string @string, TryParse<T> tryParse, out Fraction<T> fraction)
		{
			return TryParse(@string, tryParse, out fraction, out _);
		}

		/// <summary>Parses a string into a fraction.</summary>
		/// <param name="string">The string to parse.</param>
		/// <param name="tryParse">The function to parse the numerator and denominator.</param>
		/// <returns>The parsed value from the string.</returns>
		public Fraction<T> Parse(string @string, TryParse<T> tryParse)
		{
			TryParse(@string, tryParse, out Fraction<T> fraction);
			return fraction;
		}

		/// <summary>Tries to parse a string into a fraction.</summary>
		/// <param name="string">The string to parse.</param>
		/// <param name="fraction">The parsed value if succeeded.</param>
		/// <param name="exception">The exception that occurred if failed.</param>
		/// <returns>True if the parse succeeded or false if not.</returns>
		public bool TryParse(string @string, out Fraction<T> fraction, out Exception exception)
		{
			return TryParse(@string, null, out fraction, out exception);
		}

		/// <summary>Tries to parse a string into a fraction.</summary>
		/// <param name="string">The string to parse.</param>
		/// <param name="fraction">The parsed value if succeeded.</param>
		/// <returns>True if the parse succeeded or false if not.</returns>
		public bool TryParse(string @string, out Fraction<T> fraction)
		{
			return TryParse(@string, out fraction, out _);
		}

		/// <summary>Parses a string into a fraction.</summary>
		/// <param name="string">The string to parse.</param>
		/// <returns>The parsed value from the string.</returns>
		public Fraction<T> Parse(string @string)
		{
			TryParse(@string, out Fraction<T> fraction);
			return fraction;
		}

		#endregion

		#region ToString

		/// <summary>Default conversion to string for fractions.</summary>
		/// <param name="fraction">The value to convert.</param>
		/// <param name="toString">The string conversion function for the numerator and denominator.</param>
		/// <returns>The value represented as a string.</returns>
		public static string ToString(Fraction<T> fraction, Func<T, string> toString)
		{
			if (EqualTo(fraction._denominator, Constant<T>.One))
			{
				return toString(fraction._numerator);
			}
			return toString(fraction._numerator) + "/" + toString(fraction._denominator);
		}

		/// <summary>Default conversion to string for fractions.</summary>
		/// <returns>The value represented as a string.</returns>
		public override string ToString() =>
			ToString(this, value => value.ToString());

		#endregion
	}
}
