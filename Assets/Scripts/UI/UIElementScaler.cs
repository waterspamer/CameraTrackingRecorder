using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementScaler : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * (1 - Mathf.Abs(GetComponent<RectTransform>().position.x - Screen.width / 2f) / 3000f);
    }
}
