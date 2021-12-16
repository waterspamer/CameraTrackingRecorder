using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Antilatency.UnityPrototypingTools;
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
        private UnityEvent _onThreadsRipped; 
        private string _cachedPersistentDataPath;
        private int _threadCount;
        private List<Thread> _threadPool;
        private UnityEvent _onWritingFinished;
        
        
        private void Awake() {
            _nativeAndroidShare = new NativeShare();
            _onThreadsRipped = new UnityEvent();
            _threadPool = new List<Thread>();
            _onThreadsRipped.AddListener(FinalizeSharing);
            var smInstance = SettingsManager.GetInstance;
            smInstance.PersistentDataPath = Application.persistentDataPath;
            smInstance.TemporaryDataPath = Application.temporaryCachePath;
        }

        private IEnumerator FinalizingCheck(){
            while (_threadCount !=0)
                yield return new WaitForSeconds(.01f);
            _onThreadsRipped?.Invoke();
        }

        public void RepackRecordsAndShare() {
            _currentlySharingRecords = new Dictionary<UIElementManager, float>();
            _threadCount = selector.selectedElements.Count;
            StartCoroutine(FinalizingCheck());

            foreach (var element in selector.selectedElements) {
                _currentlySharingRecords.Add(element, element.progressBar.fillAmount);
                Dispatcher.RunOnMainThread(()=> StartCoroutine(element.VisualizeProgress()));
                var thread = new Thread(()=> AsyncFilePackaging(element));
                _threadPool.Add(thread);
                thread.Start();
            }
        }

        private void FinalizeSharing() {
            foreach (var thread in _threadPool)
                thread.Abort();
            
            _nativeAndroidShare.SetSubject( "Collada export" ).SetText( $"Collada (.dae) file(s) generated at {DateTime.Now}, sent by Antilatency Android sharing plug-in" )
                .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
                .Share();
        }

        private void AsyncFilePackaging(UIElementManager element) {
            WriteToTemporaryFile(element.fileName, element);
            _threadCount--;
        }

        private void WriteToTemporaryFile(string fileName, UIElementManager element ) {
            var resultColladaFile = new StringBuilder();
            var linesFromFile = File.ReadAllLines(Path.Combine(SettingsManager.GetInstance.PersistentDataPath, fileName));
            for (int i = 0; i < linesFromFile.Length; ++i) {
                element.packagingProgress = i / (float)linesFromFile.Length;
                if (i == 45) {
                    
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
                        dataString.Append(item.ToString("r") + " ");
                    var replace = linesFromFile[45].Replace(match.Value, $">{dataString}<");
                    resultColladaFile.AppendLine(replace);
                }
                else resultColladaFile.AppendLine(linesFromFile[i]);
            }

#if UNITY_EDITOR
            File.WriteAllLines($"D:\\records\\{element.fileName}", resultColladaFile.ToString().Split('\n'));
            return;
#endif
            string cachePath = Path.Combine( SettingsManager.GetInstance.TemporaryDataPath, $"{element.fileName}" );
            File.WriteAllLines(cachePath, resultColladaFile.ToString().Split('\n'));
            _nativeAndroidShare.AddFile(cachePath);
        }
    }
}
