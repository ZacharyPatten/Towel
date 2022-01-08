namespace Towel_Testing
{
	// [TestClass]
	public partial class Statics_Testing
	{
		#region SearchBinary_Testing

		[TestMethod]
		public void SearchBinary_Test()
		{
			{ // [even] collection size [found]
				int[] values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
				for (int i = 0; i < values.Length; i++)
				{
					var result = SearchBinary(values, i);
					Assert.IsTrue(result.Found);
					Assert.IsTrue(result.Index == i);
					Assert.IsTrue(result.Value == i);
				}
			}
			{ // [odd] collection size [found]
				int[] values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, };
				for (int i = 0; i < values.Length; i++)
				{
					var result = SearchBinary(values, i);
					Assert.IsTrue(result.Found);
					Assert.IsTrue(result.Index == i);
					Assert.IsTrue(result.Value == i);
				}
			}
			{ // [even] collection size [not found]
				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, };
				for (int i = 0, j = -10; j <= 10; i++, j += 2)
				{
					var result = SearchBinary(values, j);
					Assert.IsTrue(!result.Found);
					Assert.IsTrue(result.Index == i - 1);
					Assert.IsTrue(result.Value == default);
				}
			}
			{ // [odd] collection size [not found]
				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, };
				for (int i = 0, j = -10; j <= 8; i++, j += 2)
				{
					var result = SearchBinary(values, j);
					Assert.IsTrue(!result.Found);
					Assert.IsTrue(result.Index == i - 1);
					Assert.IsTrue(result.Value == default);
				}
			}
			{ // exception: invalid compare function
				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, };
				Assert.ThrowsException<ArgumentException>(() => SearchBinary<int>(values, a => (CompareResult)int.MinValue));
			}
			{ // exception: null argument
				int[]? values = null;
				Assert.ThrowsException<ArgumentException>(() => SearchBinary(values, 7));
			}
		}

		#endregion
	}
}
