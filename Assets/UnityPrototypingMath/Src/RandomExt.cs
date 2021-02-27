namespace AntilatencyMath {
    using System;

    public static class RandomExt {

        public static void ShuffleIotaSequence(this Random rand, int n, Action<int, int> swapAction) {
            // The Fisher-Yates shuffle.

            for (int i = n - 1; i > 0; i--) {
                int k = rand.Next(i + 1); // Boundary is exclusive.
                if (i != k)
                    swapAction(i, k);
            }
        }

        public static void ShuffleArray<T>(this Random rand, T[] array) {
            ShuffleIotaSequence(rand, array.Length, (i, j) => {
                T tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
            });
        }

        public static void ShuffleArrays<T1, T2>(this Random rand, T1[] array1, T2[] array2) {
            int minLength = System.Math.Min(array1.Length, array2.Length);
            ShuffleIotaSequence(rand, minLength, (i, j) => {
                T1 tmp1 = array1[i];
                array1[i] = array1[j];
                array1[j] = tmp1;

                T2 tmp2 = array2[i];
                array2[i] = array2[j];
                array2[j] = tmp2;
            });
        }

        public static double GenerateNormallyDistributedValue(Random rand) {
            // https://stackoverflow.com/questions/218060

            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            return Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
        }

        public static double3 GeneratePointInsideSphere(this Random rand, double radius) {
            // https://math.stackexchange.com/questions/87230

            double u = rand.NextDouble();
            u = Math.Pow(u, 1.0 / 3);

            double3 v = new double3(
                GenerateNormallyDistributedValue(rand),
                GenerateNormallyDistributedValue(rand),
                GenerateNormallyDistributedValue(rand));
            v.normalize();

            return (radius * u) * v;
        }

        public static doubleQ GenerateUnitQuaternion(this Random rand) {
            // https://stackoverflow.com/questions/31600717
            // http://planning.cs.uiuc.edu/node198.html
            // K. Shoemake. Uniform random rotations. Graphics Gems III, pages 124-132, 1992.

            double u1 = rand.NextDouble();
            double u2 = rand.NextDouble() * (2 * Math.PI);
            double u3 = rand.NextDouble() * (2 * Math.PI);
            double k1 = Math.Sqrt(u1);
            double k2 = Math.Sqrt(1 - u1);
            return new doubleQ(
                k2 * Math.Sin(u2),
                k2 * Math.Cos(u2),
                k1 * Math.Sin(u3),
                k1 * Math.Cos(u3));
        }
    }
}
