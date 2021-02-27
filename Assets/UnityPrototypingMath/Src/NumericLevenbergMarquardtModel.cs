
namespace AntilatencyMath {

    using System.Collections;

    public interface INumericLevenbergMarquardtModel {

        int DegreesOfFreedom { get; }

        double[] CalculateResiduals();

        object CaptureState();

        void ApplyState(object state);

        bool ApplyDelta(double[] deltas, BitArray outOfRange);
    }

    public class NumericLevenbergMarquardtModelAdaptor : ILevenbergMarquardtModel {

        public INumericLevenbergMarquardtModel Model { get; private set; }

        public double FiniteDifferenceDelta { get; private set; }

        public NumericLevenbergMarquardtModelAdaptor(INumericLevenbergMarquardtModel model, double finiteDifferenceDelta) {
            Model = model;
            FiniteDifferenceDelta = finiteDifferenceDelta;
        }

        int ILevenbergMarquardtModel.DegreesOfFreedom => Model.DegreesOfFreedom;

        void ILevenbergMarquardtModel.CollectResiduals(IResidualCollector collector, bool withDerivatives) {

            var residuals = Model.CalculateResiduals();

            if (!withDerivatives) {
                foreach (var r in residuals)
                    collector.AddResidual(r);

                return;
            }

            int numResiduals = residuals.Length;
            int numParameters = Model.DegreesOfFreedom;

            var derivatives = new double[numResiduals][];
            for (int i = 0; i < numResiduals; i++)
                derivatives[i] = new double[numParameters];

            var state = Model.CaptureState();

            var deltas = new double[numParameters];
            for (int parameterIdx = 0; parameterIdx < numParameters; parameterIdx++) {

                deltas[parameterIdx] = FiniteDifferenceDelta;

                Model.ApplyDelta(deltas, null);
                deltas[parameterIdx] = 0;

                var residualsPlusDelta = Model.CalculateResiduals();

                for (int residualIdx = 0; residualIdx < numResiduals; residualIdx++) {
                    derivatives[residualIdx][parameterIdx] =
                        (residualsPlusDelta[residualIdx] - residuals[residualIdx]) / FiniteDifferenceDelta;
                }

                Model.ApplyState(state);
            }

            for (int residualIdx = 0; residualIdx < numResiduals; residualIdx++)
                collector.AddResidual(residuals[residualIdx], derivatives[residualIdx]);
        }

        object ILevenbergMarquardtModel.CaptureState() {
            return Model.CaptureState();
        }

        void ILevenbergMarquardtModel.ApplyState(object state) {
            Model.ApplyState(state);
        }

        bool ILevenbergMarquardtModel.ApplyDelta(double[] deltas, BitArray outOfRange) {
            return Model.ApplyDelta(deltas, outOfRange);
        }
    }
}
