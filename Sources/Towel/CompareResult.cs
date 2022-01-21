namespace Towel;

/// <summary>The result of a comparison between two values.</summary>
public enum CompareResult
{
	/// <summary>The left operand is less than the right operand.</summary>
	Less = -1,
	/// <summary>The left operand is equal to the right operand.</summary>
	Equal = 0,
	/// <summary>The left operand is greater than the right operand.</summary>
	Greater = 1
}
