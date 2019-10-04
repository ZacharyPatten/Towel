using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.Mathematics;

namespace Towel_Testing
{
	#region Fraction32

	[TestClass]
	public class Fraction32_Testing
	{
		[TestMethod]
		public void FromDouble()
		{
			Fraction32 ZERO = (Fraction32)(0);

			Fraction32 A = (Fraction32)(1d / 2d);
			Assert.IsTrue(A != ZERO);

			Fraction32 B = (Fraction32)(-123d / 9d);
			Assert.IsTrue(B != ZERO);

			Fraction32 C = (Fraction32)(7d / 12d);
			Assert.IsTrue(C != ZERO);

			Fraction32 D = (Fraction32)(14d / 15d);
			Assert.IsTrue(D != ZERO);
		}
	}

	#endregion

	#region Fraction64

	[TestClass]
	public class Fraction64_Testing
	{
		[TestMethod]
		public void FromDouble()
		{
			Fraction64 ZERO = (Fraction64)(0);

			Fraction64 A = (Fraction64)(1d / 2d);
			Assert.IsTrue(A != ZERO);

			Fraction64 B = (Fraction64)(-123d / 9d);
			Assert.IsTrue(B != ZERO);

			Fraction64 C = (Fraction64)(7d / 12d);
			Assert.IsTrue(C != ZERO);

			Fraction64 D = (Fraction64)(14d / 15d);
			Assert.IsTrue(D != ZERO);
		}
	}

	#endregion

	#region Fraction128

	[TestClass]
	public class Fraction128_Testing
	{
		[TestMethod]
		public void FromDouble()
		{
			Fraction128 ZERO = (Fraction128)(0);

			Fraction128 A = (Fraction128)(1d / 2d);
			Assert.IsTrue(A != ZERO);

			Fraction128 B = (Fraction128)(-123d / 9d);
			Assert.IsTrue(B != ZERO);

			Fraction128 C = (Fraction128)(7d / 12d);
			Assert.IsTrue(C != ZERO);

			Fraction128 D = (Fraction128)(14d / 15d);
			Assert.IsTrue(D != ZERO);
		}
	}

	#endregion
}
