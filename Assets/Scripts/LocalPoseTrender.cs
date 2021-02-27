using System;
using UnityEngine;
using System.Collections;
using System.Reflection;

namespace TrendMe {

    public class LocalPoseTrender : Trender {

        [SerializeField] private AnimationCurve trendPosX;
        [SerializeField] private AnimationCurve trendPosY;
        [SerializeField] private AnimationCurve trendPosZ;

        
        [SerializeField] private AnimationCurve trendX;
        [SerializeField] private AnimationCurve trendY;
        [SerializeField] private AnimationCurve trendZ;
        [SerializeField] private AnimationCurve trendW;
        
        private Quaternion _targetRotation => transform.rotation;
        private Vector3 _targetPosition => transform.position;

        protected override string[] DataToStrings() {
            string[] data = new string[trendX.keys.Length];
            for (int i = 0; i < trendX.keys.Length; i++) {
                data[i] = trendX.keys[i].time + "," + trendPosX.keys[i].value + "," + trendPosY.keys[i].value + "," + trendPosZ.keys[i].value + "," + trendX.keys[i].value + "," + trendY.keys[i].value + "," + trendZ.keys[i].value + "," + trendW.keys[i].value;
            }
            return data;
        }

        private void OnApplicationQuit()
        {
            Save("testRecord");
        }

        protected override void GraphCurrentValue(float time)
        {
            trendPosX.AddKey(time, _targetPosition.x);
            trendPosY.AddKey(time, _targetPosition.y);
            trendPosZ.AddKey(time, _targetPosition.z);
            
            trendX.AddKey(time, _targetRotation.x);
            trendY.AddKey(time, _targetRotation.y);
            trendZ.AddKey(time, _targetRotation.z);
            trendW.AddKey(time, _targetRotation.w);

        }
    }
}