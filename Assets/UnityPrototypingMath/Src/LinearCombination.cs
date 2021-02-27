/*EXAMPLE
public class LinearCombinationTest : MonoBehaviour {

    void OnDrawGizmos(){        
        LinearCombination linearCombination = new LinearCombination (
            x => 1f,
            x => x,
            x => x * x        
        );
        LinearCombinationSolver linearCombinationSolver = new LinearCombinationSolver(linearCombination);

        float minX = float.PositiveInfinity;
        float maxX = float.NegativeInfinity;

        foreach (var child in transform) {
            var t = (Transform)child;
            linearCombinationSolver.AddPoint(t.position.x,t.position.y);

            minX = Mathf.Min (minX, t.position.x);
            maxX = Mathf.Max (maxX, t.position.x);
        }
        var parameters = linearCombinationSolver.solve ();

        Handles.color = Color.red;
        Handles.DrawPolyLine(linearCombination.samplePolyLine(parameters,minX,maxX,128));        
    }
}
*/

namespace AntilatencyMath {
    using System;
    using Accord.Math;
    using Accord.Math.Decompositions;

    public class LinearCombination {
        Func<double, double>[] terms;
        public LinearCombination(params Func<double, double>[] terms) {
            this.terms = terms;
        }
        public int N {
            get { return terms.Length; }
        }
        public double[] BaseVector(double x) {
            double[] result = new double[N];
            for (int i = 0; i < N; i++)
                result[i] = terms[i](x);
            return result;
        }

        public double Value(double[] parameters, double x) {
            return Elementwise.Multiply(parameters, BaseVector(x)).Sum();
        }

        public UnityEngine.Vector3[] samplePolyLine(double[] parameters, float from, float to, int numSegments) {
            var polyline = new UnityEngine.Vector3[numSegments + 1];
            float x = from;
            float step = (to - from) / numSegments;

            for (int i = 0; i < polyline.Length; i++) {
                polyline[i] = new UnityEngine.Vector3(x, (float)Value(parameters, x));
                x += step;
            }
            return polyline;
        }

    }

    public class LinearCombinationSolver {
        double[,] A;
        double[] B;
        LinearCombination linearCombination;

        public LinearCombinationSolver(LinearCombination linearCombination) {
            this.linearCombination = linearCombination;
            A = new double[linearCombination.N, linearCombination.N];
            B = new double[linearCombination.N];
        }

        public void AddPoint(double x, double y) {
            var f = linearCombination.BaseVector(x);
            for (int i = 0; i < linearCombination.N; i++) {
                for (int j = 0; j < linearCombination.N; j++) {
                    A[i, j] += f[i] * f[j];
                }
                B[i] += f[i] * y;
            }
        }

        public double[] solve() {
            var cholecky = new CholeskyDecomposition(A);
            return cholecky.Solve(B);
        }
    }
}
