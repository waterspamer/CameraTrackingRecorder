using System;
using System.Collections;
using System.Collections.Generic;
using Persistent;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public void SetRecordFPS(Slider slider) => 
        SettingsManager.GetInstance().SetFPS((int)slider.value);
}
