using System.Collections;
using Maths;
using Persistent;
using UnityEngine;

namespace RecordVisualization
{
    public class ReplayManager : MonoBehaviour
    {

        [SerializeField] private Transform replayCamera;



        public void StartPlaying()
        {
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
                yield return new WaitForSeconds(.1f);
            }
        }

        private void SetTransformFromMatrix(Transform refTransform, Matrix4x4 matrix)
        {
            
            refTransform.localScale = matrix.ExtractScale();
            refTransform.rotation = matrix.ExtractRotation();
            refTransform.position = matrix.ExtractPosition();
        }

    }
}
