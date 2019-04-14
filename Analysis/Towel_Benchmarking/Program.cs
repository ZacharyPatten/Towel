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
            // Note:
            // Benchmarking should always be run outside of Visual Studio by running
            // a Release mode build via the command line. These settings help set up
            // a build to be run outside of Visual Studio. By default I have all tests
            // uncommented, which means all of them will run, and this can take a very
            // long time.

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

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                BenchmarksAttribute attribute = type.GetCustomAttribute<BenchmarksAttribute>();
                if (!(attribute is null))
                {
                    var summary = BenchmarkRunner.Run(type);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class BenchmarksAttribute : Attribute
    {
        public Tag[] Tags;

        public BenchmarksAttribute(params Tag[] tags) { Tags = tags;
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
}
