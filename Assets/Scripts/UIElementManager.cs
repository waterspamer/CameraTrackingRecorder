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


    public void ShareFileToBlender()
    {
        var resultColladaFile = new StringBuilder();
        var linesFromFile = File.ReadAllLines(Path.Combine(Application.persistentDataPath, fileName));

        UnityEngine.Debug.Log($"num strings: {linesFromFile.Length}");
            
        for (int i = 0; i < linesFromFile.Length; ++i)
        {


            if (i == 45)
            {

                var matrices = ColladaFileHelper.GetUnityMatricesFromFile(fileName);

                

                var data = new List<float>();


                foreach (var m in matrices)
                {
                    var newQuaternion = Exporter.ConvertSensorToRightHanded(m.ExtractRotation() * Quaternion.AngleAxis(90, Vector3.left));
                    var newVector = Exporter.ConvertVectorToGL(m.ExtractPosition());
                    
                    var matrix =  Matrix4x4.TRS(newVector, newQuaternion , Vector3.one);
            
                    data.AddRange((new float[] {
                        matrix.m00, matrix.m01, matrix.m02, matrix.m03,
                        matrix.m10, matrix.m11, matrix.m12, matrix.m13,
                        matrix.m20, matrix.m21, matrix.m22, matrix.m23, 
                        matrix.m30, matrix.m31, matrix.m32, matrix.m33}));
                }
                string r = 
                    @"<(?'tag'\w+?).*>"      +  
                    @"(?'text'.*?)"          +   
                    @"</\k'tag'>";               


                var match = Regex.Match(linesFromFile[45], r);
            
                
                var floats = match.Groups["text"].ToString().Split(' ');
                var dataString = match.Groups["tag1"].ToString();
                foreach (var item in data)
                {
                    dataString +=(item.ToString());
                }

                dataString += match.Groups["secondTag"].ToString();
                    resultColladaFile.AppendLine(dataString);
            }
            else resultColladaFile.AppendLine(linesFromFile[i]);
        }
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //var data = File.ReadAllBytes(filePath);
        string cachePath = Path.Combine( Application.temporaryCachePath, "colladaExport.dae" );
        
        File.WriteAllLines(cachePath, resultColladaFile.ToString().Split('\n'));
        //File.WriteAllBytes( cachePath, data);
        new NativeShare().AddFile( cachePath )
            .SetSubject( "Collada export" ).SetText( $"Collada (.dae) file generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
        //File.WriteAllLines(fileName, resultColladaFile.ToString().Split('\n'));
    }
    
    
    
}
