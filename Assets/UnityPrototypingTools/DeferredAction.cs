using System;

namespace Antilatency.UnityPrototypingTools {

    public sealed class DeferredAction : IDisposable {

        public DeferredAction(Action cleanup) {
            _cleanup = cleanup;
        }

        public void Dispose() {
            if (_cleanup == null)
                return;

            _cleanup();
            _cleanup = null;
        }

        private Action _cleanup;
    }
}
