//#define stackalloc

using System;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<Step, Random>(int count, int minValue, int maxValue, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
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

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		/// <param name="excluded">Values that should be excluded during generation.</param>
		public static void NextUnique<Step, Random>(int count, int minValue, int maxValue, Span<int> excluded, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
		{
			if (count < Math.Sqrt(maxValue - minValue))
			{
				NextUniqueRollTracking(count, minValue, maxValue, excluded, random, step);
			}
			else
			{
				NextUniquePoolTracking(count, minValue, maxValue, excluded, random, step);
			}
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUniqueRollTracking<Step, Random>(int count, int minValue, int maxValue, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
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
			// Algorithm B: O(.5*count^2), Ω(count), ε(.5*count^2)
			Node? head = null;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int roll = random.Do(minValue, maxValue - i);
				if (roll < minValue || roll >= maxValue - i)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node? node = head;
				Node? previous = null;
				while (node is not null && node.Value <= roll) // O(count / 2), Ω(0), ε(count / 2)
				{
					roll++;
					previous = node;
					node = node.Next;
				}
				step.Do(roll);
				if (previous is null)
				{
					head = new Node() { Value = roll, Next = head, };
				}
				else
				{
					previous.Next = new Node() { Value = roll, Next = previous.Next };
				}
			}
#if stackalloc
			}
#endif
		}
		internal class Node
		{
			internal int Value;
			internal Node? Next;
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUniquePoolTracking<Step, Random>(int count, int minValue, int maxValue, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
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
			// Algorithm B: Θ(range + count)
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
				int rollIndex = random.Do(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = span[rollIndex];
				span[rollIndex] = span[--pool];
				step.Do(roll);
			}
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<Random>(int count, int minValue, int maxValue, Action<int> step, Random random = default)
			where Random : struct, IFunc<int, int, int>
		{
			_ = step ?? throw new ArgumentNullException(nameof(step));
			NextUnique<ActionRuntime<int>, Random>(count, minValue, maxValue, random, step);
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		public static int[] NextUnique<Random>(int count, int minValue, int maxValue, Random random = default)
			where Random : struct, IFunc<int, int, int>
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
			int i = 0;
			NextUnique<ActionRuntime<int>, Random>(count, minValue, maxValue, random, new Action<int>(value => values[i++] = value));
			return values;
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		public static int[] NextUniqueRollTracking<Random>(int count, int minValue, int maxValue, Random random = default)
			where Random : struct, IFunc<int, int, int>
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
			int i = 0;
			NextUniqueRollTracking<ActionRuntime<int>, Random>(count, minValue, maxValue, random, new Action<int>(value => values[i++] = value));
			return values;
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		public static int[] NextUniquePoolTracking<Random>(int count, int minValue, int maxValue, Random random = default)
			where Random : struct, IFunc<int, int, int>
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
			int i = 0;
			NextUniquePoolTracking<ActionRuntime<int>, Random>(count, minValue, maxValue, random, new Action<int>(value => values[i++] = value));
			return values;
		}

		public static int[] NextUniquePoolTracking<Random>(int count, int minValue, int maxValue, Span<int> excluded, Random random = default)
			where Random : struct, IFunc<int, int, int>
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
			int i = 0;
			NextUniquePoolTracking<ActionRuntime<int>, Random>(
				count: count,
				minValue: minValue,
				maxValue: maxValue,
				excluded: excluded,
				random: random,
				step: new Action<int>(value => values[i++] = value));
			return values;
		}

		public static void NextUniquePoolTracking<Step, Random>(int count, int minValue, int maxValue, Span<int> excluded, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
		{
			SetHashLinked<int, IntEquate, IntHash> set = new(expectedCount: excluded.Length);
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
			// Algorithm B: Θ(range + count)
			int pool = maxValue - minValue - set.Count;
			Span<int> span =
#if stackalloc
				pool <= 128
				?
				stackalloc int[pool]
				:
#endif
				new int[pool];
			for (int i = 0, j = minValue; i < pool; j++) // Θ(range)
			{
				if (!set.Contains(j))
				{
					span[i++] = j;
				}
			}
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Do(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = span[rollIndex];
				span[rollIndex] = span[--pool];
				step.Do(roll);
			}
		}

		public static int[] NextUniqueRollTracking<Random>(int count, int minValue, int maxValue, Span<int> excluded, Random random = default)
			where Random : struct, IFunc<int, int, int>
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
			int i = 0;
			NextUniqueRollTracking<ActionRuntime<int>, Random>(
				count: count,
				minValue: minValue,
				maxValue: maxValue,
				excluded: excluded,
				random: random,
				step: new Action<int>(value => values[i++] = value));
			return values;
		}

		public static void NextUniqueRollTracking<Step, Random>(int count, int minValue, int maxValue, Span<int> excluded, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
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
			// Algorithm B: O(.5*count^2), Ω(count), ε(.5*count^2)
			Node? head = null;
			int excludeCount = 0;
			foreach (int i in excluded)
			{
				Node? node = head;
				Node? previous = null;
				while (node is not null && node.Value <= i) // O(count / 2), Ω(0), ε(count / 2)
				{
					if (node.Value == i || i < minValue || i >= maxValue)
					{
						goto Continue;
					}
					previous = node;
					node = node.Next;
				}
				excludeCount++;
				if (previous is null)
				{
					head = new Node() { Value = i, Next = head, };
				}
				else
				{
					previous.Next = new Node() { Value = i, Next = previous.Next };
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
				int roll = random.Do(minValue, maxValue - i - excludeCount);
				if (roll < minValue || roll >= maxValue - i - excludeCount)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node? node = head;
				Node? previous = null;
				while (node is not null && node.Value <= roll) // O(count / 2), Ω(0), ε(count / 2)
				{
					roll++;
					previous = node;
					node = node.Next;
				}
				step.Do(roll);
				if (previous is null)
				{
					head = new Node() { Value = roll, Next = head, };
				}
				else
				{
					previous.Next = new Node() { Value = roll, Next = previous.Next };
				}
			}
#if stackalloc
			}
#endif
		}
	}
}