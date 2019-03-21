using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;
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
            Assert.IsTrue(Add(0, 0) == 0);
            Assert.IsTrue(Add(1, 1) == 2);
            Assert.IsTrue(Add(1, 2) == 3);
            Assert.IsTrue(Add(-1, 1) == 0);
            Assert.IsTrue(Add(1, -1) == 0);
            Assert.IsTrue(Add(-1, -1) == -2);
            // float
            Assert.IsTrue(Add(0f, 0f) == 0f);
            Assert.IsTrue(Add(1f, 1f) == 2f);
            Assert.IsTrue(Add(1f, 2f) == 3f);
            Assert.IsTrue(Add(-1f, 1f) == 0f);
            Assert.IsTrue(Add(1f, -1f) == 0f);
            Assert.IsTrue(Add(-1f, -1f) == -2f);
            // double
            Assert.IsTrue(Add(0d, 0d) == 0d);
            Assert.IsTrue(Add(1d, 1d) == 2d);
            Assert.IsTrue(Add(1d, 2d) == 3d);
            Assert.IsTrue(Add(-1d, 1d) == 0d);
            Assert.IsTrue(Add(1d, -1d) == 0d);
            Assert.IsTrue(Add(-1d, -1d) == -2d);
            // decimal
            Assert.IsTrue(Add(0M, 0M) == 0M);
            Assert.IsTrue(Add(1M, 1M) == 2M);
            Assert.IsTrue(Add(1M, 2M) == 3M);
            Assert.IsTrue(Add(-1M, 1M) == 0M);
            Assert.IsTrue(Add(1M, -1M) == 0M);
            Assert.IsTrue(Add(-1M, -1M) == -2M);

            // Stepper

            // int
            Assert.IsTrue(Add(1, 2, 3) == 6);
            Assert.IsTrue(Add(-1, -2, -3) == -6);
            // float
            Assert.IsTrue(Add(1f, 2f, 3f) == 6f);
            Assert.IsTrue(Add(-1f, -2f, -3f) == -6f);
            // double
            Assert.IsTrue(Add(1d, 2d, 3d) == 6d);
            Assert.IsTrue(Add(-1d, -2d, -3d) == -6d);
            // decimal
            Assert.IsTrue(Add(1M, 2M, 3M) == 6M);
            Assert.IsTrue(Add(-1M, -2M, -3M) == -6M);
        }

        [TestMethod]
        public void Subtract_Testing()
        {
            // Binary

            // int
            Assert.IsTrue(Subtract(0, 0) == 0);
            Assert.IsTrue(Subtract(1, 1) == 0);
            Assert.IsTrue(Subtract(1, 2) == -1);
            Assert.IsTrue(Subtract(-1, 1) == -2);
            Assert.IsTrue(Subtract(1, -1) == 2);
            Assert.IsTrue(Subtract(-1, -1) == 0);
            // float
            Assert.IsTrue(Subtract(0f, 0f) == 0f);
            Assert.IsTrue(Subtract(1f, 1f) == 0f);
            Assert.IsTrue(Subtract(1f, 2f) == -1f);
            Assert.IsTrue(Subtract(-1f, 1f) == -2f);
            Assert.IsTrue(Subtract(1f, -1f) == 2f);
            Assert.IsTrue(Subtract(-1f, -1f) == 0f);
            // double
            Assert.IsTrue(Subtract(0d, 0d) == 0d);
            Assert.IsTrue(Subtract(1d, 1d) == 0d);
            Assert.IsTrue(Subtract(1d, 2d) == -1d);
            Assert.IsTrue(Subtract(-1d, 1d) == -2d);
            Assert.IsTrue(Subtract(1d, -1d) == 2d);
            Assert.IsTrue(Subtract(-1d, -1d) == 0d);
            // decimal
            Assert.IsTrue(Subtract(0M, 0M) == 0M);
            Assert.IsTrue(Subtract(1M, 1M) == 0M);
            Assert.IsTrue(Subtract(1M, 2M) == -1M);
            Assert.IsTrue(Subtract(-1M, 1M) == -2M);
            Assert.IsTrue(Subtract(1M, -1M) == 2M);
            Assert.IsTrue(Subtract(-1M, -1M) == 0M);

            // Stepper

            // int
            Assert.IsTrue(Subtract(1, 2, 3) == -4);
            Assert.IsTrue(Subtract(-1, -2, -3) == 4);
            // float
            Assert.IsTrue(Subtract(1f, 2f, 3f) == -4f);
            Assert.IsTrue(Subtract(-1f, -2f, -3f) == 4f);
            // double
            Assert.IsTrue(Subtract(1d, 2d, 3d) == -4d);
            Assert.IsTrue(Subtract(-1d, -2d, -3d) == 4d);
            // decimal
            Assert.IsTrue(Subtract(1M, 2M, 3M) == -4M);
            Assert.IsTrue(Subtract(-1M, -2M, -3M) == 4M);
        }

        [TestMethod]
        public void Multiply_Testing()
        {
            // Binary

            // int
            Assert.IsTrue(Multiply(0, 0) == 0);
            Assert.IsTrue(Multiply(1, 1) == 1);
            Assert.IsTrue(Multiply(1, 2) == 2);
            Assert.IsTrue(Multiply(-1, 1) == -1);
            Assert.IsTrue(Multiply(1, -1) == -1);
            Assert.IsTrue(Multiply(-1, -1) == 1);
            // float
            Assert.IsTrue(Multiply(0f, 0f) == 0f);
            Assert.IsTrue(Multiply(1f, 1f) == 1f);
            Assert.IsTrue(Multiply(1f, 2f) == 2f);
            Assert.IsTrue(Multiply(-1f, 1f) == -1f);
            Assert.IsTrue(Multiply(1f, -1f) == -1f);
            Assert.IsTrue(Multiply(-1f, -1f) == 1f);
            // double
            Assert.IsTrue(Multiply(0d, 0d) == 0d);
            Assert.IsTrue(Multiply(1d, 1d) == 1d);
            Assert.IsTrue(Multiply(1d, 2d) == 2d);
            Assert.IsTrue(Multiply(-1d, 1d) == -1d);
            Assert.IsTrue(Multiply(1d, -1d) == -1d);
            Assert.IsTrue(Multiply(-1d, -1d) == 1d);
            // decimal
            Assert.IsTrue(Multiply(0M, 0M) == 0M);
            Assert.IsTrue(Multiply(1M, 1M) == 1M);
            Assert.IsTrue(Multiply(1M, 2M) == 2M);
            Assert.IsTrue(Multiply(-1M, 1M) == -1M);
            Assert.IsTrue(Multiply(1M, -1M) == -1M);
            Assert.IsTrue(Multiply(-1M, -1M) == 1M);

            // Stepper

            // int
            Assert.IsTrue(Multiply(1, 2, 3) == 6);
            Assert.IsTrue(Multiply(-1, -2, -3) == -6);
            // float
            Assert.IsTrue(Multiply(1f, 2f, 3f) == 6f);
            Assert.IsTrue(Multiply(-1f, -2f, -3f) == -6f);
            // double
            Assert.IsTrue(Multiply(1d, 2d, 3d) == 6d);
            Assert.IsTrue(Multiply(-1d, -2d, -3d) == -6d);
            // decimal
            Assert.IsTrue(Multiply(1M, 2M, 3M) == 6M);
            Assert.IsTrue(Multiply(-1M, -2M, -3M) == -6M);
        }
    }
}
