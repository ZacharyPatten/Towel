namespace Towel
{
	/// <summary>Delegate for hash code computation.</summary>
	/// <typeparam name="T">The type of this hash function.</typeparam>
	/// <param name="item">The instance to compute the hash of.</param>
	/// <returns>The computed hash of the given item.</returns>
	public delegate int Hash<T>(T item);

	/// <summary>Static wrapper for the based "object.GetHashCode" fuction.</summary>
	public static class Hash
	{
		/// <summary>Static wrapper for the instance based "object.GetHashCode" fuction.</summary>
		/// <typeparam name="T">The generic type of the hash operation.</typeparam>
		/// <param name="value">The item to get the hash code of.</param>
		/// <returns>The computed hash code using the base GetHashCode instance method.</returns>
		public static int Default<T>(T value) => value.GetHashCode();

		/// <summary>used for hash tables in this project. NOTE: CHANGING THESE VALUES MIGHT BREAK HASH TABLES</summary>
		internal static readonly int[] TableSizes = {
			2, 3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
			1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519,
			21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437, 187751, 225307,
			270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263, 1674319, 2009191,
			2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369, 8639243, 10367097, 12440522, 14928634,
			17914370, 21497255, 25796719, 30956079, 37147314, 44576800, 53492188, 64190659, 77028831, 92434645,
			110921632, 133106028, 159727317, 191672881, 230007578, 276009239, 331211261, 397453722, 476944718,
			572333963, 686801118, 824161776, 988994653, 1186794210, 1424153803, 1708985465, 2050783640, int.MaxValue };
	}
}
