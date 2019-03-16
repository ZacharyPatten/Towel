using System;
using Towel.DataStructures;
using Towel.Mathematics;
using Towel.Algorithms;
using Towel;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You are runnning the Algorithms tutorial.");
            Console.WriteLine("======================================================");
            Console.WriteLine();

            #region Sorting
            {

                Console.WriteLine(" Sorting Algorithms----------------------");
                Console.WriteLine();
                int[] dataSet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Console.Write("  Data Set:");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                // if you want to sort non-array types, see the overloads using Get<int> and Assign<int>
                // Delegates
                //Get<int> get = (int index) => { return dataSet[index]; };
                //Assign<int> assign = (int index, int value) => { dataSet[index] = value; };

                // Shuffling (Randomizing)
                Sort<int>.Shuffle(dataSet);
                Console.Write("  Shuffle (Randomizing): ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                // Bubble
                Sort<int>.Bubble(dataSet);
                Console.Write("  Bubble: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                Console.WriteLine("  shuffling dataSet...");
                Sort<int>.Shuffle(dataSet);

                // Selection
                Sort<int>.Selection(dataSet);
                Console.Write("  Selection: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                Console.WriteLine("  shuffling dataSet...");
                Sort<int>.Shuffle(dataSet);

                // Insertion
                Sort<int>.Insertion(dataSet);
                Console.Write("  Insertion: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                Console.WriteLine("  shuffling dataSet...");
                Sort<int>.Shuffle(dataSet);

                // Quick
                Sort<int>.Quick(dataSet);
                Console.Write("  Quick: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                Console.WriteLine("  shuffling dataSet...");
                Sort<int>.Shuffle(dataSet);

                // Merge
                Sort<int>.Merge(Compute.Compare, dataSet);
                Console.Write("  Merge: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                Console.WriteLine("  shuffling dataSet...");
                Sort<int>.Shuffle(dataSet);

                // Heap
                Sort<int>.Heap(Compute.Compare, dataSet);
                Console.Write("  Heap: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                Console.WriteLine("  shuffling dataSet...");
                Sort<int>.Shuffle(dataSet);

                // OddEven
                Sort<int>.OddEven(Compute.Compare, dataSet);
                Console.Write("  OddEven: ");
                Console.Write(dataSet[0]);
                for (int i = 1; i < dataSet.Length; i++)
                    Console.Write(", " + dataSet[i]);
                Console.WriteLine();

                //Sort<int>.Shuffle(get, set, 0, dataSet.Length);

                //// Slow
                //Sort<int>.Slow(Logic.compare, get, set, 0, dataSet.Length);
                //Console.Write("Slow: ");
                //Console.Write(dataSet[0]);
                //for (int i = 1; i < dataSet.Length; i++)
                //	Console.Write(", " + dataSet[i]);
                //Console.WriteLine();

                Sort<int>.Shuffle(dataSet);

                // Bogo
                //Sort<int>.Bogo(Logic.compare, get, set, 0, dataSet.Length);
                Console.Write("  Bogo: Disabled (takes forever)");
                //Console.Write(dataSet[0]);
                //for (int i = 1; i < dataSet.Length; i++)
                //	Console.Write(", " + dataSet[i]);
                //Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine();

            }
            #endregion

            #region Graph Search
            {

                Console.WriteLine(" Graph Searching----------------------");
                Console.WriteLine();

                // make a graph
                Graph<int> graph = new GraphSetOmnitree<int>(
                    Compare.Default,
                    Hash.Default);

                // add nodes
                graph.Add(0);
                graph.Add(1);
                graph.Add(2);
                graph.Add(3);

                // add edges
                graph.Add(0, 1);
                graph.Add(0, 2);
                graph.Add(1, 3);
                graph.Add(2, 3);

                //// represent a graph
                //// Note: can be any type  (doesn't have to be int?[,])
                //int?[,] adjacencyMatrix = 
                //{
                //	{ null, 1, 2, null },
                //	{ null, null, null, 5 },
                //	{ null, null, null, 1 },
                //	{ null, null, null, null }
                //};

                // make a delegate for finding neighbor nodes
                Action<int, Step<int>> neighbors =
                    (int current, Step<int> step_function) =>
                    {
                        //for (int i = 0; i < 4; i++)
                        //	if (adjacencyMatrix[current, i] != null)
                        //		step(i);
                        graph.Neighbors(current, step_function);
                    };

                // make a delegate for computing heuristics
                Func<int, int> heuristic =
                    (int node) =>
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
                    };

                // make a delegate for computing costs
                Func<int, int, int> cost =
                    (int from, int to) =>
                    {
                        if (from == 0 && to == 1)
                            return 1;
                        if (from == 0 && to == 2)
                            return 2;
                        if (from == 1 && to == 3)
                            return 5;
                        if (from == 2 && to == 3)
                            return 1;
                        if (from == 0 && to == 3)
                            return 99;
                        throw new Exception("invalid path cost computation");
                    };

                // make a delegate for determining if the goal is reached
                Func<int, bool> goal =
                    (int node) =>
                    {
                        if (node == 3)
                            return true;
                        else
                            return false;
                    };

                // run A* the algorithm
                Stepper<int> aStar_path = Search<int>.Graph<int>.Astar(
                    0,
                    graph,
                    new Search<int>.Graph<int>.Heuristic(heuristic),
                    new Search<int>.Graph<int>.Cost(cost),
                    new Search<int>.Graph<int>.Goal(goal));

                // run the Greedy algorithm
                Stepper<int> greedy_path = Search<int>.Graph<int>.Greedy(
                    0,
                    graph,
                    new Search<int>.Graph<int>.Heuristic(heuristic),
                    new Search<int>.Graph<int>.Goal(goal));

                Console.Write("  A* Path: ");
                if (aStar_path != null)
                    aStar_path((int i) => { System.Console.Write(i + " "); });
                else
                    Console.Write("  none");

                Console.WriteLine();

                Console.Write("  Greedy Path: ");
                if (greedy_path != null)
                    greedy_path((int i) => { System.Console.Write(i + " "); });
                else
                    Console.Write("  none");
                Console.WriteLine();
                Console.WriteLine();

            }
            #endregion

            #region Graph Search (Vector Game-Style Example)

            // Lets say you are coding enemy AI and you want the AI to find a path towards the player
            // in order to attack them. Here are their starting positions:
            Vector<float> enemy_location = new Vector<float>(-100, 0, -50);
            Vector<float> player_location = new Vector<float>(200, 0, -50);
            float enemy_attack_range = 3; // enemy has a melee attack with 2 range

            // Lets say most of the terrain is open, but there is a big rock in between them that they
            // must go around.
            Vector<float> rock_location = new Vector<float>(15, 0, -40);
            float rock_radius = 20;

            // So, we just need to validate movement locations (make sure the path finding algorithm
            // ignores locations inside the rock)
            Func<Vector<float>, bool> validateMovementLocation = location =>
            {
                float mag = (location - rock_location).Magnitude;
                if (mag <= rock_radius)
                    return false; // inside rock (not valid)
                return true; // not inside rock (valid)

                // NOTE:
                // This function will normally be handled by your physics engine if you are running one.
            };

            // Make sure we don't re-use locations (must be wiped after running the algorithm)
            Set<Vector<float>> already_used = new SetHashList<Vector<float>>();

            // Now we need the neighbor function (getting the neighbors of the current location).
            Search<Vector<float>>.Graph<float>.Neighbors neighborFunction = (currentLocation, neighbors) =>
            {
                // lets make a simple neighbor function that returns 4 locations (directly north, south, east, and west)
                // and the distance of each node in the graph will be 1
                Vector<float>
                    north = new Vector<float>(currentLocation.X + 1, currentLocation.Y, currentLocation.Z),
                    east = new Vector<float>(currentLocation.X, currentLocation.Y, currentLocation.Z + 1),
                    south = new Vector<float>(currentLocation.X - 1, currentLocation.Y, currentLocation.Z),
                    west = new Vector<float>(currentLocation.X, currentLocation.Y, currentLocation.Z - 1);

                // validate the locations (not inside the rock) and make sure we have not already traversed the location
                if (validateMovementLocation(north) && !already_used.Contains(north))
                {
                    already_used.Add(north); // mark for usage so we do not use this location again
                    neighbors(north);
                }
                if (validateMovementLocation(east) && !already_used.Contains(east))
                {
                    already_used.Add(east); // mark for usage so we do not use this location again
                    neighbors(east);
                }
                if (validateMovementLocation(south) && !already_used.Contains(south))
                {
                    already_used.Add(south); // mark for usage so we do not use this location again
                    neighbors(south);
                }
                if (validateMovementLocation(west) && !already_used.Contains(west))
                {
                    already_used.Add(west); // mark for usage so we do not use this location again
                    neighbors(west);
                }

                // NOTES:
                // - This neighbor function has a 90 degree per-node resolution (360 / 4 [north/south/east/west] = 90).
                // - This neighbor function has a 1 unit per-node resolution, because we went 1 unit in each direction.

                // RECOMMENDATIONS:
                // - If the path finding is failing, you may need to increase the resolution.
                // - If the algorithm is running too slow, you may need to reduce the resolution.
            };

            // Now we need the heuristic function (how close are we to the goal).
            Search<Vector<float>>.Graph<float>.Heuristic heuristicFunction = currentLocation =>
            {
                // The goal is the player's location, so we just need our distance from the player.
                return (currentLocation - player_location).Magnitude;
            };

            // Lets say there is a lot of mud around the rock, and the mud makes our player move at half their normal speed.
            // Our path finding needs to find the fastest route to the player, whether it be through the mud or not.
            Vector<float> mud_location = new Vector<float>(15, 0, -70);
            float mud_radius = 30;

            // Now we need the cost function
            Search<Vector<float>>.Graph<float>.Cost costFunction = (location1, location2) =>
            {
                // If either locations are in the mud, lets increase the cost of moving to that spot.
                float mag1 = (location1 - mud_location).Magnitude;
                if (mag1 <= mud_radius)
                    return 2;
                float mag2 = (location2 - mud_location).Magnitude;
                if (mag2 <= mud_radius)
                    return 2;

                // neither location is in the mud, it is just a standard movement at normal speed.
                return 1;
            };

            // Now we need a goal function
            Search<Vector<float>>.Graph<float>.Goal goalFunction = currentLocation =>
            {
                float mag = (currentLocation - player_location).Magnitude;
                // if the player is within the enemy's attack range WE FOUND A PATH! :)
                if (mag <= enemy_attack_range)
                    return true;

                // the enemy is not yet within attack range
                return false;
            };

            // We have all the necessary parameters. Run the pathfinding algorithms!
            Stepper<Vector<float>> aStarPath = Search<Vector<float>>.Graph<float>.Astar(
                enemy_location,
                neighborFunction,
                heuristicFunction,
                costFunction,
                goalFunction);

            // NOTE:
            // if the "Astar" function returns "null" there is no valid path. (in this example there
            // are valid paths, so I didn't add a nul check)

            // Here is the path converted to an array (easier to read while debugging)
            Vector<float>[] aStarPath_array = aStarPath.ToArray();

            // flush the duplicate locations checker before running the Greedy algorithm
            already_used.Clear();

            Stepper<Vector<float>> greedyPath = Search<Vector<float>>.Graph<float>.Greedy(
                enemy_location,
                neighborFunction,
                heuristicFunction,
                goalFunction);

            // Here is the path converted to an array (easier to read while debugging)
            Vector<float>[] greedyPath_array = greedyPath.ToArray();


            // lets calculate the movement cost of each path

            float total_cost_astar = Compute.Add<float>(step =>
            {
                for (int i = 0; i < aStarPath_array.Length - 1; i++)
                {
                    float cost = costFunction(aStarPath_array[i], aStarPath_array[i + 1]);
                    step(cost);
                }
            });

            float total_cost_greedy = Compute.Add<float>(step =>
            {
                for (int i = 0; i < greedyPath_array.Length - 1; i++)
                {
                    float cost = costFunction(greedyPath_array[i], greedyPath_array[i + 1]);
                    step(cost);
                }
            });

            // Notice that that the A* algorithm produces a less costly path than the Greedy, 
            // meaning that it is faster. The Greedy path went through the mud, but the A* path
            // took the longer route around the other side of the rock, which ended up being faster
            // than running through the mud.

            #endregion

            #region Random Generation

            Console.WriteLine(" Random Generation---------------------");
            Console.WriteLine();

            int iterationsperrandom = 3;
            Action<Random> testrandom = (Random random) =>
            {
                for (int i = 0; i < iterationsperrandom; i++)
                    Console.WriteLine("    " + i + ": " + random.Next());
                Console.WriteLine();
            };
            Arbitrary mcg_2pow59_13pow13 = new Arbitrary.Algorithms.MultiplicativeCongruent_A();
            Console.WriteLine("  mcg_2pow59_13pow13 randoms:");
            testrandom(mcg_2pow59_13pow13);
            Arbitrary mcg_2pow31m1_1132489760 = new Arbitrary.Algorithms.MultiplicativeCongruent_B();
            Console.WriteLine("  mcg_2pow31m1_1132489760 randoms:");
            testrandom(mcg_2pow31m1_1132489760);
            Arbitrary mersenneTwister = new Arbitrary.Algorithms.MersenneTwister();
            Console.WriteLine("  mersenneTwister randoms:");
            testrandom(mersenneTwister);
            Arbitrary cmr32_c2_o3 = new Arbitrary.Algorithms.CombinedMultipleRecursive();
            Console.WriteLine("  mersenneTwister randoms:");
            testrandom(cmr32_c2_o3);
            Arbitrary wh1982cmcg = new Arbitrary.Algorithms.WichmannHills1982();
            Console.WriteLine("  mersenneTwister randoms:");
            testrandom(wh1982cmcg);
            Arbitrary wh2006cmcg = new Arbitrary.Algorithms.WichmannHills2006();
            Console.WriteLine("  mersenneTwister randoms:");
            testrandom(wh2006cmcg);
            Arbitrary mwcxorsg = new Arbitrary.Algorithms.MultiplyWithCarryXorshift();
            Console.WriteLine("  mwcxorsg randoms:");
            testrandom(mwcxorsg);

            #endregion

            Console.WriteLine();
            Console.WriteLine("============================================");
            Console.WriteLine("Example Complete...");
            Console.ReadLine();
        }
    }
}
