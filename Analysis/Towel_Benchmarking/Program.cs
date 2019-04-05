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
            ISet<Tag> settings = new SetHashLinked<Tag>()
            {
                // Major Tags
                Tag.Algorithms,
                Tag.DataStructures,
                Tag.Diagnostics,
                Tag.Mathematics,
                Tag.Measurements,
                Tag.Parallels,

                // Data Structure Tags
                Tag.Link, // aka Tuple
                Tag.Indexed, // aka Array
                Tag.Addable, // aka List
                Tag.FirstInLastout, // aka Stack
                Tag.FirstInFirstOut, // aka Queue
                Tag.Heap,
                Tag.AvlTree,
                Tag.RedBlackTree,
                Tag.BTree,
                Tag.SkipList,
                Tag.Set,
                Tag.Map, // aka Dictionary
                Tag.KdTree,
                Tag.Omnitree,
                
                
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

    [AttributeUsage(AttributeTargets.Class)]
    public class BenchmarksAttribute : Attribute
    {
        public Tag[] Tags;

        public BenchmarksAttribute(params Tag[] tags)
        {
            Tags = tags;
        }
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
        Addable, // aka List
        FirstInLastout, // aka Stack
        FirstInFirstOut, // aka Queue
        Heap,
        AvlTree,
        RedBlackTree,
        BTree,
        SkipList,
        Set,
        Map, // aka Dictionary
        KdTree,
        Omnitree,

        // Mathemtatics
        Compute,
        Vector,
        Matrix,
    }
}
