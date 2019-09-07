using System;
using System.Linq;
using System.Linq.Expressions;
using Towel;
using Towel.Mathematics;
using Towel.Measurements;
using static Towel.Mathematics.Compute;

namespace Mathematics
{
	class Program
	{
		// Gets randomized values
		public static Random random = new Random();

		static void Main(string[] args)
		{
			Console.WriteLine("You are runnning the Mathematics example.");
			Console.WriteLine("==========================================");
			Console.WriteLine();

			#region Fraction

			// Fractions need some work... commenting out for now

			//Console.WriteLine("  Fractions-----------------------------------");
			//Console.WriteLine();
			//Fraction128 fraction1 = new Fraction128(2.5);
			//Console.WriteLine("    fraction1 = " + fraction1);
			//Fraction128 fraction2 = new Fraction128(3.75);
			//Console.WriteLine("    fraction2 = " + fraction2);
			//Console.WriteLine("    fraction1 + fraction2 = " + (fraction1 + fraction2));
			//Console.WriteLine("    fraction2 - fraction1 = " + (fraction1 - fraction2));
			//Console.WriteLine("    fraction1 * 2 = " + (fraction1 * 2));
			//Console.WriteLine("    fraction1 / 2 = " + (fraction1 / 2));
			//Console.WriteLine("    fraction1 > fraction2 = " + (fraction1 > fraction2));
			//Console.WriteLine("    fraction1 == fraction2 = " + (fraction1 == fraction2));
			//Console.WriteLine("    fraction1 * 2 == fraction2 = " + (fraction1 * 2 == fraction2));
			//Console.WriteLine("    fraction1 != fraction2 = " + (fraction1 != fraction2));
			//Console.WriteLine();

			#endregion

			#region Basic Operations

			Console.WriteLine("  Basics----------------------------------------------");
			Console.WriteLine();

			// Variables
			Fraction32 clamp_a = (Fraction32)(-123d / 9d);
			Fraction32 clamp_b = (Fraction32)(7d / 12d);
			Fraction32 clamp_c = (Fraction32)(14d / 15d);
			double[] summation_values = new double[]
			{
				random.NextDouble(),
				random.NextDouble(),
				random.NextDouble(),
				random.NextDouble(),
			};

			// Examples
			Console.WriteLine("    Negate(7): " + Negate(7));
			Console.WriteLine("    Add(7, 7): " + Add(7m, 7m));

			Console.Write("    Σ (" + string.Format("{0:0.00}", summation_values[0]));
			for (int i = 1; i < summation_values.Length; i++)
			{
				Console.Write(", " + string.Format("{0:0.00}", summation_values[i]));
			}
			// multiple parameter add overload example (most math functions have this overload)
			Console.WriteLine(") = " + string.Format("{0:0.00}", Add(summation_values.ToStepper())));

			Console.WriteLine("    Subtract(14, 7): " + Subtract(14f, 7f));
			Console.WriteLine("    Multiply(7, 7): " + Multiply((long)7, (long)7));
			Console.WriteLine("    Divide(14, 7): " + Divide((short)14, (short)7));
			Console.WriteLine("    AbsoluteValue(-7): " + AbsoluteValue((double)-7));
			Console.WriteLine("    Clamp(" + clamp_a + ", " + clamp_b + ", " + clamp_c + "): " + Clamp(clamp_a, clamp_b, clamp_c));
			Console.WriteLine("    Maximum(1, 2, 3): " + Maximum(1, 2, 3));
			Console.WriteLine("    Minimum(1, 2, 3): " + Minimum(1, 2, 3));
			Console.WriteLine("    LessThan(1, 2): " + LessThan((Fraction128)1, (Fraction128)2));
			Console.WriteLine("    GreaterThan(1, 2): " + GreaterThan((Fraction64)1, (Fraction64)2));
			Console.WriteLine("    Compare(7, 7): " + Compare((Fraction32)7, (Fraction32)7));
			Console.WriteLine("    Equate(7, 6): " + Equal(7, 6));
			Console.WriteLine("    EqualsLeniency(2, 1, 1): " + EqualLeniency(2, 1, 1));


			Console.WriteLine();

			#endregion

			#region More Numeric Mathematics

			Console.WriteLine("  More Numeric Mathematics----------------------------");
			Console.WriteLine();

			// Variables
			int prime_check = random.Next(1, 100000);
			int[] gcf = new int[] { random.Next(1, 500) * 2, random.Next(1, 500) * 2, random.Next(1, 500) * 2 };
			int[] lcm = new int[] { random.Next(1, 500) * 2, random.Next(1, 500) * 2, random.Next(1, 500) * 2 };
			int prime_factors = random.Next(1, 100000);
			int check = random.Next(1, 1000);

			// Examples
			Console.WriteLine("    IsPrime(" + prime_check + "): " + IsPrime(prime_check));
			Console.WriteLine("    IsNegative(" + check + "): " + IsNegative(check));
			Console.WriteLine("    IsNonNegative(" + check + "): " + IsNonNegative(check));
			Console.WriteLine("    IsPositive(" + check + "): " + IsPositive(check));
			Console.WriteLine("    IsOdd(" + check + "): " + IsOdd(check));
			Console.WriteLine("    IsEven(" + check + "): " + IsEven(check));
			Console.WriteLine("    GCF(" + gcf[0] + ", " + gcf[1] + ", " + gcf[2] + "): " + GreatestCommonFactor(gcf.ToStepper()));
			Console.WriteLine("    LCM(" + lcm[0] + ", " + lcm[1] + ", " + lcm[2] + "): " + LeastCommonMultiple(lcm.ToStepper()));
			Console.Write("    Prime Factors(" + prime_factors + "): ");
			FactorPrimes(prime_factors)(i => Console.Write(i + " "));
			Console.WriteLine();
			Console.WriteLine("    7!: " + Factorial(7));
			Console.WriteLine("    7! / (3! * 4!): " + Combinations(7, new int[] { 3, 4 }));
			Console.WriteLine("    7 choose 2: " + BinomialCoefficient(7, 2));

			Console.WriteLine();

			#endregion

			#region Trigonometry

			Console.WriteLine("  Trigonometry -----------------------------------------");
			Console.WriteLine();

			double randomDouble = random.NextDouble();
			Angle<double> randomAngle = new Angle<double>(randomDouble, Angle.Units.Radians);

			Console.WriteLine("    SinTaylorSeries(" + randomAngle + ") = " + SineTaylorSeries(randomAngle));
			Console.WriteLine("    CosinTaylorSeries(" + randomAngle + ") = " + CosineTaylorSeries(randomAngle));

			Console.WriteLine();

			#endregion

			#region Statistics

			Console.WriteLine("  Statistics-----------------------------------------");
			Console.WriteLine();

			// Variables/Data
			double mode_temp = random.NextDouble() * 100;
			double[] statistics_data = new double[random.Next(5, 7)];
			for (int i = 0; i < statistics_data.Length; i++)
			{
				if (i == 1 || i == statistics_data.Length - 1)
				{
					statistics_data[i] = mode_temp;
				}
				else
				{
					statistics_data[i] = random.NextDouble() * 100;
				}
			}
			string stat_data_string = "    data: [" + string.Format("{0:0.00}", statistics_data[0]);
			for (int i = 1; i < statistics_data.Length; i++)
			{
				stat_data_string += ", " + string.Format("{0:0.00}", statistics_data[i]);
			}
			stat_data_string += "]";
			Console.WriteLine(stat_data_string);
			Console.WriteLine();

			// Examples
			Console.WriteLine("    Mean(data): " + string.Format("{0:0.00}", Mean(statistics_data.ToStepper())));
			Console.WriteLine("    Median(data): " + string.Format("{0:0.00}", Median(statistics_data.ToStepper())));
			double[] modes = Mode(statistics_data.ToStepper()).ToArray();
			Console.WriteLine("    Mode(data): " + string.Join(",", modes.Select(x => string.Format("{0:0.00}", x))));
			Console.WriteLine("    Geometric Mean(data): " + string.Format("{0:0.00}", GeometricMean(statistics_data.ToStepper())));
			Range(out double min, out double max, statistics_data.ToStepper());
			Console.WriteLine("    Range(data): " + string.Format("{0:0.00}", min) + "-" + string.Format("{0:0.00}", max));
			Console.WriteLine("    Variance(data): " + string.Format("{0:0.00}", Variance(statistics_data.ToStepper())));
			Console.WriteLine("    Standard Deviation(data): " + string.Format("{0:0.00}", StandardDeviation(statistics_data.ToStepper())));
			Console.WriteLine("    Mean Deviation(data): " + string.Format("{0:0.00}", MeanDeviation(statistics_data.ToStepper())));
			double[] quatiles = Quantiles(4, statistics_data.ToStepper());
			Console.Write("    Quartiles(data):");
			foreach (double i in quatiles)
			{
				Console.Write(string.Format(" {0:0.00}", i));
			}

			Console.WriteLine();
			Console.WriteLine();

			#endregion

			#region Linear Algebra

			Console.WriteLine("  Linear Algebra------------------------------------");
			Console.WriteLine();

			// Vector Construction
			Vector<double> V = new Vector<double>(4, i => random.NextDouble());

			Console.WriteLine("    Vector<double> V: ");
			ConsoleWrite(V);

			Console.WriteLine("    Normalize(V): ");
			ConsoleWrite(V.Normalize());

			// Vctor Negation
			Console.WriteLine("    -V: ");
			ConsoleWrite(-V);

			// Vector Addition
			Console.WriteLine("    V + V (aka 2V): ");
			ConsoleWrite(V + V);

			// Vector Multiplication
			Console.WriteLine("    V * 2: ");
			ConsoleWrite(V * 2);

			// Vector Division
			Console.WriteLine("    V / 2: ");
			ConsoleWrite(V / 2);

			// Vector Dot Product
			Console.WriteLine("    V dot V: " + Vector<double>.DotProduct(V, V));
			Console.WriteLine();

			// Vector Cross Product
			Vector<double> V3 = new Vector<double>(3, i => random.NextDouble());

			Console.WriteLine("    Vector<double> V3: ");
			ConsoleWrite(V3);
			Console.WriteLine("    V3 cross V3: ");
			ConsoleWrite(Vector<double>.CrossProduct(V3, V3));

			// Matrix Construction
			Matrix<double> M = new Matrix<double>(4, 4, (row, column) => random.NextDouble());

			Console.WriteLine("    Matrix<double>.Identity(4, 4): ");
			ConsoleWrite(Matrix<double>.FactoryIdentity(4, 4));

			Console.WriteLine("    Matrix<double> M: ");
			ConsoleWrite(M);

			// Matrix Negation
			Console.WriteLine("    -M: ");
			ConsoleWrite(-M);

			// Matrix Addition
			Console.WriteLine("    M + M (aka 2M): ");
			ConsoleWrite(M + M);

			// Matrix Subtraction
			Console.WriteLine("    M - M: ");
			ConsoleWrite(M - M);

			// Matrix Multiplication
			Console.WriteLine("    M * M (aka M ^ 2): ");
			ConsoleWrite(M * M);

			// Matrix Power
			Console.WriteLine("    M ^ 3: ");
			ConsoleWrite(M ^ 3);

			// Matrix Multiplication
			Console.WriteLine("    minor(M, 1, 1): ");
			ConsoleWrite(M.Minor(1, 1));

			// Matrix Reduced Row Echelon
			Console.WriteLine("    ref(M): ");
			ConsoleWrite(Matrix<double>.Echelon(M));

			// Matrix Reduced Row Echelon
			Console.WriteLine("    rref(M): ");
			ConsoleWrite(Matrix<double>.ReducedEchelon(M));

			// Matrix Determinant
			Console.WriteLine("    determinent(M): " + string.Format("{0:0.00}", Matrix<double>.Determinent(M)));
			Console.WriteLine();

			// Matrix-Vector Multiplication
			Console.WriteLine("    M * V: ");
			ConsoleWrite(M * V);

			//// Matrix Lower-Upper Decomposition
			//Matrix<double> l = null, u = null;
			//Matrix<double>.DecomposeLowerUpper(M, ref l, ref u); // this method is probably bugged... working on it
			//Console.WriteLine("    Lower-Upper Decomposition:");
			//Console.WriteLine();
			//Console.WriteLine("    	lower(M):");
			//ConsoleWrite(l);
			//Console.WriteLine("    	upper(M):");
			//ConsoleWrite(u);

			// Matrix Inverse
			Matrix<double> inverse = M.Inverse();
			Console.WriteLine("    Inverse(M):");
			ConsoleWrite(inverse);

			// Quaternion Construction
			Quaternion<double> Q = new Quaternion<double>(
				random.NextDouble(),
				random.NextDouble(),
				random.NextDouble(),
				1.0d);

			Console.WriteLine("    Quaternion<double> Q: ");
			ConsoleWrite(Q);

			// Quaternion Addition
			Console.WriteLine("    Q + Q (aka 2Q):");
			ConsoleWrite(Q + Q);

			// Quaternion-Vector Rotation
			Console.WriteLine("    Q * V3 * Q':");
			// Note: the vector should be normalized on the 4th component 
			// for a proper rotation. (I did not do that)
			ConsoleWrite(V3.RotateBy(Q));

			#endregion

			#region Convex Optimization

			//Console.WriteLine("  Convex Optimization-----------------------------------");
			//Console.WriteLine();

			//double[,] tableau = new double[,]
			//{																	
			//	{ 0.0, -0.5, -3.0, -1.0, -4.0, },
			//	{ 40.0, 1.0, 1.0, 1.0, 1.0, },
			//	{ 10.0, -2.0, -1.0, 1.0, 1.0, },
			//	{ 10.0, 0.0, 1.0, 0.0, -1.0, },
			//};

			//Console.WriteLine("    tableau (double): ");
			//ConsoleWrite(tableau); Console.WriteLine();

			//Vector<double> simplex_result = LinearAlgebra.Simplex(ref tableau);

			//Console.WriteLine("    simplex(tableau): ");
			//ConsoleWrite(tableau); Console.WriteLine();

			//Console.WriteLine("    resulting maximization: ");
			//ConsoleWrite(simplex_result);

			#endregion

			#region Symbolics

			Console.WriteLine("  Symbolics---------------------------------------");
			Console.WriteLine();

			Expression<Func<double, double>> expression1 = (x) => 2d * (x / 7d);
			var symbolicExpression1 = Symbolics.Parse(expression1);
			Console.WriteLine("    Expression 1: " + symbolicExpression1);
			Console.WriteLine("      Simplified: " + symbolicExpression1.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression1.Substitute("x", 5d).Simplify());

			Expression<Func<double, double>> expression2 = (x) => 2d * x / 7d;
			var symbolicExpression2 = Symbolics.Parse(expression2);
			Console.WriteLine("    Expression 2: " + symbolicExpression2);
			Console.WriteLine("      Simplified: " + symbolicExpression2.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression2.Substitute("x", 5d).Simplify());

			Expression<Func<double, double>> expression3 = (x) => 2d - x + 7d;
			var symbolicExpression3 = Symbolics.Parse(expression3);
			Console.WriteLine("    Expression 3: " + symbolicExpression3);
			Console.WriteLine("      Simplified: " + symbolicExpression3.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression3.Substitute("x", 5d).Simplify());

			Expression<Func<double, double>> expression4 = (x) => 2d + (x - 7d);
			var symbolicExpression4 = Symbolics.Parse(expression4);
			Console.WriteLine("    Expression 4: " + symbolicExpression4);
			Console.WriteLine("      Simplified: " + symbolicExpression4.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression4.Substitute("x", 5d).Simplify());

			var symbolicExpression6 = Symbolics.Parse<double>("2 * (7 / [x])");
			Console.WriteLine("    Expression 6: " + symbolicExpression6);
			Console.WriteLine("      Simplified: " + symbolicExpression6.Simplify());
			Symbolics.Expression symbolicExpression6Simplified = symbolicExpression6.Substitute("x", 9d).Simplify();
			Console.WriteLine("      Plugin(x = 9): " + symbolicExpression6Simplified);

			var symbolicExpression7 = Symbolics.Parse<double>("10 + 8 * (7 / [x]) + 7 ^ 2");
			Console.WriteLine("    Expression 7: " + symbolicExpression7);
			Console.WriteLine("      Simplified: " + symbolicExpression7.Simplify());
			Console.WriteLine("      Plugin(x = 11): " + symbolicExpression7.Substitute("x", 11d).Simplify());
			Console.WriteLine();

			#endregion

			Console.WriteLine();
			Console.WriteLine("=================================================");
			Console.WriteLine("Example Complete...");
			Console.ReadLine();
		}

		#region Output Helpers

		public static void ConsoleWrite(Quaternion<double> quaternion)
		{
			Console.WriteLine(
				"      [ x " +
				string.Format("{0:0.00}", quaternion.X) + ", y " +
				string.Format("{0:0.00}", quaternion.Y) + ", z " +
				string.Format("{0:0.00}", quaternion.Z) + ", w " +
				string.Format("{0:0.00}", quaternion.W) + " ]");
			Console.WriteLine();
		}

		public static void ConsoleWrite(Vector<double> vector)
		{
			Console.Write("      [ ");
			for (int i = 0; i < vector.Dimensions - 1; i++)
				Console.Write(string.Format("{0:0.00}", vector[i]) + ", ");
			Console.WriteLine(string.Format("{0:0.00}", vector[vector.Dimensions - 1] + " ]"));
			Console.WriteLine();
		}

		public static void ConsoleWrite(Matrix<double> matrix)
		{
			for (int i = 0; i < matrix.Rows; i++)
			{
				Console.Write("      [ ");
				for (int j = 0; j < matrix.Columns; j++)
					Console.Write(string.Format("{0:0.00}", matrix[i, j]) + " ");
				Console.WriteLine("]");
			}
			Console.WriteLine();
		}

		#endregion
	}
}
