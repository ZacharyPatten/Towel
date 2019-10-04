using System;
using static Towel.Syntax;

namespace Towel.Mathematics
{
	/// <summary>A fraction represented as two shorts (numerator / denomnator).</summary>
	/// <citation>
	/// This fraction imlpementation was originally developed by 
	/// Syed Mehroz Alam and posted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition shorto the Towel framework.
	/// http://www.codeproject.com/Articles/9078/Fraction-class-in-C
	/// 
	/// Original Author:
	/// Author: Syed Mehroz Alam
	/// Email: smehrozalam@yahoo.com
	/// URL: Programming Home http://www.geocities.com/smehrozalam/
	/// Date: 6/15/2004
	/// Time: 10:54 AM
	/// </citation>
	[Serializable]
	public struct Fraction32
	{
		internal short _numerator;
		internal short _denominator;

		/// <summary>The maximum Fraction32 value.</summary>
		public static readonly Fraction32 MaxValue = new Fraction32(short.MaxValue, 1);
		/// <summary>The minimum Fraction32 value.</summary>
		public static readonly Fraction32 MinValue = new Fraction32(short.MinValue, 1);

		#region Properties

		/// <summary>The numerator of the fraction.</summary>
		public short Numerator
		{
			get
			{
				return this._numerator;
			}
			set
			{
				this._numerator = value;
			}
		}

		/// <summary>The denominator of the fraction.</summary>
		public short Denominator
		{
			get
			{
				return this._denominator;
			}
			set
			{
				if (value == 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, "!(" + nameof(value) + " != 0)");
				}
				this._denominator = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a fraction from an short.</summary>
		/// <param name="short">The short to represent as a fraction.</param>
		public Fraction32(short @short)
		{
			this._numerator = @short;
			this._denominator = 1;
		}

		/// <summary>Constructs a fracion from a double.</summary>
		/// <param name="rational">The double to represent as a fraction.</param>
		public Fraction32(double rational)
		{
			Rounded:
			if (short.MinValue > rational || rational > short.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(rational), rational, "!(short." + nameof(short.MinValue) + " <= " + nameof(rational) + " <= short." + nameof(short.MaxValue) + ")");
			}
			if (rational % 1 == 0)
			{
				this._numerator = (short)rational;
				this._denominator = 1;
				return;
			}
			else
			{
				try
				{
					checked
					{
						double temp_rational = rational;
						short multiple = 1;
						string temp_string = rational.ToString();
						while (temp_string.IndexOf("E") > 0)
						{
							temp_rational *= 10;
							multiple *= 10;
							temp_string = temp_rational.ToString();
						}
						int i = 0;
						while (temp_string[i] != '.')
							i++;
						int digitsAfterDecimal = temp_string.Length - i - 1;
						while (digitsAfterDecimal > 0)
						{
							temp_rational *= 10;
							multiple *= 10;
							digitsAfterDecimal--;
						}
						this._numerator = (short)Math.Round(temp_rational);
						this._denominator = multiple;
						Reduce(this);
					}
				}
				catch
				{
					rational = Math.Round(rational, 2);
					goto Rounded;
				}
			}
		}

		/// <summary>Makes a fraction given an shorteger and a denominator.</summary>
		/// <param name="numerator">The nmerator of the fraction.</param>
		/// <param name="deniminator">The denominator fo the fraction.</param>
		public Fraction32(short numerator, short deniminator)
		{
			this._numerator = numerator;
			this._denominator = deniminator;
			Reduce(this);
		}

		/// <summary>Creates a fraction by parsing a string.</summary>
		/// <param name="literal">The literal representation fo the fraction.</param>
		public Fraction32(string literal)
		{
			short i;
			for (i = 0; i < literal.Length; i++)
				if (literal[i] == '/')
					break;

			if (i == literal.Length)
			{
				double rational = double.Parse(literal);
				try
				{
					checked
					{
						if (rational % 1 == 0)
						{
							this._numerator = (short)rational;
							this._denominator = 1;
							Reduce(this);
						}
						else
						{
							double temp_rational = rational;
							short multiple = 1;
							string temp_string = rational.ToString();

							if (temp_string.Contains("E"))
							{
								while (temp_string.IndexOf("E") > 0)    // if in the form like 12E-9
								{
									temp_rational *= 10;
									multiple *= 10;
									temp_string = temp_rational.ToString();
								}
							}
							else if (temp_string.Contains("e"))
							{
								while (temp_string.IndexOf("e") > 0)    // if in the form like 12e-9
								{
									temp_rational *= 10;
									multiple *= 10;
									temp_string = temp_rational.ToString();
								}
							}

							int j = 0;
							while (temp_string[j] != '.')
								j++;
							int iDigitsAfterDecimal = temp_string.Length - j - 1;
							while (iDigitsAfterDecimal > 0)
							{
								temp_rational *= 10;
								multiple *= 10;
								iDigitsAfterDecimal--;
							}
							this._numerator = (short)Math.Round(temp_rational);
							this._denominator = multiple;
							Reduce(this);
						}
					}
				}
				catch (OverflowException overflowException)
				{
					throw new OverflowException("Conversion not possible due to overflow", overflowException);
				}
				catch (Exception exception)
				{
					throw new Exception("Conversion not possible", exception);
				}
			}
			else
			{
				this._numerator = short.Parse(literal.Substring(0, i));
				this._denominator = short.Parse(literal.Substring(i + 1));
				Reduce(this);
			}
		}

