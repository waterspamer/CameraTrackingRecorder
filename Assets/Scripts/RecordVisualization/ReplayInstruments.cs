using System;
using UnityEngine;
using UnityEngine.UI;

namespace RecordVisualization
{
    public class ReplayInstruments : MonoBehaviour
    {

        public Camera firstPersonViewCamera;

        public Camera thirdPersonViewCamera;


        private RenderTexture fpsTexture;
        private RenderTexture tpsTexture;

        public RawImage renderingPath;
        public TrailRenderer trailRenderer;

        private RenderTexture _renderTexture;


        private bool _isFps = true;
        private Color _trailColor;
        private void Awake()
        {
            _renderTexture = firstPersonViewCamera.activeTexture;
            var rect = renderingPath.rectTransform.rect;
            fpsTexture = new RenderTexture( Screen.width, Screen.height, 24 );
            tpsTexture = new RenderTexture( Screen.width, Screen.height, 24 );
            firstPersonViewCamera.targetTexture = fpsTexture;
            thirdPersonViewCamera.targetTexture = tpsTexture;
            _trailColor = trailRenderer.material.color;
            renderingPath.texture = fpsTexture;
        }

        public void SwitchCameraView()
        {
            renderingPath.texture = fpsTexture;
            _isFps = !_isFps;
            renderingPath.texture = _isFps ? fpsTexture : tpsTexture;
        }
    }
}
