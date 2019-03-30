using System;

namespace Towel_Benchmarking
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BenchmarksAttribute : Attribute
    {
        public string[] Tags;

        public BenchmarksAttribute(params string[] tags)
        {
            Tags = tags;
        }
    }
}
