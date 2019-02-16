using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Towel.Mathematics;

namespace Towel
{
	// NOTE: THIS CLASS NEEDS MAJOR OPTIMIZATION FROM SCRATCH

	/// <summary>
	/// Infinitely long integer (capped only by memory).
	/// </summary>
	/// <citation>
	/// This Integer imlpementation was originally developed by 
	/// Chew Keong TAN and posted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition into the Towel framework.
	/// http://www.codeproject.com/Articles/2728/C-BigInteger-Class
	/// 
	/// Original Author:
	/// Author: Chew Keong TAN
	/// Email: wdwxy12345@gmail.com
	/// Date: 28/9/2002
	/// </citation>
	public struct Integer
	{
		private List<int> _integer;

		#region constructor

		public Integer(int integer)
			: this(integer.ToString())
		{
			// needs optimization
		}

		public Integer(long integer)
			: this(integer.ToString())
		{
			// needs optimization
		}

		public Integer(string val = "0")
		{
			string va = Format(val);
			string v = (va[0] == '-') ? va.Substring(1) : va;
			// One in four is taken as a int
			_integer = new List<int>();
			try
			{
				for (int i = v.Length - 4; i > -4; i -= 4)
				{
					if (i < 0)
					{
						_integer.Add(int.Parse(v.Substring(0, 4 + i)));
					}
					else
					{
						_integer.Add(int.Parse(v.Substring(i, 4)));
					}
				}
			}
			catch (Exception e)
			{
				if (e.GetType() == typeof(FormatException))
				{
					throw new FormatException("The value could not be parsed.", e);
				}
				throw;
			}
			// The number of bits up to 8 Integer multiple
			int valueLength = (_integer.Count / 8 + 1) * 8; // Count equivalent of Java size ()
			for (int i = _integer.Count; i < valueLength; i++)
			{
				_integer.Add(0);
			}
			// Switch to negative complement representation
			if (va[0] == '-')   // note
			{
				_integer = ToComplement(_integer);
			}
		}

		private Integer(List<int> value)
		{
			this._integer = value;
			this.Shorten();
		}

		#endregion

		#region public

		/// <summary>
		/// Adds Integers.
		/// </summary>
		/// <param name="that">right operand</param>
		/// <returns></returns>
		public Integer Add(Integer that)
		{
			if (IsNegative(that._integer))
			{
				//Switch subtraction
				return Substract(new Integer(ToComplement(that._integer)));
			}

			// Alignment in bits
			this.Shorten();
			that.Shorten();
			int length = Math.Max(_integer.Count, that._integer.Count); // The maximum length
			List<int> op1 = new List<int>(_integer);
			List<int> op2 = new List<int>(that._integer);
			op1.AddRange(ArrayGenerator(length - op1.Count, this.IsNegative()));
			op2.AddRange(ArrayGenerator(length - op2.Count, that.IsNegative()));
			List<int> result = new List<int>();

			int carry = 0; // carry
			for (int i = 0; i < length - 1; i++)
			{
				int c = op1[i] + op2[i] + carry;
				if (c < 10000)
				{
					carry = 0;
				}
				else
				{
					c -= 10000;
					carry = 1;
				}
				result.Add(c);
			}
			if (carry == 1) // carry
			{
				if (IsPositive(op1))
				{
					result.Add(1);
				}
				else
				{
					// Negative adder overflow of zero //FIXED: Can anyone tell me what I wrote
				}
				for (int i = 0; i < 8; i++)
				{
					result.Add(0);
				}
			}
			else
			{
				// Fill the seats , make positive number 0, negative complement 9999
				result.Add(IsPositive(op1) ? 0 : 9999);
			}
			return new Integer(result);
		}

