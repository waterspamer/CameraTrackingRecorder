#pragma warning disable IDE1006 // Let public methods be named in camelCase.

namespace AntilatencyMath {

    #if UNITY_5_3_OR_NEWER
    public static class UnityConversions {

        public static float3 toFloat3(this UnityEngine.Vector3 v) {
            return new float3(v.x, v.y, v.z);
        }

        public static double3 toDouble3(this UnityEngine.Vector3 v) {
            return new double3(v.x, v.y, v.z);
        }

        public static UnityEngine.Vector3 toVector3(this float2 v) {
            return new UnityEngine.Vector3(v.x, v.y, 0);
        }

        public static UnityEngine.Vector3 toVector3(this float3 v) {
            return new UnityEngine.Vector3(v.x, v.y, v.z);
        }

        public static UnityEngine.Vector2 toVector2(this double2 v)
        {
            return new UnityEngine.Vector2((float)v.x, (float)v.y);
        }

        public static UnityEngine.Vector3 toVector3(this double2 v) {
            return new UnityEngine.Vector3((float)v.x, (float)v.y, 0);
        }

        public static UnityEngine.Vector3 toVector3(this double3 v) {
            return new UnityEngine.Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        public static floatQ toFloatQ(this UnityEngine.Quaternion q) {
            return new floatQ(q.x, q.y, q.z, q.w);
        }

        public static doubleQ toDoubleQ(this UnityEngine.Quaternion q) {
            return new doubleQ(q.x, q.y, q.z, q.w);
        }

        public static UnityEngine.Quaternion toQuaternion(this floatQ q) {
            return new UnityEngine.Quaternion(q.x, q.y, q.z, q.w);
        }

        public static UnityEngine.Quaternion toQuaternion(this doubleQ q) {
            return new UnityEngine.Quaternion((float)q.x, (float)q.y, (float)q.z, (float)q.w);
        }
    }
    #endif
}