		#endregion

		#region Operators

		public static Fraction32 operator ++(Fraction32 fraction) { return fraction + 1; }
		/// <summary>Negates a fraction.</summary>
		/// <param name="fraction">The fraction to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Fraction32 operator -(Fraction32 fraction) { return Fraction32.Negate(fraction); }
		/// <summary>Adds two operands together.</summary>
		/// <param name="left">The left operand of the addition.</param>
		/// <param name="right">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Fraction32 operator +(Fraction32 left, Fraction32 right) { return Fraction32.Add(left, right); }
		/// <summary>Subtracts two operands.</summary>
		/// <param name="left">The left operand of the subtraction.</param>
		/// <param name="right">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fraction32 operator -(Fraction32 left, Fraction32 right) { return Fraction32.Add(left, -right); }
		/// <summary>Multiplies two operands together.</summary>
		/// <param name="left">The left operand of the multiplication.</param>
		/// <param name="right">The right operand of the multiplication.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fraction32 operator *(Fraction32 left, Fraction32 right) { return Fraction32.Multiply(left, right); }
		/// <summary>Divides two operands.</summary>
		/// <param name="left">The left operand of the division.</param>
		/// <param name="right">The right operand of the division.</param>
		/// <returns>The result of the division.</returns>
		public static Fraction32 operator /(Fraction32 left, Fraction32 right) { return Fraction32.Multiply(left, Inverse(right)); }
		/// <summary>Checks for equality between two fractions.</summary>
		/// <param name="left">The first operand of the equality check.</param>
		/// <param name="right">The second operand of the equality check.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator ==(Fraction32 left, Fraction32 right) { return Fraction32.Equals(left, right); }
		/// <summary>Checks for equality between two fractions.</summary>
		/// <param name="left">The first operand of the equality check.</param>
		/// <param name="right">The second operand of the equality check.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator !=(Fraction32 left, Fraction32 right) { return !Fraction32.Equals(left, right); }
		/// <summary>Performs a less-than inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator <(Fraction32 left, Fraction32 right) { return left.Numerator * right.Denominator < right.Numerator * left.Denominator; }
		/// <summary>Performs a greater-than inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator >(Fraction32 left, Fraction32 right) { return left.Numerator * right.Denominator > right.Numerator * left.Denominator; }
		/// <summary>Performs a less-than-or-equal inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator <=(Fraction32 left, Fraction32 right) { return left.Numerator * right.Denominator <= right.Numerator * left.Denominator; }
		/// <summary>Performs a greater-than-or-equal inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator >=(Fraction32 left, Fraction32 right) { return left.Numerator * right.Denominator >= right.Numerator * left.Denominator; }
		/// <summary>Converts a double to a fraction. Precision will be lost.</summary>
		/// <param name="rational">The double to convert to a fraction.</param>
		/// <returns>The resulting double of the conversion.</returns>
		public static explicit operator Fraction32(double rational) { return new Fraction32(rational); }
		/// <summary>Implicitly converts an short shorto a fraction.</summary>
		/// <param name="integer">The integer to convert shorto a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator Fraction32(int integer) { return new Fraction32((short)integer); }
		/// <summary>Implicitly converts an short shorto a fraction.</summary>
		/// <param name="shorteger">The shorteger to convert shorto a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static implicit operator Fraction32(short shorteger) { return new Fraction32(shorteger); }
		/// <summary>Implicitly converts an short shorto a fraction.</summary>
		/// <param name="literal">The shorteger to convert shorto a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator Fraction32(string literal) { return new Fraction32(literal); }
		/// <summary>Implicitly converts an short shorto a fraction.</summary>
		/// <param name="fraction">The shorteger to convert shorto a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static implicit operator string(Fraction32 fraction) { return fraction.ToString(); }
		/// <summary>Implicitly converts an short shorto a fraction.</summary>
		/// <param name="fraction">The shorteger to convert shorto a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator double(Fraction32 fraction) { return fraction.ToDouble(); }
		public static Fraction32 operator %(Fraction32 left, Fraction32 right)
		{
			while (left > right)
				left = left - right;
			return left;
		}

		#endregion

		#region Instance Methods

		public short CompareTo(Fraction32 right)
		{
			if (this < right)
				return -1;
			else if (this > right)
				return 1;
			else return 0;
		}

		public static short CompareTo(Fraction32 left, Fraction32 right)
		{
			if (left < right)
				return -1;
			else if (left > right)
				return 1;
			else return 0;
		}

		/// <summary>Check for equality by value.</summary>
		/// <param name="left">The left operand of the equality check.</param>
		/// <param name="right">The right operand of the equality check.</param>
		/// <returns>True if equal; false if not.</returns>
		internal static bool Equals(Fraction32 left, Fraction32 right)
		{
			return (left._numerator == right._numerator && left._denominator == right._denominator);
		}

		/// <summary>Checks for equality by value.</summary>
		/// <param name="obj">The right operand of the equality check.</param>
		/// <returns>True if equal; false if not.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Fraction32)
				return Fraction32.Equals(this, (Fraction32)obj);
			return false;
		}

		/// <summary>Computes a hash code for this fraction.</summary>
		public override int GetHashCode()
		{
			return (short)((Numerator ^ Denominator) & 0xFFFFFFFF);
		}

