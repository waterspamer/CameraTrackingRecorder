using UnityEngine;

namespace Persistent
{
    public class SettingsManager
    {
        public int _fpsCount;

        public string fileName;

        public Matrix4x4[] CurrentReplayMatrixArray;

        public void SetFileName(string value) =>
            fileName = value;
        
        public void SetFPS(int value) => 
            _fpsCount = value;

        private static SettingsManager _instance;
        public static SettingsManager GetInstance() =>
            _instance ?? (_instance = new SettingsManager());
        
    }
}
