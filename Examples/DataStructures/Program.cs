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
            Console.WriteLine("    Towel.DataStructures.DataStructure. A Link/Tuple is");
            Console.WriteLine("    used when you have a small, known-sized set of objects");
            Console.WriteLine("    that you want to bundle together without making a custom");
            Console.WriteLine("    custom class.");
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
            Console.WriteLine("    Towel.DataStructures.DataStructure. An array is used when");
            Console.WriteLine("    dealing with static-sized, known-sized sets of data. Arrays");
            Console.WriteLine("    can be sorted along 1 dimensions for binary searching algorithms.");
            Console.WriteLine();

            Indexed<int> indexed = new IndexedArray<int>(test);

            Console.Write("    Filling in (0-" + (test - 1) + ")...");
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
            Console.WriteLine("    the linked-list implementation. An Addable/List is used");
            Console.WriteLine("    when dealing with an unknown quantity of data that you");
            Console.WriteLine("    will likely have to enumerate/step through everything. The");
            Console.WriteLine("    AddableArray shares the properties of an Indexed/Array in");
            Console.WriteLine("    that it can be relateively quickly sorted along 1 dimensions");
            Console.WriteLine("    for binary search algorithms.");
            Console.WriteLine();

            // AddableArray ---------------------------------------
            Addable<int> addableArray = new AddableArray<int>(test);

            Console.Write("    [AddableArray] Adding (0-" + (test - 1) + ")...");
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

            Console.Write("    [AddableLinked] Adding (0-" + (test - 1) + ")...");
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
                Console.WriteLine("    the linked-list implementation. A FirstInLastOut/Stack is used");
                Console.WriteLine("    specifically when you need the algorithm provided by the Push");
                Console.WriteLine("    and Pop functions.");
                Console.WriteLine();

                FirstInLastOut<int> firstInLastOutArray = new FirstInLastOutArray<int>();

                Console.Write("    [FirstInLastOutArray] Pushing (0-" + (test - 1) + ")...");
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

                Console.Write("    [FirstInLastOutLinked] Pushing (0-" + (test - 1) + ")...");
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
                Console.WriteLine("    the linked-list implementation. A FirstInFirstOut/Stack is used");
                Console.WriteLine("    specifically when you need the algorithm provided by the Queue");
                Console.WriteLine("    and Dequeue functions.");
                Console.WriteLine();

                FirstInFirstOut<int> firstInFirstOutArray = new FirstInFirstOutArray<int>();

                Console.Write("    [FirstInFirstOutArray] Enqueuing (0-" + (test - 1) + ")...");
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

                Console.Write("    [FirstInFirstOutLinked] Enqueuing (0-" + (test - 1) + ")...");
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
            #endregion

            #region Heap
            {
                Console.WriteLine("  Heap---------------------------------");
                Console.WriteLine();
                Console.WriteLine("    An \"Heap\" is a binary tree that stores items based on priorities.");
                Console.WriteLine("    It implements Towel.DataStructures.DataStructure like the others.");
                Console.WriteLine("    It uses sifting algorithms to move nodes vertically through itself.");
                Console.WriteLine("    It is often the best data structure for standard priority queues.");
                Console.WriteLine("    \"HeapArray\" is an implementation where the tree has been flattened");
                Console.WriteLine("    into an array.");
                Console.WriteLine();

                Console.WriteLine("    Let's say the priority is how close a number is to \"5\".");
                Console.WriteLine("    So \"Dequeue\" will give us the next closest value to \"5\".");
                Comparison Priority(int a, int b)
                {
                    int _a = Compute.AbsoluteValue(a - 5);
                    int _b = Compute.AbsoluteValue(b - 5);
                    Comparison comparison = Compare.Wrap(_b.CompareTo(_a));
                    return comparison;
                }
                Console.WriteLine();

                Heap<int> heapArray = new HeapArray<int>(Priority);

                Console.Write("    [HeapArray] Enqueuing (0-" + (test - 1) + ")...");
                for (int i = 0; i < test; i++)
                {
                    heapArray.Enqueue(i);
                }
                Console.WriteLine();

                Console.WriteLine("    [HeapArray] Dequeue: " + heapArray.Dequeue());
                Console.WriteLine("    [HeapArray] Dequeue: " + heapArray.Dequeue());
                Console.WriteLine("    [HeapArray] Peek: " + heapArray.Peek());
                Console.WriteLine("    [HeapArray] Dequeue: " + heapArray.Dequeue());
                Console.WriteLine("    [HeapArray] Count: " + heapArray.Count);

                heapArray.Clear(); // Clears the heapArray

                Console.WriteLine();
            }
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
                Console.WriteLine("    It implements Towel.DataStructures.DataStructure like the others.");
                Console.WriteLine("    It allows for very fast 1D ranged queries/traversals.");
                Console.WriteLine("    It is very similar to an Red Black tree, but uses a different sorting algorithm.");
                Console.WriteLine();

                AvlTree<int> avlTree = new AvlTreeLinked<int>();

                Console.Write("    Adding (0-" + (test - 1) + ")...");
                for (int i = 0; i < test; i++)
                {
                    avlTree.Add(i);
                }
                Console.WriteLine();

                Console.Write("    Traversal: ");
                avlTree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                //// Note: Because the nodes in AVL Tree linked do not have
                //// a parent pointer, the IEnumerable "foreach" iteration
                //// is extremely slow and should be avoided. It requires
                //// a stack for it's iteration.
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
                Console.WriteLine("    It implements Towel.DataStructures.DataStructure like the others.");
                Console.WriteLine("    It allows for very fast 1D ranged queries/traversals.");
                Console.WriteLine("    It is very similar to an AVL tree, but uses a different sorting algorithm.");
                Console.WriteLine();

                RedBlackTree<int> redBlackTree = new RedBlackTreeLinked<int>();

                Console.Write("    Adding (0-" + (test - 1) + ")...");
                for (int i = 0; i < test; i++)
                {
                    redBlackTree.Add(i);
                }
                Console.WriteLine();

                Console.Write("    Traversal: ");
                redBlackTree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                int minimum = random.Next(1, test / 2);
                int maximum = random.Next(1, test / 2) + test / 2;
                Console.Write("    Ranged Traversal [" + minimum + "-" + maximum + "]: ");
                redBlackTree.Stepper(i => Console.Write(i), minimum, maximum);
                Console.WriteLine();

                int removal = random.Next(0, test);
                Console.Write("    Remove(" + removal + "): ");
                redBlackTree.Remove(removal);
                redBlackTree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                int contains = random.Next(0, test);
                Console.WriteLine("    Contains(" + contains + "): " + redBlackTree.Contains(contains));
                Console.WriteLine("    Current Least: " + redBlackTree.CurrentLeast);
                Console.WriteLine("    Current Greatest: " + redBlackTree.CurrentGreatest);
                Console.WriteLine("    Count: " + redBlackTree.Count);

                redBlackTree.Clear(); // Clears the Red Black tree

                Console.WriteLine();
            }
            #endregion

            #region BTree
            {
                Console.WriteLine("  B Tree------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    A B Tree is a sorted binary tree that allows multiple values to");
                Console.WriteLine("    be stored per node. This makes it sort of a hybrid between a");
                Console.WriteLine("    binary tree and an array. Because multiple values are stored ");
                Console.WriteLine("    per node, it means less nodes must be traversed to completely");
                Console.WriteLine("    traverse the values in the B tree.");
                Console.WriteLine();

                Console.WriteLine("    The generic B Tree in Towel is still in development.");

                Console.WriteLine();
            }
            #endregion

            #region Set
            {
                Console.WriteLine("  Set------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    A Set is like an Addable/List, but it does not allow duplicates. Sets are");
                Console.WriteLine("    usually implemented using hash codes. Implementations with hash codes");
                Console.WriteLine("    usually have very fast \"Contains\" checks to see if a value has already");
                Console.WriteLine("    been added to the set.");
                Console.WriteLine();

                Set<int> setHashLinked = new SetHashLinked<int>();

                Console.Write("    Adding (0-" + (test - 1) + ")...");
                for (int i = 0; i < test; i++)
                {
                    setHashLinked.Add(i);
                }
                Console.WriteLine();

                Console.Write("    Traversal: ");
                setHashLinked.Stepper(i => Console.Write(i));
                Console.WriteLine();

                int a = random.Next(0, test);
                setHashLinked.Remove(a);
                Console.Write("    Remove(" + a + "): ");
                setHashLinked.Stepper(i => Console.Write(i));
                Console.WriteLine();

                int b = random.Next(0, test);
                Console.WriteLine("    Contains(" + b + "): " + setHashLinked.Contains(b));
                Console.WriteLine("    Count: " + setHashLinked.Count);

                Console.WriteLine();
            }
            #endregion

            #region Map (aka Dictionary)
            {
                Console.WriteLine("  Map------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    A Map (aka Dictionary) is similar to a Set, but it stores two values (a ");
                Console.WriteLine("    key and a value). Maps do not allow duplicate keys much like Sets don't");
                Console.WriteLine("    allow duplicate values. When provided with the key, the Map uses that key");
                Console.WriteLine("    to look up the value that it is associated with. Thus, it allows you to ");
                Console.WriteLine("    \"map\" one object to another. As with Sets, Maps are usually implemented");
                Console.WriteLine("    using hash codes.");
                Console.WriteLine();

                // Note: the first generic is the value, the second is the key
                Map<string, int> mapHashLinked = new MapHashLinked<string, int>();

                Console.WriteLine("    Let's map each int to its word representation (ex 1 -> One).");

                Console.Write("    Adding (0-" + (test - 1) + ")...");
                for (int i = 0; i < test; i++)
                {
                    mapHashLinked.Add(i, ((decimal)i).ToWords());
                }
                Console.WriteLine();

                Console.WriteLine("    Traversal: ");
                mapHashLinked.Keys(i => Console.WriteLine("      " + i + "->" + mapHashLinked[i]));
                Console.WriteLine();

                int a = random.Next(0, test);
                mapHashLinked.Remove(a);
                Console.Write("    Remove(" + a + "): ");
                mapHashLinked.Keys(i => Console.Write(i));
                Console.WriteLine();

                int b = random.Next(0, test);
                Console.WriteLine("    Contains(" + b + "): " + mapHashLinked.Contains(b));
                Console.WriteLine("    Count: " + mapHashLinked.Count);

                Console.WriteLine();
            }
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

                Console.Write("    Adding (0-" + (test - 1) + ")...");
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

                Console.Write("    Adding (0-" + (test - 1) + ")...");
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
            {
                Console.WriteLine("  KD Tree------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    A KD Tree binary tree that stores points sorted along along an");
                Console.WriteLine("    arbitrary number of dimensions. So it performs multidimensional");
                Console.WriteLine("    sorting similar to the Omnitree (Quadtree/Octree) in Towel, but");
                Console.WriteLine("    it uses a completely different algorithm and format.");
                Console.WriteLine();

                Console.WriteLine("    The generic KD Tree in Towel is still in development.");

                Console.WriteLine();
            }
            #endregion

            #region Graph
            {
                Console.WriteLine("  Graph------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    A Graph is a data structure of nodes and edges. Nodes are values");
                Console.WriteLine("    and edges are connections between those values. Graphs are often");
                Console.WriteLine("    used to model real world data such as maps, and are often used in");
                Console.WriteLine("    path finding algoritms. See the \"Algorithms\" example for path");
                Console.WriteLine("    finding examples. This is just an example of how to make a graph.");
                Console.WriteLine("    A \"GraphSetOmnitree\" is an implementation where nodes are stored.");
                Console.WriteLine("    in a Set and edges are stored in an Omnitree (aka Quadtree).");
                Console.WriteLine();

                Graph<int> graphSetOmnitree = new GraphSetOmnitree<int>();

                Console.WriteLine("    Adding Nodes (0-" + (test - 1) + ")...");
                for (int i = 0; i < test; i++)
                {
                    graphSetOmnitree.Add(i);
                }
                
                int edgesPerNode = 3;
                Console.WriteLine("    Adding Random Edges (0-3 per node)...");
                for (int i = 0; i < test; i++)
                {
                    // lets use a heap to randomize the edges using random priorities
                    Heap<(int, int)> heap = new HeapArray<(int, int)>((x, y) => Compare.Wrap(x.Item2.CompareTo(y.Item2)));
                    for (int j = 0; j < test; j++)
                    {
                        if (j != i)
                        {
                            heap.Enqueue((j, random.Next()));
                        }
                    }
                    
                    // dequeue some random edges from the heap and add them to the graph
                    int randomEdgeCount = random.Next(edgesPerNode + 1);
                    for (int j = 0; j < randomEdgeCount; j++)
                    {
                        graphSetOmnitree.Add(i, heap.Dequeue().Item1);
                    }
                }

                Console.Write("    Nodes (Traversal): ");
                graphSetOmnitree.Stepper(i => Console.Write(i));
                Console.WriteLine();

                Console.WriteLine("    Edges (Traversal): ");
                graphSetOmnitree.Stepper((from, to) => Console.WriteLine("      " + from + "->" + to));
                Console.WriteLine();

                int a = random.Next(0, test);
                Console.Write("    Neighbors (" + a + "):");
                graphSetOmnitree.Neighbors(a, i => Console.Write(" " + i));
                Console.WriteLine();
                
                int b = random.Next(0, test / 2);
                int c = random.Next(test / 2, test);
                Console.WriteLine("    Are Adjacent (" + b + ", " + c + "): " + graphSetOmnitree.Adjacent(b, c));
                Console.WriteLine("    Node Count: " + graphSetOmnitree.NodeCount);
                Console.WriteLine("    Edge Count: " + graphSetOmnitree.EdgeCount);

                graphSetOmnitree.Clear(); // Clears the graph

                Console.WriteLine();
            }
            #endregion

            #region Trie
            {
                Console.WriteLine("  Trie------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("    A Trie is a tree where portions of the data are stored in each node");
                Console.WriteLine("    such that when you traverse the tree to a leaf, you have read the contents");
                Console.WriteLine("    of that leaf along the way. Because of this, a Trie allows for its values");
                Console.WriteLine("    to share data, which is a form of compression. So a Trie may be used to save");
                Console.WriteLine("    memory. A trie may also be a very useful tool in pattern matching, because it");
                Console.WriteLine("    it allows for culling based are portions of the data.");
                Console.WriteLine();

                Console.WriteLine("    The generic Trie in Towel is still in development.");

                Console.WriteLine();
            }
            #endregion

            Console.WriteLine("============================================");
            Console.WriteLine("Examples Complete...");
            Console.ReadLine();
        }
    }
}
