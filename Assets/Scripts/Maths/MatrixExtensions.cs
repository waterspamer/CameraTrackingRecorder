using UnityEngine;

namespace Maths
{
    public static class MatrixExtensions
    {
        public static Quaternion ExtractRotation(this Matrix4x4 matrix) =>
            Quaternion.LookRotation(
                matrix.GetColumn(2),
                matrix.GetColumn(1));

        public static Vector3 ExtractPosition(this Matrix4x4 matrix) =>
            matrix.GetColumn(3);

 
        public static Vector3 ExtractScale(this Matrix4x4 matrix)=>
            new Vector3(
                matrix.GetColumn(0).magnitude,
                matrix.GetColumn(1).magnitude,
                matrix.GetColumn(2).magnitude);
    }
}