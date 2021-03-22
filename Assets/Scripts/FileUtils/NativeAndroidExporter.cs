using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Maths;
using Persistent;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FileUtils
{
    public class NativeAndroidExporter : MonoBehaviour
    {

        [SerializeField] private UIElementsSelector selector;


        private Dictionary<UIElementManager, float> _currentlySharingRecords;

        private NativeShare _nativeAndroidShare;

        private UnityEvent onThreadsRipped; 
        
        
        
        private void Awake()
        {
            _nativeAndroidShare = new NativeShare();
            onThreadsRipped = new UnityEvent();
            onThreadsRipped.AddListener(FinalizeSharing);
            SettingsManager.GetInstance.PersistentDataPath = Application.persistentDataPath;
            SettingsManager.GetInstance.TemporaryDataPath = Application.temporaryCachePath;
            
            Debug.Log(Application.persistentDataPath);
            Debug.Log(SettingsManager.GetInstance.PersistentDataPath);
            
            
            //Dispatcher.onThreadsRipped.AddListener(FinalizeSharing);
        }


        private int _threadCount;
        
        private IEnumerator FinalizingCheck()
        {
            while (_threadCount !=0)
            {
                yield return new WaitForSeconds(.01f);
            }
            
            onThreadsRipped?.Invoke();
        }

        public void RepackRecordsAndShare() {
            var nativeAndroidObj = new NativeShare();
            _currentlySharingRecords = new Dictionary<UIElementManager, float>();
            _threadCount = selector.selectedElements.Count;
            StartCoroutine(FinalizingCheck());
            foreach (var element in selector.selectedElements)
            {
                _currentlySharingRecords.Add(element, element.progressBar.fillAmount);
            }
            foreach (var element in selector.selectedElements)
            {
                //var thread = new ParameterizedThreadStart(AsyncFilePackaging(element));
                Dispatcher.RunOnMainThread(()=> StartCoroutine(element.VisualizeProgress()));
                var thread = new Thread(()=> AsyncFilePackaging(element));
                thread.Start();
                
                //Dispatcher.RunOnMainThread(() => AsyncFilePackaging(element));
            }
            
            /*
            nativeAndroidObj.SetSubject( "Collada export" ).SetText( $"Collada (.dae) file(s) generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
                .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
                .Share();
            */
            
            
        }


        private UnityEvent onWritingFinished;


        private void FinalizeSharing()
        {
            _nativeAndroidShare.SetSubject( "Collada export" ).SetText( $"Collada (.dae) file(s) generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
                .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
                .Share();
        }


        private string _cachedPersistentDataPath;
        
        private void AsyncFilePackaging(UIElementManager element)
        {

            var filePath = Path.Combine(SettingsManager.GetInstance.PersistentDataPath, element.fileName);


            
            
            WriteToTemporaryFile(element.fileName, element);
            var data = File.ReadAllBytes(filePath);
            var cachePath = Path.Combine( SettingsManager.GetInstance.TemporaryDataPath, element.fileName );
            File.WriteAllBytes( cachePath, data);
            _nativeAndroidShare.AddFile(cachePath);
            _threadCount--;
        }

        private void WriteToTemporaryFile(string fileName, UIElementManager element )
        {
            var resultColladaFile = new StringBuilder();
            var linesFromFile = File.ReadAllLines(Path.Combine(SettingsManager.GetInstance.PersistentDataPath, fileName));
            for (int i = 0; i < linesFromFile.Length; ++i)
            {
                element.packagingProgress = i / (float)linesFromFile.Length;
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
            string cachePath = Path.Combine( SettingsManager.GetInstance.TemporaryDataPath, $"{element.fileName}" );
        
            File.WriteAllLines(cachePath, resultColladaFile.ToString().Split('\n'));
        }
        
        
        
        
        
        
    
    
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
}
