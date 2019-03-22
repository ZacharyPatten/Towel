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
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply_Matrix()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply_Vector()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Multiply_Scalar()
        {
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Divide()
        {
            Assert.Inconclusive("Test Not Implemented");
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
            Assert.Inconclusive("Test Not Implemented");
        }

        [TestMethod]
        public void Equal_Leniency()
        {
            Assert.Inconclusive("Test Not Implemented");
        }
    }
}
