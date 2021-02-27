using UnityEngine;

namespace Antilatency.UnityPrototypingTools {
    public static class QuaternionH {
        // FIXME: Потеря точности в методе FromRotationVector.
        // Она происходит из-за того, что метод Quaternion.AngleAxis принимает
        // свой угловой аргумент в градусах, а потом внутри делает его неявное
        // преобразование в радианы. Так как наш аргумент уже выражен в радианах,
        // получается лишнее преобразование туда-обратно. В принципе, можно было
        // бы использовать метод Quaternion.AxisAngle, но он помечен как устаревший
        // и я боюсь, что его вовсе уберут из будущих релизов Unity.

        // -- Дополнительные способы создать кватернион --

        public static Quaternion FromVectorPart(Vector3 v, float w = 0f) {
            return new Quaternion(v.x, v.y, v.z, w);
        }

        public static Quaternion FromRotationVector(Vector3 v) {
            return Quaternion.AngleAxis(v.magnitude * Mathf.Rad2Deg, v);
        }

        // -- Дополнительные методы преобразования и немутирующие операции --

        public static Vector3 VectorPart(this Quaternion q) {
            return new Vector3(q.x, q.y, q.z);
        }

        public static float SqrNorm(this Quaternion q) {
            return q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        }

        public static float Norm(this Quaternion q) {
            return Mathf.Sqrt(q.Norm());
        }

        public static Vector4 ToVector4(this Quaternion q) {
            return new Vector4(q.x, q.y, q.z, q.w);
        }

        public static Vector3 ToRotationVector(this Quaternion q) {
            float angleDeg;
            Vector3 axis;
            q.ToAngleAxis(out angleDeg, out axis);

            return axis * (angleDeg * Mathf.Deg2Rad);
        }

        public static Matrix4x4 ToRotationMatrix(this Quaternion q) {
            return Matrix4x4.Rotate(q);
        }

        public static Quaternion Normalized(this Quaternion q) {
            float scale = 1f / q.Norm();
            return new Quaternion(q.x * scale, q.y * scale, q.z * scale, q.w * scale);
        }

        public static Quaternion Conjugate(this Quaternion q) {
            return QuaternionH.FromVectorPart(-q.VectorPart(), q.w);
        }

        public static Quaternion Inverted(this Quaternion q) {
            return Quaternion.Inverse(q);
        }

        public static Quaternion Negated(this Quaternion q) {
            return new Quaternion(-q.x, -q.y, -q.z, -q.w);
        }

        public static Quaternion Scaled(this Quaternion q, float k) {
            return new Quaternion(q.x * k, q.y * k, q.z * k, q.w * k);
        }

        public static Quaternion Add(this Quaternion a, Quaternion b) {
            return new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Quaternion Sub(this Quaternion a, Quaternion b) {
            return new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Quaternion Log(this Quaternion q) {
            Quaternion result = q;
            Log(ref result);
            return result;
        }

        public static Quaternion Exp(this Quaternion q) {
            Quaternion result = q;
            Exp(ref result);
            return result;
        }

        // -- Дополнительные мутирующие операции --

        public static void Normalize(ref Quaternion q) {
            float scale = 1f / q.Norm();
            q.x *= scale;
            q.y *= scale;
            q.z *= scale;
            q.w *= scale;
        }

        public static void Negate(ref Quaternion q) {
            q.w = -q.w;
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
        }

        public static void Scale(ref Quaternion q, float k) {
            q.w *= k;
            q.x *= k;
            q.y *= k;
            q.z *= k;
        }

        public static void Exp(ref Quaternion q) {
            // Источник: https://math.stackexchange.com/a/940427/240038
            float r = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z);
            float et = Mathf.Exp(q.w);
            float s = r >= 0.00001f ? et * Mathf.Sin(r) / r : 0f;
            q.w = et * Mathf.Cos(r);
            q.x *= s;
            q.y *= s;
            q.z *= s;
        }

        public static void Log(ref Quaternion q) {
            float r = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z);
            float t = r > 0.00001f ? Mathf.Atan2(r, q.w) / r : 0f;
            q.w = 0.5f * Mathf.Log(q.w * q.w + q.x * q.x + q.y * q.y + q.z * q.z);
            q.x *= t;
            q.y *= t;
            q.z *= t;
        }

        public static void Pow(ref Quaternion q, float n) {
            Log(ref q);
            Scale(ref q, n);
            Exp(ref q);
        }
    }
}