		/// <summary>Negates a fraction.</summary>
		/// <param name="frac1">The fraction to negate.</param>
		/// <returns>The result of the negation.</returns>
		internal static Fraction32 Negate(Fraction32 frac1)
		{
			short iNumerator = (short)(-frac1.Numerator);
			short iDenominator = frac1.Denominator;
			return (new Fraction32(iNumerator, iDenominator));
		}

		/// <summary>Compute the addition of two fracitons.</summary>
		/// <param name="frac1">The left operand of the addition.</param>
		/// <param name="frac2">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		internal static Fraction32 Add(Fraction32 frac1, Fraction32 frac2)
		{
			try
			{
				checked
				{
					short iNumerator = (short)(frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator);
					short iDenominator = (short)(frac1.Denominator * frac2.Denominator);
					return (new Fraction32(iNumerator, iDenominator));
				}
			}
			catch
			{
				// throw new System.Exception("Overflow occurred while performing arithemetic operation");
				return new Fraction32(frac1.ToDouble() + frac2.ToDouble());
			}
		}

		/// <summary>Computes the multiplication of two fractions.</summary>
		/// <param name="frac1">The left operand of the multiplication.</param>
		/// <param name="frac2">The right operand of the multiplication.</param>
		/// <returns>The result of the multiplication.</returns>
		internal static Fraction32 Multiply(Fraction32 frac1, Fraction32 frac2)
		{
			try
			{
				checked
				{
					short iNumerator = (short)(frac1.Numerator * frac2.Numerator);
					short iDenominator = (short)(frac1.Denominator * frac2.Denominator);
					return (new Fraction32(iNumerator, iDenominator));
				}
			}
			catch
			{
				// throw new System.Exception("Overflow occurred while performing arithemetic operation");
				return new Fraction32(frac1.ToDouble() * frac2.ToDouble());
			}
		}

		internal double ToDouble()
		{
			return this.Numerator / (double)this.Denominator;
		}

		/// <summary>Converts this fraction shorto a string "num/den".</summary>
		/// <returns>A string representation fo this fraction.</returns>
		public override string ToString()
		{
			string str;
			if (this.Denominator == 1)
				str = this.Numerator.ToString();
			else
				str = this.Numerator + "/" + this.Denominator;
			return str;
		}

		/// <summary>
		/// The function takes an string as an argument and returns its corresponding reduced fraction32
		/// the string can be an in the form of and shorteger, double or fraction32.
		/// e.g it can be like "123" or "123.321" or "123/456"
		/// </summary>
		public static Fraction32 Parse(string literal)
		{
			return new Fraction32(literal);
		}

		/// <summary>
		/// The function takes a floating poshort number as an argument 
		/// and returns its corresponding reduced fraction32
		/// </summary>
		public static Fraction32 ToFraction(double rational)
		{
			return new Fraction32(rational);
		}

		/// <summary>The function returns the inverse of a Fraction object.</summary>
		public static Fraction32 Inverse(Fraction32 frac1)
		{
			if (frac1.Numerator == 0)
				throw new System.ArgumentOutOfRangeException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

			short iNumerator = frac1.Denominator;
			short iDenominator = frac1.Numerator;
			return (new Fraction32(iNumerator, iDenominator));
		}

		/// <summary>
		/// The function reduces(simplifies) a Fraction object by dividing both its numerator 
		/// and denominator by their GCD
		/// </summary>
		public static void Reduce(Fraction32 frac)
		{
			try
			{
				if (frac.Numerator == 0)
				{
					frac.Denominator = 1;
					return;
				}

				short iGCD = GreatestCommonFactor(frac.Numerator, frac.Denominator);
				frac.Numerator /= iGCD;
				frac.Denominator /= iGCD;

				if (frac.Denominator < 0)   // if -ve sign in denominator
				{
					//pass -ve sign to numerator
					frac.Numerator *= -1;
					frac.Denominator *= -1;
				}
			}
			catch (Exception exception)
			{
				throw new MathematicsException("Cannot Reduce Fraction" + exception.Message, exception);
			}
		}

