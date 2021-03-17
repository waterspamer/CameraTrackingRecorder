using System;
using System.Collections;
using System.Collections.Generic;
using Persistent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public TextMeshProUGUI TextMeshPro;

    public ScrollRect listParentRect;

    private void Awake()
    {
        SettingsManager.GetInstance.listParentRect = listParentRect;
    }

    public void SetRecordFPS(Slider slider) => 
        SettingsManager.GetInstance.SetFPS((int)slider.value);


    public void SetFileName() =>
        SettingsManager.GetInstance.SetFileName(TextMeshPro.text);
}
