using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
    [TestClass]
    public class Symbolics_Testing
    {
        [TestMethod]
        public void Parse_String_Testing()
        {
            Symbolics.Constant<int> ONE = new Symbolics.Constant<int>(1);

            {
                var A = Symbolics.Parse<int>("1 + 1", int.TryParse);
                var B = ONE + ONE;
                Assert.IsTrue(A.Equals(B));
            }

            {
                var A = Symbolics.Parse<int>("1 + (1 + 1)", int.TryParse);
                var B = ONE + (ONE + ONE);
                Assert.IsTrue(A.Equals(B));
            }

            {
                var A = Symbolics.Parse<int>("1 + (1 + (1 + 1))", int.TryParse);
                var B = ONE + (ONE + (ONE + ONE));
                Assert.IsTrue(A.Equals(B));
            }

            Assert.Inconclusive("Test Method Not Fully Implemented");
        }
    }
}
