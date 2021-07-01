using System;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Next (with exclusions)

		/// <summary>
		/// Generates <paramref name="count"/> random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="TStep">The type of function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="TRandom">The type of random to generation algorithm.</typeparam>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="excluded">Values that should be excluded during generation.</param>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void Next<TStep, TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (count * excluded.Length + .5 * Math.Pow(excluded.Length, 2) < (maxValue - minValue) + count + 2 * excluded.Length)
			{
				NextRollTracking(count, minValue, maxValue, excluded, random, step);
			}
			else
			{
				NextPoolTracking(count, minValue, maxValue, excluded, random, step);
			}
		}

		/// <inheritdoc cref="Next{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"></inheritdoc>
		public static void NextRollTracking<TStep, TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			// Algorithm B: O(count * excluded.Length + .5*excluded.Length^2)
			Node<int>? head = null;
			int excludeCount = 0;
			foreach (int i in excluded) // Θ(excluded.Length)
			{
				if (i < minValue || i >= maxValue)
				{
					continue;
				}
				Node<int>? node = head;
				Node<int>? previous = null;
				while (node is not null && node.Value <= i) // O(.5*excluded.Length), Ω(0), ε(.5*excluded.Length)
				{
					if (node.Value == i)
					{
						goto Continue;
					}
					previous = node;
					node = node.Next;
				}
				excludeCount++;
				if (previous is null)
				{
					head = new Node<int>() { Value = i, Next = head, };
				}
				else
				{
					previous.Next = new Node<int>() { Value = i, Next = previous.Next };
				}
			Continue:
				continue;
			}
			if (excludeCount >= maxValue - minValue)
			{
				throw new ArgumentException($"{nameof(excluded)}.{nameof(excluded.Length)} >= {nameof(count)}");
			}
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int roll = random.Invoke(minValue, maxValue - i - excludeCount);
				if (roll < minValue || roll >= maxValue - i - excludeCount)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node<int>? node = head;
				while (node is not null && node.Value <= roll) // O(excluded.Length), Ω(0)
				{
					roll++;
					node = node.Next;
				}
				step.Invoke(roll);
			}
		}

		/// <inheritdoc cref="Next{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"></inheritdoc>
		public static void NextPoolTracking<TStep, TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			// Algorithm: Θ(range + count + 2*excluded.Length)
			SetHashLinked<int, Int32Equate, Int32Hash> set = new(expectedCount: excluded.Length); // Θ(excluded)
			foreach (int value in excluded)
			{
				if (minValue <= value && value < maxValue)
				{
					set.TryAdd(value);
				}
			}
			if (set.Count >= maxValue - minValue)
			{
				throw new ArgumentException($"{nameof(excluded)}.{nameof(excluded.Length)} >= {nameof(maxValue)} - {nameof(minValue)}");
			}
			int pool = maxValue - minValue - set.Count;
			Span<int> span = new int[pool];
			for (int i = 0, j = minValue; i < pool; j++) // Θ(range + excluded.Length)
			{
				if (!set.Contains(j))
				{
					span[i++] = j;
				}
			}
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Invoke(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = span[rollIndex];
				step.Invoke(roll);
			}
		}

		#region Overloads

		/// <inheritdoc cref="Next{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"></inheritdoc>
		/// <param name="step">The function to perform on each randomly generated value.</param>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
		public static void Next<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, Action<int> step, TRandom random = default)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			Next<SAction<int>, TRandom>(count, minValue, maxValue, excluded, random, step);
		}

		/// <inheritdoc cref="Next{TRandom}(int, int, int, ReadOnlySpan{int}, Action{int}, TRandom)"></inheritdoc>
		public static void NextRollTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, Action<int> step, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextRollTracking<SAction<int>, TRandom>(count, minValue, maxValue, excluded, random, step);
		}

		/// <inheritdoc cref="Next{TRandom}(int, int, int, ReadOnlySpan{int}, Action{int}, TRandom)"></inheritdoc>
		public static void NextPoolTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, Action<int> step, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextPoolTracking<SAction<int>, TRandom>(count, minValue, maxValue, excluded, random, step);
		}

		/// <inheritdoc cref="Next{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"></inheritdoc>
		/// <returns>The randomly generated values.</returns>
		public static int[] Next<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			int[] values = new int[count];
			Next<FillArray<int>, TRandom>(count, minValue, maxValue, excluded, random, values);
			return values;
		}

		/// <inheritdoc cref="Next{TRandom}(int, int, int, ReadOnlySpan{int}, TRandom)"/>
		public static int[] NextRollTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			int[] values = new int[count];
			NextRollTracking<FillArray<int>, TRandom>(count, minValue, maxValue, excluded, random, values);
			return values;
		}

		/// <inheritdoc cref="Next{TRandom}(int, int, int, ReadOnlySpan{int}, TRandom)"/>
		public static int[] NextPoolTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			int[] values = new int[count];
			NextPoolTracking<FillArray<int>, TRandom>(count, minValue, maxValue, excluded, random, values);
			return values;
		}

		#endregion

		#endregion

		#region NextUnique

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="TStep">The type of function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="TRandom">The type of random to generation algorithm.</typeparam>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<TStep, TRandom>(int count, int minValue, int maxValue, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (count < Math.Sqrt(maxValue - minValue))
			{
				NextUniqueRollTracking(count, minValue, maxValue, random, step);
			}
			else
			{
				NextUniquePoolTracking(count, minValue, maxValue, random, step);
			}
		}

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, TRandom, TStep)"/>
		public static void NextUniqueRollTracking<TStep, TRandom>(int count, int minValue, int maxValue, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
#if stackalloc
			if (count <= 128)
			{
				// Algorithm B: Θ(.5*count^2)
				Span<int> span = stackalloc int[count];
				for (int i = 0; i < count; i++) // Θ(count)
				{
					int roll = random.Do(minValue, maxValue - i);
					if (roll < minValue || roll >= maxValue - i)
					{
						throw new ArgumentException("The Random provided returned a value outside the requested range.");
					}
					int index = 0;
					for (; index < i && span[index] <= roll; index++)
					{
						roll++;
					}
					step.Do(roll);
					for (int j = i; j > index; j--)
					{
						span[j] = span[j - 1];
					}
					span[index] = roll;
				}
			}
			else
			{
#endif
			// Algorithm: O(.5*count^2), Ω(count), ε(.5*count^2)
			Node<int>? head = null;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int roll = random.Invoke(minValue, maxValue - i);
				if (roll < minValue || roll >= maxValue - i)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node<int>? node = head;
				Node<int>? previous = null;
				while (node is not null && node.Value <= roll) // O(.5*count), Ω(0), ε(.5*count)
				{
					roll++;
					previous = node;
					node = node.Next;
				}
				step.Invoke(roll);
				if (previous is null)
				{
					head = new Node<int>() { Value = roll, Next = head, };
				}
				else
				{
					previous.Next = new Node<int>() { Value = roll, Next = previous.Next };
				}
			}
#if stackalloc
			}
