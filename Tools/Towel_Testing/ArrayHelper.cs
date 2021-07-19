using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	[TestClass]
	public partial class ArrayHelper_Testing
	{
		#region string.Replace (multiple)

		[TestMethod]
		public void newFromRanges()
		{
			{
				int[] array = ArrayHelper.NewFromRanges();
				Assert.IsTrue(array == Array.Empty<int>());
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..0);
				Assert.IsTrue(array == Array.Empty<int>());
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..0, 0..0);
				Assert.IsTrue(array == Array.Empty<int>());
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..1);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..2);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0, 1 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..2);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..3);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1, 2 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..3);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1, 2 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(3..0);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 3, 2, 1, }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..1, 0..1);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0, 0 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..2, 0..1);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0, 1, 0 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..2, 0..2);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0, 1, 0, 1 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..2, 1..2);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1, 1 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..3, 1..3);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1, 2, 1, 2 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..1, 0..1, 0..1);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0, 0, 0 }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(0..2, 0..2, 0..2);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 0, 1, 0, 1, 0, 1, }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..3, 1..3, 1..3);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1, 2, 1, 2, 1, 2, }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(3..1, 1..3, 3..1);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 3, 2, 1, 2, 3, 2, }));
			}
			{
				int[] array = ArrayHelper.NewFromRanges(1..3, 3..1, 1..3);
				Assert.IsTrue(EquateSequence(array, stackalloc int[] { 1, 2, 3, 2, 1, 2, }));
			}
		}

		#endregion
	}
}
