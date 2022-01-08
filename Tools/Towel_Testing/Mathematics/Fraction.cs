namespace Towel_Testing.Mathematics
{
	[TestClass]
	public class Fraction_Testing
	{
		#region Addition

		[TestMethod]
		public void Addition()
		{
			{ // int
				{
					Fraction<int> a = new(1, 1);
					Fraction<int> b = new(1, 1);
					Fraction<int> c = new(2, 1);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new(1, 2);
					Fraction<int> b = new(1, 2);
					Fraction<int> c = new(1, 1);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new(2, 1);
					Fraction<int> b = new(2, 1);
					Fraction<int> c = new(4, 1);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new(1, 3);
					Fraction<int> b = new(1, 3);
					Fraction<int> c = new(2, 3);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<int> a = new(1, 2);
					Fraction<int> b = new(1, 4);
					Fraction<int> c = new(3, 4);
					Assert.IsTrue(a + b == c);
				}
			}
			{ // float
				{
					Fraction<float> a = new(1f, 1f);
					Fraction<float> b = new(1f, 1f);
					Fraction<float> c = new(2f, 1f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new(1f, 2f);
					Fraction<float> b = new(1f, 2f);
					Fraction<float> c = new(1f, 1f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new(2f, 1f);
					Fraction<float> b = new(2f, 1f);
					Fraction<float> c = new(4f, 1f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new(1f, 3f);
					Fraction<float> b = new(1f, 3f);
					Fraction<float> c = new(2f, 3f);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<float> a = new(1f, 2f);
					Fraction<float> b = new(1f, 4f);
					Fraction<float> c = new(3f, 4f);
					Assert.IsTrue(a + b == c);
				}
			}
			{ // double
				{
					Fraction<double> a = new(1d, 1d);
					Fraction<double> b = new(1d, 1d);
					Fraction<double> c = new(2d, 1d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new(1d, 2d);
					Fraction<double> b = new(1d, 2d);
					Fraction<double> c = new(1d, 1d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new(2d, 1d);
					Fraction<double> b = new(2d, 1d);
					Fraction<double> c = new(4d, 1d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new(1d, 3d);
					Fraction<double> b = new(1d, 3d);
					Fraction<double> c = new(2d, 3d);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<double> a = new(1d, 2d);
					Fraction<double> b = new(1d, 4d);
					Fraction<double> c = new(3d, 4d);
					Assert.IsTrue(a + b == c);
				}
			}
			{ // decimal
				{
					Fraction<decimal> a = new(1m, 1m);
					Fraction<decimal> b = new(1m, 1m);
					Fraction<decimal> c = new(2m, 1m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new(1m, 2m);
					Fraction<decimal> b = new(1m, 2m);
					Fraction<decimal> c = new(1m, 1m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new(2m, 1m);
					Fraction<decimal> b = new(2m, 1m);
					Fraction<decimal> c = new(4m, 1m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new(1m, 3m);
					Fraction<decimal> b = new(1m, 3m);
					Fraction<decimal> c = new(2m, 3m);
					Assert.IsTrue(a + b == c);
				}
				{
					Fraction<decimal> a = new(1m, 2m);
					Fraction<decimal> b = new(1m, 4m);
					Fraction<decimal> c = new(3m, 4m);
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
					Fraction<int> a = new(1, 1);
					Fraction<int> b = new(1, 1);
					Fraction<int> c = new(0, 1);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<int> a = new(1, 1);
					Fraction<int> b = new(1, 2);
					Fraction<int> c = new(1, 2);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<int> a = new(3, 4);
					Fraction<int> b = new(2, 4);
					Fraction<int> c = new(1, 4);
					Assert.IsTrue(a - b == c);
				}
			}
			{ // float
				{
					Fraction<float> a = new(1f, 1f);
					Fraction<float> b = new(1f, 1f);
					Fraction<float> c = new(0f, 1f);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<float> a = new(1f, 1f);
					Fraction<float> b = new(1f, 2f);
					Fraction<float> c = new(1f, 2f);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<float> a = new(3f, 4f);
					Fraction<float> b = new(2f, 4f);
					Fraction<float> c = new(1f, 4f);
					Assert.IsTrue(a - b == c);
				}
			}
			{ // double
				{
					Fraction<double> a = new(1d, 1d);
					Fraction<double> b = new(1d, 1d);
					Fraction<double> c = new(0d, 1d);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<double> a = new(1d, 1d);
					Fraction<double> b = new(1d, 2d);
					Fraction<double> c = new(1d, 2d);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<double> a = new(3d, 4d);
					Fraction<double> b = new(2d, 4d);
					Fraction<double> c = new(1d, 4d);
					Assert.IsTrue(a - b == c);
				}
			}
			{ // decimal
				{
					Fraction<decimal> a = new(1m, 1m);
					Fraction<decimal> b = new(1m, 1m);
					Fraction<decimal> c = new(0m, 1m);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<decimal> a = new(1m, 1m);
					Fraction<decimal> b = new(1m, 2m);
					Fraction<decimal> c = new(1m, 2m);
					Assert.IsTrue(a - b == c);
				}
				{
					Fraction<decimal> a = new(3m, 4m);
					Fraction<decimal> b = new(2m, 4m);
					Fraction<decimal> c = new(1m, 4m);
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
					Fraction<int> a = new(1, 1);
					Fraction<int> b = new(0, 2);
					Fraction<int> c = new(0, 1);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<int> a = new(1, 1);
					Fraction<int> b = new(1, 2);
					Fraction<int> c = new(1, 2);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<int> a = new(1, 2);
					Fraction<int> b = new(1, 2);
					Fraction<int> c = new(1, 4);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<int> a = new(1, 3);
					Fraction<int> b = new(1, 5);
					Fraction<int> c = new(1, 15);
					Assert.IsTrue(a * b == c);
				}
			}
			{ // float
				{
					Fraction<float> a = new(1f, 1f);
					Fraction<float> b = new(0f, 2f);
					Fraction<float> c = new(0f, 1f);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<float> a = new(1f, 1f);
					Fraction<float> b = new(1f, 2f);
					Fraction<float> c = new(1f, 2f);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<float> a = new(1f, 2f);
					Fraction<float> b = new(1f, 2f);
					Fraction<float> c = new(1f, 4f);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<float> a = new(1f, 3f);
					Fraction<float> b = new(1f, 5f);
					Fraction<float> c = new(1f, 15f);
					Assert.IsTrue(a * b == c);
				}
			}
			{ // double
				{
					Fraction<double> a = new(1d, 1d);
					Fraction<double> b = new(0d, 2d);
					Fraction<double> c = new(0d, 1d);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<double> a = new(1d, 1d);
					Fraction<double> b = new(1d, 2d);
					Fraction<double> c = new(1d, 2d);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<double> a = new(1d, 2d);
					Fraction<double> b = new(1d, 2d);
					Fraction<double> c = new(1d, 4d);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<double> a = new(1d, 3d);
					Fraction<double> b = new(1d, 5d);
					Fraction<double> c = new(1d, 15d);
					Assert.IsTrue(a * b == c);
				}
			}
			{ // decimal
				{
					Fraction<decimal> a = new(1m, 1m);
					Fraction<decimal> b = new(0m, 2m);
					Fraction<decimal> c = new(0m, 1m);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<decimal> a = new(1m, 1m);
					Fraction<decimal> b = new(1m, 2m);
					Fraction<decimal> c = new(1m, 2m);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<decimal> a = new(1m, 2m);
					Fraction<decimal> b = new(1m, 2m);
					Fraction<decimal> c = new(1m, 4m);
					Assert.IsTrue(a * b == c);
				}
				{
					Fraction<decimal> a = new(1m, 3m);
					Fraction<decimal> b = new(1m, 5m);
					Fraction<decimal> c = new(1m, 15m);
					Assert.IsTrue(a * b == c);
				}
			}
		}

		#endregion
	}
}
