using UnityEngine;

namespace Antilatency.UnityPrototypingTools {

    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class TransformChangeNotifier : MonoBehaviour {

        public delegate void TransformChangeHandler();

        public event TransformChangeHandler OnTransformChange;

        private void Update() {
            if (transform.hasChanged) {
                var handler = OnTransformChange;
                if (handler != null)
                    handler();

                transform.hasChanged = false;
            }
        }
    }
}
