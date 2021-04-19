using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class BackGestureInvoker : MonoBehaviour
    {
        public UnityEvent onBackGestureEvent;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                onBackGestureEvent?.Invoke();
        }
    }
}
