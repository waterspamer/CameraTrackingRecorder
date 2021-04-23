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
using UnityEngine.Events;
using UnityEngine.UI;

public class Exporter : MonoBehaviour
{
    [SerializeField] private TextAsset referenceColladaTextAsset;

    public Text recordInfoText; 

    public Transform _trackedObject;

    public int frameCounter = 0;
    
    public string _timeString;
    public string _dataString;

    public List<Vector3> _positions;
    public List<Quaternion> _rotations;
    public List<float> _transformData;
    public List<float> _timeData;


    private void Awake()
    {
        _trackedObject.GetComponent<AltTrackingDirect>().GetTrackingState(out State state);
        _objPose = state.pose;
        frameCounter = 0;
        _positions = new List<Vector3>();
        _rotations = new List<Quaternion>();
        _timeData = new List<float>();
        _transformData = new List<float>();
        _dataString = String.Empty;
        _timeString = String.Empty;
        _writingRoutine = Writer();
        //_writingThread = new Thread(()=> StartCoroutine(_writingRoutine));
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

        _dataString = _dataString + (sb.ToString());
        _dataString.Remove(_dataString.Length - 1);
        _dataString += "</float_array>";

        foreach (var item in _timeData)
        {
            _timeString += item + " ";
        }

        _timeString.Remove(_timeString.Length - 1);
        _timeString += "</float_array>";
        
    }

    public void Export()
    {
        _transformData.Clear();
        for(int i = 0; i < _positions.Count; ++i)
        {
            var newQuaternion = _rotations[i];
            var newVector = _positions[i];
            var matrix =  Matrix4x4.TRS(newVector, newQuaternion , Vector3.one);
            _transformData.AddRange((new float[] {
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
        var linesFromFile = referenceColladaTextAsset.text.Split(new string[]{"\n"}, StringSplitOptions.None);
        for (int i = 0; i < linesFromFile.Length; ++i)
        {
            if (i == 37)
            {
                resultColladaFile.AppendLine(_timeString);
            }

            else if (i == 45)
            {
                resultColladaFile.AppendLine(_dataString);
            }
            else if (i == 39)
            {
                resultColladaFile.AppendLine($"<accessor source=\"#Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-input-array\" count=\"{frameCounter/16}\" stride=\"1\">");
            }
            else if (i == 47)
            {
                resultColladaFile.AppendLine($"<accessor source=\"#Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-output-array\" count=\"{frameCounter}\" stride=\"16\">");
            }
            else if (i == 53)
            {
                resultColladaFile.AppendLine($"<Name_array id=\"Camera_001_Camera_001_testRecord_Animation_Base_Layer_transform-interpolation-array\" count=\"{frameCounter}\">");
                for (int j = 0; j < frameCounter; j++)
                    resultColladaFile.Append("LINEAR ");
                resultColladaFile.Append("</Name_array>");
            }
            else resultColladaFile.AppendLine(linesFromFile[i]);
        }
        File.WriteAllLines(fileName, resultColladaFile.ToString().Split('\n'));
    }

    private float _startTime = 0f;

    
    
    

    public void SetRecordingState(bool state) => _recording = state;


    private Thread _writingThread;
    private UnityEvent _frameAddingCallback;
    
    private IEnumerator _writingRoutine;

    private void OnApplicationQuit()
    {
        cToken.Cancel();
    }

    CancellationTokenSource cToken = new CancellationTokenSource();
    public void StartWriting()
    {
        _recording = true;
        //_supportThread.Start();
        //Dispatcher.RunAsync(()=> AppendNewRecordLine());
        var stateTimer = new TrackingDataRecordUpdater(9999);
        var autoEvent = new AutoResetEvent(false);
        //var cToken = new CancellationTokenSource();
        ThreadedMethodInvoker.ThreadRoutine(cToken, _trackedObject.GetComponent<AltTrackingDirect>());
        //cToken.Cancel();
        //var trackingStateTimer = new Timer(stateTimer.CheckStatus, autoEvent, 100000, (int)(1000f/ 120f));
        //autoEvent.WaitOne();
        //trackingStateTimer.Start();
        var startTime = 0;
        
        _frameAddingCallback = new UnityEvent();
        _frameAddingCallback.AddListener(() => AppendNewRecordLine((1f/ 120f)));
        StartCoroutine(_writingRoutine);
        
    }

    public void StopWriting()
    {
        _recording = false;
        StopCoroutine(_writingRoutine);
        //todo stateTimer.Dispose();
        Export();
    }
    
    
    
    class TrackingDataRecordUpdater
    {
        private int invokeCount;
        private int  maxCount;

        public TrackingDataRecordUpdater(int count)
        {
            invokeCount  = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(System.Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Debug.Log(DateTime.Now.ToString("h:mm:ss.fff"));

            if(invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }
    
    private void AppendNewRecordLine(float timeGone)
    {
        _startTime += timeGone;
        //_startTime += 1f / 60f;
        frameCounter++;
        recordInfoText.text = $"{Math.Round(frameCounter / 60f, 2)} s ({frameCounter} frames)";
        _timeData.Add(_startTime);
        _positions.Add(_objPose.position);
        _rotations.Add(_objPose.rotation);
        /*
        _timeData.Add(_startTime);
        _positions.Add(_trackedObject.position);
        _rotations.Add(_trackedObject.rotation);
        */
    }
    
    private Pose _objPose;
    private Thread _supportThread;
    //
    IEnumerator Writer()
    {
        while (_recording)
        {
            //Dispatcher.RunAsync(()=> AppendNewRecordLine());
                
            _frameAddingCallback?.Invoke();
            /*
            _startTime += Time.deltaTime;
            frameCounter++;
            recordInfoText.text = $"{Math.Round(frameCounter / 60f, 2)} s ({frameCounter} frames)";
            _timeData.Add(_startTime);
            _positions.Add(_trackedObject.position);
            _rotations.Add(_trackedObject.rotation);
            */
            
            yield return new WaitForSeconds(1f/60f);
        }
    }





    private bool _recording;
}
