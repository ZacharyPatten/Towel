using BenchmarkDotNet.Running;
using System;
using System.Reflection;
using Towel.DataStructures;

namespace Towel_Benchmarking
{
	public class Program
	{
		public static void Main()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				BenchmarksAttribute attribute = type.GetCustomAttribute<BenchmarksAttribute>();
				if (!(attribute is null))
				{
					foreach (Tag tag in attribute.Tags)
					{
						if (!BenchmarkSettings.EnabledTags.Contains(tag))
						{
							goto SkipBenchmark;
						}
					}
					var summary = BenchmarkRunner.Run(type);
				}
				SkipBenchmark:
				continue;
			}
		}
	}

	#region Benchmark Settings

	/// <summary>
	/// Allows customization of the benchmarking.
	/// </summary>
	public static class BenchmarkSettings
	{
		/// <summary>
		/// Allows the enabling/disabling of various benchmarking methods.
		/// A benchmark will only run if all its tags are enabled. The "tags"
		/// are just the values of the "Benchmarks" attribute on the class.
		/// </summary>
		public static ISet<Tag> EnabledTags = new SetHashLinked<Tag>()
		{
            // Major Tags ------------------------------------------
            Tag.Algorithms,
			Tag.DataStructures,
			Tag.Diagnostics,
			Tag.Mathematics,
			Tag.Measurements,
			Tag.Parallels,

            // Data Structure Tags ---------------------------------
            Tag.Link, // aka Tuple
            Tag.Indexed, // aka Array
            Tag.AddableArray, // aka ListArray
            Tag.AddableLinked, // aka ListArray
            Tag.FirstInLastOutArray, // aka Stack as Array
            Tag.FirstInLastOutLinked, // aka Stack as Linked List
            Tag.FirstInFirstOutArray, // aka Queue as Array
            Tag.FirstInFirstOutLinked, // aka Queue as Linked List
            Tag.HeapArray,
			Tag.AvlTreeLinked,
			Tag.RedBlackTreeLinked,
			Tag.BTree,
			Tag.SkipList,
			Tag.SetHashArray,
			Tag.SetHashLinked,
			Tag.Map, // aka Dictionary
            Tag.KdTree,
			Tag.Omnitree,
		};

		/// <summary>
		/// Settings for the data structure becnhmarking methods.
		/// </summary>
		public static class DataStructures
		{
			/// <summary>
			/// The insertion counts for the relative insertion methods of
			/// the data structures: add, enqueue, push, etc.
			/// </summary>
			public static int[] InsertionCounts => new int[] { 100, 100000 };
		}
	}

	#endregion

	#region Benchmarking Framework

	// This code is framework code for benchmarking.
	// It should not be edited to perform benchamrking.

	[AttributeUsage(AttributeTargets.Class)]
	public class BenchmarksAttribute : Attribute
	{
		public Tag[] Tags;

		public BenchmarksAttribute(params Tag[] tags) { Tags = tags; }
	}

	public enum Tag
	{
		// Major Tags
		Algorithms,
		DataStructures,
		Diagnostics,
		Mathematics,
		Measurements,
		Parallels,

		// Data Structure Tags
		Link, // aka Tuple
		Indexed, // aka Array
		AddableArray, // aka ListArray
		AddableLinked, // aka LinkedList
		FirstInFirstOutArray, // aka Queue as Array
		FirstInFirstOutLinked, // aka Queue as Linked List
		FirstInLastOutArray, // aka Stack as Array
		FirstInLastOutLinked, // aka Stack as Linked List
		HeapArray, // as Flattened Array
		AvlTreeLinked,
		RedBlackTreeLinked,
		BTree,
		SkipList,
		SetHashArray,
		SetHashLinked,
		Map, // aka Dictionary
		KdTree,
		Omnitree,

		// Mathemtatics
		Compute,
		Vector,
		Matrix,
	}

	#endregion
}
