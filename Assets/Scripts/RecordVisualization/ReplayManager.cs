using System;
using System.Collections;
using System.Threading;
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
        [SerializeField] private ExtendedSlider timeLine;
        [SerializeField] private Text replayHeader;
        [SerializeField] private Text fpsSettingsLabel;
        
        public UnityEvent onPlayButtonActivate;

        public UnityEvent onWindowActivated;
        public UnityEvent onWindowDeactivated;

        
        
        public void StartPlaying()
        {
            timeLine.minValue = 0;
            timeLine.maxValue = SettingsManager.GetInstance.CurrentReplayMatrixArray.Length -1 ;
            replayHeader.text = SettingsManager.GetInstance.fileName;
            //fpsSettingsLabel.text = SettingsManager.GetInstance._fpsCount.ToString();
            replayWindow.SetActive(true);
            _playableRoutine = Play();
            StartCoroutine(_playableRoutine);
        }


        public void ResetTimeLine() =>
            timeLine.value = 0;


        public void StopPlaying()
        {


            if (!isPaused)
            {
                StopCoroutine(_playableRoutine);
                _currentFrame = 0;
            }
        }


        public bool isUserMoving
        {
            get;
            set;
        }

        public bool isPaused
        {
            get;
            set;
        }
        
        public void EnablePlayButton()
        {
            onPlayButtonActivate?.Invoke();
        }

        private void Awake()
        {
            _playableRoutine = Play();
            
        }

        
        


        private Matrix4x4[] _currentReplayMatrices;
        
        public void SetReplayFrame()
        {
            if (isUserMoving)
                SetTransformFromMatrix(replayCamera, _currentReplayMatrices[(int)timeLine.value]);
        }
        
        private IEnumerator _playableRoutine;



        private int _currentFrame = 0;

        public ReplayManager(bool isPaused, bool isUserMoving)
        {
            this.isPaused = isPaused;
            this.isUserMoving = isUserMoving;
        }

        IEnumerator Play()
        {
            _currentReplayMatrices = SettingsManager.GetInstance.CurrentReplayMatrixArray;
            
            for (_currentFrame = (int)timeLine.value; _currentFrame < _currentReplayMatrices.Length; ++_currentFrame)
            {
                timeLine.value = _currentFrame;
                SetTransformFromMatrix(replayCamera, _currentReplayMatrices[_currentFrame]);
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
