namespace Towel
{
	/// <summary>Delegate for serializing an instance of a generic type.</summary>
	/// <typeparam name="T">The type of instance for the serialization.</typeparam>
	/// <param name="instance">The instance of the serialization.</param>
	/// <returns>A string representing the serialized instance.</returns>
	[System.Serializable]
	public delegate string Serialize<T>(T instance);

	/// <summary>Delegate for deserializing a string to a generic type.</summary>
	/// <typeparam name="T">The type of instance for the deserialization.</typeparam>
	/// <param name="serialization">The string of the deserialization.</param>
	/// <returns>The instance of the generic type from deserialization.</returns>
	[System.Serializable]
	public delegate T Deserialize<T>(string serialization);
}
