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
        public void Add()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Subtract()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply()
        {
            Assert.Inconclusive("Test Not Implemented");
        }
        
        [TestMethod]
        public void Divide()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void CrossProduct()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void DotProduct()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Normalize()
        {
            Assert.Inconclusive("Test Not Implemented");
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
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Equal_leniency()
        {
            Assert.Inconclusive("Test Not Implemented");
        }
    }
}
