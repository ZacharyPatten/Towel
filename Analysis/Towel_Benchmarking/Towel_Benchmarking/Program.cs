using BenchmarkDotNet.Running;
using System;
using System.Reflection;

namespace Towel_Benchmarking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!(type.GetCustomAttribute<BenchmarksAttribute>() is null))
                {
                    var summary = BenchmarkRunner.Run<Program>();
                }
            }
        }
    }
}
