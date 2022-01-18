using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TargetFrameRate : MonoBehaviour
{

    public int frameRate = 60;
    public Text fps;

    private void Awake()
    {
        frameTimes = new Queue<float>(20);
        Application.targetFrameRate = frameRate;
    }

    private void Update()
    {
        frameTimes.Enqueue(Time.deltaTime);
        if (fps) fps.text = (1f/frameTimes.Average()).ToString();
        if (frameTimes.Count > 19) frameTimes.Dequeue();
    }

    private Queue<float> frameTimes;
    
    
    

}
