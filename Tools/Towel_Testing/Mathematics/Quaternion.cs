namespace Towel_Testing.Mathematics
{
	[TestClass]
	public class Quaternion_Testing
	{
		#region Magnitude

		[TestMethod]
		public void Magnitude()
		{
			// float
			{
				Quaternion<float> A = new(2f, 2f, 2f, 2f);
				Assert.IsTrue(A.Magnitude == 4f);
			}
			{
				Quaternion<float> A = new(3f, 3f, 3f, 3f);
				Assert.IsTrue(A.Magnitude == 6f);
			}
			// double
			{
				Quaternion<double> A = new(2d, 2d, 2d, 2d);
				Assert.IsTrue(A.Magnitude == 4d);
			}
			{
				Quaternion<double> A = new(3d, 3d, 3d, 3d);
				Assert.IsTrue(A.Magnitude == 6d);
			}
			// decimal
			{
				Quaternion<decimal> A = new(2m, 2m, 2m, 2m);
				Assert.IsTrue(A.Magnitude == 4m);
			}
			{
				Quaternion<decimal> A = new(3m, 3m, 3m, 3m);
				Assert.IsTrue(A.Magnitude == 6m);
			}
		}

		#endregion

		#region MagnitudeSquared

		[TestMethod]
		public void MagnitudeSquared()
		{
			// float
			{
				Quaternion<float> A = new(2f, 2f, 2f, 2f);
				Assert.IsTrue(A.MagnitudeSquared == 16f);
			}
			{
				Quaternion<float> A = new(3f, 3f, 3f, 3f);
				Assert.IsTrue(A.MagnitudeSquared == 36f);
			}
			// double
			{
				Quaternion<double> A = new(2d, 2d, 2d, 2d);
				Assert.IsTrue(A.MagnitudeSquared == 16d);
			}
			{
				Quaternion<double> A = new(3d, 3d, 3d, 3d);
				Assert.IsTrue(A.MagnitudeSquared == 36d);
			}
			// decimal
			{
				Quaternion<decimal> A = new(2m, 2m, 2m, 2m);
				Assert.IsTrue(A.MagnitudeSquared == 16m);
			}
			{
				Quaternion<decimal> A = new(3m, 3m, 3m, 3m);
				Assert.IsTrue(A.MagnitudeSquared == 36m);
			}
		}

		#endregion

		#region Conjugate

		[TestMethod]
		public void Conjugate()
		{
			// int
			{
				Quaternion<int> A = new(2, 2, 2, 2);
				Quaternion<int> B = new(-2, -2, -2, 2);
				Assert.IsTrue(A.Conjugate() == B);
			}
			{
				Quaternion<int> A = new(-2, -2, -2, -2);
				Quaternion<int> B = new(2, 2, 2, -2);
				Assert.IsTrue(A.Conjugate() == B);
			}
			// float
			{
				Quaternion<float> A = new(2f, 2f, 2f, 2f);
				Quaternion<float> B = new(-2f, -2f, -2f, 2f);
				Assert.IsTrue(A.Conjugate() == B);
			}
			{
				Quaternion<float> A = new(-2f, -2f, -2f, -2f);
				Quaternion<float> B = new(2f, 2f, 2f, -2f);
				Assert.IsTrue(A.Conjugate() == B);
			}
			// double
			{
				Quaternion<double> A = new(2d, 2d, 2d, 2d);
				Quaternion<double> B = new(-2d, -2d, -2d, 2d);
				Assert.IsTrue(A.Conjugate() == B);
			}
			{
				Quaternion<double> A = new(-2d, -2d, -2d, -2d);
				Quaternion<double> B = new(2d, 2d, 2d, -2d);
				Assert.IsTrue(A.Conjugate() == B);
			}
			// decimal
			{
				Quaternion<decimal> A = new(2m, 2m, 2m, 2m);
				Quaternion<decimal> B = new(-2m, -2m, -2m, 2m);
				Assert.IsTrue(A.Conjugate() == B);
			}
			{
				Quaternion<decimal> A = new(-2m, -2m, -2m, -2m);
				Quaternion<decimal> B = new(2m, 2m, 2m, -2m);
				Assert.IsTrue(A.Conjugate() == B);
			}
		}

		#endregion

		#region Add

		[TestMethod]
		public void Add()
		{
			// int
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(1, 2, 3, 4);
				Quaternion<int> C = new(2, 3, 4, 5);
				Assert.IsTrue(A + B == C);
			}
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(-1, -2, -3, -4);
				Quaternion<int> C = new(0, -1, -2, -3);
				Assert.IsTrue(A + B == C);
			}
			// float
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(1f, 2f, 3f, 4f);
				Quaternion<float> C = new(2f, 3f, 4f, 5f);
				Assert.IsTrue(A + B == C);
			}
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(-1f, -2f, -3f, -4f);
				Quaternion<float> C = new(0f, -1f, -2f, -3f);
				Assert.IsTrue(A + B == C);
			}
			// double
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(1d, 2d, 3d, 4d);
				Quaternion<double> C = new(2d, 3d, 4d, 5d);
				Assert.IsTrue(A + B == C);
			}
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(-1d, -2d, -3d, -4d);
				Quaternion<double> C = new(0d, -1d, -2d, -3d);
				Assert.IsTrue(A + B == C);
			}
			// decimal
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1m);
				Quaternion<decimal> B = new(1m, 2m, 3m, 4m);
				Quaternion<decimal> C = new(2m, 3m, 4m, 5m);
				Assert.IsTrue(A + B == C);
			}
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1m);
				Quaternion<decimal> B = new(-1m, -2m, -3m, -4m);
				Quaternion<decimal> C = new(0m, -1m, -2m, -3m);
				Assert.IsTrue(A + B == C);
			}
		}

		#endregion

		#region Subtract

		[TestMethod]
		public void Subtract()
		{
			// int
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(1, 2, 3, 4);
				Quaternion<int> C = new(0, -1, -2, -3);
				Assert.IsTrue(A - B == C);
			}
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(-1, -2, -3, -4);
				Quaternion<int> C = new(2, 3, 4, 5);
				Assert.IsTrue(A - B == C);
			}
			// float
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(1f, 2f, 3f, 4f);
				Quaternion<float> C = new(0f, -1f, -2f, -3f);
				Assert.IsTrue(A - B == C);
			}
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(-1f, -2f, -3f, -4f);
				Quaternion<float> C = new(2f, 3f, 4f, 5f);
				Assert.IsTrue(A - B == C);
			}
			// double
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(1d, 2d, 3d, 4d);
				Quaternion<double> C = new(0d, -1d, -2d, -3d);
				Assert.IsTrue(A - B == C);
			}
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(-1d, -2d, -3d, -4d);
				Quaternion<double> C = new(2d, 3d, 4d, 5d);
				Assert.IsTrue(A - B == C);
			}
			// decimal
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1m);
				Quaternion<decimal> B = new(1m, 2m, 3m, 4m);
				Quaternion<decimal> C = new(0m, -1m, -2m, -3m);
				Assert.IsTrue(A - B == C);
			}
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1m);
				Quaternion<decimal> B = new(-1m, -2m, -3m, -4m);
				Quaternion<decimal> C = new(2m, 3m, 4m, 5m);
				Assert.IsTrue(A - B == C);
			}
		}

		#endregion

		#region Multiply_Quaternion

		[TestMethod]
		public void Multiply_Quaternion()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Multiply_Vector

		[TestMethod]
		public void Multiply_Vector()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Multiply_Scalar

		[TestMethod]
		public void Multiply_Scalar()
		{
			// int
			{
				Quaternion<int> A = new(2, 3, 4, 5);
				Quaternion<int> B = new(-2, -3, -4, -5);
				Assert.IsTrue(A * -1 == B);
			}
			{
				Quaternion<int> A = new(1, 2, 3, 4);
				Quaternion<int> B = new(2, 4, 6, 8);
				Assert.IsTrue(A * 2 == B);
			}
			// float
			{
				Quaternion<float> A = new(2f, 3f, 4f, 5f);
				Quaternion<float> B = new(-2f, -3f, -4f, -5f);
				Assert.IsTrue(A * -1f == B);
			}
			{
				Quaternion<float> A = new(1f, 2f, 3f, 4f);
				Quaternion<float> B = new(2f, 4f, 6f, 8f);
				Assert.IsTrue(A * 2f == B);
			}
			// double
			{
				Quaternion<double> A = new(2d, 3d, 4d, 5d);
				Quaternion<double> B = new(-2d, -3d, -4d, -5d);
				Assert.IsTrue(A * -1d == B);
			}
			{
				Quaternion<double> A = new(1d, 2d, 3d, 4d);
				Quaternion<double> B = new(2d, 4d, 6d, 8d);
				Assert.IsTrue(A * 2d == B);
			}
			// decimal
			{
				Quaternion<decimal> A = new(2m, 3m, 4m, 5m);
				Quaternion<decimal> B = new(-2m, -3m, -4m, -5m);
				Assert.IsTrue(A * -1m == B);
			}
			{
				Quaternion<decimal> A = new(1m, 2m, 3m, 4m);
				Quaternion<decimal> B = new(2m, 4m, 6m, 8m);
				Assert.IsTrue(A * 2m == B);
			}
		}

		#endregion

		#region Normalize

		[TestMethod]
		public void Normalize()
		{
			// float
			{
				Quaternion<float> A = new(2f, 2f, 2f, 2f);
				Quaternion<float> B = new(0.5f, 0.5f, 0.5f, 0.5f);
				Assert.IsTrue(A.Normalize() == B);
			}
			{
				Quaternion<float> A = new(3f, 3f, 3f, 3f);
				Quaternion<float> B = new(0.5f, 0.5f, 0.5f, 0.5f);
				Assert.IsTrue(A.Normalize() == B);
			}
			// double
			{
				Quaternion<double> A = new(2d, 2d, 2d, 2d);
				Quaternion<double> B = new(0.5d, 0.5d, 0.5d, 0.5d);
				Assert.IsTrue(A.Normalize() == B);
			}
			{
				Quaternion<double> A = new(3d, 3d, 3d, 3d);
				Quaternion<double> B = new(0.5d, 0.5d, 0.5d, 0.5d);
				Assert.IsTrue(A.Normalize() == B);
			}
			// decimal
			{
				Quaternion<decimal> A = new(2m, 2m, 2m, 2m);
				Quaternion<decimal> B = new(0.5m, 0.5m, 0.5m, 0.5m);
				Assert.IsTrue(A.Normalize() == B);
			}
			{
				Quaternion<decimal> A = new(3m, 3m, 3m, 3m);
				Quaternion<decimal> B = new(0.5m, 0.5m, 0.5m, 0.5m);
				Assert.IsTrue(A.Normalize() == B);
			}
		}

		#endregion

		#region Invert

		[TestMethod]
		public void Invert()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Lerp

		[TestMethod]
		public void Lerp()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Slerp

		[TestMethod]
		public void Slerp()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Rotate

		[TestMethod]
		public void Rotate()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Equal

		[TestMethod]
		public void Equal()
		{
			// int
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(1, 1, 1, 1);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<int> A = new(-1, -1, -1, -1);
				Quaternion<int> B = new(-1, -1, -1, -1);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<int> A = new(1, 2, 3, 4);
				Quaternion<int> B = new(1, 2, 3, 4);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<int> A = new(-1, -2, -3, -4);
				Quaternion<int> B = new(-1, -2, -3, -4);
				Assert.IsTrue(A == B);
			}
			// float
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(1f, 1f, 1f, 1f);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<float> A = new(-1f, -1f, -1f, -1f);
				Quaternion<float> B = new(-1f, -1f, -1f, -1f);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<float> A = new(1f, 2f, 3f, 4f);
				Quaternion<float> B = new(1f, 2f, 3f, 4f);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<float> A = new(-1f, -2f, -3f, -4f);
				Quaternion<float> B = new(-1f, -2f, -3f, -4f);
				Assert.IsTrue(A == B);
			}
			// double
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(1d, 1d, 1d, 1d);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<double> A = new(-1d, -1d, -1d, -1d);
				Quaternion<double> B = new(-1d, -1d, -1d, -1d);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<double> A = new(1d, 2d, 3d, 4d);
				Quaternion<double> B = new(1d, 2d, 3d, 4d);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<double> A = new(-1d, -2d, -3d, -4d);
				Quaternion<double> B = new(-1d, -2d, -3d, -4d);
				Assert.IsTrue(A == B);
			}
			// decimal
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1m);
				Quaternion<decimal> B = new(1m, 1m, 1m, 1m);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<decimal> A = new(-1m, -1m, -1m, -1m);
				Quaternion<decimal> B = new(-1m, -1m, -1m, -1m);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<decimal> A = new(1m, 2m, 3m, 4m);
				Quaternion<decimal> B = new(1m, 2m, 3m, 4m);
				Assert.IsTrue(A == B);
			}
			{
				Quaternion<decimal> A = new(-1m, -2m, -3m, -4m);
				Quaternion<decimal> B = new(-1m, -2m, -3m, -4m);
				Assert.IsTrue(A == B);
			}
		}

		#endregion

		#region Equal_leniency

		[TestMethod]
		public void Equal_leniency()
		{
			// int
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(2, 2, 2, 2);
				Assert.IsTrue(A.Equal(B, 1));
			}
			{
				Quaternion<int> A = new(-1, -1, -1, -1);
				Quaternion<int> B = new(-2, -2, -2, -2);
				Assert.IsTrue(A.Equal(B, 1));
			}
			{
				Quaternion<int> A = new(1, 2, 3, 4);
				Quaternion<int> B = new(2, 3, 4, 5);
				Assert.IsTrue(A.Equal(B, 1));
			}
			{
				Quaternion<int> A = new(-1, -2, -3, -4);
				Quaternion<int> B = new(-2, -3, -4, -5);
				Assert.IsTrue(A.Equal(B, 1));
			}
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(3, 3, 3, 3);
				Assert.IsFalse(A.Equal(B, 1));
			}
			{
				Quaternion<int> A = new(-1, -1, -1, -1);
				Quaternion<int> B = new(-3, -3, -3, -3);
				Assert.IsFalse(A.Equal(B, 1));
			}
			{
				Quaternion<int> A = new(1, 1, 1, 1);
				Quaternion<int> B = new(3, 3, 3, 3);
				Assert.IsTrue(A.Equal(B, 2));
			}
			// float
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(2f, 2f, 2f, 2f);
				Assert.IsTrue(A.Equal(B, 1f));
			}
			{
				Quaternion<float> A = new(-1f, -1f, -1f, -1f);
				Quaternion<float> B = new(-2f, -2f, -2f, -2f);
				Assert.IsTrue(A.Equal(B, 1f));
			}
			{
				Quaternion<float> A = new(1f, 2f, 3f, 4f);
				Quaternion<float> B = new(2f, 3f, 4f, 5f);
				Assert.IsTrue(A.Equal(B, 1f));
			}
			{
				Quaternion<float> A = new(-1f, -2f, -3f, -4f);
				Quaternion<float> B = new(-2f, -3f, -4f, -5f);
				Assert.IsTrue(A.Equal(B, 1f));
			}
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(3f, 3f, 3f, 3f);
				Assert.IsFalse(A.Equal(B, 1f));
			}
			{
				Quaternion<float> A = new(-1f, -1f, -1f, -1f);
				Quaternion<float> B = new(-3f, -3f, -3f, -3f);
				Assert.IsFalse(A.Equal(B, 1f));
			}
			{
				Quaternion<float> A = new(1f, 1f, 1f, 1f);
				Quaternion<float> B = new(3f, 3f, 3f, 3f);
				Assert.IsTrue(A.Equal(B, 2f));
			}
			// double
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(2d, 2d, 2d, 2d);
				Assert.IsTrue(A.Equal(B, 1d));
			}
			{
				Quaternion<double> A = new(-1d, -1d, -1d, -1d);
				Quaternion<double> B = new(-2d, -2d, -2d, -2d);
				Assert.IsTrue(A.Equal(B, 1d));
			}
			{
				Quaternion<double> A = new(1d, 2d, 3d, 4d);
				Quaternion<double> B = new(2d, 3d, 4d, 5d);
				Assert.IsTrue(A.Equal(B, 1d));
			}
			{
				Quaternion<double> A = new(-1d, -2d, -3d, -4d);
				Quaternion<double> B = new(-2d, -3d, -4d, -5d);
				Assert.IsTrue(A.Equal(B, 1d));
			}
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(3d, 3d, 3d, 3d);
				Assert.IsFalse(A.Equal(B, 1d));
			}
			{
				Quaternion<double> A = new(-1d, -1d, -1d, -1d);
				Quaternion<double> B = new(-3d, -3d, -3d, -3d);
				Assert.IsFalse(A.Equal(B, 1d));
			}
			{
				Quaternion<double> A = new(1d, 1d, 1d, 1d);
				Quaternion<double> B = new(3d, 3d, 3d, 3d);
				Assert.IsTrue(A.Equal(B, 2f));
			}
			// decimal
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1);
				Quaternion<decimal> B = new(2m, 2m, 2m, 2);
				Assert.IsTrue(A.Equal(B, 1m));
			}
			{
				Quaternion<decimal> A = new(-1m, -1m, -1m, -1);
				Quaternion<decimal> B = new(-2m, -2m, -2m, -2);
				Assert.IsTrue(A.Equal(B, 1m));
			}
			{
				Quaternion<decimal> A = new(1m, 2m, 3m, 4);
				Quaternion<decimal> B = new(2m, 3m, 4m, 5);
				Assert.IsTrue(A.Equal(B, 1m));
			}
			{
				Quaternion<decimal> A = new(-1m, -2m, -3m, -4);
				Quaternion<decimal> B = new(-2m, -3m, -4m, -5);
				Assert.IsTrue(A.Equal(B, 1m));
			}
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1);
				Quaternion<decimal> B = new(3m, 3m, 3m, 3);
				Assert.IsFalse(A.Equal(B, 1m));
			}
			{
				Quaternion<decimal> A = new(-1m, -1m, -1m, -1);
				Quaternion<decimal> B = new(-3m, -3m, -3m, -3);
				Assert.IsFalse(A.Equal(B, 1m));
			}
			{
				Quaternion<decimal> A = new(1m, 1m, 1m, 1m);
				Quaternion<decimal> B = new(3m, 3m, 3m, 3m);
				Assert.IsTrue(A.Equal(B, 2m));
			}
		}

		#endregion
	}
}
