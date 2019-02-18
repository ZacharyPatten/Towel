namespace Towel
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
	public struct Fraction32
	{
        private short _numerator;
        private short _denominator;

        #region Constants

        /// <summary>The maximum Fraction32 value.</summary>
        public static readonly Fraction32 MaxValue = new Fraction32(short.MaxValue, 1);

        /// <summary>The minimum Fraction32 value.</summary>
        public static readonly Fraction32 MinValue = new Fraction32(short.MinValue, 1);

        #endregion
            
		#region Properties

		/// <summary>The denominator of the fraction.</summary>
		public short Denominator
		{
			get { return _denominator; }
			set
			{
				if (value == 0) { throw new System.ArgumentException("Denominator cannot be 0", "value"); }
				_denominator = value;
			}
		}

		/// <summary>The numerator of the fraction.</summary>
		public short Numerator
		{
			get { return _numerator; }
			set { _numerator = value; }
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a fraction from an short.</summary>
		/// <param name="shorteger">The short to represent as a fraction.</param>
		public Fraction32(short shorteger)
		{
			this._numerator = shorteger;
			this._denominator = 1;
		}

		/// <summary>Constructs a fracion from a double.</summary>
		/// <param name="rational">The double to represent as a fraction.</param>
		public Fraction32(double rational)
		{
		Rounded:

			if (rational > short.MaxValue) { throw new System.ArgumentException("rational > short.MaxValue"); }

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
						_numerator = (short)System.Math.Round(temp_rational);
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

		/// <summary>Makes a fraction given an shorteger and a denominator.</summary>
		/// <param name="numerator">The nmerator of the fraction.</param>
		/// <param name="deniminator">The denominator fo the fraction.</param>
		public Fraction32(short numerator, short deniminator)
		{
			_numerator = numerator;
			_denominator = deniminator;
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
				double rational = System.Convert.ToDouble(literal);
				try
				{
					checked
					{
						if (rational % 1 == 0)	// if whole number
						{
							this._numerator = (short)rational;
							this._denominator = 1;
							Fraction32.Reduce(this);
						}
						else
						{
							double temp_rational = rational;
							short multiple = 1;
							string temp_string = rational.ToString();

							if (temp_string.Contains("E"))
							{
								while (temp_string.IndexOf("E") > 0)	// if in the form like 12E-9
								{
									temp_rational *= 10;
									multiple *= 10;
									temp_string = temp_rational.ToString();
								}
							}
							else if (temp_string.Contains("e"))
							{
								while (temp_string.IndexOf("e") > 0)	// if in the form like 12e-9
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
							_numerator = (short)System.Math.Round(temp_rational);
							_denominator = multiple;
							Reduce(this);
						}
					}
				}
				catch (System.OverflowException ex)
				{
					throw new System.OverflowException("Conversion not possible due to overflow", ex);
				}
				catch (System.Exception ex)
				{
					throw new System.Exception("Conversion not possible", ex);
				}
			}
			else
			{
				// else string is in the form of Numerator/Denominator
				short iNumerator = System.Convert.ToInt16(literal.Substring(0, i));
				short iDenominator = System.Convert.ToInt16(literal.Substring(i + 1));

				this._numerator = iNumerator;
				this._denominator = iDenominator;
				Fraction32.Reduce(this);
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
		private static bool Equals(Fraction32 left, Fraction32 right)
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
		private static Fraction32 Negate(Fraction32 frac1)
		{
			short iNumerator = (short)(-frac1.Numerator);
			short iDenominator = frac1.Denominator;
			return (new Fraction32(iNumerator, iDenominator));
		}

		/// <summary>Compute the addition of two fracitons.</summary>
		/// <param name="frac1">The left operand of the addition.</param>
		/// <param name="frac2">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		private static Fraction32 Add(Fraction32 frac1, Fraction32 frac2)
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
		private static Fraction32 Multiply(Fraction32 frac1, Fraction32 frac2)
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

		private static short GreatestCommonDenominator(short first, short second)
		{
			// take absolute values
			if (first < 0) first = (short)(-first);
			if (second < 0) second = (short)(-second);
			do
			{
				if (first < second)
				{
					short tmp = first;	// swap the two operands
					first = second;
					second = tmp;
				}
				first = (short)(first % second);
			} while (first != 0);
			return second;
		}

		private double ToDouble()
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

				short iGCD = GreatestCommonDenominator(frac.Numerator, frac.Denominator);
				frac.Numerator /= iGCD;
				frac.Denominator /= iGCD;

				if (frac.Denominator < 0)	// if -ve sign in denominator
				{
					//pass -ve sign to numerator
					frac.Numerator *= -1;
					frac.Denominator *= -1;
				}
			} // end try
			catch (System.Exception exp)
			{
				throw new System.Exception("Cannot reduce Fraction: " + exp.Message);
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
	public struct Fraction64
	{
        private int _numerator;
        private int _denominator;

        #region Constants

        /// <summary>The maximum Fraction64 value.</summary>
        public static readonly Fraction64 MaxValue = new Fraction64(int.MaxValue, 1);

        /// <summary>The minimum Fraction64 value.</summary>
        public static readonly Fraction64 MinValue = new Fraction64(int.MinValue, 1);

        #endregion
            
        #region Properties

        /// <summary>The denominator of the fraction.</summary>
        public int Denominator
		{
			get { return _denominator; }
			set
			{
                if (value != 0)
                {
                    _denominator = value;
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException("Denominator cannot be assigned a ZERO Value");
                }
			}
		}

		/// <summary>The numerator of the fraction.</summary>
		public int Numerator
		{
			get { return _numerator; }
			set { _numerator = value; }
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
				double rational = System.Convert.ToDouble(literal);
				try
				{
					checked
					{
						if (rational % 1 == 0)	// if whole number
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
								while (temp_string.IndexOf("E") > 0)	// if in the form like 12E-9
								{
									temp_rational *= 10;
									multiple *= 10;
									temp_string = temp_rational.ToString();
								}
							}
							else if (temp_string.Contains("e"))
							{
								while (temp_string.IndexOf("e") > 0)	// if in the form like 12e-9
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
							_numerator = (int)System.Math.Round(temp_rational);
							_denominator = multiple;
							Reduce(this);
						}
					}
				}
				catch (System.OverflowException ex)
				{
					throw new System.OverflowException("Conversion not possible due to overflow", ex);
				}
				catch (System.Exception ex)
				{
					throw new System.Exception("Conversion not possible", ex);
				}
			}
			else
			{
				// else string is in the form of Numerator/Denominator
				int iNumerator = System.Convert.ToInt32(literal.Substring(0, i));
				int iDenominator = System.Convert.ToInt32(literal.Substring(i + 1));

				this._numerator = iNumerator;
				this._denominator = iDenominator;
				Fraction64.Reduce(this);
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
		private static bool Equals(Fraction64 left, Fraction64 right)
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
		private static Fraction64 Negate(Fraction64 frac1)
		{
			int iNumerator = -frac1.Numerator;
			int iDenominator = frac1.Denominator;
			return (new Fraction64(iNumerator, iDenominator));
		}

		/// <summary>Compute the addition of two fracitons.</summary>
		/// <param name="frac1">The left operand of the addition.</param>
		/// <param name="frac2">The right operand of the addition.</param>
		/// <returns>The result of the addition.</returns>
		private static Fraction64 Add(Fraction64 frac1, Fraction64 frac2)
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
		private static Fraction64 Multiply(Fraction64 frac1, Fraction64 frac2)
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

		private static int GreatestCommonDenominator(int first, int second)
		{
			// take absolute values
			if (first < 0) first = -first;
			if (second < 0) second = -second;
			do
			{
				if (first < second)
				{
					int tmp = first;	// swap the two operands
					first = second;
					second = tmp;
				}
				first = first % second;
			} while (first != 0);
			return second;
		}

		private double ToDouble()
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

				int iGCD = GreatestCommonDenominator(frac.Numerator, frac.Denominator);
				frac.Numerator /= iGCD;
				frac.Denominator /= iGCD;

				if (frac.Denominator < 0)	// if -ve sign in denominator
				{
					//pass -ve sign to numerator
					frac.Numerator *= -1;
					frac.Denominator *= -1;
				}
			} // end try
			catch (System.Exception exp)
			{
				throw new System.Exception("Cannot reduce Fraction", exp);
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
    public struct Fraction128
	{
        private long _numerator;
        private long _denominator;
        
        #region Constants

        /// <summary>The maximum Fraction128 value.</summary>
        public readonly static Fraction128 MaxValue = new Fraction128(long.MaxValue, 1);

        /// <summary>The minimum Fraction128 value.</summary>
        public readonly static Fraction128 MinValue = new Fraction128(long.MinValue, 1);

        #endregion
            
		#region Properties

		public long Denominator
		{
			get { return _denominator; }
			set
			{
				if (value != 0)
					_denominator = value;
				else
					throw new System.ArgumentOutOfRangeException("Denominator cannot be assigned a ZERO Value");
			}
		}

		public long Numerator
		{
			get { return _numerator; }
			set { _numerator = value; }
		}

		#endregion

		#region Constructors

		public Fraction128(long longeger)
		{
			this._numerator = longeger;
			this._denominator = 1;
		}

		public Fraction128(double rational)
		{
		Rounded:

			if (rational > long.MaxValue)
				throw new System.ArgumentOutOfRangeException("Fraction128 construction invalid (rational > long.MaxValue)");
			else if (rational < long.MinValue)
				throw new System.ArgumentOutOfRangeException("Fraction128 construction invalid (rational < long.MinValue)");
			else if (rational % 1 == 0)	// if whole number
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
						while (temp_string.IndexOf("E") > 0)	// if in the form like 12E-9
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
				catch (System.OverflowException)
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
				double rational = System.Convert.ToDouble(literal);
				try
				{
					checked
					{
						if (rational % 1 == 0)	// if whole number
						{
							this._numerator = (long)rational;
							this._denominator = 1;
							Fraction128.Reduce(this);
						}
						else
						{
							double temp_rational = rational;
							long multiple = 1;
							string temp_string = rational.ToString();
							while (temp_string.IndexOf("E") > 0)	// if in the form like 12E-9
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
				catch (System.OverflowException ex)
				{
					throw new System.OverflowException("Conversion not possible due to overflow", ex);
				}
				catch (System.Exception ex)
				{
					throw new System.Exception("Conversion not possible", ex);
				}
			}
			else
			{
				// else string is in the form of Numerator/Denominator
				long iNumerator = System.Convert.ToInt64(literal.Substring(0, i));
				long iDenominator = System.Convert.ToInt64(literal.Substring(i + 1));

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

		private static bool Equals(Fraction128 left, Fraction128 right)
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
		private static Fraction128 Negate(Fraction128 frac1)
		{
			long iNumerator = -frac1.Numerator;
			long iDenominator = frac1.Denominator;
			return (new Fraction128(iNumerator, iDenominator));
		}

		private static Fraction128 Add(Fraction128 frac1, Fraction128 frac2)
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

		private static Fraction128 Multiply(Fraction128 frac1, Fraction128 frac2)
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

		private static long GreatestCommonDenominator(long first, long second)
		{
			// take absolute values
			if (first < 0) first = -first;
			if (second < 0) second = -second;
			do
			{
				if (first < second)
				{
					long tmp = first;	// swap the two operands
					first = second;
					second = tmp;
				}
				first = first % second;
			} while (first != 0);
			return second;
		}

		private double ToDouble()
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
		public static void Reduce(Fraction128 frac)
		{
			try
			{
				if (frac.Numerator == 0)
				{
					frac.Denominator = 1;
					return;
				}

				long iGCD = GreatestCommonDenominator(frac.Numerator, frac.Denominator);
				frac.Numerator /= iGCD;
				frac.Denominator /= iGCD;

				if (frac.Denominator < 0)	// if -ve sign in denominator
				{
					//pass -ve sign to numerator
					frac.Numerator *= -1;
					frac.Denominator *= -1;
				}
			} // end try
			catch (System.Exception ex)
			{
				throw new System.Exception("Cannot reduce Fraction: ", ex);
			}
		}

		#endregion
	}
}
