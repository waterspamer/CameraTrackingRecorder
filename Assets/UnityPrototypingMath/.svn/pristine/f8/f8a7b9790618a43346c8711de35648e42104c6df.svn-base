namespace AntilatencyMath {
    using System.Collections;

    public interface ILevenbergMarquardtModel {

        int DegreesOfFreedom { get; }

        void CollectResiduals(IResidualCollector collector, bool withDerivatives);

        object CaptureState();

        void ApplyState(object state);

        bool ApplyDelta(double[] deltas, BitArray outOfRange);
    }

    public interface IResidualCollector {
        void AddResidual(double value, double[] derivatives = null);
    } 
}
