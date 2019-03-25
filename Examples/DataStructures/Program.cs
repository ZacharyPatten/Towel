using System;

using Towel;
using Towel.Mathematics;
using Towel.DataStructures;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int test = 10;

            Console.WriteLine("You are runnning the Data Structures example.");
            Console.WriteLine("======================================================");
            Console.WriteLine();

            #region Link

            Console.WriteLine("  Testing Link-------------------------------");
            Console.WriteLine("   Size: 6");
            Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
            Console.Write("    Traversal: ");
            link.Stepper((dynamic current) => { Console.Write(current); });
            Console.WriteLine();
            // Saving to a file
            //string linklink_file = "link." + ToExtension(link.GetType());
            //Console.WriteLine("    File: \"" + linklink_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(linklink_file, link));
            //Link<int, int, int, int, int, int> deserialized_linklink;
            //Console.WriteLine("    Deserialized: " + Deserialize(linklink_file, out deserialized_linklink));
            Console.WriteLine();

            #endregion

            #region Array

            Console.WriteLine("  Testing Array_Array<int>-------------------");
            Indexed<int> array = new IndexedArray<int>(test);
            for (int i = 0; i < test; i++)
                array[i] = i;
            Console.Write("    Traversal: ");
            array.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            // Saving to a file
            //string arrayarray_file = "array." + ToExtension(array.GetType());
            //Console.WriteLine("    File: \"" + arrayarray_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(arrayarray_file, array));
            //ArrayArray<int> deserialized_arrayarray;
            //Console.WriteLine("    Deserialized: " + Deserialize(arrayarray_file, out deserialized_arrayarray));
            Console.WriteLine();

            #endregion

            #region List

            Console.WriteLine("  Testing List_Array<int>--------------------");
            Addable<int> list_array = new AddableArray<int>(test);
            for (int i = 0; i < test; i++)
                list_array.Add(i);
            Console.Write("    Traversal: ");
            list_array.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            //string list_array_serialization = (list_array as ListArray<int>).Serialize(x => x.ToString());
            //using (StreamWriter writer = new StreamWriter("ListArray.ListArray"))
            //{
            //    writer.WriteLine(list_array_serialization);
            //}
            //using (StreamReader reader = new StreamReader("ListArray.ListArray"))
            //{
            //    list_array = ListArray<int>.Deserialize(reader.ReadToEnd(), x => Int16.Parse(x.Trim()));
            //}
            //Console.Write("    Serialization/Deserialization is possible.");
            list_array.Add(11);
            list_array.Remove(7);
            Console.WriteLine();
            Console.WriteLine();


            //ListArray<ListArray<int>> list_array2 = new ListArray<ListArray<int>>(test);
            //for (int i = 0; i < test; i++)
            //{
            //    ListArray<int> nested_list = new ListArray<int>();
            //    for (int j = 0; j < test; j++)
            //    {
            //        nested_list.Add(j);
            //    }
            //    list_array2.Add(nested_list);
            //}
            //string list_array2_serialization = list_array2.Serialize(x => x.Serialize(y => y.ToString()));
            //using (StreamWriter writer = new StreamWriter("ListArray2.ListArray"))
            //{
            //    writer.WriteLine(list_array2_serialization);
            //}
            //using (StreamReader reader = new StreamReader("ListArray2.ListArray"))
            //{
            //    list_array2 = ListArray<ListArray<int>>.Deserialize(reader.ReadToEnd(), x => ListArray<int>.Deserialize(x, y => Int16.Parse(y.Trim())));
            //}

            Console.WriteLine("  Testing List_Linked<int>-------------------");
            Addable<int> list_linked = new AddableLinked<int>();
            for (int i = 0; i < test; i++)
                list_linked.Add(i);
            Console.Write("    Traversal: ");
            list_linked.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();





            // Saving to a file
            //string listlinked_file = "list_linked." + ToExtension(list_linked.GetType());
            //Console.WriteLine("    File: \"" + listlinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(listlinked_file, list_linked));
            //ListLinked<int> deserialized_listlinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(listlinked_file, out deserialized_listlinked));
            Console.WriteLine();

            #endregion

            #region Stack

            Console.WriteLine("  Testing Stack_Linked<int>------------------");
            FirstInLastOut<int> stack_linked = new FirstInLastOutLinked<int>();
            for (int i = 0; i < test; i++)
                stack_linked.Push(i);
            Console.Write("    Traversal: ");
            stack_linked.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            // Saving to a file
            //string stacklinked_file = "stack_linked." + ToExtension(stack_linked.GetType());
            //Console.WriteLine("    File: \"" + stacklinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(stacklinked_file, stack_linked));
            //StackLinked<int> deserialized_stacklinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(stacklinked_file, out deserialized_stacklinked));
            Console.WriteLine();

            #endregion

            #region Queue

            Console.WriteLine("  Testing Queue_Linked<int>------------------");
            FirstInFirstOut<int> queue_linked = new FirstInFirstOutLinked<int>();
            for (int i = 0; i < test; i++)
                queue_linked.Enqueue(i);
            Console.Write("    Traversal: ");
            queue_linked.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            // Saving to a file
            //string queuelinked_file = "queue_linked." + ToExtension(queue_linked.GetType());
            //Console.WriteLine("    File: \"" + queuelinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(queuelinked_file, queue_linked));
            //QueueLinked<int> deserialized_queuelinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(queuelinked_file, out deserialized_queuelinked));
            Console.WriteLine();

            #endregion

            #region Heap

            Console.WriteLine("  Testing Heap_Array<int>--------------------");
            Heap<int> heap_array = new HeapArray<int>(Compute.Compare);
            for (int i = 0; i < test; i++)
                heap_array.Enqueue(i);
            Console.Write("    Delegate: ");
            heap_array.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            // Saving to a file
            //string heaplinked_file = "heap_array." + ToExtension(heap_array.GetType());
            //Console.WriteLine("    File: \"" + heaplinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(heaplinked_file, heap_array));
            //HeapArray<int> deserialized_heaplinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(heaplinked_file, out deserialized_heaplinked));
            Console.WriteLine();

            #endregion

            #region Tree

            Console.WriteLine("  Testing Tree_Map<int>----------------------");
            Tree<int> tree_Map = new TreeMap<int>(0, Compute.Equal, Hash.Default);
            for (int i = 1; i < test; i++)
                tree_Map.Add(i, i / (int)System.Math.Sqrt(test));
            Console.Write("    Children of 0 (root): ");
            tree_Map.Children(0, (int i) => { Console.Write(i + " "); });
            Console.WriteLine();
            Console.Write("    Children of " + ((int)System.Math.Sqrt(test) - 1) + " (root): ");
            tree_Map.Children(((int)System.Math.Sqrt(test) - 1), (int i) => { Console.Write(i + " "); });
            Console.WriteLine();
            Console.Write("    Traversal: ");
            tree_Map.Stepper((int i) => { Console.Write(i + " "); });
            Console.WriteLine();
            // Saving to a file
            //string treelinked_file = "tree_Map." + ToExtension(tree_Map.GetType());
            //Console.WriteLine("    File: \"" + treelinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(treelinked_file, tree_Map));
            //TreeMap<int> deserialized_treelinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(treelinked_file, out deserialized_treelinked));
            Console.WriteLine();

            #endregion

            #region AVL Tree
            {
                Console.WriteLine("  AvlTree------------------------");
                Console.WriteLine();
                Console.WriteLine("    An AVL tree is a sorted binary tree.");
                Console.WriteLine("    It allows for very fast 1D ranged queries/traversals.");
                Console.WriteLine();

                AvlTree<int> avlTree = new AvlTreeLinked<int>();
                
                Console.Write("    Adding (0-" + test + ")...");
                for (int i = 0; i < test; i++)
                {
                    avlTree.Add(i);
                }
                Console.WriteLine();

                Console.Write("    Traversal: ");
                avlTree.Stepper((int i) => Console.Write(i));
                Console.WriteLine();

                //// Note: Although the AVL tree implements IEnumerable, it should be
                //// avoided. IEnumerable does not allow for recursion on recursive
                //// data structures, so it requires a stack be stored on the program's
                //// heap, which is incredibly slow.
                //
                //Console.Write("    Traversal Foreach: ");
                //foreach (int i in avlTree)
                //{
                //    Console.Write(i);
                //}
                //Console.WriteLine();
                
                int minimum = random.Next(1, test / 2);
                int maximum = random.Next(1, test / 2) + test / 2;
                Console.Write("    Ranged Traversal [" + minimum + "-" + maximum + "]: ");
                avlTree.Stepper(i => Console.Write(i), minimum, maximum);
                Console.WriteLine();

                int removal = random.Next(0, test);
                Console.Write("    Remove(" + removal + "): ");
                avlTree.Remove(removal);
                avlTree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                int contains = random.Next(0, test);
                Console.WriteLine("    Contains(" + contains + "): " + avlTree.Contains(contains));
                Console.WriteLine("    Current Least: " + avlTree.CurrentLeast);
                Console.WriteLine("    Current Greatest: " + avlTree.CurrentGreatest);
                Console.WriteLine("    Count: " + avlTree.Count);

                avlTree.Clear(); // Clears the AVL tree

                Console.WriteLine();
            }
            #endregion

            #region Red-Black Tree

            Console.WriteLine("  Testing RedBlack_Linked<int>---------------");
            RedBlackTree<int> redBlackTree_linked = new RedBlackTreeLinked<int>(Compute.Compare);
            for (int i = 0; i < test; i++)
                redBlackTree_linked.Add(i);
            Console.Write("    Traversal: ");
            redBlackTree_linked.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            // Saving to a file
            //string redblacktreelinked_file = "redBlackTree_linked." + ToExtension(redBlackTree_linked.GetType());
            //Console.WriteLine("    File: \"" + redblacktreelinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(redblacktreelinked_file, redBlackTree_linked));
            //RedBlackTreeLinked<int> deserialized_redblacktreelinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(redblacktreelinked_file, out deserialized_redblacktreelinked));
            Console.WriteLine();

            #endregion

            #region BTree

            //Console.WriteLine("  Testing BTree_LinkedArray<int>-------------");
            //BTree<int> btree_linked = new BTree_LinkedArray<int>(Logic.compare, 3);
            //for (int i = 0; i < test; i++)
            //	btree_linked.Add(i);
            //Console.Write("    Delegate: ");
            //btree_linked.Stepper((int current) => { Console.Write(current); });
            //Console.WriteLine();
            //Console.Write("    IEnumerator: ");
            //foreach (int current in btree_linked)
            //	Console.Write(current);
            //Console.WriteLine();
            //Console.WriteLine("  Press Enter to continue...");
            //string maplinked_file = "maplinked.quad";
            //Console.WriteLine("    File: \"" + maplinked_file + "\"");
            //Console.WriteLine("    Serialized: " + Serialize(maplinked_file, hashTable_linked));
            //Omnitree_LinkedLinkedLists<int, double> deserialized_maplinked;
            //Console.WriteLine("    Deserialized: " + Deserialize(maplinked_file, out deserialized_maplinked));
            //Console.ReadLine();
            //Console.WriteLine();

            #endregion

            #region Set

            Console.WriteLine("  Testing Set_Hash<int>----------------------");
            Set<int> set_linked = new SetHashList<int>(Compute.Equal, Hash.Default);
            for (int i = 0; i < test; i++)
                set_linked.Add(i);
            // Traversal
            Console.Write("    Traversal: ");
            set_linked.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            Console.Write("    Table Size: " + (set_linked as SetHashList<int>).TableSize);
            Console.WriteLine();
            Console.WriteLine();

            #endregion

            #region Map

            Console.WriteLine("  Testing MapHashList<int, int>--------------");
            Map<int, int> map_sethash = new MapHashLinked<int, int>(Compute.Equal, Hash.Default);
            for (int i = 0; i < test; i++)
                map_sethash.Add(i, i);
            Console.Write("    Look Ups: ");
            for (int i = 0; i < test; i++)
                Console.Write(map_sethash[i]);
            Console.WriteLine();
            // Traversal
            Console.Write("    Traversal: ");
            map_sethash.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            Console.Write("    Table Size: " + (map_sethash as MapHashLinked<int, int>).TableSize);
            Console.WriteLine();
            Console.WriteLine();

            #endregion

            #region OmnitreePoints
            {
                Console.WriteLine("  OmnitreePoints----------------");
                Console.WriteLine();
                Console.WriteLine("    An Omnitree is an ND SPT that allows for");
                Console.WriteLine("    multidimensional sorting. Any time you need to look");
                Console.WriteLine("    items up based on multiple fields/properties, then");
                Console.WriteLine("    you might want to use an Omnitree. If you need to");
                Console.WriteLine("    perform ranged queries on multiple dimensions, then");
                Console.WriteLine("    the Omnitree is the data structure for you.");
                Console.WriteLine();

                OmnitreePoints<int, double, double, double> omnitree =
                    new OmnitreePointsLinked<int, double, double, double>(
                        // This is a location delegate. (how to locate the item along each dimension)
                        (int index, out double a, out double b, out double c) =>
                        {
                            a = index;
                            b = index;
                            c = index;
                        });
                
                Console.Write("    Adding (0-" + test + ")...");
                for (int i = 0; i < test; i++)
                {
                    omnitree.Add(i);
                }
                Console.WriteLine();

                Console.Write("    Traversal: ");
                omnitree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                int minimum = random.Next(1, test / 2);
                int maximum = random.Next(1, test / 2) + test / 2;
                Console.Write("    Spacial Traversal [" +
                    "(" + minimum + ", " + minimum + ", " + minimum + ")->" +
                    "(" + maximum + ", " + maximum + ", " + maximum + ")]: ");
                omnitree.Stepper(i => Console.Write(i),
                    minimum, maximum,
                    minimum, maximum,
                    minimum, maximum);
                Console.WriteLine();

                // Note: this "look up" is just a very narrow spacial query that (since we know the data)
                // wil only give us one result.
                int lookUp = random.Next(0, test);
                Console.Write("    Look Up (" + lookUp + "): ");
                omnitree.Stepper(i => Console.Write(i),
                    lookUp, lookUp,
                    lookUp, lookUp,
                    lookUp, lookUp);
                Console.WriteLine();

                //void DoNothing() { }
                //// Ignoring dimensions on traversals example.
                //// If you want to ignore a column on a traversal, you can do so like this:
                //omnitree.Stepper(i => DoNothing(),
                //    lookUp, lookUp,
                //    Omnitree.Bound<int>.None, Omnitree.Bound<int>.None,
                //    Omnitree.Bound<int>.None, Omnitree.Bound<int>.None);
                //Console.WriteLine();

                Console.Write("    Counting Items In a Space [" +
                    "(" + minimum + ", " + minimum + ", " + minimum + ")->" +
                    "(" + maximum + ", " + maximum + ", " + maximum + ")]: " +
                    omnitree.CountSubSpace(
                        minimum, maximum,
                        minimum, maximum,
                        minimum, maximum));
                Console.WriteLine();

                int removalMinimum = random.Next(1, test / 2);
                int removalMaximum = random.Next(1, test / 2) + test / 2;
                Console.Write("    Remove (" + removalMinimum + "-" + removalMaximum +"): ");
                omnitree.Remove(
                    removalMinimum, removalMaximum,
                    removalMinimum, removalMaximum,
                    removalMinimum, removalMaximum);
                omnitree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                Console.WriteLine("    Dimensions: " + omnitree.Dimensions);
                Console.WriteLine("    Count: " + omnitree.Count);

                omnitree.Clear(); // Clears the Omnitree

                Console.WriteLine();

            }
            #endregion

            #region OmnitreeBounds
            {

                Console.WriteLine("  Testing OmnitreeBoundsLinked<int, double>-------");
                // Construction
                OmnitreeBounds<int, double, double, double> omnitreeBounds_linked = new OmnitreeBoundsLinked<int, double, double, double>(
                    (int index,
                     out double min1, out double max1,
                     out double min2, out double max2,
                     out double min3, out double max3) =>
                    {
                        min1 = index; max1 = index;
                        min2 = index; max2 = index;
                        min3 = index; max3 = index;
                    });

                // Properties
                Console.WriteLine("      Dimensions: " + omnitreeBounds_linked.Dimensions);
                Console.WriteLine("      Count: " + omnitreeBounds_linked.Count);

                // Addition
                Console.Write("    Adding 0-" + test + ": ");
                for (int i = 0; i < test; i++)
                    omnitreeBounds_linked.Add(i);
                omnitreeBounds_linked.Stepper((int current) => { Console.Write(current); });
                Console.WriteLine();
                Console.WriteLine("      Count: " + omnitreeBounds_linked.Count);
                // Traversal
                Console.Write("    Traversal [ALL]: ");
                omnitreeBounds_linked.Stepper((int current) => { Console.Write(current); });
                Console.WriteLine();
                // Look Up 1
                //Console.Write("    Traversal [(" + (test / 2) + ", " + (test / 2) + ", " + (test / 2) + ")->(" + test + ", " + test + ", " + test + ")]: ");
                //omnitreeBounds_linked.Stepper((int current) => { Console.Write(current); },
                //    test / 2, test,
                //    test / 2, test,
                //    test / 2, test);
                //Console.WriteLine();
                // Removal
                Console.Write("    Remove 0-" + test / 3 + ": ");
                omnitreeBounds_linked.RemoveOverlapped(
                    0, test / 3,
                    0, test / 3,
                    0, test / 3);
                omnitreeBounds_linked.Stepper((int current) => { Console.Write(current); });
                Console.WriteLine();
                Console.WriteLine("      Count: " + omnitreeBounds_linked.Count);
                // Clear
                Console.Write("    Clear: ");
                omnitreeBounds_linked.Clear();
                omnitreeBounds_linked.Stepper((int current) => { Console.Write(current); });
                Console.WriteLine();
                Console.WriteLine("      Count: " + omnitreeBounds_linked.Count);
                Console.WriteLine();

            }
            #endregion

            #region KD Tree

            ////List<KdTreeNode<float, string>> testNodes = new List_Linked<KdTreeNode<float, string>>();
            //KdTree_Linked<string, float> tree = new KdTree_Linked<string, float>(
            //	2,
            //	Logic.compare,
            //	float.MinValue,
            //	float.MaxValue,
            //	0,
            //	Arithmetic.Add,
            //	Arithmetic.Subtract,
            //	Arithmetic.Multiply);

            //List<KdTree_Linked<string, float>.Node> testNodes =
            //	new List_Linked<KdTree_Linked<string, float>.Node>
            //{
            //	new KdTree_Linked<string, float>.Node(new float[] { 5, 5 }, "Root"),
            //	new KdTree_Linked<string, float>.Node(new float[] { 2.5f, 2.5f }, "Root-Left"),
            //	new KdTree_Linked<string, float>.Node(new float[] { 7.5f, 7.5f }, "Root-Right"),
            //	new KdTree_Linked<string, float>.Node(new float[] { 1, 10 }, "Root-Left-Left"),
            //	new KdTree_Linked<string, float>.Node(new float[] { 10, 10 }, "Root-Right-Right")
            //};

            //foreach (var node in testNodes)
            //	if (!tree.Add(node.Point, node.Value))
            //		throw new Exception("Failed to add node to tree");

            //var nodesToRemove = new KdTreeNode<float, string>[] {
            //	testNodes[1], // Root-Left
            //	testNodes[0] // Root
            //};

            //foreach (var nodeToRemove in nodesToRemove)
            //{
            //	tree.RemoveAt(nodeToRemove.Point);
            //	testNodes.Remove(nodeToRemove);

            //	Assert.IsNull(tree.FindValue(nodeToRemove.Value));
            //	Assert.IsNull(tree.FindValueAt(nodeToRemove.Point));

            //	foreach (var testNode in testNodes)
            //	{
            //		Assert.AreEqual(testNode.Value, tree.FindValueAt(testNode.Point));
            //		Assert.AreEqual(testNode.Point, tree.FindValue(testNode.Value));
            //	}

            //	Assert.AreEqual(testNodes.Count, tree.Count);
            //}

            #endregion

            #region Graph

            Console.WriteLine("  Testing Graph_SetOmnitree<int>-------------");
            Graph<int> graph = new GraphSetOmnitree<int>(Compute.Equal, Compute.Compare, Hash.Default);
            // add nodes
            for (int i = 0; i < test; i++)
                graph.Add(i);
            // add edges
            for (int i = 0; i < test - 1; i++)
                graph.Add(i, i + 1);
            Console.Write("    Traversal: ");
            graph.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
            Console.WriteLine("    Edges: ");
            //((Graph_SetQuadtree<int>)graph)._edges.Foreach((Graph_SetQuadtree<int>.Edge e) => { Console.WriteLine("     " + e.Start + " " + e.End); });
            graph.Stepper(
                    (int current) =>
                    {
                        Console.Write("     " + current + ": ");
                        graph.Neighbors(current,
                        (int a) =>
                        {
                            Console.Write(a);
                        });
                        Console.WriteLine();
                    });
            Console.WriteLine();

            #endregion

            Console.WriteLine("============================================");
            Console.WriteLine("Examples Complete...");
            Console.ReadLine();
        }
    }
}
