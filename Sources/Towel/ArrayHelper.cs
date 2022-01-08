namespace Towel
{
	/// <summary>Static helper methods for arrays.</summary>
	public static class ArrayHelper
	{
		/// <summary>Constructs a new 1D array from a sequence of ranges.</summary>
		/// <returns>A new 1D array from the sequence of ranges.</returns>
		/// <inheritdoc cref="NewFromRanges{T, TSelect}(TSelect, Range[])"/>
		public static int[] NewFromRanges(params Range[] ranges) =>
			NewFromRanges<int, Identity<int>>(default, ranges);

		/// <inheritdoc cref="NewFromRanges{T, TSelect}(TSelect, Range[])"/>
		public static T[] NewFromRanges<T>(Func<int, T> select, params Range[] ranges) =>
			NewFromRanges<T, SFunc<int, T>>(select, ranges);

		/// <summary>Constructs a new 1D array from a sequence of ranges and a selction method.</summary>
		/// <typeparam name="T">The element type of the array to construct.</typeparam>
		/// <typeparam name="TSelect">The type of method for selecting <typeparamref name="T"/> from <see cref="int"/> values.</typeparam>
		/// <param name="select">The method for selecting <typeparamref name="T"/> from <see cref="int"/> values.</param>
		/// <param name="ranges">The ranges to construct the array from.</param>
		/// <returns>A new 1D array from the sequence of ranges and the selction method.</returns>
		public static T[] NewFromRanges<T, TSelect>(TSelect select, params Range[] ranges)
			where TSelect : struct, IFunc<int, T>
		{
			if (ranges is null) throw new ArgumentNullException(nameof(ranges));
			int length = 0;
			foreach (var range in ranges)
			{
				if (range.Start.IsFromEnd) throw new ArgumentException(nameof(ranges));
				if (range.End.IsFromEnd) throw new ArgumentException(nameof(ranges));
				if (range.Start.Value < range.End.Value)
				{
					length += range.End.Value - range.Start.Value;
				}
				else
				{
					length += range.Start.Value - range.End.Value;
				}
			}
			if (length is 0)
			{
				return Array.Empty<T>();
			}
			T[] result = new T[length];
			int index = 0;
			foreach (var range in ranges)
			{
				if (range.Start.Value < range.End.Value)
				{
					for (int i = range.Start.Value; i < range.End.Value; i++)
					{
						result[index++] = select.Invoke(i);
					}
				}
				else
				{
					for (int i = range.Start.Value; i > range.End.Value; i--)
					{
						result[index++] = select.Invoke(i);
					}
				}
			}
			return result;
		}
	}
}
