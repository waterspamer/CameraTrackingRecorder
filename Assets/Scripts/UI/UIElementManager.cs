using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FileUtils;
using Maths;
using Persistent;
using RecordVisualization;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIElementManager : MonoBehaviour
{



    [HideInInspector] public string fileName;
    [HideInInspector] public string creationDate;
    [HideInInspector] public string fileSize;
    
    [SerializeField] private Text nameText;
    [SerializeField] private Text creationDateText;
    [SerializeField] private Text fileSizeText;



    private bool _selected = false;
    

    public  UnityEvent onSelected;
    
    public  UnityEvent onDeselected;


    public void Select()
    {
        Debug.Log("This Item Selected");
        _selected = !_selected;
        if (_selected)
        {
            onSelected?.Invoke();
            return;
        }
        Deselect();
    }

    public void Deselect()
    {
        onDeselected?.Invoke();
    }


    public void Initialize()
    {
        nameText.text = fileName;
        creationDateText.text = creationDate;
        fileSizeText.text = fileSize;
    }



    

    private void Awake()
    {
        _onThreadedLoadingEnded = new UnityEvent();
        _onThreadedLoadingEnded.AddListener(StartPlaying);
    }


    public void AddToSelected() => SettingsManager.GetInstance().Selector.AddItemToSelectedList(this);
    
    public void RemoveFromSelected() => SettingsManager.GetInstance().Selector.RemoveItemFromSelectedList(this);

    public void StartReplay()
    {
        FindObjectOfType<ReplayManager>().onWindowActivated?.Invoke();
        _cachedStrings = ColladaFileHelper.GetFileStrings(fileName);
        var thread = new Thread(ThreadedMatrixSetting);
        thread.Start();
    }


    private void StartPlaying() =>
        Dispatcher.RunOnMainThread(() => (FindObjectOfType(typeof(ReplayManager)) as ReplayManager)?.StartPlaying());

    private string[] _cachedStrings;
    
    void ThreadedMatrixSetting()
    {
        SettingsManager.GetInstance().CurrentReplayMatrixArray = ColladaFileHelper.GetUnityMatricesFromStringArrayThreaded(_cachedStrings);
        _onThreadedLoadingEnded?.Invoke();
    }

    private UnityEvent _onThreadedLoadingEnded;

    
    
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


    public async Task ShareFileToBlender()
    {
        var resultColladaFile = new StringBuilder();
        var linesFromFile = File.ReadAllLines(Path.Combine(Application.persistentDataPath, fileName));
        for (int i = 0; i < linesFromFile.Length; ++i)
        {
            if (i == 45)
            {
                var matrices = await ColladaFileHelper.GetUnityMatricesFromFile(fileName);
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