		#endregion
	}

	/// <summary>A fraction represented as two integers (numerator / denomnator).</summary>
	/// <citation>
	/// This fraction imlpementation was originally developed by 
	/// Syed Mehroz Alam and posted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition into the Towel framework.
	/// http://www.codeproject.com/Articles/9078/Fraction-class-in-C
	/// 
	/// Original Author:
	/// Author: Syed Mehroz Alam
	/// Email: smehrozalam@yahoo.com
	/// URL: Programming Home http://www.geocities.com/smehrozalam/
	/// Date: 6/15/2004
	/// Time: 10:54 AM
	/// </citation>
	[Serializable]
	public struct Fraction64
	{
		internal int _numerator;
		internal int _denominator;

		/// <summary>The maximum Fraction64 value.</summary>
		public static readonly Fraction64 MaxValue = new Fraction64(int.MaxValue, 1);
		/// <summary>The minimum Fraction64 value.</summary>
		public static readonly Fraction64 MinValue = new Fraction64(int.MinValue, 1);

		#region Properties

		/// <summary>The numerator of the fraction.</summary>
		public int Numerator
		{
			get
			{
				return this._numerator;
			}
			set
			{
				this._numerator = value;
			}
		}

		/// <summary>The denominator of the fraction.</summary>
		public int Denominator
		{
			get
			{
				return this._denominator;
			}
			set
			{
				if (value == 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, "!(" + nameof(value) + " != 0)");
				}
				this._denominator = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a fraction from an int.</summary>
		/// <param name="integer">The int to represent as a fraction.</param>
		public Fraction64(int integer)
		{
			this._numerator = integer;
			this._denominator = 1;
		}

		/// <summary>Constructs a fracion from a double.</summary>
		/// <param name="rational">The double to represent as a fraction.</param>
		public Fraction64(double rational)
		{
			Rounded:
			if (int.MinValue > rational || rational > int.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(rational), rational, "!(int." + nameof(int.MinValue) + " <= " + nameof(rational) + " <= int." + nameof(int.MaxValue) + ")");
			}
			if (rational > int.MaxValue)
				throw new System.ArgumentOutOfRangeException("Fraction64 construction invalid (rational > int.MaxValue)");
			else if (rational % 1 == 0)
			{
				this._numerator = (int)rational;
				this._denominator = 1;
				return;
			}
			else
			{
				try
				{
					checked
					{
						double temp_rational = rational;
						int multiple = 1;
						string temp_string = rational.ToString();
						while (temp_string.IndexOf("E") > 0)
						{
							temp_rational *= 10;
							multiple *= 10;
							temp_string = temp_rational.ToString();
						}
						int i = 0;
						while (temp_string[i] != '.')
							i++;
						int digitsAfterDecimal = temp_string.Length - i - 1;
						while (digitsAfterDecimal > 0)
						{
							temp_rational *= 10;
							multiple *= 10;
							digitsAfterDecimal--;
						}
						_numerator = (int)System.Math.Round(temp_rational);
						_denominator = multiple;
						Reduce(this);
					}
				}
				catch
				{
					rational = System.Math.Round(rational, 5);
					goto Rounded;
				}
			}
		}

		/// <summary>Makes a fraction given an integer and a denominator.</summary>
		/// <param name="numerator">The nmerator of the fraction.</param>
		/// <param name="deniminator">The denominator fo the fraction.</param>
		public Fraction64(int numerator, int deniminator)
		{
			_numerator = numerator;
			_denominator = deniminator;
			Reduce(this);
		}

		/// <summary>Creates a fraction by parsing a string.</summary>
		/// <param name="literal">The literal representation fo the fraction.</param>
		public Fraction64(string literal)
		{
			int i;
			for (i = 0; i < literal.Length; i++)
				if (literal[i] == '/')
					break;

			if (i == literal.Length)
			{
				double rational = double.Parse(literal);
				try
				{
					checked
					{
						if (rational % 1 == 0)  // if whole number
						{
							this._numerator = (int)rational;
							this._denominator = 1;
							Fraction64.Reduce(this);
						}
						else
						{
							double temp_rational = rational;
							int multiple = 1;
							string temp_string = rational.ToString();

							if (temp_string.Contains("E"))
							{
								while (temp_string.IndexOf("E") > 0)    // if in the form like 12E-9
								{
									temp_rational *= 10;
									multiple *= 10;
									temp_string = temp_rational.ToString();
								}
							}
							else if (temp_string.Contains("e"))
							{
								while (temp_string.IndexOf("e") > 0)    // if in the form like 12e-9
								{
									temp_rational *= 10;
									multiple *= 10;
									temp_string = temp_rational.ToString();
								}
							}

							int j = 0;
							while (temp_string[j] != '.')
								j++;
							int iDigitsAfterDecimal = temp_string.Length - j - 1;
							while (iDigitsAfterDecimal > 0)
							{
								temp_rational *= 10;
								multiple *= 10;
								iDigitsAfterDecimal--;
							}
							_numerator = (int)Math.Round(temp_rational);
							_denominator = multiple;
							Reduce(this);
						}
					}
				}
				catch (OverflowException overflowException)
				{
					throw new OverflowException("Conversion not possible due to overflow", overflowException);
				}
				catch (Exception exception)
				{
					throw new Exception("Conversion not possible", exception);
				}
			}
			else
			{
				this._numerator = int.Parse(literal.Substring(0, i));
				this._denominator = int.Parse(literal.Substring(i + 1));
				Reduce(this);
			}
		}

		#endregion

		#region Operators

		public static Fraction64 operator ++(Fraction64 fraction) { return fraction + 1; }
		/// <summary>Negates a fraction.</summary>
		/// <param name="fraction">The fraction to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Fraction64 operator -(Fraction64 fraction) { return Fraction64.Negate(fraction); }
		/// <summary>Adds two operands together.</summary>
		/// <param name="left">The left operand of the addition.</param>
		/// <param name="right">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Fraction64 operator +(Fraction64 left, Fraction64 right) { return Fraction64.Add(left, right); }
		/// <summary>Subtracts two operands.</summary>
		/// <param name="left">The left operand of the subtraction.</param>
		/// <param name="right">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fraction64 operator -(Fraction64 left, Fraction64 right) { return Fraction64.Add(left, -right); }
		/// <summary>Multiplies two operands together.</summary>
		/// <param name="left">The left operand of the multiplication.</param>
		/// <param name="right">The right operand of the multiplication.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fraction64 operator *(Fraction64 left, Fraction64 right) { return Fraction64.Multiply(left, right); }
		/// <summary>Divides two operands.</summary>
		/// <param name="left">The left operand of the division.</param>
		/// <param name="right">The right operand of the division.</param>
		/// <returns>The result of the division.</returns>
		public static Fraction64 operator /(Fraction64 left, Fraction64 right) { return Fraction64.Multiply(left, Inverse(right)); }
		/// <summary>Checks for equality between two fractions.</summary>
		/// <param name="left">The first operand of the equality check.</param>
		/// <param name="right">The second operand of the equality check.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator ==(Fraction64 left, Fraction64 right) { return Fraction64.Equals(left, right); }
		/// <summary>Checks for equality between two fractions.</summary>
		/// <param name="left">The first operand of the equality check.</param>
		/// <param name="right">The second operand of the equality check.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator !=(Fraction64 left, Fraction64 right) { return !Fraction64.Equals(left, right); }
		/// <summary>Performs a less-than inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator <(Fraction64 left, Fraction64 right) { return left.Numerator * right.Denominator < right.Numerator * left.Denominator; }
		/// <summary>Performs a greater-than inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator >(Fraction64 left, Fraction64 right) { return left.Numerator * right.Denominator > right.Numerator * left.Denominator; }
		/// <summary>Performs a less-than-or-equal inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator <=(Fraction64 left, Fraction64 right) { return left.Numerator * right.Denominator <= right.Numerator * left.Denominator; }
		/// <summary>Performs a greater-than-or-equal inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator >=(Fraction64 left, Fraction64 right) { return left.Numerator * right.Denominator >= right.Numerator * left.Denominator; }
		/// <summary>Converts a double to a fraction. Precision will be lost.</summary>
		/// <param name="rational">The double to convert to a fraction.</param>
		/// <returns>The resulting double of the conversion.</returns>
		public static explicit operator Fraction64(double rational) { return new Fraction64(rational); }
		/// <summary>Implicitly converts an int into a fraction.</summary>
		/// <param name="integer">The integer to convert into a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static implicit operator Fraction64(int integer) { return new Fraction64(integer); }
		/// <summary>Implicitly converts an int into a fraction.</summary>
		/// <param name="literal">The integer to convert into a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator Fraction64(string literal) { return new Fraction64(literal); }
		/// <summary>Implicitly converts an int into a fraction.</summary>
		/// <param name="fraction">The integer to convert into a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static implicit operator string(Fraction64 fraction) { return fraction.ToString(); }
		/// <summary>Implicitly converts an int into a fraction.</summary>
		/// <param name="fraction">The integer to convert into a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator double(Fraction64 fraction) { return fraction.ToDouble(); }
		public static Fraction64 operator %(Fraction64 left, Fraction64 right)
		{
			while (left > right)
				left = left - right;
			return left;
		}

		#endregion

		#region Instance Methods

		public int CompareTo(Fraction64 right)
		{
			if (this < right)
				return -1;
			else if (this > right)
				return 1;
			else return 0;
		}

		public static int CompareTo(Fraction64 left, Fraction64 right)
		{
			if (left < right)
				return -1;
			else if (left > right)
				return 1;
			else return 0;
		}

		/// <summary>Check for equality by value.</summary>
		/// <param name="left">The left operand of the equality check.</param>
		/// <param name="right">The right operand of the equality check.</param>
		/// <returns>True if equal; false if not.</returns>
		internal static bool Equals(Fraction64 left, Fraction64 right)
		{
			return (left._numerator == right._numerator && left._denominator == right._denominator);
		}

		/// <summary>Checks for equality by value.</summary>
		/// <param name="obj">The right operand of the equality check.</param>
		/// <returns>True if equal; false if not.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Fraction64)
				return Fraction64.Equals(this, (Fraction64)obj);
			return false;
		}

		/// <summary>Computes a hash code for this fraction.</summary>
		public override int GetHashCode()
		{
			return (int)((Numerator ^ Denominator) & 0xFFFFFFFF);
		}

		/// <summary>Negates a fraction.</summary>
		/// <param name="frac1">The fraction to negate.</param>
		/// <returns>The result of the negation.</returns>
		internal static Fraction64 Negate(Fraction64 frac1)
		{
			int iNumerator = -frac1.Numerator;
			int iDenominator = frac1.Denominator;
			return (new Fraction64(iNumerator, iDenominator));
		}

		/// <summary>Compute the addition of two fracitons.</summary>
		/// <param name="frac1">The left operand of the addition.</param>
		/// <param name="frac2">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		internal static Fraction64 Add(Fraction64 frac1, Fraction64 frac2)
		{
			try
			{
				checked
				{
					int iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
					int iDenominator = frac1.Denominator * frac2.Denominator;
					return (new Fraction64(iNumerator, iDenominator));
				}
			}
			catch
			{
				// throw new System.Exception("Overflow occurred while performing arithemetic operation");
				return new Fraction64(frac1.ToDouble() + frac2.ToDouble());
			}
		}

		/// <summary>Computes the multiplication of two fractions.</summary>
		/// <param name="frac1">The left operand of the multiplication.</param>
		/// <param name="frac2">The right operand of the multiplication.</param>
		/// <returns>The result of the multiplication.</returns>
		internal static Fraction64 Multiply(Fraction64 frac1, Fraction64 frac2)
		{
			try
			{
				checked
				{
					int iNumerator = frac1.Numerator * frac2.Numerator;
					int iDenominator = frac1.Denominator * frac2.Denominator;
					return (new Fraction64(iNumerator, iDenominator));
				}
			}
			catch
			{
				// throw new System.Exception("Overflow occurred while performing arithemetic operation");
				return new Fraction64(frac1.ToDouble() * frac2.ToDouble());
			}
		}

		internal double ToDouble()
		{
			return this.Numerator / (double)this.Denominator;
		}

		/// <summary>Converts this fraction into a string "num/den".</summary>
		/// <returns>A string representation fo this fraction.</returns>
		public override string ToString()
		{
			string str;
			if (this.Denominator == 1)
				str = this.Numerator.ToString();
			else
				str = this.Numerator + "/" + this.Denominator;
			return str;
		}

		/// <summary>
		/// The function takes an string as an argument and returns its corresponding reduced fraction64
		/// the string can be an in the form of and integer, double or fraction64.
		/// e.g it can be like "123" or "123.321" or "123/456"
		/// </summary>
		public static Fraction64 Parse(string literal)
		{
			return new Fraction64(literal);
		}

		/// <summary>
		/// The function takes a floating point number as an argument 
		/// and returns its corresponding reduced fraction64
		/// </summary>
		public static Fraction64 ToFraction(double rational)
		{
			return new Fraction64(rational);
		}

		/// <summary>The function returns the inverse of a Fraction object.</summary>
		public static Fraction64 Inverse(Fraction64 frac1)
		{
			if (frac1.Numerator == 0)
				throw new System.ArgumentOutOfRangeException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

			int iNumerator = frac1.Denominator;
			int iDenominator = frac1.Numerator;
			return (new Fraction64(iNumerator, iDenominator));
		}

		/// <summary>
		/// The function reduces(simplifies) a Fraction object by dividing both its numerator 
		/// and denominator by their GCD
		/// </summary>
		public static void Reduce(Fraction64 frac)
		{
			try
			{
				if (frac.Numerator == 0)
				{
					frac.Denominator = 1;
					return;
				}
				int gcf = GreatestCommonFactor(frac.Numerator, frac.Denominator);
				frac.Numerator /= gcf;
				frac.Denominator /= gcf;
				if (frac.Denominator < 0)
				{
					frac.Numerator *= -1;
					frac.Denominator *= -1;
				}
			}
			catch (Exception exception)
			{
				throw new MathematicsException("Cannot Reduce Fraction" + exception.Message, exception);
			}
		}

		#endregion
	}

	/// <summary>A fraction represented as two longs (numerator / denomnator).</summary>
	/// <citation>
	/// This fraction imlpementation was originally developed by 
	/// Syed Mehroz Alam and posted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition into the Towel framework.
	/// http://www.codeproject.com/Articles/9078/Fraction-class-in-C
	/// 
	/// Original Author:
	/// Author: Syed Mehroz Alam
	/// Email: smehrozalam@yahoo.com
	/// URL: Programming Home http://www.geocities.com/smehrozalam/
	/// Date: 6/15/2004
	/// Time: 10:54 AM
	/// </citation>
	[Serializable]
	public struct Fraction128
	{
		internal long _numerator;
		internal long _denominator;

		/// <summary>The maximum Fraction128 value.</summary>
		public readonly static Fraction128 MaxValue = new Fraction128(long.MaxValue, 1);
		/// <summary>The minimum Fraction128 value.</summary>
		public readonly static Fraction128 MinValue = new Fraction128(long.MinValue, 1);

		#region Properties

		/// <summary>The numerator of the fraction.</summary>
		public long Numerator
		{
			get
			{
				return this._numerator;
			}
			set
			{
				this._numerator = value;
			}
		}

		/// <summary>The denominator of the fraction.</summary>
		public long Denominator
		{
			get
			{
				return this._denominator;
			}
			set
			{
				if (value == 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, "!(" + nameof(value) + " != 0)");
				}
				this._denominator = value;
			}
		}

		#endregion

		#region Constructors

		public Fraction128(long @long)
		{
			this._numerator = @long;
			this._denominator = 1;
		}

		public Fraction128(double rational)
		{
			Rounded:

			if (long.MinValue > rational || rational > long.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(rational), rational, "!(long." + nameof(long.MinValue) + " <= " + nameof(rational) + " <= long." + nameof(long.MaxValue) + ")");
			}
			else if (rational % 1 == 0) // if whole number
			{
				this._numerator = (long)rational;
				this._denominator = 1;
				Fraction128.Reduce(this);
			}
			else
			{
				try
				{
					checked
					{
						double temp_rational = rational;
						long multiple = 1;
						string temp_string = rational.ToString();
						while (temp_string.IndexOf("E") > 0)    // if in the form like 12E-9
						{
							temp_rational *= 10;
							multiple *= 10;
							temp_string = temp_rational.ToString();
						}
						int i = 0;
						while (temp_string[i] != '.')
							i++;
						long digitsAfterDecimal = temp_string.Length - i - 1;
						while (digitsAfterDecimal > 0)
						{
							temp_rational *= 10;
							multiple *= 10;
							digitsAfterDecimal--;
						}
						_numerator = (long)System.Math.Round(temp_rational);
						_denominator = multiple;
						Reduce(this);
					}
				}
				catch (OverflowException)
				{
					rational = System.Math.Round(rational, 10);
					goto Rounded;
				}
			}
		}

		public Fraction128(long numerator, long deniminator)
		{
			_numerator = numerator;
			_denominator = deniminator;
			Reduce(this);
		}

		public Fraction128(string literal)
		{
			int i;
			for (i = 0; i < literal.Length; i++)
				if (literal[i] == '/')
					break;

			if (i == literal.Length)
			{
				double rational = double.Parse(literal);
				try
				{
					checked
					{
						if (rational % 1 == 0)  // if whole number
						{
							this._numerator = (long)rational;
							this._denominator = 1;
							Reduce(this);
						}
						else
						{
							double temp_rational = rational;
							long multiple = 1;
							string temp_string = rational.ToString();
							while (temp_string.IndexOf("E") > 0)    // if in the form like 12E-9
							{
								temp_rational *= 10;
								multiple *= 10;
								temp_string = temp_rational.ToString();
							}
							int j = 0;
							while (temp_string[j] != '.')
								j++;
							long iDigitsAfterDecimal = temp_string.Length - j - 1;
							while (iDigitsAfterDecimal > 0)
							{
								temp_rational *= 10;
								multiple *= 10;
								iDigitsAfterDecimal--;
							}
							_numerator = (long)System.Math.Round(temp_rational);
							_denominator = multiple;
							Reduce(this);
						}
					}
				}
				catch (OverflowException overflowException)
				{
					throw new OverflowException("Conversion not possible due to overflow", overflowException);
				}
				catch (Exception exception)
				{
					throw new Exception("Conversion not possible", exception);
				}
			}
			else
			{
				// else string is in the form of Numerator/Denominator
				long iNumerator = long.Parse(literal.Substring(0, i));
				long iDenominator = long.Parse(literal.Substring(i + 1));

				this._numerator = iNumerator;
				this._denominator = iDenominator;
				Fraction128.Reduce(this);
			}
		}

		#endregion

		#region Operators

		public static Fraction128 operator ++(Fraction128 fraction) { return fraction + 1; }
		/// <summary>Negates a fraction.</summary>
		/// <param name="fraction">The fraction to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Fraction128 operator -(Fraction128 fraction) { return Fraction128.Negate(fraction); }
		/// <summary>Adds two operands together.</summary>
		/// <param name="left">The left operand of the addition.</param>
		/// <param name="right">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Fraction128 operator +(Fraction128 left, Fraction128 right) { return (Fraction128.Add(left, right)); }
		/// <summary>Subtracts two operands.</summary>
		/// <param name="left">The left operand of the subtraction.</param>
		/// <param name="right">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fraction128 operator -(Fraction128 left, Fraction128 right) { return (Fraction128.Add(left, -right)); }
		/// <summary>Multiplies two operands together.</summary>
		/// <param name="left">The left operand of the multiplication.</param>
		/// <param name="right">The right operand of the multiplication.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fraction128 operator *(Fraction128 left, Fraction128 right) { return (Fraction128.Multiply(left, right)); }
		/// <summary>Divides two operands.</summary>
		/// <param name="left">The left operand of the division.</param>
		/// <param name="right">The right operand of the division.</param>
		/// <returns>The result of the division.</returns>
		public static Fraction128 operator /(Fraction128 left, Fraction128 right) { return (Fraction128.Multiply(left, Inverse(right))); }
		/// <summary>Checks for equality between two fractions.</summary>
		/// <param name="left">The first operand of the equality check.</param>
		/// <param name="right">The second operand of the equality check.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator ==(Fraction128 left, Fraction128 right) { return Fraction128.Equals(left, right); }
		/// <summary>Checks for equality between two fractions.</summary>
		/// <param name="left">The first operand of the equality check.</param>
		/// <param name="right">The second operand of the equality check.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool operator !=(Fraction128 left, Fraction128 right) { return !Fraction128.Equals(left, right); }
		/// <summary>Performs a less-than inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator <(Fraction128 left, Fraction128 right) { return left.Numerator * right.Denominator < right.Numerator * left.Denominator; }
		/// <summary>Performs a greater-than inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator >(Fraction128 left, Fraction128 right) { return left.Numerator * right.Denominator > right.Numerator * left.Denominator; }
		/// <summary>Performs a less-than-or-equal inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator <=(Fraction128 left, Fraction128 right) { return left.Numerator * right.Denominator <= right.Numerator * left.Denominator; }
		/// <summary>Performs a greater-than-or-equal inquality between two operands.</summary>
		/// <param name="left">The left operand of the inequality.</param>
		/// <param name="right">The right operand of the inequality.</param>
		/// <returns>The value of the inequality.</returns>
		public static bool operator >=(Fraction128 left, Fraction128 right) { return left.Numerator * right.Denominator >= right.Numerator * left.Denominator; }
		/// <summary>Converts a double to a fraction. Precision will be lost.</summary>
		/// <param name="rational">The double to convert to a fraction.</param>
		/// <returns>The resulting double of the conversion.</returns>
		public static explicit operator Fraction128(double rational) { return new Fraction128(rational); }
		/// <summary>Implicitly converts an long longo a fraction.</summary>
		/// <param name="longeger">The longeger to convert longo a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static implicit operator Fraction128(long longeger) { return new Fraction128(longeger); }
		/// <summary>Implicitly converts an long longo a fraction.</summary>
		/// <param name="literal">The longeger to convert longo a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator Fraction128(string literal) { return new Fraction128(literal); }
		/// <summary>Implicitly converts an long longo a fraction.</summary>
		/// <param name="fraction">The longeger to convert longo a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static implicit operator string(Fraction128 fraction) { return fraction.ToString(); }
		/// <summary>Implicitly converts an long longo a fraction.</summary>
		/// <param name="fraction">The longeger to convert longo a fraction.</param>
		/// <returns>The resulting fraction representation.</returns>
		public static explicit operator double(Fraction128 fraction) { return fraction.ToDouble(); }
		public static Fraction128 operator %(Fraction128 left, Fraction128 right)
		{
			while (left > right)
				left = left - right;
			return left;
		}

		#endregion

		#region Instance Methods

		public int CompareTo(Fraction128 right)
		{
			if (this < right)
				return -1;
			else if (this > right)
				return 1;
			else return 0;
		}

		public static int CompareTo(Fraction128 left, Fraction128 right)
		{
			if (left < right)
				return -1;
			else if (left > right)
				return 1;
			else return 0;
		}

		internal static bool Equals(Fraction128 left, Fraction128 right)
		{
			return (left._numerator == right._numerator && left._denominator == right._denominator);
		}

		/// <summary>Checks for equality with another object.</summary>
		/// <param name="obj">The object to equate with this.</param>
		/// <returns>The result of the equate.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Fraction128)
				return Fraction128.Equals(this, (Fraction128)obj);
			return false;
		}

		/// <summary>Returns a hash code for this Fraction128.</summary>
		public override int GetHashCode()
		{
			return (int)((Numerator ^ Denominator) & 0xFFFFFFFF);
		}

		/// <summary>
		/// longernal function for negation
		/// </summary>
		internal static Fraction128 Negate(Fraction128 frac1)
		{
			long iNumerator = -frac1.Numerator;
			long iDenominator = frac1.Denominator;
			return (new Fraction128(iNumerator, iDenominator));
		}

		internal static Fraction128 Add(Fraction128 frac1, Fraction128 frac2)
		{
			try
			{
				checked
				{
					long iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
					long iDenominator = frac1.Denominator * frac2.Denominator;
					return (new Fraction128(iNumerator, iDenominator));
				}
			}
			catch
			{
				// throw new System.Exception("Overflow occurred while performing arithemetic operation");
				return new Fraction128(frac1.ToDouble() + frac2.ToDouble());
			}
		}

		internal static Fraction128 Multiply(Fraction128 frac1, Fraction128 frac2)
		{
			try
			{
				checked
				{
					long iNumerator = frac1.Numerator * frac2.Numerator;
					long iDenominator = frac1.Denominator * frac2.Denominator;
					return (new Fraction128(iNumerator, iDenominator));
				}
			}
			catch
			{
				// throw new System.Exception("Overflow occurred while performing arithemetic operation");
				return new Fraction128(frac1.ToDouble() * frac2.ToDouble());
			}
		}

		//internal static long GreatestCommonDenominator(long first, long second)
		//{
		//	// take absolute values
		//	if (first < 0) first = -first;
		//	if (second < 0) second = -second;
		//	do
		//	{
		//		if (first < second)
		//		{
		//			long tmp = first;	// swap the two operands
		//			first = second;
		//			second = tmp;
		//		}
		//		first = first % second;
		//	} while (first != 0);
		//	return second;
		//}

		internal double ToDouble()
		{
			return this.Numerator / (double)this.Denominator;
		}

		public override string ToString()
		{
			string str;
			if (this.Denominator == 1)
				str = this.Numerator.ToString();
			else
				str = this.Numerator + "/" + this.Denominator;
			return str;
		}

		/// <summary>
		/// The function takes an string as an argument and returns its corresponding reduced fraction64
		/// the string can be an in the form of and longeger, double or fraction64.
		/// e.g it can be like "123" or "123.321" or "123/456"
		/// </summary>
		public static Fraction128 Parse(string literal)
		{
			return new Fraction128(literal);
		}

		/// <summary>
		/// The function takes a floating polong number as an argument 
		/// and returns its corresponding reduced fraction64
		/// </summary>
		public static Fraction128 ToFraction(double rational)
		{
			return new Fraction128(rational);
		}

		/// <summary>The function returns the inverse of a Fraction object.</summary>
		public static Fraction128 Inverse(Fraction128 frac1)
		{
			if (frac1.Numerator == 0)
				throw new System.ArgumentOutOfRangeException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

			long iNumerator = frac1.Denominator;
			long iDenominator = frac1.Numerator;
			return (new Fraction128(iNumerator, iDenominator));
		}

		/// <summary>
		/// The function reduces(simplifies) a Fraction object by dividing both its numerator 
		/// and denominator by their GCD
		/// </summary>
		public static void Reduce(Fraction128 fraction)
		{
			try
			{
				if (fraction.Numerator == 0)
				{
					fraction.Denominator = 1;
					return;
				}
				long gcf = GreatestCommonFactor(fraction.Numerator, fraction.Denominator);
				fraction.Numerator /= gcf;
				fraction.Denominator /= gcf;
				if (fraction.Denominator < 0)
				{
					fraction.Numerator *= -1;
					fraction.Denominator *= -1;
				}
			}
			catch (Exception exception)
			{
				throw new MathematicsException("Cannot Reduce Fraction" + exception.Message, exception);
			}
		}

		#endregion
	}
}
