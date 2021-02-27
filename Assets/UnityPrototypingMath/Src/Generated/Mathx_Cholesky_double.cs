// This file is autogenerated. Edit Mathx_Cholesky.tt instead.
namespace AntilatencyMath {

    using T = System.Double;

    public static partial class Mathx {

        public static bool choleskyDecompose(T[,] matrix, T[,] result) {

            T Tolerance = (T)1e-30;

            var nRows = matrix.GetLength(0);
            //var nCols = matrix.GetLength(1);

            for (int i = 0; i < nRows; ++i) {
                for (int j = 0; j < i; ++j) {
                    T s = 0;
                    for (int k = 0; k < j; k++)
                        s += result[i, k] * result[j, k];
                    result[i, j] = (matrix[i, j] - s) / result[j, j];
                }

                T sum = 0;
                for (int k = 0; k < i; k++)
                    sum += result[i, k] * result[i, k];

                sum = matrix[i, i] - sum;
                result[i, i] = Mathx.sqrt(sum);
                if (Mathx.abs(sum) < Tolerance) {
                    return false;
                }
            }
            return true;
        }

        public static T[,] choleskyDecompose(T[,] matrix) {
            var result = new T[matrix.GetLength(0), matrix.GetLength(1)];
            if (!choleskyDecompose(matrix, result)) {
                throw new System.ArithmeticException("Matrix is not positive definite.");
            }

            return result;
        }

        public static void choleskySolve(T[,] decomposition, T[] vector, T[] result) {
            var nRows = decomposition.GetLength(0);

            for (int i = 0; i < nRows; i++) {
                T sum = 0;
                for (int j = 0; j < i; j++)
                    sum += decomposition[i, j] * result[j];

                result[i] = (vector[i] - sum) / decomposition[i, i];
            }

            for (int i = nRows - 1; i >= 0; i--) {
                T sum = 0;
                for (int j = i + 1; j < nRows; j++)
                    sum += decomposition[j, i] * result[j];

                result[i] = (result[i] - sum) / decomposition[i, i];
            }
        }


        public static T[] choleskySolve(T[,] decomposition, T[] vector) {
            var result = new T[vector.Length];
            choleskySolve(decomposition, vector, result);

            return result;
        }
    }
}
