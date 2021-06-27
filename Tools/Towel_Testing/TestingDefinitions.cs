using System;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	#region TestingRandom

	/// <summary>This is for testing purposes only. It lets you force values on a Random.</summary>
	public class TestingRandom : Random
	{
		private readonly Func<int> _algorithm;
		public TestingRandom(Func<int> algorithm) => _algorithm = algorithm;
		public override int Next() => _algorithm();
		public override int Next(int maxValue) => _algorithm();
		public override int Next(int minValue, int maxValue) => _algorithm();
		public override void NextBytes(byte[] buffer) => throw new NotImplementedException();
		public override void NextBytes(Span<byte> buffer) => throw new NotImplementedException();
		public override double NextDouble() => throw new NotImplementedException();
	}

	#endregion

	#region Ref

	/// <summary>Represents a boxed struct value.</summary>
	/// <typeparam name="T">The type of value to be boxed.</typeparam>
	public class Ref<T>
		where T : struct
	{
		public T Value { get; set; }
		private Ref(T value) => Value = value;
		public static Ref<T> operator *(Ref<T> a, Ref<T> b) => Multiplication(a.Value, b.Value);
		public static Ref<T> operator +(Ref<T> a, Ref<T> b) => Addition(a.Value, b.Value);
		public static Ref<T> operator /(Ref<T> a, Ref<T> b) => Division(a.Value, b.Value);
		public static Ref<T> operator -(Ref<T> a, Ref<T> b) => Subtraction(a.Value, b.Value);
		public static Ref<T> operator %(Ref<T> a, Ref<T> b) => Remainder(a.Value, b.Value);
		public static implicit operator Ref<T>(T value) => new(value);
		public static implicit operator T(Ref<T> a) => a.Value;
		public static bool operator <=(Ref<T> a, Ref<T> b) => LessThanOrEqual(a.Value, b.Value);
		public static bool operator >=(Ref<T> a, Ref<T> b) => GreaterThanOrEqual(a.Value, b.Value);
		public static bool operator <(Ref<T> a, Ref<T> b) => LessThan(a.Value, b.Value);
		public static bool operator >(Ref<T> a, Ref<T> b) => GreaterThan(a.Value, b.Value);
		public static bool operator ==(Ref<T> a, Ref<T> b) => Equate(a.Value, b.Value);
		public static bool operator ==(Ref<T> a, T b) => Equate(a.Value, b);
		public static bool operator ==(T a, Ref<T> b) => Equate(a, b.Value);
		public static bool operator !=(Ref<T> a, Ref<T> b) => Inequate(a.Value, b.Value);
		public static bool operator !=(Ref<T> a, T b) => Inequate(a.Value, b);
		public static bool operator !=(T a, Ref<T> b) => Inequate(a, b.Value);
		public static Ref<T> operator -(Ref<T> a) => Negation(a.Value);
		public override bool Equals(object? obj) => obj is Ref<T> b && Equate(Value, b.Value);
		public override int GetHashCode() => Value.GetHashCode();
		public override string? ToString() => Value.ToString();

		static Ref()
		{
#warning TODO: need to add conversion routing to "Convert<T>"
			Constant<Ref<T>>.Zero = Constant<T>.Zero;
			Constant<Ref<T>>.One = Constant<T>.One;
			Constant<Ref<T>>.Two = Constant<T>.Two;
			Constant<Ref<T>>.Three = Constant<T>.Three;
			Constant<Ref<T>>.Four = Constant<T>.Four;
			Constant<Ref<T>>.Ten = Constant<T>.Ten;
			Constant<Ref<T>>.NegativeOne = Constant<T>.NegativeOne;
			Constant<Ref<T>>.Pi = Constant<T>.Pi;
			Constant<Ref<T>>.Pi2 = Constant<T>.Pi2; 
			Constant<Ref<T>>.PiOver2 = Constant<T>.PiOver2;
			Constant<Ref<T>>.Pi3Over2 = Constant<T>.Pi3Over2;
			Constant<Ref<T>>.FourOverPiSquared = Constant<T>.FourOverPiSquared;
			Constant<Ref<T>>.FourOverπSquared = Constant<T>.FourOverπSquared;
			Constant<Ref<T>>.Negative4OverPiSquared = Constant<T>.Negative4OverPiSquared;
			Constant<Ref<T>>.Negative4OverπSquared = Constant<T>.Negative4OverπSquared;
		}
	}

	#endregion
}