		/// <summary>
		/// Subtract two Integers
		/// </summary>
		/// <param name="that">right operand</param>
		/// <returns></returns>
		public Integer Substract(Integer that)
		{
			if (IsNegative(that._integer))
			{
				return Add(new Integer(ToComplement(that._integer)));
			}

			//Alignment in bits
			this.Shorten();
			that.Shorten();
			int length = Math.Max(_integer.Count, that._integer.Count);
			List<int> op1 = new List<int>(_integer);
			List<int> op2 = new List<int>(that._integer);
			op1.AddRange(ArrayGenerator(length - op1.Count, this.IsNegative()));
			op2.AddRange(ArrayGenerator(length - op2.Count, that.IsNegative()));
			List<int> result = new List<int>();

			int borrow = 0;
			for (int i = 0; i < length - 1; i++)
			{
				int c = op1[i] - op2[i] - borrow;
				if (c >= 0)
				{
					borrow = 0;
				}
				else
				{
					c += 10000;// Negative to positive
					borrow = 1;// There borrow
				}
				result.Add(c);
			}
			if (borrow == 1) // Borrow Processing
			{
				if (IsNegative(op1))
				{
					result.Add(9998); //e.g. -200-900 = -1100
				}
				else
				{
					// Positive subtraction overflow to 0 // FIXED: I wrote this is what
				}
				for (int i = 0; i < 8; i++)
				{
					result.Add(9999); // Increase 8
				}
			}
			else
			{
				// Fill the seats , negative complement 9999 , a positive number up 0
				result.Add(IsNegative(op1) ? 9999 : 0);
			}

			return new Integer(result);
		}

		/// <summary>
		/// Multiplies two Integers
		/// </summary>
		/// <param name="val">multiplier</param>
		/// <param name="shift">Displacement</param>
		/// <returns></returns>
		private Integer Multiply(int val, int shift) // Internal use , the two parameters should be positive
		{
			List<int> result = new List<int>();
			for (int i = 0; i < shift; i++)
			{
				result.Add(0); // Shift up 0
			}
			int carry = 0;
			for (int i = 0; i < _integer.Count - 1; i++)
			{
				int tmp = _integer[i] * val + carry;
				result.Add(tmp % 10000);
				carry = tmp / 10000;
			}
			if (carry != 0)
			{
				result.Add(carry);
				for (int i = 0; i < 8; i++)
				{
					result.Add(0);
				}
			}
			else
			{
				result.Add(0);
			}
			return new Integer(result);
		}

		/// <summary>
		/// Multiplies two Integers
		/// </summary>
		/// <param name="that">multiplier</param>
		/// <returns></returns>
		public Integer Multiply(Integer that)
		{
			Integer op1 = IsNegative(_integer) ? new Integer(ToComplement(_integer)) : this;
			List<int> op2 = IsNegative(that._integer) ? ToComplement(that._integer) : that._integer;
			// Bitwise
			List<Integer> rs = new List<Integer>(); // Intermediate results
			for (int i = 0; i < op2.Count - 1; i++)
			{
				rs.Add(op1.Multiply(op2[i], i));
			}
			// Results for bitwise adding up
			Integer result = rs[0];
			for (int i = 1; i < rs.Count; i++)
			{
				result = result.Add(rs[i]);
			}
			// Analyzing the number of positive and negative
			return (GetLast(_integer) + GetLast(that._integer)) == 9999
					? new Integer(ToComplement(result._integer))
					: result;
		}

		/// <summary>
		/// Long integer division (int positive number) for internal use
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		private Integer Div(int that)
		{
			List<int> result = new List<int>();
			long remain = 0; // remainder
			for (int i = _integer.Count - 1; i > -1; i--)
			{
				long tmp = _integer[i] + remain;
				result.Add((int)(tmp / that));
				remain = (tmp % that) * 10000;    // To ensure that this step does not overflow , you must use long type
			}
			result.Reverse();
			for (int i = 0; i < 8 - (result.Count) % 8; i++)
			{
				result.Add(0);
			}
			return new Integer(result);
		}

