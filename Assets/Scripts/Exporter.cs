using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using Antilatency;
using Antilatency.Alt.Tracking;
using Antilatency.Integration;
using Persistent;
using Threading;
using UnityEngine.Events;
using UnityEngine.UI;

public class Exporter : MonoBehaviour
{
    [SerializeField] private TextAsset referenceColladaTextAsset;

    public Text recordInfoText;

    public int frameCounter = 0;
    
    public string _timeString;
    public string _dataString;

    public List<Vector3> _positions;
    public List<Quaternion> _rotations;
    public List<float> _transformData;
    public List<string> _timeData;
    public AltTrackingDirect TrackingDirect;


    public Text xText;
    public Text yText;
    public Text zText;



    private void Awake()
    {
        frameCounter = 0;
        _positions = new List<Vector3>();
        _rotations = new List<Quaternion>();
        _timeData = new List<string>();
        _transformData = new List<float>();
        _dataString = String.Empty;
        _timeString = String.Empty;
        _textVisualizingRoutine = WriterVisualizer();
    }

    private void MakeStrings()
    {
        _dataString =
            "<float_array id=\"Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-output-array\"";
        _timeString =
            "<float_array id=\"Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-output-array\"";
        _dataString += $" count=\"{_positions.Count}\">";
        _timeString += $" count=\"{_timeData.Count}\">";
        var sb = new StringBuilder();
        foreach (var item in _transformData)
        {
            sb.Append(item + " ");
        }

        _dataString += (sb.ToString());
        _dataString.Remove(_dataString.Length - 1);
        _dataString += "</float_array>";

        foreach (var item in _timeData)
        {
            _timeString += item + " ";
        }

        _timeString.Remove(_timeString.Length - 1);
        _timeString += "</float_array>";
        
    }

    private void Export()
    {
        _transformData.Clear();
        for(int i = 0; i < _positions.Count; ++i)
        {
            var newQuaternion = _rotations[i];
            var newVector = _positions[i];
            var matrix =  Matrix4x4.TRS(newVector, newQuaternion , Vector3.one);
            _transformData.AddRange((new[] {
                matrix.m00, matrix.m01, matrix.m02, matrix.m03,
                matrix.m10, matrix.m11, matrix.m12, matrix.m13,
                matrix.m20, matrix.m21, matrix.m22, matrix.m23, 
                matrix.m30, matrix.m31, matrix.m32, matrix.m33}));
        }
        MakeStrings();
        CreateColladaFile(Application.persistentDataPath +$"/{SettingsManager.GetInstance.fileName}.dae");
    }

    private static Quaternion ConvertSensorToRightHanded(float x, float y, float z, float w) {

        Quaternion output;
        output.x = -x;
        output.y = -z;
        output.z = -y;
        output.w = w;

        return output;
    }
    
    public static Vector3 ConvertVectorToGL (Vector3 v3)=>
        new Vector3(v3.x, v3.z, v3.y);

    public static Quaternion ConvertSensorToRightHanded(Quaternion q) =>
        ConvertSensorToRightHanded(q.x, q.y, q.z, q.w);
    

    private void CreateColladaFile(string fileName)
    {
        var resultColladaFile = new StringBuilder();
        var linesFromFile = referenceColladaTextAsset.text.Split(new[]{"\n"}, StringSplitOptions.None);
        for (var i = 0; i < linesFromFile.Length; ++i)
        {
            switch (i)
            {
                case 37:
                    resultColladaFile.AppendLine(_timeString);
                    break;
                case 45:
                    resultColladaFile.AppendLine(_dataString);
                    break;
                case 39:
                    resultColladaFile.AppendLine($"<accessor source=\"#Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-input-array\" count=\"{frameCounter/16}\" stride=\"1\">");
                    break;
                case 47:
                    resultColladaFile.AppendLine($"<accessor source=\"#Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-output-array\" count=\"{frameCounter}\" stride=\"16\">");
                    break;
                case 53:
                {
                    resultColladaFile.AppendLine($"<Name_array id=\"Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-interpolation-array\" count=\"{frameCounter}\">");
                    for (var j = 0; j < frameCounter; j++)
                        resultColladaFile.Append("LINEAR ");
                    resultColladaFile.Append("</Name_array>");
                    break;
                }
                default:
                    resultColladaFile.AppendLine(linesFromFile[i]);
                    break;
            }
        }
        File.WriteAllLines(fileName, resultColladaFile.ToString().Split('\n'));
    }
    private Thread _writingThread;
    private UnityEvent _frameAddingCallback;
    private IEnumerator _textVisualizingRoutine;

    private void OnApplicationQuit()
    {
        _cToken.Cancel();
    }
    
    CancellationTokenSource _cToken = new CancellationTokenSource();
    public void StartWriting()
    {
        _cToken = new CancellationTokenSource();
        var startTime = DateTime.Now;
        _recording = true;
        ThreadedMethodInvoker.StartSynchronizedRoutine(() =>
        {
            frameCounter++;
            var timeDelta = (DateTime.Now - startTime);
            _timeData.Add(timeDelta.ToString("s'.'fff"));
            TrackingDirect.GetTrackingState(out var state);
            _positions.Add(state.pose.position);
            _rotations.Add(state.pose.rotation);
        }, 8, _cToken);
        StartCoroutine(_textVisualizingRoutine);
    }
    
    
    public void StopWriting()
    {
        _recording = false;
        _cToken.Cancel();
        Export();
    }
    IEnumerator WriterVisualizer()
    {
        yield return new WaitForSeconds(.1f);
        while (_recording)
        {
            recordInfoText.text = $"{_timeData?.Last()} s {_timeData?.Count} frames";
            var pos = _positions.Last();
            xText.text = pos.x.ToString("F3");
            yText.text = pos.y.ToString("F3");
            zText.text = pos.z.ToString("F3");
            
            yield return new WaitForSeconds(1f/60f);
        }
    }





    private bool _recording;
}
