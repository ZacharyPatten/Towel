using BenchmarkDotNet.Attributes;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Map vs Dictionary (Add)")]
	public class MapVsDictionary_Add
	{
		[Params(10, 100, 1000, 10000)]
		public int N;

		[Benchmark] public void MapDelegates()
		{
			IMap<int, int> map = MapHashLinked.New<int, int>();
			for (int i = 0; i < N; i++)
			{
				map.TryAdd(i, i, out _);
			}
		}

		[Benchmark] public void MapStructs()
		{
			MapHashLinked<int, int, IntEquate, IntHash> map = new();
			for (int i = 0; i < N; i++)
			{
				map.TryAdd(i, i, out _);
			}
		}

		struct IntHash : IFunc<int, int>
		{
			public int Do(int a) => a;
		}

		struct IntEquate : IFunc<int, int, bool>
		{
			public bool Do(int a, int b) => a == b;
		}

		[Benchmark] public void Dictionary()
		{
			System.Collections.Generic.Dictionary<int, int> dictionary = new();
			for (int i = 0; i < N; i++)
			{
				dictionary.TryAdd(i, i);
			}
		}
	}

	[Tag(Program.Name, "Map vs Dictionary (Look Up)")]
	public class MapVsDictionary_LookUp
	{
		[Params(10, 100, 1000, 10000)]
		public int N;

		IMap<int, int> mapHashLinked;
		IMap<int, int> mapHashLinkedStructs;
		System.Collections.Generic.Dictionary<int, int> dictionary;
		int temp;

		[IterationSetup]
		public void IterationSetup()
		{
			mapHashLinked = MapHashLinked.New<int, int>();
			mapHashLinkedStructs = new MapHashLinked<int, int, IntEquate, IntHash>();
			dictionary = new System.Collections.Generic.Dictionary<int, int>();
			for (int i = 0; i < N; i++)
			{
				mapHashLinked.Add(i, i);
				mapHashLinkedStructs.Add(i, i);
				dictionary.Add(i, i);
			}
		}

		public void Temp() => Console.Write(temp);

		[Benchmark]
		public void MapDelegates()
		{
			for (int i = 0; i < N; i++)
			{
				temp = mapHashLinked[i];
			}
		}

		[Benchmark]
		public void MapStructs()
		{
			for (int i = 0; i < N; i++)
			{
				temp = mapHashLinkedStructs[i];
			}
		}

		struct IntHash : IFunc<int, int>
		{
			public int Do(int a) => a;
		}

		struct IntEquate : IFunc<int, int, bool>
		{
			public bool Do(int a, int b) => a == b;
		}

		[Benchmark]
		public void Dictionary()
		{
			for (int i = 0; i < N; i++)
			{
				temp = dictionary[i];
			}
		}
	}
}
