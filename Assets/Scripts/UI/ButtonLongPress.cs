using Persistent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
 
public class ButtonLongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    public float holdTime => SettingsManager.GetInstance().longPressTime;
 

    private bool held = false;
    public UnityEvent onClick = new UnityEvent();
 
    public UnityEvent onLongPress = new UnityEvent();
 
    public void OnPointerDown(PointerEventData eventData)
    {
        held = false;
        Invoke("OnLongPress", holdTime);
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
 
        if (!held && !eventData.dragging)
            onClick.Invoke();
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }
 
    private void OnLongPress()
    {
        held = true;
        onLongPress.Invoke();
    }
}