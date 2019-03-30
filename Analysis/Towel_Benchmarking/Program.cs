using BenchmarkDotNet.Running;
using System;
using System.Reflection;
using Towel.DataStructures;

namespace Towel_Benchmarking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Settings
            //
            // You can control what benchmarking is done by commenting out
            // tags. A benchmark will only run if all its tags are enabled.
            //
            ISet<string> settings = new SetHashLinked<string>()
            {
                "Algorithms",
                "DataStructures",
                "Diagnostics",
                "Mathematics",
                "Measurements",
                "Parallel",

            };

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                BenchmarksAttribute attribute = type.GetCustomAttribute<BenchmarksAttribute>();
                if (!(attribute is null))
                {
                    var summary = BenchmarkRunner.Run<Program>();
                }
            }
        }
    }
}
