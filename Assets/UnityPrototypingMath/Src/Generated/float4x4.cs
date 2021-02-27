// This file is autogenerated. Edit MatrixMxN.tt instead.
namespace AntilatencyMath {
    using number = System.Single;
    using numberM = float4;
    using numberN = float4;
    using numberD = float4;
    using numberMxN = float4x4;
    using numberNxM = float4x4;

#if UNITY_5_3_OR_NEWER
    [System.Serializable]
#endif
    public partial struct float4x4 {

        public numberN row0, row1, row2, row3;

        public float4x4(
                number val00,
                number val01,
                number val02,
                number val03,
                number val10,
                number val11,
                number val12,
                number val13,
                number val20,
                number val21,
                number val22,
                number val23,
                number val30,
                number val31,
                number val32,
                number val33) {

            row0.x = val00;
            row0.y = val01;
            row0.z = val02;
            row0.w = val03;
            row1.x = val10;
            row1.y = val11;
            row1.z = val12;
            row1.w = val13;
            row2.x = val20;
            row2.y = val21;
            row2.z = val22;
            row2.w = val23;
            row3.x = val30;
            row3.y = val31;
            row3.z = val32;
            row3.w = val33;
        }

        private float4x4(number val) {
            row0.x = val;
            row0.y = val;
            row0.z = val;
            row0.w = val;
            row1.x = val;
            row1.y = val;
            row1.z = val;
            row1.w = val;
            row2.x = val;
            row2.y = val;
            row2.z = val;
            row2.w = val;
            row3.x = val;
            row3.y = val;
            row3.z = val;
            row3.w = val;
        }

        public float4x4(numberN row0, numberN row1, numberN row2, numberN row3) {
            this.row0 = row0;
            this.row1 = row1;
            this.row2 = row2;
            this.row3 = row3;
        }

        public static numberMxN withRows(numberN row0, numberN row1, numberN row2, numberN row3) {
            return new numberMxN(row0, row1, row2, row3);
        }

        public static numberMxN withCols(numberM col0, numberM col1, numberM col2, numberM col3) {
            return new numberMxN(
                col0.x,
                col1.x,
                col2.x,
                col3.x,
                col0.y,
                col1.y,
                col2.y,
                col3.y,
                col0.z,
                col1.z,
                col2.z,
                col3.z,
                col0.w,
                col1.w,
                col2.w,
                col3.w
            );
        }

        public static numberMxN diagonal(numberD v) {
            var result = new numberMxN();
            result.row0.x = v.x;
            result.row1.y = v.y;
            result.row2.z = v.z;
            result.row3.w = v.w;
        
            return result;
        }

        public static numberMxN diagonal(number v) {
            var result = new numberMxN();
            result.row0.x = v;
            result.row1.y = v;
            result.row2.z = v;
            result.row3.w = v;
        
            return result;
        }

        public static numberMxN filled(number val) {
            return new numberMxN(val);
        }

        public static readonly numberMxN zero = new numberMxN();

        public static readonly numberMxN ones = numberMxN.filled(1);

        public static readonly numberMxN identity = numberMxN.diagonal(1);

        public numberN row(int iRow) {
            CheckRowIndex(iRow);
            switch (iRow) {
                default:
                case 0: return row0;
                case 1: return row1;
                case 2: return row2;
                case 3: return row3;
            }
        }

        public numberM col(int iCol) {
            CheckColIndex(iCol);
            switch (iCol) {
                default:
                case 0: return new numberM(row0.x, row1.x, row2.x, row3.x);
                case 1: return new numberM(row0.y, row1.y, row2.y, row3.y);
                case 2: return new numberM(row0.z, row1.z, row2.z, row3.z);
                case 3: return new numberM(row0.w, row1.w, row2.w, row3.w);
            }
        }

        public void setRow(int iRow, numberN v) {
            CheckRowIndex(iRow);
            switch (iRow) {
                case 0: row0 = v; return;
                case 1: row1 = v; return;
                case 2: row2 = v; return;
                case 3: row3 = v; return;
            }
        }

        public void setCol(int iCol, numberM v) {
            CheckColIndex(iCol);
            switch (iCol) {
                case 0: row0.x = v.x;
                row1.x = v.y;
                row2.x = v.z;
                row3.x = v.w; break;
                case 1: row0.y = v.x;
                row1.y = v.y;
                row2.y = v.z;
                row3.y = v.w; break;
                case 2: row0.z = v.x;
                row1.z = v.y;
                row2.z = v.z;
                row3.z = v.w; break;
                case 3: row0.w = v.x;
                row1.w = v.y;
                row2.w = v.z;
                row3.w = v.w; break;
            }
        }

