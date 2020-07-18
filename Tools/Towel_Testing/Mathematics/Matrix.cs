using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
	[TestClass]
	public class Matrix_Testing
	{
		#region FactoryIdentity

		[TestMethod] public void FactoryIdentity()
		{
			{ // 1 x 1
				Matrix<int> a = Matrix<int>.FactoryIdentity(1, 1);
				Matrix<int> b = new int[,]
				{
					{ 1 },
				};
				Assert.IsTrue(a == b);
			}

			{ // 2 x 2
				Matrix<int> a = Matrix<int>.FactoryIdentity(2, 2);
				Matrix<int> b = new int[,]
				{
					{ 1, 0, },
					{ 0, 1, },
				};
				Assert.IsTrue(a == b);
			}

			{ // 3 x 3
				Matrix<int> a = Matrix<int>.FactoryIdentity(3, 3);
				Matrix<int> b = new int[,]
				{
					{ 1, 0, 0, },
					{ 0, 1, 0, },
					{ 0, 0, 1, },
				};
				Assert.IsTrue(a == b);
			}

			{ // 4 x 4
				Matrix<int> a = Matrix<int>.FactoryIdentity(4, 4);
				Matrix<int> b = new int[,]
				{
					{ 1, 0, 0, 0, },
					{ 0, 1, 0, 0, },
					{ 0, 0, 1, 0, },
					{ 0, 0, 0, 1, },
				};
			}

			// Exceptions
			{
				Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryIdentity(0, 1));
				Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryIdentity(1, 0));
			}
		}

		#endregion

		#region FactoryZero

		[TestMethod] public void FactoryZero()
		{
			{ // 1 x 1
				Matrix<int> a = Matrix<int>.FactoryZero(1, 1);
				Matrix<int> b = new int[,]
				{
					{ 0, },
				};
				Assert.IsTrue(a == b);
			}

			{ // 2 x 2
				Matrix<int> a = Matrix<int>.FactoryZero(2, 2);
				Matrix<int> b = new int[,]
				{
					{ 0, 0 },
					{ 0, 0 },
				};
				Assert.IsTrue(a == b);
			}

			{ // 3 x 3
				Matrix<int> a = Matrix<int>.FactoryZero(3, 3);
				Matrix<int> b = new int[,]
				{
					{ 0, 0, 0 },
					{ 0, 0, 0 },
					{ 0, 0, 0 },
				};
				Assert.IsTrue(a == b);
			}

			{ // 4 x 4
				Matrix<int> a = Matrix<int>.FactoryZero(4, 4);
				Matrix<int> b = new int[,]
				{
					{ 0, 0, 0, 0 },
					{ 0, 0, 0, 0 },
					{ 0, 0, 0, 0 },
					{ 0, 0, 0, 0 },
				};
				Assert.IsTrue(a == b);
			}

			// Exceptions
			{
				Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryZero(0, 1));
				Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryZero(1, 0));
			}
		}

		#endregion

		#region Negate

		[TestMethod] public void Negate()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{ -1, -2, -3, },
					{ -4, -5, -6, },
					{ -7, -8, -9, },
				};
				Assert.IsTrue(-A == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ -1f, -2f, -3f, },
					{ -4f, -5f, -6f, },
					{ -7f, -8f, -9f, },
				};
				Assert.IsTrue(-A == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ -1d, -2d, -3d, },
					{ -4d, -5d, -6d, },
					{ -7d, -8d, -9d, },
				};
				Assert.IsTrue(-A == B);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ -1m, -2m, -3m, },
					{ -4m, -5m, -6m, },
					{ -7m, -8m, -9m, },
				};
				Assert.IsTrue(-A == B);
			}
		}

		#endregion

		#region Add

		[TestMethod] public void Add()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{  2,  4,  6, },
					{  8, 10, 12, },
					{ 14, 16, 18, },
				};
				Assert.IsTrue(A + A == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{  2f,  4f,  6f, },
					{  8f, 10f, 12f, },
					{ 14f, 16f, 18f, },
				};
				Assert.IsTrue(A + A == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{  2d,  4d,  6d, },
					{  8d, 10d, 12d, },
					{ 14d, 16d, 18d, },
				};
				Assert.IsTrue(A + A == B);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{  2m,  4m,  6m, },
					{  8m, 10m, 12m, },
					{ 14m, 16m, 18m, },
				};
				Assert.IsTrue(A + A == B);
			}

			// Exceptions
			{
				Matrix<decimal> A = new Matrix<decimal>(2, 2);
				Matrix<decimal> B = new Matrix<decimal>(3, 3);
				Assert.ThrowsException<MathematicsException>(() => A + B);
			}
		}

		#endregion

		#region Subtract

		[TestMethod] public void Subtract()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{  3,  5,  7, },
					{  9, 11, 13, },
					{ 15, 17, 19, },
				};
				Matrix<int> B = new int[,]
				{
					{ 2, 3,  4, },
					{ 5, 6,  7, },
					{ 8, 9, 10, },
				};
				Matrix<int> C = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Assert.IsTrue(A - B == C);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{  3f,  5f,  7f, },
					{  9f, 11f, 13f, },
					{ 15f, 17f, 19f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 2f, 3f,  4f, },
					{ 5f, 6f,  7f, },
					{ 8f, 9f, 10f, },
				};
				Matrix<float> C = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Assert.IsTrue(A - B == C);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{  3d,  5d,  7d, },
					{  9d, 11d, 13d, },
					{ 15d, 17d, 19d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 2d, 3d,  4d, },
					{ 5d, 6d,  7d, },
					{ 8d, 9d, 10d, },
				};
				Matrix<double> C = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Assert.IsTrue(A - B == C);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{  3m,  5m,  7m, },
					{  9m, 11m, 13m, },
					{ 15m, 17m, 19m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 2m, 3m,  4m, },
					{ 5m, 6m,  7m, },
					{ 8m, 9m, 10m, },
				};
				Matrix<decimal> C = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Assert.IsTrue(A - B == C);
			}

			// Exceptions
			{
				Matrix<decimal> A = new Matrix<decimal>(2, 2);
				Matrix<decimal> B = new Matrix<decimal>(3, 3);
				Assert.ThrowsException<MathematicsException>(() => A - B);
			}
		}

		#endregion

		#region Multiply_Matrix

		[TestMethod] public void Multiply_Matrix()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
				};
				Matrix<int> B = new int[,]
				{
					{  7,  8, },
					{  9, 10, },
					{ 11, 12, },
				};
				Matrix<int> C = new int[,]
				{
					{  58,  64, },
					{ 139, 154, },
				};
				Assert.IsTrue(A * B == C);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
				};
				Matrix<float> B = new float[,]
				{
					{  7f,  8f, },
					{  9f, 10f, },
					{ 11f, 12f, },
				};
				Matrix<float> C = new float[,]
				{
					{  58f,  64f, },
					{ 139f, 154f, },
				};
				Assert.IsTrue(A * B == C);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
				};
				Matrix<double> B = new double[,]
				{
					{  7d,  8d, },
					{  9d, 10d, },
					{ 11d, 12d, },
				};
				Matrix<double> C = new double[,]
				{
					{  58d,  64d, },
					{ 139d, 154d, },
				};
				Assert.IsTrue(A * B == C);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{  7m,  8m, },
					{  9m, 10m, },
					{ 11m, 12m, },
				};
				Matrix<decimal> C = new decimal[,]
				{
					{  58m,  64m, },
					{ 139m, 154m, },
				};
				Assert.IsTrue(A * B == C);
			}

			// Exceptions
			{
				Matrix<decimal> A = new Matrix<decimal>(2, 2);
				Matrix<decimal> B = new Matrix<decimal>(3, 3);
				Assert.ThrowsException<MathematicsException>(() => A * B);
			}
		}

		#endregion

		#region Multiply_Vector

		[TestMethod] public void Multiply_Vector()
		{
			// int
			{
				Matrix<int> A = new Matrix<int>(3, 3)
				{
					[0] = 2,
					[1] = 3,
					[2] = -4,
					[3] = 11,
					[4] = 8,
					[5] = 7,
					[6] = 2,
					[7] = 5,
					[8] = 3,
				};
				Vector<int> B = new Vector<int>(3, 7, 5);
				Vector<int> C = new Vector<int>(7, 124, 56);
				Assert.IsTrue(A * B == C);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{  2f, 3f, -4f, },
					{ 11f, 8f,  7f, },
					{  2f, 5f,  3f, },
				};
				Vector<float> B = new Vector<float>(3, 7, 5);
				Vector<float> C = new Vector<float>(7, 124, 56);
				Assert.IsTrue(A * B == C);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{  2d, 3d, -4d, },
					{ 11d, 8d,  7d, },
					{  2d, 5d,  3d, },
				};
				Vector<double> B = new Vector<double>(3d, 7d, 5d);
				Vector<double> C = new Vector<double>(7d, 124d, 56d);
				Assert.IsTrue(A * B == C);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{  2m, 3m, -4m, },
					{ 11m, 8m,  7m, },
					{  2m, 5m,  3m, },
				};
				Vector<decimal> B = new Vector<decimal>(3m, 7m, 5m);
				Vector<decimal> C = new Vector<decimal>(7m, 124m, 56m);
				Assert.IsTrue(A * B == C);
			}

			// Exceptions
			{
				Vector<int> V = new Vector<int>(1);
				Matrix<int> M = new Matrix<int>(2, 2);
				Assert.ThrowsException<MathematicsException>(() => M * V);
			}
		}

		#endregion

		#region Multiply_Scalar

		[TestMethod] public void Multiply_Scalar()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{  2,  4,  6, },
					{  8, 10, 12, },
					{ 14, 16, 18, },
				};
				Assert.IsTrue(A * 2 == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{  2f,  4f,  6f, },
					{  8f, 10f, 12f, },
					{ 14f, 16f, 18f, },
				};
				Assert.IsTrue(A * 2f == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{  2d,  4d,  6d, },
					{  8d, 10d, 12d, },
					{ 14d, 16d, 18d, },
				};
				Assert.IsTrue(A * 2d == B);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{  2m,  4m,  6m, },
					{  8m, 10m, 12m, },
					{ 14m, 16m, 18m, },
				};
				Assert.IsTrue(A * 2m == B);
			}
		}

		#endregion

		#region Divide

		[TestMethod] public void Divide()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{  2,  4,  6, },
					{  8, 10, 12, },
					{ 14, 16, 18, },
				};
				Matrix<int> B = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Assert.IsTrue(A / 2 == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{  2f,  4f,  6f, },
					{  8f, 10f, 12f, },
					{ 14f, 16f, 18f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Assert.IsTrue(A / 2f == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{  2d,  4d,  6d, },
					{  8d, 10d, 12d, },
					{ 14d, 16d, 18d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Assert.IsTrue(A / 2d == B);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{  2m,  4m,  6m, },
					{  8m, 10m, 12m, },
					{ 14m, 16m, 18m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Assert.IsTrue(A / 2m == B);
			}
		}

		#endregion

		#region Power

		[TestMethod] public void Power()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, },
					{ 3, 4, },
				};
				Matrix<int> B = new int[,]
				{
					{ 37,  54, },
					{ 81, 118, },
				};
				Assert.IsTrue((A ^ 3) == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, },
					{ 3f, 4f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 37f,  54f, },
					{ 81f, 118f, },
				};
				Assert.IsTrue((A ^ 3) == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, },
					{ 3d, 4d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 37d,  54d, },
					{ 81d, 118d, },
				};
				Assert.IsTrue((A ^ 3) == B);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, },
					{ 3m, 4m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 37m,  54m, },
					{ 81m, 118m, },
				};
				Assert.IsTrue((A ^ 3) == B);
			}

			// Exceptions
			{
				Matrix<decimal> A = new Matrix<decimal>(2, 3);
				Assert.ThrowsException<MathematicsException>(() => A ^ 3);
			}
		}

		#endregion

		#region Determinant

		[TestMethod] public void DeterminantLaplace()
		{
			// int 
			{
				Matrix<int> a = new int[,]
				{
					{ 1, 2, },
					{ 3, 4, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == -2);
			}
			{
				Matrix<int> a = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == 0);
			}
			// float
			{
				Matrix<float> a = new float[,]
				{
					{ 1f, 2f, },
					{ 3f, 4f, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == -2);
			}
			{
				Matrix<float> a = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == 0);
			}
			// double
			{
				Matrix<double> a = new double[,]
				{
					{ 1d, 2d, },
					{ 3d, 4d, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == -2);
			}
			{
				Matrix<double> a = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == 0);
			}
			// decimal
			{
				Matrix<decimal> a = new decimal[,]
				{
					{ 1m, 2m, },
					{ 3m, 4m, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == -2);
			}
			{
				Matrix<decimal> a = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Assert.IsTrue(a.DeterminantLaplace() == 0);
			}
			// Exceptions
			{
				Matrix<decimal> a = new Matrix<decimal>(2, 3);
				Assert.ThrowsException<MathematicsException>(() => a.DeterminantLaplace());
			}
		}

		[TestMethod] public void DeterminantGaussian()
		{
			// int 
			{
				Matrix<int> a = new int[,]
				{
					{ 1, 2, },
					{ 3, 4, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == -2);
			}
			{
				Matrix<int> a = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == 0);
			}
			// float
			{
				Matrix<float> a = new float[,]
				{
					{ 1f, 2f, },
					{ 3f, 4f, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == -2);
			}
			{
				Matrix<float> a = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == 0);
			}
			// double
			{
				Matrix<double> a = new double[,]
				{
					{ 1d, 2d, },
					{ 3d, 4d, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == -2);
			}
			{
				Matrix<double> a = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == 0);
			}
			// decimal
			{
				Matrix<decimal> a = new decimal[,]
				{
					{ 1m, 2m, },
					{ 3m, 4m, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == -2);
			}
			{
				Matrix<decimal> a = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Assert.IsTrue(a.DeterminantGaussian() == 0);
			}
			// Exceptions
			{
				Matrix<decimal> a = new Matrix<decimal>(2, 3);
				Assert.ThrowsException<MathematicsException>(() => a.DeterminantGaussian());
			}
		}

		#endregion

		#region Trace

		[TestMethod] public void Trace()
		{
			// int
			{
				Matrix<int> a = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Assert.IsTrue(a.Trace() == 15);
			}

			// float
			{
				Matrix<float> a = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Assert.IsTrue(a.Trace() == 15f);
			}

			// double
			{
				Matrix<double> a = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Assert.IsTrue(a.Trace() == 15d);
			}

			// decimal
			{
				Matrix<decimal> a = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Assert.IsTrue(a.Trace() == 15m);
			}

			// Exceptions
			{
				Matrix<decimal> a = new Matrix<decimal>(2, 3);
				Assert.ThrowsException<MathematicsException>(() => a.Trace());
			}
		}

		#endregion

		#region Minor

		[TestMethod] public void Minor()
		{
			#region 2x2 Success
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, },
					{ 3, 4, },
				};

				{ // row 0, column 0
					Matrix<int> B = new int[,]
					{
						{ 4, },
					};
					Assert.IsTrue(A.Minor(0, 0) == B);
				}
				{ // row 0, column 1
					Matrix<int> B = new int[,]
					{
						{ 3, },
					};
					Assert.IsTrue(A.Minor(0, 1) == B);
				}
				{ // row 1, column 0
					Matrix<int> B = new int[,]
					{
						{ 2, },
					};
					Assert.IsTrue(A.Minor(1, 0) == B);
				}
				{ // row 1, column 1
					Matrix<int> B = new int[,]
					{
						{ 1, },
					};
					Assert.IsTrue(A.Minor(1, 1) == B);
				}
			}
			#endregion

			#region 3x3 Success
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3 },
					{ 4, 5, 6 },
					{ 7, 8, 9 },
				};

				{ // row 0, column 0
					Matrix<int> B = new int[,]
					{
						{ 5, 6, },
						{ 8, 9, },
					};
					Assert.IsTrue(A.Minor(0, 0) == B);
				}
				{ // row 0, column 1
					Matrix<int> B = new int[,]
					{
						{ 4, 6, },
						{ 7, 9, },
					};
					Assert.IsTrue(A.Minor(0, 1) == B);
				}
				{ // row 0, column 2
					Matrix<int> B = new int[,]
					{
						{ 4, 5, },
						{ 7, 8, },
					};
					Assert.IsTrue(A.Minor(0, 2) == B);
				}
				{ // row 1, column 0
					Matrix<int> B = new int[,]
					{
						{ 2, 3, },
						{ 8, 9, },
					};
					Assert.IsTrue(A.Minor(1, 0) == B);
				}
				{ // row 1, column 1
					Matrix<int> B = new int[,]
					{
						{ 1, 3, },
						{ 7, 9, },
					};
					Assert.IsTrue(A.Minor(1, 1) == B);
				}
				{ // row 1, column 2
					Matrix<int> B = new int[,]
					{
						{ 1, 2, },
						{ 7, 8, },
					};
					Assert.IsTrue(A.Minor(1, 2) == B);
				}
				{ // row 2, column 0
					Matrix<int> B = new int[,]
					{
						{ 2, 3, },
						{ 5, 6, },
					};
					Assert.IsTrue(A.Minor(2, 0) == B);
				}
				{ // row 2, column 1
					Matrix<int> B = new int[,]
					{
						{ 1, 3, },
						{ 4, 6, },
					};
					Assert.IsTrue(A.Minor(2, 1) == B);
				}
				{ // row 2, column 2
					Matrix<int> B = new int[,]
					{
						{ 1, 2, },
						{ 4, 5, },
					};
					Assert.IsTrue(A.Minor(2, 2) == B);
				}
			}
			#endregion

			#region Exceptions
			{
				// null
				{
					Assert.ThrowsException<ArgumentNullException>(() => Matrix<int>.Minor(null, 0, 0));
				}
				// 1x1
				{
					Matrix<int> A = new int[,]
					{
						{ 1, },
					};
					Assert.ThrowsException<MathematicsException>(() => A.Minor(0, 0));
				}
				// 2x2
				{
					Matrix<int> A = new int[,]
					{
						{ 1, 2, },
						{ 3, 4, },
					};
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 0,  2));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 2,  0));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 2,  2));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 0, -2));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor(-2,  0));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor(-2, -2));
				}
				// 3x3
				{
					Matrix<int> A = new int[,]
					{
						{ 1, 2, 3 },
						{ 4, 5, 6 },
						{ 7, 8, 9 },
					};
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 0,  3));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 3,  0));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 3,  3));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor( 0, -3));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor(-3,  0));
					Assert.ThrowsException<ArgumentOutOfRangeException>(() => A.Minor(-3, -3));
				}
			}
			#endregion
		}

		#endregion

		#region ConcatenateRowWise

		[TestMethod] public void ConcatenateRowWise()
		{
			{ // [2 x 2] + [2 x 2] = [2 x 4]
				Matrix<int> A = new int[,]
				{
					{ 2, 2, },
					{ 2, 2, },
				};
				Matrix<int> B = new int[,]
				{
					{ 3, 3, },
					{ 3, 3, },
				};
				Matrix<int> C = new int[,]
				{
					{ 2, 2, 3, 3, },
					{ 2, 2, 3, 3, },
				};
				Assert.IsTrue(A.ConcatenateRowWise(B) == C);
			}
			{ // [2 x 3] + [2 x 1] = [2 x 3]
				Matrix<int> A = new int[,]
				{
					{ 2, 2, 2, },
					{ 2, 2, 2, },
				};
				Matrix<int> B = new int[,]
				{
					{ 3, },
					{ 3, },
				};
				Matrix<int> C = new int[,]
				{
					{ 2, 2, 2, 3, },
					{ 2, 2, 2, 3, },
				};
				Assert.IsTrue(A.ConcatenateRowWise(B) == C);
			}
			{ // [3 x 3] + [3 x 4] = [3 x 7]
				Matrix<int> A = new int[,]
				{
					{ 2, 2, 2, },
					{ 2, 2, 2, },
					{ 2, 2, 2, },
				};
				Matrix<int> B = new int[,]
				{
					{ 3, 3, 3, 3, },
					{ 3, 3, 3, 3, },
					{ 3, 3, 3, 3, },
				};
				Matrix<int> C = new int[,]
				{
					{ 2, 2, 2, 3, 3, 3, 3, },
					{ 2, 2, 2, 3, 3, 3, 3, },
					{ 2, 2, 2, 3, 3, 3, 3, },
				};
				Assert.IsTrue(A.ConcatenateRowWise(B) == C);
			}
		}

		#endregion

		#region Echelon

