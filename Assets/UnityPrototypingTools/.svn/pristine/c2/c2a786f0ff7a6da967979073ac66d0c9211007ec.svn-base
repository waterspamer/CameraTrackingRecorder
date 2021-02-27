using UnityEngine;

namespace Antilatency.UnityPrototypingTools {

    public class ShowWorldPosition : MonoBehaviour {

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            var p = transform.position;
            UnityEditor.Handles.Label(p, string.Format("({0}, {1}, {2})", p.x, p.y, p.z));
        }
#endif

    }
}