        public number this[int iRow, int iCol] { 
            get {
                CheckRowIndex(iRow);
                CheckColIndex(iCol);
                var idx = 4 * iRow + iCol;
                switch (idx) {
                    default:
                    case 0: return row0.x;
                    case 1: return row0.y;
                    case 2: return row0.z;
                    case 3: return row0.w;
                    case 4: return row1.x;
                    case 5: return row1.y;
                    case 6: return row1.z;
                    case 7: return row1.w;
                    case 8: return row2.x;
                    case 9: return row2.y;
                    case 10: return row2.z;
                    case 11: return row2.w;
                    case 12: return row3.x;
                    case 13: return row3.y;
                    case 14: return row3.z;
                    case 15: return row3.w;
                }
            }

            set {
                CheckRowIndex(iRow);
                CheckColIndex(iCol);
                var idx = 4 * iRow + iCol;
                switch (idx) {
                    default:
                    case 0: row0.x = value; break;
                    case 1: row0.y = value; break;
                    case 2: row0.z = value; break;
                    case 3: row0.w = value; break;
                    case 4: row1.x = value; break;
                    case 5: row1.y = value; break;
                    case 6: row1.z = value; break;
                    case 7: row1.w = value; break;
                    case 8: row2.x = value; break;
                    case 9: row2.y = value; break;
                    case 10: row2.z = value; break;
                    case 11: row2.w = value; break;
                    case 12: row3.x = value; break;
                    case 13: row3.y = value; break;
                    case 14: row3.z = value; break;
                    case 15: row3.w = value; break;
                }
            }
        }

        public double4x4 toDouble4x4() {
            return new double4x4(row0.toDouble4(), row1.toDouble4(), row2.toDouble4(), row3.toDouble4());
        }

        public static numberMxN operator - (numberMxN a) {
            return new numberMxN(-a.row0, -a.row1, -a.row2, -a.row3);
        }

        public static numberMxN operator + (numberMxN a, numberMxN b) {
            return new numberMxN(a.row0 + b.row0, a.row1 + b.row1, a.row2 + b.row2, a.row3 + b.row3);
        }

        public static numberMxN operator - (numberMxN a, numberMxN b) {
            return new numberMxN(a.row0 - b.row0, a.row1 - b.row1, a.row2 - b.row2, a.row3 - b.row3);
        }

        public static numberMxN operator * (number a, numberMxN b) {
            return new numberMxN(a * b.row0, a * b.row1, a * b.row2, a * b.row3);
        }

        public static numberMxN operator * (numberMxN a, number b) {
            return new numberMxN(a.row0 * b, a.row1 * b, a.row2 * b, a.row3 * b);
        }

        public static numberMxN operator / (numberMxN a, number b) {
            return new numberMxN(a.row0 / b, a.row1 / b, a.row2 / b, a.row3 / b);
        }

        public static numberM operator * (numberMxN a, numberN b) {
            return new numberM(Mathx.dot(a.row0, b), Mathx.dot(a.row1, b), Mathx.dot(a.row2, b), Mathx.dot(a.row3, b));
        }

        public static numberN operator * (numberM a, numberMxN b) {
            numberN result = new numberN();
            result += a.x * b.row0;
            result += a.y * b.row1;
            result += a.z * b.row2;
            result += a.w * b.row3;

            return result;
        }

        public static float4x2 operator * (numberMxN a, float4x2 b) {
            var result = float4x2.zero;
            for (int i = 0; i < 4; i++)
            for (int j = 0; j < 2; j++)
                result[i, j] = Mathx.dot(a.row(i), b.col(j));

            return result;
        }

        public static float4x3 operator * (numberMxN a, float4x3 b) {
            var result = float4x3.zero;
            for (int i = 0; i < 4; i++)
            for (int j = 0; j < 3; j++)
                result[i, j] = Mathx.dot(a.row(i), b.col(j));

            return result;
        }

        public static float4x4 operator * (numberMxN a, float4x4 b) {
            var result = float4x4.zero;
            for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                result[i, j] = Mathx.dot(a.row(i), b.col(j));

            return result;
        }

        public numberNxM transpose() {
            var result = new numberNxM();
            for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                result[j, i] = this[i, j];

            return result;
        }

        private static void CheckRowIndex(int iRow) {
            if (iRow < 0 || iRow >= 4)
                throw new System.IndexOutOfRangeException("Matrix index is out of bounds");
        }

        private static void CheckColIndex(int iCol) {
            if (iCol < 0 || iCol > 4)
                throw new System.IndexOutOfRangeException("Matrix index is out of bounds");
        }
    }
}
