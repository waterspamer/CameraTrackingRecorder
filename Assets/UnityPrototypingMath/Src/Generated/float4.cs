// This file is autogenerated. Edit VectorN.tt instead.
namespace AntilatencyMath {
    using number = System.Single;
    using numberN = float4;
    using numberQ = floatQ;

    #if UNITY_5_3_OR_NEWER
    [System.Serializable]
    #endif
    public partial struct float4 {

        public number x, y, z, w;

        public float4 (number x, number y, number z, number w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public number this[int idx] {
            get {
                CheckIndex(idx);
                switch (idx) {
                    default:
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                }
            }

            set {
                CheckIndex(idx);
                switch (idx) {
                    case 0: x = value; return;
                    case 1: y = value; return;
                    case 2: z = value; return;
                    case 3: w = value; return;
                }
            }
        }

        public number sqrMagnitude {
            get { return x * x + y * y + z * z + w * w; }
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
            z /= mag;
            w /= mag;
        }

        public float2 toFloat2() {
            return new float2(x, y);
        }

        public float3 toFloat3() {
            return new float3(x, y, z);
        }

        public double2 toDouble2() {
            return new double2((double)x, (double)y);
        }

        public double3 toDouble3() {
            return new double3((double)x, (double)y, (double)z);
        }

        public double4 toDouble4() {
            return new double4((double)x, (double)y, (double)z, (double)w);
        }

        public numberQ toFloatQ() {
            return new numberQ(x, y, z, w);
        }

        public number[] toArray() {
            return new number[] { x, y, z, w };
        }

        public static readonly numberN zero =
            new numberN(0, 0, 0, 0);

        public static readonly numberN ones =
            new numberN(1, 1, 1, 1);

        public static readonly numberN posX =
            new numberN(1, 0, 0, 0);

        public static readonly numberN negX =
            new numberN(-1, 0, 0, 0);

        public static readonly numberN posY =
            new numberN(0, 1, 0, 0);

        public static readonly numberN negY =
            new numberN(0, -1, 0, 0);

        public static readonly numberN posZ =
            new numberN(0, 0, 1, 0);

        public static readonly numberN negZ =
            new numberN(0, 0, -1, 0);

        public static readonly numberN posW =
            new numberN(0, 0, 0, 1);

        public static readonly numberN negW =
            new numberN(0, 0, 0, -1);

        public static numberN operator - (numberN a) {
            return new numberN(-a.x, -a.y, -a.z, -a.w);
        }

        public static numberN operator + (numberN a, numberN b) {
            return new numberN(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static numberN operator - (numberN a, numberN b) {
            return new numberN(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static numberN operator * (number a, numberN b) {
            return new numberN(a * b.x, a * b.y, a * b.z, a * b.w);
        }

        public static numberN operator * (numberN a, number b) {
            return new numberN(a.x * b, a.y * b, a.z * b, a.w * b);
        }

        public static numberN operator * (numberN a, numberN b) {
            return new numberN(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public static numberN operator / (numberN a, number b) {
            return new numberN(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public override string ToString() {
            return x.ToString() + " " + y.ToString() + " " + z.ToString() + " " + w.ToString();
        }

        private static void CheckIndex(int idx) {
            if (idx < 0 || idx >= 4)
                throw new System.IndexOutOfRangeException();
        }
    }

    public static partial class Mathx {

        public static numberN lerp(numberN a, numberN b, number t) {
            return (1 - t) * a + t * b;
        }

        public static number dot(numberN a, numberN b) {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static float4x2 outer(numberN a, float2 b) {
            return new float4x2(
                a.x * b.x, a.x * b.y,
                a.y * b.x, a.y * b.y,
                a.z * b.x, a.z * b.y,
                a.w * b.x, a.w * b.y
            );
        }
        public static float4x3 outer(numberN a, float3 b) {
            return new float4x3(
                a.x * b.x, a.x * b.y, a.x * b.z,
                a.y * b.x, a.y * b.y, a.y * b.z,
                a.z * b.x, a.z * b.y, a.z * b.z,
                a.w * b.x, a.w * b.y, a.w * b.z
            );
        }
        public static float4x4 outer(numberN a, float4 b) {
            return new float4x4(
                a.x * b.x, a.x * b.y, a.x * b.z, a.x * b.w,
                a.y * b.x, a.y * b.y, a.y * b.z, a.y * b.w,
                a.z * b.x, a.z * b.y, a.z * b.z, a.z * b.w,
                a.w * b.x, a.w * b.y, a.w * b.z, a.w * b.w
            );
        }
        public static numberN abs(numberN v) {
            var result = new numberN();
            result.x = abs(v.x);
            result.y = abs(v.y);
            result.z = abs(v.z);
            result.w = abs(v.w);

            return result;
        }

        public static numberN sqr(numberN v) {
            var result = new numberN();
            result.x = sqr(v.x);
            result.y = sqr(v.y);
            result.z = sqr(v.z);
            result.w = sqr(v.w);

            return result;
        }

        public static numberN sqrt(numberN v) {
            var result = new numberN();
            result.x = sqrt(v.x);
            result.y = sqrt(v.y);
            result.z = sqrt(v.z);
            result.w = sqrt(v.w);

            return result;
        }

        public static numberN exp(numberN v) {
            var result = new numberN();
            result.x = exp(v.x);
            result.y = exp(v.y);
            result.z = exp(v.z);
            result.w = exp(v.w);

            return result;
        }

        public static numberN log(numberN v) {
            var result = new numberN();
            result.x = log(v.x);
            result.y = log(v.y);
            result.z = log(v.z);
            result.w = log(v.w);

            return result;
        }

        public static numberN sin(numberN v) {
            var result = new numberN();
            result.x = sin(v.x);
            result.y = sin(v.y);
            result.z = sin(v.z);
            result.w = sin(v.w);

            return result;
        }

        public static numberN cos(numberN v) {
            var result = new numberN();
            result.x = cos(v.x);
            result.y = cos(v.y);
            result.z = cos(v.z);
            result.w = cos(v.w);

            return result;
        }

        public static numberN tan(numberN v) {
            var result = new numberN();
            result.x = tan(v.x);
            result.y = tan(v.y);
            result.z = tan(v.z);
            result.w = tan(v.w);

            return result;
        }

        public static numberN plus(numberN a, numberN b) {
            var result = new numberN();
            result.x = plus(a.x, b.x);
            result.y = plus(a.y, b.y);
            result.z = plus(a.z, b.z);
            result.w = plus(a.w, b.w);

            return result;
        }

        public static numberN minus(numberN a, numberN b) {
            var result = new numberN();
            result.x = minus(a.x, b.x);
            result.y = minus(a.y, b.y);
            result.z = minus(a.z, b.z);
            result.w = minus(a.w, b.w);

            return result;
        }

        public static numberN atan2(numberN a, numberN b) {
            var result = new numberN();
            result.x = atan2(a.x, b.x);
            result.y = atan2(a.y, b.y);
            result.z = atan2(a.z, b.z);
            result.w = atan2(a.w, b.w);

            return result;
        }


    }
}
