using System.Collections;
using Persistent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
 
public class ButtonLongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private float holdTime => SettingsManager.GetInstance.longPressTime;
    private bool isSelectingMode => SettingsManager.GetInstance.isSelectingMode;

    public GameObject circleVisual;
    
    
    private bool held = false;
    public UnityEvent onClick = new UnityEvent();
 
    public UnityEvent onLongPress = new UnityEvent();
    
    public UnityEvent onPointerDown = new UnityEvent();
    
    public UnityEvent onSelect = new UnityEvent();



    private bool _dragging;
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging " + gameObject.name);
        
        SettingsManager.GetInstance.listParentRect.OnDrag(eventData);
        CancelInvoke("OnLongPress");
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
        SettingsManager.GetInstance.listParentRect.OnEndDrag(eventData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
        SettingsManager.GetInstance.listParentRect.OnBeginDrag(eventData);
    }
    
    
    public void OnPointerDown(PointerEventData eventData) {
        if (!isSelectingMode)
            Invoke("OnLongPress", holdTime);
        //else onSelect?.Invoke();
        circleVisual.GetComponent<RectTransform>().position = eventData.position;
        onPointerDown?.Invoke();
        held = false;
            //&& !eventData.IsPointerMoving()
            //!eventData.dragging  && 
        
        //else onSelect?.Invoke();
    }
 
    public void OnPointerUp(PointerEventData eventData) {

        CancelInvoke("OnLongPress");
        if (!held && !_dragging && !isSelectingMode)
            onClick.Invoke();
        if (!held && !_dragging && isSelectingMode)
            Invoke("OnLongPress", holdTime);
    }
 
    public void OnPointerExit(PointerEventData eventData) {

        //CancelInvoke("OnLongPress");
    }
 
    private void OnLongPress() {
        held = true;
        onLongPress.Invoke();
    }
    
    

    
    /*

    private Vector2 pointerLastData = Vector2.zero;
    private float lastTouchTime;

    IEnumerator PointerMovementHandler()
    {
#if UNITY_ANDROID
        //pointerLastData = Input.touches[0].position;
#endif
#if UNITY_EDITOR
        pointerLastData = Input.mousePosition;
#endif
        while (lastTouchTime < holdTime)
        {
            yield return new WaitForSeconds(0.016777f);
            lastTouchTime += 0.016777f;
            if ((new Vector2( Input.mousePosition.x, Input.mousePosition.y) - pointerLastData).magnitude > 10f)
            {
                CancelInvoke("OnLongPress");
                yield break;
            }
        }
    }

    private bool _lockedShortClick;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
        onPointerDown?.Invoke();
        if (!isSelectingMode)
        {
            StartCoroutine(PointerMovementHandler());
            Invoke("OnLongPress", holdTime);
        }
            
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging " + gameObject.name);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
        if (!isSelectingMode && lastTouchTime < holdTime)
            onClick?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exit");
        //CancelInvoke("OnLongPress");
    }
    
    */
}