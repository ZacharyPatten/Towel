using System;
using System.Collections.Generic;
using static Towel.Syntax;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Status of data structure iteration.</summary>
	[Serializable]
	public enum StepStatus
	{
		#region Members

		/// <summary>Stepper was not broken.</summary>
		Continue = 0,
		/// <summary>Stepper was broken.</summary>
		Break = 1,

		#endregion
	};

	#region 1 Dimensional

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

	#region 2 Dimensional

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
	public delegate void StepRef1<T1, T2>(ref T1 a, T2 b);

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	public delegate void StepRef2<T1, T2>(T1 a, ref T2 b);

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

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepRefBreak1<T1, T2>(ref T1 a, T2 b);

	/// <summary>Delegate for an action to perform while stepping.</summary>
	/// <typeparam name="T1">The type of the object to step on.</typeparam>
	/// <typeparam name="T2">The type of the object to step on.</typeparam>
	/// <param name="a">The first component of the step.</param>
	/// <param name="b">The second component of the step.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepRefBreak2<T1, T2>(T1 a, ref T2 b);

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
	public static class Step
	{
		#region Members

		/// <summary>Adds a step to the gaps (in-betweens) of another step funtion.</summary>
		/// <typeparam name="T">The generic type of the step function.</typeparam>
		/// <param name="step">The step to add a gap step to.</param>
		/// <param name="gapStep">The step to perform in the gaps.</param>
		/// <returns>The combined step + gapStep function.</returns>
		public static Step<T> Gaps<T>(this Step<T> step, Step<T> gapStep)
		{
			bool first = true;
			return
				x =>
				{
					if (!first)
					{
						gapStep(x);
					}
					step(x);
					first = false;
				};
		}

		#endregion
	}

	/// <summary>Extension methods.</summary>
	public static class Stepper
	{
		#region Members

		/// <summary>Converts the values in this stepper to another type.</summary>
		/// <typeparam name="A">The generic type of the values of the original stepper.</typeparam>
		/// <typeparam name="B">The generic type of the values to convert the stepper into.</typeparam>
		/// <param name="stepper">The stepper to convert.</param>
		/// <param name="func">The conversion function.</param>
		/// <returns>The converted stepper.</returns>
		public static Stepper<B> Convert<A, B>(this Stepper<A> stepper, Func<A, B> func)
		{
			return b =>
				stepper(a =>
				{
					b(func(a));
				});
		}

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
				stepper(x =>
				{
					if (predicate(x))
					{
						step(x);
					}
				});

		/// <summary>Steps through a set number of integers.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="iterations">The number of times to iterate.</param>
		/// <param name="step">The step function.</param>
		public static void Iterate(int iterations, Step<int> step)
		{
			for (int i = 0; i < iterations; i++)
			{
				step(i);
			}
		}

		/// <summary>Converts an IEnumerable into a stepper delegate./></summary>
		/// <typeparam name="T">The generic type being iterated.</typeparam>
		/// <param name="iEnumerable">The IEnumerable to convert.</param>
		/// <returns>The stepper delegate comparable to the IEnumerable provided.</returns>
		public static Stepper<T> ToStepper<T>(this IEnumerable<T> iEnumerable) =>
			step =>
			{
				foreach (T value in iEnumerable)
				{
					step(value);
				}
			};

		/// <summary>Converts an IEnumerable into a stepper delegate./></summary>
		/// <typeparam name="T">The generic type being iterated.</typeparam>
		/// <param name="iEnumerable">The IEnumerable to convert.</param>
		/// <returns>The stepper delegate comparable to the IEnumerable provided.</returns>
		public static StepperBreak<T> ToStepperBreak<T>(this IEnumerable<T> iEnumerable) =>
			step =>
			{
				foreach (T value in iEnumerable)
				{
					if (step(value) == Break)
					{
						return Break;
					};
				}
				return Continue;
			};

		/// <summary>Converts an array into a stepper delegate./></summary>
		/// <typeparam name="T">The generic type being iterated.</typeparam>
		/// <param name="array">The array to convert.</param>
		/// <returns>The stepper delegate comparable to the array provided.</returns>
		public static StepperRef<T> ToStepperRef<T>(this T[] array) =>
			step =>
			{
				for (int i = 0; i < array.Length; i++)
				{
					step(ref array[i]);
				}
			};

		/// <summary>Converts an array into a stepper delegate./></summary>
		/// <typeparam name="T">The generic type being iterated.</typeparam>
		/// <param name="array">The array to convert.</param>
		/// <returns>The stepper delegate comparable to the array provided.</returns>
		public static StepperRefBreak<T> ToStepperRefBreak<T>(this T[] array) =>
			step =>
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (step(ref array[i]) == Break)
					{
						return Break;
					};
				}
				return Continue;
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
			SetHashLinked<T> set = new SetHashLinked<T>(equate, hash);
			stepper(x =>
			{
				if (set.Contains(x))
				{
					duplicateFound = true;
					return Break;
				}
				else
				{
					set.Add(x);
					return Continue;
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
			SetHashLinked<T> set = new SetHashLinked<T>(equate, hash);
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

		/// <summary>Determines if the stepper contains any of the predicated values.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to determine if any predicated values exist.</param>
		/// <param name="where">The predicate.</param>
		/// <returns>True if any of the predicated values exist or </returns>
		public static bool Any<T>(this Stepper<T> stepper, Predicate<T> where)
		{
			bool any = false;
			stepper(x => any = any || where(x));
			return any;
		}

		/// <summary>Determines if the stepper contains any of the predicated values.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to determine if any predicated values exist.</param>
		/// <param name="where">The predicate.</param>
		/// <returns>True if any of the predicated values exist or </returns>
		public static bool Any<T>(this StepperBreak<T> stepper, Predicate<T> where)
		{
			bool any = false;
			stepper(x => (any = where(x))
				? Break
				: Continue);
			return any;
		}

		#endregion
	}
}
