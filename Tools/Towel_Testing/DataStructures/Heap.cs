using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class HeapArray_Testing
	{
		[TestMethod]
		public void Dequeue_Testing()
		{
			void Test<T>(T[] values, Func<T, T, CompareResult> compare)
			{
				T[] clonedValues = (T[])values.Clone();
				Shuffle<T>(clonedValues);
				IHeap<T> heap = HeapArray.New<T>(compare);
				clonedValues.Stepper(x => heap.Enqueue(x));
				foreach (T value in values)
				{
					T dequeue = heap.Dequeue();
					Assert.IsTrue(value!.Equals(dequeue));
				}
			}

			{ // int compare
				int[] values = (..100).ToArray();
				Array.Sort(values, (a, b) => -a.CompareTo(b));
				Test(values, Compare);
			}
			{ // string compare
				string[] values = (..100).ToArray(i => i.ToString());
				Array.Sort(values, (a, b) => -a.CompareTo(b));
				Test(values, Compare);
			}
			{ // int reverse compare
				int[] values = (..100).ToArray();
				Array.Sort(values);
				Test(values, (a, b) => Compare(b, a));
			}
			{ // string reverse compare
				string[] values = (..100).ToArray(i => i.ToString());
				Array.Sort(values);
				Test(values, (a, b) => Compare(b, a));
			}
		}
	}
}
