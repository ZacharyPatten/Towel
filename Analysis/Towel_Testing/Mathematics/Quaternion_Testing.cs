using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
    [TestClass]
    public class Quaternion_Testing
    {
        [TestMethod]
        public void Magnitude()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void MagnitudeSquared()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Conjugate()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Add()
        {
            // int
            {
                Quaternion<int> A = new Quaternion<int>(1, 1, 1, 1);
                Quaternion<int> B = new Quaternion<int>(1, 2, 3, 4);
                Quaternion<int> C = new Quaternion<int>(2, 3, 4, 5);
                Assert.IsTrue(A + B == C);
            }
            {
                Quaternion<int> A = new Quaternion<int>(1, 1, 1, 1);
                Quaternion<int> B = new Quaternion<int>(-1, -2, -3, -4);
                Quaternion<int> C = new Quaternion<int>(0, -1, -2, -3);
                Assert.IsTrue(A + B == C);
            }
            // float
            {
                Quaternion<float> A = new Quaternion<float>(1f, 1f, 1f, 1f);
                Quaternion<float> B = new Quaternion<float>(1f, 2f, 3f, 4f);
                Quaternion<float> C = new Quaternion<float>(2f, 3f, 4f, 5f);
                Assert.IsTrue(A + B == C);
            }
            {
                Quaternion<float> A = new Quaternion<float>(1f, 1f, 1f, 1f);
                Quaternion<float> B = new Quaternion<float>(-1f, -2f, -3f, -4f);
                Quaternion<float> C = new Quaternion<float>(0f, -1f, -2f, -3f);
                Assert.IsTrue(A + B == C);
            }
            // double
            {
                Quaternion<double> A = new Quaternion<double>(1d, 1d, 1d, 1d);
                Quaternion<double> B = new Quaternion<double>(1d, 2d, 3d, 4d);
                Quaternion<double> C = new Quaternion<double>(2d, 3d, 4d, 5d);
                Assert.IsTrue(A + B == C);
            }
            {
                Quaternion<double> A = new Quaternion<double>(1d, 1d, 1d, 1d);
                Quaternion<double> B = new Quaternion<double>(-1d, -2d, -3d, -4d);
                Quaternion<double> C = new Quaternion<double>(0d, -1d, -2d, -3d);
                Assert.IsTrue(A + B == C);
            }
            // decimal
            {
                Quaternion<decimal> A = new Quaternion<decimal>(1m, 1m, 1m, 1m);
                Quaternion<decimal> B = new Quaternion<decimal>(1m, 2m, 3m, 4m);
                Quaternion<decimal> C = new Quaternion<decimal>(2m, 3m, 4m, 5m);
                Assert.IsTrue(A + B == C);
            }
            {
                Quaternion<decimal> A = new Quaternion<decimal>(1m, 1m, 1m, 1m);
                Quaternion<decimal> B = new Quaternion<decimal>(-1m, -2m, -3m, -4m);
                Quaternion<decimal> C = new Quaternion<decimal>(0m, -1m, -2m, -3m);
                Assert.IsTrue(A + B == C);
            }
        }

        [TestMethod]
        public void Subtract()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply_Quaternion()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply_Vector()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply_Scalar()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Normalize()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Invert()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Lerp()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Slerp()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Rotate()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Equal()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Equal_leniency()
        {
            Assert.Inconclusive("Test Not Implemented");
        }
    }
}
