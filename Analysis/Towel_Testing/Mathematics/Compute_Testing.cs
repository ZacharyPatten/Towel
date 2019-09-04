using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Towel;
using Towel.Mathematics;
using Towel.Measurements;
using static Towel.Mathematics.Compute;

namespace Towel_Testing.Mathematics
{
	[TestClass]
	public class Compute_Testing
	{
		[TestMethod]
		public void Negate_Testing()
		{
			{ // int
				Assert.IsTrue(Negate(-3) == 3);
				Assert.IsTrue(Negate(-2) == 2);
				Assert.IsTrue(Negate(-1) == 1);
				Assert.IsTrue(Negate(0) == 0);
				Assert.IsTrue(Negate(1) == -1);
				Assert.IsTrue(Negate(2) == -2);
				Assert.IsTrue(Negate(3) == -3);
			}
			{ // float
				Assert.IsTrue(Negate(-3f) == 3f);
				Assert.IsTrue(Negate(-2f) == 2f);
				Assert.IsTrue(Negate(-1f) == 1f);
				Assert.IsTrue(Negate(-0.5f) == 0.5f);
				Assert.IsTrue(Negate(0f) == 0f);
				Assert.IsTrue(Negate(0.5f) == -0.5f);
				Assert.IsTrue(Negate(1f) == -1f);
				Assert.IsTrue(Negate(2f) == -2f);
				Assert.IsTrue(Negate(3f) == -3f);
			}
			{ // double
				Assert.IsTrue(Negate(-3d) == 3d);
				Assert.IsTrue(Negate(-2d) == 2d);
				Assert.IsTrue(Negate(-1d) == 1d);
				Assert.IsTrue(Negate(-0.5d) == 0.5d);
				Assert.IsTrue(Negate(0d) == 0d);
				Assert.IsTrue(Negate(0.5d) == -0.5d);
				Assert.IsTrue(Negate(1d) == -1d);
				Assert.IsTrue(Negate(2d) == -2d);
				Assert.IsTrue(Negate(3d) == -3d);
			}
			{ // decimal
				Assert.IsTrue(Negate(-3m) == 3m);
				Assert.IsTrue(Negate(-2m) == 2m);
				Assert.IsTrue(Negate(-1m) == 1m);
				Assert.IsTrue(Negate(-0.5m) == 0.5m);
				Assert.IsTrue(Negate(0m) == 0m);
				Assert.IsTrue(Negate(0.5m) == -0.5m);
				Assert.IsTrue(Negate(1m) == -1m);
				Assert.IsTrue(Negate(2m) == -2m);
				Assert.IsTrue(Negate(3m) == -3m);
			}
		}

		[TestMethod]
		public void Add_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Add(0, 0) == 0 + 0);
			Assert.IsTrue(Add(1, 1) == 1 + 1);
			Assert.IsTrue(Add(1, 2) == 1 + 2);
			Assert.IsTrue(Add(-1, 1) == -1 + 1);
			Assert.IsTrue(Add(1, -1) == 1 + -1);
			Assert.IsTrue(Add(-1, -1) == -1 + -1);
			// float
			Assert.IsTrue(Add(0f, 0f) == 0f + 0f);
			Assert.IsTrue(Add(1f, 1f) == 1f + 1f);
			Assert.IsTrue(Add(1f, 2f) == 1f + 2f);
			Assert.IsTrue(Add(-1f, 1f) == -1f + 1f);
			Assert.IsTrue(Add(1f, -1f) == 1f + -1f);
			Assert.IsTrue(Add(-1f, -1f) == -1f + -1f);
			// double
			Assert.IsTrue(Add(0d, 0d) == 0d + 0d);
			Assert.IsTrue(Add(1d, 1d) == 1d + 1d);
			Assert.IsTrue(Add(1d, 2d) == 1d + 2d);
			Assert.IsTrue(Add(-1d, 1d) == -1d + 1d);
			Assert.IsTrue(Add(1d, -1d) == 1d + -1d);
			Assert.IsTrue(Add(-1d, -1d) == -1d + -1d);
			// decimal
			Assert.IsTrue(Add(0m, 0m) == 0m + 0m);
			Assert.IsTrue(Add(1m, 1m) == 1m + 1m);
			Assert.IsTrue(Add(1m, 2m) == 1m + 2m);
			Assert.IsTrue(Add(-1m, 1m) == -1m + 1m);
			Assert.IsTrue(Add(1m, -1m) == 1m + -1m);
			Assert.IsTrue(Add(-1m, -1m) == -1m + -1m);

