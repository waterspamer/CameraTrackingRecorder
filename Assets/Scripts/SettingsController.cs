using System;
using System.Collections;
using System.Collections.Generic;
using Persistent;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public Text fileNameText;

    public ScrollRect listParentRect;

    public Text errorMessageObject;

    public SceneManager sceneManager;

    private UnityEvent<string> _onErrorMessageEvent;


    private void Awake()
    {
        _onErrorMessageEvent = new TMP_InputField.SelectionEvent();
        SettingsManager.GetInstance.listParentRect = listParentRect;
        _onErrorMessageEvent.AddListener(errorText=>
        {
            errorMessageObject.text = errorText;
            errorMessageObject.GetComponent<Animator>().Play("DefaultAnim");
        });
    }

    public void SetRecordFPS(Slider slider) => 
        SettingsManager.GetInstance.SetFPS((int)slider.value);


    enum StringCheckResult
    {
        Length,
        Emptiness,
        OK
    }
    
    private StringCheckResult CheckTextForErrors(string text)
    {
        Debug.Log("Invoked");
        if (text.Length < 1)
        {
            _onErrorMessageEvent?.Invoke("Empty file name !");
            return StringCheckResult.Emptiness;
        }

        if (text.Length > 14)
        {
            _onErrorMessageEvent?.Invoke("File name too long !");
            return StringCheckResult.Length;
        }
        return StringCheckResult.OK;
    }
    
    public void SetFileName()
    {
        if (CheckTextForErrors(fileNameText.text) != StringCheckResult.OK) return;
        SettingsManager.GetInstance.SetFileName(fileNameText.text);
        sceneManager.LoadScene("RecordingScene");
    }
}
