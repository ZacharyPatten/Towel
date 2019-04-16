using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
            Assert.IsTrue(Negate(1) == -1);
            Assert.IsTrue(Negate(1d) == -1d);
            Assert.IsTrue(Negate(0) == 0);
            Assert.IsTrue(Negate(-1) == 1);
            Assert.IsTrue(Negate(-1d) == 1d);
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

            Assert.IsTrue(Equal(0f, 0f));
            Assert.IsTrue(Equal(1f, 1f));
            Assert.IsTrue(Equal(2f, 2f));

            Assert.IsTrue(Equal(0d, 0d));
            Assert.IsTrue(Equal(1d, 1d));
            Assert.IsTrue(Equal(2d, 2d));

            Assert.IsTrue(Equal(0m, 0m));
            Assert.IsTrue(Equal(1m, 1m));
            Assert.IsTrue(Equal(2m, 2m));

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
    }
}
