namespace Towel
{
	/// <summary>Converts a value to a string representation.</summary>
	/// <typeparam name="T">The type of value to convert.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value in string representation.</returns>
	public delegate string ToString<T>(T value);

	/// <summary>Static wrapper for the based "object.ToString" function.</summary>
	public static class ToString
	{
		/// <summary>Static wrapper for the based "object.ToString" fuction.</summary>
		/// <typeparam name="T">The type of value to convert.</typeparam>
		/// <param name="value">The value to convert.</param>
		/// <returns>The value in string representation.</returns>
		public static string Default<T>(T value) => value.ToString();
	}
}
