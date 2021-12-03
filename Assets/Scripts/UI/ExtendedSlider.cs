using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ExtendedSlider : Slider
    {
        public UnityEvent onPointerDown;
        public UnityEvent onPointerUp;
        
        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            onPointerDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData) {
            base.OnPointerUp(eventData);
            onPointerUp?.Invoke();
        }
    }
}
