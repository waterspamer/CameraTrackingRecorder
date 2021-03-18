using System;
using UnityEngine;
using UnityEngine.UI;

namespace RecordVisualization
{
    public class ReplayInstruments : MonoBehaviour
    {

        public Camera firstPersonViewCamera;

        public Camera thirdPersonViewCamera;


        public RenderTexture fpsTexture;
        public RenderTexture tpsTexture;

        public RawImage renderingPath;
        public TrailRenderer trailRenderer;

        private RenderTexture _renderTexture;


        private bool _isFps = true;

        private void Awake()
        {
            _renderTexture = firstPersonViewCamera.activeTexture;
        }

        public void SwitchCameraView()
        {
            _isFps = !_isFps;
            trailRenderer.enabled = !_isFps;
            renderingPath.texture = _isFps ? fpsTexture : tpsTexture;
        }
    
    
    }
}
