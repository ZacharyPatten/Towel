using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
    [TestClass]
    public class Matrix_Testing
    {
        [TestMethod]
        public void FactoryIdentity()
        {
            { // 1 x 1
                Matrix<int> a = Matrix<int>.FactoryIdentity(1, 1);
                Matrix<int> b = new Matrix<int>(1, 1)
                {
                    [0] = 1,
                };
                Assert.IsTrue(a == b);
            }
            { // 2 x 2
                Matrix<int> a = Matrix<int>.FactoryIdentity(2, 2);
                Matrix<int> b = new Matrix<int>(2, 2)
                {
                    [0] = 1, [1] = 0,
                    [2] = 0, [3] = 1,
                };
                Assert.IsTrue(a == b);
            }
            { // 3 x 3
                Matrix<int> a = Matrix<int>.FactoryIdentity(3, 3);
                Matrix<int> b = new Matrix<int>(3, 3)
                {
                    [0] = 1, [1] = 0, [2] = 0,
                    [3] = 0, [4] = 1, [5] = 0,
                    [6] = 0, [7] = 0, [8] = 1,
                };
                Assert.IsTrue(a == b);
            }
            { // 4 x 4
                Matrix<int> a = Matrix<int>.FactoryIdentity(4, 4);
                Matrix<int> b = new Matrix<int>(4, 4)
                {
                    [0]  = 1, [1]  = 0, [2]  = 0, [3]  = 0,
                    [4]  = 0, [5]  = 1, [6]  = 0, [7]  = 0,
                    [8]  = 0, [9]  = 0, [10] = 1, [11] = 0,
                    [12] = 0, [13] = 0, [14] = 0, [15] = 1,
                };
                Assert.IsTrue(a == b);
            }
            { // argument exception
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryIdentity(0, 1));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryIdentity(1, 0));
            }
        }

        [TestMethod]
        public void FactoryZero()
        {
            { // 1 x 1
                Matrix<int> a = Matrix<int>.FactoryZero(1, 1);
                Matrix<int> b = new Matrix<int>(1, 1)
                {
                    [0] = 0,
                };
                Assert.IsTrue(a == b);
            }
            { // 2 x 2
                Matrix<int> a = Matrix<int>.FactoryZero(2, 2);
                Matrix<int> b = new Matrix<int>(2, 2)
                {
                    [0] = 0, [1] = 0,
                    [2] = 0, [3] = 0,
                };
                Assert.IsTrue(a == b);
            }
            { // 3 x 3
                Matrix<int> a = Matrix<int>.FactoryZero(3, 3);
                Matrix<int> b = new Matrix<int>(3, 3)
                {
                    [0] = 0, [1] = 0, [2] = 0,
                    [3] = 0, [4] = 0, [5] = 0,
                    [6] = 0, [7] = 0, [8] = 0,
                };
                Assert.IsTrue(a == b);
            }
            { // 4 x 4
                Matrix<int> a = Matrix<int>.FactoryZero(4, 4);
                Matrix<int> b = new Matrix<int>(4, 4)
                {
                    [0]  = 0, [1]  = 0, [2]  = 0, [3]  = 0,
                    [4]  = 0, [5]  = 0, [6]  = 0, [7]  = 0,
                    [8]  = 0, [9]  = 0, [10] = 0, [11] = 0,
                    [12] = 0, [13] = 0, [14] = 0, [15] = 0,
                };
                Assert.IsTrue(a == b);
            }
            { // argument exception
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryZero(0, 1));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix<int>.FactoryZero(1, 0));
            }
        }

        [TestMethod]
        public void Negate()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { -1, -2, -3, },
                    { -4, -5, -6, },
                    { -7, -8, -9, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(-A == B);
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { -1f, -2f, -3f, },
                    { -4f, -5f, -6f, },
                    { -7f, -8f, -9f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(-A == B);
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { -1d, -2d, -3d, },
                    { -4d, -5d, -6d, },
                    { -7d, -8d, -9d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(-A == B);
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { -1m, -2m, -3m, },
                    { -4m, -5m, -6m, },
                    { -7m, -8m, -9m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(-A == B);
            }
        }

        [TestMethod]
        public void Add()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 2, 4, 6, },
                    { 8, 10, 12, },
                    { 14, 16, 18, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A + A == B);
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 2f, 4f, 6f, },
                    { 8f, 10f, 12f, },
                    { 14f, 16f, 18f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A + A == B);
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 2d, 4d, 6d, },
                    { 8d, 10d, 12d, },
                    { 14d, 16d, 18d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A + A == B);
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 2m, 4m, 6m, },
                    { 8m, 10m, 12m, },
                    { 14m, 16m, 18m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A + A == B);
            }
        }

        [TestMethod]
        public void Subtract()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 3, 5, 7, },
                    { 9, 11, 13, },
                    { 15, 17, 19, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 2, 3, 4, },
                    { 5, 6, 7, },
                    { 8, 9, 10, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                int[,] c = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> C = new Matrix<int>(3, 3, (row, column) => c[row, column]);

                Assert.IsTrue(A - B == C);
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 3f, 5f, 7f, },
                    { 9f, 11f, 13f, },
                    { 15f, 17f, 19f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 2f, 3f, 4f, },
                    { 5f, 6f, 7f, },
                    { 8f, 9f, 10f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                float[,] c = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> C = new Matrix<float>(3, 3, (row, column) => c[row, column]);

                Assert.IsTrue(A - B == C);
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 3d, 5d, 7d, },
                    { 9d, 11d, 13d, },
                    { 15d, 17d, 19d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 2d, 3d, 4d, },
                    { 5d, 6d, 7d, },
                    { 8d, 9d, 10d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                double[,] c = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> C = new Matrix<double>(3, 3, (row, column) => c[row, column]);

                Assert.IsTrue(A - B == C);
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 3m, 5m, 7m, },
                    { 9m, 11m, 13m, },
                    { 15m, 17m, 19m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 2m, 3m, 4m, },
                    { 5m, 6m, 7m, },
                    { 8m, 9m, 10m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                decimal[,] c = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> C = new Matrix<decimal>(3, 3, (row, column) => c[row, column]);

                Assert.IsTrue(A - B == C);
            }
        }

        [TestMethod]
        public void Multiply_Matrix()
        {
            {  // int
                Matrix<int> A = new Matrix<int>(2, 3)
                {
                    [0] = 1, [1] = 2, [2] = 3,
                    [3] = 4, [4] = 5, [5] = 6,
                };

                Matrix<int> B = new Matrix<int>(3, 2)
                {
                    [0] =  7,  [1] =  8,
                    [2] =  9,  [3] = 10,
                    [4] = 11,  [5] = 12,
                };

                Matrix<int> C = new Matrix<int>(2, 2)
                {
                    [0] =  58, [1] =  64,
                    [2] = 139, [3] = 154,
                };

                Assert.IsTrue(A * B == C);
            }
            {  // float
                Matrix<float> A = new Matrix<float>(2, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                };

                Matrix<float> B = new Matrix<float>(3, 2)
                {
                    [0] =  7f,  [1] =  8f,
                    [2] =  9f,  [3] = 10f,
                    [4] = 11f,  [5] = 12f,
                };

                Matrix<float> C = new Matrix<float>(2, 2)
                {
                    [0] =  58f, [1] =  64f,
                    [2] = 139f, [3] = 154f,
                };

                Assert.IsTrue(A * B == C);
            }
            {  // double
                Matrix<double> A = new Matrix<double>(2, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                };

                Matrix<double> B = new Matrix<double>(3, 2)
                {
                    [0] =  7d,  [1] =  8d,
                    [2] =  9d,  [3] = 10d,
                    [4] = 11d,  [5] = 12d,
                };

                Matrix<double> C = new Matrix<double>(2, 2)
                {
                    [0] =  58d, [1] =  64d,
                    [2] = 139d, [3] = 154d,
                };

                Assert.IsTrue(A * B == C);
            }
            {  // decimal
                Matrix<decimal> A = new Matrix<decimal>(2, 3)
                {
                    [0] = 1m, [1] = 2m, [2] = 3m,
                    [3] = 4m, [4] = 5m, [5] = 6m,
                };

                Matrix<decimal> B = new Matrix<decimal>(3, 2)
                {
                    [0] =  7m,  [1] =  8m,
                    [2] =  9m,  [3] = 10m,
                    [4] = 11m,  [5] = 12m,
                };

                Matrix<decimal> C = new Matrix<decimal>(2, 2)
                {
                    [0] =  58m, [1] =  64m,
                    [2] = 139m, [3] = 154m,
                };

                Assert.IsTrue(A * B == C);
            }
            { // Dimension Missmatch
                Matrix<decimal> A = new Matrix<decimal>(2, 2);
                Matrix<decimal> B = new Matrix<decimal>(3, 3);
                Assert.ThrowsException<MathematicsException>(() => A * B);
            }
        }

        [TestMethod]
        public void Multiply_Vector()
        {
            { // int
                Matrix<int> matrix = new Matrix<int>(3, 3)
                {
                    [0] =  2, [1] = 3, [2] = -4,
                    [3] = 11, [4] = 8, [5] =  7,
                    [6] =  2, [7] = 5, [8] =  3,
                };

                Vector<int> vector = new Vector<int>(3, 7, 5);

                Vector<int> result = new Vector<int>(7, 124, 56);

                Assert.IsTrue(result == (matrix * vector));
            }
            { // float
                Matrix<float> matrix = new Matrix<float>(3, 3)
                {
                    [0] =  2f, [1] = 3f, [2] = -4f,
                    [3] = 11f, [4] = 8f, [5] =  7f,
                    [6] =  2f, [7] = 5f, [8] =  3f,
                };

                Vector<float> vector = new Vector<float>(3f, 7f, 5f);

                Vector<float> result = new Vector<float>(7f, 124f, 56f);

                Assert.IsTrue(result == (matrix * vector));
            }
            { // double
                Matrix<double> matrix = new Matrix<double>(3, 3)
                {
                    [0] =  2d, [1] = 3d, [2] = -4d,
                    [3] = 11d, [4] = 8d, [5] =  7d,
                    [6] =  2d, [7] = 5d, [8] =  3d,
                };

                Vector<double> vector = new Vector<double>(3d, 7d, 5d);

                Vector<double> result = new Vector<double>(7d, 124d, 56d);

                Assert.IsTrue(result == (matrix * vector));
            }
            { // decimal
                Matrix<decimal> matrix = new Matrix<decimal>(3, 3)
                {
                    [0] =  2m, [1] = 3m, [2] = -4m,
                    [3] = 11m, [4] = 8m, [5] =  7m,
                    [6] =  2m, [7] = 5m, [8] =  3m,
                };

                Vector<decimal> vector = new Vector<decimal>(3m, 7m, 5m);

                Vector<decimal> result = new Vector<decimal>(7m, 124m, 56m);

                Assert.IsTrue(result == (matrix * vector));
            }
            { // Dimension Mismatch
                Vector<int> V = new Vector<int>(1);
                Matrix<int> M = new Matrix<int>(2, 2);
                Assert.ThrowsException<MathematicsException>(() => M * V);
            }
        }

        [TestMethod]
        public void Multiply_Scalar()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 2, 4, 6, },
                    { 8, 10, 12, },
                    { 14, 16, 18, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A * 2 == B);
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 2f, 4f, 6f, },
                    { 8f, 10f, 12f, },
                    { 14f, 16f, 18f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A * 2f == B);
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 2d, 4d, 6d, },
                    { 8d, 10d, 12d, },
                    { 14d, 16d, 18d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A * 2d == B);
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 2m, 4m, 6m, },
                    { 8m, 10m, 12m, },
                    { 14m, 16m, 18m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A * 2m == B);
            }
        }

        [TestMethod]
        public void Divide()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 2, 4, 6, },
                    { 8, 10, 12, },
                    { 14, 16, 18, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A / 2 == B);
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 2f, 4f, 6f, },
                    { 8f, 10f, 12f, },
                    { 14f, 16f, 18f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A / 2f == B);
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 2d, 4d, 6d, },
                    { 8d, 10d, 12d, },
                    { 14d, 16d, 18d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A / 2d == B);
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 2m, 4m, 6m, },
                    { 8m, 10m, 12m, },
                    { 14m, 16m, 18m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A / 2m == B);
            }
        }

        [TestMethod]
        public void Power()
        {
            { // int
                Matrix<int> A = new Matrix<int>(2, 2)
                {
                    [0] = 1, [1] = 2,
                    [2] = 3, [3] = 4,
                };

                Matrix<int> B = new Matrix<int>(2, 2)
                {
                    [0] = 37, [1] =  54,
                    [2] = 81, [3] = 118,
                };

                Assert.IsTrue((A ^ 3) == B);
            }
            { // float
                Matrix<float> A = new Matrix<float>(2, 2)
                {
                    [0] = 1f, [1] = 2f,
                    [2] = 3f, [3] = 4f,
                };

                Matrix<float> B = new Matrix<float>(2, 2)
                {
                    [0] = 37f, [1] =  54f,
                    [2] = 81f, [3] = 118f,
                };

                Assert.IsTrue((A ^ 3) == B);
            }
            { // double
                Matrix<double> A = new Matrix<double>(2, 2)
                {
                    [0] = 1d, [1] = 2d,
                    [2] = 3d, [3] = 4d,
                };

                Matrix<double> B = new Matrix<double>(2, 2)
                {
                    [0] = 37d, [1] =  54d,
                    [2] = 81d, [3] = 118d,
                };

                Assert.IsTrue((A ^ 3) == B);
            }
            { // decimal
                Matrix<decimal> A = new Matrix<decimal>(2, 2)
                {
                    [0] = 1m, [1] = 2m,
                    [2] = 3m, [3] = 4m,
                };

                Matrix<decimal> B = new Matrix<decimal>(2, 2)
                {
                    [0] = 37m, [1] =  54m,
                    [2] = 81m, [3] = 118m,
                };

                Assert.IsTrue((A ^ 3) == B);
            }
            { // non-square matrix
                Matrix<decimal> A = new Matrix<decimal>(2, 3);
                Assert.ThrowsException<MathematicsException>(() => A ^ 3);
            }
        }

        [TestMethod]
        public void Determinent()
        {
            // 2 x 2 -------------------------
            { // int
                Matrix<int> a = new Matrix<int>(2, 2)
                {
                    [0] = 1, [1] = 2,
                    [2] = 3, [3] = 4,
                };
                Assert.IsTrue(a.Determinent() == -2);
            }
            { // float
                Matrix<float> a = new Matrix<float>(2, 2)
                {
                    [0] = 1f, [1] = 2f,
                    [2] = 3f, [3] = 4f,
                };
                Assert.IsTrue(a.Determinent() == -2f);
            }
            { // double
                Matrix<double> a = new Matrix<double>(2, 2)
                {
                    [0] = 1d, [1] = 2d,
                    [2] = 3d, [3] = 4d,
                };
                Assert.IsTrue(a.Determinent() == -2d);
            }
            { // decimal
                Matrix<decimal> a = new Matrix<decimal>(2, 2)
                {
                    [0] = 1m, [1] = 2m,
                    [2] = 3m, [3] = 4m,
                };
                Assert.IsTrue(a.Determinent() == -2m);
            }

            // 3 x 3 -------------------------
            //{ // int
            //    Matrix<int> a = new Matrix<int>(3, 3)
            //    {
            //        [0] = 1, [1] = 2, [2] = 3,
            //        [3] = 4, [4] = 5, [5] = 6,
            //        [6] = 7, [7] = 8, [8] = 9,
            //    };
            //    Assert.IsTrue(a.Determinent() == 0);
            //}
            { // float
                Matrix<float> a = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                    [6] = 7f, [7] = 8f, [8] = 9f,
                };
                Assert.IsTrue(a.Determinent() == 0f);
            }
            { // double
                Matrix<double> a = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                    [6] = 7d, [7] = 8d, [8] = 9d,
                };
                Assert.IsTrue(a.Determinent() == 0d);
            }
            //{ // decimal
            //    Matrix<decimal> a = new Matrix<decimal>(3, 3)
            //    {
            //        [0] = 1m, [1] = 2m, [2] = 3m,
            //        [3] = 4m, [4] = 5m, [5] = 6m,
            //        [6] = 7m, [7] = 8m, [8] = 9m,
            //    };
            //    Assert.IsTrue(a.Determinent() == 0m);
            //}

            // non-square ------------------------
            {
                Matrix<decimal> a = new Matrix<decimal>(2, 3);
                Assert.ThrowsException<MathematicsException>(() => a.Determinent());
            }
        }
        
        [TestMethod]
        public void Trace()
        {
            { // int
                Matrix<int> a = new Matrix<int>(3, 3)
                {
                    [0] = 1, [1] = 2, [2] = 3,
                    [3] = 4, [4] = 5, [5] = 6,
                    [6] = 7, [7] = 8, [8] = 9,
                };
                Assert.IsTrue(a.Trace() == 15);
            }
            { // float
                Matrix<float> a = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                    [6] = 7f, [7] = 8f, [8] = 9f,
                };
                Assert.IsTrue(a.Trace() == 15f);
            }
            { // double
                Matrix<double> a = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                    [6] = 7d, [7] = 8d, [8] = 9d,
                };
                Assert.IsTrue(a.Trace() == 15d);
            }
            { // decimal
                Matrix<decimal> a = new Matrix<decimal>(3, 3)
                {
                    [0] = 1m, [1] = 2m, [2] = 3m,
                    [3] = 4m, [4] = 5m, [5] = 6m,
                    [6] = 7m, [7] = 8m, [8] = 9m,
                };
                Assert.IsTrue(a.Trace() == 15m);
            }
            { // dimension missmatch
                Matrix<decimal> a = new Matrix<decimal>(2, 3);
                Assert.ThrowsException<MathematicsException>(() => a.Trace());
            }
        }

        [TestMethod]
        public void Minor()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void ConcatenateRowWise()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Echelon()
        {
            // 2 x 2 ------------------------------------
            { // float
                Matrix<float> a = new Matrix<float>(2, 2)
                {
                    [0] = 1f, [1] = 2f,
                    [2] = 3f, [3] = 4f,
                };

                Matrix<float> b = new Matrix<float>(2, 2)
                {
                    [0] = 1f, [1] = 2f,
                    [2] = 0f, [3] = 1f,
                };
                
                Assert.IsTrue(a.Echelon() == b);
            }
            { // double
                Matrix<double> a = new Matrix<double>(2, 2)
                {
                    [0] = 1d, [1] = 2d,
                    [2] = 3d, [3] = 4d,
                };

                Matrix<double> b = new Matrix<double>(2, 2)
                {
                    [0] = 1d, [1] = 2d,
                    [2] = 0d, [3] = 1d,
                };

                Assert.IsTrue(a.Echelon() == b);
            }
            { // decimal
                Matrix<decimal> a = new Matrix<decimal>(2, 2)
                {
                    [0] = 1m, [1] = 2m,
                    [2] = 3m, [3] = 4m,
                };

                Matrix<decimal> b = new Matrix<decimal>(2, 2)
                {
                    [0] = 1m, [1] = 2m,
                    [2] = 0m, [3] = 1m,
                };

                Assert.IsTrue(a.Echelon() == b);
            }

            // 3 x 3 ----------------------------------------
            { // float
                Matrix<float> a = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                    [6] = 7f, [7] = 8f, [8] = 9f,
                };

                Matrix<float> b = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 0f, [4] = 1f, [5] = 2f,
                    [6] = 0f, [7] = 0f, [8] = 0f,
                };
                
                Assert.IsTrue(a.Echelon() == b);
            }
            { // double
                Matrix<double> a = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                    [6] = 7d, [7] = 8d, [8] = 9d,
                };

                Matrix<double> b = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 0d, [4] = 1d, [5] = 2d,
                    [6] = 0d, [7] = 0d, [8] = 0d,
                };
                
                Assert.IsTrue(a.Echelon() == b);
            }

            // Note: decimal needs to be looked at

            //{ // decimal
            //    Matrix<decimal> a = new Matrix<decimal>(3, 3)
            //    {
            //        [0] = 1m, [1] = 2m, [2] = 3m,
            //        [3] = 4m, [4] = 5m, [5] = 6m,
            //        [6] = 7m, [7] = 8m, [8] = 9m,
            //    };

            //    Matrix<decimal> b = new Matrix<decimal>(3, 3)
            //    {
            //        [0] = 1m, [1] = 2m, [2] = 3m,
            //        [3] = 0m, [4] = 1m, [5] = 2m,
            //        [6] = 0m, [7] = 0m, [8] = 0m,
            //    };
              
            //    Assert.IsTrue(a.Echelon().Equal(b, 0.1m));
            //}
        }

        [TestMethod]
        public void ReducedEchelon()
        {
            { // float
                Matrix<float> A = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                    [6] = 7f, [7] = 8f, [8] = 9f,
                };

                Matrix<float> B = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 0f, [2] = -1f,
                    [3] = 0f, [4] = 1f, [5] =  2f,
                    [6] = 0f, [7] = 0f, [8] =  0f,
                };

                Assert.IsTrue(A.ReducedEchelon() == B);
            }
            { // double
                Matrix<double> A = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                    [6] = 7d, [7] = 8d, [8] = 9d,
                };

                Matrix<double> B = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 0d, [2] = -1d,
                    [3] = 0d, [4] = 1d, [5] =  2d,
                    [6] = 0d, [7] = 0d, [8] =  0d,
                };

                Assert.IsTrue(A.ReducedEchelon() == B);
            }
        }

        [TestMethod]
        public void Inverse()
        {
            { // float
                Matrix<float> A = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                    [6] = 7f, [7] = 8f, [8] = 9f,
                };

                Matrix<float> B = new Matrix<float>(3, 3)
                {
                    [0] = -11f/12f, [1] =  1f/3f, [2] = 1f/12f,
                    [3] =   -1f/6f, [4] =  1f/3f, [5] = -1f/6f,
                    [6] =    3f/4f, [7] = -1f/3f, [8] = 1f/12f,
                };

                Assert.IsTrue(A.Inverse().Equal(B, 0.1f));
            }
            { // double
                Matrix<double> A = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                    [6] = 7d, [7] = 8d, [8] = 9d,
                };

                Matrix<double> B = new Matrix<double>(3, 3)
                {
                    [0] = -11d/12d, [1] =  1d/3d, [2] = 1d/12d,
                    [3] =   -1d/6d, [4] =  1d/3d, [5] = -1d/6d,
                    [6] =    3d/4d, [7] = -1d/3d, [8] = 1d/12d,
                };

                Assert.IsTrue(A.Inverse().Equal(B, 0.1d));
            }
        }

        [TestMethod]
        public void Ajoint()
        {
            //{ // int
            //    Matrix<int> A = new Matrix<int>(3, 3)
            //    {
            //        [0] = 1, [1] = 2, [2] = 3,
            //        [3] = 4, [4] = 5, [5] = 6,
            //        [6] = 7, [7] = 8, [8] = 9,
            //    };

            //    Matrix<int> B = new Matrix<int>(3, 3)
            //    {
            //        [0] = -3, [1] =   6, [2] = -3,
            //        [3] =  6, [4] = -12, [5] =  6,
            //        [6] = -3, [7] =   6, [8] = -3,
            //    };

            //    Assert.IsTrue(A.Adjoint() == B);
            //}
            { // float
                Matrix<float> A = new Matrix<float>(3, 3)
                {
                    [0] = 1f, [1] = 2f, [2] = 3f,
                    [3] = 4f, [4] = 5f, [5] = 6f,
                    [6] = 7f, [7] = 8f, [8] = 9f,
                };

                Matrix<float> B = new Matrix<float>(3, 3)
                {
                    [0] = -3f, [1] =   6f, [2] = -3f,
                    [3] =  6f, [4] = -12f, [5] =  6f,
                    [6] = -3f, [7] =   6f, [8] = -3f,
                };

                Assert.IsTrue(A.Adjoint().Equal(B, 0.01f));
            }
            { // double
                Matrix<double> A = new Matrix<double>(3, 3)
                {
                    [0] = 1d, [1] = 2d, [2] = 3d,
                    [3] = 4d, [4] = 5d, [5] = 6d,
                    [6] = 7d, [7] = 8d, [8] = 9d,
                };

                Matrix<double> B = new Matrix<double>(3, 3)
                {
                    [0] = -3d, [1] =   6d, [2] = -3d,
                    [3] =  6d, [4] = -12d, [5] =  6d,
                    [6] = -3d, [7] =   6d, [8] = -3d,
                };

                Assert.IsTrue(A.Adjoint().Equal(B, 0.01d));
            }
            //{ // decimal
            //    Matrix<decimal> A = new Matrix<decimal>(3, 3)
            //    {
            //        [0] = 1m, [1] = 2m, [2] = 3m,
            //        [3] = 4m, [4] = 5m, [5] = 6m,
            //        [6] = 7m, [7] = 8m, [8] = 9m,
            //    };

            //    Matrix<decimal> B = new Matrix<decimal>(3, 3)
            //    {
            //        [0] = -3m, [1] =   6m, [2] = -3m,
            //        [3] =  6m, [4] = -12m, [5] =  6m,
            //        [6] = -3m, [7] =   6m, [8] = -3m,
            //    };

            //    Assert.IsTrue(A.Adjoint() == B);
            //}
        }

        [TestMethod]
        public void Transpose()
        {
            {
                Matrix<int> A = new Matrix<int>(1, 1)
                {
                    [0] = 1,
                };

                Matrix<int> B = new Matrix<int>(1, 1)
                {
                    [0] = 1,
                };

                Assert.IsTrue(A.Transpose() == B);
            }
            {
                Matrix<int> A = new Matrix<int>(2, 2)
                {
                    [0] = 1, [1] = 2,
                    [2] = 3, [3] = 4,
                };

                Matrix<int> B = new Matrix<int>(2, 2)
                {
                    [0] = 1, [1] = 3,
                    [2] = 2, [3] = 4,
                };

                Assert.IsTrue(A.Transpose() == B);
            }
            {
                Matrix<int> A = new Matrix<int>(3, 2)
                {
                    [0] = 1, [1] = 2,
                    [2] = 3, [3] = 4,
                    [4] = 5, [5] = 6,
                };

                Matrix<int> B = new Matrix<int>(2, 3)
                {
                    [0] = 1, [1] = 3, [2] = 5,
                    [3] = 2, [4] = 4, [5] = 6,
                };

                Assert.IsTrue(A.Transpose() == B);
            }
        }

        [TestMethod]
        public void DecomposeLowerUpper()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Rotate()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Equal()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A == B);
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A == B);
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A == B);
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A == B);
            }
        }

        [TestMethod]
        public void Equal_Leniency()
        {
            // int
            {
                int[,] a = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 2, 3, 4, },
                    { 5, 6, 7, },
                    { 8, 9, 10, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A.Equal(B, 1));
            }
            {
                int[,] a = new int[,]
                {
                    { 1, 2, 3, },
                    { 4, 5, 6, },
                    { 7, 8, 9, },
                };
                Matrix<int> A = new Matrix<int>(3, 3, (row, column) => a[row, column]);

                int[,] b = new int[,]
                {
                    { 3, 4, 5, },
                    { 6, 7, 8, },
                    { 9, 10, 11, },
                };
                Matrix<int> B = new Matrix<int>(3, 3, (row, column) => b[row, column]);

                Assert.IsFalse(A.Equal(B, 1));
            }

            // float
            {
                float[,] a = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 2f, 3f, 4f, },
                    { 5f, 6f, 7f, },
                    { 8f, 9f, 10f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A.Equal(B, 1f));
            }
            {
                float[,] a = new float[,]
                {
                    { 1f, 2f, 3f, },
                    { 4f, 5f, 6f, },
                    { 7f, 8f, 9f, },
                };
                Matrix<float> A = new Matrix<float>(3, 3, (row, column) => a[row, column]);

                float[,] b = new float[,]
                {
                    { 3f, 4f, 5f, },
                    { 6f, 7f, 8f, },
                    { 9f, 10f, 11f, },
                };
                Matrix<float> B = new Matrix<float>(3, 3, (row, column) => b[row, column]);

                Assert.IsFalse(A.Equal(B, 1f));
            }

            // double
            {
                double[,] a = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 2d, 3d, 4d, },
                    { 5d, 6d, 7d, },
                    { 8d, 9d, 10d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A.Equal(B, 1d));
            }
            {
                double[,] a = new double[,]
                {
                    { 1d, 2d, 3d, },
                    { 4d, 5d, 6d, },
                    { 7d, 8d, 9d, },
                };
                Matrix<double> A = new Matrix<double>(3, 3, (row, column) => a[row, column]);

                double[,] b = new double[,]
                {
                    { 3d, 4d, 5d, },
                    { 6d, 7d, 8d, },
                    { 9d, 10d, 11d, },
                };
                Matrix<double> B = new Matrix<double>(3, 3, (row, column) => b[row, column]);

                Assert.IsFalse(A.Equal(B, 1d));
            }

            // decimal
            {
                decimal[,] a = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 2m, 3m, 4m, },
                    { 5m, 6m, 7m, },
                    { 8m, 9m, 10m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsTrue(A.Equal(B, 1m));
            }
            {
                decimal[,] a = new decimal[,]
                {
                    { 1m, 2m, 3m, },
                    { 4m, 5m, 6m, },
                    { 7m, 8m, 9m, },
                };
                Matrix<decimal> A = new Matrix<decimal>(3, 3, (row, column) => a[row, column]);

                decimal[,] b = new decimal[,]
                {
                    { 3m, 4m, 5m, },
                    { 6m, 7m, 8m, },
                    { 9m, 10m, 11m, },
                };
                Matrix<decimal> B = new Matrix<decimal>(3, 3, (row, column) => b[row, column]);

                Assert.IsFalse(A.Equal(B, 1m));
            }
        }
    }
}
