namespace Towel
{
	/// <summary>Tries to parse a value from a string.</summary>
	/// <typeparam name="T">The generic type to parse.</typeparam>
	/// <param name="string">The string to parse into a value.</param>
	/// <param name="value">The parse value or default.</param>
	/// <returns>True if the parse succeeded or false if not.</returns>
	public delegate bool TryParse<T>(string @string, out T value);
}
