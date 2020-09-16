using System;
using System.Linq;
using Towel;
using Towel.DataStructures;
using Towel.Mathematics;
using static Towel.Statics;

namespace GraphSearch
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("You are runnning the GraphSearch example.");
			Console.WriteLine("======================================================");
			Console.WriteLine();

			#region Simple Example
			{
				Console.WriteLine("  Graph Searching----------------------");
				Console.WriteLine();

				// visualization
				//
				//    [0]-----(1)---->[1]
				//     |               |
				//     |               |
				//    (99)            (2)
				//     |               |
				//     |               |
				//     v               v
				//    [3]<----(5)-----[2]
				//
				//    [nodes in brackets]
				//    (edge costs in parenthases)

				// make a graph
				IGraph<int> graph = new GraphSetOmnitree<int>()
				{
					// add nodes
					0, 1, 2, 3,
					// add edges
					{ 0, 1 },
					{ 1, 2 },
					{ 2, 3 },
					{ 0, 3 },
				};

				// make a heuristic function
				static int Heuristic(int node) => node switch
				{
					0 => 2,
					1 => 3,
					2 => 2,
					3 => 0,
					_ => throw new NotImplementedException(),
				};

				// make a cost function
				static int Cost(int from, int to) => (from, to) switch
				{
					(0, 1) => 1,
					(1, 2) => 2,
					(2, 3) => 5,
					(0, 3) => 99,
					_ => throw new NotImplementedException(),
				};

				// make a goal function
				static bool Goal(int node) => node == 3;

				// run the A* algorithm
				Action<Action<int>> graphAStarPath = SearchGraph(0, graph, Heuristic, Cost, Goal, out int graphAStarTotalCost);
				// run the Dijkstra algorithm
				Action<Action<int>> graphDijkstraPath = SearchGraph(0, graph, Heuristic, Goal);

				// print the paths to the console
				static void PrintPathToConsole(Action<Action<int>> path)
				{
					if (path != null)
					{
						path(node => Console.Write(node + " "));
					}
					else
					{
						Console.Write("none");
					}
				}
				Console.WriteLine("    Using Graph Data Structure...");
				Console.Write("    A* Path:       ");
				PrintPathToConsole(graphAStarPath);
				Console.WriteLine();
				Console.Write("    Dijkstra Path: ");
				PrintPathToConsole(graphDijkstraPath);
				Console.WriteLine();

				/// You don't have to use the graph data structure. Instead, you can use
				/// a function to get the neighbors of a node.
				static void Neighbors(int node, Action<int> step)
				{
					switch (node)
					{
						case 0: step(1); step(3); break;
						case 1: step(2); break;
						case 2: step(3); break;
					}
				}

				// run the A* algorithm
				Action<Action<int>> functionAStarPath = SearchGraph(0, Neighbors, Heuristic, Cost, Goal, out int functionAStarTotalCost);
				// run the Dijkstra algorithm
				Action<Action<int>> functionDdijkstraPath = SearchGraph(0, Neighbors, Heuristic, Goal);

				Console.WriteLine("    Using Neighbors Function...");
				Console.Write("    A* Path:       ");
				PrintPathToConsole(graphAStarPath);
				Console.WriteLine();
				Console.Write("    Dijkstra Path: ");
				PrintPathToConsole(graphDijkstraPath);
				Console.WriteLine();

				Console.WriteLine();
			}
			#endregion

			#region Vector Game-Style Example
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
					// if the location is inside the rock, it is not a valid movement, or if
					// the location has already been used, we can consider it invalid

					location.Subtract(rockLocation, ref validationVectorStorage);
					float magnitude = validationVectorStorage.Magnitude;
					return !(magnitude <= rockRadius || alreadyUsed.Contains(location));
				}

				// Now we need the neighbor function (getting the neighbors of the current location).
				void neighborFunction(Vector<float> currentLocation, Action<Vector<float>> neighbors)
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

					void HandleNeighbor(Vector<float> neighbor)
					{
						if (validateMovementLocation(neighbor))
						{
							alreadyUsed.Add(neighbor); // mark location as used
							neighbors(neighbor);
						}
					}

					HandleNeighbor(new Vector<float>(x + distanceResolution, y, z)); // north
					HandleNeighbor(new Vector<float>(x, y, z + distanceResolution)); // east
					HandleNeighbor(new Vector<float>(x - distanceResolution, y, z)); // south
					HandleNeighbor(new Vector<float>(x, y, z - distanceResolution)); // west
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
					// If the location we are moving to is in the mud, it makes units
					// move slower, so it has a higher cost. If not, it is a standard
					// movement speed.
					to.Subtract(mudLocation, ref costVectorStorage);
					float magnitude = costVectorStorage.Magnitude;
					return magnitude <= mudRadius ? 2f : 1f;
				}

				Vector<float> goalVectorStorage = null; // storage to prevent a ton of vectors from being allocated

				// Goal function
				bool goalFunction(Vector<float> currentLocation)
				{
					// if the player is within the enemy's attack range WE FOUND A PATH! :)
					currentLocation.Subtract(playerLocation, ref goalVectorStorage);
					float magnitude = goalVectorStorage.Magnitude;
					return magnitude <= enemyAttackRange;
				}

				// We have all the necessary parameters. Run the pathfinding algorithms!
				Action<Action<Vector<float>>> aStarPath =
					SearchGraph(
						enemyLocation,
						neighborFunction,
						heuristicFunction,
						costFunction,
						goalFunction,
						out float aStarTotalPathCost);

				// Flush the already used markers before running the DijkstraPath algorithm.
				// Normally you won't run two algorithms for the same graph/location, but 
				// we are running both algorithms in this example to demonstrate the
				// differences between them.

				alreadyUsed.Clear();

				Action<Action<Vector<float>>> dijkstraPath =
					SearchGraph(
						enemyLocation,
						neighborFunction,
						heuristicFunction,
						goalFunction);

				// Note: the breadth-first-search algorithm is slow as balls. Lets try to run it
				// but if it takes too long (say... over 2 seconds) we will cancel it.

				alreadyUsed.Clear();

				DateTime startTime = DateTime.Now;
				TimeSpan timeSpan = TimeSpan.FromSeconds(2);
				Action<Action<Vector<float>>> breadthFirstSearch =
					SearchGraph(
						enemyLocation,
						neighborFunction,
						node =>
						{
							if (DateTime.Now - startTime > timeSpan)
								return Break;
							else if (goalFunction(node))
								return Goal;
							else
								return Continue;
						});

				// NOTE: If there is no valid path, then "Search.Graph" will return "null."
				// For this example, I know that there will be a valid path so I did not 
				// include a null check.

				// Lets convert the paths into arrays so you can look at them in the debugger. :)
				Vector<float>[] aStarPathArray = aStarPath.ToArray();
				Vector<float>[] dijkstraPathArray = dijkstraPath.ToArray();

				// lets calculate the movement cost of each path to see how they compare
				float dijkstraTotalCost = Addition<float>(step =>
				{
					for (int i = 0; i < dijkstraPathArray.Length - 1; i++)
					{
						step(costFunction(dijkstraPathArray[i], dijkstraPathArray[i + 1]));
					}
				});

				bool IsAStarPathBetterThanijkstra = aStarTotalPathCost < dijkstraTotalCost;

				// Notice that that the A* algorithm produces a less costly path than the DijkstraPath, 
				// meaning that it is faster. The DijkstraPath path went through the mud, but the A* path
				// took the longer route around the other side of the rock, which ended up being faster
				// than running through the mud.
			}
			#endregion

			Console.WriteLine();
			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}
	}
}
