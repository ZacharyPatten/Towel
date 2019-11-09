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
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.Sort_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.ListArray_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.ListLinked_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.StackArray_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.StackLinked_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.QueueArray_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.QueueLinked_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.HeapArray_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.AvlTreeLinked_Benchmarks));
			BenchmarkRunner.Run(typeof(Towel_Benchmarking.DataStructures.RedBlackTreeLinked_Benchmarks));
		}
	}

	#region Shared Random Data Generation

	public class Person
	{
		public Guid Id;
		public string FirstName;
		public string LastName;
		public DateTime DateOfBirth;
	}

	public static partial class RandomData
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
