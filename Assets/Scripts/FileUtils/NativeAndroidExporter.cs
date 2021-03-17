using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NativeAndroidExporter : MonoBehaviour
{

    [SerializeField] private UIElementsSelector selector;
    

    public void ShareRecords()
    {
        
        var nativeAndroidObj = new NativeShare();


        foreach (var element in selector.selectedElements)
        {
            var filePath = Path.Combine(Application.persistentDataPath, element.fileName);
            var data = File.ReadAllBytes(filePath);
            var cachePath = Path.Combine( Application.temporaryCachePath, element.fileName );
            File.WriteAllBytes( cachePath, data);
            nativeAndroidObj.AddFile(cachePath);
        }
        nativeAndroidObj.SetSubject( "Collada export" ).SetText( $"Collada (.dae) file(s) generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }
}
