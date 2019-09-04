using System;
using System.Collections.Generic;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Status of data structure iteration.</summary>
	[Serializable]
	public enum StepStatus
	{
		/// <summary>Continue normal iteration.</summary>
		Continue = 0,
		/// <summary>Iteration cancelation.</summary>
		Break = 1,
	};

	#region <T>

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	public delegate void Step<T>(T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	public delegate void StepRef<T>(ref T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepBreak<T>(T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepRefBreak<T>(ref T current);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step">The foreach function to perform on each iteration.</param>
	public delegate void Stepper<T>(Step<T> step);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step">The foreach function to perform on each iteration.</param>
	public delegate void StepperRef<T>(StepRef<T> step);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step">The foreach function to perform on each iteration.</param>
	public delegate StepStatus StepperBreak<T>(StepBreak<T> step);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step">The foreach function to perform on each iteration.</param>
	public delegate StepStatus StepperRefBreak<T>(StepRefBreak<T> step);

	#endregion

	#region <T1, T2>

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	public delegate void Step<T1, T2>(T1 a, T2 b);

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	public delegate void StepRef<T1, T2>(ref T1 a, ref T2 b);

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepBreak<T1, T2>(T1 a, T2 b);

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepRefBreak<T1, T2>(ref T1 a, ref T2 b);

	/// <summary>Delegate for stepping through a collection.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="step">The action to perform on every step.</param>
	public delegate void Stepper<T1, T2>(Step<T1, T2> step);

	/// <summary>Delegate for stepping through a collection.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="step">The action to perform on every step.</param>
	public delegate void StepperRef<T1, T2>(StepRef<T1, T2> step);

	/// <summary>Delegate for stepping through a collection.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="step">The action to perform on every step.</param>
	public delegate void StepperBreak<T1, T2>(StepBreak<T1, T2> step);

	/// <summary>Delegate for stepping through a collection.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="step">The action to perform on every step.</param>
	public delegate void StepperRefBreak<T1, T2>(StepRefBreak<T1, T2> step);

	#endregion

	/// <summary>Extension methods.</summary>
	public static class Stepper
	{
		/// <summary>Appends values to the stepper.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to append to.</param>
		/// <param name="values">The values to append to the stepper.</param>
		/// <returns>The resulting stepper with the appended values.</returns>
		public static Stepper<T> Append<T>(this Stepper<T> stepper, params T[] values) =>
			stepper.Concat(values.ToStepper());

		/// <summary>Builds a stepper from values.</summary>
		/// <typeparam name="T">The generic type of the stepper to build.</typeparam>
		/// <param name="values">The values to build the stepper from.</param>
		/// <returns>The resulting stepper function for the provided values.</returns>
		public static Stepper<T> Build<T>(params T[] values) =>
			values.ToStepper();

		/// <summary>Concatenates steppers.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The first stepper of the contactenation.</param>
		/// <param name="otherSteppers">The other steppers of the concatenation.</param>
		/// <returns>The concatenated steppers as a single stepper.</returns>
		public static Stepper<T> Concat<T>(this Stepper<T> stepper, params Stepper<T>[] otherSteppers) =>
			step =>
			{
				stepper(step);
				foreach (Stepper<T> otherStepper in otherSteppers)
				{
					otherStepper(step);
				}
			};

		/// <summary>Filters a stepper using a where predicate.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to filter.</param>
		/// <param name="predicate">The predicate of the where filter.</param>
		/// <returns>The filtered stepper.</returns>
		public static Stepper<T> Where<T>(this Stepper<T> stepper, Predicate<T> predicate) =>
			step =>
			{
				stepper(x =>
				{
					if (predicate(x))
					{
						step(x);
					}
				});
			};

		/// <summary>Creates a stepper from an iteration pattern.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="iterations">The number of times to iterate.</param>
		/// <param name="func">The generation pattern for the iteration.</param>
		/// <returns>A stepper build from the iteration pattern.</returns>
		public static Stepper<T> Iterate<T>(int iterations, Func<int, T> func) =>
			step =>
			{
				for (int i = 0; i < iterations; i++)
				{
					step(func(i));
				}
			};

		/// <summary>Converts an IEnumerable into a stepper delegate./></summary>
		/// <typeparam name="T">The generic type being iterated.</typeparam>
		/// <param name="enumerable">The Ienumerable to convert.</param>
		/// <returns>The stepper delegate comparable to the IEnumerable provided.</returns>
		public static Stepper<T> ToStepper<T>(this IEnumerable<T> enumerable) =>
			step =>
			{
				foreach (T x in enumerable)
				{
					step(x);
				}
			};

		/// <summary>Converts the stepper into an array.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to convert.</param>
		/// <returns>The array created from the stepper.</returns>
		public static T[] ToArray<T>(this Stepper<T> stepper)
		{
			int count = stepper.Count();
			T[] array = new T[count];
			int i = 0;
			stepper(x => array[i++] = x);
			return array;
		}

		/// <summary>Counts the number of items in the stepper.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to count the items of.</param>
		/// <returns>The number of items in the stepper.</returns>
		public static int Count<T>(this Stepper<T> stepper)
		{
			int count = 0;
			stepper(step => count++);
			return count;
		}

		/// <summary>Reduces the stepper to be every nth value.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to reduce.</param>
		/// <param name="nth">Represents the values to reduce the stepper to; "5" means every 5th value.</param>
		/// <returns>The reduced stepper function.</returns>
		public static Stepper<T> EveryNth<T>(this Stepper<T> stepper, int nth)
		{
			if (stepper == null)
			{
				throw new ArgumentNullException(nameof(stepper));
			}
			if (nth <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(nth), nth, "!(" + nameof(nth) + " > 0)");
			}
			int i = 1;
			return step =>
			{
				stepper(x =>
				{
					if (i == nth)
					{
						step(x);
						i = 1;
					}
					else
					{
						i++;
					}
				});
			};
		}

		/// <summary>Determines if the data contains any duplicates.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The stepper function for the data.</param>
		/// <param name="equate">An equality function for the data</param>
		/// <param name="hash">A hashing function for the data.</param>
		/// <returns>True if the data contains duplicates. False if not.</returns>
		public static bool ContainsDuplicates<T>(this StepperBreak<T> stepper, Equate<T> equate, Hash<T> hash)
		{
			bool duplicateFound = false;
			SetHashArray<T> set = new SetHashArray<T>(equate, hash);
			stepper(x =>
			{
				if (set.Contains(x))
				{
					duplicateFound = true;
					return StepStatus.Break;
				}
				else
				{
					set.Add(x);
					return StepStatus.Continue;
				}
			});
			return duplicateFound;
		}

		/// <summary>Determines if the data contains any duplicates.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The stepper function for the data.</param>
		/// <param name="equate">An equality function for the data</param>
		/// <param name="hash">A hashing function for the data.</param>
		/// <returns>True if the data contains duplicates. False if not.</returns>
		/// <remarks>Use the StepperBreak overload if possible. It is more effiecient.</remarks>
		public static bool ContainsDuplicates<T>(this Stepper<T> stepper, Equate<T> equate, Hash<T> hash)
		{
			bool duplicateFound = false;
			SetHashArray<T> set = new SetHashArray<T>(equate, hash);
			stepper(x =>
			{
				if (set.Contains(x))
				{
					duplicateFound = true;
				}
				else
				{
					set.Add(x);
				}
			});
			return duplicateFound;
		}

		/// <summary>Determines if the data contains any duplicates.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The stepper function for the data.</param>
		/// <returns>True if the data contains duplicates. False if not.</returns>
		public static bool ContainsDuplicates<T>(this StepperBreak<T> stepper) =>
			ContainsDuplicates(stepper, Equate.Default, Hash.Default);

		/// <summary>Determines if the data contains any duplicates.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The stepper function for the data.</param>
		/// <returns>True if the data contains duplicates. False if not.</returns>
		/// <remarks>Use the StepperBreak overload if possible. It is more effiecient.</remarks>
		public static bool ContainsDuplicates<T>(this Stepper<T> stepper) =>
			ContainsDuplicates(stepper, Equate.Default, Hash.Default);
	}
}
