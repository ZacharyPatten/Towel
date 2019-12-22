using System;
using Towel;

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
}
