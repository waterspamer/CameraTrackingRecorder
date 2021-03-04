using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FileUtils;
using Maths;
using Persistent;
using RecordVisualization;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class UIElementManager : MonoBehaviour
{



    [HideInInspector] public string fileName;
    [HideInInspector] public string creationDate;
    
    [SerializeField] private Text nameText;
    [SerializeField] private Text creationDateText;



    public void Initialize()
    {
        nameText.text = fileName;
        creationDateText.text = creationDate;
    }
        
    
    
    
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


    public void ShareFileToBlender()
    {
        var resultColladaFile = new StringBuilder();
        var linesFromFile = File.ReadAllLines(Path.Combine(Application.persistentDataPath, fileName));
        for (int i = 0; i < linesFromFile.Length; ++i)
        {
            if (i == 45)
            {
                var matrices = ColladaFileHelper.GetUnityMatricesFromFile(fileName);
                var data = new List<float>();
                foreach (var m in matrices)
                {
                    var newQuaternion = Exporter.ConvertSensorToRightHanded(  m.ExtractRotation() * Quaternion.AngleAxis(90, Vector3.left));
                    var newVector = Exporter.ConvertVectorToGL(m.ExtractPosition());
                    
                    var matrix =  Matrix4x4.TRS(newVector, newQuaternion , Vector3.one);
            
                    data.AddRange((new float[] {
                        matrix.m00, matrix.m01, matrix.m02, matrix.m03,
                        matrix.m10, matrix.m11, matrix.m12, matrix.m13,
                        matrix.m20, matrix.m21, matrix.m22, matrix.m23, 
                        matrix.m30, matrix.m31, matrix.m32, matrix.m33}));
                }
                
                string valuePattern = @">.*<";

                var match = Regex.Match(linesFromFile[45], valuePattern);

                StringBuilder dataString = new StringBuilder(); 
                foreach (var item in data)
                {
                    dataString.Append($"{item} ");
                }

                var replace = linesFromFile[45].Replace(match.Value, $">{dataString}<");
                resultColladaFile.AppendLine(replace);
            }
            else resultColladaFile.AppendLine(linesFromFile[i]);
        }
        string cachePath = Path.Combine( Application.temporaryCachePath, "colladaExport.dae" );
        
        File.WriteAllLines(cachePath, resultColladaFile.ToString().Split('\n'));
        new NativeShare().AddFile( cachePath )
            .SetSubject( "Collada export" ).SetText( $"Collada (.dae) file generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }

}
