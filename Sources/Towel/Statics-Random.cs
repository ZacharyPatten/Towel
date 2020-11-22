using System;

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
			int[] array = new int[pool];
			for (int i = 0, j = minValue; j < maxValue; i++, j++) // Θ(range)
				array[i] = j;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Do(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = array[rollIndex];
				array[rollIndex] = array[--pool];
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
			int[] values = new int[count];
			int i = 0;
			NextUnique<ActionRuntime<int>, Random>(count, minValue, maxValue, random, new Action<int>(value => values[i++] = value));
			return values;
		}
	}
}
