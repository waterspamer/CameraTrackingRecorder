using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class BlurAnimationController : MonoBehaviour
{

    [Range(0, 5f)]
    public float blurValue = 0;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().material.SetFloat("_Size", blurValue);
    }
}
