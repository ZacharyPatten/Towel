using System;
using System.Linq;
using System.Linq.Expressions;
using Towel;
using Towel.Mathematics;
using Towel.Measurements;
using static Towel.Syntax;
using static Towel.Mathematics.MathematicsSyntax;

namespace Mathematics
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("You are runnning the Mathematics example.");
			Console.WriteLine("==========================================");
			Console.WriteLine();

			Random random = new Random();

			#region Basic Operations

			Console.WriteLine("  Basics----------------------------------------------");
			Console.WriteLine();

			// Variables
			Fraction<short> clampA = new Fraction<short>(-123, 9); // -123 / 9
			Fraction<short> clampB = new Fraction<short>(7, 12);   //    7 / 12
			Fraction<short> clampC = new Fraction<short>(14, 15);  //   14 / 15
			double[] values = new double[4];
			Action<Action<double>> valueStepper = values.ToStepper();
			values.Format(x => random.NextDouble());

			// Examples

			double negation = Negation(7);
			Console.WriteLine("    Negate(7): " + negation);

			decimal addition = Addition(7m, 7m);
			Console.WriteLine("    Add(7, 7): " + addition);

			double summation = Σ(valueStepper);
			Console.WriteLine("    Σ (" + string.Join(", ", values.Select(x => Format(x))) + ") = " + summation);

			float subtraction = Subtraction(14f, 7f);
			Console.WriteLine("    Subtract(14, 7): " + subtraction);

			long multiplication = Multiplication(7L, 7L);
			Console.WriteLine("    Multiply(7, 7): " + multiplication);

			double product = Π(valueStepper);
			Console.WriteLine("    Π (" + string.Join(", ", values.Select(x => Format(x))) + ") = " + product);

			short division = Division((short)14, (short)7);
			Console.WriteLine("    Divide(14, 7): " + division);

			double absoluteValue = AbsoluteValue(-7d);
			Console.WriteLine("    AbsoluteValue(-7): " + absoluteValue);

			Fraction<short> clamp = Clamp(clampA, clampB, clampC);
			Console.WriteLine("    Clamp(" + clampA + ", " + clampB + ", " + clampC + "): " + clamp);

			int maximum = Maximum(1, 2, 3);
			Console.WriteLine("    Maximum(1, 2, 3): " + maximum);

			int minimum = Minimum(1, 2, 3);
			Console.WriteLine("    Minimum(1, 2, 3): " + minimum);

			bool lessThan = LessThan((Fraction<int>)1, (Fraction<int>)2);
			Console.WriteLine("    LessThan(1, 2): " + lessThan);

			bool greaterThan = GreaterThan((Fraction<int>)1, (Fraction<int>)2);
			Console.WriteLine("    GreaterThan(1, 2): " + greaterThan);

			CompareResult compare = Comparison((Fraction<short>)7, (Fraction<short>)7);
			Console.WriteLine("    Compare(7, 7): " + compare);

			bool equality = EqualTo(7, 6);
			Console.WriteLine("    Equate(7, 6): " + equality);

			bool equalsLeniency = EqualToLeniency(2, 1, 1);
			Console.WriteLine("    EqualsLeniency(2, 1, 1): " + equalsLeniency);

			Console.WriteLine();

			#endregion

			#region More Numeric Mathematics

			Console.WriteLine("  More Numeric Mathematics----------------------------");
			Console.WriteLine();

			// some random ints for the examples
			int random1 = random.Next(1, 100000);
			int random2 = random.Next(1, 1000);
			int random3 = random.Next(1, 1000);
			int random4 = random.Next(1, 1000);
			int random5 = random.Next(1, 1000);
			int random6 = random.Next(1, 1000);
			int random7 = random.Next(6, 10);
			int random8 = random.Next(1, 100000);
			int[] randomInts1 = new int[3];
			randomInts1.Format(x => random.Next(1, 500) * 2);
			Action<Action<int>> randomInts1Stepper = randomInts1.ToStepper();
			int[] randomInts2 = new int[3];
			randomInts2.Format(x => random.Next(1, 500) * 2);
			Action<Action<int>> randomInts2Stepper = randomInts2.ToStepper();

			bool isPrime = IsPrime(random1);
			Console.WriteLine("    IsPrime(" + random1 + "): " + isPrime);

			bool isNegative = IsNegative(random2);
			Console.WriteLine("    IsNegative(" + random2 + "): " + isNegative);

			bool isNonNegative = IsNonNegative(random3);
			Console.WriteLine("    IsNonNegative(" + random3 + "): " + isNonNegative);

			bool isPositive = IsPositive(random4);
			Console.WriteLine("    IsPositive(" + random4 + "): " + isPositive);

			bool isOdd = IsOdd(random5);
			Console.WriteLine("    IsOdd(" + random5 + "): " + isOdd);

			bool isEven = IsEven(random6);
			Console.WriteLine("    IsEven(" + random6 + "): " + isEven);

			int greatestCommonFactor = GreatestCommonFactor(randomInts1Stepper);
			Console.WriteLine("    GCF(" + string.Join(", ", randomInts1) + "): " + greatestCommonFactor);

			int leastCommonMultiple = LeastCommonMultiple(randomInts2Stepper);
			Console.WriteLine("    LCM(" + string.Join(", ", randomInts2) + "): " + leastCommonMultiple);

			int factorial = Factorial(random7);
			Console.WriteLine("    " + random7 + "!: " + factorial);

			int combinations = Combinations(7, Ɐ(3, 4));
			Console.WriteLine("    7! / (3! * 4!): " + combinations);

			int binomialCoefficient = BinomialCoefficient(7, 2);
			Console.WriteLine("    7 choose 2: " + binomialCoefficient);

			Console.Write("    Prime Factors(" + random8 + "): ");
			FactorPrimes(random8, prime => Console.Write(prime + " "));
			Console.WriteLine();

			Console.WriteLine();

			#endregion

			#region Trigonometry

			Console.WriteLine("  Trigonometry -----------------------------------------");
			Console.WriteLine();

			double randomDouble = random.NextDouble();
			Angle<double> randomAngle = new Angle<double>(randomDouble, Angle.Units.Radians);

			double sineTaylorSeries = SineTaylorSeries(randomAngle);
			Console.WriteLine("    SinTaylorSeries(" + randomAngle + ") = " + Format(sineTaylorSeries));

			double cosineTaylorSeries = CosineTaylorSeries(randomAngle);
			Console.WriteLine("    CosinTaylorSeries(" + randomAngle + ") = " + Format(cosineTaylorSeries));

			Console.WriteLine();

			#endregion

			#region Statistics

			Console.WriteLine("  Statistics-----------------------------------------");
			Console.WriteLine();

			// Data Generation
			double mode_temp = random.NextDouble() * 100;
			double[] dataArray = new double[random.Next(5, 7)];
			dataArray.Format(x => random.NextDouble() * 100);

			// Lets copy a value in the array to ensure there is at least one 
			// duplicate (so the "Mode" example will has something to show)
			dataArray[^1] = dataArray[0];

			// Print Data
			Console.WriteLine("    data: [" + string.Join(", ", dataArray.Select(x => Format(x))) + "]");
			Console.WriteLine();

			Action<Action<double>> data = dataArray.ToStepper();

			// Examples

			double mean = Mean(data);
			Console.WriteLine("    Mean(data): " + Format(mean));

			double median = Median(data);
			Console.WriteLine("    Median(data): " + Format(median));

			//// Here is an example if you just want the values in a list
			//
			//IList<double> modes = new ListArray<double>();
			//Mode(data, x => modes.Add(x));
			//Console.WriteLine("    Mode(data): " +
			//	(modes.Count > 0
			//	? string.Join(",", modes.Select(x => Format(x)))
			//	: "None"));

			bool noModes = true;
			Console.Write("    Mode(data): ");
			Mode(data,
				Extensions.Gaps<double>(
					x => { Console.Write(Format(x)); noModes = false; },
					x => Console.Write(", ")));
			if (noModes)
			{
				Console.Write("None");
			}
			Console.WriteLine();

			double geometricMean = GeometricMean(data);
			Console.WriteLine("    Geometric Mean(data): " + Format(geometricMean));

			Range(out double min, out double max, data);
			Console.WriteLine("    Range(data): " + Format(min) + "-" + Format(max));

			double variance = Variance(dataArray.ToStepper());
			Console.WriteLine("    Variance(data): " + Format(variance));

			double standardDeviation = StandardDeviation(data);
			Console.WriteLine("    Standard Deviation(data): " + Format(standardDeviation));

			double meanDeviation = MeanDeviation(data);
			Console.WriteLine("    Mean Deviation(data): " + Format(meanDeviation));

			double[] quatiles = Quantiles(4, data);
			Console.Write("    Quartiles(data):");
			foreach (double i in quatiles)
			{
				Console.Write(Format(i));
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
			Console.WriteLine("    V dot V: " + V.DotProduct(V));
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
			ConsoleWrite(M.Power(3));

			// Matrix Multiplication
			Console.WriteLine("    minor(M, 1, 1): ");
			ConsoleWrite(M.Minor(1, 1));

			//// Matrix Reduced Row Echelon
			//Console.WriteLine("    ref(M): ");
			//ConsoleWrite(Matrix<double>.Echelon(M));

			// Matrix Reduced Row Echelon
			Console.WriteLine("    rref(M): ");
			ConsoleWrite(Matrix<double>.ReducedEchelon(M));

			// Matrix Determinant
			Console.WriteLine("    determinant(M): " + string.Format("{0:0.00}", Matrix<double>.Determinant(M)));
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

			Expression<Func<double, double>> expression1 = x => 2d * (x / 7d);
			var symbolicExpression1 = Symbolics.Parse(expression1);
			Console.WriteLine("    Expression 1: " + symbolicExpression1);
			Console.WriteLine("      Simplified: " + symbolicExpression1.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression1.Substitute("x", 5d).Simplify());

			Expression<Func<double, double>> expression2 = x => 2d * x / 7d;
			var symbolicExpression2 = Symbolics.Parse(expression2);
			Console.WriteLine("    Expression 2: " + symbolicExpression2);
			Console.WriteLine("      Simplified: " + symbolicExpression2.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression2.Substitute("x", 5d).Simplify());

			Expression<Func<double, double>> expression3 = x => 2d - x + 7d;
			var symbolicExpression3 = Symbolics.Parse(expression3);
			Console.WriteLine("    Expression 3: " + symbolicExpression3);
			Console.WriteLine("      Simplified: " + symbolicExpression3.Simplify());
			Console.WriteLine("      Plugin(5): " + symbolicExpression3.Substitute("x", 5d).Simplify());

			Expression<Func<double, double>> expression4 = x => 2d + (x - 7d);
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
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}

		#region Output Helpers

		static string Format(double value)
		{
			return string.Format("{0:0.00}", value);
		}

		static void ConsoleWrite(Quaternion<double> quaternion)
		{
			Console.WriteLine(
				"      [ x " +
				Format(quaternion.X) + ", y " +
				Format(quaternion.Y) + ", z " +
				Format(quaternion.Z) + ", w " +
				Format(quaternion.W) + " ]");
			Console.WriteLine();
		}

		static void ConsoleWrite(Vector<double> vector)
		{
			Console.Write("      [ ");
			for (int i = 0; i < vector.Dimensions - 1; i++)
				Console.Write(Format(vector[i]) + ", ");
			Console.WriteLine(Format(vector[vector.Dimensions - 1]) + " ]");
			Console.WriteLine();
		}

		static void ConsoleWrite(Matrix<double> matrix)
		{
			for (int i = 0; i < matrix.Rows; i++)
			{
				Console.Write("      [ ");
				for (int j = 0; j < matrix.Columns; j++)
					Console.Write(Format(matrix[i, j]) + " ");
				Console.WriteLine("]");
			}
			Console.WriteLine();
		}

		#endregion
	}
}
