using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel;

namespace Towel_Testing
{
	[TestClass]
	public class Assume_Testing
	{
		[TestMethod]
		public void TryParse_Testing()
		{
			Assert.IsTrue(Assume.TryParse("1", out int _integer) && _integer == 1);
			Assert.IsTrue(Assume.TryParse("1.2", out float _float) && _float == 1.2f);
			Assert.IsTrue(Assume.TryParse("1.23", out double _double) && _double == 1.23d);
			Assert.IsTrue(Assume.TryParse("1.234", out decimal _decimal) && _decimal == 1.234m);
		}
	}
}
