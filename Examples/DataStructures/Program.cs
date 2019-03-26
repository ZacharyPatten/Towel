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

            #region Link (aka Tuple)

            Console.WriteLine("  Link------------------------------------");
            Console.WriteLine();
            Console.WriteLine("    A \"Link\" is like a System.Tuple that implements");
            Console.WriteLine("    Towel.DataStructures.DataStructure.");
            Console.WriteLine();

            Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
            Console.Write("    Traversal: ");
            link.Stepper(i => Console.Write(i));
            Console.WriteLine();

            Console.WriteLine("    Size: " + link.Size);
            Console.WriteLine();

            #endregion

            #region Indexed (aka Array)

            Console.WriteLine("  Indexed---------------------------------");
            Console.WriteLine();
            Console.WriteLine("    An \"Indexed\" is just a wrapper for arrays that implements");
            Console.WriteLine("    Towel.DataStructures.DataStructure.");
            Console.WriteLine();

            Indexed<int> indexed = new IndexedArray<int>(test);

            Console.Write("    Filling in (0-" + test + ")...");
            for (int i = 0; i < test; i++)
            {
                indexed[i] = i;
            }
            Console.WriteLine();

            Console.Write("    Traversal: ");
            indexed.Stepper(i => Console.Write(i));
            Console.WriteLine();

            Console.WriteLine("    Length: " + indexed.Length);

            Console.WriteLine();

            #endregion

            #region Addable (aka List)

            Console.WriteLine("  Addable---------------------------------");
            Console.WriteLine();
            Console.WriteLine("    An \"Addable\" is like an IList that implements");
            Console.WriteLine("    Towel.DataStructures.DataStructure. \"AddableArray\" is");
            Console.WriteLine("    the array implementation while \"AddableLinked\" is the");
            Console.WriteLine("    the linked-list implementation.");
            Console.WriteLine();

            // AddableArray ---------------------------------------
            Addable<int> addableArray = new AddableArray<int>(test);

            Console.Write("    [AddableArray] Adding (0-" + test + ")...");
            for (int i = 0; i < test; i++)
            {
                addableArray.Add(i);
            }
            Console.WriteLine();

            Console.Write("    [AddableArray] Traversal: ");
            addableArray.Stepper(i => Console.Write(i));
            Console.WriteLine();

            Console.WriteLine("    [AddableArray] Count: " + addableArray.Count);

            addableArray.Clear(); // Clears the addable

            Console.WriteLine();

            // AddableLinked ---------------------------------------
            Addable<int> addableLinked = new AddableLinked<int>();

            Console.Write("    [AddableLinked] Adding (0-" + test + ")...");
            for (int i = 0; i < test; i++)
            {
                addableLinked.Add(i);
            }
            Console.WriteLine();

            Console.Write("    [AddableLinked] Traversal: ");
            addableLinked.Stepper(i => Console.Write(i));
            Console.WriteLine();

            Console.WriteLine("    [AddableLinked] Count: " + addableLinked.Count);

            addableLinked.Clear(); // Clears the addable

            Console.WriteLine();

            #endregion

            #region FirstInLastOut (aka stack)
            {
                Console.WriteLine("  FirstInLastOut---------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An \"FirstInLastOut\" is a Stack that implements");
                Console.WriteLine("    Towel.DataStructures.DataStructure. \"FirstInLastOutArray\" is");
                Console.WriteLine("    the array implementation while \"FirstInLastOutLinked\" is the");
                Console.WriteLine("    the linked-list implementation.");
                Console.WriteLine();

                FirstInLastOut<int> firstInLastOutArray = new FirstInLastOutArray<int>();

                Console.Write("    [FirstInLastOutArray] Pushing (0-" + test + ")...");
                for (int i = 0; i < test; i++)
                {
                    firstInLastOutArray.Push(i);
                }
                Console.WriteLine();

                Console.Write("    [FirstInLastOutArray] Traversal: ");
                firstInLastOutArray.Stepper(i => Console.Write(i));
                Console.WriteLine();

                Console.WriteLine("    [FirstInLastOutArray] Pop: " + firstInLastOutArray.Pop());
                Console.WriteLine("    [FirstInLastOutArray] Pop: " + firstInLastOutArray.Pop());
                Console.WriteLine("    [FirstInLastOutArray] Peek: " + firstInLastOutArray.Peek());
                Console.WriteLine("    [FirstInLastOutArray] Pop: " + firstInLastOutArray.Pop());
                Console.WriteLine("    [FirstInLastOutArray] Count: " + firstInLastOutArray.Count);

                firstInLastOutArray.Clear(); // Clears the firstInLastOut

                Console.WriteLine();

                FirstInLastOut<int> firstInLastOutLinked = new FirstInLastOutLinked<int>();

                Console.Write("    [FirstInLastOutLinked] Pushing (0-" + test + ")...");
                for (int i = 0; i < test; i++)
                {
                    firstInLastOutLinked.Push(i);
                }
                Console.WriteLine();

                Console.Write("    [FirstInLastOutLinked] Traversal: ");
                firstInLastOutLinked.Stepper(i => Console.Write(i));
                Console.WriteLine();

                Console.WriteLine("    [FirstInLastOutLinked] Pop: " + firstInLastOutLinked.Pop());
                Console.WriteLine("    [FirstInLastOutLinked] Pop: " + firstInLastOutLinked.Pop());
                Console.WriteLine("    [FirstInLastOutLinked] Peek: " + firstInLastOutLinked.Peek());
                Console.WriteLine("    [FirstInLastOutLinked] Pop: " + firstInLastOutLinked.Pop());
                Console.WriteLine("    [FirstInLastOutLinked] Count: " + firstInLastOutLinked.Count);

                firstInLastOutLinked.Clear(); // Clears the firstInLastOut

                Console.WriteLine();
            }
            #endregion

            #region FirstInFirstOut (aka Queue)
            {
                Console.WriteLine("  FirstInFirstOut---------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An \"FirstInFirstOut\" is a Queue that implements");
                Console.WriteLine("    Towel.DataStructures.DataStructure. \"FirstInFirstOutArray\" is");
                Console.WriteLine("    the array implementation while \"FirstInFirstOutLinked\" is the");
                Console.WriteLine("    the linked-list implementation.");
                Console.WriteLine();

                FirstInFirstOut<int> firstInFirstOutArray = new FirstInFirstOutArray<int>();

                Console.Write("    [FirstInFirstOutArray] Enqueuing (0-" + test + ")...");
                for (int i = 0; i < test; i++)
                {
                    firstInFirstOutArray.Enqueue(i);
                }
                Console.WriteLine();

                Console.Write("    [FirstInFirstOutArray] Traversal: ");
                firstInFirstOutArray.Stepper(i => Console.Write(i));
                Console.WriteLine();

                Console.WriteLine("    [FirstInFirstOutArray] Dequeue: " + firstInFirstOutArray.Dequeue());
                Console.WriteLine("    [FirstInFirstOutArray] Dequeue: " + firstInFirstOutArray.Dequeue());
                Console.WriteLine("    [FirstInFirstOutArray] Peek: " + firstInFirstOutArray.Peek());
                Console.WriteLine("    [FirstInFirstOutArray] Dequeue: " + firstInFirstOutArray.Dequeue());
                Console.WriteLine("    [FirstInFirstOutArray] Count: " + firstInFirstOutArray.Count);

                firstInFirstOutArray.Clear(); // Clears the firstInLastOut

                Console.WriteLine();

                FirstInFirstOut<int> firstInFirstOutLinked = new FirstInFirstOutLinked<int>();

                Console.Write("    [FirstInFirstOutLinked] Enqueuing (0-" + test + ")...");
                for (int i = 0; i < test; i++)
                {
                    firstInFirstOutLinked.Enqueue(i);
                }
                Console.WriteLine();

                Console.Write("    [FirstInFirstOutLinked] Traversal: ");
                firstInFirstOutLinked.Stepper(i => Console.Write(i));
                Console.WriteLine();

                Console.WriteLine("    [FirstInFirstOutLinked] Pop: " + firstInFirstOutLinked.Dequeue());
                Console.WriteLine("    [FirstInFirstOutLinked] Pop: " + firstInFirstOutLinked.Dequeue());
                Console.WriteLine("    [FirstInFirstOutLinked] Peek: " + firstInFirstOutLinked.Peek());
                Console.WriteLine("    [FirstInFirstOutLinked] Pop: " + firstInFirstOutLinked.Dequeue());
                Console.WriteLine("    [FirstInFirstOutLinked] Count: " + firstInFirstOutLinked.Count);

                firstInFirstOutLinked.Clear(); // Clears the firstInLastOut

                Console.WriteLine();
            }

            Console.WriteLine("  Testing Queue_Linked<int>------------------");
            FirstInFirstOut<int> queue_linked = new FirstInFirstOutLinked<int>();
            for (int i = 0; i < test; i++)
                queue_linked.Enqueue(i);
            Console.Write("    Traversal: ");
            queue_linked.Stepper((int current) => { Console.Write(current); });
            Console.WriteLine();
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
            Console.WriteLine();

            #endregion

            #region Tree

            //Console.WriteLine("  Tree-----------------------------");

            //Tree<int> tree_Map = new TreeMap<int>(0, Compute.Equal, Hash.Default);

            //for (int i = 1; i < test; i++)
            //{
            //    tree_Map.Add(i, i / Compute.SquareRoot(i));
            //}
            //Console.Write("    Children of 0 (root): ");
            //tree_Map.Children(0, (int i) => { Console.Write(i + " "); });
            //Console.WriteLine();
            //Console.Write("    Children of " + ((int)System.Math.Sqrt(test) - 1) + " (root): ");
            //tree_Map.Children(((int)System.Math.Sqrt(test) - 1), (int i) => { Console.Write(i + " "); });
            //Console.WriteLine();
            //Console.Write("    Traversal: ");
            //tree_Map.Stepper((int i) => { Console.Write(i + " "); });
            //Console.WriteLine();

            //Console.WriteLine();

            #endregion

            #region AVL Tree
            {
                Console.WriteLine("  AvlTree------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An AVL Tree is a sorted binary tree.");
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
            {
                Console.WriteLine("  Red-Black Tree------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An Red-Black Tree is a sorted binary tree.");
                Console.WriteLine("    It allows for very fast 1D ranged queries/traversals.");
                Console.WriteLine();

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
            }
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

            #region Map (aka Dictionary)

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
                Console.WriteLine("  OmnitreePoints--------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An Omnitree is an ND SPT that allows for");
                Console.WriteLine("    multidimensional sorting. Any time you need to look");
                Console.WriteLine("    items up based on multiple fields/properties, then");
                Console.WriteLine("    you might want to use an Omnitree. If you need to");
                Console.WriteLine("    perform ranged queries on multiple dimensions, then");
                Console.WriteLine("    the Omnitree is the data structure for you.");
                Console.WriteLine();
                Console.WriteLine("    The \"OmnitreePoints\" stores individual points (vectors),");
                Console.WriteLine("    and the \"OmnitreeBounds\" stores bounded objects (spaces).");
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
                Console.Write("    Remove (" + removalMinimum + "-" + removalMaximum + "): ");
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
                Console.WriteLine("  OmnitreeBounds--------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An Omnitree is an ND SPT that allows for");
                Console.WriteLine("    multidimensional sorting. Any time you need to look");
                Console.WriteLine("    items up based on multiple fields/properties, then");
                Console.WriteLine("    you might want to use an Omnitree. If you need to");
                Console.WriteLine("    perform ranged queries on multiple dimensions, then");
                Console.WriteLine("    the Omnitree is the data structure for you.");
                Console.WriteLine();
                Console.WriteLine("    The \"OmnitreePoints\" stores individual points (vectors),");
                Console.WriteLine("    and the \"OmnitreeBounds\" stores bounded objects (spaces).");
                Console.WriteLine();

                OmnitreeBounds<int, double, double, double> omnitree =
                    new OmnitreeBoundsLinked<int, double, double, double>(
                    // This is a location delegate. (how to locate the item along each dimension)
                    (int index,
                     out double min1, out double max1,
                     out double min2, out double max2,
                     out double min3, out double max3) =>
                    {
                        min1 = index; max1 = index;
                        min2 = index; max2 = index;
                        min3 = index; max3 = index;
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
                omnitree.StepperOverlapped(i => Console.Write(i),
                    minimum, maximum,
                    minimum, maximum,
                    minimum, maximum);
                Console.WriteLine();

                // Note: this "look up" is just a very narrow spacial query that (since we know the data)
                // wil only give us one result.
                int lookUp = random.Next(0, test);
                Console.Write("    Look Up (" + lookUp + "): ");
                omnitree.StepperOverlapped(i => Console.Write(i),
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
                    omnitree.CountSubSpaceOverlapped(
                        minimum, maximum,
                        minimum, maximum,
                        minimum, maximum));
                Console.WriteLine();

                int removalMinimum = random.Next(1, test / 2);
                int removalMaximum = random.Next(1, test / 2) + test / 2;
                Console.Write("    Remove (" + removalMinimum + "-" + removalMaximum + "): ");
                omnitree.RemoveOverlapped(
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

            Console.WriteLine("  Graph--------------------------------");
            Graph<int> graph = new GraphSetOmnitree<int>();
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
