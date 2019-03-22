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
            Assert.Fail();
        }

        [TestMethod]
        public void MagnitudeSquared()
        {
            Assert.Fail();
        }
        
        [TestMethod]
        public void Add()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Subtract()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Multiply()
        {
            Assert.Fail();
        }
        
        [TestMethod]
        public void Divide()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CrossProduct()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DotProduct()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Normalize()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Angle()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RotateBy()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void LinearInterpolation()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SphereicalInterpolation()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void BarycentricInterpolation()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Equal()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Equal_leniency()
        {
            Assert.Fail();
        }
    }
}
