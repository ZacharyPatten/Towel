using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using static Towel.Statics;
using Towel.Measurements;
using Towel.DataStructures;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Towel_Testing
{
	[TestClass] public class Statics_Testing
	{
		#region source...

		#pragma warning disable IDE1006 // Naming Styles

		[TestMethod] public void sourcelineNumber_Testing() =>
			sourcelinenumberTest(sourcelinenumber());
		public static void sourcelinenumberTest(int result, [CallerLineNumber] int expected = default) =>
			Assert.IsTrue(result == expected);

		[TestMethod] public void sourcefilename_Testing() =>
			sourcefilepathTest(sourcefilepath());
		public static void sourcefilepathTest(string result, [CallerFilePath] string? expected = default) => 
			Assert.IsTrue(result == expected);

		[TestMethod] public void sourcemembername_Testing() =>
			sourcemembernameTest(sourcemembername());
		public static void sourcemembernameTest(string result, [CallerMemberName] string? expected = default) =>
			Assert.IsTrue(result == expected);

		[TestMethod] public void sourceof_Testing()
		{
			// Note: if this test fails it means that the CallerArgumentExpression is
			// available in the current version of C# and the "sourceof" methods in Towel
			// should be enabled.
			Assert.IsTrue(sourceofTempTest(1 == 2) is null);

			#if false
			sourceofTest(sourceof(1 == 2).Source, 1 == 2);
			sourceof(1 == 2, out string source);
			sourceofTest(source, 1 == 2);
			#endif
		}

		#if false
		public static void sourceofTest<T>(string result, T expression, [CallerArgumentExpression("expression")] string expected = default) => Assert.IsTrue(result == expected);
		#endif
		public static string? sourceofTempTest<T>(T expression, [CallerArgumentExpression("expression")] string? expected = default) => expected;

		#pragma warning restore IDE1006 // Naming Styles

		#endregion

		#region TryParse_Testing

		[TestMethod] public void TryParse_Testing()
		{
			// successful parse
			Assert.IsTrue(TryParse<int>("1") == (true, 1));
			Assert.IsTrue(TryParse<float>("1.2") == (true, 1.2f));
			Assert.IsTrue(TryParse<double>("1.23") == (true, 1.23d));
			Assert.IsTrue(TryParse<decimal>("1.234") == (true, 1.234m));
			Assert.IsTrue(TryParse<ConsoleColor>("Red") == (true, ConsoleColor.Red));
			Assert.IsTrue(TryParse<StringComparison>("Ordinal") == (true, StringComparison.Ordinal));

			// parse fails
			Assert.IsTrue(TryParse<int>("a") == (false, default(int)));
			Assert.IsTrue(TryParse<float>("a") == (false, default(float)));
			Assert.IsTrue(TryParse<double>("a") == (false, default(double)));
			Assert.IsTrue(TryParse<decimal>("a") == (false, default(decimal)));
			Assert.IsTrue(TryParse<ConsoleColor>("a") == (false, default(ConsoleColor)));
			Assert.IsTrue(TryParse<StringComparison>("a") == (false, default(StringComparison)));
		}

		#endregion

		#region Convert_Testing

		[TestMethod] public void Convert_Testing()
		{
			{ // int
				Assert.IsTrue(Convert<int, int>(7) == 7);
				Assert.IsTrue(Convert<int, float>(7) == 7f);
				Assert.IsTrue(Convert<int, double>(7) == 7d);
				Assert.IsTrue(Convert<int, decimal>(7) == 7m);
			}
			{ // float
				Assert.IsTrue(Convert<float, int>(7f) == 7);
				Assert.IsTrue(Convert<float, float>(7f) == 7f);
				Assert.IsTrue(Convert<float, double>(7f) == 7d);
				Assert.IsTrue(Convert<float, decimal>(7f) == 7m);
			}
			{ // double
				Assert.IsTrue(Convert<double, int>(7d) == 7);
				Assert.IsTrue(Convert<double, float>(7d) == 7f);
				Assert.IsTrue(Convert<double, double>(7d) == 7d);
				Assert.IsTrue(Convert<double, decimal>(7d) == 7m);
			}
			{ // decimal
				Assert.IsTrue(Convert<decimal, int>(7m) == 7);
				Assert.IsTrue(Convert<decimal, float>(7m) == 7f);
				Assert.IsTrue(Convert<decimal, double>(7m) == 7d);
				Assert.IsTrue(Convert<decimal, decimal>(7m) == 7m);
			}
		}

		#endregion

		#region Negation_Testing

		[TestMethod] public void Negation_Testing()
		{
			{ // int
				Assert.IsTrue(Negation(-3) == 3);
				Assert.IsTrue(Negation(-2) == 2);
				Assert.IsTrue(Negation(-1) == 1);
				Assert.IsTrue(Negation(0) == 0);
				Assert.IsTrue(Negation(1) == -1);
				Assert.IsTrue(Negation(2) == -2);
				Assert.IsTrue(Negation(3) == -3);
			}
			{ // float
				Assert.IsTrue(Negation(-3f) == 3f);
				Assert.IsTrue(Negation(-2f) == 2f);
				Assert.IsTrue(Negation(-1f) == 1f);
				Assert.IsTrue(Negation(-0.5f) == 0.5f);
				Assert.IsTrue(Negation(0f) == 0f);
				Assert.IsTrue(Negation(0.5f) == -0.5f);
				Assert.IsTrue(Negation(1f) == -1f);
				Assert.IsTrue(Negation(2f) == -2f);
				Assert.IsTrue(Negation(3f) == -3f);
			}
			{ // double
				Assert.IsTrue(Negation(-3d) == 3d);
				Assert.IsTrue(Negation(-2d) == 2d);
				Assert.IsTrue(Negation(-1d) == 1d);
				Assert.IsTrue(Negation(-0.5d) == 0.5d);
				Assert.IsTrue(Negation(0d) == 0d);
				Assert.IsTrue(Negation(0.5d) == -0.5d);
				Assert.IsTrue(Negation(1d) == -1d);
				Assert.IsTrue(Negation(2d) == -2d);
				Assert.IsTrue(Negation(3d) == -3d);
			}
			{ // decimal
				Assert.IsTrue(Negation(-3m) == 3m);
				Assert.IsTrue(Negation(-2m) == 2m);
				Assert.IsTrue(Negation(-1m) == 1m);
				Assert.IsTrue(Negation(-0.5m) == 0.5m);
				Assert.IsTrue(Negation(0m) == 0m);
				Assert.IsTrue(Negation(0.5m) == -0.5m);
				Assert.IsTrue(Negation(1m) == -1m);
				Assert.IsTrue(Negation(2m) == -2m);
				Assert.IsTrue(Negation(3m) == -3m);
			}
		}

		#endregion

		#region Addition_Testing

		[TestMethod] public void Addition_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Addition(0, 0) == 0 + 0);
			Assert.IsTrue(Addition(1, 1) == 1 + 1);
			Assert.IsTrue(Addition(1, 2) == 1 + 2);
			Assert.IsTrue(Addition(-1, 1) == -1 + 1);
			Assert.IsTrue(Addition(1, -1) == 1 + -1);
			Assert.IsTrue(Addition(-1, -1) == -1 + -1);
			// float
			Assert.IsTrue(Addition(0f, 0f) == 0f + 0f);
			Assert.IsTrue(Addition(1f, 1f) == 1f + 1f);
			Assert.IsTrue(Addition(1f, 2f) == 1f + 2f);
			Assert.IsTrue(Addition(-1f, 1f) == -1f + 1f);
			Assert.IsTrue(Addition(1f, -1f) == 1f + -1f);
			Assert.IsTrue(Addition(-1f, -1f) == -1f + -1f);
			// double
			Assert.IsTrue(Addition(0d, 0d) == 0d + 0d);
			Assert.IsTrue(Addition(1d, 1d) == 1d + 1d);
			Assert.IsTrue(Addition(1d, 2d) == 1d + 2d);
			Assert.IsTrue(Addition(-1d, 1d) == -1d + 1d);
			Assert.IsTrue(Addition(1d, -1d) == 1d + -1d);
			Assert.IsTrue(Addition(-1d, -1d) == -1d + -1d);
			// decimal
			Assert.IsTrue(Addition(0m, 0m) == 0m + 0m);
			Assert.IsTrue(Addition(1m, 1m) == 1m + 1m);
			Assert.IsTrue(Addition(1m, 2m) == 1m + 2m);
			Assert.IsTrue(Addition(-1m, 1m) == -1m + 1m);
			Assert.IsTrue(Addition(1m, -1m) == 1m + -1m);
			Assert.IsTrue(Addition(-1m, -1m) == -1m + -1m);

			// Stepper

			// int
			Assert.IsTrue(Addition(1, 2, 3) == 1 + 2 + 3);
			Assert.IsTrue(Addition(-1, -2, -3) == -1 + -2 + -3);
			// float
			Assert.IsTrue(Addition(1f, 2f, 3f) == 1f + 2f + 3f);
			Assert.IsTrue(Addition(-1f, -2f, -3f) == -1f + -2f + -3f);
			// double
			Assert.IsTrue(Addition(1d, 2d, 3d) == 1d + 2d + 3d);
			Assert.IsTrue(Addition(-1d, -2d, -3d) == -1d + -2d + -3d);
			// decimal
			Assert.IsTrue(Addition(1m, 2m, 3m) == 1m + 2m + 3m);
			Assert.IsTrue(Addition(-1m, -2m, -3m) == -1m + -2m + -3m);
		}

		#endregion

		#region Subtraction_Testing

		[TestMethod] public void Subtraction_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Subtraction(0, 0) == 0 - 0);
			Assert.IsTrue(Subtraction(1, 1) == 1 - 1);
			Assert.IsTrue(Subtraction(1, 2) == 1 - 2);
			Assert.IsTrue(Subtraction(-1, 1) == -1 - 1);
			Assert.IsTrue(Subtraction(1, -1) == 1 - -1);
			Assert.IsTrue(Subtraction(-1, -1) == -1 - -1);
			// float
			Assert.IsTrue(Subtraction(0f, 0f) == 0f - 0f);
			Assert.IsTrue(Subtraction(1f, 1f) == 1f - 1f);
			Assert.IsTrue(Subtraction(1f, 2f) == 1f - 2f);
			Assert.IsTrue(Subtraction(-1f, 1f) == -1f - 1f);
			Assert.IsTrue(Subtraction(1f, -1f) == 1f - -1f);
			Assert.IsTrue(Subtraction(-1f, -1f) == -1f - -1f);
			// double
			Assert.IsTrue(Subtraction(0d, 0d) == 0d - 0d);
			Assert.IsTrue(Subtraction(1d, 1d) == 1d - 1d);
			Assert.IsTrue(Subtraction(1d, 2d) == 1d - 2d);
			Assert.IsTrue(Subtraction(-1d, 1d) == -1d - 1d);
			Assert.IsTrue(Subtraction(1d, -1d) == 1d - -1d);
			Assert.IsTrue(Subtraction(-1d, -1d) == -1d - -1d);
			// decimal
			Assert.IsTrue(Subtraction(0m, 0m) == 0m - 0m);
			Assert.IsTrue(Subtraction(1m, 1m) == 1m - 1m);
			Assert.IsTrue(Subtraction(1m, 2m) == 1m - 2m);
			Assert.IsTrue(Subtraction(-1m, 1m) == -1m - 1m);
			Assert.IsTrue(Subtraction(1m, -1m) == 1 - -1m);
			Assert.IsTrue(Subtraction(-1m, -1m) == -1m - -1m);

			// Stepper

			// int
			Assert.IsTrue(Subtraction(1, 2, 3) == 1 - 2 - 3);
			Assert.IsTrue(Subtraction(-1, -2, -3) == -1 - -2 - -3);
			// float
			Assert.IsTrue(Subtraction(1f, 2f, 3f) == 1f - 2f - 3f);
			Assert.IsTrue(Subtraction(-1f, -2f, -3f) == -1f - -2f - -3f);
			// double
			Assert.IsTrue(Subtraction(1d, 2d, 3d) == 1d - 2d - 3d);
			Assert.IsTrue(Subtraction(-1d, -2d, -3d) == -1d - -2d - -3d);
			// decimal
			Assert.IsTrue(Subtraction(1m, 2m, 3m) == 1m - 2m - 3m);
			Assert.IsTrue(Subtraction(-1m, -2m, -3m) == -1m - -2m - -3m);
		}

		#endregion

		#region Multiplication_Testing

		[TestMethod] public void Multiplication_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Multiplication(0, 0) == 0 * 0);
			Assert.IsTrue(Multiplication(1, 1) == 1 * 1);
			Assert.IsTrue(Multiplication(1, 2) == 1 * 2);
			Assert.IsTrue(Multiplication(-1, 1) == -1 * 1);
			Assert.IsTrue(Multiplication(1, -1) == 1 * -1);
			Assert.IsTrue(Multiplication(-1, -1) == -1 * -1);
			// float
			Assert.IsTrue(Multiplication(0f, 0f) == 0f * 0f);
			Assert.IsTrue(Multiplication(1f, 1f) == 1f * 1f);
			Assert.IsTrue(Multiplication(1f, 2f) == 1f * 2f);
			Assert.IsTrue(Multiplication(-1f, 1f) == -1f * 1f);
			Assert.IsTrue(Multiplication(1f, -1f) == 1f * -1f);
			Assert.IsTrue(Multiplication(-1f, -1f) == -1f * -1f);
			// double
			Assert.IsTrue(Multiplication(0d, 0d) == 0d * 0d);
			Assert.IsTrue(Multiplication(1d, 1d) == 1d * 1d);
			Assert.IsTrue(Multiplication(1d, 2d) == 1d * 2d);
			Assert.IsTrue(Multiplication(-1d, 1d) == -1d * 1d);
			Assert.IsTrue(Multiplication(1d, -1d) == 1d * -1d);
			Assert.IsTrue(Multiplication(-1d, -1d) == -1d * -1d);
			// decimal
			Assert.IsTrue(Multiplication(0m, 0m) == 0m * 0m);
			Assert.IsTrue(Multiplication(1m, 1m) == 1m * 1m);
			Assert.IsTrue(Multiplication(1m, 2m) == 1m * 2m);
			Assert.IsTrue(Multiplication(-1m, 1m) == -1m * 1m);
			Assert.IsTrue(Multiplication(1m, -1m) == 1m * -1m);
			Assert.IsTrue(Multiplication(-1m, -1m) == -1m * -1m);

			// Stepper

			// int
			Assert.IsTrue(Multiplication(1, 2, 3) == 1 * 2 * 3);
			Assert.IsTrue(Multiplication(-1, -2, -3) == -1 * -2 * -3);
			// float
			Assert.IsTrue(Multiplication(1f, 2f, 3f) == 1f * 2f * 3f);
			Assert.IsTrue(Multiplication(-1f, -2f, -3f) == -1f * -2f * -3f);
			// double
			Assert.IsTrue(Multiplication(1d, 2d, 3d) == 1d * 2d * 3d);
			Assert.IsTrue(Multiplication(-1d, -2d, -3d) == -1d * -2d * -3d);
			// decimal
			Assert.IsTrue(Multiplication(1m, 2m, 3m) == 1m * 2m * 3m);
			Assert.IsTrue(Multiplication(-1m, -2m, -3m) == -1m * -2m * -3m);
		}

		#endregion

		#region Division_Testing

		[TestMethod] public void Division_Testing()
		{
			// Binary

			// int
			try
			{
				int result = Division(0, 0);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Division(1, 1) == 1 / 1);
			Assert.IsTrue(Division(2, 1) == 2 / 1);
			Assert.IsTrue(Division(4, 2) == 4 / 2);
			Assert.IsTrue(Division(-4, 2) == -4 / 2);
			Assert.IsTrue(Division(4, -2) == 4 / -2);
			Assert.IsTrue(Division(-4, -2) == -4 / -2);
			// float
			Assert.IsTrue(float.IsNaN(Division(0f, 0f)));
			Assert.IsTrue(Division(1f, 1f) == 1f / 1f);
			Assert.IsTrue(Division(2f, 1f) == 2f / 1f);
			Assert.IsTrue(Division(4f, 2f) == 4f / 2f);
			Assert.IsTrue(Division(-4f, 2f) == -4f / 2f);
			Assert.IsTrue(Division(4f, -2f) == 4f / -2f);
			Assert.IsTrue(Division(-4f, -2f) == -4f / -2f);
			// double
			Assert.IsTrue(double.IsNaN(Division(0d, 0d)));
			Assert.IsTrue(Division(1d, 1d) == 1d / 1d);
			Assert.IsTrue(Division(2d, 1d) == 2d / 1d);
			Assert.IsTrue(Division(4d, 2d) == 4d / 2d);
			Assert.IsTrue(Division(-4d, 2d) == -4d / 2d);
			Assert.IsTrue(Division(4d, -2d) == 4d / -2d);
			Assert.IsTrue(Division(-4d, -2d) == -4d / -2d);
			// decimal
			try
			{
				decimal result = Division(0m, 0m);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Division(1m, 1m) == 1m / 1m);
			Assert.IsTrue(Division(2m, 1m) == 2m / 1m);
			Assert.IsTrue(Division(4m, 2m) == 4m / 2m);
			Assert.IsTrue(Division(-4m, 2m) == -4m / 2m);
			Assert.IsTrue(Division(4m, -2m) == 4m / -2m);
			Assert.IsTrue(Division(-4m, -2m) == -4m / -2m);

			// Stepper

			// int
			Assert.IsTrue(Division(100, 10, 10) == 100 / 10 / 10);
			Assert.IsTrue(Division(-100, -10, -10) == -100 / -10 / -10);
			// float
			Assert.IsTrue(Division(100f, 10f, 10f) == 100f / 10f / 10f);
			Assert.IsTrue(Division(-100f, -10f, -10f) == -100f / -10f / -10f);
			// double
			Assert.IsTrue(Division(100d, 10d, 10d) == 100d / 10d / 10d);
			Assert.IsTrue(Division(-100d, -10d, -10d) == -100d / -10d / -10d);
			// decimal
			Assert.IsTrue(Division(100m, 10m, 10m) == 100m / 10m / 10m);
			Assert.IsTrue(Division(-100m, -10m, -10m) == -100m / -10m / -10m);
		}

		#endregion

		#region Inversion_Testing

		[TestMethod] public void Inversion_Testing()
		{
			// Note: not entirely sure about the invert method... :/ may remove it

			Assert.IsTrue(Inversion(1) == 1 / 1);
			Assert.IsTrue(Inversion(-1) == 1 / -1);
			Assert.IsTrue(Inversion(2) == 1 / 2);

			Assert.IsTrue(Inversion(0f) == 1f / 0f);
			Assert.IsTrue(Inversion(1f) == 1f / 1f);
			Assert.IsTrue(Inversion(-1f) == 1f / -1f);
			Assert.IsTrue(Inversion(2f) == 1f / 2f);

			Assert.IsTrue(Inversion(0d) == 1d / 0d);
			Assert.IsTrue(Inversion(1d) == 1d / 1d);
			Assert.IsTrue(Inversion(-1d) == 1d / -1d);
			Assert.IsTrue(Inversion(2d) == 1d / 2d);

			Assert.IsTrue(Inversion(1m) == 1m / 1m);
			Assert.IsTrue(Inversion(-1m) == 1m / -1m);
			Assert.IsTrue(Inversion(2m) == 1m / 2m);

			Assert.ThrowsException<DivideByZeroException>(() => Inversion(0));
			Assert.ThrowsException<DivideByZeroException>(() => Inversion(0m));
		}

		#endregion

		#region Remainder_Testing

		[TestMethod] public void Remainder_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Remainder(0, 1) == 0 % 1);
			Assert.IsTrue(Remainder(1, 1) == 1 % 1);
			Assert.IsTrue(Remainder(8, 3) == 8 % 3);
			Assert.IsTrue(Remainder(-8, 3) == -8 % 3);
			Assert.IsTrue(Remainder(8, -3) == 8 % 3);
			Assert.IsTrue(Remainder(-8, -3) == -8 % 3);

			// float
			Assert.IsTrue(float.IsNaN(Remainder(0f, 0f)));
			Assert.IsTrue(Remainder(0f, 1f) == 0f % 1f);
			Assert.IsTrue(Remainder(1f, 1f) == 1f % 1f);
			Assert.IsTrue(Remainder(8f, 3f) == 8f % 3f);
			Assert.IsTrue(Remainder(-8f, 3f) == -8f % 3f);
			Assert.IsTrue(Remainder(8f, -3f) == 8f % -3f);
			Assert.IsTrue(Remainder(-8f, -3f) == -8f % -3f);

			// double
			Assert.IsTrue(double.IsNaN(Remainder(0d, 0d)));
			Assert.IsTrue(Remainder(0d, 1d) == 0d % 1d);
			Assert.IsTrue(Remainder(1d, 1d) == 1d % 1d);
			Assert.IsTrue(Remainder(8d, 3d) == 8d % 3d);
			Assert.IsTrue(Remainder(-8d, 3d) == -8d % 3d);
			Assert.IsTrue(Remainder(8d, -3d) == 8d % -3d);
			Assert.IsTrue(Remainder(-8d, -3d) == -8d % -3d);

			// decimal
			Assert.IsTrue(Remainder(0m, 1m) == 0m % 1m);
			Assert.IsTrue(Remainder(1m, 1m) == 1m % 1m);
			Assert.IsTrue(Remainder(8m, 3m) == 8m % 3m);
			Assert.IsTrue(Remainder(-8m, 3m) == -8m % 3m);
			Assert.IsTrue(Remainder(8m, -3m) == 8m % -3m);
			Assert.IsTrue(Remainder(-8m, -3m) == -8m % -3m);

			// Stepper

			// int
			Assert.IsTrue(Remainder(15, 8, 3) == 15 % 8 % 3);
			Assert.IsTrue(Remainder(-15, -8, -3) == -15 % -8 % -3);
			// float
			Assert.IsTrue(Remainder(15f, 8f, 3f) == 15f % 8f % 3f);
			Assert.IsTrue(Remainder(-15f, -8f, -3f) == -15f % -8f % -3f);
			// double
			Assert.IsTrue(Remainder(15d, 8d, 3d) == 15d % 8d % 3d);
			Assert.IsTrue(Remainder(-15d, -8d, -3d) == -15d % -8d % -3d);
			// decimal
			Assert.IsTrue(Remainder(15m, 8m, 3m) == 15m % 8m % 3m);
			Assert.IsTrue(Remainder(-15m, -8m, -3m) == -15m % -8m % -3m);

			Assert.ThrowsException<DivideByZeroException>(() => Remainder(0, 0));
			Assert.ThrowsException<DivideByZeroException>(() => Remainder(0m, 0m));
		}

		#endregion

		#region Power_Testing

		[TestMethod] public void Power_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Power(0, 0) == 1);
			Assert.IsTrue(Power(1, 1) == 1);
			Assert.IsTrue(Power(2, 1) == 2);
			Assert.IsTrue(Power(2, 2) == 4);
			Assert.IsTrue(Power(-2, 2) == 4);
			Assert.IsTrue(Power(-2, 3) == -8);

			// float
			Assert.IsTrue(Power(0f, 0f) == 1f);
			Assert.IsTrue(Power(1f, 1f) == 1f);
			Assert.IsTrue(Power(2f, 1f) == 2f);
			Assert.IsTrue(Power(2f, 2f) == 4f);
			Assert.IsTrue(Power(-2f, 2f) == 4f);
			Assert.IsTrue(Power(-2f, 3f) == -8f);

			// double
			Assert.IsTrue(Power(0d, 0d) == 1d);
			Assert.IsTrue(Power(1d, 1d) == 1d);
			Assert.IsTrue(Power(2d, 1d) == 2d);
			Assert.IsTrue(Power(2d, 2d) == 4d);
			Assert.IsTrue(Power(-2d, 2d) == 4d);
			Assert.IsTrue(Power(-2d, 3d) == -8d);

			// decimal
			Assert.IsTrue(Power(0m, 0m) == 1m);
			Assert.IsTrue(Power(1m, 1m) == 1m);
			Assert.IsTrue(Power(2m, 1m) == 2m);
			Assert.IsTrue(Power(2m, 2m) == 4m);
			Assert.IsTrue(Power(-2m, 2m) == 4m);
			Assert.IsTrue(Power(-2m, 3m) == -8m);
		}

		#endregion

		#region Equate_Testing

		[TestMethod] public void Equate_Testing()
		{
			Assert.IsTrue(Equate(0, 0));
			Assert.IsTrue(Equate(1, 1));
			Assert.IsTrue(Equate(2, 2));
			Assert.IsFalse(Equate(0, 1));

			Assert.IsTrue(Equate(0f, 0f));
			Assert.IsTrue(Equate(1f, 1f));
			Assert.IsTrue(Equate(2f, 2f));
			Assert.IsFalse(Equate(0f, 1f));

			Assert.IsTrue(Equate(0d, 0d));
			Assert.IsTrue(Equate(1d, 1d));
			Assert.IsTrue(Equate(2d, 2d));
			Assert.IsFalse(Equate(0d, 1d));

			Assert.IsTrue(Equate(0m, 0m));
			Assert.IsTrue(Equate(1m, 1m));
			Assert.IsTrue(Equate(2m, 2m));
			Assert.IsFalse(Equate(0m, 1m));

			// More than 2 operands

			Assert.IsTrue(Equate(0, 0, 0));
			Assert.IsTrue(Equate(1, 1, 1));
			Assert.IsTrue(Equate(2, 2, 2));

			Assert.IsFalse(Equate(0, 0, 1));
			Assert.IsFalse(Equate(1, 1, 2));
			Assert.IsFalse(Equate(2, 2, 3));

			Assert.IsFalse(Equate(0, 1, 0));
			Assert.IsFalse(Equate(1, 2, 1));
			Assert.IsFalse(Equate(2, 3, 2));

			Assert.IsFalse(Equate(1, 0, 0));
			Assert.IsFalse(Equate(2, 1, 1));
			Assert.IsFalse(Equate(3, 2, 2));

			// nulls

			Assert.IsTrue(Equate<string?>(null, null));
			Assert.IsFalse(Equate<string?>(null, ""));
			Assert.IsFalse(Equate<string?>("", null));
			Assert.IsTrue(Equate("", ""));
		}

		#endregion

		#region EqualToLeniency_Testing

		[TestMethod] public void EqualToLeniency_Testing()
		{
			Assert.IsTrue(EqualToLeniency(0, 0, 0));
			Assert.IsTrue(EqualToLeniency(1, 1, 0));
			Assert.IsTrue(EqualToLeniency(2, 2, 0));

			Assert.IsTrue(EqualToLeniency(0f, 0f, 0f));
			Assert.IsTrue(EqualToLeniency(1f, 1f, 0f));
			Assert.IsTrue(EqualToLeniency(2f, 2f, 0f));

			Assert.IsTrue(EqualToLeniency(0d, 0d, 0d));
			Assert.IsTrue(EqualToLeniency(1d, 1d, 0d));
			Assert.IsTrue(EqualToLeniency(2d, 2d, 0d));

			Assert.IsTrue(EqualToLeniency(0m, 0m, 0m));
			Assert.IsTrue(EqualToLeniency(1m, 1m, 0m));
			Assert.IsTrue(EqualToLeniency(2m, 2m, 0m));

			Assert.IsTrue(EqualToLeniency(0, 1, 1));
			Assert.IsTrue(EqualToLeniency(1, 2, 1));
			Assert.IsTrue(EqualToLeniency(2, 3, 1));

			Assert.IsTrue(EqualToLeniency(0f, 1f, 1f));
			Assert.IsTrue(EqualToLeniency(1f, 2f, 1f));
			Assert.IsTrue(EqualToLeniency(2f, 3f, 1f));

			Assert.IsTrue(EqualToLeniency(0d, 1d, 1d));
			Assert.IsTrue(EqualToLeniency(1d, 2d, 1d));
			Assert.IsTrue(EqualToLeniency(2d, 3d, 1d));

			Assert.IsTrue(EqualToLeniency(0m, 1m, 1m));
			Assert.IsTrue(EqualToLeniency(1m, 2m, 1m));
			Assert.IsTrue(EqualToLeniency(2m, 3m, 1m));

			Assert.IsFalse(EqualToLeniency(0, 2, 1));
			Assert.IsFalse(EqualToLeniency(1, 3, 1));
			Assert.IsFalse(EqualToLeniency(2, 4, 1));

			Assert.IsFalse(EqualToLeniency(0f, 2f, 1f));
			Assert.IsFalse(EqualToLeniency(1f, 3f, 1f));
			Assert.IsFalse(EqualToLeniency(2f, 4f, 1f));

			Assert.IsFalse(EqualToLeniency(0d, 2d, 1d));
			Assert.IsFalse(EqualToLeniency(1d, 3d, 1d));
			Assert.IsFalse(EqualToLeniency(2d, 4d, 1d));

			Assert.IsFalse(EqualToLeniency(0m, 2m, 1m));
			Assert.IsFalse(EqualToLeniency(1m, 3m, 1m));
			Assert.IsFalse(EqualToLeniency(2m, 4m, 1m));
		}

		#endregion

		#region SineTaylorSeries_Testing

		[TestMethod] public void SineTaylorSeries_Testing()
		{
			double sine_zero = SineTaylorSeries(new Angle<double>(0d, Angle.Units.Radians));
			Assert.IsTrue(sine_zero == 0d);

			double sine_pi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(sine_pi, 0d, .00001d));

			double sine_2pi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(sine_2pi, 0d, .00001d));

			double sine_halfPi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(sine_halfPi, 1d, .00001d));

			double sine_3halfsPi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(sine_3halfsPi, -1d, .00001d));

			double sine_neghalfPi = SineTaylorSeries(new Angle<double>(-Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(sine_neghalfPi, -1d, .00001d));

			double sine_neg3halfsPi = SineTaylorSeries(new Angle<double>(-Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(sine_neg3halfsPi, 1d, .00001d));
		}

		#endregion

		#region CosineTaylorSeries_Testing

		[TestMethod] public void CosineTaylorSeries_Testing()
		{
			double cosine_zero = CosineTaylorSeries(new Angle<double>(0d, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_zero, 1d, .00001d));

			double cosine_pi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_pi, -1d, .00001d));

			double cosine_2pi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_2pi, 1d, .00001d));

			double cosine_halfPi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_halfPi, 0d, .00001d));

			double cosine_3halfsPi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_3halfsPi, 0d, .00001d));

			double cosine_neghalfPi = CosineTaylorSeries(new Angle<double>(-Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_neghalfPi, 0d, .00001d));

			double cosine_neg3halfsPi = CosineTaylorSeries(new Angle<double>(-Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualToLeniency(cosine_neg3halfsPi, 0d, .00001d));
		}

		#endregion

		#region SquareRoot_Testing

		[TestMethod] public void SquareRoot_Testing()
		{
			// int
			Assert.IsTrue(SquareRoot(1) == 1);
			Assert.IsTrue(SquareRoot(2) == 1);
			Assert.IsTrue(SquareRoot(3) == 1);
			Assert.IsTrue(SquareRoot(4) == 2);
			Assert.IsTrue(SquareRoot(5) == 2);
			Assert.IsTrue(SquareRoot(6) == 2);
			Assert.IsTrue(SquareRoot(7) == 2);
			Assert.IsTrue(SquareRoot(8) == 2);
			Assert.IsTrue(SquareRoot(9) == 3);
			Assert.IsTrue(SquareRoot(10) == 3);
		}

		#endregion

		#region IsPrime_Testing

		[TestMethod] public void IsPrime_Testing()
		{
			int[] values =
			{
				2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
				103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
			};
			ISet<int> primeNumbers = SetHashLinked.New<int>();
			foreach (int i in values)
			{
				primeNumbers.Add(i);
			}
			for (int i = 2; i < 200; i++)
			{
				if (primeNumbers.Contains(i))
				{
					Assert.IsTrue(IsPrime(i), i.ToString());
				}
				else
				{
					Assert.IsFalse(IsPrime(i), i.ToString());
				}
			}
		}

		#endregion

		#region IsEven_Testing

		[TestMethod] public void IsEven_Testing()
		{
			Random random = new();

			{ // int
				bool isEven = true;
				for (int i = -100; i < 100; i++)
				{
					Assert.IsTrue(isEven ? IsEven(i) : !IsEven(i));
					isEven = !isEven;
				}
			}
			{ // float
				bool isEven = true;
				for (float i = -100f; i < 100; i++)
				{
					Assert.IsTrue(isEven ? IsEven(i) : !IsEven(i));
					isEven = !isEven;

					// only whole numbers can be even... test a random rational value
					float randomRatio = (float)random.NextDouble();
					if (randomRatio > 0d)
					{
						Assert.IsFalse(IsEven(i + randomRatio));
					}
				}
				random.NextDouble();
			}
			{ // double
				bool isEven = true;
				for (double i = -100; i < 100d; i++)
				{
					Assert.IsTrue(isEven ? IsEven(i) : !IsEven(i));
					isEven = !isEven;

					// only whole numbers can be even... test a random rational value
					double randomRatio = random.NextDouble();
					if (randomRatio > 0d)
					{
						Assert.IsFalse(IsEven(i + randomRatio));
					}
				}
			}
			{ // decimal
				bool isEven = true;
				for (decimal i = -100; i < 100m; i++)
				{
					Assert.IsTrue(isEven ? IsEven(i) : !IsEven(i));
					isEven = !isEven;

					// only whole numbers can be even... test a random rational value
					decimal randomRatio = (decimal)random.NextDouble();
					if (randomRatio > 0m)
					{
						Assert.IsFalse(IsEven(i + randomRatio));
					}
				}
			}
		}

		#endregion

		#region IsOdd_Testing

		[TestMethod] public void IsOdd_Testing()
		{
			Random random = new();

			{ // int
				bool isOdd = false;
				for (int i = -100; i < 100; i++)
				{
					Assert.IsTrue(isOdd == IsOdd(i));
					isOdd = !isOdd;
				}
			}
			{ // float
				bool isOdd = false;
				for (float i = -100f; i < 100; i++)
				{
					Assert.IsTrue(isOdd == IsOdd(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					float randomRatio = (float)random.NextDouble();
					if (randomRatio > 0d)
					{
						Assert.IsFalse(IsOdd(i + randomRatio));
					}
				}
				random.NextDouble();
			}
			{ // double
				bool isOdd = false;
				for (double i = -100; i < 100d; i++)
				{
					Assert.IsTrue(isOdd == IsOdd(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					double randomRatio = random.NextDouble();
					if (randomRatio > 0d)
					{
						Assert.IsFalse(IsOdd(i + randomRatio));
					}
				}
			}
			{ // decimal
				bool isOdd = false;
				for (decimal i = -100; i < 100m; i++)
				{
					Assert.IsTrue(isOdd == IsOdd(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					decimal randomRatio = random.NextDecimal(0, 10000) / 10000;
					if (randomRatio > 0m)
					{
						Assert.IsFalse(IsOdd(i + randomRatio));
					}
				}
			}
			{ // Ref<int>
				bool isOdd = false;
				for (int i = -100; i < 100; i++)
				{
					Assert.IsTrue(isOdd == IsOdd<Ref<int>>(i));
					isOdd = !isOdd;
				}
			}
			{ // Ref<float>
				bool isOdd = false;
				for (float i = -100f; i < 100; i++)
				{
					Assert.IsTrue(isOdd == IsOdd<Ref<float>>(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					float randomRatio = (float)random.NextDouble();
					if (randomRatio > 0d)
					{
						Assert.IsFalse(IsOdd<Ref<float>>(i + randomRatio));
					}
				}
				random.NextDouble();
			}
			{ // Ref<double>
				bool isOdd = false;
				for (double i = -100; i < 100d; i++)
				{
					Assert.IsTrue(isOdd == IsOdd<Ref<double>>(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					double randomRatio = random.NextDouble();
					if (randomRatio > 0d)
					{
						Assert.IsFalse(IsOdd<Ref<double>>(i + randomRatio));
					}
				}
			}
			{ // Ref<decimal>
				bool isOdd = false;
				for (decimal i = -100; i < 100m; i++)
				{
					Assert.IsTrue(isOdd == IsOdd<Ref<decimal>>(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					decimal randomRatio = random.NextDecimal(0, 10000) / 10000;
					if (randomRatio > 0m)
					{
						Assert.IsFalse(IsOdd<Ref<decimal>>(i + randomRatio));
					}
				}
			}
		}

		#endregion

		#region AbsoluteValue_Testing

		[TestMethod] public void AbsoluteValue_Testing()
		{
			{ // int
				Assert.IsTrue(AbsoluteValue(-3) == 3);
				Assert.IsTrue(AbsoluteValue(-2) == 2);
				Assert.IsTrue(AbsoluteValue(-1) == 1);
				Assert.IsTrue(AbsoluteValue(0) == 0);
				Assert.IsTrue(AbsoluteValue(1) == 1);
				Assert.IsTrue(AbsoluteValue(2) == 2);
				Assert.IsTrue(AbsoluteValue(3) == 3);
			}
			{ // float
				Assert.IsTrue(AbsoluteValue(-3f) == 3f);
				Assert.IsTrue(AbsoluteValue(-2f) == 2f);
				Assert.IsTrue(AbsoluteValue(-1f) == 1f);
				Assert.IsTrue(AbsoluteValue(-0.5f) == 0.5f);
				Assert.IsTrue(AbsoluteValue(0f) == 0f);
				Assert.IsTrue(AbsoluteValue(0.5f) == 0.5f);
				Assert.IsTrue(AbsoluteValue(1f) == 1f);
				Assert.IsTrue(AbsoluteValue(2f) == 2f);
				Assert.IsTrue(AbsoluteValue(3f) == 3f);
			}
			{ // double
				Assert.IsTrue(AbsoluteValue(-3d) == 3d);
				Assert.IsTrue(AbsoluteValue(-2d) == 2d);
				Assert.IsTrue(AbsoluteValue(-1d) == 1d);
				Assert.IsTrue(AbsoluteValue(-0.5d) == 0.5d);
				Assert.IsTrue(AbsoluteValue(0d) == 0d);
				Assert.IsTrue(AbsoluteValue(0.5d) == 0.5d);
				Assert.IsTrue(AbsoluteValue(1d) == 1d);
				Assert.IsTrue(AbsoluteValue(2d) == 2d);
				Assert.IsTrue(AbsoluteValue(3d) == 3d);
			}
			{ // decimal
				Assert.IsTrue(AbsoluteValue(-3m) == 3m);
				Assert.IsTrue(AbsoluteValue(-2m) == 2m);
				Assert.IsTrue(AbsoluteValue(-1m) == 1m);
				Assert.IsTrue(AbsoluteValue(-0.5m) == 0.5m);
				Assert.IsTrue(AbsoluteValue(0m) == 0m);
				Assert.IsTrue(AbsoluteValue(0.5m) == 0.5m);
				Assert.IsTrue(AbsoluteValue(1m) == 1m);
				Assert.IsTrue(AbsoluteValue(2m) == 2m);
				Assert.IsTrue(AbsoluteValue(3m) == 3m);
			}
			{ // Ref<int>

				Assert.IsTrue(AbsoluteValue<Ref<int>>(-3) == 3);
				Assert.IsTrue(AbsoluteValue<Ref<int>>(-2) == 2);
				Assert.IsTrue(AbsoluteValue<Ref<int>>(-1) == 1);
				Assert.IsTrue(AbsoluteValue<Ref<int>>( 0) == 0);
				Assert.IsTrue(AbsoluteValue<Ref<int>>( 1) == 1);
				Assert.IsTrue(AbsoluteValue<Ref<int>>( 2) == 2);
				Assert.IsTrue(AbsoluteValue<Ref<int>>( 3) == 3);
			}
			{ // Ref<float>
				Assert.IsTrue(AbsoluteValue<Ref<float>>(-3f) == 3f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>(-2f) == 2f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>(-1f) == 1f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>(-0.5f) == 0.5f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>( 0f) == 0f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>( 0.5f) == 0.5f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>( 1f) == 1f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>( 2f) == 2f);
				Assert.IsTrue(AbsoluteValue<Ref<float>>( 3f) == 3f);
			}
			{ // Ref<double>
				Assert.IsTrue(AbsoluteValue<Ref<double>>(-3d) == 3d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(-2d) == 2d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(-1d) == 1d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(-0.5d) == 0.5d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(0d) == 0d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(0.5d) == 0.5d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(1d) == 1d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(2d) == 2d);
				Assert.IsTrue(AbsoluteValue<Ref<double>>(3d) == 3d);
			}
			{ // Ref<decimal>
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(-3m) == 3m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(-2m) == 2m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(-1m) == 1m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(-0.5m) == 0.5m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(0m) == 0m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(0.5m) == 0.5m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(1m) == 1m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(2m) == 2m);
				Assert.IsTrue(AbsoluteValue<Ref<decimal>>(3m) == 3m);
			}
		}

		#endregion

		#region Maximum_Testing

		[TestMethod]
		public void Maximum_Testing()
		{
			{ // int
				Assert.IsTrue(Maximum(compare: null, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) is (10, 5));
				Assert.IsTrue(Maximum(compare: null, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) is (0, 5));
				Assert.IsTrue(Maximum(compare: null, 0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) is (5, 5));
			}
			{ // float
				Assert.IsTrue(Maximum(compare: null, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) is (10, 5f));
				Assert.IsTrue(Maximum(compare: null, 5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) is (0, 5f));
				Assert.IsTrue(Maximum(compare: null, 0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) is (5, 5f));
				Assert.IsTrue(Maximum(compare: null, -0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) is (8, 3f));
			}
			{ // double
				Assert.IsTrue(Maximum(compare: null, -5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) is (10, 5d));
				Assert.IsTrue(Maximum(compare: null, 5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) is (0, 5d));
				Assert.IsTrue(Maximum(compare: null, 0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) is (5, 5d));
				Assert.IsTrue(Maximum(compare: null, -0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) is (8, 3d));
			}
			{ // decimal
				Assert.IsTrue(Maximum(compare: null, -5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) is (10, 5m));
				Assert.IsTrue(Maximum(compare: null, 5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) is (0, 5m));
				Assert.IsTrue(Maximum(compare: null, 0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) is (5, 5m));
				Assert.IsTrue(Maximum(compare: null, -0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) is (8, 3m));
			}
		}

		#endregion

		#region MaximumValue_Testing

		[TestMethod] public void MaximumValue_Testing()
		{
			{ // int
				Assert.IsTrue(MaximumValue(compare: null, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) is 5);
				Assert.IsTrue(MaximumValue(compare: null, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) is 5);
				Assert.IsTrue(MaximumValue(compare: null, 0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) is 5);
			}
			{ // float
				Assert.IsTrue(MaximumValue(compare: null, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) is 5f);
				Assert.IsTrue(MaximumValue(compare: null, 5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) is 5f);
				Assert.IsTrue(MaximumValue(compare: null, 0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) is 5f);
				Assert.IsTrue(MaximumValue(compare: null, -0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) is 3f);
			}
			{ // double
				Assert.IsTrue(MaximumValue(compare: null, -5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) is 5d);
				Assert.IsTrue(MaximumValue(compare: null, 5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) is 5d);
				Assert.IsTrue(MaximumValue(compare: null, 0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) is 5d);
				Assert.IsTrue(MaximumValue(compare: null, -0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) is 3d);
			}
			{ // decimal
				Assert.IsTrue(MaximumValue(compare: null, -5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) is 5m);
				Assert.IsTrue(MaximumValue(compare: null, 5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) is 5m);
				Assert.IsTrue(MaximumValue(compare: null, 0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) is 5m);
				Assert.IsTrue(MaximumValue(compare: null, -0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) is 3m);
			}
		}

		#endregion

		#region MaximumIndex_Testing

		[TestMethod]
		public void MaximumIndex_Testing()
		{
			{ // int
				Assert.IsTrue(MaximumIndex(compare: null, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) is 10);
				Assert.IsTrue(MaximumIndex(compare: null, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) is 0);
				Assert.IsTrue(MaximumIndex(compare: null, 0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) is 5);
			}
			{ // float
				Assert.IsTrue(MaximumIndex(compare: null, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) is 10);
				Assert.IsTrue(MaximumIndex(compare: null, 5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) is 0);
				Assert.IsTrue(MaximumIndex(compare: null, 0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) is 5);
				Assert.IsTrue(MaximumIndex(compare: null, -0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) is 8);
			}
			{ // double
				Assert.IsTrue(MaximumIndex(compare: null, -5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) is 10);
				Assert.IsTrue(MaximumIndex(compare: null, 5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) is 0);
				Assert.IsTrue(MaximumIndex(compare: null, 0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) is 5);
				Assert.IsTrue(MaximumIndex(compare: null, -0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) is 8);
			}
			{ // decimal
				Assert.IsTrue(MaximumIndex(compare: null, -5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) is 10);
				Assert.IsTrue(MaximumIndex(compare: null, 5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) is 0);
				Assert.IsTrue(MaximumIndex(compare: null, 0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) is 5);
				Assert.IsTrue(MaximumIndex(compare: null, -0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) is 8);
			}
		}

		#endregion

		#region Minimum_Testing

		[TestMethod]
		public void Minimum_Testing()
		{
			{ // int
				Assert.IsTrue(Minimum(compare: null, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) is (0, -5));
				Assert.IsTrue(Minimum(compare: null, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) is (10, -5));
				Assert.IsTrue(Minimum(compare: null, 0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) is (10, -5));
			}
			{ // float
				Assert.IsTrue(Minimum(compare: null, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) is (0, -5f));
				Assert.IsTrue(Minimum(compare: null, 5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) is (10, -5f));
				Assert.IsTrue(Minimum(compare: null, 0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) is (10, -5f));
				Assert.IsTrue(Minimum(compare: null, -0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) is (2, -3f));
			}
			{ // double
				Assert.IsTrue(Minimum(compare: null, -5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) is (0, -5d));
				Assert.IsTrue(Minimum(compare: null, 5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) is (10, -5d));
				Assert.IsTrue(Minimum(compare: null, 0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) is (10, -5d));
				Assert.IsTrue(Minimum(compare: null, -0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) is (2, -3d));
			}
			{ // decimal
				Assert.IsTrue(Minimum(compare: null, -5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) is (0, -5m));
				Assert.IsTrue(Minimum(compare: null, 5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) is (10, -5m));
				Assert.IsTrue(Minimum(compare: null, 0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) is (10, -5m));
				Assert.IsTrue(Minimum(compare: null, -0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) is (2, -3m));
			}
		}

		#endregion

		#region MinimumValue_Testing

		[TestMethod] public void MinimumValue_Testing()
		{
			{ // int
				Assert.IsTrue(MinimumValue(compare: null, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) is -5);
				Assert.IsTrue(MinimumValue(compare: null, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) is -5);
				Assert.IsTrue(MinimumValue(compare: null, 0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) is -5);
			}
			{ // float
				Assert.IsTrue(MinimumValue(compare: null, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) is -5f);
				Assert.IsTrue(MinimumValue(compare: null, 5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) is -5f);
				Assert.IsTrue(MinimumValue(compare: null, 0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) is -5f);
				Assert.IsTrue(MinimumValue(compare: null, -0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) is -3f);
			}
			{ // double
				Assert.IsTrue(MinimumValue(compare: null, -5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) is -5d);
				Assert.IsTrue(MinimumValue(compare: null, 5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) is -5d);
				Assert.IsTrue(MinimumValue(compare: null, 0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) is -5d);
				Assert.IsTrue(MinimumValue(compare: null, -0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) is -3d);
			}
			{ // decimal
				Assert.IsTrue(MinimumValue(compare: null, -5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) is -5m);
				Assert.IsTrue(MinimumValue(compare: null, 5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) is -5m);
				Assert.IsTrue(MinimumValue(compare: null, 0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) is -5m);
				Assert.IsTrue(MinimumValue(compare: null, -0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) is -3m);
			}
		}

		#endregion

		#region MinimumIndex_Testing

		[TestMethod]
		public void MinimumIndex_Testing()
		{
			{ // int
				Assert.IsTrue(MinimumIndex(compare: null, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) is 0);
				Assert.IsTrue(MinimumIndex(compare: null, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, 0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) is 10);
			}
			{ // float
				Assert.IsTrue(MinimumIndex(compare: null, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) is 0);
				Assert.IsTrue(MinimumIndex(compare: null, 5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, 0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, -0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) is 2);
			}
			{ // double
				Assert.IsTrue(MinimumIndex(compare: null, -5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) is 0);
				Assert.IsTrue(MinimumIndex(compare: null, 5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, 0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, -0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) is 2);
			}
			{ // decimal
				Assert.IsTrue(MinimumIndex(compare: null, -5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) is 0);
				Assert.IsTrue(MinimumIndex(compare: null, 5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, 0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) is 10);
				Assert.IsTrue(MinimumIndex(compare: null, -0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) is 2);
			}
		}

		#endregion

		#region LeastCommonMultiple_Testing

		[TestMethod] public void LeastCommonMultiple_Testing()
		{
			{ // int
				Assert.IsTrue(LeastCommonMultiple(1, 2, 3) == 6);
				Assert.IsTrue(LeastCommonMultiple(1, 2, 3, 4) == 12);
				Assert.IsTrue(LeastCommonMultiple(1, 2, 3, 4, 5) == 60);
				Assert.IsTrue(LeastCommonMultiple(1, 2, 3, 4, 5, 6) == 60);
				Assert.IsTrue(LeastCommonMultiple(1, 2, 3, 4, 5, 6, 7) == 420);
				Assert.IsTrue(LeastCommonMultiple(1, 2, 3, 4, 5, 6, 7, 8) == 840);
			}
			{ // float
				Assert.IsTrue(LeastCommonMultiple(1f, 2f, 3f) == 6f);
				Assert.IsTrue(LeastCommonMultiple(1f, 2f, 3f, 4f) == 12f);
				Assert.IsTrue(LeastCommonMultiple(1f, 2f, 3f, 4f, 5f) == 60f);
				Assert.IsTrue(LeastCommonMultiple(1f, 2f, 3f, 4f, 5f, 6f) == 60f);
				Assert.IsTrue(LeastCommonMultiple(1f, 2f, 3f, 4f, 5f, 6f, 7f) == 420f);
				Assert.IsTrue(LeastCommonMultiple(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f) == 840f);
			}
			{ // double
				Assert.IsTrue(LeastCommonMultiple(1d, 2d, 3d) == 6d);
				Assert.IsTrue(LeastCommonMultiple(1d, 2d, 3d, 4d) == 12d);
				Assert.IsTrue(LeastCommonMultiple(1d, 2d, 3d, 4d, 5d) == 60d);
				Assert.IsTrue(LeastCommonMultiple(1d, 2d, 3d, 4d, 5d, 6d) == 60d);
				Assert.IsTrue(LeastCommonMultiple(1d, 2d, 3d, 4d, 5d, 6d, 7d) == 420d);
				Assert.IsTrue(LeastCommonMultiple(1d, 2d, 3d, 4d, 5d, 6d, 7d, 8d) == 840d);
			}
			{ // decimal
				Assert.IsTrue(LeastCommonMultiple(1m, 2m, 3m) == 6m);
				Assert.IsTrue(LeastCommonMultiple(1m, 2m, 3m, 4m) == 12m);
				Assert.IsTrue(LeastCommonMultiple(1m, 2m, 3m, 4m, 5m) == 60m);
				Assert.IsTrue(LeastCommonMultiple(1m, 2m, 3m, 4m, 5m, 6m) == 60m);
				Assert.IsTrue(LeastCommonMultiple(1m, 2m, 3m, 4m, 5m, 6m, 7m) == 420m);
				Assert.IsTrue(LeastCommonMultiple(1m, 2m, 3m, 4m, 5m, 6m, 7m, 8m) == 840m);
			}
		}

		#endregion

		#region GreatestCommonFactor_Testing

		[TestMethod] public void GreatestCommonFactor_Testing()
		{
			{ // int
				Assert.IsTrue(GreatestCommonFactor(10, 20, 40) == 10);
				Assert.IsTrue(GreatestCommonFactor(15, 25, 45) == 5);
				Assert.IsTrue(GreatestCommonFactor(2, 2, 4) == 2);
				Assert.IsTrue(GreatestCommonFactor(3, 5, 7) == 1);
				Assert.IsTrue(GreatestCommonFactor(-2, -4, -6) == 2);
			}
			{ // float
				Assert.IsTrue(GreatestCommonFactor(10f, 20f, 40f) == 10f);
				Assert.IsTrue(GreatestCommonFactor(15f, 25f, 45f) == 5f);
				Assert.IsTrue(GreatestCommonFactor(2f, 2f, 4f) == 2f);
				Assert.IsTrue(GreatestCommonFactor(3f, 5f, 7f) == 1f);
				Assert.IsTrue(GreatestCommonFactor(-2f, -4f, -6f) == 2f);
			}
			{ // double
				Assert.IsTrue(GreatestCommonFactor(10d, 20d, 40d) == 10d);
				Assert.IsTrue(GreatestCommonFactor(15d, 25d, 45d) == 5d);
				Assert.IsTrue(GreatestCommonFactor(2d, 2d, 4d) == 2d);
				Assert.IsTrue(GreatestCommonFactor(3d, 5d, 7d) == 1d);
				Assert.IsTrue(GreatestCommonFactor(-2d, -4d, -6d) == 2d);
			}
			{ // decimal
				Assert.IsTrue(GreatestCommonFactor(10m, 20m, 40m) == 10m);
				Assert.IsTrue(GreatestCommonFactor(15m, 25m, 45m) == 5m);
				Assert.IsTrue(GreatestCommonFactor(2m, 2m, 4m) == 2m);
				Assert.IsTrue(GreatestCommonFactor(3m, 5m, 7m) == 1m);
				Assert.IsTrue(GreatestCommonFactor(-2m, -4m, -6m) == 2m);
			}
		}

		#endregion

		#region Factorial_Testing

		[TestMethod] public void Factorial_Testing()
		{
			{ // int
				Assert.IsTrue(Factorial(1) == 1);
				Assert.IsTrue(Factorial(2) == 2);
				Assert.IsTrue(Factorial(3) == 6);
				Assert.IsTrue(Factorial(4) == 24);
				Assert.IsTrue(Factorial(5) == 120);
			}
			{ // float
				Assert.IsTrue(Factorial(1f) == 1);
				Assert.IsTrue(Factorial(2f) == 2);
				Assert.IsTrue(Factorial(3f) == 6);
				Assert.IsTrue(Factorial(4f) == 24);
				Assert.IsTrue(Factorial(5f) == 120);
			}
			{ // double
				Assert.IsTrue(Factorial(1d) == 1);
				Assert.IsTrue(Factorial(2d) == 2);
				Assert.IsTrue(Factorial(3d) == 6);
				Assert.IsTrue(Factorial(4d) == 24);
				Assert.IsTrue(Factorial(5d) == 120);
			}
			{ // decimal
				Assert.IsTrue(Factorial(1m) == 1);
				Assert.IsTrue(Factorial(2m) == 2);
				Assert.IsTrue(Factorial(3m) == 6);
				Assert.IsTrue(Factorial(4m) == 24);
				Assert.IsTrue(Factorial(5m) == 120);
			}
		}

		#endregion

		#region BinomialCoefficient_Testing

		[TestMethod] public void BinomialCoefficient_Testing()
		{
			{ // int
				Assert.IsTrue(BinomialCoefficient(1, 1) == 1);
				Assert.IsTrue(BinomialCoefficient(7, 2) == 21);
				Assert.IsTrue(BinomialCoefficient(8, 4) == 70);
				Assert.IsTrue(BinomialCoefficient(10, 3) == 120);
				Assert.IsTrue(BinomialCoefficient(11, 5) == 462);
			}
			{ // float
				Assert.IsTrue(BinomialCoefficient(1f, 1f) == 1f);
				Assert.IsTrue(BinomialCoefficient(7f, 2f) == 21f);
				Assert.IsTrue(BinomialCoefficient(8f, 4f) == 70f);
				Assert.IsTrue(BinomialCoefficient(10f, 3f) == 120f);
				Assert.IsTrue(BinomialCoefficient(11f, 5f) == 462f);
			}
			{ // double
				Assert.IsTrue(BinomialCoefficient(1d, 1d) == 1d);
				Assert.IsTrue(BinomialCoefficient(7d, 2d) == 21d);
				Assert.IsTrue(BinomialCoefficient(8d, 4d) == 70d);
				Assert.IsTrue(BinomialCoefficient(10d, 3d) == 120d);
				Assert.IsTrue(BinomialCoefficient(11d, 5d) == 462d);
			}
			{ // decimal
				Assert.IsTrue(BinomialCoefficient(1m, 1m) == 1m);
				Assert.IsTrue(BinomialCoefficient(7m, 2m) == 21m);
				Assert.IsTrue(BinomialCoefficient(8m, 4m) == 70m);
				Assert.IsTrue(BinomialCoefficient(10m, 3m) == 120m);
				Assert.IsTrue(BinomialCoefficient(11m, 5m) == 462m);
			}
		}

		#endregion

		#region Median_Testing

		[TestMethod] public void Median_Testing()
		{
			{ // int
				Assert.IsTrue(Median(1, 2, 3, 4, 5) == 3);
				Assert.IsTrue(Median(3, 1, 2, 4, 5) == 3);
				Assert.IsTrue(Median(1, 2, 4, 5, 3) == 3);
			}
			{ // float
				Assert.IsTrue(Median(1f, 2f, 3f, 4f, 5f) == 3f);
				Assert.IsTrue(Median(3f, 1f, 2f, 4f, 5f) == 3f);
				Assert.IsTrue(Median(1f, 2f, 4f, 5f, 3f) == 3f);

				Assert.IsTrue(Median(1f, 2f, 3f, 4f, 5f, 6f) == 3.5f);
			}
			{ // double
				Assert.IsTrue(Median(1d, 2d, 3d, 4d, 5d) == 3d);
				Assert.IsTrue(Median(3d, 1d, 2d, 4d, 5d) == 3d);
				Assert.IsTrue(Median(1d, 2d, 4d, 5d, 3d) == 3d);

				Assert.IsTrue(Median(1d, 2d, 3d, 4d, 5d, 6d) == 3.5d);
			}
			{ // decimal
				Assert.IsTrue(Median(1m, 2m, 3m, 4m, 5m) == 3m);
				Assert.IsTrue(Median(3m, 1m, 2m, 4m, 5m) == 3m);
				Assert.IsTrue(Median(1m, 2m, 4m, 5m, 3m) == 3m);

				Assert.IsTrue(Median(1m, 2m, 3m, 4m, 5m, 6m) == 3.5m);
			}
		}

		#endregion

		#region Mode_Testing

		[TestMethod]
		public void Mode_Testing()
		{
			{ // int
				ListArray<int> list = new();
				Mode(x => list.Add(x), 1, 2, 3, 4, 5, 1);
				Assert.IsTrue(list.Count is 1);
				Assert.IsTrue(list[0] is 1);
			}
			{ // float
				ListArray<float> list = new();
				Mode(x => list.Add(x), 1f, 2f, 3f, 4f, 5f, 1f);
				Assert.IsTrue(list.Count is 1);
				Assert.IsTrue(list[0] is 1f);
			}
			{ // double
				ListArray<double> list = new();
				Mode(x => list.Add(x), 1d, 2d, 3d, 4d, 5d, 1d);
				Assert.IsTrue(list.Count is 1);
				Assert.IsTrue(list[0] is 1d);
			}
			{ // decimal
				ListArray<decimal> list = new();
				Mode(x => list.Add(x), 1m, 2m, 3m, 4m, 5m, 1m);
				Assert.IsTrue(list.Count is 1);
				Assert.IsTrue(list[0] is 1m);
			}
		}

		#endregion

		#region Clamp

		[TestMethod] public void Clamp_Testing()
		{
			{ // int
				Assert.IsTrue(Clamp(5, 3, 7) == 5);
				Assert.IsTrue(Clamp(3, 5, 7) == 5);
				Assert.IsTrue(Clamp(9, 3, 7) == 7);
			}
			{ // float
				Assert.IsTrue(Clamp(5f, 3f, 7f) == 5f);
				Assert.IsTrue(Clamp(3f, 5f, 7f) == 5f);
				Assert.IsTrue(Clamp(9f, 3f, 7f) == 7f);
			}
			{ // double
				Assert.IsTrue(Clamp(5d, 3d, 7d) == 5d);
				Assert.IsTrue(Clamp(3d, 5d, 7d) == 5d);
				Assert.IsTrue(Clamp(9d, 3d, 7d) == 7d);
			}
			{ // decimal
				Assert.IsTrue(Clamp(5m, 3m, 7m) == 5m);
				Assert.IsTrue(Clamp(3m, 5m, 7m) == 5m);
				Assert.IsTrue(Clamp(9m, 3m, 7m) == 7m);
			}

			{ // Ref<int>
				Assert.IsTrue(Clamp<Ref<int>>(5, 3, 7) == 5);
				Assert.IsTrue(Clamp<Ref<int>>(3, 5, 7) == 5);
				Assert.IsTrue(Clamp<Ref<int>>(9, 3, 7) == 7);
			}
			{ // Ref<float>
				Assert.IsTrue(Clamp<Ref<float>>(5f, 3f, 7f) == 5f);
				Assert.IsTrue(Clamp<Ref<float>>(3f, 5f, 7f) == 5f);
				Assert.IsTrue(Clamp<Ref<float>>(9f, 3f, 7f) == 7f);
			}
			{ // Ref<double>
				Assert.IsTrue(Clamp<Ref<double>>(5d, 3d, 7d) == 5d);
				Assert.IsTrue(Clamp<Ref<double>>(3d, 5d, 7d) == 5d);
				Assert.IsTrue(Clamp<Ref<double>>(9d, 3d, 7d) == 7d);
			}
			{ // Ref<decimal>
				Assert.IsTrue(Clamp<Ref<decimal>>(5m, 3m, 7m) == 5m);
				Assert.IsTrue(Clamp<Ref<decimal>>(3m, 5m, 7m) == 5m);
				Assert.IsTrue(Clamp<Ref<decimal>>(9m, 3m, 7m) == 7m);
			}
		}

		#endregion

		#region InequalTo_Testing

		[TestMethod] public void InequalTo_Testing()
		{
			Assert.IsTrue(Inequate(0, 1));
			Assert.IsTrue(Inequate(-1, 1));
			Assert.IsFalse(Inequate(1, 1));
			Assert.IsTrue(Inequate(6, 7));

			Assert.IsTrue(Inequate(0f, 1f));
			Assert.IsTrue(Inequate(-1f, 1f));
			Assert.IsFalse(Inequate(1f, 1f));
			Assert.IsTrue(Inequate(6f, 7f));

			Assert.IsTrue(Inequate(0d, 1d));
			Assert.IsTrue(Inequate(-1d, 1d));
			Assert.IsFalse(Inequate(1d, 1d));
			Assert.IsTrue(Inequate(6d, 7d));

			Assert.IsTrue(Inequate(0m, 1m));
			Assert.IsTrue(Inequate(-1m, 1m));
			Assert.IsFalse(Inequate(1m, 1m));
			Assert.IsTrue(Inequate(6m, 7m));
		}

		#endregion

		#region Comparison_Testing

		[TestMethod] public void Comparison_Testing()
		{
			Assert.IsTrue(Compare(0, 0) is Equal);
			Assert.IsTrue(Compare(-1, 0) is Less);
			Assert.IsTrue(Compare(1, 0) is Greater);
			Assert.IsTrue(Compare(777, 333) is Greater);
			Assert.IsTrue(Compare(333, 777) is Less);
			Assert.IsTrue(Compare(777, 777) is Equal);

			Assert.IsTrue(Compare(0f, 0f) is Equal);
			Assert.IsTrue(Compare(-1f, 0f) is Less);
			Assert.IsTrue(Compare(1f, 0f) is Greater);
			Assert.IsTrue(Compare(777f, 333f) is Greater);
			Assert.IsTrue(Compare(333f, 777f) is Less);
			Assert.IsTrue(Compare(777f, 777f) is Equal);

			Assert.IsTrue(Compare(0d, 0d) is Equal);
			Assert.IsTrue(Compare(-1d, 0d) is Less);
			Assert.IsTrue(Compare(1d, 0d) is Greater);
			Assert.IsTrue(Compare(777d, 333d) is Greater);
			Assert.IsTrue(Compare(333d, 777d) is Less);
			Assert.IsTrue(Compare(777d, 777d) is Equal);

			Assert.IsTrue(Compare(0m, 0m) is Equal);
			Assert.IsTrue(Compare(-1m, 0m) is Less);
			Assert.IsTrue(Compare(1m, 0m) is Greater);
			Assert.IsTrue(Compare(777m, 333m) is Greater);
			Assert.IsTrue(Compare(333m, 777m) is Less);
			Assert.IsTrue(Compare(777m, 777m) is Equal);
		}

		#endregion

		#region LessThan_Testing

		[TestMethod] public void LessThan_Testing()
		{
			Assert.IsFalse(LessThan(0, 0));
			Assert.IsTrue(LessThan(-1, 0));
			Assert.IsFalse(LessThan(1, 0));
			Assert.IsFalse(LessThan(777, 333));
			Assert.IsTrue(LessThan(333, 777));
			Assert.IsFalse(LessThan(777, 777));

			Assert.IsFalse(LessThan(0f, 0f));
			Assert.IsTrue(LessThan(-1f, 0f));
			Assert.IsFalse(LessThan(1f, 0f));
			Assert.IsFalse(LessThan(777f, 333f));
			Assert.IsTrue(LessThan(333f, 777f));
			Assert.IsFalse(LessThan(777f, 777f));

			Assert.IsFalse(LessThan(0d, 0d));
			Assert.IsTrue(LessThan(-1d, 0d));
			Assert.IsFalse(LessThan(1d, 0d));
			Assert.IsFalse(LessThan(777d, 333d));
			Assert.IsTrue(LessThan(333d, 777d));
			Assert.IsFalse(LessThan(777d, 777d));

			Assert.IsFalse(LessThan(0m, 0m));
			Assert.IsTrue(LessThan(-1m, 0m));
			Assert.IsFalse(LessThan(1m, 0m));
			Assert.IsFalse(LessThan(777m, 333m));
			Assert.IsTrue(LessThan(333m, 777m));
			Assert.IsFalse(LessThan(777m, 777m));
		}

		#endregion

		#region GreaterThan_Testing

		[TestMethod] public void GreaterThan_Testing()
		{
			Assert.IsFalse(GreaterThan(0, 0));
			Assert.IsFalse(GreaterThan(-1, 0));
			Assert.IsTrue(GreaterThan(1, 0));
			Assert.IsTrue(GreaterThan(777, 333));
			Assert.IsFalse(GreaterThan(333, 777));
			Assert.IsFalse(GreaterThan(777, 777));

			Assert.IsFalse(GreaterThan(0f, 0f));
			Assert.IsFalse(GreaterThan(-1f, 0f));
			Assert.IsTrue(GreaterThan(1f, 0f));
			Assert.IsTrue(GreaterThan(777f, 333f));
			Assert.IsFalse(GreaterThan(333f, 777f));
			Assert.IsFalse(GreaterThan(777f, 777f));

			Assert.IsFalse(GreaterThan(0d, 0d));
			Assert.IsFalse(GreaterThan(-1d, 0d));
			Assert.IsTrue(GreaterThan(1d, 0d));
			Assert.IsTrue(GreaterThan(777d, 333d));
			Assert.IsFalse(GreaterThan(333d, 777d));
			Assert.IsFalse(GreaterThan(777d, 777d));

			Assert.IsFalse(GreaterThan(0m, 0m));
			Assert.IsFalse(GreaterThan(-1m, 0m));
			Assert.IsTrue(GreaterThan(1m, 0m));
			Assert.IsTrue(GreaterThan(777m, 333m));
			Assert.IsFalse(GreaterThan(333m, 777m));
			Assert.IsFalse(GreaterThan(777m, 777m));
		}

		#endregion

		#region LessThanOrEqual_Testing

		[TestMethod] public void LessThanOrEqual_Testing()
		{
			Assert.IsTrue(LessThanOrEqual(0, 0));
			Assert.IsTrue(LessThanOrEqual(-1, 0));
			Assert.IsFalse(LessThanOrEqual(1, 0));
			Assert.IsFalse(LessThanOrEqual(777, 333));
			Assert.IsTrue(LessThanOrEqual(333, 777));
			Assert.IsTrue(LessThanOrEqual(777, 777));

			Assert.IsTrue(LessThanOrEqual(0f, 0f));
			Assert.IsTrue(LessThanOrEqual(-1f, 0f));
			Assert.IsFalse(LessThanOrEqual(1f, 0f));
			Assert.IsFalse(LessThanOrEqual(777f, 333f));
			Assert.IsTrue(LessThanOrEqual(333f, 777f));
			Assert.IsTrue(LessThanOrEqual(777f, 777f));

			Assert.IsTrue(LessThanOrEqual(0d, 0d));
			Assert.IsTrue(LessThanOrEqual(-1d, 0d));
			Assert.IsFalse(LessThanOrEqual(1d, 0d));
			Assert.IsFalse(LessThanOrEqual(777d, 333d));
			Assert.IsTrue(LessThanOrEqual(333d, 777d));
			Assert.IsTrue(LessThanOrEqual(777d, 777d));

			Assert.IsTrue(LessThanOrEqual(0m, 0m));
			Assert.IsTrue(LessThanOrEqual(-1m, 0m));
			Assert.IsFalse(LessThanOrEqual(1m, 0m));
			Assert.IsFalse(LessThanOrEqual(777m, 333m));
			Assert.IsTrue(LessThanOrEqual(333m, 777m));
			Assert.IsTrue(LessThanOrEqual(777m, 777m));
		}

		#endregion

		#region GreaterThanOrEqual_Testing

		[TestMethod] public void GreaterThanOrEqual_Testing()
		{
			Assert.IsTrue(GreaterThanOrEqual(0, 0));
			Assert.IsFalse(GreaterThanOrEqual(-1, 0));
			Assert.IsTrue(GreaterThanOrEqual(1, 0));
			Assert.IsTrue(GreaterThanOrEqual(777, 333));
			Assert.IsFalse(GreaterThanOrEqual(333, 777));
			Assert.IsTrue(GreaterThanOrEqual(777, 777));

			Assert.IsTrue(GreaterThanOrEqual(0f, 0f));
			Assert.IsFalse(GreaterThanOrEqual(-1f, 0f));
			Assert.IsTrue(GreaterThanOrEqual(1f, 0f));
			Assert.IsTrue(GreaterThanOrEqual(777f, 333f));
			Assert.IsFalse(GreaterThanOrEqual(333f, 777f));
			Assert.IsTrue(GreaterThanOrEqual(777f, 777f));

			Assert.IsTrue(GreaterThanOrEqual(0d, 0d));
			Assert.IsFalse(GreaterThanOrEqual(-1d, 0d));
			Assert.IsTrue(GreaterThanOrEqual(1d, 0d));
			Assert.IsTrue(GreaterThanOrEqual(777d, 333d));
			Assert.IsFalse(GreaterThanOrEqual(333d, 777d));
			Assert.IsTrue(GreaterThanOrEqual(777d, 777d));

			Assert.IsTrue(GreaterThanOrEqual(0m, 0m));
			Assert.IsFalse(GreaterThanOrEqual(-1m, 0m));
			Assert.IsTrue(GreaterThanOrEqual(1m, 0m));
			Assert.IsTrue(GreaterThanOrEqual(777m, 333m));
			Assert.IsFalse(GreaterThanOrEqual(333m, 777m));
			Assert.IsTrue(GreaterThanOrEqual(777m, 777m));
		}

		#endregion

		#region FactorPrimes_Testing

		public static void FactorPrimes_Test<T>(T value, params T[] expectedFactors)
		{
			IList<T> list = new ListLinked<T>();
			expectedFactors.Stepper(x => list.Add(x));
			Assert.IsTrue(list.Count > 0);
			FactorPrimes(value, x => list.RemoveFirst(x));
			Assert.IsTrue(list.Count == 0);
		}

		[TestMethod] public void FactorPrimes_Testing()
		{
			{ // int
				FactorPrimes_Test<int>(2,  /* factors: */ 2);
				FactorPrimes_Test<int>(4,  /* factors: */ 2, 2);
				FactorPrimes_Test<int>(7,  /* factors: */ 7);
				FactorPrimes_Test<int>(9,  /* factors: */ 3, 3);
				FactorPrimes_Test<int>(10, /* factors: */ 2, 5);
				FactorPrimes_Test<int>(15, /* factors: */ 3, 5);
				FactorPrimes_Test<int>(21, /* factors: */ 7, 3);
			}
			{ // decimal
				FactorPrimes_Test<decimal>(2,  /* factors: */ 2);
				FactorPrimes_Test<decimal>(4,  /* factors: */ 2, 2);
				FactorPrimes_Test<decimal>(7,  /* factors: */ 7);
				FactorPrimes_Test<decimal>(9,  /* factors: */ 3, 3);
				FactorPrimes_Test<decimal>(10, /* factors: */ 2, 5);
				FactorPrimes_Test<decimal>(15, /* factors: */ 3, 5);
				FactorPrimes_Test<decimal>(21, /* factors: */ 7, 3);
			}
		}

		#endregion

		#region SearchBinary_Testing

		[TestMethod] public void SearchBinary_Test()
		{
			{ // [even] collection size [found]
				int[] values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
				for (int i = 0; i < values.Length; i++)
				{
					var result = SearchBinary(values, i);
					Assert.IsTrue(result.Found);
					Assert.IsTrue(result.Index == i);
					Assert.IsTrue(result.Value == i);
				}
			}
			{ // [odd] collection size [found]
				int[] values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, };
				for (int i = 0; i < values.Length; i++)
				{
					var result = SearchBinary(values, i);
					Assert.IsTrue(result.Found);
					Assert.IsTrue(result.Index == i);
					Assert.IsTrue(result.Value == i);
				}
			}
			{ // [even] collection size [not found]
				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, };
				for (int i = 0, j = -10; j <= 10; i++, j += 2)
				{
					var result = SearchBinary(values, j);
					Assert.IsTrue(!result.Found);
					Assert.IsTrue(result.Index == i - 1);
					Assert.IsTrue(result.Value == default);
				}
			}
			{ // [odd] collection size [not found]
				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, };
				for (int i = 0, j = -10; j <= 8; i++, j += 2)
				{
					var result = SearchBinary(values, j);
					Assert.IsTrue(!result.Found);
					Assert.IsTrue(result.Index == i - 1);
					Assert.IsTrue(result.Value == default);
				}
			}
			{ // exception: invalid compare function
				int[] values = { -9, -7, -5, -3, -1, 1, 3, 5, 7, };
				Assert.ThrowsException<ArgumentException>(() => SearchBinary<int>(values, a => (CompareResult)int.MinValue));
			}
			{ // exception: null argument
				int[]? values = null;
				Assert.ThrowsException<ArgumentException>(() => SearchBinary(values, 7));
			}
		}

		#endregion

		#region Sort

		public const int SortSize = 10;
		public const int SortRandomSeed = 7;

		public static void TestSortAlgorithm(
			Action<int[], Func<int, int, CompareResult>> algorithm,
			Action<int[], int, int, Func<int, int, CompareResult>> algorithmPartial,
			int? sizeOverride = null)
		{
			void Test(int sizeAdjusted)
			{
				Random random = new(SortRandomSeed);
				int[] array = new int[sizeAdjusted];
				Extensions.Iterate(sizeAdjusted, i => array[i] = i);
				Shuffle<int>(array, random);
				Assert.IsFalse(IsOrdered<int>(array), "Test failed (invalid randomization).");
				algorithm(array, Compare);
				Assert.IsTrue(IsOrdered<int>(array), "Sorting algorithm failed.");
			}

			Test(sizeOverride ?? SortSize); // Even Data Set
			Test((sizeOverride ?? SortSize) + 1); // Odd Data Set
			if (sizeOverride is null) Test(1000); // Large(er) Data Set

			{ // Partial Array Sort
				int[] array = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithmPartial(array, 3, 7, Compare);
				int[] expected = { 9, 8, 7, /*|*/ 2, 3, 4, 5, 6, /*|*/ 1, 0 };
				Assert.IsTrue(Equate<int>(array, expected));
			}

			{ // Partial Array Sort
				int[] array = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithmPartial(array, 4, 8, Compare);
				int[] expected = { 10, 9, 8, 7, /*|*/ 2, 3, 4, 5, 6, /*|*/ 1, 0 };
				Assert.IsTrue(Equate<int>(array, expected));
			}

			if (sizeOverride is null)
			{ // Partial Array Sort
				Random random = new(SortRandomSeed);
				int[] array = (1000..0).ToArray();
				Shuffle<int>(array.AsSpan(10..990), random);
				algorithmPartial(array, 10, 990, Compare);
				int[] expected = (1000..990).ToArray().Concat((10..991).ToArray()).Concat((9..0).ToArray()).ToArray();
				Assert.IsTrue(Equate<int>(array, expected));
			}
		}

		public delegate void SortSpan<T>(Span<T> span);

		public static void TestSortAlgorithmSpan(
			SortSpan<int> algorithm,
			int? sizeOverride = null)
		{
			{
				Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				SortTim<int, Int32Compare>(span);
				Assert.IsTrue(IsOrdered<int>(span));
			}
			{
				Span<int> span = new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				SortTim<int, Int32Compare>(span);
				Assert.IsTrue(IsOrdered<int>(span));
			}
			{
				if (!sizeOverride.HasValue || sizeOverride.Value > 1000)
				{
					Random random = new(7);
					var array = (..1000).ToArray();
					Shuffle<int>(array, random);
					algorithm(array);
					Assert.IsTrue(IsOrdered<int>(array));
				}
			}
		}

		[TestMethod]
		public void Shuffle_Testing()
		{
			Random random = new(SortRandomSeed);
			int[] array = new int[SortSize];
			Extensions.Iterate(SortSize, i => array[i] = i);
			Shuffle<int>(array, random);
			Assert.IsFalse(IsOrdered<int>(array));
		}

		[TestMethod]
		public void Bubble_Testing() => TestSortAlgorithm(
			(array, compare) => SortBubble<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortBubble<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Insertion_Testing() => TestSortAlgorithm(
			(array, compare) => SortInsertion<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortInsertion<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Selection_Testing() => TestSortAlgorithm(
			(array, compare) => SortSelection<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortSelection<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Merge_Testing() => TestSortAlgorithm(
			(array, compare) => SortMerge<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortMerge<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Quick_Testing() => TestSortAlgorithm(
			(array, compare) => SortQuick<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortQuick<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Heap_Testing() => TestSortAlgorithm(
			(array, compare) => SortHeap<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortHeap<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void OddEven_Testing() => TestSortAlgorithm(
			(array, compare) => SortOddEven<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortOddEven<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Slow_Testing() => TestSortAlgorithm(
			(array, compare) => SortSlow<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortSlow<int>(start, end, i => array[i], (i, v) => array[i] = v, compare),
			10);

		[TestMethod]
		public void Gnome_Testing() => TestSortAlgorithm(
			(array, compare) => SortGnome<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortGnome<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Comb_Testing() => TestSortAlgorithm(
			(array, compare) => SortComb<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortComb<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Shell_Testing() => TestSortAlgorithm(
			(array, compare) => SortShell<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortShell<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Cocktail_Testing() => TestSortAlgorithm(
			(array, compare) => SortCocktail<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortCocktail<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Cycle_Testing() => TestSortAlgorithm(
			(array, compare) => SortCycle<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortCycle<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void Pancake_Testing() => TestSortAlgorithm(
			(array, compare) => SortPancake(array, compare),
			(array, start, end, compare) => Assert.Inconclusive());

		[TestMethod]
		public void Stooge_Testing() => TestSortAlgorithm(
			(array, compare) => SortStooge(array, compare),
			(array, start, end, compare) => Assert.Inconclusive());

		[TestMethod]
		public void Bogo_Testing() => TestSortAlgorithm(
			(array, compare) => SortBogo(array, compare),
			(array, start, end, compare) => SortBogo<int>(start, end, i => array[i], (i, v) => array[i] = v, compare),
			6);

		[TestMethod]
		public void Tim_Testing() => TestSortAlgorithm(
			(array, compare) => SortTim<int>(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, compare),
			(array, start, end, compare) => SortTim<int>(start, end, i => array[i], (i, v) => array[i] = v, compare));

		[TestMethod]
		public void BubbleSpan_Test() => TestSortAlgorithmSpan(x => SortBubble<int, Int32Compare>(x));

		[TestMethod]
		public void InsertionSpan_Test() => TestSortAlgorithmSpan(x => SortInsertion<int, Int32Compare>(x));

		[TestMethod]
		public void SelectionSpan_Test() => TestSortAlgorithmSpan(x => SortInsertion<int, Int32Compare>(x));

		[TestMethod]
		public void MergeSpan_Test() => TestSortAlgorithmSpan(x => SortMerge<int, Int32Compare>(x));

		[TestMethod]
		public void QuickSpan_Test() => TestSortAlgorithmSpan(x => SortQuick<int, Int32Compare>(x));

		[TestMethod]
		public void HeapSpan_Test() => TestSortAlgorithmSpan(x => SortHeap<int, Int32Compare>(x));

		[TestMethod]
		public void OddEvenSpan_Test() => TestSortAlgorithmSpan(x => SortOddEven<int, Int32Compare>(x));

		[TestMethod]
		public void SlowSpan_Test() => TestSortAlgorithmSpan(x => SortSlow<int, Int32Compare>(x), 10);

		[TestMethod]
		public void GnomeSpan_Test() => TestSortAlgorithmSpan(x => SortGnome<int, Int32Compare>(x));

		[TestMethod]
		public void CombSpan_Test() => TestSortAlgorithmSpan(x => SortComb<int, Int32Compare>(x));

		[TestMethod]
		public void ShellSpan_Test() => TestSortAlgorithmSpan(x => SortShell<int, Int32Compare>(x));

		[TestMethod]
		public void CocktailSpan_Test() => TestSortAlgorithmSpan(x => SortCocktail<int, Int32Compare>(x));

		[TestMethod]
		public void CycleSpan_Test() => TestSortAlgorithmSpan(x => SortCycle<int, Int32Compare>(x));

		[TestMethod]
		public void BogoSpan_Test() => TestSortAlgorithmSpan(x => SortBogo<int, Int32Compare>(x), 6);

		[TestMethod]
		public void TimSpan_Test() => TestSortAlgorithmSpan(x => SortTim<int, Int32Compare>(x));

		#endregion

		#region IsPalindrome

		[TestMethod] public void IsPalindrome_Span_Testing()
		{
			// char----------------

			// odd
			Assert.IsTrue(IsPalindrome("a"));
			Assert.IsTrue(IsPalindrome("aaa"));
			Assert.IsTrue(IsPalindrome("bab"));
			Assert.IsTrue(IsPalindrome("ababa"));
			Assert.IsTrue(IsPalindrome("cbabc"));
			// even
			Assert.IsTrue(IsPalindrome("aa"));
			Assert.IsTrue(IsPalindrome("aabbaa"));
			Assert.IsTrue(IsPalindrome("aabbccbbaa"));
			// false
			Assert.IsFalse(IsPalindrome("ab"));
			Assert.IsFalse(IsPalindrome("abc"));
			Assert.IsFalse(IsPalindrome("abaa"));
			Assert.IsFalse(IsPalindrome("aaba"));
			Assert.IsFalse(IsPalindrome("baaa"));
			Assert.IsFalse(IsPalindrome("aaab"));

			// non-char------------

			// odd
			Assert.IsTrue(IsPalindrome<int>(new[] { 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1, 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 2, 1, 2 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 2, 1, 2, 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 3, 2, 1, 2, 3 }));
			// even
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1, 2, 2, 1, 1, }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1, 2, 2, 3, 3, 2, 2, 1, 1, }));
			// false
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 2 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 2, 3 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 2, 1, 1 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 1, 2, 1 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 2, 1, 1, 1 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 1, 1, 2 }));
		}

		[TestMethod] public void IsPalindrome_NonSpan_Testing()
		{
			// char-----------------

			// odd
			string a = "a";
			Assert.IsTrue(IsPalindrome(0, a.Length - 1, i => a[i]));
			string aaa = "aaa";
			Assert.IsTrue(IsPalindrome(0, aaa.Length - 1, i => aaa[i]));
			string bab = "bab";
			Assert.IsTrue(IsPalindrome(0, bab.Length - 1, i => bab[i]));
			string ababa = "ababa";
			Assert.IsTrue(IsPalindrome(0, ababa.Length - 1, i => ababa[i]));
			string cbabc = "cbabc";
			Assert.IsTrue(IsPalindrome(0, cbabc.Length - 1, i => cbabc[i]));
			// even
			string aa = "aa";
			Assert.IsTrue(IsPalindrome(0, aa.Length - 1, i => aa[i]));
			string aabbaa = "aabbaa";
			Assert.IsTrue(IsPalindrome(0, aabbaa.Length - 1, i => aabbaa[i]));
			string aabbccbbaa = "aabbccbbaa";
			Assert.IsTrue(IsPalindrome(0, aabbccbbaa.Length - 1, i => aabbccbbaa[i]));
			// false
			string ab = "ab";
			Assert.IsFalse(IsPalindrome(0, ab.Length - 1, i => ab[i]));
			string abc = "abc";
			Assert.IsFalse(IsPalindrome(0, abc.Length - 1, i => abc[i]));
			string abaa = "abaa";
			Assert.IsFalse(IsPalindrome(0, abaa.Length - 1, i => abaa[i]));
			string aaba = "aaba";
			Assert.IsFalse(IsPalindrome(0, aaba.Length - 1, i => aaba[i]));
			string baaa = "baaa";
			Assert.IsFalse(IsPalindrome(0, baaa.Length - 1, i => baaa[i]));
			string aaab = "aaab";
			Assert.IsFalse(IsPalindrome(0, aaab.Length - 1, i => aaab[i]));
			// partials
			string bbb = "bbb";
			Assert.IsTrue(IsPalindrome(1, bbb.Length - 2, i => bbb[i]));
			string babab = "babab";
			Assert.IsTrue(IsPalindrome(1, babab.Length - 2, i => babab[i]));
			string babb = "babb";
			Assert.IsFalse(IsPalindrome(1, babb.Length - 2, i => babb[i]));
			string babbb = "babbb";
			Assert.IsFalse(IsPalindrome(1, babbb.Length - 2, i => babbb[i]));

			// non char-------------

			// odd
			int[] _1 = { 1 };
			Assert.IsTrue(IsPalindrome(0, _1.Length - 1, i => _1[i]));
			int[] _1_1_1 = { 1, 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1_1.Length - 1, i => _1_1_1[i]));
			int[] _2_1_2 = { 2, 1, 2 };
			Assert.IsTrue(IsPalindrome(0, _2_1_2.Length - 1, i => _2_1_2[i]));
			int[] _1_2_1_2_1 = { 1, 2, 1, 2, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_2_1_2_1.Length - 1, i => _1_2_1_2_1[i]));
			int[] _3_2_1_2_3 = { 3, 2, 1, 2, 3 };
			Assert.IsTrue(IsPalindrome(0, _3_2_1_2_3.Length - 1, i => _3_2_1_2_3[i]));
			// even
			int[] _1_1 = { 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1.Length - 1, i => _1_1[i]));
			int[] _1_1_2_2_1_1 = { 1, 1, 2, 2, 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1_2_2_1_1.Length - 1, i => _1_1_2_2_1_1[i]));
			int[] _1_1_2_2_3_3_2_2_1_1 = { 1, 1, 2, 2, 3, 3, 2, 2, 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1_2_2_3_3_2_2_1_1.Length - 1, i => _1_1_2_2_3_3_2_2_1_1[i]));
			// false
			int[] _1_2 = { 1, 2 };
			Assert.IsFalse(IsPalindrome(0, _1_2.Length - 1, i => _1_2[i]));
			int[] _1_2_3 = { 1, 2, 3 };
			Assert.IsFalse(IsPalindrome(0, _1_2_3.Length - 1, i => _1_2_3[i]));
			int[] _1_2_1_1 = { 1, 2, 1, 1 };
			Assert.IsFalse(IsPalindrome(0, _1_2_1_1.Length - 1, i => _1_2_1_1[i]));
			int[] _1_1_2_1 = { 1, 1, 2, 1 };
			Assert.IsFalse(IsPalindrome(0, _1_1_2_1.Length - 1, i => _1_1_2_1[i]));
			int[] _2_1_1_1 = { 2, 1, 1, 1 };
			Assert.IsFalse(IsPalindrome(0, _2_1_1_1.Length - 1, i => _2_1_1_1[i]));
			int[] _1_1_1_2 = { 1, 1, 1, 2 };
			Assert.IsFalse(IsPalindrome(0, _1_1_1_2.Length - 1, i => _1_1_1_2[i]));
			// partials
			int[] _2_2_2 = { 2, 2, 2 };
			Assert.IsTrue(IsPalindrome(1, _2_2_2.Length - 2, i => _2_2_2[i]));
			int[] _2_1_2_1_2 = { 2, 1, 2, 1, 2 };
			Assert.IsTrue(IsPalindrome(1, _2_1_2_1_2.Length - 2, i => _2_1_2_1_2[i]));
			int[] _2_1_2_2 = { 2, 1, 2, 2 };
			Assert.IsFalse(IsPalindrome(1, _2_1_2_2.Length - 2, i => _2_1_2_2[i]));
			int[] _2_1_2_2_2 = { 2, 1, 2, 2, 2 };
			Assert.IsFalse(IsPalindrome(1, _2_1_2_2_2.Length - 2, i => _2_1_2_2_2[i]));
		}

		#endregion

		#region IsInterleaved

		[TestMethod] public void IsInterleavedRecursive_Testing()
		{
			Assert.IsTrue(IsInterleavedRecursive("a", "z", "az"));
			Assert.IsTrue(IsInterleavedRecursive("ab", "yz", "aybz"));
			Assert.IsTrue(IsInterleavedRecursive("abc", "xyz", "axbycz"));
			Assert.IsTrue(IsInterleavedRecursive("abcd", "wxyz", "awbxcydz"));

			Assert.IsTrue(IsInterleavedRecursive("a", "z", "za"));
			Assert.IsTrue(IsInterleavedRecursive("ab", "yz", "yazb"));
			Assert.IsTrue(IsInterleavedRecursive("abc", "xyz", "xaybzc"));
			Assert.IsTrue(IsInterleavedRecursive("abcd", "wxyz", "waxbyczd"));

			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "aazz"));
			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "azaz"));
			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "zaza"));
			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "zzaa"));

			Assert.IsTrue(IsInterleavedRecursive("", "", ""));

			Assert.IsFalse(IsInterleavedRecursive("a", "", ""));
			Assert.IsFalse(IsInterleavedRecursive("", "a", ""));
			Assert.IsFalse(IsInterleavedRecursive("", "", "a"));
			Assert.IsFalse(IsInterleavedRecursive("a", "a", ""));
			Assert.IsFalse(IsInterleavedRecursive("a", "a", "aaa"));
		}

		[TestMethod] public void IsInterleavedIterative_Testing()
		{
			Assert.IsTrue(IsInterleavedIterative("a", "z", "az"));
			Assert.IsTrue(IsInterleavedIterative("ab", "yz", "aybz"));
			Assert.IsTrue(IsInterleavedIterative("abc", "xyz", "axbycz"));
			Assert.IsTrue(IsInterleavedRecursive("abcd", "wxyz", "awbxcydz"));

			Assert.IsTrue(IsInterleavedIterative("a", "z", "za"));
			Assert.IsTrue(IsInterleavedIterative("ab", "yz", "yazb"));
			Assert.IsTrue(IsInterleavedIterative("abc", "xyz", "xaybzc"));
			Assert.IsTrue(IsInterleavedIterative("abcd", "wxyz", "waxbyczd"));

			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "aazz"));
			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "azaz"));
			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "zaza"));
			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "zzaa"));

			Assert.IsTrue(IsInterleavedIterative("", "", ""));

			Assert.IsFalse(IsInterleavedIterative("a", "", ""));
			Assert.IsFalse(IsInterleavedIterative("", "a", ""));
			Assert.IsFalse(IsInterleavedIterative("", "", "a"));
			Assert.IsFalse(IsInterleavedIterative("a", "a", ""));
			Assert.IsFalse(IsInterleavedIterative("a", "a", "aaa"));
		}

		#endregion

		#region IsReorderOf

		[TestMethod] public void IsReorderOf_Testing()
		{
			Assert.IsTrue(IsReorderOf<char>("a", "a"));
			Assert.IsTrue(IsReorderOf<char>("ab", "ba"));
			Assert.IsTrue(IsReorderOf<char>("abc", "cba"));

			Assert.IsTrue(IsReorderOf<char>("aab", "baa"));
			Assert.IsTrue(IsReorderOf<char>("aab", "aba"));
			Assert.IsTrue(IsReorderOf<char>("aab", "aab"));

			Assert.IsTrue(IsReorderOf<char>("aabb", "bbaa"));
			Assert.IsTrue(IsReorderOf<char>("aabb", "abab"));
			Assert.IsTrue(IsReorderOf<char>("aabb", "abba"));
			Assert.IsTrue(IsReorderOf<char>("aabb", "aabb"));

			Assert.IsFalse(IsReorderOf<char>("a", "b"));
			Assert.IsFalse(IsReorderOf<char>("aa", "bb"));
			Assert.IsFalse(IsReorderOf<char>("ab", "aa"));
			Assert.IsFalse(IsReorderOf<char>("ab", "bb"));

			Assert.IsFalse(IsReorderOf<char>("aa", "a"));
			Assert.IsFalse(IsReorderOf<char>("a", "aa"));
			Assert.IsFalse(IsReorderOf<char>("ab", "aab"));
			Assert.IsFalse(IsReorderOf<char>("aab", "ab"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aaabbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aabbbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aabbccc"));
			Assert.IsFalse(IsReorderOf<char>("aaabbcc", "aabbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbbcc", "aabbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbccc", "aabbcc"));

			Assert.IsFalse(IsReorderOf<char>("aabb", "aaab"));
			Assert.IsFalse(IsReorderOf<char>("bbaa", "aaab"));

			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aaabcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "abbbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aabccc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "abbccc"));

			Assert.IsFalse(IsReorderOf<char>("a", ""));
			Assert.IsFalse(IsReorderOf<char>("", "a"));
			Assert.IsFalse(IsReorderOf<char>(null, "a"));
			Assert.IsFalse(IsReorderOf<char>("a", null));

			Assert.IsTrue(IsReorderOf<char>(null, null));
			Assert.IsTrue(IsReorderOf<char>("", ""));
			Assert.IsTrue(IsReorderOf<char>(null, ""));
			Assert.IsTrue(IsReorderOf<char>("", null));
		}

		#endregion

		#region SetEquals

		[TestMethod] public void SetEquals_Testing()
		{
			Assert.IsTrue(SetEquals<char>("a", "a"));
			Assert.IsTrue(SetEquals<char>("ab", "ba"));
			Assert.IsTrue(SetEquals<char>("abc", "cba"));

			Assert.IsTrue(SetEquals<char>("aab", "baa"));
			Assert.IsTrue(SetEquals<char>("aab", "aba"));
			Assert.IsTrue(SetEquals<char>("aab", "aab"));

			Assert.IsTrue(SetEquals<char>("aabb", "bbaa"));
			Assert.IsTrue(SetEquals<char>("aabb", "abab"));
			Assert.IsTrue(SetEquals<char>("aabb", "abba"));
			Assert.IsTrue(SetEquals<char>("aabb", "aabb"));

			Assert.IsFalse(SetEquals<char>("a", "b"));
			Assert.IsFalse(SetEquals<char>("aa", "bb"));
			Assert.IsFalse(SetEquals<char>("ab", "aa"));
			Assert.IsFalse(SetEquals<char>("ab", "bb"));

			Assert.IsTrue(SetEquals<char>("aa", "a"));
			Assert.IsTrue(SetEquals<char>("a", "aa"));
			Assert.IsTrue(SetEquals<char>("ab", "aab"));
			Assert.IsTrue(SetEquals<char>("aab", "ab"));
			Assert.IsTrue(SetEquals<char>("aabbcc", "aaabbcc"));
			Assert.IsTrue(SetEquals<char>("aabbcc", "aabbbcc"));
			Assert.IsTrue(SetEquals<char>("aabbcc", "aabbccc"));
			Assert.IsTrue(SetEquals<char>("aaabbcc", "aabbcc"));
			Assert.IsTrue(SetEquals<char>("aabbbcc", "aabbcc"));
			Assert.IsTrue(SetEquals<char>("aabbccc", "aabbcc"));

			Assert.IsTrue(SetEquals<char>("aabb", "aaab"));
			Assert.IsTrue(SetEquals<char>("bbaa", "aaab"));

			Assert.IsTrue(SetEquals<char>("aabbcc", "aaabcc"));
			Assert.IsTrue(SetEquals<char>("aabbcc", "abbbcc"));
			Assert.IsTrue(SetEquals<char>("aabbcc", "aabccc"));
			Assert.IsTrue(SetEquals<char>("aabbcc", "abbccc"));

			Assert.IsFalse(SetEquals<char>("a", ""));
			Assert.IsFalse(SetEquals<char>("", "a"));
			Assert.IsFalse(SetEquals<char>(null, "a"));
			Assert.IsFalse(SetEquals<char>("a", null));

			Assert.IsTrue(SetEquals<char>(null, null));
			Assert.IsTrue(SetEquals<char>("", ""));
			Assert.IsTrue(SetEquals<char>(null, ""));
			Assert.IsTrue(SetEquals<char>("", null));

			Assert.IsTrue(SetEquals<int>(Array.Empty<int>(), Array.Empty<int>()));
			Assert.IsTrue(SetEquals<int>(new[] { 1 }, new[] { 1 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2 }, new[] { 1, 2 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2 }, new[] { 2, 1 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2, 3 }, new[] { 3, 2, 1 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2, 3 }, new[] { 2, 1, 3 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2, 3 }, new[] { 2, 3, 1 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2, 3 }, new[] { 1, 3, 2 }));
			Assert.IsTrue(SetEquals<int>(new[] { 1, 2, 3 }, new[] { 3, 1, 2 }));
		}

		#endregion

		#region ContainsDuplicates

		[TestMethod] public void ContainsDuplicates_Testing()
		{
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { 0 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { 1 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { -1 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { 0, 1 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { -1, 0 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { -1, 0, 1 }));
		}

		#endregion

		#region Contains

		[TestMethod] public void Contains_Testing()
		{
			{
				Span<int> span = stackalloc int[] { };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(stackalloc int[] { }, 0));
				Assert.IsFalse(Contains(stackalloc int[] { }, 1));
			}
			{
				Span<int> span = stackalloc int[] { 1, };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(span, 0));
				Assert.IsTrue(Contains(span, 1));
				Assert.IsFalse(Contains(span, 2));
			}
			{
				Span<int> span = stackalloc int[] { 1, 2, };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(span, 0));
				Assert.IsTrue(Contains(span, 1));
				Assert.IsTrue(Contains(span, 2));
				Assert.IsFalse(Contains(span, 3));
			}
			{
				Span<int> span = stackalloc int[] { 1, 2, 3, };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(span, 0));
				Assert.IsTrue(Contains(span, 1));
				Assert.IsTrue(Contains(span, 2));
				Assert.IsTrue(Contains(span, 3));
				Assert.IsFalse(Contains(span, 4));
			}
		}

		#endregion

		#region Any

		[TestMethod] public void Any_Testing()
		{
			Assert.IsFalse(Any(stackalloc int[] { }, i => true));
			Assert.IsFalse(Any(stackalloc int[] { }, i => false));

			Assert.IsTrue(Any(stackalloc int[] { 0 }, i => true));
			Assert.IsFalse(Any(stackalloc int[] { 0 }, i => false));
			Assert.IsTrue(Any(stackalloc int[] { 0 }, i => i is 0));

			Assert.IsTrue(Any(stackalloc int[] { 0, 1 }, i => true));
			Assert.IsFalse(Any(stackalloc int[] { 0, 1 }, i => false));
			Assert.IsFalse(Any(stackalloc int[] { 0, 1 }, i => i is -1));
			Assert.IsTrue(Any(stackalloc int[] { 0, 1 }, i => i is 0));
			Assert.IsTrue(Any(stackalloc int[] { 0, 1 }, i => i is 1));
			Assert.IsFalse(Any(stackalloc int[] { 0, 1 }, i => i is 2));
		}

		#endregion

		#region NextUniqueRollTracking

		[TestMethod]
		public void NextUniqueRollTracking_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			{
				Func<int, int, int> next = (_, max) => -5; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, -5, 0, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { -5, -4, -3, -2, -1, }));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => -500; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, -500, 0, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < -500 || i >= 0));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(-1, 0, 1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(1, 0, -1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 1, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region NextUniquePoolTracking (with exclusions)

		[TestMethod]
		public void NextUniquePoolTracking_exclusions_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 1, 3, 5, 7, 9, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 2, 4, 6, 8, }));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 0, 2, 4, 6, 8, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 1, 3, 5, 7, 9, }));
			}
			{
				Random random = new();
				const int minValue = 0;
				const int maxValue = 1000;
				const int count = 6;
				for (int i = 0; i < 1000; i++)
				{
					const int excludeCount = 100;
					int[] excludes = new int[excludeCount];
					for (int j = 0; j < excludeCount; j++)
					{
						excludes[j] = random.Next(minValue, maxValue);
					}
					int[] output = random.NextUniquePoolTracking(
						count: count,
						minValue: minValue,
						maxValue: maxValue,
						excluded: excludes);
					Assert.IsTrue(output.Length is count);
					Assert.IsTrue(!ContainsDuplicates<int>(output));
					Assert.IsTrue(!Any<int>(output, value => value < minValue));
					Assert.IsTrue(!Any<int>(output, value => value >= maxValue));
					Assert.IsTrue(!Any<int>(output, value => Contains(excludes, value)));
				}
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(-1, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(1, 0, -1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 2, excluded: new[] { 0 }, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region NextUniquePoolTracking (with exclusions)

		[TestMethod]
		public void NextUniqueRollTracking_exclusions_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 1, 3, 5, 7, 9, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 2, 4, 6, 8, }));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 0, 2, 4, 6, 8, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 1, 3, 5, 7, 9, }));
			}
			{
				Random random = new();
				const int minValue = 0;
				const int maxValue = 1000;
				const int count = 6;
				for (int i = 0; i < 1000; i++)
				{
					const int excludeCount = 100;
					int[] excludes = new int[excludeCount];
					for (int j = 0; j < excludeCount; j++)
					{
						excludes[j] = random.Next(minValue, maxValue);
					}
					int[] output = random.NextUniqueRollTracking(
						count: count,
						minValue: minValue,
						maxValue: maxValue,
						excluded: excludes);
					Assert.IsTrue(output.Length is count);
					Assert.IsTrue(!ContainsDuplicates<int>(output));
					Assert.IsTrue(!Any<int>(output, value => value < minValue));
					Assert.IsTrue(!Any<int>(output, value => value >= maxValue));
					Assert.IsTrue(!Any<int>(output, value => Contains(excludes, value)));
				}
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(-1, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(1, 0, -1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 2, excluded: new[] { 0 }, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region NextUniquePoolTracking

		[TestMethod]
		public void NextUniquePoolTracking_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, -5, 0, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { -5, -4, -3, -2, -1, }));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, -500, 0, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < -500 || i >= 0));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniquePoolTracking<SFunc<int, int, int>>(-1, 0, 1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniquePoolTracking<SFunc<int, int, int>>(1, 0, -1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniquePoolTracking<SFunc<int, int, int>>(2, 0, 1, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region CombineRanges

		[TestMethod] public void CombineRanges_Testing()
		{
			{
				Assert.IsTrue(SetEquals<(int, int)>(Array.Empty<(int, int)>(), Array.Empty<(int, int)>()));
			}
			{
				(int, int)[] a = new[]
				{
					(1,   5),
					(4,   7),
					(15, 18),
					(3,  10),
				};
				(int, int)[] b = new[]
				{
					(1,  10),
					(15, 18),
				};
				Assert.IsTrue(SetEquals<(int, int)>(CombineRanges(a).ToArray(), b));
			}
			{
				(DateTime, DateTime)[] a =
				{
					(new DateTime(2000, 1, 1), new DateTime(2002, 1, 1)),
					(new DateTime(2000, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2003, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2011, 1, 1), new DateTime(2016, 1, 1)),
				};
				(DateTime, DateTime)[] b =
				{
					(new DateTime(2000, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2011, 1, 1), new DateTime(2016, 1, 1)),
				};
				Assert.IsTrue(SetEquals<(DateTime, DateTime)>(CombineRanges(a).ToArray(), b));
			}
			{
				(string, string)[] a = new[]
				{
					("tux", "zebra"),
					("a",   "hippo"),
					("boy", "joust"),
					("car", "dog"),
				};
				(string, string)[] b = new[]
				{
					("a", "joust"),
					("tux", "zebra"),
				};
				Assert.IsTrue(SetEquals<(string, string)>(CombineRanges(a).ToArray(), b));
			}
		}

		#endregion

		#region Zip

#if false

		[TestMethod] public void Zip_Testing()
		{
			{
				int[] a = { 1, 2, 3 };
				string[] b = { "one", "two", "three" };
				(int, string)[] c = { (a[0], b[0]), (a[1], b[1]), (a[2], b[2]) };
				Assert.IsTrue(Zip(a, b).SequenceEqual(c));
				Assert.ThrowsException<ArgumentNullException>(() => Zip(default(int[]), b).ToList());
				Assert.ThrowsException<ArgumentNullException>(() => Zip(a, default(string[])).ToList());
				Assert.ThrowsException<ArgumentNullException>(() => Zip(default(int[]), default(string[])).ToList());
			}
			{
				int[] a = { 1, 2 };
				string[] b = { "one", "two", "three" };
				Assert.ThrowsException<ArgumentException>(() => Zip(a, b).ToList());
			}
			{
				int[] a = { 1, 2, 3 };
				string[] b = { "one", "two" };
				Assert.ThrowsException<ArgumentException>(() => Zip(a, b).ToList());
			}
		}

#endif

		#endregion

		#region TryParseRomanNumeral

		[TestMethod]
		public void TryParseRomanNumeral_Testing()
		{
			Assert.IsTrue(TryParseRomanNumeral("I")       is (true,    1));
			Assert.IsTrue(TryParseRomanNumeral("II")      is (true,    2));
			Assert.IsTrue(TryParseRomanNumeral("III")     is (true,    3));
			Assert.IsTrue(TryParseRomanNumeral("IV")      is (true,    4));
			Assert.IsTrue(TryParseRomanNumeral("V")       is (true,    5));
			Assert.IsTrue(TryParseRomanNumeral("VI")      is (true,    6));
			Assert.IsTrue(TryParseRomanNumeral("VII")     is (true,    7));
			Assert.IsTrue(TryParseRomanNumeral("VIII")    is (true,    8));
			Assert.IsTrue(TryParseRomanNumeral("IX")      is (true,    9));
			Assert.IsTrue(TryParseRomanNumeral("X")       is (true,   10));
			Assert.IsTrue(TryParseRomanNumeral("XI")      is (true,   11));
			Assert.IsTrue(TryParseRomanNumeral("XII")     is (true,   12));
			Assert.IsTrue(TryParseRomanNumeral("XIII")    is (true,   13));
			Assert.IsTrue(TryParseRomanNumeral("XIV")     is (true,   14));
			Assert.IsTrue(TryParseRomanNumeral("XV")      is (true,   15));
			Assert.IsTrue(TryParseRomanNumeral("XVI")     is (true,   16));
			Assert.IsTrue(TryParseRomanNumeral("XVII")    is (true,   17));
			Assert.IsTrue(TryParseRomanNumeral("XVIII")   is (true,   18));
			Assert.IsTrue(TryParseRomanNumeral("XIX")     is (true,   19));
			Assert.IsTrue(TryParseRomanNumeral("XX")      is (true,   20));
			Assert.IsTrue(TryParseRomanNumeral("XXI")     is (true,   21));
			Assert.IsTrue(TryParseRomanNumeral("XXII")    is (true,   22));
			Assert.IsTrue(TryParseRomanNumeral("XXIII")   is (true,   23));
			Assert.IsTrue(TryParseRomanNumeral("XXIV")    is (true,   24));
			Assert.IsTrue(TryParseRomanNumeral("XXV")     is (true,   25));
			Assert.IsTrue(TryParseRomanNumeral("XXVI")    is (true,   26));
			Assert.IsTrue(TryParseRomanNumeral("XXVII")   is (true,   27));
			Assert.IsTrue(TryParseRomanNumeral("XXVIII")  is (true,   28));
			Assert.IsTrue(TryParseRomanNumeral("XXIX")    is (true,   29));
			Assert.IsTrue(TryParseRomanNumeral("XXX")     is (true,   30));
			Assert.IsTrue(TryParseRomanNumeral("XXXI")    is (true,   31));
			Assert.IsTrue(TryParseRomanNumeral("XXXII")   is (true,   32));
			Assert.IsTrue(TryParseRomanNumeral("XXXIII")  is (true,   33));
			Assert.IsTrue(TryParseRomanNumeral("XXXIV")   is (true,   34));
			Assert.IsTrue(TryParseRomanNumeral("XXXV")    is (true,   35));
			Assert.IsTrue(TryParseRomanNumeral("XXXVI")   is (true,   36));
			Assert.IsTrue(TryParseRomanNumeral("XXXVII")  is (true,   37));
			Assert.IsTrue(TryParseRomanNumeral("XXXVIII") is (true,   38));
			Assert.IsTrue(TryParseRomanNumeral("XXXIX")   is (true,   39));
			Assert.IsTrue(TryParseRomanNumeral("XL")      is (true,   40));
			Assert.IsTrue(TryParseRomanNumeral("XLI")     is (true,   41));
			Assert.IsTrue(TryParseRomanNumeral("XLII")    is (true,   42));
			Assert.IsTrue(TryParseRomanNumeral("XLIII")   is (true,   43));
			Assert.IsTrue(TryParseRomanNumeral("XLIV")    is (true,   44));
			Assert.IsTrue(TryParseRomanNumeral("XLV")     is (true,   45));
			Assert.IsTrue(TryParseRomanNumeral("XLVI")    is (true,   46));
			Assert.IsTrue(TryParseRomanNumeral("XLVII")   is (true,   47));
			Assert.IsTrue(TryParseRomanNumeral("XLVIII")  is (true,   48));
			Assert.IsTrue(TryParseRomanNumeral("XLIX")    is (true,   49));
			Assert.IsTrue(TryParseRomanNumeral("L")       is (true,   50));
			Assert.IsTrue(TryParseRomanNumeral("C")       is (true,  100));
			Assert.IsTrue(TryParseRomanNumeral("D")       is (true,  500));
			Assert.IsTrue(TryParseRomanNumeral("M")       is (true, 1000));

			Assert.IsTrue(TryParseRomanNumeral(null)  is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("")    is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("a")   is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("aI")  is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("Ia")  is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("aIa") is (false, default(int)));
		}

		#endregion

		#region TryToRomanNumeral

		[TestMethod]
		public void TryToRomanNumeral_Testing()
		{
			Assert.IsTrue(TryToRomanNumeral(   1) is (true,       "I"));
			Assert.IsTrue(TryToRomanNumeral(   2) is (true,      "II"));
			Assert.IsTrue(TryToRomanNumeral(   3) is (true,     "III"));
			Assert.IsTrue(TryToRomanNumeral(   4) is (true,      "IV"));
			Assert.IsTrue(TryToRomanNumeral(   5) is (true,       "V"));
			Assert.IsTrue(TryToRomanNumeral(   6) is (true,      "VI"));
			Assert.IsTrue(TryToRomanNumeral(   7) is (true,     "VII"));
			Assert.IsTrue(TryToRomanNumeral(   8) is (true,    "VIII"));
			Assert.IsTrue(TryToRomanNumeral(   9) is (true,      "IX"));
			Assert.IsTrue(TryToRomanNumeral(  10) is (true,       "X"));
			Assert.IsTrue(TryToRomanNumeral(  11) is (true,      "XI"));
			Assert.IsTrue(TryToRomanNumeral(  12) is (true,     "XII"));
			Assert.IsTrue(TryToRomanNumeral(  13) is (true,    "XIII"));
			Assert.IsTrue(TryToRomanNumeral(  14) is (true,     "XIV"));
			Assert.IsTrue(TryToRomanNumeral(  15) is (true,      "XV"));
			Assert.IsTrue(TryToRomanNumeral(  16) is (true,     "XVI"));
			Assert.IsTrue(TryToRomanNumeral(  17) is (true,    "XVII"));
			Assert.IsTrue(TryToRomanNumeral(  18) is (true,   "XVIII"));
			Assert.IsTrue(TryToRomanNumeral(  19) is (true,     "XIX"));
			Assert.IsTrue(TryToRomanNumeral(  20) is (true,      "XX"));
			Assert.IsTrue(TryToRomanNumeral(  21) is (true,     "XXI"));
			Assert.IsTrue(TryToRomanNumeral(  22) is (true,    "XXII"));
			Assert.IsTrue(TryToRomanNumeral(  23) is (true,   "XXIII"));
			Assert.IsTrue(TryToRomanNumeral(  24) is (true,    "XXIV"));
			Assert.IsTrue(TryToRomanNumeral(  25) is (true,     "XXV"));
			Assert.IsTrue(TryToRomanNumeral(  26) is (true,    "XXVI"));
			Assert.IsTrue(TryToRomanNumeral(  27) is (true,   "XXVII"));
			Assert.IsTrue(TryToRomanNumeral(  28) is (true,  "XXVIII"));
			Assert.IsTrue(TryToRomanNumeral(  29) is (true,    "XXIX"));
			Assert.IsTrue(TryToRomanNumeral(  30) is (true,     "XXX"));
			Assert.IsTrue(TryToRomanNumeral(  31) is (true,    "XXXI"));
			Assert.IsTrue(TryToRomanNumeral(  32) is (true,   "XXXII"));
			Assert.IsTrue(TryToRomanNumeral(  33) is (true,  "XXXIII"));
			Assert.IsTrue(TryToRomanNumeral(  34) is (true,   "XXXIV"));
			Assert.IsTrue(TryToRomanNumeral(  35) is (true,    "XXXV"));
			Assert.IsTrue(TryToRomanNumeral(  36) is (true,   "XXXVI"));
			Assert.IsTrue(TryToRomanNumeral(  37) is (true,  "XXXVII"));
			Assert.IsTrue(TryToRomanNumeral(  38) is (true, "XXXVIII"));
			Assert.IsTrue(TryToRomanNumeral(  39) is (true,   "XXXIX"));
			Assert.IsTrue(TryToRomanNumeral(  40) is (true,      "XL"));
			Assert.IsTrue(TryToRomanNumeral(  41) is (true,     "XLI"));
			Assert.IsTrue(TryToRomanNumeral(  42) is (true,    "XLII"));
			Assert.IsTrue(TryToRomanNumeral(  43) is (true,   "XLIII"));
			Assert.IsTrue(TryToRomanNumeral(  44) is (true,    "XLIV"));
			Assert.IsTrue(TryToRomanNumeral(  45) is (true,     "XLV"));
			Assert.IsTrue(TryToRomanNumeral(  46) is (true,    "XLVI"));
			Assert.IsTrue(TryToRomanNumeral(  47) is (true,   "XLVII"));
			Assert.IsTrue(TryToRomanNumeral(  48) is (true,  "XLVIII"));
			Assert.IsTrue(TryToRomanNumeral(  49) is (true,    "XLIX"));
			Assert.IsTrue(TryToRomanNumeral(  50) is (true,       "L"));
			Assert.IsTrue(TryToRomanNumeral( 100) is (true,       "C"));
			Assert.IsTrue(TryToRomanNumeral( 500) is (true,       "D"));
			Assert.IsTrue(TryToRomanNumeral(1000) is (true,       "M"));

			Assert.IsTrue(TryToRomanNumeral(    0) is (false, null));
			Assert.IsTrue(TryToRomanNumeral( 4000) is (false, null));
			Assert.IsTrue(TryToRomanNumeral(   -1) is (false, null));
			Assert.IsTrue(TryToRomanNumeral(-3999) is (false, null));
		}

		#endregion

		#region RomanNumeralSynch

		[TestMethod]
		public void RomanNumeralSynch_Testing()
		{
			for (int i = 1; i < 4000; i++)
			{
				var (toSuccess, romanNumerals) = TryToRomanNumeral(i);
				Assert.IsTrue(toSuccess);
				var (fromSuccess, value) = TryParseRomanNumeral(romanNumerals);
				Assert.IsTrue(fromSuccess);
				Assert.IsTrue(value == i);
			}
		}

		#endregion

		#region IsOrdered

		[TestMethod]
		public void IsOrdered_Testing()
		{
			{
				Assert.IsTrue(IsOrdered<int>(Ɐ<int>()));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 2)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 2, 3)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 1)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 1, 2)));

				Assert.IsFalse(IsOrdered<int>(Ɐ(2, 1)));
				Assert.IsFalse(IsOrdered<int>(Ɐ(3, 1, 2)));
				Assert.IsFalse(IsOrdered<int>(Ɐ(1, 3, 2)));

				Assert.IsTrue(IsOrdered<char>(Ɐ<char>()));
				Assert.IsTrue(IsOrdered<char>("a"));
				Assert.IsTrue(IsOrdered<char>("ab"));
				Assert.IsTrue(IsOrdered<char>("abc"));
				Assert.IsTrue(IsOrdered<char>("aa"));
				Assert.IsTrue(IsOrdered<char>("aab"));

				Assert.IsFalse(IsOrdered<char>("ba"));
				Assert.IsFalse(IsOrdered<char>("cab"));
				Assert.IsFalse(IsOrdered<char>("acb"));
			}
			{
				Assert.IsTrue(Ɐ<int>().IsOrdered());
				Assert.IsTrue(Ɐ(1).IsOrdered());
				Assert.IsTrue(Ɐ(1, 2).IsOrdered());
				Assert.IsTrue(Ɐ(1, 2, 3).IsOrdered());
				Assert.IsTrue(Ɐ(1, 1).IsOrdered());
				Assert.IsTrue(Ɐ(1, 1, 2).IsOrdered());

				Assert.IsFalse(Ɐ(2, 1).IsOrdered());
				Assert.IsFalse(Ɐ(3, 1, 2).IsOrdered());
				Assert.IsFalse(Ɐ(1, 3, 2).IsOrdered());

				Assert.IsTrue(Ɐ<char>().IsOrdered());
				Assert.IsTrue("a".IsOrdered());
				Assert.IsTrue("ab".IsOrdered());
				Assert.IsTrue("abc".IsOrdered());
				Assert.IsTrue("aa".IsOrdered());
				Assert.IsTrue("aab".IsOrdered());

				Assert.IsFalse("ba".IsOrdered());
				Assert.IsFalse("cab".IsOrdered());
				Assert.IsFalse("acb".IsOrdered());
			}
			{
				int[] ints0 = Array.Empty<int>();
				Assert.IsTrue(IsOrdered(0, ints0.Length - 1, i => ints0[i]));
				int[] ints1 = { 1 };
				Assert.IsTrue(IsOrdered(0, ints1.Length - 1, i => ints1[i]));
				int[] ints2 = { 1, 2 };
				Assert.IsTrue(IsOrdered(0, ints2.Length - 1, i => ints2[i]));
				int[] ints3 = { 1, 2, 3 };
				Assert.IsTrue(IsOrdered(0, ints3.Length - 1, i => ints3[i]));

				int[] ints4 = { 2, 1 };
				Assert.IsFalse(IsOrdered(0, ints4.Length - 1, i => ints4[i]));
				int[] ints5 = { 3, 1, 2 };
				Assert.IsFalse(IsOrdered(0, ints5.Length - 1, i => ints5[i]));
				int[] ints6 = { 1, 3, 2 };
				Assert.IsFalse(IsOrdered(0, ints6.Length - 1, i => ints6[i]));

				string s0 = "";
				Assert.IsTrue(IsOrdered(0, s0.Length - 1, i => s0[i]));
				string s1 = "a";
				Assert.IsTrue(IsOrdered(0, ints1.Length - 1, i => s1[i]));
				string s2 = "ab";
				Assert.IsTrue(IsOrdered(0, s2.Length - 1, i => s2[i]));
				string s3 = "abc";
				Assert.IsTrue(IsOrdered(0, s3.Length - 1, i => s3[i]));

				string s4 = "ba";
				Assert.IsFalse(IsOrdered(0, s4.Length - 1, i => s4[i]));
				string s5 = "cab";
				Assert.IsFalse(IsOrdered(0, s5.Length - 1, i => s5[i]));
				string s6 = "acb";
				Assert.IsFalse(IsOrdered(0, s6.Length - 1, i => s6[i]));
			}
		}

		#endregion

		#region FilterOrdered

		[TestMethod]
		public void FilterOrdered_Testing()
		{
			Assert.IsTrue(Equate<int>(Ɐ<int>().FilterOrdered().ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>(Ɐ(1).FilterOrdered().ToArray(), Ɐ(1)));
			Assert.IsTrue(Equate<int>(Ɐ(1, 2).FilterOrdered().ToArray(), Ɐ(1, 2)));
			Assert.IsTrue(Equate<int>(Ɐ(2, 1).FilterOrdered().ToArray(), Ɐ(2)));
			Assert.IsTrue(Equate<int>(Ɐ(1, 2, 3).FilterOrdered().ToArray(), Ɐ(1, 2, 3)));
			Assert.IsTrue(Equate<int>(Ɐ(1, -1, 2, -2, 3).FilterOrdered().ToArray(), Ɐ(1, 2, 3)));
		}

		#endregion

		#region GetGreatest

		[TestMethod]
		public void GetGreatest_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => GetGreatest(Ɐ<int>(), 1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest(Ɐ(1), 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest(Ɐ(1), -1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest(Ɐ(1), 2));

			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1, 2), 1), Ɐ(2)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(2, 1), 1), Ɐ(2)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1, 2, 3), 1), Ɐ(3)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(3, 2, 1), 1), Ɐ(3)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1, 3, 2), 1), Ɐ(3)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1, 1, 1), 2), Ɐ(1, 1)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1, 2, 3), 2), Ɐ(3, 2)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(3, 2, 1), 2), Ɐ(3, 2)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(1, 2, 3), 3), Ɐ(3, 2, 1)));
			Assert.IsTrue(SetEquals<int>(GetGreatest(Ɐ(3, 2, 1), 3), Ɐ(3, 2, 1)));
		}

		#endregion

		#region GetLeast

		[TestMethod]
		public void GetLeast_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => GetLeast(Ɐ<int>(), 1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast(Ɐ(1), 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast(Ɐ(1), -1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast(Ɐ(1), 2));

			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(1), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(1, 2), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(2, 1), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(1, 2, 3), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(3, 2, 1), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(3, 1, 2), 1), Ɐ(1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(1, 1, 1), 2), Ɐ(1, 1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(1, 2, 3), 2), Ɐ(1, 2)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(3, 2, 1), 2), Ɐ(1, 2)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(1, 2, 3), 3), Ɐ(3, 2, 1)));
			Assert.IsTrue(SetEquals<int>(GetLeast(Ɐ(3, 2, 1), 3), Ɐ(3, 2, 1)));
		}

		#endregion

		#region HammingDistance

		[TestMethod]
		public void HammingDistance_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => HammingDistance("a", ""));
			Assert.ThrowsException<ArgumentException>(() => HammingDistance("", "a"));

			Assert.IsTrue(HammingDistance("", "") is 0);
			Assert.IsTrue(HammingDistance("a", "a") is 0);
			Assert.IsTrue(HammingDistance("a", "b") is 1);
			Assert.IsTrue(HammingDistance("aa", "aa") is 0);
			Assert.IsTrue(HammingDistance("aa", "ab") is 1);
			Assert.IsTrue(HammingDistance("aa", "bb") is 2);
			Assert.IsTrue(HammingDistance("aaa", "aaa") is 0);
			Assert.IsTrue(HammingDistance("aaa", "aab") is 1);
			Assert.IsTrue(HammingDistance("aaa", "aba") is 1);
			Assert.IsTrue(HammingDistance("aaa", "baa") is 1);
			Assert.IsTrue(HammingDistance("aaa", "bab") is 2);
			Assert.IsTrue(HammingDistance("aaa", "bba") is 2);
			Assert.IsTrue(HammingDistance("aaa", "bbb") is 3);
		}

		#endregion
	}
}
