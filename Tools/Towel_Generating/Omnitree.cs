﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Towel;
using static Towel.Statics;

namespace Towel_Generating
{
	internal static class Omnitree
	{
		internal static void Generate(int dimensions = 7)
		{
			StringBuilder file = new StringBuilder();
			file.AppendLine($@"//------------------------------------------------------------------------------");
			file.AppendLine(@"// <auto-generated>");
			file.AppendLine($@"//	This code was generated from ""{sourcefilepath()}"".");
			file.AppendLine(@"// </auto-generated>");
			file.AppendLine($@"//------------------------------------------------------------------------------");
			file.AppendLine($@"");
			//file.AppendLine($@"#if false");
			file.AppendLine($@"using System;");
			if (dimensions >= 30)
			{
				file.AppendLine($@"using System.Numerics;");
			}
			file.AppendLine($@"using static Towel.Statics;");
			file.AppendLine($@"using static Towel.DataStructures.Omnitree;");
			file.AppendLine($@"");
			file.AppendLine($@"namespace Towel.DataStructures.TEMP");
			file.AppendLine($@"{{");
			file.AppendLine($@"	#region Notes");
			file.AppendLine($@"");
			file.AppendLine($@"	// Visualizations--------------------------------------------------");
			file.AppendLine($@"	//");
			file.AppendLine($@"	// 1 Dimensional:");
			file.AppendLine($@"	//");
			file.AppendLine($@"	//  -1D |-----------|-----------| +1D");
			file.AppendLine($@"	//");
			file.AppendLine($@"	//       <--- 0 ---> <--- 1 --->");
			file.AppendLine($@"	//");
			file.AppendLine($@"	// 2 Dimensional:");
			file.AppendLine($@"	//       _____________________");
			file.AppendLine($@"	//      |          |          |  +2D");
			file.AppendLine($@"	//      |          |          |   ^");
			file.AppendLine($@"	//      |     2    |     3    |   |");
			file.AppendLine($@"	//      |          |          |   |");
			file.AppendLine($@"	//      |----------|----------|   |");
			file.AppendLine($@"	//      |          |          |   |");
			file.AppendLine($@"	//      |          |          |   |");
			file.AppendLine($@"	//      |     0    |     1    |   |");
			file.AppendLine($@"	//      |          |          |   v");
			file.AppendLine($@"	//      |__________|__________|  -2D");
			file.AppendLine($@"	//");
			file.AppendLine($@"	//       -1D <-----------> +1D ");
			file.AppendLine($@"	//");
			file.AppendLine($@"	// 3 Dimensional:");
			file.AppendLine($@"	//");
			file.AppendLine($@"	//            +3D     _____________________");
			file.AppendLine($@"	//           7       /         /          /|");
			file.AppendLine($@"	//          /       /    6    /     7    / |");
			file.AppendLine($@"	//         /       /---------/----------/  |");
			file.AppendLine($@"	//        /       /    2    /     3    /|  |");
			file.AppendLine($@"	//       L       /_________/__________/ |  |");
			file.AppendLine($@"	//    -3D       |          |          | | /|          +2D");
			file.AppendLine($@"	//              |          |          | |/ |           ^");
			file.AppendLine($@"	//              |     2    |     3    | /  |           |");
			file.AppendLine($@"	//              |          |          |/|  | <-- 5     |");
			file.AppendLine($@"	//              |----------|----------| |  |           |");
			file.AppendLine($@"	//              |          |          | |  /           |");
			file.AppendLine($@"	//              |          |          | | /            |");
			file.AppendLine($@"	//              |     0    |     1    | |/             |");
			file.AppendLine($@"	//              |          |          | /              v");
			file.AppendLine($@"	//              |__________|__________|/              -2D");
			file.AppendLine($@"	//             ");
			file.AppendLine($@"	//                   ^");
			file.AppendLine($@"	//                   |");
			file.AppendLine($@"	//                   4 (behind 0)");
			file.AppendLine($@"	//");
			file.AppendLine($@"	//               -1D <-----------> +1D");
			file.AppendLine($@"");
			file.AppendLine($@"	#endregion");
			file.AppendLine($@"");
			file.AppendLine($@"	/// <summary>Contains the necessary type definitions for the various omnitree types.</summary>");
			file.AppendLine($@"	public static partial class Omnitree");
			file.AppendLine($@"	{{");
			for (int i = 1; i <= dimensions; i++)
			{
				file.AppendLine($@"		#region {i} Dimensional");
				file.AppendLine($@"");

				#region public struct Vector<Axis1, Axis2, Axis3...>

				file.AppendLine($@"		/// <summary>Represents a {i}D vector.</summary>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		/// <typeparam name=""A{j}"">The generic type of the {j} dimension.</typeparam>");
				}
				file.AppendLine($@"		public struct Vector<{Join(1..(i + 1), n => $"A{n}", ", ")}>");
				file.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			/// <summary>The value along axis {j}.</summary>");
					file.AppendLine($@"			public A{j} Axis{j};");
				}
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>Returns a vector with defaulted values.</summary>");
				file.AppendLine($@"			public static Vector<{Join(1..(i + 1), n => $"A{n}", ", ")}> Default =>");
				file.AppendLine($@"				new Vector<{Join(1..(i + 1), n => $"A{n}", ", ")}>(");
				file.AppendLine($@"					{Join(1..(i + 1), n => $"default", ", ")});");
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>A location along each axis.</summary>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			/// <param name=""axis{j}"">The location along axis {j}.</param>");
				}
				file.AppendLine($@"			public Vector({Join(1..(i + 1), n => $"A{n} axis{n}", ", ")})");
				file.AppendLine($@"			{{");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"				this.Axis{j} = axis{j};");
				}
				file.AppendLine($@"			}}");
				file.AppendLine($@"		}}");
				file.AppendLine($@"");

				#endregion

				#region public struct Bounds<Axis1, Axis2, Axis3...>

				file.AppendLine($@"		/// <summary>Represents a {i}D bounding box.</summary>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		/// <typeparam name=""A{j}"">The generic type of the {j} dimension.</typeparam>");
				}
				file.AppendLine($@"		public struct Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}>");
				file.AppendLine($@"		{{");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			/// <summary>The minimum value along the {j} dimension.</summary>");
					file.AppendLine($@"			public Bound<A{j}> Min{j};");
					file.AppendLine($@"			/// <summary>The maximum value along the {j} dimension.</summary>");
					file.AppendLine($@"			public Bound<A{j}> Max{j};");
				}
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>Extends infinitely along each axis.</summary>");
				file.AppendLine($@"			public static Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}> None =>");
				file.AppendLine($@"				new Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}>(");
				file.AppendLine($@"					{Join(1..(i + 1), n => $"Bound<A{n}>.None, Bound<A{n}>.None", ", ")});");
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>A set of values denoting a range (or lack of range) along each axis.</summary>");
				file.AppendLine($@"			public Bounds({Join(1..(i + 1), n => $"Bound<A{n}> min{n}, Bound<A{n}> max{n}", ", ")})");
				file.AppendLine($@"			{{");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"				this.Min{j} = min{j};");
					file.AppendLine($@"				this.Max{j} = max{j};");
				}
				file.AppendLine($@"			}}");
				file.AppendLine($@"		}}");
				file.AppendLine($@"");

				#endregion

				#region Helper Methods

				file.AppendLine($@"		/// <summary>Checks a node for inclusion (overlap) between two bounds.</summary>");
				file.AppendLine($@"		/// <returns>True if the spaces overlap; False if not.</returns>");
				file.AppendLine($@"		public static bool InclusionCheck<{Join(1..(i + 1), n => $"A{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}>(");
				file.AppendLine($@"			Omnitree.Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}> a, Omnitree.Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}> b, {Join(1..(i + 1), n => $"Compare{n} compare{n} = default", ", ")})");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			where Compare{j} : struct, IFunc<A{j}, A{j}, CompareResult>");
				}
				file.AppendLine($@"			=>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			a.Max{j}.Exists && b.Min{j}.Exists && compare{j}.Do(a.Max{j}.Value, b.Min{j}.Value) == CompareResult.Less    ? false :");
					file.AppendLine($@"			a.Min{j}.Exists && b.Max{j}.Exists && compare{j}.Do(a.Min{j}.Value, b.Max{j}.Value) == CompareResult.Greater ? false :");
				}
				file.AppendLine($@"			true;");
				file.AppendLine($@"");
				file.AppendLine($@"		public static bool EncapsulationCheck<{Join(1..(i + 1), n => $"A{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}>(");
				file.AppendLine($@"			Omnitree.Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}> a, Omnitree.Bounds<{Join(1..(i + 1), n => $"A{n}", ", ")}> b, {Join(1..(i + 1), n => $"Compare{n} compare{n} = default", ", ")})");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			where Compare{j} : struct, IFunc<A{j}, A{j}, CompareResult>");
				}
				file.AppendLine($@"			=>");
				file.AppendLine($@"			(");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			(a.Min{j}.Exists && !b.Min{j}.Exists) {(j == i ? "" : "||")}");
				}
				file.AppendLine($@"			)");
				file.AppendLine($@"			? false :");
				file.AppendLine($@"			(");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			(a.Max{j}.Exists && !b.Max{j}.Exists) {(j == i ? "" : "||")}");
				}
				file.AppendLine($@"			)");
				file.AppendLine($@"			? false :");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			b.Min{i}.Exists && a.Min{i}.Exists && compare{i}.Do(a.Min{i}.Value, b.Min{i}.Value) != CompareResult.Less ? false :");
					file.AppendLine($@"			b.Max{i}.Exists && a.Max{i}.Exists && compare{i}.Do(a.Max{i}.Value, b.Max{i}.Value) != CompareResult.Greater ? false :");
				}
				file.AppendLine($@"			true;");
				file.AppendLine($@"");
				file.AppendLine($@"		public static bool EqualsCheck<{Join(1..(i + 1), n => $"A{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}>(");
				file.AppendLine($@"			Omnitree.Vector<{Join(1..(i + 1), n => $"A{n}", ", ")}> a, Omnitree.Vector<{Join(1..(i + 1), n => $"A{n}", ", ")}> b, {Join(1..(i + 1), n => $"Compare{n} compare{n} = default", ", ")})");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			where Compare{j} : struct, IFunc<A{j}, A{j}, CompareResult>");
				}
				file.AppendLine($@"			=>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			compare{j}.Do(a.Axis{j}, b.Axis{j}) is Equal{(j == i ? "; " : " &&")}");
				}
				file.AppendLine($@"");
				file.AppendLine($@"		/// <summary>Checks if a bounds straddles a point if the point extended as a plane along each dimension.</summary>");
				file.AppendLine($@"		/// <param name=""a"">The bounds to determine if it straddles the extended point.</param>");
				file.AppendLine($@"		/// <param name=""b"">The point representing an extended plan along each axis.</param>");
				file.AppendLine($@"		/// <returns>True if the extended point was straddled or false if not.</returns>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		/// <typeparam name=""A{j}"">The generic type of the {j} dimension.</typeparam>");
					file.AppendLine($@"		/// <typeparam name=""Compare{j}"">The method for comparing elements along the {j} dimension.</typeparam>");
					file.AppendLine($@"		/// <param name=""compare{j}"">The method for comparing elements along the {j} dimension.</param>");
				}
				file.AppendLine($@"		public static bool StraddlesLines<{Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}>(");
				file.AppendLine($@"			Omnitree.Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> a, Omnitree.Vector<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> b, {Join(1..(i + 1), n => $"Compare{n} compare{n} = default", ", ")})");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			where Compare{j} : struct, IFunc<Axis{j}, Axis{j}, CompareResult>");
				}
				file.AppendLine($@"			=>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			(!a.Min{j}.Exists || (a.Min{j}.Exists && compare{j}.Do(a.Min{j}.Value, b.Axis{j}) != Greater)) &&");
					file.AppendLine($@"			(!a.Max{j}.Exists || (a.Max{j}.Exists && compare{j}.Do(a.Max{j}.Value, b.Axis{j}) != Less)) ? true :");
				}
				file.AppendLine($@"			false;");
				file.AppendLine($@"");

				#endregion

				file.AppendLine($@"		#endregion {i} Dimensional");
				file.AppendLine($@"");
			}
			file.AppendLine($@"	}}");
			file.AppendLine($@"");
			for (int i = 1; i <= dimensions; i++)
			{
				file.AppendLine($@"	#region {i} Dimensional");
				file.AppendLine($@"");

				#region public interface IOmnitree<...>

				file.AppendLine($@"	/// <summary>Inheritance base for {i}D omnitrees.</summary>");
				file.AppendLine($@"	/// <typeparam name=""T"">The element type of the omnitree.</typeparam>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"	/// <typeparam name=""Axis{j}"">The type of the {j}D axis.</typeparam>");
				}
				file.AppendLine($@"	public interface IOmnitree<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}, {Join(1..(i + 1), n => $"Subdivide{n}", ", ")}> : IOmnitree<T> {{ }}");
				file.AppendLine($@"");

				#endregion

				#region public interface IOmnitreePoints<...>

				file.AppendLine($@"	#region OmnitreePoints");
				file.AppendLine($@"");
				file.AppendLine($@"	public interface IOmnitreePoints<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}, {Join(1..(i + 1), n => $"Subdivide{n}", ", ")}> : IOmnitree<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}, {Join(1..(i + 1), n => $"Subdivide{n}", ", ")}>");
				file.AppendLine($@"	{{");
				file.AppendLine($@"		// todo");
				file.AppendLine($@"	}}");
				file.AppendLine($@"");

				#endregion

				#region public class OmnitreePointsLinked<...>

				file.AppendLine($@"	public class OmnitreePointsLinked<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}, {Join(1..(i + 1), n => $"Subdivide{n}", ", ")}, Locate> //: IOmnitree<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		where Compare{j} : struct, IFunc<Axis{j}, Axis{j}, CompareResult>");
				}
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		where Subdivide{j} : struct, IFunc<T[], Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}>, Axis{j}>");
				}
				file.AppendLine($@"		where Locate : struct, IFunc<T, {(i is 1 ? "" : "(")}{Join(1..(i + 1), n => $"Axis{n}", ", ")}{(i is 1 ? "" : ")")}>");
				file.AppendLine($@"	{{");
				file.AppendLine($@"		internal {(i > 30 ? "const" : "readonly static")} {(i > 30 ? "BigInteger" : "int")} ChildrenPerNode = {(i > 30 ? $"BigInteger.Pow(2, {i})" : $"{Math.Pow(2, i)}")};");
				file.AppendLine($@"");
				file.AppendLine($@"		internal Node _top;");
				file.AppendLine($@"		/// <summary>Caches the next time to calculate loads (lower count).</summary>");
				file.AppendLine($@"		internal int _naturalLogLower = 1;");
				file.AppendLine($@"		/// <summary>Caches the next time to calculate loads (upper count).</summary>");
				file.AppendLine($@"		internal int _naturalLogUpper = 1;");
				file.AppendLine($@"		/// <summary>ln(count); min = _defaultLoad.</summary>");
				file.AppendLine($@"		internal int _load = 1;");
				file.AppendLine($@"		internal Locate _locate;");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		internal Compare{j} _compare{j};");
				}
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		internal Subdivide{j} _subdivide{j};");
				}
				file.AppendLine($@"");

				#region Nested Types

				file.AppendLine($@"		#region Nested Types");
				file.AppendLine($@"");

				#region Node

				file.AppendLine($@"		/// <summary>Can be a leaf or a branch.</summary>");
				file.AppendLine($@"		internal abstract class Node");
				file.AppendLine($@"		{{");
				file.AppendLine($@"			internal Omnitree.Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> Bounds;");
				file.AppendLine($@"			internal Branch Parent;");
				file.AppendLine($@"			internal {(i < 30 ? "int" : nameof(BigInteger))} Index;");
				file.AppendLine($@"			internal int Count;");
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>The depth this node is located in the Omnitree.</summary>");
				file.AppendLine($@"			internal int Depth");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				get");
				file.AppendLine($@"				{{");
				file.AppendLine($@"					int depth = -1;");
				file.AppendLine($@"					for (Node node = this; node is not null; node = node.Parent)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						depth++;");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					return depth;");
				file.AppendLine($@"				}}");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>Constructs a node.</summary>");
				file.AppendLine($@"			/// <param name=""bounds"">The bounds of this node.</param>");
				file.AppendLine($@"			/// <param name=""parent"">The parent of this node.</param>");
				file.AppendLine($@"			/// <param name=""index"">The number of elements stored in this node and its children.</param>");
				file.AppendLine($@"			internal Node(Omnitree.Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> bounds, Branch parent, {(i < 30 ? "int" : nameof(BigInteger))} index)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				Bounds = bounds;");
				file.AppendLine($@"				Parent = parent;");
				file.AppendLine($@"				Index = index;");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal Node(Node nodeToClone)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				Bounds = nodeToClone.Bounds;");
				file.AppendLine($@"				Parent = nodeToClone.Parent;");
				file.AppendLine($@"				Index = nodeToClone.Index;");
				file.AppendLine($@"				Count = nodeToClone.Count;");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal abstract Node Clone();");
				file.AppendLine($@"		}}");
				file.AppendLine($@"");

				#endregion

				#region Branch

				file.AppendLine($@"		/// <summary>A branch in the tree. Only contains nodes.</summary>");
				file.AppendLine($@"		internal class Branch : Node");
				file.AppendLine($@"		{{");
				file.AppendLine($@"			internal Node[] Children;");
				file.AppendLine($@"			internal Omnitree.Vector<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> PointOfDivision;");
				file.AppendLine($@"");
				file.AppendLine($@"			/// <summary>Gets child by index.</summary>");
				file.AppendLine($@"			/// <param name=""child_index"">The index of the child to get.</param>");
				file.AppendLine($@"			/// <returns>The child of the given index or null if non-existent.</returns>");
				file.AppendLine($@"			internal Node this[{(i < 30 ? "int" : nameof(BigInteger))} child_index]");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				get");
				file.AppendLine($@"				{{");
				file.AppendLine($@"					if (Children is null)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						return null;");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					if (Children.Length == ChildrenPerNode)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						return Children[(int)child_index];");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					foreach (Node node in Children)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						if (node.Index == child_index)");
				file.AppendLine($@"						{{");
				file.AppendLine($@"							return node;");
				file.AppendLine($@"						}}");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					return null;");
				file.AppendLine($@"				}}");
				file.AppendLine($@"				set");
				file.AppendLine($@"				{{");
				file.AppendLine($@"					// This error check should be unnecessary... but fuck it... might as well");
				file.AppendLine($@"					if (value.Index != child_index)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						throw new System.Exception(""Bug in Omnitree(index / property mis - match when setting a child on a branch)"");");
				file.AppendLine($@"					}}");
				file.AppendLine($@"");
				file.AppendLine($@"					if (Children is null)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						Children = Ɐ(value);");
				file.AppendLine($@"						return;");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					else if (Children.Length == ChildrenPerNode)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						Children[(int)child_index] = value;");
				file.AppendLine($@"						return;");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					for (int i = 0; i < Children.Length; i++)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						if (Children[i].Index == child_index)");
				file.AppendLine($@"						{{");
				file.AppendLine($@"							Children[i] = value;");
				file.AppendLine($@"							return;");
				file.AppendLine($@"						}}");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					Node[] newArray = new Node[Children.Length + 1];");
				file.AppendLine($@"					if (newArray.Length == ChildrenPerNode)");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						for (int i = 0; i < Children.Length; i++)");
				file.AppendLine($@"						{{");
				file.AppendLine($@"							newArray[(int)Children[i].Index] = Children[i];");
				file.AppendLine($@"						}}");
				file.AppendLine($@"						newArray[(int)value.Index] = value;");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					else");
				file.AppendLine($@"					{{");
				file.AppendLine($@"						Array.Copy(Children, newArray, Children.Length);");
				file.AppendLine($@"						newArray[newArray.Length - 1] = value;");
				file.AppendLine($@"					}}");
				file.AppendLine($@"					Children = newArray;");
				file.AppendLine($@"				}}");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal Branch(Omnitree.Vector<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> pointOfDivision, Omnitree.Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> bounds, Branch parent, {(i < 30 ? "int" : nameof(BigInteger))} index)");
				file.AppendLine($@"				: base(bounds, parent, index)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				PointOfDivision = pointOfDivision;");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal Branch(Branch branchToClone) : base(branchToClone)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				Children = branchToClone.Children.Clone() as Node[];");
				file.AppendLine($@"				PointOfDivision = branchToClone.PointOfDivision;");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal override Node Clone() => new Branch(this);");
				file.AppendLine($@"		}}");
				file.AppendLine($@"");

				#endregion

				#region Leaf

				file.AppendLine($@"		/// <summary>A branch in the tree. Only contains items.</summary>");
				file.AppendLine($@"		internal class Leaf : Node");
				file.AppendLine($@"		{{");
				file.AppendLine($@"			internal class Node");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				internal T Value;");
				file.AppendLine($@"				internal Leaf.Node Next;");
				file.AppendLine($@"");
				file.AppendLine($@"				internal Node(T value, Leaf.Node next)");
				file.AppendLine($@"				{{");
				file.AppendLine($@"					Value = value;");
				file.AppendLine($@"					Next = next;");
				file.AppendLine($@"				}}");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal Leaf.Node Head;");
				file.AppendLine($@"");
				file.AppendLine($@"			internal Leaf(Omnitree.Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}> bounds, Branch parent, {(i < 30 ? "int" : nameof(BigInteger))} index)");
				file.AppendLine($@"				: base(bounds, parent, index) {{ }}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal Leaf(Leaf leaf) : base(leaf)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				Head = new Node(leaf.Head.Value, null);");
				file.AppendLine($@"				Node a = Head;");
				file.AppendLine($@"				Node b = leaf.Head;");
				file.AppendLine($@"				while (b is not null)");
				file.AppendLine($@"				{{");
				file.AppendLine($@"					a.Next = new Node(b.Next.Value, null);");
				file.AppendLine($@"					a = a.Next;");
				file.AppendLine($@"					b = b.Next;");
				file.AppendLine($@"				}}");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal void Add(T addition)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				Head = new Leaf.Node(addition, Head);");
				file.AppendLine($@"				Count++;");
				file.AppendLine($@"			}}");
				file.AppendLine($@"");
				file.AppendLine($@"			internal override OmnitreePointsLinked<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}, {Join(1..(i + 1), n => $"Compare{n}", ", ")}, {Join(1..(i + 1), n => $"Subdivide{n}", ", ")}, Locate>.Node Clone() => new Leaf(this);");
				file.AppendLine($@"		}}");
				file.AppendLine($@"");

				#endregion

				file.AppendLine($@"		#endregion");
				file.AppendLine($@"");

				#endregion

				#region Constructors

				file.AppendLine($@"		#region Constructors");
				file.AppendLine($@"");
				file.AppendLine($@"		/// <summary>This constructor is for cloning purposes</summary>");
				file.AppendLine($@"		internal OmnitreePointsLinked(OmnitreePointsLinked<T, {Join(1..(i + 1), n => $"Axis{n}", ", ")}> omnitree)");
				file.AppendLine($@"		{{");
				file.AppendLine($@"			_top = omnitree._top.Clone();");
				file.AppendLine($@"			_load = omnitree._load;");
				file.AppendLine($@"			_locate = omnitree._locate;");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			_compare{j} = omnitree._compare{j};");
					file.AppendLine($@"			_subdivisionOverride{j} = omnitree._subdivisionOverride{j};");
				}
				file.AppendLine($@"		}}");
				file.AppendLine($@"");
				file.AppendLine($@"		internal OmnitreePointsLinked(");
				file.AppendLine($@"			Locate locate,");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			Compare{j} compare{j},");
					file.AppendLine($@"			Subdivide{j} subdivide{j},");
				}
				file.AppendLine($@"			)");
				file.AppendLine($@"		{{");
				file.AppendLine($@"			if (locate is null)");
				file.AppendLine($@"			{{");
				file.AppendLine($@"				throw new ArgumentNullException(nameof(locate));");
				file.AppendLine($@"			}}");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			if (compare{j} is null)");
					file.AppendLine($@"			{{");
					file.AppendLine($@"				throw new ArgumentNullException(nameof(compare{j}));");
					file.AppendLine($@"			}}");
				}
				file.AppendLine($@"		{{");
				file.AppendLine($@"			this._locate = locate;");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			_compare{j} = compare{j};");
					file.AppendLine($@"			_subdivide{j} = subdivide{j};");
				}
				file.AppendLine($@"			_top = new Leaf(Omnitree.Bounds<{Join(1..(i + 1), n => $"Axis{n}", ", ")}>.None, null, -1);");
				file.AppendLine($@"			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);");
				file.AppendLine($@"		}}");
				file.AppendLine($@"");
				file.AppendLine($@"		/// <summary>Constructs a new {i}D omnitree that stores points.</summary>");
				file.AppendLine($@"		/// <param name=""locate"">The delegate for locating items in {i}D space.</param>");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"		/// <param name=""compare{j}"">The delegate for comparing values along the {j}D axis.</param>");
					file.AppendLine($@"		/// <param name=""subdivisionOverride{j}"" >The subdivision overide to be used when splitting the {j} dimension.</param>");
				}
				file.AppendLine($@"		public OmnitreePointsLinked(");
				file.AppendLine($@"			Locate locate,");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			Compare{j} compare{j} = null,");
					file.AppendLine($@"			Subdivide{j} subdivide{j} = null,");
				}
				file.AppendLine($@"			) : this(locate,");
				for (int j = 1; j <= i; j++)
				{
					file.AppendLine($@"			compare{j},");
					file.AppendLine($@"			subdivide{j},");
				}
				file.AppendLine($@"		) {{ }}");
				file.AppendLine($@"");
				file.AppendLine($@"		#endregion");

				#endregion

				file.AppendLine($@"	}}");

				#endregion

				file.AppendLine($@"");
				file.AppendLine($@"	#endregion");
				file.AppendLine($@"");
				file.AppendLine($@"	#endregion");
				file.AppendLine($@"");
			}
			file.AppendLine($@"}}");
			//file.AppendLine($@"#endif");

			//File.WriteAllText(Path.GetDirectoryName(sourcefilepath()) + Path.Combine("..", "..","..","Sources","Towel", "DataStructures", "Omnitree2.cs"), file.ToString());
		}
	}
}