#endif
		}

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, TRandom, TStep)"/>
		public static void NextUniquePoolTracking<TStep, TRandom>(int count, int minValue, int maxValue, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			// Algorithm: Θ(range + count)
			int pool = maxValue - minValue;
			Span<int> span =
#if stackalloc
				pool <= 128
				?
				stackalloc int[pool]
				:
#endif
				new int[pool];

			for (int i = 0, j = minValue; j < maxValue; i++, j++) // Θ(range)
			{
				span[i] = j;
			}
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Invoke(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = span[rollIndex];
				span[rollIndex] = span[--pool];
				step.Invoke(roll);
			}
		}

		#region Overloads

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, TRandom, TStep)"></inheritdoc>
		/// <param name="step">The function to perform on each randomly generated value.</param>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
		public static void NextUnique<TRandom>(int count, int minValue, int maxValue, Action<int> step, TRandom random = default)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextUnique<SAction<int>, TRandom>(count, minValue, maxValue, random, step);
		}

		/// <inheritdoc cref="NextUnique{TRandom}(int, int, int, Action{int}, TRandom)"></inheritdoc>
		public static void NextUniqueRollTracking<TRandom>(int count, int minValue, int maxValue, Action<int> step, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextUniqueRollTracking<SAction<int>, TRandom>(count, minValue, maxValue, random, step);
		}

		/// <inheritdoc cref="NextUnique{TRandom}(int, int, int, Action{int}, TRandom)"></inheritdoc>
		public static void NextUniquePoolTracking<TRandom>(int count, int minValue, int maxValue, Action<int> step, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextUniquePoolTracking<SAction<int>, TRandom>(count, minValue, maxValue, random, step);
		}

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, TRandom, TStep)"></inheritdoc>
		/// <returns>The randomly generated values.</returns>
		public static int[] NextUnique<TRandom>(int count, int minValue, int maxValue, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int[] values = new int[count];
			NextUnique<FillArray<int>, TRandom>(count, minValue, maxValue, random, values);
			return values;
		}

		/// <inheritdoc cref="NextUnique{TRandom}(int, int, int, TRandom)"></inheritdoc>
		public static int[] NextUniqueRollTracking<TRandom>(int count, int minValue, int maxValue, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int[] values = new int[count];
			NextUniqueRollTracking<FillArray<int>, TRandom>(count, minValue, maxValue, random, values);
			return values;
		}

		/// <inheritdoc cref="NextUnique{TRandom}(int, int, int, TRandom)"></inheritdoc>
		public static int[] NextUniquePoolTracking<TRandom>(int count, int minValue, int maxValue, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int[] values = new int[count];
			NextUniquePoolTracking<FillArray<int>, TRandom>(count, minValue, maxValue, random, values);
			return values;
		}

		#endregion

		#endregion

		#region NextUnique (with exclusions)

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="TStep">The type of function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="TRandom">The type of random to generation algorithm.</typeparam>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="excluded">Values that should be excluded during generation.</param>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<TStep, TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (count + excluded.Length < Math.Sqrt(maxValue - minValue))
			{
				NextUniqueRollTracking(count, minValue, maxValue, excluded, random, step);
			}
			else
			{
				NextUniquePoolTracking(count, minValue, maxValue, excluded, random, step);
			}
		}

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"/>
		public static void NextUniqueRollTracking<TStep, TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
