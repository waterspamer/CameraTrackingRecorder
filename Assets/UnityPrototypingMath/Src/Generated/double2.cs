// This file is autogenerated. Edit VectorN.tt instead.
namespace AntilatencyMath {
    using number = System.Double;
    using numberN = double2;
    using numberQ = doubleQ;

    #if UNITY_5_3_OR_NEWER
    [System.Serializable]
    #endif
    public partial struct double2 {

        public number x, y;

        public double2 (number x, number y) {
            this.x = x;
            this.y = y;
        }

        public number this[int idx] {
            get {
                CheckIndex(idx);
                switch (idx) {
                    default:
                    case 0: return x;
                    case 1: return y;
                }
            }

            set {
                CheckIndex(idx);
                switch (idx) {
                    case 0: x = value; return;
                    case 1: y = value; return;
                }
            }
        }

        public number sqrMagnitude {
            get { return x * x + y * y; }
        }

        public number magnitude {
            get { return (number)System.Math.Sqrt(sqrMagnitude); }
        }

        public numberN normalized {
            get { return this / magnitude; }
        }

        public void normalize() {
            var mag = magnitude;
            x /= mag;
            y /= mag;
        }

        public float2 toFloat2() {
            return new float2((float)x, (float)y);
        }

        public float3 toFloat3() {
            return new float3((float)x, (float)y, 0);
        }

        public float4 toFloat4() {
            return new float4((float)x, (float)y, 0, 0);
        }

        public double3 toDouble3() {
            return new double3(x, y, 0);
        }

        public double4 toDouble4() {
            return new double4(x, y, 0, 0);
        }

        public number[] toArray() {
            return new number[] { x, y };
        }

        public static readonly numberN zero =
            new numberN(0, 0);

        public static readonly numberN ones =
            new numberN(1, 1);

        public static readonly numberN posX =
            new numberN(1, 0);

        public static readonly numberN negX =
            new numberN(-1, 0);

        public static readonly numberN posY =
            new numberN(0, 1);

        public static readonly numberN negY =
            new numberN(0, -1);

        public static numberN operator - (numberN a) {
            return new numberN(-a.x, -a.y);
        }

        public static numberN operator + (numberN a, numberN b) {
            return new numberN(a.x + b.x, a.y + b.y);
        }

        public static numberN operator - (numberN a, numberN b) {
            return new numberN(a.x - b.x, a.y - b.y);
        }

        public static numberN operator * (number a, numberN b) {
            return new numberN(a * b.x, a * b.y);
        }

        public static numberN operator * (numberN a, number b) {
            return new numberN(a.x * b, a.y * b);
        }

        public static numberN operator * (numberN a, numberN b) {
            return new numberN(a.x * b.x, a.y * b.y);
        }

        public static numberN operator / (numberN a, number b) {
            return new numberN(a.x / b, a.y / b);
        }

        public override string ToString() {
            return x.ToString() + " " + y.ToString();
        }

        private static void CheckIndex(int idx) {
            if (idx < 0 || idx >= 2)
                throw new System.IndexOutOfRangeException();
        }
    }

    public static partial class Mathx {

        public static numberN lerp(numberN a, numberN b, number t) {
            return (1 - t) * a + t * b;
        }

        public static number dot(numberN a, numberN b) {
            return a.x * b.x + a.y * b.y;
        }

        public static double2x2 outer(numberN a, double2 b) {
            return new double2x2(
                a.x * b.x, a.x * b.y,
                a.y * b.x, a.y * b.y
            );
        }
        public static double2x3 outer(numberN a, double3 b) {
            return new double2x3(
                a.x * b.x, a.x * b.y, a.x * b.z,
                a.y * b.x, a.y * b.y, a.y * b.z
            );
        }
        public static double2x4 outer(numberN a, double4 b) {
            return new double2x4(
                a.x * b.x, a.x * b.y, a.x * b.z, a.x * b.w,
                a.y * b.x, a.y * b.y, a.y * b.z, a.y * b.w
            );
        }
        public static numberN abs(numberN v) {
            var result = new numberN();
            result.x = abs(v.x);
            result.y = abs(v.y);

            return result;
        }

        public static numberN sqr(numberN v) {
            var result = new numberN();
            result.x = sqr(v.x);
            result.y = sqr(v.y);

            return result;
        }

        public static numberN sqrt(numberN v) {
            var result = new numberN();
            result.x = sqrt(v.x);
            result.y = sqrt(v.y);

            return result;
        }

        public static numberN exp(numberN v) {
            var result = new numberN();
            result.x = exp(v.x);
            result.y = exp(v.y);

            return result;
        }

        public static numberN log(numberN v) {
            var result = new numberN();
            result.x = log(v.x);
            result.y = log(v.y);

            return result;
        }

        public static numberN sin(numberN v) {
            var result = new numberN();
            result.x = sin(v.x);
            result.y = sin(v.y);

            return result;
        }

        public static numberN cos(numberN v) {
            var result = new numberN();
            result.x = cos(v.x);
            result.y = cos(v.y);

            return result;
        }

        public static numberN tan(numberN v) {
            var result = new numberN();
            result.x = tan(v.x);
            result.y = tan(v.y);

            return result;
        }

        public static numberN plus(numberN a, numberN b) {
            var result = new numberN();
            result.x = plus(a.x, b.x);
            result.y = plus(a.y, b.y);

            return result;
        }

        public static numberN minus(numberN a, numberN b) {
            var result = new numberN();
            result.x = minus(a.x, b.x);
            result.y = minus(a.y, b.y);

            return result;
        }

        public static numberN atan2(numberN a, numberN b) {
            var result = new numberN();
            result.x = atan2(a.x, b.x);
            result.y = atan2(a.y, b.y);

            return result;
        }


    }
}
