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
            Assert.Inconclusive("Test Not Implemented");
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
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Determinent()
        {
            Assert.Inconclusive("Test Not Implemented");
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
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void ReducedEchelon()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Inverse()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Ajoint()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Transpose()
        {
            Assert.Inconclusive("Test Not Implemented");
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