#if stackalloc
			if (count + excluded.Length <= 128)
			{
				// Algorithm B: Θ(.5*count^2)
				int excludeCount = 0;
				Span<int> span = stackalloc int[count + excluded.Length];
				{
					foreach (int exclude in excluded)
					{
						if (minValue <= exclude && exclude < maxValue)
						{
							int index = 0;
							for (; index < excludeCount && exclude > span[index]; index++)
							{
								if (span[index] == exclude)
								{
									goto Continue;
								}
							}
							for (int j = excludeCount + 1; j > index; j--)
							{
								span[j] = span[j - 1];
							}
							span[excludeCount++] = exclude;
						}
					Continue:
						continue;
					}
				}
				if (maxValue - minValue - excludeCount < count)
				{
					throw new ArgumentException($"{nameof(maxValue)} - {nameof(minValue)} - {nameof(excluded)}.Length [{maxValue - minValue - excludeCount < count}] < {nameof(count)} [{count}]");
				}
				for (int i = 0; i < count; i++) // Θ(count)
				{
					int roll = random.Do(minValue, maxValue - i + excludeCount);
					if (roll < minValue || roll >= maxValue - i + excludeCount)
					{
						throw new ArgumentException("The Random provided returned a value outside the requested range.");
					}
					int index = 0;
					for (; index < i + excludeCount && span[index] <= roll; index++)
					{
						roll++;
					}
					step.Do(roll);
					for (int j = i + excludeCount; j > index; j--)
					{
						span[j] = span[j - 1];
					}
					span[index] = roll;
				}
			}
			else
			{
#endif
			// Algorithm B: O(.5*(count + excluded.Length)^2 + .5*excluded.Length^2), Ω(count + excluded.Length), ε(.5*(count + excluded.Length)^2 + .5*excluded.Length^2)
			Node<int>? head = null;
			int excludeCount = 0;
			foreach (int i in excluded) // Θ(excluded.Length)
			{
				if (i < minValue || i >= maxValue)
				{
					continue;
				}
				Node<int>? node = head;
				Node<int>? previous = null;
				while (node is not null && node.Value <= i) // O(.5*excluded.Length), Ω(0), ε(.5*excluded.Length)
				{
					if (node.Value == i)
					{
						goto Continue;
					}
					previous = node;
					node = node.Next;
				}
				excludeCount++;
				if (previous is null)
				{
					head = new Node<int>() { Value = i, Next = head, };
				}
				else
				{
					previous.Next = new Node<int>() { Value = i, Next = previous.Next };
				}
			Continue:
				continue;
			}
			if (maxValue - minValue - excludeCount < count)
			{
				throw new ArgumentException($"{nameof(maxValue)} - {nameof(minValue)} - {nameof(excluded)}.Length [{maxValue - minValue - excludeCount < count}] < {nameof(count)} [{count}]");
			}
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int roll = random.Invoke(minValue, maxValue - i - excludeCount);
				if (roll < minValue || roll >= maxValue - i - excludeCount)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node<int>? node = head;
				Node<int>? previous = null;
				while (node is not null && node.Value <= roll) // O(.5*(count + excluded.Length)), Ω(0), ε(.5*(count + excluded.Length))
				{
					roll++;
					previous = node;
					node = node.Next;
				}
				step.Invoke(roll);
				if (previous is null)
				{
					head = new Node<int>() { Value = roll, Next = head, };
				}
				else
				{
					previous.Next = new Node<int>() { Value = roll, Next = previous.Next };
				}
			}
