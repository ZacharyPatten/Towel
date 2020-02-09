using System;

namespace Towel
{
	/// <summary>Struct that allows percentage syntax that will be evaluated at runtime.</summary>
	public struct ChanceSyntax
	{
		/// <summary>The random algorithm currently being used by chance syntax.</summary>
		public static Random Algorithm = new Random();

		//public static ChanceSyntax Chance => default;

#pragma warning disable IDE0060 // Remove unused parameter

		/// <summary>Creates a chance from a percentage that will be evaluated at runtime.</summary>
		/// <param name="percentage">The value of the percentage.</param>
		/// <param name="chance">The chance syntax struct object.</param>
		/// <returns></returns>
		public static bool operator %(double percentage, ChanceSyntax chance) =>
			percentage < 0 ? throw new ArgumentOutOfRangeException(nameof(chance)) :
			percentage > 100 ? throw new ArgumentOutOfRangeException(nameof(chance)) :
			percentage is 100 ? true :
			percentage is 0 ? false :
			Algorithm.NextDouble() < percentage / 100;

#pragma warning restore IDE0060 // Remove unused parameter
	}
}
