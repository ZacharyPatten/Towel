using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
    [TestClass]
    public class Compute_Testing
    {
        [TestMethod]
        public void Negate()
        {
            Assert.IsTrue(Compute.Negate(1) == -1);
            Assert.IsTrue(Compute.Negate(1d) == -1d);
            Assert.IsTrue(Compute.Negate(0) == 0);
            Assert.IsTrue(Compute.Negate(-1) == 1);
            Assert.IsTrue(Compute.Negate(-1d) == 1d);
        }
    }
}