#if false

		[TestMethod] public void Echelon()
		{
			// float
			{ 
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, },
					{ 3f, 4f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 1f, 2f, },
					{ 0f, 1f, },
				};
				Assert.IsTrue(A.Echelon() == B);
			}
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 0f, 1f, 2f, },
					{ 0f, 0f, 0f, },
				};
				Assert.IsTrue(A.Echelon() == B);
			}
			// double
			{ 
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, },
					{ 3d, 4d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 1d, 2d, },
					{ 0d, 1d, },
				};
				Assert.IsTrue(A.Echelon() == B);
			}
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 0d, 1d, 2d, },
					{ 0d, 0d, 0d, },
				};
				Assert.IsTrue(A.Echelon() == B);
			}

			// decimal
			{ 
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, },
					{ 3m, 4m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 1m, 2m, },
					{ 0m, 1m, },
				};
				Assert.IsTrue(A.Echelon() == B);
			}
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 0m, 1m, 2m, },
					{ 0m, 0m, 0m, },
				};
				Assert.IsTrue(A.Echelon() == B);
			}
		}

#endif

		#endregion

		#region ReducedEchelon

		[TestMethod] public void ReducedEchelon()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{ 1, 0, -1, },
					{ 0, 1,  2, },
					{ 0, 0,  0, },
				};
				Assert.IsTrue(A.ReducedEchelon() == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 1f, 0f, -1f, },
					{ 0f, 1f,  2f, },
					{ 0f, 0f,  0f, },
				};
				Assert.IsTrue(A.ReducedEchelon() == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 1d, 0d, -1d, },
					{ 0d, 1d,  2d, },
					{ 0d, 0d,  0d, },
				};
				Assert.IsTrue(A.ReducedEchelon() == B);
			}
			{ // decimal
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 1m, 0m, -1m, },
					{ 0m, 1m,  2m, },
					{ 0m, 0m,  0m, },
				};
				Assert.IsTrue(A.ReducedEchelon() == B);
			}

		}

		#endregion

		#region Inverse

		[TestMethod] public void Inverse()
		{
			{ // float
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 0f, 4f, 5f, },
					{ 1f, 0f, 6f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 12f / 11f, -6f / 11f, -1f / 11f, },
					{  5f / 22f,  3f / 22f, -5f / 22f, },
					{ -2f / 11f,  1f / 11f,  2f / 11f, },
				};
				Assert.IsTrue(A.Inverse() == B);
			}
			{ // double
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 0d, 4d, 5d, },
					{ 1d, 0d, 6d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 12d / 11d, -6d / 11d, -1d / 11d, },
					{  5d / 22d,  3d / 22d, -5d / 22d, },
					{ -2d / 11d,  1d / 11d,  2d / 11d, },
				};
				Assert.IsTrue(A.Inverse() == B);
			}
		}

		#endregion

		#region Ajoint

		[TestMethod] public void Ajoint()
		{
			{   // int
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{ -3,   6, -3, },
					{  6, -12,  6, },
					{ -3,   6, -3, },
				};
				Assert.IsTrue(A.Adjoint() == B);
			}
			{ // float
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ -3f,   6f, -3f, },
					{  6f, -12f,  6f, },
					{ -3f,   6f, -3f, },
				};
				Assert.IsTrue(A.Adjoint().Equal(B, 0.01f));
			}
			{ // double
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ -3d,   6d, -3d, },
					{  6d, -12d,  6d, },
					{ -3d,   6d, -3d, },
				};
				Assert.IsTrue(A.Adjoint().Equal(B, 0.01d));
			}
			{ // decimal
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ -3m,   6m, -3m, },
					{  6m, -12m,  6m, },
					{ -3m,   6m, -3m, },
				};
				Assert.IsTrue(A.Adjoint() == B);
			}
		}

		#endregion

		#region Transpose

		[TestMethod] public void Transpose()
		{
			{
				Matrix<int> A = new int[,]
				{
					{ 1 },
				};
				Matrix<int> B = new int[,]
				{
					{ 1 },
				};
				Assert.IsTrue(A.Transpose() == B);
			}
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, },
					{ 3, 4, },
				};
				Matrix<int> B = new int[,]
				{
					{ 1, 3, },
					{ 2, 4, },
				};
				Assert.IsTrue(A.Transpose() == B);
			}
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
				};
				Matrix<int> B = new int[,]
				{
					{ 1, 4, },
					{ 2, 5, },
					{ 3, 6, },
				};
				Assert.IsTrue(A.Transpose() == B);
			}
		}

		#endregion

		#region DecomposeLowerUpper

		[TestMethod] public void DecomposeLowerUpper()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Rotate

		[TestMethod] public void Rotate()
		{
			Assert.Inconclusive("Test Not Implemented");
		}

		#endregion

		#region Equal

		[TestMethod] public void Equal()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Assert.IsTrue(A == B);
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Assert.IsTrue(A == B);
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Assert.IsTrue(A == B);
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Assert.IsTrue(A == B);
			}
		}

		#endregion

		#region Equal_Leniency

		[TestMethod] public void Equal_Leniency()
		{
			// int
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{ 2, 3, 4, },
					{ 5, 6, 7, },
					{ 8, 9, 10, },
				};
				Assert.IsTrue(A.Equal(B, 1));
			}
			{
				Matrix<int> A = new int[,]
				{
					{ 1, 2, 3, },
					{ 4, 5, 6, },
					{ 7, 8, 9, },
				};
				Matrix<int> B = new int[,]
				{
					{ 3, 4, 5, },
					{ 6, 7, 8, },
					{ 9, 10, 11, },
				};
				Assert.IsFalse(A.Equal(B, 1));
			}

			// float
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 2f, 3f, 4f, },
					{ 5f, 6f, 7f, },
					{ 8f, 9f, 10f, },
				};
				Assert.IsTrue(A.Equal(B, 1f));
			}
			{
				Matrix<float> A = new float[,]
				{
					{ 1f, 2f, 3f, },
					{ 4f, 5f, 6f, },
					{ 7f, 8f, 9f, },
				};
				Matrix<float> B = new float[,]
				{
					{ 3f, 4f, 5f, },
					{ 6f, 7f, 8f, },
					{ 9f, 10f, 11f, },
				};
				Assert.IsFalse(A.Equal(B, 1f));
			}

			// double
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 2d, 3d, 4d, },
					{ 5d, 6d, 7d, },
					{ 8d, 9d, 10d, },
				};
				Assert.IsTrue(A.Equal(B, 1d));
			}
			{
				Matrix<double> A = new double[,]
				{
					{ 1d, 2d, 3d, },
					{ 4d, 5d, 6d, },
					{ 7d, 8d, 9d, },
				};
				Matrix<double> B = new double[,]
				{
					{ 3d, 4d, 5d, },
					{ 6d, 7d, 8d, },
					{ 9d, 10d, 11d, },
				};
				Assert.IsFalse(A.Equal(B, 1d));
			}

			// decimal
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 2m, 3m, 4m, },
					{ 5m, 6m, 7m, },
					{ 8m, 9m, 10m, },
				};
				Assert.IsTrue(A.Equal(B, 1m));
			}
			{
				Matrix<decimal> A = new decimal[,]
				{
					{ 1m, 2m, 3m, },
					{ 4m, 5m, 6m, },
					{ 7m, 8m, 9m, },
				};
				Matrix<decimal> B = new decimal[,]
				{
					{ 3m, 4m, 5m, },
					{ 6m, 7m, 8m, },
					{ 9m, 10m, 11m, },
				};
				Assert.IsFalse(A.Equal(B, 1m));
			}
		}

		#endregion

		#region Github Issue #53

		public class MyFloat
		{
			private float value;
			public MyFloat(float value)
			{
				this.value = value;
			}

			public static MyFloat operator *(MyFloat a, MyFloat b) => new MyFloat(a.value * b.value);
			public static MyFloat operator +(MyFloat a, MyFloat b) => new MyFloat(a.value + b.value);
			public static MyFloat operator /(MyFloat a, MyFloat b) => new MyFloat(a.value / b.value);
			public static MyFloat operator -(MyFloat a, MyFloat b) => new MyFloat(a.value - b.value);
			public static MyFloat Zero => new MyFloat(0);
			public static implicit operator MyFloat(float a) => new MyFloat(a);
			public static implicit operator float(MyFloat a) => a.value;
			public static implicit operator MyFloat(int a) => new MyFloat(a); // this one is needed for Constant<T>.Zero

			// additional necesary operators beyond the provided example in the github issue
			public static bool operator <=(MyFloat a, MyFloat b) => a.value <= b.value;
			public static bool operator >=(MyFloat a, MyFloat b) => a.value >= b.value;
			public static bool operator ==(MyFloat a, MyFloat b) => a.value == b.value;
			public static bool operator !=(MyFloat a, MyFloat b) => a.value != b.value;
			public static MyFloat operator -(MyFloat a) => new MyFloat(-a.value);
		}

		[TestMethod] public void GithubIssue53()
		{
			var a = new Matrix<MyFloat>(3, 5);
			for (int i = 0; i < a.Rows; i++)
				for (int j = 0; j < a.Columns; j++)
					a[i, j] = 3;

			var b = new Matrix<MyFloat>(5, 6);
			for (int i = 0; i < b.Rows; i++)
				for (int j = 0; j < b.Columns; j++)
					b[i, j] = 5;

			var c = a * b;

			Matrix<MyFloat> expectedResult = new MyFloat[,]
			{
				{ 75,  75,  75,  75,  75,  75, },
				{ 75,  75,  75,  75,  75,  75, },
				{ 75,  75,  75,  75,  75,  75, },
			};
			Assert.IsTrue(c == expectedResult);
		}

		#endregion
	}
}
