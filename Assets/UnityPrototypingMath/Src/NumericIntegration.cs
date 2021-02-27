namespace AntilatencyMath {

    public static partial class Mathx {

        public static double trapezoidal(System.Func<double, double> f, double a, double b, int num) {

            var points = linspace(a, b, num);
            double step = (b - a) / (num - 1);

            double sum = 0.0;
            int idx = 0;
            foreach (var t in points) {
                if (idx == 0 || idx == num - 1)
                    sum += f(t) / 2.0;
                else
                    sum += f(t);

                idx++;
            }

            return sum * step;
        }

        public static double simpson(System.Func<double, double> f, double a, double b, int num) {

            var points = linspace(a, b, num);

            double step = (b - a) / (num - 1);

            double sum = 0.0;
            int idx = 0;
            foreach (var t in points) {

                double k = (idx % 2 == 0) ? 2.0 : 4.0;
                if (idx == 0 || idx == num - 1) {
                    k = 1.0;
                }

                sum += k * f(t);
                idx++;
            }

            return sum / 3 * step;
        }
    }
}
