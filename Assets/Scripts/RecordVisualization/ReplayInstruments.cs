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
        private Color _trailColor;
        private void Awake()
        {
            _renderTexture = firstPersonViewCamera.activeTexture;
            _trailColor = trailRenderer.material.color;
        }

        public void SwitchCameraView()
        {
            _isFps = !_isFps;
            renderingPath.texture = _isFps ? fpsTexture : tpsTexture;
        }
    
    
    }
}
