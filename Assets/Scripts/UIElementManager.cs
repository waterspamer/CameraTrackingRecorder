using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FileUtils;
using Persistent;
using RecordVisualization;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class UIElementManager : MonoBehaviour
{



    [HideInInspector] public string fileName;
    [SerializeField] private Text nameText;


    public void Initialize() =>
        nameText.text = fileName;
    
    
    
    public void AssignReplayMatrices(Matrix4x4[] matrices) =>
        SettingsManager.GetInstance().CurrentReplayMatrixArray = matrices;


    public void StartReplay()
    {
        SettingsManager.GetInstance().CurrentReplayMatrixArray =
            ColladaFileHelper.GetUnityMatricesFromFile(Path.Combine(Application.persistentDataPath, fileName));
        
        (FindObjectOfType(typeof (ReplayManager)) as ReplayManager)?.StartPlaying();
    }

    
    
    public void ShareFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        var data = File.ReadAllBytes(filePath);
        string cachePath = Path.Combine( Application.temporaryCachePath, "colladaExport.dae" );
        File.WriteAllBytes( cachePath, data);
        new NativeShare().AddFile( cachePath )
            .SetSubject( "Collada export" ).SetText( $"Collada (.dae) file generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }
}
