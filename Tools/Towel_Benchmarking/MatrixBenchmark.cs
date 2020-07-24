using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Benchmarking
{
    using TS = Matrix<int>;
    public class MatrixBenchmark
    {
        static TS CreateMatrix(int size)
            => new Matrix<int>(size, size, (x, y) => x + y);

        private static TS createdMatrix3 = CreateMatrix(3);
        private static TS createdMatrix6 = CreateMatrix(6);
        private static TS createdMatrix9 = CreateMatrix(9);
        private static TS createdMatrix20 = CreateMatrix(20);
        private static TS createdMatrix50 = CreateMatrix(50);

        [Benchmark] public void MatrixAndLaplace3()
            => createdMatrix3.DeterminantLaplace();
        [Benchmark] public void MatrixAndLaplace6()
            => createdMatrix6.DeterminantLaplace();
        [Benchmark] public void MatrixAndLaplace9()
            => createdMatrix9.DeterminantLaplace();

        [Benchmark] public void MatrixAndGaussian3()
            => createdMatrix3.DeterminantGaussian();

        [Benchmark] public void MatrixAndGaussian6()
            => createdMatrix6.DeterminantGaussian();

        [Benchmark] public void MatrixAndGaussian9()
            => createdMatrix9.DeterminantGaussian();

        [Benchmark] public void CreatingMatrix20()
            => CreateMatrix(20);

        [Benchmark] public void CreatingMatrix50()
            => CreateMatrix(50);

        [Benchmark] public void Transpose20()
            => createdMatrix20.Transpose();

        [Benchmark] public void MatrixAndMultiply6()
            => TS.Multiply(createdMatrix6, createdMatrix6);

        [Benchmark] public void MatrixAndMultiply20()
            => TS.Multiply(createdMatrix20, createdMatrix20);

        [Benchmark] public void MatrixAndAdd6()
            => TS.Add(createdMatrix6, createdMatrix6);

        [Benchmark] public void MatrixAndAdd20()
            => TS.Add(createdMatrix20, createdMatrix20);

        [Benchmark] public void SafeIndexing()
        {
            for (int i = 0; i < createdMatrix9.Rows; i++)
            for (int j = 0; j < createdMatrix9.Columns; j++)
            {
                var c = createdMatrix9[i, j];
            }
        }

        [Benchmark] public void FastIndexing()
        {
            for (int i = 0; i < createdMatrix9.Rows; i++)
            for (int j = 0; j < createdMatrix9.Columns; j++)
            {
                var c = createdMatrix9[i, j];
            }
        }
    }
}
