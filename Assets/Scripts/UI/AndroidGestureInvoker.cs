using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class AndroidGestureInvoker : MonoBehaviour
    {
        public UnityEvent onBackGesture;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                onBackGesture?.Invoke();
        }
    }
}
