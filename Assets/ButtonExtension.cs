using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonExtension : Button
{

    [SerializeField] public GameObject circle;
    // Start is called before the first frame update
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
       // var circleRect = circle.rect;
        //circleRect.size *= 2;
        //circle.transform.localScale *= 2;
        circle.GetComponent<RectTransform>().position = eventData.position;
        StartCoroutine(scalingRoutine);
    }

    private IEnumerator scalingRoutine;

    protected override void Awake()
    {
        base.Awake();
        scalingRoutine = CircleScaling();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        StopCoroutine(scalingRoutine);
        circle.transform.localScale = Vector3.one * .01f;
        GetComponent<Animator>().enabled = true;
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
