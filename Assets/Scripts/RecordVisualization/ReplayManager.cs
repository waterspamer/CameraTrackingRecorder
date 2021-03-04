using System;
using System.Collections;
using Maths;
using Persistent;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RecordVisualization
{
    public class ReplayManager : MonoBehaviour
    {

        [SerializeField] private Transform replayCamera;
        [SerializeField] private GameObject replayWindow;
        [SerializeField] private Slider timeLine;
        



        public UnityEvent onWindowActivated;
        public UnityEvent onWindowDeactivated;
        

        public void StartPlaying()
        {
            
            onWindowActivated?.Invoke();
            timeLine.minValue = 0;
            timeLine.maxValue = SettingsManager.GetInstance().CurrentReplayMatrixArray.Length;
            
            replayWindow.SetActive(true);
            _playableRoutine = new TypeCache.TypeCollection.Enumerator();
            _playableRoutine = Play();
            StartCoroutine(_playableRoutine);
        }


        public void StopPlaying()
        {
            
            onWindowDeactivated?.Invoke();
            StopCoroutine(_playableRoutine);
            //_playableRoutine.
        }

        private void Awake()
        {
            _playableRoutine = Play();
        }


        private IEnumerator _playableRoutine;
        
        
        IEnumerator Play()
        {
            var matrices = SettingsManager.GetInstance().CurrentReplayMatrixArray;
            for (int i = 0; i < matrices.Length; ++i)
            {
                timeLine.value = i;
                SetTransformFromMatrix(replayCamera, matrices[i]);
                yield return new WaitForSeconds(1f / 60f);
            }
        }

        private void SetTransformFromMatrix(Transform refTransform, Matrix4x4 matrix)
        {
            
            refTransform.localScale = matrix.ExtractScale();
            refTransform.rotation = matrix.ExtractRotation();
            refTransform.localPosition = matrix.ExtractPosition();
        }
    }
}
