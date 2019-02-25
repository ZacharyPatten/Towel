namespace Towel
{
	/// <summary>Status of data structure iteration.</summary>
	[System.Serializable]
	public enum StepStatus
	{
		/// <summary>Continue normal iteration.</summary>
		Continue = 0,
		/// <summary>Iteration cancelation.</summary>
		Break = 1,
		/// <summary>Restart the iteration.</summary>
		Restart = 2
	};

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	[System.Serializable]
	public delegate void Step<T>(T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	[System.Serializable]
	public delegate void StepRef<T>(ref T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	[System.Serializable]
	public delegate StepStatus StepBreak<T>(T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	[System.Serializable]
	public delegate StepStatus StepRefBreak<T>(ref T current);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step_function">The foreach function to perform on each iteration.</param>
	[System.Serializable]
	public delegate void Stepper<T>(Step<T> step_function);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step_function">The foreach function to perform on each iteration.</param>
	[System.Serializable]
	public delegate void StepperRef<T>(StepRef<T> step_function);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step_function">The foreach function to perform on each iteration.</param>
	[System.Serializable]
	public delegate void StepperBreak<T>(StepBreak<T> step_function);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step_function">The foreach function to perform on each iteration.</param>
	[System.Serializable]
	public delegate void StepperRefBreak<T>(StepRefBreak<T> step_function);

	/// <summary>Extension methods.</summary>
	public static class Step
	{
		/// <summary>Converts an IEnumerable into a stepper delegate./></summary>
		/// <typeparam name="T">The generic type being iterated.</typeparam>
		/// <param name="enumerable">The Ienumerable to convert.</param>
		/// <returns>The stepper delegate comparable to the IEnumerable provided.</returns>
		public static Stepper<T> Stepper<T>(this System.Collections.Generic.IEnumerable<T> enumerable)
		{
			return (Step<T> step) =>
			{
				foreach (T _step in enumerable)
					step(_step);
			};
		}

		/// <summary>Checks to see if a given object is in this data structure.</summary>
		/// <typeparam name="T">The generic type stored in the structure.</typeparam>
		/// <param name="stepper">The structure to check against.</param>
		/// <param name="check">The item to check for.</param>
		/// <param name="equate">Delegate for equating two instances of the same type.</param>
		/// <returns>true if the item is in this structure; false if not.</returns>
		public static bool Contains<T>(this StepperBreak<T> stepper, T check, Equate<T> equate)
		{
			bool contains = false;
			stepper((T step) =>
			{
				if (equate(step, check))
				{
					contains = true;
					return StepStatus.Break;
				}
				return StepStatus.Continue;
			});
			return contains;
		}

		/// <summary>Checks to see if a given object is in this data structure.</summary>
		/// <typeparam name="T">The generic type stored in the structure.</typeparam>
		/// <typeparam name="K">The type of the key to look up.</typeparam>
		/// <param name="stepper">The structure to check against.</param>
		/// <param name="key">The key to check for.</param>
		/// <param name="equate">Delegate for equating two instances of different types.</param>
		/// <returns>true if the item is in this structure; false if not.</returns>
		public static bool Contains<T, K>(this StepperBreak<T> stepper, K key, Equate<T, K> equate)
		{
			bool contains = false;
			stepper((T step) =>
			{
				if (equate(step, key))
				{
					contains = true;
					return StepStatus.Break;
				}
				return StepStatus.Continue;
			});
			return contains;
		}

		/// <summary>Looks up an item this structure by a given key.</summary>
		/// <typeparam name="T">The generic type stored in the structure.</typeparam>
		/// <typeparam name="K">The type of the key to look up.</typeparam>
		/// <param name="stepper">The structure to check against.</param>
		/// <param name="key">The key to look up.</param>
		/// <param name="equate">Delegate for equating two instances of different types.</param>
		/// <returns>The item with the corresponding to the given key.</returns>
		public static T Get<T, K>(this StepperBreak<T> stepper, K key, Equate<T, K> equate)
		{
			bool contains = false;
			T item = default(T);
			stepper((T step) =>
			{
				if (equate(step, key))
				{
					contains = true;
					item = step;
					return StepStatus.Break;
				}
				return StepStatus.Continue;
			});
			if (contains == false)
				throw new System.InvalidOperationException("item not found in structure");
			return item;
		}

		/// <summary>Trys to look up an item this structure by a given key.</summary>
		/// <typeparam name="T">The generic type stored in the structure.</typeparam>
		/// <typeparam name="K">The type of the key to look up.</typeparam>
		/// <param name="stepper">The structure to check against.</param>
		/// <param name="key">The key to look up.</param>
		/// <param name="equate">Delegate for equating two instances of different types.</param>
		/// <param name="item">The item if it was found or null if not the default(Type) value.</param>
		/// <returns>true if the key was found; false if the key was not found.</returns>
		public static bool TryGet<T, K>(this StepperBreak<T> stepper, K key, Equate<T, K> equate, out T item)
		{
			bool contains = false;
			T temp = default(T);
			stepper((T step) =>
			{
				if (equate(step, key))
				{
					contains = true;
					temp = step;
					return StepStatus.Break;
				}
				return StepStatus.Continue;
			});
			item = temp;
			return contains;
		}

		/// <summary>Converts the stepper into an array.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to convert.</param>
		/// <returns>The array created from the stepper.</returns>
		public static T[] ToArray<T>(this Stepper<T> stepper)
		{
			int count = stepper.Count();
			T[] elements = new T[count];
			int i = 0;
			stepper((T step) => { elements[i++] = step; });
			return elements;
		}
		
		/// <summary>Counts the number of items in the stepper.</summary>
		/// <typeparam name="T">The generic type of the stepper.</typeparam>
		/// <param name="stepper">The stepper to count the items of.</param>
		/// <returns>The number of items in the stepper.</returns>
		public static int Count<T>(this Stepper<T> stepper)
		{
			int count = 0;
			stepper((T step) => { count++; });
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
                throw new System.ArgumentNullException("stepper");
            if (nth <= 0)
                throw new System.InvalidOperationException("Attempting to EveryNth a stepper to an invalid reduction.");

            int i = 1;
            return (Step<T> step) =>
                {
                    stepper((T item) =>
                        {
                            if (i == nth)
                            {
                                step(item);
                                i = 1;
                            }
                            else
                            {
                                i++;
                            }
                        });
                };
        }

        /// <summary>
        /// Determines if the data contains any duplicates.
        /// </summary>
        /// <typeparam name="T">The generic type of the data.</typeparam>
        /// <param name="stepper">The stepper function for the data.</param>
        /// <param name="equate">An equality function for the data</param>
        /// <param name="hash">A hashing function for the data.</param>
        /// <returns>True if the data contains duplicates. False if not.</returns>
        public static bool ContainsDuplicates<T>(this StepperBreak<T> stepper, Equate<T> equate, Hash<T> hash)
        {
            bool duplicateFound = false;
            Towel.DataStructures.SetHashArray<T> set = new Towel.DataStructures.SetHashArray<T>(equate, hash);
            stepper((T item) =>
            {
                if (set.Contains(item))
                {
                    duplicateFound = true;
                    return StepStatus.Break;
                }
                else
                {
                    set.Add(item);
                    return StepStatus.Continue;
                }
            });
            return duplicateFound;
        }

        /// <summary>
        /// Determines if the data contains any duplicates.
        /// </summary>
        /// <typeparam name="T">The generic type of the data.</typeparam>
        /// <param name="stepper">The stepper function for the data.</param>
        /// <param name="equate">An equality function for the data</param>
        /// <param name="hash">A hashing function for the data.</param>
        /// <returns>True if the data contains duplicates. False if not.</returns>
        /// <remarks>Use the StepperBreak overload if possible. It is more effiecient.</remarks>
        public static bool ContainsDuplicates<T>(this Stepper<T> stepper, Equate<T> equate, Hash<T> hash)
        {
            bool duplicateFound = false;
            Towel.DataStructures.SetHashArray<T> set = new Towel.DataStructures.SetHashArray<T>(equate, hash);
            stepper((T item) =>
            {
                if (set.Contains(item))
                {
                    duplicateFound = true;
                }
                else
                {
                    set.Add(item);
                }
            });
            return duplicateFound;
        }

        /// <summary>
        /// Determines if the data contains any duplicates.
        /// </summary>
        /// <typeparam name="T">The generic type of the data.</typeparam>
        /// <param name="stepper">The stepper function for the data.</param>
        /// <returns>True if the data contains duplicates. False if not.</returns>
        public static bool ContainsDuplicates<T>(this StepperBreak<T> stepper)
        {
            return ContainsDuplicates(stepper, Equate.Default, Hash.Default);
        }

        /// <summary>
        /// Determines if the data contains any duplicates.
        /// </summary>
        /// <typeparam name="T">The generic type of the data.</typeparam>
        /// <param name="stepper">The stepper function for the data.</param>
        /// <returns>True if the data contains duplicates. False if not.</returns>
        /// <remarks>Use the StepperBreak overload if possible. It is more effiecient.</remarks>
        public static bool ContainsDuplicates<T>(this Stepper<T> stepper)
        {
            return ContainsDuplicates(stepper, Equate.Default, Hash.Default);
        }
    }
}
