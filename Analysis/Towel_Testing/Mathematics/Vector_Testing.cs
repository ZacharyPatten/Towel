using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
    [TestClass]
    public class Vector_Testing
    {
        [TestMethod]
        public void Negate()
        {
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Assert.IsTrue(-a == new Vector<int>(-1, -2, -3));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Assert.IsTrue(-a == new Vector<float>(-1f, -2f, -3f));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Assert.IsTrue(-a == new Vector<double>(-1d, -2d, -3d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Assert.IsTrue(-a == new Vector<decimal>(-1m, -2m, -3m));
            }
        }

        [TestMethod]
        public void Add()
        {
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Vector<int> b = new Vector<int>(1, 2, 3);
                Assert.IsTrue(a + b == new Vector<int>(2, 4, 6));
            }
            {
                Vector<int> a = new Vector<int>(-1, -2, -3);
                Vector<int> b = new Vector<int>(-1, -2, -3);
                Assert.IsTrue(a + b == new Vector<int>(-2, -4, -6));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Vector<float> b = new Vector<float>(1f, 2f, 3f);
                Assert.IsTrue(a + b == new Vector<float>(2f, 4f, 6f));
            }
            {
                Vector<float> a = new Vector<float>(-1f, -2f, -3f);
                Vector<float> b = new Vector<float>(-1f, -2f, -3f);
                Assert.IsTrue(a + b == new Vector<float>(-2f, -4f, -6f));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Vector<double> b = new Vector<double>(1d, 2d, 3d);
                Assert.IsTrue(a + b == new Vector<double>(2d, 4d, 6d));
            }
            {
                Vector<double> a = new Vector<double>(-1d, -2d, -3d);
                Vector<double> b = new Vector<double>(-1d, -2d, -3d);
                Assert.IsTrue(a + b == new Vector<double>(-2d, -4d, -6d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Vector<decimal> b = new Vector<decimal>(1m, 2m, 3m);
                Assert.IsTrue(a + b == new Vector<decimal>(2m, 4m, 6m));
            }
            {
                Vector<decimal> a = new Vector<decimal>(-1m, -2m, -3m);
                Vector<decimal> b = new Vector<decimal>(-1m, -2m, -3m);
                Assert.IsTrue(a + b == new Vector<decimal>(-2m, -4m, -6m));
            }
        }

        [TestMethod]
        public void Subtract()
        {
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Vector<int> b = new Vector<int>(-1, -2, -3);
                Assert.IsTrue(a - b == new Vector<int>(2, 4, 6));
            }
            {
                Vector<int> a = new Vector<int>(-1, -2, -3);
                Vector<int> b = new Vector<int>(1, 2, 3);
                Assert.IsTrue(a - b == new Vector<int>(-2, -4, -6));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Vector<float> b = new Vector<float>(-1f, -2f, -3f);
                Assert.IsTrue(a - b == new Vector<float>(2f, 4f, 6f));
            }
            {
                Vector<float> a = new Vector<float>(-1f, -2f, -3f);
                Vector<float> b = new Vector<float>(1f, 2f, 3f);
                Assert.IsTrue(a - b == new Vector<float>(-2f, -4f, -6f));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Vector<double> b = new Vector<double>(-1d, -2d, -3d);
                Assert.IsTrue(a - b == new Vector<double>(2d, 4d, 6d));
            }
            {
                Vector<double> a = new Vector<double>(-1d, -2d, -3d);
                Vector<double> b = new Vector<double>(1d, 2d, 3d);
                Assert.IsTrue(a - b == new Vector<double>(-2d, -4d, -6d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Vector<decimal> b = new Vector<decimal>(-1m, -2m, -3m);
                Assert.IsTrue(a - b == new Vector<decimal>(2m, 4m, 6m));
            }
            {
                Vector<decimal> a = new Vector<decimal>(-1m, -2m, -3m);
                Vector<decimal> b = new Vector<decimal>(1m, 2m, 3m);
                Assert.IsTrue(a - b == new Vector<decimal>(-2m, -4m, -6m));
            }
        }

        [TestMethod]
        public void Multiply()
        {
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Assert.IsTrue(a * 2 == new Vector<int>(2, 4, 6));
            }
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Assert.IsTrue(a * -2 == new Vector<int>(-2, -4, -6));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Assert.IsTrue(a * 2f == new Vector<float>(2f, 4f, 6f));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Assert.IsTrue(a * -2f == new Vector<float>(-2f, -4f, -6f));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Assert.IsTrue(a * 2d == new Vector<double>(2d, 4d, 6d));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Assert.IsTrue(a * -2d == new Vector<double>(-2d, -4d, -6d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Assert.IsTrue(a * 2m == new Vector<decimal>(2m, 4m, 6m));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Assert.IsTrue(a * -2m == new Vector<decimal>(-2m, -4m, -6m));
            }
        }

        [TestMethod]
        public void Divide()
        {
            {
                Vector<int> a = new Vector<int>(2, 4, 6);
                Assert.IsTrue(a / 2 == new Vector<int>(1, 2, 3));
            }
            {
                Vector<int> a = new Vector<int>(2, 4, 6);
                Assert.IsTrue(a / -2 == new Vector<int>(-1, -2, -3));
            }
            {
                Vector<float> a = new Vector<float>(2f, 4f, 6f);
                Assert.IsTrue(a / 2f == new Vector<float>(1f, 2f, 3f));
            }
            {
                Vector<float> a = new Vector<float>(2f, 4f, 6f);
                Assert.IsTrue(a / -2f == new Vector<float>(-1f, -2f, -3f));
            }
            {
                Vector<double> a = new Vector<double>(2d, 4d, 6d);
                Assert.IsTrue(a / 2d == new Vector<double>(1d, 2d, 3d));
            }
            {
                Vector<double> a = new Vector<double>(2d, 4d, 6d);
                Assert.IsTrue(a / -2d == new Vector<double>(-1d, -2d, -3d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(2m, 4m, 6m);
                Assert.IsTrue(a / 2m == new Vector<decimal>(1m, 2m, 3m));
            }
            {
                Vector<decimal> a = new Vector<decimal>(2m, 4m, 6m);
                Assert.IsTrue(a / -2m == new Vector<decimal>(-1m, -2m, -3m));
            }
        }

        [TestMethod]
        public void Magnitude()
        {
            {
                Vector<float> a = new Vector<float>(2f, 2f, 2f, 2f);
                Assert.IsTrue(a.Magnitude == 4f);
            }
            {
                Vector<double> a = new Vector<double>(2d, 2d, 2d, 2d);
                Assert.IsTrue(a.Magnitude == 4d);
            }
            {
                Vector<decimal> a = new Vector<decimal>(2m, 2m, 2m, 2m);
                Assert.IsTrue(a.Magnitude == 4m);
            }
        }

        [TestMethod]
        public void MagnitudeSquared()
        {
            {
                Vector<float> a = new Vector<float>(2f, 2f, 2f, 2f);
                Assert.IsTrue(a.MagnitudeSquared == 16f);
            }
            {
                Vector<double> a = new Vector<double>(2d, 2d, 2d, 2d);
                Assert.IsTrue(a.MagnitudeSquared == 16d);
            }
            {
                Vector<decimal> a = new Vector<decimal>(2m, 2m, 2m, 2m);
                Assert.IsTrue(a.MagnitudeSquared == 16m);
            }
        }

        [TestMethod]
        public void CrossProduct()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void DotProduct()
        {
            { // int
                Vector<int> A = new Vector<int>(1, 2, 3);
                Vector<int> B = new Vector<int>(4, 5, 6);
                Assert.IsTrue(A.DotProduct(B) == 32);
            }
            { // float
                Vector<float> A = new Vector<float>(1f, 2f, 3f);
                Vector<float> B = new Vector<float>(4f, 5f, 6f);
                Assert.IsTrue(A.DotProduct(B) == 32f);
            }
            { // double
                Vector<double> A = new Vector<double>(1d, 2d, 3d);
                Vector<double> B = new Vector<double>(4d, 5d, 6d);
                Assert.IsTrue(A.DotProduct(B) == 32d);
            }
            { // decimal
                Vector<decimal> A = new Vector<decimal>(1m, 2m, 3m);
                Vector<decimal> B = new Vector<decimal>(4m, 5m, 6m);
                Assert.IsTrue(A.DotProduct(B) == 32m);
            }
            { // dimension missmatch
                Vector<int> A = new Vector<int>(2);
                Vector<int> B = new Vector<int>(3);
                Assert.ThrowsException<MathematicsException>(() => A.DotProduct(B));
            }
        }

        [TestMethod]
        public void Normalize()
        {
            {
                Vector<float> a = new Vector<float>(2f, 2f, 2f, 2f);
                Assert.IsTrue(a.Normalize() == new Vector<float>(2f / 4f, 2f / 4f, 2f / 4f, 2f / 4f));
            }
            {
                Vector<double> a = new Vector<double>(2d, 2d, 2d, 2d);
                Assert.IsTrue(a.Normalize() == new Vector<double>(2d / 4d, 2d / 4d, 2d / 4d, 2d / 4d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(2m, 2m, 2m, 2m);
                Assert.IsTrue(a.Normalize() == new Vector<decimal>(2m / 4m, 2m / 4m, 2m / 4m, 2m / 4m));
            }
        }

        [TestMethod]
        public void Angle()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void RotateBy()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void LinearInterpolation()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void SphereicalInterpolation()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void BarycentricInterpolation()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Equal()
        {
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Vector<int> b = new Vector<int>(1, 2, 3);
                Assert.IsTrue(a == b);
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Vector<float> b = new Vector<float>(1f, 2f, 3f);
                Assert.IsTrue(a == b);
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Vector<double> b = new Vector<double>(1d, 2d, 3d);
                Assert.IsTrue(a == b);
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Vector<decimal> b = new Vector<decimal>(1m, 2m, 3m);
                Assert.IsTrue(a == b);
            }
        }

        [TestMethod]
        public void Equal_leniency()
        {
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Vector<int> b = new Vector<int>(3, 4, 5);
                Assert.IsFalse(a.Equal(b, 1));
            }
            {
                Vector<int> a = new Vector<int>(1, 2, 3);
                Vector<int> b = new Vector<int>(2, 3, 4);
                Assert.IsTrue(a.Equal(b, 1));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Vector<float> b = new Vector<float>(3f, 4f, 5f);
                Assert.IsFalse(a.Equal(b, 1f));
            }
            {
                Vector<float> a = new Vector<float>(1f, 2f, 3f);
                Vector<float> b = new Vector<float>(2f, 3f, 4f);
                Assert.IsTrue(a.Equal(b, 1f));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Vector<double> b = new Vector<double>(3d, 4d, 5d);
                Assert.IsFalse(a.Equal(b, 1d));
            }
            {
                Vector<double> a = new Vector<double>(1d, 2d, 3d);
                Vector<double> b = new Vector<double>(2d, 3d, 4d);
                Assert.IsTrue(a.Equal(b, 1d));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Vector<decimal> b = new Vector<decimal>(3m, 4m, 5m);
                Assert.IsFalse(a.Equal(b, 1m));
            }
            {
                Vector<decimal> a = new Vector<decimal>(1m, 2m, 3m);
                Vector<decimal> b = new Vector<decimal>(2m, 3m, 4m);
                Assert.IsTrue(a.Equal(b, 1m));
            }
        }
    }
}
