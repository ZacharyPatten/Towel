namespace Towel_Testing.Mathematics
{
	[TestClass]
	public class Vector_Testing
	{
		#region Negate

		[TestMethod]
		public void Negate()
		{
			{
				Vector<int> a = new(1, 2, 3);
				Assert.IsTrue(-a == new Vector<int>(-1, -2, -3));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Assert.IsTrue(-a == new Vector<float>(-1f, -2f, -3f));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Assert.IsTrue(-a == new Vector<double>(-1d, -2d, -3d));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Assert.IsTrue(-a == new Vector<decimal>(-1m, -2m, -3m));
			}
		}

		#endregion

		#region Add

		[TestMethod]
		public void Add()
		{
			{
				Vector<int> a = new(1, 2, 3);
				Vector<int> b = new(1, 2, 3);
				Assert.IsTrue(a + b == new Vector<int>(2, 4, 6));
			}
			{
				Vector<int> a = new(-1, -2, -3);
				Vector<int> b = new(-1, -2, -3);
				Assert.IsTrue(a + b == new Vector<int>(-2, -4, -6));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Vector<float> b = new(1f, 2f, 3f);
				Assert.IsTrue(a + b == new Vector<float>(2f, 4f, 6f));
			}
			{
				Vector<float> a = new(-1f, -2f, -3f);
				Vector<float> b = new(-1f, -2f, -3f);
				Assert.IsTrue(a + b == new Vector<float>(-2f, -4f, -6f));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Vector<double> b = new(1d, 2d, 3d);
				Assert.IsTrue(a + b == new Vector<double>(2d, 4d, 6d));
			}
			{
				Vector<double> a = new(-1d, -2d, -3d);
				Vector<double> b = new(-1d, -2d, -3d);
				Assert.IsTrue(a + b == new Vector<double>(-2d, -4d, -6d));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Vector<decimal> b = new(1m, 2m, 3m);
				Assert.IsTrue(a + b == new Vector<decimal>(2m, 4m, 6m));
			}
			{
				Vector<decimal> a = new(-1m, -2m, -3m);
				Vector<decimal> b = new(-1m, -2m, -3m);
				Assert.IsTrue(a + b == new Vector<decimal>(-2m, -4m, -6m));
			}
		}

		#endregion

		#region Subtract

		[TestMethod]
		public void Subtract()
		{
			{
				Vector<int> a = new(1, 2, 3);
				Vector<int> b = new(-1, -2, -3);
				Assert.IsTrue(a - b == new Vector<int>(2, 4, 6));
			}
			{
				Vector<int> a = new(-1, -2, -3);
				Vector<int> b = new(1, 2, 3);
				Assert.IsTrue(a - b == new Vector<int>(-2, -4, -6));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Vector<float> b = new(-1f, -2f, -3f);
				Assert.IsTrue(a - b == new Vector<float>(2f, 4f, 6f));
			}
			{
				Vector<float> a = new(-1f, -2f, -3f);
				Vector<float> b = new(1f, 2f, 3f);
				Assert.IsTrue(a - b == new Vector<float>(-2f, -4f, -6f));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Vector<double> b = new(-1d, -2d, -3d);
				Assert.IsTrue(a - b == new Vector<double>(2d, 4d, 6d));
			}
			{
				Vector<double> a = new(-1d, -2d, -3d);
				Vector<double> b = new(1d, 2d, 3d);
				Assert.IsTrue(a - b == new Vector<double>(-2d, -4d, -6d));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Vector<decimal> b = new(-1m, -2m, -3m);
				Assert.IsTrue(a - b == new Vector<decimal>(2m, 4m, 6m));
			}
			{
				Vector<decimal> a = new(-1m, -2m, -3m);
				Vector<decimal> b = new(1m, 2m, 3m);
				Assert.IsTrue(a - b == new Vector<decimal>(-2m, -4m, -6m));
			}
		}

		#endregion

		#region Multiply

		[TestMethod]
		public void Multiply()
		{
			{
				Vector<int> a = new(1, 2, 3);
				Assert.IsTrue(a * 2 == new Vector<int>(2, 4, 6));
			}
			{
				Vector<int> a = new(1, 2, 3);
				Assert.IsTrue(a * -2 == new Vector<int>(-2, -4, -6));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Assert.IsTrue(a * 2f == new Vector<float>(2f, 4f, 6f));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Assert.IsTrue(a * -2f == new Vector<float>(-2f, -4f, -6f));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Assert.IsTrue(a * 2d == new Vector<double>(2d, 4d, 6d));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Assert.IsTrue(a * -2d == new Vector<double>(-2d, -4d, -6d));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Assert.IsTrue(a * 2m == new Vector<decimal>(2m, 4m, 6m));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Assert.IsTrue(a * -2m == new Vector<decimal>(-2m, -4m, -6m));
			}
		}

		#endregion

		#region Divide

		[TestMethod]
		public void Divide()
		{
			{
				Vector<int> a = new(2, 4, 6);
				Assert.IsTrue(a / 2 == new Vector<int>(1, 2, 3));
			}
			{
				Vector<int> a = new(2, 4, 6);
				Assert.IsTrue(a / -2 == new Vector<int>(-1, -2, -3));
			}
			{
				Vector<float> a = new(2f, 4f, 6f);
				Assert.IsTrue(a / 2f == new Vector<float>(1f, 2f, 3f));
			}
			{
				Vector<float> a = new(2f, 4f, 6f);
				Assert.IsTrue(a / -2f == new Vector<float>(-1f, -2f, -3f));
			}
			{
				Vector<double> a = new(2d, 4d, 6d);
				Assert.IsTrue(a / 2d == new Vector<double>(1d, 2d, 3d));
			}
			{
				Vector<double> a = new(2d, 4d, 6d);
				Assert.IsTrue(a / -2d == new Vector<double>(-1d, -2d, -3d));
			}
			{
				Vector<decimal> a = new(2m, 4m, 6m);
				Assert.IsTrue(a / 2m == new Vector<decimal>(1m, 2m, 3m));
			}
			{
				Vector<decimal> a = new(2m, 4m, 6m);
				Assert.IsTrue(a / -2m == new Vector<decimal>(-1m, -2m, -3m));
			}
		}

		#endregion

		#region Magnitude

		[TestMethod]
		public void Magnitude()
		{
			{
				Vector<float> a = new(2f, 2f, 2f, 2f);
				Assert.IsTrue(a.Magnitude == 4f);
			}
			{
				Vector<double> a = new(2d, 2d, 2d, 2d);
				Assert.IsTrue(a.Magnitude == 4d);
			}
			{
				Vector<decimal> a = new(2m, 2m, 2m, 2m);
				Assert.IsTrue(a.Magnitude == 4m);
			}
		}

		#endregion

		#region MagnitudeSquared

		[TestMethod]
		public void MagnitudeSquared()
		{
			{
				Vector<float> a = new(2f, 2f, 2f, 2f);
				Assert.IsTrue(a.MagnitudeSquared == 16f);
			}
			{
				Vector<double> a = new(2d, 2d, 2d, 2d);
				Assert.IsTrue(a.MagnitudeSquared == 16d);
			}
			{
				Vector<decimal> a = new(2m, 2m, 2m, 2m);
				Assert.IsTrue(a.MagnitudeSquared == 16m);
			}
		}

		#endregion

		#region CrossProduct

		[TestMethod]
		public void CrossProduct()
		{
			{ // int
				Vector<int> A = new(1, 2, 3);
				Vector<int> B = new(4, 5, 6);
				Vector<int> C = new(-3, 6, -3);
				Assert.IsTrue(A.CrossProduct(B) == C);
			}
			{ // float
				Vector<float> A = new(1f, 2f, 3f);
				Vector<float> B = new(4f, 5f, 6f);
				Vector<float> C = new(-3f, 6f, -3f);
				Assert.IsTrue(A.CrossProduct(B) == C);
			}
			{ // double
				Vector<double> A = new(1d, 2d, 3d);
				Vector<double> B = new(4d, 5d, 6d);
				Vector<double> C = new(-3d, 6d, -3d);
				Assert.IsTrue(A.CrossProduct(B) == C);
			}
			{ // decimal
				Vector<decimal> A = new(1m, 2m, 3m);
				Vector<decimal> B = new(4m, 5m, 6m);
				Vector<decimal> C = new(-3m, 6m, -3m);
				Assert.IsTrue(A.CrossProduct(B) == C);
			}
			{ // dimension missmatch
				Vector<int> A = new(2);
				Vector<int> B = new(3);
				Assert.ThrowsException<MathematicsException>(() => A.CrossProduct(B));
			}
		}

		#endregion

		#region DotProduct

		[TestMethod]
		public void DotProduct()
		{
			{ // int
				Vector<int> A = new(1, 2, 3);
				Vector<int> B = new(4, 5, 6);
				Assert.IsTrue(A.DotProduct(B) == 32);
			}
			{ // float
				Vector<float> A = new(1f, 2f, 3f);
				Vector<float> B = new(4f, 5f, 6f);
				Assert.IsTrue(A.DotProduct(B) == 32f);
			}
			{ // double
				Vector<double> A = new(1d, 2d, 3d);
				Vector<double> B = new(4d, 5d, 6d);
				Assert.IsTrue(A.DotProduct(B) == 32d);
			}
			{ // decimal
				Vector<decimal> A = new(1m, 2m, 3m);
				Vector<decimal> B = new(4m, 5m, 6m);
				Assert.IsTrue(A.DotProduct(B) == 32m);
			}
			{ // dimension missmatch
				Vector<int> A = new(2);
				Vector<int> B = new(3);
				Assert.ThrowsException<MathematicsException>(() => A.DotProduct(B));
			}
		}

		#endregion

		#region Normalize

		[TestMethod]
		public void Normalize()
		{
			{
				Vector<float> a = new(2f, 2f, 2f, 2f);
				Assert.IsTrue(a.Normalize() == new Vector<float>(2f / 4f, 2f / 4f, 2f / 4f, 2f / 4f));
			}
			{
				Vector<double> a = new(2d, 2d, 2d, 2d);
				Assert.IsTrue(a.Normalize() == new Vector<double>(2d / 4d, 2d / 4d, 2d / 4d, 2d / 4d));
			}
			{
				Vector<decimal> a = new(2m, 2m, 2m, 2m);
				Assert.IsTrue(a.Normalize() == new Vector<decimal>(2m / 4m, 2m / 4m, 2m / 4m, 2m / 4m));
			}
		}

		#endregion

		#region Angle_Testing

		[TestMethod]
		public void Angle_Testing()
		{
			// Note: need to update this once the Towel Trig functions are completed (it is using system trig functions)

			{ // float
				Vector<float> a = new(2f, 9f, -3f);
				Vector<float> b = new(-3f, -4f, 8f);
				Angle<float> angle = a.Angle(b, f => new Angle<float>((float)Math.Acos(f), Angle.Units.Radians));
				Angle<float> expected = new(136.2f, Angle.Units.Degrees);
				float angleDegrees = angle[Angle.Units.Degrees];
				float expectedDegrees = expected[Angle.Units.Degrees];
				Assert.IsTrue(EqualToLeniency(angleDegrees, expectedDegrees, 0.1f));
			}
			{ // double
				Vector<double> a = new(2d, 9d, -3d);
				Vector<double> b = new(-3d, -4d, 8d);
				Angle<double> angle = a.Angle(b, d => new Angle<double>(Math.Acos(d), Angle.Units.Radians));
				Angle<double> expected = new(136.2d, Angle.Units.Degrees);
				double angleDegrees = angle[Angle.Units.Degrees];
				double expectedDegrees = expected[Angle.Units.Degrees];
				Assert.IsTrue(EqualToLeniency(angleDegrees, expectedDegrees, 0.1d));
			}
			{ // decimal
				Vector<decimal> a = new(2m, 9m, -3m);
				Vector<decimal> b = new(-3m, -4m, 8m);
				Angle<decimal> angle = a.Angle(b, m => new Angle<decimal>((decimal)Math.Acos((double)m), Angle.Units.Radians));
				Angle<decimal> expected = new(136.2m, Angle.Units.Degrees);
				decimal angleDegrees = angle[Angle.Units.Degrees];
				decimal expectedDegrees = expected[Angle.Units.Degrees];
				Assert.IsTrue(EqualToLeniency(angleDegrees, expectedDegrees, 0.1m));
			}
		}

		#endregion

		#region Projection

		[TestMethod]
		public void Projection()
		{
			{ // float
				Vector<float> A = new(1f, 0f, 3f);
				Vector<float> B = new(-1f, 4f, 2f);
				Vector<float> C = new(1f / 2f, 0f, 3f / 2f);
				Assert.IsTrue(A.Projection(B) == C);
			}
			{ // double
				Vector<double> A = new(1d, 0d, 3d);
				Vector<double> B = new(-1d, 4d, 2d);
				Vector<double> C = new(1d / 2d, 0d, 3d / 2d);
				Assert.IsTrue(A.Projection(B) == C);
			}
			{ // decimal
				Vector<decimal> A = new(1m, 0m, 3m);
				Vector<decimal> B = new(-1m, 4m, 2m);
				Vector<decimal> C = new(1m / 2m, 0m, 3m / 2m);
				Assert.IsTrue(A.Projection(B) == C);
			}
			{ // dimension missmatch
				Vector<float> A = new(2);
				Vector<float> B = new(3);
				Assert.ThrowsException<MathematicsException>(() => A.Projection(B));
			}
		}

		#endregion

		#region RotateBy

		[TestMethod]
		public void RotateBy()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region LinearInterpolation

		[TestMethod]
		public void LinearInterpolation()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region SphereicalInterpolation

		[TestMethod]
		public void SphereicalInterpolation()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region BarycentricInterpolation

		[TestMethod]
		public void BarycentricInterpolation()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Equal

		[TestMethod]
		public void Equal()
		{
			{
				Vector<int> a = new(1, 2, 3);
				Vector<int> b = new(1, 2, 3);
				Assert.IsTrue(a == b);
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Vector<float> b = new(1f, 2f, 3f);
				Assert.IsTrue(a == b);
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Vector<double> b = new(1d, 2d, 3d);
				Assert.IsTrue(a == b);
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Vector<decimal> b = new(1m, 2m, 3m);
				Assert.IsTrue(a == b);
			}
		}

		#endregion

		#region Equal_leniency

		[TestMethod]
		public void Equal_leniency()
		{
			{
				Vector<int> a = new(1, 2, 3);
				Vector<int> b = new(3, 4, 5);
				Assert.IsFalse(a.Equal(b, 1));
			}
			{
				Vector<int> a = new(1, 2, 3);
				Vector<int> b = new(2, 3, 4);
				Assert.IsTrue(a.Equal(b, 1));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Vector<float> b = new(3f, 4f, 5f);
				Assert.IsFalse(a.Equal(b, 1f));
			}
			{
				Vector<float> a = new(1f, 2f, 3f);
				Vector<float> b = new(2f, 3f, 4f);
				Assert.IsTrue(a.Equal(b, 1f));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Vector<double> b = new(3d, 4d, 5d);
				Assert.IsFalse(a.Equal(b, 1d));
			}
			{
				Vector<double> a = new(1d, 2d, 3d);
				Vector<double> b = new(2d, 3d, 4d);
				Assert.IsTrue(a.Equal(b, 1d));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Vector<decimal> b = new(3m, 4m, 5m);
				Assert.IsFalse(a.Equal(b, 1m));
			}
			{
				Vector<decimal> a = new(1m, 2m, 3m);
				Vector<decimal> b = new(2m, 3m, 4m);
				Assert.IsTrue(a.Equal(b, 1m));
			}
		}

		#endregion
	}
}