			// Stepper

			// int
			Assert.IsTrue(Add(1, 2, 3) == 1 + 2 + 3);
			Assert.IsTrue(Add(-1, -2, -3) == -1 + -2 + -3);
			// float
			Assert.IsTrue(Add(1f, 2f, 3f) == 1f + 2f + 3f);
			Assert.IsTrue(Add(-1f, -2f, -3f) == -1f + -2f + -3f);
			// double
			Assert.IsTrue(Add(1d, 2d, 3d) == 1d + 2d + 3d);
			Assert.IsTrue(Add(-1d, -2d, -3d) == -1d + -2d + -3d);
			// decimal
			Assert.IsTrue(Add(1m, 2m, 3m) == 1m + 2m + 3m);
			Assert.IsTrue(Add(-1m, -2m, -3m) == -1m + -2m + -3m);
		}

		[TestMethod]
		public void Subtract_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Subtract(0, 0) == 0 - 0);
			Assert.IsTrue(Subtract(1, 1) == 1 - 1);
			Assert.IsTrue(Subtract(1, 2) == 1 - 2);
			Assert.IsTrue(Subtract(-1, 1) == -1 - 1);
			Assert.IsTrue(Subtract(1, -1) == 1 - -1);
			Assert.IsTrue(Subtract(-1, -1) == -1 - -1);
			// float
			Assert.IsTrue(Subtract(0f, 0f) == 0f - 0f);
			Assert.IsTrue(Subtract(1f, 1f) == 1f - 1f);
			Assert.IsTrue(Subtract(1f, 2f) == 1f - 2f);
			Assert.IsTrue(Subtract(-1f, 1f) == -1f - 1f);
			Assert.IsTrue(Subtract(1f, -1f) == 1f - -1f);
			Assert.IsTrue(Subtract(-1f, -1f) == -1f - -1f);
			// double
			Assert.IsTrue(Subtract(0d, 0d) == 0d - 0d);
			Assert.IsTrue(Subtract(1d, 1d) == 1d - 1d);
			Assert.IsTrue(Subtract(1d, 2d) == 1d - 2d);
			Assert.IsTrue(Subtract(-1d, 1d) == -1d - 1d);
			Assert.IsTrue(Subtract(1d, -1d) == 1d - -1d);
			Assert.IsTrue(Subtract(-1d, -1d) == -1d - -1d);
			// decimal
			Assert.IsTrue(Subtract(0m, 0m) == 0m - 0m);
			Assert.IsTrue(Subtract(1m, 1m) == 1m - 1m);
			Assert.IsTrue(Subtract(1m, 2m) == 1m - 2m);
			Assert.IsTrue(Subtract(-1m, 1m) == -1m - 1m);
			Assert.IsTrue(Subtract(1m, -1m) == 1 - -1m);
			Assert.IsTrue(Subtract(-1m, -1m) == -1m - -1m);

			// Stepper

			// int
			Assert.IsTrue(Subtract(1, 2, 3) == 1 - 2 - 3);
			Assert.IsTrue(Subtract(-1, -2, -3) == -1 - -2 - -3);
			// float
			Assert.IsTrue(Subtract(1f, 2f, 3f) == 1f - 2f - 3f);
			Assert.IsTrue(Subtract(-1f, -2f, -3f) == -1f - -2f - -3f);
			// double
			Assert.IsTrue(Subtract(1d, 2d, 3d) == 1d - 2d - 3d);
			Assert.IsTrue(Subtract(-1d, -2d, -3d) == -1d - -2d - -3d);
			// decimal
			Assert.IsTrue(Subtract(1m, 2m, 3m) == 1m - 2m - 3m);
			Assert.IsTrue(Subtract(-1m, -2m, -3m) == -1m - -2m - -3m);
		}

		[TestMethod]
		public void Multiply_Testing()
		{
			// Binary

			// int
			Assert.IsTrue(Multiply(0, 0) == 0 * 0);
			Assert.IsTrue(Multiply(1, 1) == 1 * 1);
			Assert.IsTrue(Multiply(1, 2) == 1 * 2);
			Assert.IsTrue(Multiply(-1, 1) == -1 * 1);
			Assert.IsTrue(Multiply(1, -1) == 1 * -1);
			Assert.IsTrue(Multiply(-1, -1) == -1 * -1);
			// float
			Assert.IsTrue(Multiply(0f, 0f) == 0f * 0f);
			Assert.IsTrue(Multiply(1f, 1f) == 1f * 1f);
			Assert.IsTrue(Multiply(1f, 2f) == 1f * 2f);
			Assert.IsTrue(Multiply(-1f, 1f) == -1f * 1f);
			Assert.IsTrue(Multiply(1f, -1f) == 1f * -1f);
			Assert.IsTrue(Multiply(-1f, -1f) == -1f * -1f);
			// double
			Assert.IsTrue(Multiply(0d, 0d) == 0d * 0d);
			Assert.IsTrue(Multiply(1d, 1d) == 1d * 1d);
			Assert.IsTrue(Multiply(1d, 2d) == 1d * 2d);
			Assert.IsTrue(Multiply(-1d, 1d) == -1d * 1d);
			Assert.IsTrue(Multiply(1d, -1d) == 1d * -1d);
			Assert.IsTrue(Multiply(-1d, -1d) == -1d * -1d);
			// decimal
			Assert.IsTrue(Multiply(0m, 0m) == 0m * 0m);
			Assert.IsTrue(Multiply(1m, 1m) == 1m * 1m);
			Assert.IsTrue(Multiply(1m, 2m) == 1m * 2m);
			Assert.IsTrue(Multiply(-1m, 1m) == -1m * 1m);
			Assert.IsTrue(Multiply(1m, -1m) == 1m * -1m);
			Assert.IsTrue(Multiply(-1m, -1m) == -1m * -1m);

			// Stepper

			// int
			Assert.IsTrue(Multiply(1, 2, 3) == 1 * 2 * 3);
			Assert.IsTrue(Multiply(-1, -2, -3) == -1 * -2 * -3);
			// float
			Assert.IsTrue(Multiply(1f, 2f, 3f) == 1f * 2f * 3f);
			Assert.IsTrue(Multiply(-1f, -2f, -3f) == -1f * -2f * -3f);
			// double
			Assert.IsTrue(Multiply(1d, 2d, 3d) == 1d * 2d * 3d);
			Assert.IsTrue(Multiply(-1d, -2d, -3d) == -1d * -2d * -3d);
			// decimal
			Assert.IsTrue(Multiply(1m, 2m, 3m) == 1m * 2m * 3m);
			Assert.IsTrue(Multiply(-1m, -2m, -3m) == -1m * -2m * -3m);
		}

		[TestMethod]
		public void Divide_Testing()
		{
			// Binary

			// int
			try
			{
				int result = Divide(0, 0);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Divide(1, 1) == 1 / 1);
			Assert.IsTrue(Divide(2, 1) == 2 / 1);
			Assert.IsTrue(Divide(4, 2) == 4 / 2);
			Assert.IsTrue(Divide(-4, 2) == -4 / 2);
			Assert.IsTrue(Divide(4, -2) == 4 / -2);
			Assert.IsTrue(Divide(-4, -2) == -4 / -2);
			// float
			Assert.IsTrue(float.IsNaN(Divide(0f, 0f)));
			Assert.IsTrue(Divide(1f, 1f) == 1f / 1f);
			Assert.IsTrue(Divide(2f, 1f) == 2f / 1f);
			Assert.IsTrue(Divide(4f, 2f) == 4f / 2f);
			Assert.IsTrue(Divide(-4f, 2f) == -4f / 2f);
			Assert.IsTrue(Divide(4f, -2f) == 4f / -2f);
			Assert.IsTrue(Divide(-4f, -2f) == -4f / -2f);
			// double
			Assert.IsTrue(double.IsNaN(Divide(0d, 0d)));
			Assert.IsTrue(Divide(1d, 1d) == 1d / 1d);
			Assert.IsTrue(Divide(2d, 1d) == 2d / 1d);
			Assert.IsTrue(Divide(4d, 2d) == 4d / 2d);
			Assert.IsTrue(Divide(-4d, 2d) == -4d / 2d);
			Assert.IsTrue(Divide(4d, -2d) == 4d / -2d);
			Assert.IsTrue(Divide(-4d, -2d) == -4d / -2d);
			// decimal
			try
			{
				decimal result = Divide(0m, 0m);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Divide(1m, 1m) == 1m / 1m);
			Assert.IsTrue(Divide(2m, 1m) == 2m / 1m);
			Assert.IsTrue(Divide(4m, 2m) == 4m / 2m);
			Assert.IsTrue(Divide(-4m, 2m) == -4m / 2m);
			Assert.IsTrue(Divide(4m, -2m) == 4m / -2m);
			Assert.IsTrue(Divide(-4m, -2m) == -4m / -2m);

			// Stepper

			// int
			Assert.IsTrue(Divide(100, 10, 10) == 100 / 10 / 10);
			Assert.IsTrue(Divide(-100, -10, -10) == -100 / -10 / -10);
			// float
			Assert.IsTrue(Divide(100f, 10f, 10f) == 100f / 10f / 10f);
			Assert.IsTrue(Divide(-100f, -10f, -10f) == -100f / -10f / -10f);
			// double
			Assert.IsTrue(Divide(100d, 10d, 10d) == 100d / 10d / 10d);
			Assert.IsTrue(Divide(-100d, -10d, -10d) == -100d / -10d / -10d);
			// decimal
			Assert.IsTrue(Divide(100m, 10m, 10m) == 100m / 10m / 10m);
			Assert.IsTrue(Divide(-100m, -10m, -10m) == -100m / -10m / -10m);
		}

		[TestMethod]
		public void Invert_Testing()
		{
			// Note: not entirely sure about the invert method... :/ may remove it

			try
			{
				int result = Invert(0);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Invert(1) == 1 / 1);
			Assert.IsTrue(Invert(-1) == 1 / -1);
			Assert.IsTrue(Invert(2) == 1 / 2);

			Assert.IsTrue(Invert(0f) == 1f / 0f);
			Assert.IsTrue(Invert(1f) == 1f / 1f);
			Assert.IsTrue(Invert(-1f) == 1f / -1f);
			Assert.IsTrue(Invert(2f) == 1f / 2f);

			Assert.IsTrue(Invert(0d) == 1d / 0d);
			Assert.IsTrue(Invert(1d) == 1d / 1d);
			Assert.IsTrue(Invert(-1d) == 1d / -1d);
			Assert.IsTrue(Invert(2d) == 1d / 2d);

			try
			{
				decimal result = Invert(0m);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Invert(1m) == 1m / 1m);
			Assert.IsTrue(Invert(-1m) == 1m / -1m);
			Assert.IsTrue(Invert(2m) == 1m / 2m);
		}

		[TestMethod]
		public void Modulo_Testing()
		{
			// Binary

			// int
			try
			{
				int result = Modulo(0, 0);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Modulo(0, 1) == 0 % 1);
			Assert.IsTrue(Modulo(1, 1) == 1 % 1);
			Assert.IsTrue(Modulo(8, 3) == 8 % 3);
			Assert.IsTrue(Modulo(-8, 3) == -8 % 3);
			Assert.IsTrue(Modulo(8, -3) == 8 % 3);
			Assert.IsTrue(Modulo(-8, -3) == -8 % 3);

			// float
			Assert.IsTrue(float.IsNaN(Modulo(0f, 0f)));
			Assert.IsTrue(Modulo(0f, 1f) == 0f % 1f);
			Assert.IsTrue(Modulo(1f, 1f) == 1f % 1f);
			Assert.IsTrue(Modulo(8f, 3f) == 8f % 3f);
			Assert.IsTrue(Modulo(-8f, 3f) == -8f % 3f);
			Assert.IsTrue(Modulo(8f, -3f) == 8f % -3f);
			Assert.IsTrue(Modulo(-8f, -3f) == -8f % -3f);

			// double
			Assert.IsTrue(double.IsNaN(Modulo(0d, 0d)));
			Assert.IsTrue(Modulo(0d, 1d) == 0d % 1d);
			Assert.IsTrue(Modulo(1d, 1d) == 1d % 1d);
			Assert.IsTrue(Modulo(8d, 3d) == 8d % 3d);
			Assert.IsTrue(Modulo(-8d, 3d) == -8d % 3d);
			Assert.IsTrue(Modulo(8d, -3d) == 8d % -3d);
			Assert.IsTrue(Modulo(-8d, -3d) == -8d % -3d);

			// decimal
			try
			{
				decimal result = Modulo(0m, 0m);
				Assert.Fail();
			}
			catch (DivideByZeroException) { }
			Assert.IsTrue(Modulo(0m, 1m) == 0m % 1m);
			Assert.IsTrue(Modulo(1m, 1m) == 1m % 1m);
			Assert.IsTrue(Modulo(8m, 3m) == 8m % 3m);
			Assert.IsTrue(Modulo(-8m, 3m) == -8m % 3m);
			Assert.IsTrue(Modulo(8m, -3m) == 8m % -3m);
			Assert.IsTrue(Modulo(-8m, -3m) == -8m % -3m);

			// Stepper

			// int
			Assert.IsTrue(Modulo(15, 8, 3) == 15 % 8 % 3);
			Assert.IsTrue(Modulo(-15, -8, -3) == -15 % -8 % -3);
			// float
			Assert.IsTrue(Modulo(15f, 8f, 3f) == 15f % 8f % 3f);
			Assert.IsTrue(Modulo(-15f, -8f, -3f) == -15f % -8f % -3f);
			// double
			Assert.IsTrue(Modulo(15d, 8d, 3d) == 15d % 8d % 3d);
			Assert.IsTrue(Modulo(-15d, -8d, -3d) == -15d % -8d % -3d);
			// decimal
			Assert.IsTrue(Modulo(15m, 8m, 3m) == 15m % 8m % 3m);
			Assert.IsTrue(Modulo(-15m, -8m, -3m) == -15m % -8m % -3m);
		}

		[TestMethod]
		public void Power_Testing()
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

		[TestMethod]
		public void Equal_Testing()
		{
			Assert.IsTrue(Equal(0, 0));
			Assert.IsTrue(Equal(1, 1));
			Assert.IsTrue(Equal(2, 2));
			Assert.IsFalse(Equal(0, 1));

			Assert.IsTrue(Equal(0f, 0f));
			Assert.IsTrue(Equal(1f, 1f));
			Assert.IsTrue(Equal(2f, 2f));
			Assert.IsFalse(Equal(0f, 1f));

			Assert.IsTrue(Equal(0d, 0d));
			Assert.IsTrue(Equal(1d, 1d));
			Assert.IsTrue(Equal(2d, 2d));
			Assert.IsFalse(Equal(0d, 1d));

			Assert.IsTrue(Equal(0m, 0m));
			Assert.IsTrue(Equal(1m, 1m));
			Assert.IsTrue(Equal(2m, 2m));
			Assert.IsFalse(Equal(0m, 1m));

			// More than 2 operands

			Assert.IsTrue(Equal(0, 0, 0));
			Assert.IsTrue(Equal(1, 1, 1));
			Assert.IsTrue(Equal(2, 2, 2));

			Assert.IsFalse(Equal(0, 0, 1));
			Assert.IsFalse(Equal(1, 1, 2));
			Assert.IsFalse(Equal(2, 2, 3));

			Assert.IsFalse(Equal(0, 1, 0));
			Assert.IsFalse(Equal(1, 2, 1));
			Assert.IsFalse(Equal(2, 3, 2));

			Assert.IsFalse(Equal(1, 0, 0));
			Assert.IsFalse(Equal(2, 1, 1));
			Assert.IsFalse(Equal(3, 2, 2));
		}

		[TestMethod]
		public void Equal_leniency_Testing()
		{
			Assert.IsTrue(EqualLeniency(0, 0, 0));
			Assert.IsTrue(EqualLeniency(1, 1, 0));
			Assert.IsTrue(EqualLeniency(2, 2, 0));

			Assert.IsTrue(EqualLeniency(0f, 0f, 0f));
			Assert.IsTrue(EqualLeniency(1f, 1f, 0f));
			Assert.IsTrue(EqualLeniency(2f, 2f, 0f));

			Assert.IsTrue(EqualLeniency(0d, 0d, 0d));
			Assert.IsTrue(EqualLeniency(1d, 1d, 0d));
			Assert.IsTrue(EqualLeniency(2d, 2d, 0d));

			Assert.IsTrue(EqualLeniency(0m, 0m, 0m));
			Assert.IsTrue(EqualLeniency(1m, 1m, 0m));
			Assert.IsTrue(EqualLeniency(2m, 2m, 0m));

			Assert.IsTrue(EqualLeniency(0, 1, 1));
			Assert.IsTrue(EqualLeniency(1, 2, 1));
			Assert.IsTrue(EqualLeniency(2, 3, 1));

			Assert.IsTrue(EqualLeniency(0f, 1f, 1f));
			Assert.IsTrue(EqualLeniency(1f, 2f, 1f));
			Assert.IsTrue(EqualLeniency(2f, 3f, 1f));

			Assert.IsTrue(EqualLeniency(0d, 1d, 1d));
			Assert.IsTrue(EqualLeniency(1d, 2d, 1d));
			Assert.IsTrue(EqualLeniency(2d, 3d, 1d));

			Assert.IsTrue(EqualLeniency(0m, 1m, 1m));
			Assert.IsTrue(EqualLeniency(1m, 2m, 1m));
			Assert.IsTrue(EqualLeniency(2m, 3m, 1m));

			Assert.IsFalse(EqualLeniency(0, 2, 1));
			Assert.IsFalse(EqualLeniency(1, 3, 1));
			Assert.IsFalse(EqualLeniency(2, 4, 1));

			Assert.IsFalse(EqualLeniency(0f, 2f, 1f));
			Assert.IsFalse(EqualLeniency(1f, 3f, 1f));
			Assert.IsFalse(EqualLeniency(2f, 4f, 1f));

			Assert.IsFalse(EqualLeniency(0d, 2d, 1d));
			Assert.IsFalse(EqualLeniency(1d, 3d, 1d));
			Assert.IsFalse(EqualLeniency(2d, 4d, 1d));

			Assert.IsFalse(EqualLeniency(0m, 2m, 1m));
			Assert.IsFalse(EqualLeniency(1m, 3m, 1m));
			Assert.IsFalse(EqualLeniency(2m, 4m, 1m));
		}

		[TestMethod]
		public void SineTaylorSeries_Testing()
		{
			double sine_zero = SineTaylorSeries(new Angle<double>(0d, Angle.Units.Radians));
			Assert.IsTrue(sine_zero == 0d);

			double sine_pi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(sine_pi, 0d, .00001d));

			double sine_2pi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(sine_2pi, 0d, .00001d));

			double sine_halfPi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(sine_halfPi, 1d, .00001d));

			double sine_3halfsPi = SineTaylorSeries(new Angle<double>(Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(sine_3halfsPi, -1d, .00001d));

			double sine_neghalfPi = SineTaylorSeries(new Angle<double>(-Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(sine_neghalfPi, -1d, .00001d));

			double sine_neg3halfsPi = SineTaylorSeries(new Angle<double>(-Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(sine_neg3halfsPi, 1d, .00001d));
		}

		[TestMethod]
		public void CosineTaylorSeries_Testing()
		{
			double cosine_zero = CosineTaylorSeries(new Angle<double>(0d, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_zero, 1d, .00001d));

			double cosine_pi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_pi, -1d, .00001d));

			double cosine_2pi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_2pi, 1d, .00001d));

			double cosine_halfPi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_halfPi, 0d, .00001d));

			double cosine_3halfsPi = CosineTaylorSeries(new Angle<double>(Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_3halfsPi, 0d, .00001d));

			double cosine_neghalfPi = CosineTaylorSeries(new Angle<double>(-Constant<double>.Pi / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_neghalfPi, 0d, .00001d));

			double cosine_neg3halfsPi = CosineTaylorSeries(new Angle<double>(-Constant<double>.Pi * 3 / 2, Angle.Units.Radians));
			Assert.IsTrue(EqualLeniency(cosine_neg3halfsPi, 0d, .00001d));
		}

		[TestMethod]
		public void SquareRoot_Testing()
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

		[TestMethod]
		public void IsPrime_Testing()
		{
			HashSet<int> primeNumbers = new HashSet<int>()
			{
				2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
				103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
			};

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

		[TestMethod]
		public void IsEven_Testing()
		{
			Random random = new Random();

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

		[TestMethod]
		public void IsOdd_Testing()
		{
			Random random = new Random();

			{ // int
				bool isOdd = false;
				for (int i = -100; i < 100; i++)
				{
					Assert.IsTrue(isOdd ? IsOdd(i) : !IsOdd(i));
					isOdd = !isOdd;
				}
			}
			{ // float
				bool isOdd = false;
				for (float i = -100f; i < 100; i++)
				{
					Assert.IsTrue(isOdd ? IsOdd(i) : !IsOdd(i));
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
					Assert.IsTrue(isOdd ? IsOdd(i) : !IsOdd(i));
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
					Assert.IsTrue(isOdd ? IsOdd(i) : !IsOdd(i));
					isOdd = !isOdd;

					// only whole numbers can be even... test a random rational value
					decimal randomRatio = random.NextDecimal(10000) / 10000;
					if (randomRatio > 0m)
					{
						Assert.IsFalse(IsOdd(i + randomRatio));
					}
				}
			}
		}

		[TestMethod]
		public void AbsoluteValue_Testing()
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
		}

		[TestMethod]
		public void Maximum_Testing()
		{
			{ // int
				Assert.IsTrue(Maximum(-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) == 5);
				Assert.IsTrue(Maximum(5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) == 5);
				Assert.IsTrue(Maximum(0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) == 5);
			}
			{ // float
				Assert.IsTrue(Maximum(-5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) == 5f);
				Assert.IsTrue(Maximum(5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) == 5f);
				Assert.IsTrue(Maximum(0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) == 5f);
				Assert.IsTrue(Maximum(-0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) == 3f);
			}
			{ // double
				Assert.IsTrue(Maximum(-5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) == 5d);
				Assert.IsTrue(Maximum(5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) == 5d);
				Assert.IsTrue(Maximum(0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) == 5d);
				Assert.IsTrue(Maximum(-0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) == 3d);
			}
			{ // decimal
				Assert.IsTrue(Maximum(-5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) == 5m);
				Assert.IsTrue(Maximum(5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) == 5m);
				Assert.IsTrue(Maximum(0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) == 5m);
				Assert.IsTrue(Maximum(-0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) == 3m);
			}
		}

		[TestMethod]
		public void Minimum_Testing()
		{
			{ // int
				Assert.IsTrue(Minimum(-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5) == -5);
				Assert.IsTrue(Minimum(5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5) == -5);
				Assert.IsTrue(Minimum(0, 4, 3, 2, 1, 5, -1, -2, -3, -4, -5) == -5);
			}
			{ // float
				Assert.IsTrue(Minimum(-5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f) == -5f);
				Assert.IsTrue(Minimum(5f, 4f, 3f, 2f, 1f, 0f, -1f, -2f, -3f, -4f, -5f) == -5f);
				Assert.IsTrue(Minimum(0f, 4f, 3f, 2f, 1f, 5f, -1f, -2f, -3f, -4f, -5f) == -5f);
				Assert.IsTrue(Minimum(-0.5f, -0.4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 0.4f, 0.5f) == -3f);
			}
			{ // double
				Assert.IsTrue(Minimum(-5d, -4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 4d, 5d) == -5d);
				Assert.IsTrue(Minimum(5d, 4d, 3d, 2d, 1d, 0d, -1d, -2d, -3d, -4d, -5d) == -5d);
				Assert.IsTrue(Minimum(0d, 4d, 3d, 2d, 1d, 5d, -1d, -2d, -3d, -4d, -5d) == -5d);
				Assert.IsTrue(Minimum(-0.5d, -0.4d, -3d, -2d, -1d, 0d, 1d, 2d, 3d, 0.4d, 0.5d) == -3d);
			}
			{ // decimal
				Assert.IsTrue(Minimum(-5m, -4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 4m, 5m) == -5m);
				Assert.IsTrue(Minimum(5m, 4m, 3m, 2m, 1m, 0m, -1m, -2m, -3m, -4m, -5m) == -5m);
				Assert.IsTrue(Minimum(0m, 4m, 3m, 2m, 1m, 5m, -1m, -2m, -3m, -4m, -5m) == -5m);
				Assert.IsTrue(Minimum(-0.5m, -0.4m, -3m, -2m, -1m, 0m, 1m, 2m, 3m, 0.4m, 0.5m) == -3m);
			}
		}

		[TestMethod]
		public void LeastCommonMultiple_Testing()
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

		[TestMethod]
		public void GreatestCommonFactor_Testing()
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

		[TestMethod]
		public void Factorial_testing()
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

		[TestMethod]
		public void BinomialCoefficient_testing()
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

		[TestMethod]
		public void Median_testing()
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

		[TestMethod]
		public void Convert_Testing()
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

		[TestMethod]
		public void Clamp_Testing()
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
		}

		[TestMethod]
		public void NotEqual_Testing()
		{
			Assert.IsTrue(NotEqual(0, 1));
			Assert.IsTrue(NotEqual(-1, 1));
			Assert.IsFalse(NotEqual(1, 1));
			Assert.IsTrue(NotEqual(6, 7));

			Assert.IsTrue(NotEqual(0f, 1f));
			Assert.IsTrue(NotEqual(-1f, 1f));
			Assert.IsFalse(NotEqual(1f, 1f));
			Assert.IsTrue(NotEqual(6f, 7f));

			Assert.IsTrue(NotEqual(0d, 1d));
			Assert.IsTrue(NotEqual(-1d, 1d));
			Assert.IsFalse(NotEqual(1d, 1d));
			Assert.IsTrue(NotEqual(6d, 7d));

			Assert.IsTrue(NotEqual(0m, 1m));
			Assert.IsTrue(NotEqual(-1m, 1m));
			Assert.IsFalse(NotEqual(1m, 1m));
			Assert.IsTrue(NotEqual(6m, 7m));
		}

		[TestMethod]
		public void Compare_Testing()
		{
			Assert.IsTrue(Compare(0, 0) == CompareResult.Equal);
			Assert.IsTrue(Compare(-1, 0) == CompareResult.Less);
			Assert.IsTrue(Compare(1, 0) == CompareResult.Greater);
			Assert.IsTrue(Compare(777, 333) == CompareResult.Greater);
			Assert.IsTrue(Compare(333, 777) == CompareResult.Less);
			Assert.IsTrue(Compare(777, 777) == CompareResult.Equal);

			Assert.IsTrue(Compare(0f, 0f) == CompareResult.Equal);
			Assert.IsTrue(Compare(-1f, 0f) == CompareResult.Less);
			Assert.IsTrue(Compare(1f, 0f) == CompareResult.Greater);
			Assert.IsTrue(Compare(777f, 333f) == CompareResult.Greater);
			Assert.IsTrue(Compare(333f, 777f) == CompareResult.Less);
			Assert.IsTrue(Compare(777f, 777f) == CompareResult.Equal);

			Assert.IsTrue(Compare(0d, 0d) == CompareResult.Equal);
			Assert.IsTrue(Compare(-1d, 0d) == CompareResult.Less);
			Assert.IsTrue(Compare(1d, 0d) == CompareResult.Greater);
			Assert.IsTrue(Compare(777d, 333d) == CompareResult.Greater);
			Assert.IsTrue(Compare(333d, 777d) == CompareResult.Less);
			Assert.IsTrue(Compare(777d, 777d) == CompareResult.Equal);

			Assert.IsTrue(Compare(0m, 0m) == CompareResult.Equal);
			Assert.IsTrue(Compare(-1m, 0m) == CompareResult.Less);
			Assert.IsTrue(Compare(1m, 0m) == CompareResult.Greater);
			Assert.IsTrue(Compare(777m, 333m) == CompareResult.Greater);
			Assert.IsTrue(Compare(333m, 777m) == CompareResult.Less);
			Assert.IsTrue(Compare(777m, 777m) == CompareResult.Equal);
		}

		[TestMethod]
		public void LessThan_Testing()
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

		[TestMethod]
		public void GreaterThan_Testing()
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

		[TestMethod]
		public void LessThanOrEqual_Testing()
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

		[TestMethod]
		public void GreaterThanOrEqual_Testing()
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
	}
}
