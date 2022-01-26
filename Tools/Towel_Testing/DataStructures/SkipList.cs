namespace Towel_Testing.DataStructures;

[TestClass]
public class SkipList_Testing
{
	[TestMethod]
	public void AddTest_Random()
	{
		const int size = 100;
		int[] array = new int[size];
		const int max = size * size;
		Random random = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = random.Next(max);
		}
		var list = SkipList.New<int>(12);
		foreach (var x in array)
		{
			list.Add(x);
		}
		int[] act = list.ToArray();
		Array.Sort(array);
		CollectionAssert.AreEqual(array, act);
	}

	[TestMethod]
	public void IteratorTest()
	{
		int[] array = { 54, 12, 21, 33, 43, 45, 54, 81, 92, 93, 54, 99, 54 };
		var list = SkipList.New<int>(5);
		foreach (var x in array)
		{
			list.Add(x);
		}
		Array.Sort(array);
		int[] actual = list.ToArray();
		CollectionAssert.AreEqual(array, actual);
	}

	[TestMethod]
	public void False_Removal_Test()
	{
		var list = SkipList.New<int>(4);
		for (int i = 0; i < 10; i++)
		{
			Assert.IsFalse(list.TryRemove(i).Success);
		}
		for (int i = 100; i <= 200; i++)
		{
			list.Add(i);
		}
		for (int i = 0; i < 100; i++)
		{
			Assert.IsFalse(list.TryRemove(i).Success);
		}
	}

	[TestMethod]
	public void Add_Remove_Search_Test_1()
	{
		const int size = 1000;
		int[] array = new int[size];
		const int max = size * size;
		Random random = new();
		for (int i = 0; i < size; i++) array[i] = random.Next(max);
		var list = SkipList.New<int>(15);
		foreach (var x in array)
		{
			list.Add(x);
		}
		Array.Sort(array);
		for (int i = 0; i < max; i++)
		{
			if (Array.BinarySearch(array, 0, size, i) >= 0)
			{
				Assert.IsTrue(list.Contains(i));
			}
			else
			{
				Assert.IsFalse(list.Contains(i));
			}
		}
		foreach (var x in array)
		{
			Assert.IsTrue(list.Contains(x));
			Assert.IsTrue(list.TryRemove(x).Success);
		}
		Assert.IsTrue(list.Count is 0);
	}

	[TestMethod]
	public void Add_Remove_Search_Test_2()
	{
		const int size = 4000;
		int[] array = new int[size];
		const int max = size * size;
		Random random = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = random.Next(max);
		}
		var list = SkipList.New<int>(20);
		foreach (var x in array)
		{
			list.Add(x);
		}
		Array.Sort(array);
		for (int i = 0; i < max; i++)
		{
			if (Array.BinarySearch(array, 0, size, i) >= 0)
			{
				Assert.IsTrue(list.Contains(i));
			}
			else
			{
				Assert.IsFalse(list.Contains(i));
			}
		}
		foreach (var x in array)
		{
			Assert.IsTrue(list.TryRemove(x).Success);
		}
		Assert.IsTrue(list.Count is 0);
	}

	[TestMethod]
	public void CloneTest()
	{
		const int size = 4000;
		int[] array = new int[size];
		const int max = size * size;
		Random random = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = random.Next(max);
		}
		var list = SkipList.New<int>(20);
		foreach (var x in array)
		{
			list.Add(x);
		}
		var clone = list.Clone();
		list.RemoveAll(x => true);
		Assert.IsTrue(list.Count is 0);
		Array.Sort(array);
		for (int i = 0; i < max; i++)
		{
			if (Array.BinarySearch(array, 0, size, i) >= 0)
			{
				Assert.IsTrue(clone.Contains(i));
			}
			else
			{
				Assert.IsFalse(clone.Contains(i));
			}
		}
		foreach (var x in array)
		{
			Assert.IsTrue(clone.TryRemove(x).Success);
		}
		Assert.IsTrue(clone.Count == 0);
	}

	[TestMethod]
	public void Remove_Test()
	{
		const int size = 4000;
		int[] array = new int[size];
		const int max = size * size;
		Random random = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = random.Next(max);
		}
		var list = SkipList.New<int>(20);
		for (int i = 0; i < size; i++)
		{
			list.Add(max + random.Next(max));
		}
		list.Clear();
		foreach (var x in array)
		{
			list.Add(x);
		}
		Array.Sort(array);
		CollectionAssert.AreEqual(array, list.ToArray());
	}

	[TestMethod]
	public void ClearTest()
	{
		const int size = 4000;
		int[] array = new int[size];
		const int max = size * size;
		Random random = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = random.Next(max);
		}
		var list = SkipList.New<int>(20);
		for (int i = 0; i < size; i++)
		{
			list.Add(max + random.Next(max));
		}
		list.Clear();
		foreach (var x in array)
		{
			list.Add(x);
		}
		Array.Sort(array);
		CollectionAssert.AreEqual(array, list.ToArray());
	}

	[TestMethod]
	public void RemoveFirstTest()
	{
		int[] array = { 90, 10, 23, 25, 37, 14, 38, 11, 24, 98, 77 };
		var list = SkipList.New<int>(3);
		foreach (var x in array)
		{
			list.Add(x);
		}
		list.Add(190);
		list.Add(102);
		list.Add(204);
		Assert.IsTrue(list.TryRemoveFirst(x => x > 100));
		Assert.IsTrue(list.TryRemoveFirst(x => x > 100));
		Assert.IsTrue(list.TryRemoveFirst(x => x > 100));
		Assert.IsFalse(list.TryRemoveFirst(x => x > 100));
		Array.Sort(array);
		int[] actual = list.ToArray();
		CollectionAssert.AreEqual(array, actual);
	}

	[TestMethod]
	public void RemoveAllTest_1()
	{
		const int size = 50;
		int[] array = new int[size];
		const int max = 500;
		Random r = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = r.Next(max);
		}
		var list = SkipList.New<int>(7);
		foreach (var x in array)
		{
			list.Add(x);
		}
		for (int i = 0; i < size; i++)
		{
			list.Add(max + r.Next(max));
		}
		list.RemoveAll(x => x >= max);
		Array.Sort(array);
		int[] actual = list.ToArray();
		CollectionAssert.AreEqual(array, actual);
	}

	[TestMethod]
	public void RemoveAllTest_2()
	{
		const int size = 500;
		int[] array = new int[size];
		const int max = 5000;
		Random r = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = r.Next(max) * 2; // Even numbers
		}
		var list = SkipList.New<int>(7);
		foreach (var x in array)
		{
			list.Add(x);
		}
		for (int i = 0; i < size; i++)
		{
			list.Add(1 + (2 * r.Next(max))); // Odd numbers
		}
		list.RemoveAll(x => x % 2 is not 0);
		Array.Sort(array);
		int[] actual = list.ToArray();
		CollectionAssert.AreEqual(array, actual);
	}

	[TestMethod]
	public void EmptyIterationTest_1()
	{
		var list = SkipList.New<int>(5);
		CollectionAssert.AreEqual(Array.Empty<int>(), list.ToArray());
	}

	[TestMethod]
	public void EmptyIterationTest_2()
	{
		var list = SkipList.New<int>(5);
		foreach (var _ in list)
		{
			Assert.Fail();
		}
	}

	[TestMethod]
	public void EmptyIterationTest_3()
	{
		var list = SkipList.New<int>(5);
		int[] x = RandomIntArray(100, 1000000);
		foreach (var item in x)
		{
			list.Add(item);
		}
		foreach (var item in x)
		{
			Assert.IsTrue(list.TryRemove(item).Success);
		}
		CollectionAssert.AreEqual(Array.Empty<int>(), list.ToArray());
	}

	[TestMethod]
	public void EmptyIterationTest_4()
	{
		var list = SkipList.New<int>(5);
		int[] array = RandomIntArray(100, 1000000);
		foreach (var item in array)
		{
			list.Add(item);
		}
		foreach (var item in array)
		{
			Assert.IsTrue(list.TryRemove(item).Success);
		}
		foreach (var _ in list)
		{
			Assert.Fail();
		}
	}
	public static int[] RandomIntArray(int size, int max)
	{
		int[] array = new int[size];
		Random random = new();
		for (int i = 0; i < size; i++)
		{
			array[i] = random.Next(max);
		}
		return array;
	}
}
