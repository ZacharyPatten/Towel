namespace Towel
{
	/// <summary>Unary operator for criteria testing.</summary>
	/// <typeparam name="T">The type of item for the predicate.</typeparam>
	/// <param name="item">The item of the predicate.</param>
	/// <returns>True if the item passes the criteria test. False if not.</returns>
	[System.Serializable]
	public delegate bool Predicate<T>(T item);
}
