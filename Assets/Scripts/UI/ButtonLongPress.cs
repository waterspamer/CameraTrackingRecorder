using Persistent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
 
public class ButtonLongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private float holdTime => SettingsManager.GetInstance.longPressTime;
    private bool isSelectingMode => SettingsManager.GetInstance.isSelectingMode;


    public bool isSelectMode = false;
    public GameObject circleVisual;
    
    
    private bool held = false;
    public UnityEvent onClick = new UnityEvent();
 
    public UnityEvent onLongPress = new UnityEvent();
    
    public UnityEvent onPointerDown = new UnityEvent();
 
    public void OnPointerDown(PointerEventData eventData) {
        circleVisual.GetComponent<RectTransform>().position = eventData.position;
        onPointerDown?.Invoke();
        held = false;
        if (!eventData.dragging && !eventData.IsPointerMoving() && !isSelectingMode)
            Invoke("OnLongPress", holdTime);
    }
 
    public void OnPointerUp(PointerEventData eventData) {
        CancelInvoke("OnLongPress");
        if (!held && !eventData.dragging && !isSelectingMode)
            onClick.Invoke();
        if (!held && !eventData.dragging && isSelectingMode)
            Invoke("OnLongPress", holdTime);
    }
 
    public void OnPointerExit(PointerEventData eventData) {
        CancelInvoke("OnLongPress");
    }
 
    private void OnLongPress() {
        held = true;
        onLongPress.Invoke();
    }
}