using System;
using Towel;
using static Towel.Syntax;

#pragma warning disable IDE0039 // Use local function

namespace Syntax
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("You are runnning the Syntax example.");
			Console.WriteLine("============================================");
			Console.WriteLine();

			#region TryParse
			{
				TryParse("123.4", out double a);
				TryParse("12.3", out float b);
				TryParse("1", out byte c);
				TryParse("1234", out int d);
				TryParse("1234", out Program e);

				Console.WriteLine("  TryParse------------------------------------");
				Console.WriteLine();
				Console.WriteLine("    TryParse(\"123.4\", out double a) := " + a + "d");
				Console.WriteLine("    TryParse(\"12.3\", out float b) := " + b + "f");
				Console.WriteLine("    TryParse(\"1\", out byte c) := " + c);
				Console.WriteLine("    TryParse(\"1234\", out int d) := " + d);
				Console.WriteLine("    TryParse(\"1234\", out Program e) := " + (e?.ToString() ?? "null"));
				Console.WriteLine();
			}
			#endregion

			#region Convert
			{
				double a = Convert<int, double>(1234);
				float b = Convert<int, float>(123);
				int c = Convert<double, int>(123.4d);
				int d = Convert<float, int>(12.3f);

				Console.WriteLine("  Convert------------------------------------");
				Console.WriteLine();
				Console.WriteLine("    Convert<int, double>(1234) := " + a + "d");
				Console.WriteLine("    Convert<int, float>(123) := " + b + "f");
				Console.WriteLine("    Convert<double, int>(123.4d) := " + c);
				Console.WriteLine("    Convert<float, int>(12.3f) := " + d);
				Console.WriteLine();
			}
			#endregion

			#region Stepper
			{
				Console.WriteLine("  Stepper------------------------------------");
				Console.WriteLine();
				Console.WriteLine("    A Towel.Stepper<T> in Towel is similar to a ");
				Console.WriteLine("    System.Collections.Generic.IEnumerable<T> but");
				Console.WriteLine("    it uses delegates rather than an enumerator.");
				Console.WriteLine("    There are pros/cons to both methodologies.");
				Console.WriteLine();

				System.Collections.Generic.IEnumerable<int> iEnumerable = new int[] { 1, 2, 3, };
				Console.Write("    iEnumerable values:");
				foreach (int value in iEnumerable)
				{
					Console.Write(" " + value);
				}
				Console.WriteLine();

				Stepper<int> stepper = new int[] { 1, 2, 3, }.ToStepper();
				Console.Write("    stepper values:");
				stepper(value => Console.Write(" " + value));
				Console.WriteLine();

				/// You can "break" a foreach loop, but you cannot break a stepper traversal.
				/// For this, there is another type of stepper that is breakable. "Towel.StepperBreak<T>"
				/// is a breakable version of the stepper.

				StepperBreak<int> stepperBreak = new int[] { 1, 2, 3, 4, 5, 6, }.ToStepperBreak();
				Console.Write("    stepperBreak values:");
				stepperBreak(value =>
				{
					Console.Write(" " + value);
					return value >= 3 ? Break : Continue;
				});
				Console.WriteLine();

				/// You cannot alter the values of an IEnumerable during iteration, however,
				/// you can do so with a "Towel.StepperRef<T>".

				StepperRef<int> stepperRef = new int[] { 0, 1, 2, }.ToStepperRef();
				Console.Write("    stepperRef values:");
				stepperRef((ref int value) =>
				{
					value++;
					Console.Write(" " + value);
				});
				Console.WriteLine();

				/// The "Towel.StepperRefBreak<T>" is a stepper type that allows for altering
				/// values and breaking iteration.

				StepperRefBreak<int> stepperRefBreak = new int[] { 0, 1, 2, 3, 4, 5, }.ToStepperRefBreak();
				Console.Write("    stepperRefBreak values:");
				stepperRefBreak((ref int value) =>
				{
					value++;
					Console.Write(" " + value);
					return value >= 3 ? Break : Continue;
				});
				Console.WriteLine();

				/// Here is an example of creating a stepper from only functions (no backing
				/// data structure).
				Stepper<int> stepperFunctional = s => { s(1); s(2); s(3); };
				Console.Write("    stepperFunctional values:");
				stepperFunctional(value => Console.Write(" " + value));

				Console.WriteLine();
				Console.WriteLine();
			}
			#endregion

			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.ReadLine();
		}
	}
}

#pragma warning restore IDE0039 // Use local function
