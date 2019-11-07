using BenchmarkDotNet.Running;
using System;
using System.Reflection;
using Towel;
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

	/// <summary>Allows customization of the benchmarking.</summary>
	public static partial class BenchmarkSettings
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
			Tag.Mathematics,
			Tag.Measurements,

			// Algorithm Tags --------------------------------------

			Tag.Sort,

			// Data Structure Tags ---------------------------------

			Tag.Link,
			Tag.Array,
			Tag.ListArray,
			Tag.ListLinked,
			Tag.StackArray,
			Tag.StackLinked,
			Tag.QueueArray,
			Tag.QueueLinked,
			Tag.HeapArray,
			Tag.AvlTreeLinked,
			Tag.RedBlackTreeLinked,
			Tag.BTree,
			Tag.SkipList,
			Tag.SetHashArray,
			Tag.SetHashLinked,
			Tag.Map,
			Tag.KdTree,
			Tag.Omnitree,
		};
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
		Mathematics,
		Measurements,

		// Algorithm Tags
		Sort,

		// Data Structure Tags
		Link, // aka Tuple
		Array,
		ListArray,
		ListLinked,
		QueueArray,
		QueueLinked,
		StackArray,
		StackLinked,
		HeapArray,
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

	#region Shared Random Data Generation

	public class Person
	{
		public Guid Id;
		public string FirstName;
		public string LastName;
		public DateTime DateOfBirth;
	}

	public static partial class BenchmarkSettings
	{
		/// <summary>
		/// Settings for the data structure becnhmarking methods.
		/// </summary>
		public static class DataStructures
		{
			public static Person[][] RandomData => GenerateBenchmarkData();

			public static Person[][] GenerateBenchmarkData()
			{
				DateTime minimumBirthDate = new DateTime(1950, 1, 1);
				DateTime maximumBirthDate = new DateTime(2000, 1, 1);

				Random random = new Random();
				Person[] GenerateData(int count)
				{
					Person[] data = new Person[count];
					for (int i = 0; i < count; i++)
					{
						data[i] = new Person()
						{
							Id = Guid.NewGuid(),
							FirstName = random.NextAlphabeticString(random.Next(5, 11)),
							LastName = random.NextAlphabeticString(random.Next(5, 11)),
							DateOfBirth = random.NextDateTime(minimumBirthDate, maximumBirthDate)
						};
					}
					return data;
				}

				return new Person[][]
				{
					GenerateData(100),
					GenerateData(100000),
				};
			}
		}
	}

	#endregion
}
