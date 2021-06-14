namespace Towel
{
	// Note: this is for "inheritdoc" but there is a bug:
	// https://github.com/dotnet/roslyn/issues/54069

	/// <summary>A type that has clonable instances.</summary>
	/// <typeparam name="T">The type of instances that can be cloned.</typeparam>
	public interface ICloneable<T>
	{
		/// <summary>Clones a <typeparamref name="T"/>.</summary>
		/// <returns>A clone of the <typeparamref name="T"/>.</returns>
		public T Clone();
	}
}