		/// <summary>
		/// Long integer division
		/// </summary>
		/// <param name="that">divisor</param>
		/// <returns></returns>
		public Integer Divide(Integer that)
		{
			//MARK: Note that the division will not be rounded only reserved bit integer
			if (that.IsZero())
			{
				throw new DivideByZeroException("Divisor can not be 0 .");
			}
			// If the divisor is int type , in addition to direct , high efficiency
			int parsed;
			//FIXED: In addition to int before the turn to ensure that the dividend is positive
			if (int.TryParse(that.ToString(), out parsed))
			{
				if (this.IsNegative() || that.IsNegative())
				{
					if (this.IsNegative() && !that.IsNegative())
					{
						return new Integer(
								ToComplement(new Integer(ToComplement(this._integer)).Div(Math.Abs(parsed))._integer));
					}
					else if (!this.IsNegative() && that.IsNegative())
					{
						return new Integer(
								ToComplement(Div(Math.Abs(parsed))._integer));
					}
					else
					{
						return new Integer(
								new Integer(ToComplement(this._integer)).Div(Math.Abs(parsed))._integer);
					}
				}
				return Div(Math.Abs(parsed));
			}

			// Divisor is not an int , violence binary search
			// Always be a positive number
			Integer op1 = IsNegative(_integer) ? new Integer(ToComplement(_integer)) : this;
			Integer op2 = IsNegative(that._integer) ? new Integer(ToComplement(that._integer)) : that;
			op1.Shorten();
			op2.Shorten();
			// Violent struggle binary search before : turn the division subtraction // theoretically based on the maximum Int64 can be calculated as long as the gap is less than 17 digits can be used, but need to consider what kind of programs more efficient
			if (((op1._integer.Count > 127 || op2._integer.Count > 127) && (op1.ToString().Length - op2.ToString().Length < 10000 || op1.ToString().Length < op2.ToString().Length))) // Only for Long -digit number and close the entry into force in the case of the dividend is less than the divisor are available
			{
				long ans = 0;
				Integer remain = op1.Substract(op2);
				while (remain >= 0)
				{
					ans++;
					remain = remain.Substract(op2);
				}

				return (GetLast(_integer) + GetLast(that._integer) == 9999)
				? new Integer("-" + ans.ToString())
				: new Integer(ans.ToString());
			}

			Integer one = 1;
			Integer left = 0;
			Integer right = op1;

			// Binary search
			while (right.GreaterOrEqual(left))
			{
				Integer x = left.Add(right).Div(2);
				if (x.IsLessOrEqualToQuotient(op1, op2))
				{
					left = x.Add(one);
				}
				else
				{
					right = x.Substract(one);
				}
			}
			right = left.Substract(one);
			return (GetLast(_integer) + GetLast(that._integer) == 9999)
					? new Integer(ToComplement(right._integer))
					: right;
		}

		public Integer Divide(int that)
		{
			if (that == 0)
			{
				throw new DivideByZeroException("Divisor can not be 0 .");
			}
			if ((that > 0 && this.IsNegative()) || (that < 0) && this.IsPositive())
			{
				return new Integer(ToComplement(Div(Math.Abs(that))._integer));
			}
			return Div(Math.Abs(that));
		}

		public Integer Modulus(Integer that)
		{
			Integer abs = Compute<Integer>.AbsoluteValue(that);
			Integer result = Compute<Integer>.AbsoluteValue(this);
			while (result < that)
			{
				result = result - that;
			}
			if ((this > 0 && that < 0) || (this > 0 && that < 0))
				return -result;
			else
				return result;
		}

		#endregion

		#region private

		private bool GreaterOrEqual(Integer that)
		{
			return !IsNegative(Substract(that)._integer);
		}

		private bool IsLessOrEqualToQuotient(Integer op1, Integer op2)
		{
			return op1.GreaterOrEqual(Multiply(op2));
		}

		private static string Format(string input)
		{
			string preProcessed = input.Trim().Replace(" ", "").Replace(",", ""); // Remove spaces and commas
			if (preProcessed.Contains('.'))
			{
				for (int i = preProcessed.IndexOf('.') + 1; i < preProcessed.Length; i++)
				{
					if (preProcessed[i] != '0')
					{
						throw new FormatException("We can not handle decimals .");
					}
				}
				// Decimal places are 0, erase decimal
			}
			bool isNegative = false;
			bool findValid = false;
			StringBuilder va = (preProcessed.Contains('.')) ? new StringBuilder(preProcessed.Split('.')[0]) : new StringBuilder(preProcessed);
			// Remove excess operators
			for (int i = 0; i < va.Length; i++)
			{
				switch (va[i])
				{
					case '+':
						va[i] = ' ';
						break;
					case '-':
						isNegative = !isNegative;
						va[i] = ' ';
						break;
					case ' ':
						break;
					default:
						findValid = true;
						break;
				}
				if (findValid)
				{
					break;
				}
			}
			va = va.Replace(" ", "");
			if (isNegative)
			{
				va.Insert(0, '-');
			}
			return va.ToString();
		}

		private static int GetLast(List<int> value)
		{
			return value[value.Count - 1];
		}

		private static bool IsPositive(List<int> list)
		{
			return (GetLast(list) == 0);
		}

		private static bool IsNegative(List<int> list)
		{
			return (GetLast(list) == 9999);
		}

		private bool IsPositive()
		{
			return (GetLast(_integer) == 0) && (!this.IsZero());
		}

		public bool IsNegative()
		{
			return (GetLast(_integer) == 9999);
		}

