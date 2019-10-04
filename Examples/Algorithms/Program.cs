using System;
using System.Linq;
using Towel;
using Towel.Algorithms;
using Towel.DataStructures;
using Towel.Mathematics;
using static Towel.Syntax;

namespace Algorithms
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("You are runnning the Algorithms example.");
			Console.WriteLine("======================================================");
			Console.WriteLine();

			#region Sorting
			{
				// Note: these functions are not restricted to array types. You can use the
				// overloads with "Get" and "Assign" delegates to use them on any int-indexed
				// data structure.

				Console.WriteLine("  Sorting Algorithms----------------------");
				Console.WriteLine();
				int[] dataSet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
				Console.Write("    Data Set:" + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				// Shuffling (Randomizing)
				Sort.Shuffle(dataSet);
				Console.Write("    Shuffle (Randomizing): " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				// Bubble
				Sort.Bubble(dataSet);
				Console.Write("    Bubble:    " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// Selection
				Sort.Selection(dataSet);
				Console.Write("    Selection: " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// Insertion
				Sort.Insertion(dataSet);
				Console.Write("    Insertion: " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// Quick
				Sort.Quick(dataSet);
				Console.Write("    Quick:     " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// Merge
				Sort.Merge(dataSet);
				Console.Write("    Merge:     " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// Heap
				Sort.Heap(dataSet);
				Console.Write("    Heap:      " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// OddEven
				Sort.OddEven(dataSet);
				Console.Write("    OddEven:   " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				//Console.WriteLine("  shuffling dataSet...");
				//Sort.Shuffle(dataSet);

				//// Slow
				//Sort<int>.Slow(Logic.compare, get, set, 0, dataSet.Length);
				//Console.Write("Slow: " + string.Join(", ", dataSet.Select(x => x.ToString())));
				//Console.WriteLine();

				Console.WriteLine("    shuffling dataSet...");
				Sort.Shuffle(dataSet);

				// Bogo
				Console.Write("    Bogo:      Disabled (takes forever)");
				//Sort.Bogo(dataSet);
				//Console.Write("    Bogo: " + string.Join(", ", dataSet.Select(x => x.ToString())));
				Console.WriteLine();

				Console.WriteLine();
				Console.WriteLine();
			}
			#endregion

			#region Graph Search (Using Graph Data Structure)
			{
				Console.WriteLine("  Graph Searching----------------------");
				Console.WriteLine();

				// make a graph
				IGraph<int> graph = new GraphSetOmnitree<int>()
				{
                    // add nodes
                    0,
					1,
					2,
					3,
                    // add edges
                    { 0, 1 },
					{ 1, 2 },
					{ 2, 3 },
					{ 0, 3 },

					// visualization
					//
					//     0 --------> 1
					//     |           |
					//     |           |
					//     |           |
					//     v           v
					//     3 <-------- 2
				};

				// make a heuristic function
				int heuristic(int node)
				{
					switch (node)
					{
						case 0:
							return 3;
						case 1:
							return 6;
						case 2:
							return 1;
						case 3:
							return 0;
						default:
							throw new NotImplementedException();
					}
				}

				// make a cost function
				int cost(int from, int to)
				{
					if (from == 0 && to == 1)
						return 1;
					if (from == 1 && to == 2)
						return 2;
					if (from == 2 && to == 3)
						return 5;
					if (from == 0 && to == 3)
						return 99;
					throw new Exception("invalid path cost computation");
				}

				// make a goal function
				bool goal(int node)
				{
					if (node == 3)
						return true;
					else
						return false;
				}

				// run A* the algorithm
				Stepper<int> aStar_path = Search.Graph(0, graph, heuristic, cost, goal);
				Console.Write("    A* Path:     ");
				if (aStar_path != null)
				{
					aStar_path(i => Console.Write(i + " "));
				}
				else
				{
					Console.Write("none");
				}
				Console.WriteLine();

				// run the Greedy algorithm
				Stepper<int> greedy_path = Search.Graph(0, graph, heuristic, goal);
				Console.Write("    Greedy Path: ");
				if (greedy_path != null)
				{
					greedy_path(i => Console.Write(i + " "));
				}
				else
				{
					Console.Write("none");
				}
				Console.WriteLine();

				Console.WriteLine();
			}
			#endregion

			#region Graph Search (Vector Game-Style Example)
			{
				Console.WriteLine("  Graph Searching (Vector Game-Style Example)-------------------");
				Console.WriteLine();
				Console.WriteLine("    Debug the code. The path is to large to write to the console.");
				Console.WriteLine();

				// Visualization:
				//
				//                        BOULDER
				//                          ____
				//        ENEMY           /      \          PLAYER
				//          X            |        |           X
				//                        \______/
				//                      /##########\
				//                     |############|
				//                      \##########/
				//                          MUD

				// Lets say you are coding enemy AI and you want the AI to find a path towards the player
				// in order to attack them. Here are their starting positions:
				Vector<float> enemyLocation = new Vector<float>(-100f, 0f, -50f);
				Vector<float> playerLocation = new Vector<float>(200f, 0f, -50f);
				float enemyAttackRange = 3f; // enemy has a melee attack with 3 range

				// Lets say most of the terrain is open, but there is a big rock in between them that they
				// must go around.
				Vector<float> rockLocation = new Vector<float>(15f, 0f, -40f);
				float rockRadius = 20f;

				// Make sure we don't re-use locations (must be wiped after running the algorithm)
				ISet<Vector<float>> alreadyUsed = new SetHashLinked<Vector<float>>();

				Vector<float> validationVectorStorage = null; // storage to prevent a ton of vectors from being allocated

				// So, we just need to validate movement locations (make sure the path finding algorithm
				// ignores locations inside the rock)
				bool validateMovementLocation(Vector<float> location)
				{
					// if the location is inside the rock, it is not a valid movement
					location.Subtract(rockLocation, ref validationVectorStorage);
					float magnitude = validationVectorStorage.Magnitude;
					if (magnitude <= rockRadius)
					{
						return false;
					}

					// NOTE: If you are running a physics engine, you might be able to just call it to validate a location.

					// if the location was already used, then let's consider it invalid, because
					// another path (which is faster) has already reached that location
					if (alreadyUsed.Contains(location))
					{
						return false;
					}

					return true;
				}

				// Now we need the neighbor function (getting the neighbors of the current location).
				void neighborFunction(Vector<float> currentLocation, Step<Vector<float>> neighbors)
				{
					// NOTES:
					// - This neighbor function has a 90 degree per-node resolution (360 / 4 [north/south/east/west] = 90).
					// - This neighbor function has a 1 unit per-node distance resolution, because we went 1 unit in each direction.

					// RECOMMENDATIONS:
					// - If the path finding is failing, you may need to increase the resolution.
					// - If the algorithm is running too slow, you may need to reduce the resolution.

					float distanceResolution = 1;

					float x = currentLocation.X;
					float y = currentLocation.Y;
					float z = currentLocation.Z;

					// Note: I'm using the X-axis and Z-axis here, but which axis you need to use
					// depends on your environment. Your "north" could be along the Y-axis for example.

					Vector<float> north = new Vector<float>(x + distanceResolution, y, z);
					if (validateMovementLocation(north))
					{
						alreadyUsed.Add(north); // mark location as used
						neighbors(north);
					}

					Vector<float> east = new Vector<float>(x, y, z + distanceResolution);
					if (validateMovementLocation(east))
					{
						alreadyUsed.Add(east); // mark location as used
						neighbors(east);
					}

					Vector<float> south = new Vector<float>(x - distanceResolution, y, z);
					if (validateMovementLocation(south))
					{
						alreadyUsed.Add(south); // mark location as used
						neighbors(south);
					}

					Vector<float> west = new Vector<float>(x, y, z - distanceResolution);
					if (validateMovementLocation(west))
					{
						alreadyUsed.Add(west); // mark location as used
						neighbors(west);
					}
				}

				Vector<float> heuristicVectorStorage = null; // storage to prevent a ton of vectors from being allocated

				// Heuristic function (how close are we to the goal)
				float heuristicFunction(Vector<float> currentLocation)
				{
					// The goal is the player's location, so we just need our distance from the player.
					currentLocation.Subtract(playerLocation, ref heuristicVectorStorage);
					return heuristicVectorStorage.Magnitude;
				}

				// Lets say there is a lot of mud around the rock, and the mud makes our player move at half their normal speed.
				// Our path finding needs to find the fastest route to the player, whether it be through the mud or not.
				Vector<float> mudLocation = new Vector<float>(15f, 0f, -70f);
				float mudRadius = 30f;

				Vector<float> costVectorStorage = null; // storage to prevent a ton of vectors from being allocated

				// Cost function
				float costFunction(Vector<float> from, Vector<float> to)
				{
					// If the location we are moving to is in the mud, lets adjust the
					// cost because mud makes us move slower.
					to.Subtract(mudLocation, ref costVectorStorage);
					float magnitude = costVectorStorage.Magnitude;
					if (magnitude <= mudRadius)
					{
						return 2f;
					}

					// neither location is in the mud, it is just a standard movement at normal speed.
					return 1f;
				}

				Vector<float> goalVectorStorage = null; // storage to prevent a ton of vectors from being allocated

				// Goal function
				bool goalFunction(Vector<float> currentLocation)
				{
					// if the player is within the enemy's attack range WE FOUND A PATH! :)
					currentLocation.Subtract(playerLocation, ref goalVectorStorage);
					float magnitude = goalVectorStorage.Magnitude;
					if (magnitude <= enemyAttackRange)
					{
						return true;
					}

					// the enemy is not yet within attack range
					return false;
				}

				// We have all the necessary parameters. Run the pathfinding algorithms!
				Stepper<Vector<float>> aStarPath =
					Search.Graph(
						enemyLocation,
						neighborFunction,
						heuristicFunction,
						costFunction,
						goalFunction);

				// Flush the already used markers before running the Greedy algorithm.
				// Normally you won't run two algorithms for the same graph/location, but 
				// we are running both algorithms in this example to demonstrate the
				// differences between them.
				alreadyUsed.Clear();

				Stepper<Vector<float>> greedyPath =
					Search.Graph(
						enemyLocation,
						neighborFunction,
						heuristicFunction,
						goalFunction);

				//// Note: the dijkstra algorithm is slow as balls, but feel free to uncomment
				//// and run it if you want to.
				//
				//alreadyUsed.Clear();
				//
				//Stepper<Vector<float>> dijkstraPath =
				//	Search.Graph(
				//		enemyLocation,
				//		neighborFunction,
				//		goalFunction);

				// NOTE: If there is no valid path, then "Search.Graph" will return "null."
				// For this example, I know that there will be a valid path so I did not 
				// include a null check.

				// Lets convert the paths into arrays so you can look at them in the debugger. :)
				Vector<float>[] aStarPathArray = aStarPath.ToArray();
				Vector<float>[] greedyPathArray = greedyPath.ToArray();
				//Vector<float>[] dijkstraPathArray = dijkstraPath.ToArray();

				// lets calculate the movement cost of each path to see how they compare
				float astartTotalCost = Addition<float>(step =>
				{
					for (int i = 0; i < aStarPathArray.Length - 1; i++)
					{
						step(costFunction(aStarPathArray[i], aStarPathArray[i + 1]));
					}
				});
				float greedyTotalCost = Addition<float>(step =>
				{
					for (int i = 0; i < greedyPathArray.Length - 1; i++)
					{
						step(costFunction(greedyPathArray[i], greedyPathArray[i + 1]));
					}
				});
				//float dijkstraTotalCost = Compute.Add<float>(step =>
				//{
				//	for (int i = 0; i < dijkstraPathArray.Length - 1; i++)
				//	{
				//		step(costFunction(dijkstraPathArray[i], dijkstraPathArray[i + 1]));
				//	}
				//});

				// Notice that that the A* algorithm produces a less costly path than the Greedy, 
				// meaning that it is faster. The Greedy path went through the mud, but the A* path
				// took the longer route around the other side of the rock, which ended up being faster
				// than running through the mud.
			}
			#endregion

			Console.WriteLine();
			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.ReadLine();
		}
	}
}
