using UnityEngine;

namespace Persistent
{
    public class SettingsManager
    {
        private static int _fpsCount;

        public Matrix4x4[] CurrentReplayMatrixArray;
        
        
        public void SetFPS(int value) => 
            _fpsCount = value;

        private static SettingsManager _instance;
        public static SettingsManager GetInstance() =>
            _instance ?? (_instance = new SettingsManager());
        
    }
}
