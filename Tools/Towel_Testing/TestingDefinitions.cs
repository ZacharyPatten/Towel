using System;
using Towel;
using static Towel.Syntax;

namespace Towel_Testing
{
	#region TestingRandom

	/// <summary>This is for testing purposes only. It lets you force values on a Random.</summary>
	public class TestingRandom : Random
	{
		const string message = "Testing Error.";
		private readonly Func<int> _algorithm;
		public TestingRandom(Func<int> algorithm) => _algorithm = algorithm;
		public override int Next() => _algorithm();
		public override int Next(int maxValue) => _algorithm();
		public override int Next(int minValue, int maxValue) => _algorithm();
		public override void NextBytes(byte[] buffer) => throw new TowelBugException(message);
		public override void NextBytes(Span<byte> buffer) => throw new TowelBugException(message);
		public override double NextDouble() => throw new TowelBugException(message);
	}

	#endregion

	#region RefNumeric

	public class RefNumeric<T>
	{
		public readonly T _value;
		public RefNumeric(T value) => _value = value;
		public static RefNumeric<T> operator *(RefNumeric<T> a, RefNumeric<T> b) => new RefNumeric<T>(Multiplication(a._value, b._value));
		public static RefNumeric<T> operator +(RefNumeric<T> a, RefNumeric<T> b) => new RefNumeric<T>(Addition(a._value, b._value));
		public static RefNumeric<T> operator /(RefNumeric<T> a, RefNumeric<T> b) => new RefNumeric<T>(Division(a._value, b._value));
		public static RefNumeric<T> operator -(RefNumeric<T> a, RefNumeric<T> b) => new RefNumeric<T>(Subtraction(a._value, b._value));
		public static RefNumeric<T> operator %(RefNumeric<T> a, RefNumeric<T> b) => new RefNumeric<T>(Remainder(a._value, b._value));
		public static implicit operator RefNumeric<T>(T a) => new RefNumeric<T>(a);
		public static implicit operator T(RefNumeric<T> a) => a._value;
		public static implicit operator RefNumeric<T>(int a) => new RefNumeric<T>(Convert<int, T>(a));
		public static bool operator <=(RefNumeric<T> a, RefNumeric<T> b) => LessThanOrEqual(a._value, b._value);
		public static bool operator >=(RefNumeric<T> a, RefNumeric<T> b) => GreaterThanOrEqual(a._value, b._value);
		public static bool operator <(RefNumeric<T> a, RefNumeric<T> b) => LessThan(a._value, b._value);
		public static bool operator >(RefNumeric<T> a, RefNumeric<T> b) => GreaterThan(a._value, b._value);
		public static bool operator ==(RefNumeric<T> a, RefNumeric<T> b) => EqualTo(a._value, b._value);
		public static bool operator !=(RefNumeric<T> a, RefNumeric<T> b) => InequalTo(a._value, b._value);
		public static RefNumeric<T> operator -(RefNumeric<T> a) => new RefNumeric<T>(Negation(a._value));
		public override bool Equals(object obj) => obj is RefNumeric<T> b ? EqualTo(_value, b._value) : false;
		public override int GetHashCode() => _value.GetHashCode();
		public override string ToString() => _value.ToString();

		//public static RefNumeric<T> Zero => new RefNumeric<T>(Convert<int, T>(0));
	}

	#endregion

	#region Numerics

	#if false

	// I may use this in the future... It makes the unit tests smaller, but
	// it also makes them more complicated and harder to read initially...

	public class Numerics
	{
		public delegate void TestMethod<T>();
		static readonly Type[] Types =
		{
			typeof(int),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(RefNumeric<int>),
			typeof(RefNumeric<float>),
			typeof(RefNumeric<double>),
			typeof(RefNumeric<decimal>),
		};
		public static void Enumerate(TestMethod<object> numericTestMethod)
		{
			MethodInfo originalMethodInfo = numericTestMethod.GetMethodInfo();
			MethodInfo genericMethodInfo = originalMethodInfo.GetGenericMethodDefinition();
			foreach (Type type in Types)
			{
				MethodInfo numericMethodInfo = genericMethodInfo.MakeGenericMethod(type);
				numericMethodInfo.Invoke(null, null);
			}
		}
	}

	#endif

	#endregion
}
