using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FPSSetter : MonoBehaviour
{

    [SerializeField] private Text fpsText;

    private Slider slider =>
        GetComponent<Slider>();


    public void SetFPSText() =>
        fpsText.text =  $"FPS : {slider.value.ToString(CultureInfo.InvariantCulture)}";
}
