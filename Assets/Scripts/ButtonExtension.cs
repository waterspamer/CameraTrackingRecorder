using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonExtension : Button, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] public GameObject circle;


    [SerializeField] private UnityEvent onLongPointerDown;
    [SerializeField] private UnityEvent onRegularPointerDown;
    
    // Start is called before the first frame update
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("Pressed");
        _pressed = true;
        StartCoroutine(IncreasePressTimeCounter(eventData));
        
        
    }

    private IEnumerator scalingRoutine;
    private float _pressTime;


    private bool _pressed;
    protected override void Awake()
    {
        base.Awake();
        scalingRoutine = CircleScaling();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
       
        
        base.OnPointerUp(eventData);
        _pressed = false;
        if (_pressTime < 0.3f)
        {
            Debug.Log("ShortPress");
            onRegularPointerDown?.Invoke();
        }

        _pressTime = 0f;
    }

    IEnumerator IncreasePressTimeCounter(PointerEventData eventData)
    {

        while (_pressed)
        {
            Debug.Log(_pressTime);
            yield return new WaitForFixedUpdate();
            _pressTime += Time.fixedDeltaTime;
            if (Vector2.Distance(eventData.position, eventData.pressPosition) > 1f)
            {
                yield break;
            }
                
            if  (_pressTime > 0.3f)
            {
                onLongPointerDown?.Invoke();
                Debug.Log("Long Press");
                //_pressTime = 0f;
                yield break;
            }
        }
        yield break;
    }

    IEnumerator CircleScaling()
    {
        while (true)
        {
            circle.transform.localScale *= 1.11f;
            yield return new WaitForFixedUpdate();
        }
        
    }
}
