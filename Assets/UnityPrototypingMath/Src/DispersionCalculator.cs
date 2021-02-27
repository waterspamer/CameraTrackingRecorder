namespace AntilatencyMath {

    public class DispersionCalculator {

        public DispersionCalculator() {
            count = 0;
            sum = 0;
            sumSquares = 0;
        }

        public void Add(double x) {
            count++;
            sum += x;
            sumSquares += x * x;
        }

        public int count { get; private set; }
        public double sum { get; private set; }
        public double sumSquares { get; private set; }

        public double average => sum / count;

        public double dispersion => sumSquares / count - Mathx.sqr(sum / count);

        public double stdVariance => Mathx.sqrt(dispersion);
    }
}
