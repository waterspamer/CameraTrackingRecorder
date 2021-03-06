﻿using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Persistent
{
    public class SettingsManager
    {
        public int _fpsCount;

        public UIElementsSelector Selector;

        public float longPressTime = .25f;

        public bool isSelectingMode;

        public ScrollRect listParentRect;

        public string PersistentDataPath;

        public string TemporaryDataPath;

        public string fileName;

        public Matrix4x4[] CurrentReplayMatrixArray;

        public void SetFileName(string value) =>
            fileName = value;
        
        public void SetFPS(int value) => 
            _fpsCount = value;

        private static SettingsManager _instance;
        public static SettingsManager GetInstance =>
            _instance ?? (_instance = new SettingsManager());
        
    }
}
