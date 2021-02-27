namespace AntilatencyMath {

    using System;

    public static partial class Mathx {

        public static (double, T, double) bisectEx<T>(
                Func<double, T> f1,
                Func<T, double> f2,
                double left,
                double right,
                int maxIterations = int.MaxValue,
                double epsilon = 0,
                double delta = 0) {

            int numIterations = 0;
            double x;
            var y = double.PositiveInfinity;
            T obj;

            do {
                x = (left + right) / 2;
                obj = f1(x);
                y = f2(obj);

                if (numIterations > maxIterations)
                    break;

                if (Mathx.abs(y) < epsilon)
                    break;

                if (Mathx.abs(left - right) < delta)
                    break;

                if (y < 0)
                    right = x;
                else
                    left = x;

                numIterations++;

            } while (true);

            return (x, obj, y);
        }
    }
}
