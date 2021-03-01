using System.Collections;
using Maths;
using Persistent;
using UnityEngine;

namespace RecordVisualization
{
    public class ReplayManager : MonoBehaviour
    {

        [SerializeField] private Transform replayCamera;
        [SerializeField] private GameObject replayWindow;



        public void StartPlaying()
        {
            replayWindow.SetActive(true);
            StartCoroutine(Play());
        }


        public void StopPlaying()
        {
            StopCoroutine(Play());   
        }
        
        
        
        
        IEnumerator Play()
        {
            var matrices = SettingsManager.GetInstance().CurrentReplayMatrixArray;
            for (int i = 0; i < matrices.Length; ++i)
            {
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
