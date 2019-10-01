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
			{ // successful parse
				Assert.IsTrue(Assume.TryParse("1", out int _integer) && _integer == 1);
				Assert.IsTrue(Assume.TryParse("1.2", out float _float) && _float == 1.2f);
				Assert.IsTrue(Assume.TryParse("1.23", out double _double) && _double == 1.23d);
				Assert.IsTrue(Assume.TryParse("1.234", out decimal _decimal) && _decimal == 1.234m);
			}
			{ // default value (parse fails)
				Assert.IsTrue(Assume.TryParse<int>("a", Default: 1) == 1);
				Assert.IsTrue(Assume.TryParse<float>("a", Default: 1) == 1);
				Assert.IsTrue(Assume.TryParse<double>("a", Default: 1) == 1);
				Assert.IsTrue(Assume.TryParse<decimal>("a", Default: 1) == 1);
			}
			{ // parse fails
				Assert.IsFalse(Assume.TryParse("a", out int _));
				Assert.IsFalse(Assume.TryParse("a", out float _));
				Assert.IsFalse(Assume.TryParse("a", out double _));
				Assert.IsFalse(Assume.TryParse("a", out decimal _));
			}
			{ // successful parse (default override)
				Assert.IsTrue(Assume.TryParse<int>("1", Default: -1) == 1);
				Assert.IsTrue(Assume.TryParse<float>("1.2", Default: -1) == 1.2f);
				Assert.IsTrue(Assume.TryParse<double>("1.23", Default: -1) == 1.23d);
				Assert.IsTrue(Assume.TryParse<decimal>("1.234", Default: -1) == 1.234m);
			}
		}
	}
}
