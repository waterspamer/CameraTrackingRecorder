using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TrendMe
{
    public class CsvToAnimHelper : MonoBehaviour
    {
        [Header("File Settings")]
        [SerializeField] private TextAsset recordedAnimationFile;
        [SerializeField] private GameObject referencedGameObject;



        private string[] _propertyNames =
        {
            "localPosition.x", 
            "localPosition.y",
            "localPosition.z", 
            "localRotation.x", 
            "localRotation.y",
            "localRotation.z",
            "localRotation.w"
        };

        
#if UNITY_EDITOR
        private AnimationClip GetAnimationClipFromCsv(TextAsset textAsset)
        {
            var clip = new AnimationClip();
            var text = textAsset.text;
            var reader = new StreamReader(AssetDatabase.GetAssetPath(textAsset));
            
            Debug.Log("Generation started !");
            var time = Time.time;
            var curves = new List<AnimationCurve>(7);
            for (int i = 0; i < 7; i++)
            {
                curves.Add(new AnimationCurve());
            }
            while (!reader.EndOfStream)
            {
                var parsedLine = reader.ReadLine()?.Split(',');
                for (int i = 0; i < parsedLine?.Length-1; i++)
                {
                    var curve = new AnimationCurve();
                    curves[i].AddKey(float.Parse(parsedLine?[0] ?? string.Empty),
                        float.Parse(parsedLine?[i+1] ?? string.Empty));
                }
            }

            for (int i = 0; i < curves.Count; i++)
            {
                clip.SetCurve("", typeof(Transform), _propertyNames[i], curves[i]);
            }
            Debug.Log("Generation ended in " +(Time.time - time).ToString(CultureInfo.InvariantCulture) + " seconds!");
            return clip;
        }

        private void CreateAnimationAsset(AnimationClip animationClip, string animationClipName)
        {
            if (!AssetDatabase.IsValidFolder("Assets/GeneratedAnimations"))
                AssetDatabase.CreateFolder("Assets", "GeneratedAnimations");
            Debug.Log("Writing " + animationClip.name + " to " + "Assets/GeneratedAnimations/" + animationClipName + ".anim");
            AssetDatabase.CreateAsset(animationClip, "Assets/GeneratedAnimations/" + animationClipName + ".anim");
            AssetDatabase.SaveAssets();
            Debug.Log("Completed !");
        }


        public void ExportToFbx()
        {
            
        }

        public void GenerateAnimFile() =>
            CreateAnimationAsset(GetAnimationClipFromCsv(recordedAnimationFile), 
                recordedAnimationFile.name);
#endif
    }
}
