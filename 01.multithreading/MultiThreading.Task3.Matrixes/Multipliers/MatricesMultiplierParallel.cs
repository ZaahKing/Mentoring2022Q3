using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MultiThreading.Task3.MatrixMultiplier.Matrices;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var tasks = new List<Task>();
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            for (long i = 0; i < m1.RowCount; i++)
            {
                for (long j = 0; j < m2.ColCount; j++)
                {
                    tasks.Add(CulculateMatrixElement(m1, m2, resultMatrix, i, j));
                }
            }

            Task.WaitAll(tasks.ToArray());
            return resultMatrix;
        }

        private static Task CulculateMatrixElement(IMatrix m1, IMatrix m2, Matrix resultMatrix, long i, long j)
        {
            return Task.Run(() =>
            {
                long sum = 0;
                for (long k = 0; k < m1.ColCount; k++)
                {
                    sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                }

                lock (resultMatrix)
                {
                    resultMatrix.SetElement(i, j, sum);
                }
            });
        }
    }
}