#if stackalloc
			}
#endif
		}

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"/>
		public static void NextUniquePoolTracking<TStep, TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default, TStep step = default)
			where TStep : struct, IAction<int>
			where TRandom : struct, IFunc<int, int, int>
		{
			// Algorithm: Θ(range + count + 2*excluded.Length)
			SetHashLinked<int, Int32Equate, Int32Hash> set = new(expectedCount: excluded.Length); // Θ(excluded)
			foreach (int value in excluded)
			{
				if (minValue <= value && value < maxValue)
				{
					set.TryAdd(value);
				}
			}
			if (maxValue - minValue - set.Count < count)
			{
				throw new ArgumentException($"{nameof(maxValue)} - {nameof(minValue)} - {nameof(excluded)}.Length [{maxValue - minValue - set.Count < count}] < {nameof(count)} [{count}]");
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int pool = maxValue - minValue - set.Count;
			Span<int> span =
#if stackalloc
				pool <= 128
				?
				stackalloc int[pool]
				:
#endif
				new int[pool];
			for (int i = 0, j = minValue; i < pool; j++) // Θ(range + excluded.Length)
			{
				if (!set.Contains(j))
				{
					span[i++] = j;
				}
			}
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Invoke(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = span[rollIndex];
				span[rollIndex] = span[--pool];
				step.Invoke(roll);
			}
		}

		#region Overloads

		/// <inheritdoc cref="NextUnique{TStep, TRandom}(int, int, int, ReadOnlySpan{int}, TRandom, TStep)"></inheritdoc>
		/// <param name="step">The function to perform on each randomly generated value.</param>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
		public static void NextUnique<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, Action<int> step, TRandom random = default)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextUnique<SAction<int>, TRandom>(count, minValue, maxValue, excluded, random, step);
		}

		/// <inheritdoc cref="NextUnique{Random}(int, int, int, ReadOnlySpan{int}, Action{int}, Random)"></inheritdoc>
		public static void NextUniqueRollTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, Action<int> step, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextUniqueRollTracking<SAction<int>, TRandom>(count, minValue, maxValue, excluded, random, step);
		}

		/// <inheritdoc cref="NextUnique{Random}(int, int, int, ReadOnlySpan{int}, Action{int}, Random)"></inheritdoc>
		public static void NextUniquePoolTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, Action<int> step, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (step is null)
			{
				throw new ArgumentNullException(nameof(step));
			}
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			NextUniquePoolTracking<SAction<int>, TRandom>(count, minValue, maxValue, excluded, random, step);
		}

		/// <inheritdoc cref="NextUnique{Step, Random}(int, int, int, ReadOnlySpan{int}, Random, Step)"/>
		/// <returns>The randomly generated values.</returns>
		public static int[] NextUnique<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int[] values = new int[count];
			NextUnique<FillArray<int>, TRandom>(count, minValue, maxValue, excluded, random, values);
			return values;
		}

		/// <inheritdoc cref="NextUnique{Random}(int, int, int, ReadOnlySpan{int}, Random)"/>
		public static int[] NextUniqueRollTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int[] values = new int[count];
			NextUniqueRollTracking<FillArray<int>, TRandom>(count, minValue, maxValue, excluded, random, values);
			return values;
		}

		/// <inheritdoc cref="NextUnique{Random}(int, int, int, ReadOnlySpan{int}, Random)"/>
		public static int[] NextUniquePoolTracking<TRandom>(int count, int minValue, int maxValue, ReadOnlySpan<int> excluded, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			int[] values = new int[count];
			NextUniquePoolTracking<FillArray<int>, TRandom>(count, minValue, maxValue, excluded, random, values);
			return values;
		}

		#endregion

		#endregion
	}
}