		public bool IsZero()
		{
			foreach (var i in _integer)
			{
				if (i != 0)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Translate to complement representation
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns></returns>
		private static List<int> ToComplement(List<int> value)
		{
			List<int> comp = new List<int>(value.Count);
			foreach (var i in value)
			{
				//tmp = 9999 - i;
				comp.Add(9999 - i);
				//((9999 - i)>=0)?comp.Add(9999 - i):comp.Add(19999 - i);
			}
			comp[0] = comp[0] + 1;
			int j = 0;
			// After the last one plus one carry
			while (comp[j] > 9999)
			{
				comp[j] = comp[j] - 10000;
				if (j + 1 < comp.Count)
				{
					comp[j + 1] = comp[j + 1] + 1;
					j++;
				}
			}
			return comp;
		}

		/// <summary>
		/// Generate an array filled with
		/// </summary>
		/// <param name="length">Array length</param>
		/// <param name="negative">Are negative with</param>
		/// <returns></returns>
		private static int[] ArrayGenerator(int length, bool negative)
		{
			int[] ans = new int[length];
			for (int i = 0; i < length; i++)
			{
				ans[i] = negative ? 9999 : 0;
			}
			return ans;
		}


		/// <summary>
		/// Shrinkage source group
		/// </summary>
		private void Shorten()
		{
			int validCount = _integer.Count;
			for (int i = _integer.Count - 1; i >= 0; i--)
			{
				if (_integer[i] == 0 || _integer[i] == 9999)
				{
					validCount--;
				}
				else
				{
					break;
				}
			}
			int valueLength = (validCount / 8 + 2) * 8;
			if (valueLength < _integer.Count)
			{
				_integer.RemoveRange(valueLength, _integer.Count - valueLength);
			}
		}
		#endregion

		#region operator

		public static implicit operator Integer(int integer)
		{
			return new Integer(integer);
		}

		public static implicit operator Integer(long integer)
		{
			return new Integer(integer);
		}

		public static Integer operator -(Integer op1)
		{
			string s = op1.ToString();
			if (s[0] == '-')
				return new Integer(s.Substring(1));
			else
				return new Integer("-" + s);
		}

		public static Integer operator +(Integer op1, Integer op2)
		{
			return op1.Add(op2);
		}

		public static Integer operator -(Integer op1, Integer op2)
		{
			return op1.Substract(op2);
		}

		public static Integer operator *(Integer op1, Integer op2)
		{
			return op1.Multiply(op2);
		}

		public static Integer operator /(Integer op1, Integer op2)
		{
			return op1.Divide(op2);
		}

		public static Integer operator %(Integer op1, Integer op2)
		{
			return op1.Modulus(op2);
		}

		public static bool operator >(Integer op1, Integer op2)
		{
			return op1.Substract(op2).IsPositive();
		}

		public static bool operator >=(Integer op1, Integer op2)
		{
			var ans = op1.Substract(op2);
			return (ans.IsPositive() || ans.IsZero());
		}

		public static bool operator <(Integer op1, Integer op2)
		{
			return op1.Substract(op2).IsNegative();
		}

		public static bool operator <=(Integer op1, Integer op2)
		{
			var ans = op1.Substract(op2);
			return (ans.IsNegative() || ans.IsZero());
		}

		public static bool operator ==(Integer op1, Integer op2)
		{
			// op1 may cause NullReferenceException. It allowed here Thrown
			// ReSharper disable once PossibleNullReferenceException
			if (op1._integer != null)
			{ return op1.Equals(op2); }
			return op2 == null;
		}

		public static bool operator !=(Integer op1, Integer op2)
		{
			return !(op1 == op2);
		}

		#endregion

		#region overrides

		public override bool Equals(object obj)
		{
			return (this.ToString() == obj.ToString());
			//return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override string ToString()
		{
			List<int> v = IsNegative(_integer) ? ToComplement(_integer) : _integer;
			StringBuilder bigIntBuilder = new StringBuilder();
			for (int i = v.Count - 1; i > -1; i--)
			{
				bigIntBuilder.Append(v[i].ToString("D4"));
			}
			// Remove the forefront of 0, negative minus sign
			while (bigIntBuilder.Length > 0 && bigIntBuilder[0] == '0')
			{
				bigIntBuilder.Remove(0, 1);
			}
			if (bigIntBuilder.Length == 0)
			{
				return "0";
			}
			return IsNegative(_integer) ? bigIntBuilder.Insert(0, '-').ToString() : bigIntBuilder.ToString();
		}

		#endregion
	}
}

