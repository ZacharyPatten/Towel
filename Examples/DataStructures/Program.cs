using System;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace DataStructures
{
	class Program
	{
		static void Main()
		{
			Random random = new();
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
			link.Stepper(Console.Write);
			Console.WriteLine();

			Console.WriteLine($"    Size: {link.Size}");
			Console.WriteLine();

			#endregion

			#region Array

			Console.WriteLine("  Array---------------------------------");
			Console.WriteLine();
			Console.WriteLine("    An Array<T> is just a wrapper for arrays that implements");
			Console.WriteLine("    Towel.DataStructures.DataStructure. An array is used when");
			Console.WriteLine("    dealing with static-sized, known-sized sets of data. Arrays");
			Console.WriteLine("    can be sorted along 1 dimensions for binary searching algorithms.");
			Console.WriteLine();

			IArray<int> array = new Array<int>(test);

			Console.Write($"    Filling in (0-{test - 1})...");
			for (int i = 0; i < test; i++)
			{
				array[i] = i;
			}
			Console.WriteLine();

			Console.Write("    Traversal: ");
			array.Stepper(Console.Write);
			Console.WriteLine();

			Console.WriteLine($"    Length: {array.Length}");

			Console.WriteLine();

			#endregion

			#region List

			Console.WriteLine("  List---------------------------------");
			Console.WriteLine();
			Console.WriteLine("    An List is like an IList that implements");
			Console.WriteLine("    Towel.DataStructures.DataStructure. \"ListArray\" is");
			Console.WriteLine("    the array implementation while \"ListLinked\" is the");
			Console.WriteLine("    the linked-list implementation. An List is used");
			Console.WriteLine("    when dealing with an unknown quantity of data that you");
			Console.WriteLine("    will likely have to enumerate/step through everything. The");
			Console.WriteLine("    ListArray shares the properties of an Array in");
			Console.WriteLine("    that it can be relateively quickly sorted along 1 dimensions");
			Console.WriteLine("    for binary search algorithms.");
			Console.WriteLine();

			// ListArray ---------------------------------------
			IList<int> listArray = new ListArray<int>(test);

			Console.Write($"    [ListArray] Adding (0-{test - 1})...");
			for (int i = 0; i < test; i++)
			{
				listArray.Add(i);
			}
			Console.WriteLine();

			Console.Write("    [ListArray] Traversal: ");
			listArray.Stepper(Console.Write);
			Console.WriteLine();

			Console.WriteLine($"    [ListArray] Count: {listArray.Count}");

			listArray.Clear();

			Console.WriteLine();

			// ListLinked ---------------------------------------
			IList<int> listLinked = new ListLinked<int>();

			Console.Write($"    [ListLinked] Adding (0-{test - 1})...");
			for (int i = 0; i < test; i++)
			{
				listLinked.Add(i);
			}
			Console.WriteLine();

			Console.Write("    [ListLinked] Traversal: ");
			listLinked.Stepper(Console.Write);
			Console.WriteLine();

			Console.WriteLine($"    [ListLinked] Count: {listLinked.Count}");

			listLinked.Clear();

			Console.WriteLine();

			#endregion

			#region Stack
			{
				Console.WriteLine("  Stack---------------------------------");
				Console.WriteLine();
				Console.WriteLine("    An \"Stack\" is a Stack that implements");
				Console.WriteLine("    Towel.DataStructures.DataStructure. \"StackArray\" is");
				Console.WriteLine("    the array implementation while \"StackLinked\" is the");
				Console.WriteLine("    the linked-list implementation. A Stack is used");
				Console.WriteLine("    specifically when you need the algorithm provided by the Push");
				Console.WriteLine("    and Pop functions.");
				Console.WriteLine();

				IStack<int> stackArray = new StackArray<int>();

				Console.Write($"    [StackArray] Pushing (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					stackArray.Push(i);
				}
				Console.WriteLine();

				Console.Write("    [StackArray] Traversal: ");
				stackArray.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine($"    [StackArray] Pop: {stackArray.Pop()}");
				Console.WriteLine($"    [StackArray] Pop: {stackArray.Pop()}");
				Console.WriteLine($"    [StackArray] Peek: {stackArray.Peek()}");
				Console.WriteLine($"    [StackArray] Pop: {stackArray.Pop()}");
				Console.WriteLine($"    [StackArray] Count: {stackArray.Count}");

				stackArray.Clear();

				Console.WriteLine();

				IStack<int> stackLinked = new StackLinked<int>();

				Console.Write($"    [StackLinked] Pushing (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					stackLinked.Push(i);
				}
				Console.WriteLine();

				Console.Write("    [StackLinked] Traversal: ");
				stackLinked.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine($"    [StackLinked] Pop: {stackLinked.Pop()}");
				Console.WriteLine($"    [StackLinked] Pop: {stackLinked.Pop()}");
				Console.WriteLine($"    [StackLinked] Peek: {stackLinked.Peek()}");
				Console.WriteLine($"    [StackLinked] Pop: {stackLinked.Pop()}");
				Console.WriteLine($"    [StackLinked] Count: {stackLinked.Count}");

				stackLinked.Clear();

				Console.WriteLine();
			}
			#endregion

			#region Queue
			{
				Console.WriteLine("  Queue---------------------------------");
				Console.WriteLine();
				Console.WriteLine("    An \"Queue\" is a Queue that implements");
				Console.WriteLine("    Towel.DataStructures.DataStructure. \"QueueArray\" is");
				Console.WriteLine("    the array implementation while \"QueueLinked\" is the");
				Console.WriteLine("    the linked-list implementation. A Queue/Stack is used");
				Console.WriteLine("    specifically when you need the algorithm provided by the Queue");
				Console.WriteLine("    and Dequeue functions.");
				Console.WriteLine();

				IQueue<int> queueArray = new QueueArray<int>();

				Console.Write($"    [QueueArray] Enqueuing (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					queueArray.Enqueue(i);
				}
				Console.WriteLine();

				Console.Write("    [QueueArray] Traversal: ");
				queueArray.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine($"    [QueueArray] Dequeue: {queueArray.Dequeue()}");
				Console.WriteLine($"    [QueueArray] Dequeue: {queueArray.Dequeue()}");
				Console.WriteLine($"    [QueueArray] Peek: {queueArray.Peek()}");
				Console.WriteLine($"    [QueueArray] Dequeue: {queueArray.Dequeue()}");
				Console.WriteLine($"    [QueueArray] Count: {queueArray.Count}");

				queueArray.Clear();

				Console.WriteLine();

				IQueue<int> queueLinked = new QueueLinked<int>();

				Console.Write($"    [QueueLinked] Enqueuing (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					queueLinked.Enqueue(i);
				}
				Console.WriteLine();

				Console.Write("    [QueueLinked] Traversal: ");
				queueLinked.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine($"    [QueueLinked] Pop: {queueLinked.Dequeue()}");
				Console.WriteLine($"    [QueueLinked] Pop: {queueLinked.Dequeue()}");
				Console.WriteLine($"    [QueueLinked] Peek: {queueLinked.Peek()}");
				Console.WriteLine($"    [QueueLinked] Pop: {queueLinked.Dequeue()}");
				Console.WriteLine($"    [QueueLinked] Count: {queueLinked.Count}");

				queueLinked.Clear();

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

				static CompareResult Priority(int a, int b)
				{
					int _a = AbsoluteValue(a - 5);
					int _b = AbsoluteValue(b - 5);
					CompareResult comparison = Compare(_b, _a);
					return comparison;
				}
				Console.WriteLine();

				IHeap<int> heapArray = HeapArray.New<int>(Priority);

				Console.Write($"    [HeapArray] Enqueuing (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					heapArray.Enqueue(i);
				}
				Console.WriteLine();

				Console.WriteLine($"    [HeapArray] Dequeue: {heapArray.Dequeue()}");
				Console.WriteLine($"    [HeapArray] Dequeue: {heapArray.Dequeue()}");
				Console.WriteLine($"    [HeapArray] Peek: {heapArray.Peek()}");
				Console.WriteLine($"    [HeapArray] Dequeue: {heapArray.Dequeue()}");
				Console.WriteLine($"    [HeapArray] Count: {heapArray.Count}");

				heapArray.Clear();

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

				IAvlTree<int> avlTree = AvlTreeLinked.New<int>();

				Console.Write($"    Adding (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					avlTree.Add(i);
				}
				Console.WriteLine();

				Console.Write("    Traversal: ");
				avlTree.Stepper(Console.Write);
				Console.WriteLine();

				//// The "foreach" enumeration works for avl trees, but it is not optimized
				//// and you should prefer the stepper function (it is faster).
				//
				//Console.Write("    Traversal Foreach: ");
				//foreach (int i in avlTree)
				//{
				//    Console.Write(i);
				//}
				//Console.WriteLine();

				int minimum = random.Next(1, test / 2);
				int maximum = random.Next(1, test / 2) + test / 2;
				Console.Write($"    Ranged Traversal [{minimum}-{maximum}]: ");
				avlTree.Stepper(minimum, maximum, Console.Write);
				Console.WriteLine();

				int removal = random.Next(0, test);
				Console.Write($"    Remove({removal}): ");
				avlTree.Remove(removal);
				avlTree.Stepper(Console.Write);
				Console.WriteLine();

				int contains = random.Next(0, test);
				Console.WriteLine($"    Contains({contains}): {avlTree.Contains(contains)}");
				Console.WriteLine($"    Current Least: {avlTree.CurrentLeast}");
				Console.WriteLine($"    Current Greatest: {avlTree.CurrentGreatest}");
				Console.WriteLine($"    Count: {avlTree.Count}");

				avlTree.Clear();

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

				IRedBlackTree<int> redBlackTree = RedBlackTreeLinked.New<int>();

				Console.Write($"    Adding (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					redBlackTree.Add(i);
				}
				Console.WriteLine();

				Console.Write("    Traversal: ");
				redBlackTree.Stepper(Console.Write);
				Console.WriteLine();

				int minimum = random.Next(1, test / 2);
				int maximum = random.Next(1, test / 2) + test / 2;
				Console.Write($"    Ranged Traversal [{minimum}-{maximum}]: ");
				redBlackTree.Stepper(minimum, maximum, Console.Write);
				Console.WriteLine();

				int removal = random.Next(0, test);
				Console.Write($"    Remove({removal}): ");
				redBlackTree.Remove(removal);
				redBlackTree.Stepper(Console.Write);
				Console.WriteLine();

				int contains = random.Next(0, test);
				Console.WriteLine($"    Contains({contains}): {redBlackTree.Contains(contains)}");
				Console.WriteLine($"    Current Least: {redBlackTree.CurrentLeast}");
				Console.WriteLine($"    Current Greatest: {redBlackTree.CurrentGreatest}");
				Console.WriteLine($"    Count: {redBlackTree.Count}");

				redBlackTree.Clear();

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
				Console.WriteLine("    A Set is like an List, but it does not allow duplicates. Sets are");
				Console.WriteLine("    usually implemented using hash codes. Implementations with hash codes");
				Console.WriteLine("    usually have very fast \"Contains\" checks to see if a value has already");
				Console.WriteLine("    been added to the set.");
				Console.WriteLine();

				ISet<int> setHashLinked = SetHashLinked.New<int>();

				Console.Write($"    Adding (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					setHashLinked.Add(i);
				}
				Console.WriteLine();

				Console.Write("    Traversal: ");
				setHashLinked.Stepper(Console.Write);
				Console.WriteLine();

				int a = random.Next(0, test);
				setHashLinked.Remove(a);
				Console.Write($"    Remove({a}): ");
				setHashLinked.Stepper(Console.Write);
				Console.WriteLine();

				int b = random.Next(0, test);
				Console.WriteLine($"    Contains({b}): {setHashLinked.Contains(b)}");
				Console.WriteLine($"    Count: {setHashLinked.Count}");

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
				IMap<string, int> mapHashLinked = MapHashLinked.New<string, int>();

				Console.WriteLine("    Let's map each int to its word representation (ex 1 -> One).");

				Console.Write($"    Adding (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					mapHashLinked.Add(i, ((decimal)i).ToEnglishWords());
				}
				Console.WriteLine();

				Console.WriteLine("    Traversal: ");
				mapHashLinked.Pairs(pair => Console.WriteLine($"      {pair.Key}->{pair.Value}"));
				Console.WriteLine();

				int a = random.Next(0, test);
				mapHashLinked.Remove(a);
				Console.Write($"    Remove({a}): ");
				mapHashLinked.Keys(key => Console.Write(key));
				Console.WriteLine();

				int b = random.Next(0, test);
				Console.WriteLine($"    Contains({b}): {mapHashLinked.Contains(b)}");
				Console.WriteLine($"    Count: {mapHashLinked.Count}");

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

				IOmnitreePoints<int, double, string, decimal> omnitree =
					new OmnitreePointsLinked<int, double, string, decimal>(
						// This is a location delegate. (how to locate the item along each dimension)
						(int index, out double a, out string b, out decimal c) =>
						{
							a = index;
							b = index.ToString();
							c = index;
						});

				Console.Write($"    Adding (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					omnitree.Add(i);
				}
				Console.WriteLine();

				Console.Write("    Traversal: ");
				omnitree.Stepper(Console.Write);
				Console.WriteLine();

				//// The "foreach" enumeration works for omnitrees, but it is not optimized
				//// and you should prefer the stepper function (it is faster).
				//
				//Console.Write("    Traversal (Foreach): ");
				//foreach (var i in omnitree)
				//{
				//	Console.Write(i);
				//}
				//Console.WriteLine();

				int minimumXZ = random.Next(1, test / 2);
				int maximumXZ = random.Next(1, test / 2) + test / 2;
				string minimumY = minimumXZ.ToString();
				string maximumY = maximumXZ.ToString();
				Console.Write("    Spacial Traversal [" +
					$"({minimumXZ}, \"{minimumY}\", {minimumXZ}m)->" +
					$"({maximumXZ}, \"{maximumY}\", {maximumXZ}m)]: ");
				omnitree.Stepper(Console.Write,
					minimumXZ, maximumXZ,
					minimumY, maximumY,
					minimumXZ, maximumXZ);
				Console.WriteLine();

				// Note: this "look up" is just a very narrow spacial query that (since we know the data)
				// wil only give us one result.
				int lookUp = random.Next(0, test);
				string lookUpToString = lookUp.ToString();
				Console.Write($"    Look Up ({lookUp}, \"{lookUpToString}\", {lookUp}m): ");
				omnitree.Stepper(Console.Write,
					lookUp, lookUp,
					lookUpToString, lookUpToString,
					lookUp, lookUp);
				Console.WriteLine();

				// Ignoring dimensions on traversals example.
				// If you want to ignore a column on a traversal, you can do so like this:
				omnitree.Stepper(i => { /*Do Nothing*/ },
					lookUp, lookUp,
					// The "None" means there is no bound, so all values are valid
					None, None,
					None, None);

				Console.Write("    Counting Items In a Space [" +
					$"({minimumXZ}, \"{minimumY}\", {minimumXZ}m)->" +
					$"({maximumXZ}, \"{maximumY}\", {maximumXZ}m)]: " +
					omnitree.CountSubSpace(
						minimumXZ, maximumXZ,
						minimumY, maximumY,
						minimumXZ, maximumXZ));
				Console.WriteLine();

				int removalMinimum = random.Next(1, test / 2);
				int removalMaximum = random.Next(1, test / 2) + test / 2;
				string removalMinimumY = removalMinimum.ToString();
				string removalMaximumY = removalMaximum.ToString();
				Console.Write($"    Remove ({removalMinimum}-{removalMaximum}): ");
				omnitree.Remove(
					removalMinimum, removalMaximum,
					removalMinimumY, removalMaximumY,
					removalMinimum, removalMaximum);
				omnitree.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine($"    Dimensions: {omnitree.Dimensions}");
				Console.WriteLine($"    Count: {omnitree.Count}");

				omnitree.Clear();

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

				IOmnitreeBounds<int, double, string, decimal> omnitree =
					new OmnitreeBoundsLinked<int, double, string, decimal>(
					// This is a location delegate. (how to locate the item along each dimension)
					(int index,
					 out double min1, out double max1,
					 out string min2, out string max2,
					 out decimal min3, out decimal max3) =>
					{
						string indexToString = index.ToString();

						min1 = index; max1 = index;
						min2 = indexToString; max2 = indexToString;
						min3 = index; max3 = index;
					});

				Console.Write($"    Adding (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					omnitree.Add(i);
				}
				Console.WriteLine();

				Console.Write("    Traversal: ");
				omnitree.Stepper(Console.Write);
				Console.WriteLine();

				//// The "foreach" enumeration works for omnitrees, but it is not optimized
				//// and you should prefer the stepper function (it is faster).
				//
				//Console.Write("    Traversal (Foreach): ");
				//foreach (var i in omnitree)
				//{
				//	Console.Write(i);
				//}
				//Console.WriteLine();

				int minimumXZ = random.Next(1, test / 2);
				int maximumXZ = random.Next(1, test / 2) + test / 2;
				string minimumY = minimumXZ.ToString();
				string maximumY = maximumXZ.ToString();
				Console.Write("    Spacial Traversal [" +
					$"({minimumXZ}, \"{minimumY}\", {minimumXZ}m)->" +
					$"({maximumXZ}, \"{maximumY}\", {maximumXZ}m)]: ");
				omnitree.StepperOverlapped(Console.Write,
					minimumXZ, maximumXZ,
					minimumY, maximumY,
					minimumXZ, maximumXZ);
				Console.WriteLine();

				// Note: this "look up" is just a very narrow spacial query that (since we know the data)
				// wil only give us one result.
				int lookUpXZ = random.Next(0, test);
				string lookUpY = lookUpXZ.ToString();
				Console.Write($"    Look Up ({lookUpXZ}, \"{lookUpY}\", {lookUpXZ}m): ");
				omnitree.StepperOverlapped(Console.Write,
					lookUpXZ, lookUpXZ,
					lookUpY, lookUpY,
					lookUpXZ, lookUpXZ);
				Console.WriteLine();

				// Ignoring dimensions on traversals example.
				// If you want to ignore a dimension on a traversal, you can do so like this:
				omnitree.StepperOverlapped(i => { /*Do Nothing*/ },
					lookUpXZ, lookUpXZ,
					// The "None" means there is no bound, so all values are valid
					None, None,
					None, None);

				Console.Write("    Counting Items In a Space [" +
					$"({minimumXZ}, \"{minimumY}\", {minimumXZ}m)->" +
					$"({maximumXZ}, \"{maximumY}\", {maximumXZ}m)]: " +
					omnitree.CountSubSpaceOverlapped(
						minimumXZ, maximumXZ,
						minimumY, maximumY,
						minimumXZ, maximumXZ));
				Console.WriteLine();

				int removalMinimumXZ = random.Next(1, test / 2);
				int removalMaximumXZ = random.Next(1, test / 2) + test / 2;
				string removalMinimumY = removalMinimumXZ.ToString();
				string removalMaximumY = removalMaximumXZ.ToString();
				Console.Write($"    Remove ({removalMinimumXZ}-{removalMaximumXZ}): ");
				omnitree.RemoveOverlapped(
					removalMinimumXZ, removalMaximumXZ,
					removalMinimumY, removalMaximumY,
					removalMinimumXZ, removalMaximumXZ);
				omnitree.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine($"    Dimensions: {omnitree.Dimensions}");
				Console.WriteLine($"    Count: {omnitree.Count}");

				omnitree.Clear();

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

				IGraph<int> graphSetOmnitree = GraphSetOmnitree.New<int>();

				Console.WriteLine($"    Adding Nodes (0-{test - 1})...");
				for (int i = 0; i < test; i++)
				{
					graphSetOmnitree.Add(i);
				}

				int edgesPerNode = 3;
				Console.WriteLine("    Adding Random Edges (0-3 per node)...");
				for (int i = 0; i < test; i++)
				{
					// lets use a heap to randomize the edges using random priorities
					IHeap<(int, int)> heap = HeapArray.New<(int, int)>();
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
				graphSetOmnitree.Stepper(Console.Write);
				Console.WriteLine();

				Console.WriteLine("    Edges (Traversal): ");
				graphSetOmnitree.Edges(edge => Console.WriteLine($"      {edge.Item1}->{edge.Item2}"));
				Console.WriteLine();

				int a = random.Next(0, test);
				Console.Write($"    Neighbors ({a}):");
				graphSetOmnitree.Neighbors(a, i => Console.Write($" {i}"));
				Console.WriteLine();

				int b = random.Next(0, test / 2);
				int c = random.Next(test / 2, test);
				Console.WriteLine($"    Are Adjacent ({b}, {c}): {graphSetOmnitree.Adjacent(b, c)}");
				Console.WriteLine($"    Node Count: {graphSetOmnitree.NodeCount}");
				Console.WriteLine($"    Edge Count: {graphSetOmnitree.EdgeCount}");

				graphSetOmnitree.Clear();

				Console.WriteLine();
			}
			#endregion

			#region Trie
			{
				Console.WriteLine("  Trie------------------------------------------------");
				Console.WriteLine();
				Console.WriteLine("    A Trie is a tree that stores values so that partial keys may be shared");
				Console.WriteLine("    between values to reduce memory redundancies (take less space). For");
				Console.WriteLine("    example, \"fart\" and \"farm\" both have the letters \"far\" in common,");
				Console.WriteLine("    and a trie accounts for this and would not store duplicate letters for");
				Console.WriteLine("    those words ['f'->'a'->'r'->('t'||'m')]. Tries are generally used on");
				Console.WriteLine("    large data sets like storing all the words in the English language.");
				Console.WriteLine();

				string[] strings = new string[]
				{
					"zero",
					"one",
					"two",
					"three",
					"four",
					"five",
					"six",
					"seven",
					"eight",
					"nine",
				};

				ITrie<char, int> trie = TrieLinkedHashLinked.New<char, int>();

				Console.WriteLine("    Adding...");
				for (int i = 0; i < strings.Length; i++)
				{
					trie.Add(i, strings[i].ToStepper());
					Console.WriteLine("      " + strings[i]);
				}

				Console.WriteLine("    Traversal:");
				trie.Stepper((stepper, value) => Console.WriteLine($"      {stepper.ConcatToString()}: {value}"));

				//// The "foreach" enumeration works for tries, but it is not optimized
				//// and you should prefer the stepper function (it is faster).
				//
				//Console.WriteLine("    Traversal (Foreach): ");
				//foreach (var i in trie)
				//{
				//	Console.WriteLine("      " + i.ConcatToString());
				//}

				Console.WriteLine($"    Count: {trie.Count}");

				Console.WriteLine($"    Get(\"three\"): {trie.Get("three".ToStepper())}");

				Console.WriteLine($"    Contains(\"six\"): {trie.Contains("six".ToStepper())}");

				Console.WriteLine("    Remove(\"six\")...");
				trie.Remove("six".ToStepper());

				Console.WriteLine($"    Contains(\"six\"): {trie.Contains("six".ToStepper())}");

				Console.WriteLine($"    Count: {trie.Count}");

				Console.WriteLine();
			}
			#endregion

			Console.WriteLine("============================================");
			Console.WriteLine("Examples Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}
	}
}
