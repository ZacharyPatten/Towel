using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
	[TestClass] public class Fraction_Testing
	{
		#region Addition

		[TestMethod] public void Addition()
		{
			{ // int
				{
					Fraction<int> a = new Fraction<int>(1, 1);
					Fraction<int> b = new Fraction<int>(1, 1);
					Fraction<int> c = new Fraction<int>(2, 1);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 2);
					Fraction<int> b = new Fraction<int>(1, 2);
					Fraction<int> c = new Fraction<int>(1, 1);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(2, 1);
					Fraction<int> b = new Fraction<int>(2, 1);
					Fraction<int> c = new Fraction<int>(4, 1);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 3);
					Fraction<int> b = new Fraction<int>(1, 3);
					Fraction<int> c = new Fraction<int>(2, 3);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 2);
					Fraction<int> b = new Fraction<int>(1, 4);
					Fraction<int> c = new Fraction<int>(3, 4);
					Assert.IsTrue(a + b == c);
				}
			}
			{ // float
				{
					Fraction<float> a = new Fraction<float>(1f, 1f);
					Fraction<float> b = new Fraction<float>(1f, 1f);
					Fraction<float> c = new Fraction<float>(2f, 1f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 2f);
					Fraction<float> b = new Fraction<float>(1f, 2f);
					Fraction<float> c = new Fraction<float>(1f, 1f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(2f, 1f);
					Fraction<float> b = new Fraction<float>(2f, 1f);
					Fraction<float> c = new Fraction<float>(4f, 1f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 3f);
					Fraction<float> b = new Fraction<float>(1f, 3f);
					Fraction<float> c = new Fraction<float>(2f, 3f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 2f);
					Fraction<float> b = new Fraction<float>(1f, 4f);
					Fraction<float> c = new Fraction<float>(3f, 4f);
					Assert.IsTrue(a + b == c);
				}
			}
			{ // double
				{
					Fraction<double> a = new Fraction<double>(1d, 1d);
					Fraction<double> b = new Fraction<double>(1d, 1d);
					Fraction<double> c = new Fraction<double>(2d, 1d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 2d);
					Fraction<double> b = new Fraction<double>(1d, 2d);
					Fraction<double> c = new Fraction<double>(1d, 1d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(2d, 1d);
					Fraction<double> b = new Fraction<double>(2d, 1d);
					Fraction<double> c = new Fraction<double>(4d, 1d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 3d);
					Fraction<double> b = new Fraction<double>(1d, 3d);
					Fraction<double> c = new Fraction<double>(2d, 3d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 2d);
					Fraction<double> b = new Fraction<double>(1d, 4d);
					Fraction<double> c = new Fraction<double>(3d, 4d);
					Assert.IsTrue(a + b == c);
				}
			}
			{ // decimal
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> c = new Fraction<decimal>(2m, 1m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> c = new Fraction<decimal>(1m, 1m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(2m, 1m);
					Fraction<decimal> b = new Fraction<decimal>(2m, 1m);
					Fraction<decimal> c = new Fraction<decimal>(4m, 1m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 3m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 3m);
					Fraction<decimal> c = new Fraction<decimal>(2m, 3m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 4m);
					Fraction<decimal> c = new Fraction<decimal>(3m, 4m);
					Assert.IsTrue(a + b == c);
				}
			}
		}

		#endregion

		#region Subtraction

		[TestMethod]
		public void Subtraction()
		{
			{ // int
				{
					Fraction<int> a = new Fraction<int>(1, 1);
					Fraction<int> b = new Fraction<int>(1, 1);
					Fraction<int> c = new Fraction<int>(0, 1);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 1);
					Fraction<int> b = new Fraction<int>(1, 2);
					Fraction<int> c = new Fraction<int>(1, 2);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(3, 4);
					Fraction<int> b = new Fraction<int>(2, 4);
					Fraction<int> c = new Fraction<int>(1, 4);
					Assert.IsTrue(a - b == c);
				}
			}
			{ // float
				{
					Fraction<float> a = new Fraction<float>(1f, 1f);
					Fraction<float> b = new Fraction<float>(1f, 1f);
					Fraction<float> c = new Fraction<float>(0f, 1f);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 1f);
					Fraction<float> b = new Fraction<float>(1f, 2f);
					Fraction<float> c = new Fraction<float>(1f, 2f);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(3f, 4f);
					Fraction<float> b = new Fraction<float>(2f, 4f);
					Fraction<float> c = new Fraction<float>(1f, 4f);
					Assert.IsTrue(a - b == c);
				}
			}
			{ // double
				{
					Fraction<double> a = new Fraction<double>(1d, 1d);
					Fraction<double> b = new Fraction<double>(1d, 1d);
					Fraction<double> c = new Fraction<double>(0d, 1d);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 1d);
					Fraction<double> b = new Fraction<double>(1d, 2d);
					Fraction<double> c = new Fraction<double>(1d, 2d);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(3d, 4d);
					Fraction<double> b = new Fraction<double>(2d, 4d);
					Fraction<double> c = new Fraction<double>(1d, 4d);
					Assert.IsTrue(a - b == c);
				}
			}
			{ // decimal
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> c = new Fraction<decimal>(0m, 1m);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> c = new Fraction<decimal>(1m, 2m);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(3m, 4m);
					Fraction<decimal> b = new Fraction<decimal>(2m, 4m);
					Fraction<decimal> c = new Fraction<decimal>(1m, 4m);
					Assert.IsTrue(a - b == c);
				}
			}
		}

		#endregion

		#region Multiplication

		[TestMethod]
		public void Multiplication()
		{
			{ // int
				{
					Fraction<int> a = new Fraction<int>(1, 1);
					Fraction<int> b = new Fraction<int>(0, 2);
					Fraction<int> c = new Fraction<int>(0, 1);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 1);
					Fraction<int> b = new Fraction<int>(1, 2);
					Fraction<int> c = new Fraction<int>(1, 2);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 2);
					Fraction<int> b = new Fraction<int>(1, 2);
					Fraction<int> c = new Fraction<int>(1, 4);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<int> a = new Fraction<int>(1, 3);
					Fraction<int> b = new Fraction<int>(1, 5);
					Fraction<int> c = new Fraction<int>(1, 15);
					Assert.IsTrue(a * b == c);
				}
			}
			{ // float
				{
					Fraction<float> a = new Fraction<float>(1f, 1f);
					Fraction<float> b = new Fraction<float>(0f, 2f);
					Fraction<float> c = new Fraction<float>(0f, 1f);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 1f);
					Fraction<float> b = new Fraction<float>(1f, 2f);
					Fraction<float> c = new Fraction<float>(1f, 2f);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 2f);
					Fraction<float> b = new Fraction<float>(1f, 2f);
					Fraction<float> c = new Fraction<float>(1f, 4f);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<float> a = new Fraction<float>(1f, 3f);
					Fraction<float> b = new Fraction<float>(1f, 5f);
					Fraction<float> c = new Fraction<float>(1f, 15f);
					Assert.IsTrue(a * b == c);
				}
			}
			{ // double
				{
					Fraction<double> a = new Fraction<double>(1d, 1d);
					Fraction<double> b = new Fraction<double>(0d, 2d);
					Fraction<double> c = new Fraction<double>(0d, 1d);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 1d);
					Fraction<double> b = new Fraction<double>(1d, 2d);
					Fraction<double> c = new Fraction<double>(1d, 2d);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 2d);
					Fraction<double> b = new Fraction<double>(1d, 2d);
					Fraction<double> c = new Fraction<double>(1d, 4d);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<double> a = new Fraction<double>(1d, 3d);
					Fraction<double> b = new Fraction<double>(1d, 5d);
					Fraction<double> c = new Fraction<double>(1d, 15d);
					Assert.IsTrue(a * b == c);
				}
			}
			{ // decimal
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> b = new Fraction<decimal>(0m, 2m);
					Fraction<decimal> c = new Fraction<decimal>(0m, 1m);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 1m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> c = new Fraction<decimal>(1m, 2m);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 2m);
					Fraction<decimal> c = new Fraction<decimal>(1m, 4m);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<decimal> a = new Fraction<decimal>(1m, 3m);
					Fraction<decimal> b = new Fraction<decimal>(1m, 5m);
					Fraction<decimal> c = new Fraction<decimal>(1m, 15m);
					Assert.IsTrue(a * b == c);
				}
			}
		}

		#endregion
	}
}
