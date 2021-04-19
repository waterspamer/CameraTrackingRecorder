using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Persistent;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.IO.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;

namespace FileUtils
{
    public static class ColladaFileHelper 
    {
        
        
        private static ReadHandle readHandle;
        static NativeArray<ReadCommand> cmds;




        public static  void SetMatricesFromFileToPersistent(string fileName)
        {
            var ms = GetUnityMatricesFromFile(fileName);
            SettingsManager.GetInstance.CurrentReplayMatrixArray =ms;
        }



        public struct DataJob : IJob
        {
            public void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public static Matrix4x4[] GetUnityMatricesFromFile(string fileName)

        {
            var path = Path.Combine(SettingsManager.GetInstance.PersistentDataPath, fileName);
            var linesFromFile = File.ReadAllLines(path);
            Debug.Log(linesFromFile[45].Length);
            
            string r = 
                @"<(?'tag'\w+?).*>"      +  
                @"(?'text'.*?)"          +   
                @"</\k'tag'>";               


            var m = Regex.Match(linesFromFile[45], r);
            
            
            var floats = m.Groups["text"].ToString().Split(' ');
            
            var output = new Matrix4x4[floats.Length /16];
            
            
            var reloadMatrices = new AsyncOperation();
            
            
            
            for (int i = 0; i < floats.Length-1; i += 16)
            {

                var outputMatrix = new Matrix4x4
                {
                    m00 = float.Parse(floats[i]),
                    m01 = float.Parse(floats[i + 1]),
                    m02 = float.Parse(floats[i + 2]),
                    m03 = float.Parse(floats[i + 3]),
                    m10 = float.Parse(floats[i + 4]),
                    m11 = float.Parse(floats[i + 5]),
                    m12 = float.Parse(floats[i + 6]),
                    m13 = float.Parse(floats[i + 7]),
                    m20 = float.Parse(floats[i + 8]),
                    m21 = float.Parse(floats[i + 9]),
                    m22 = float.Parse(floats[i + 10]),
                    m23 = float.Parse(floats[i + 11]),
                    m30 = float.Parse(floats[i + 12]),
                    m31 = float.Parse(floats[i + 13]),
                    m32 = float.Parse(floats[i + 14]),
                    m33 = float.Parse(floats[i + 15])
                };

                output[i/16] = outputMatrix;
            }
            return output;
        }


        public static string[] GetFileStrings(string fileName) =>
            File.ReadAllLines(Path.Combine(SettingsManager.GetInstance.PersistentDataPath, fileName));

        
        
        public static Matrix4x4[] GetUnityMatricesFromStringArrayThreaded(string[] strings)
        {
            var linesFromFile = strings;
            Debug.Log(linesFromFile[45].Length);
            
            string r = 
                @"<(?'tag'\w+?).*>"      +  
                @"(?'text'.*?)"          +   
                @"</\k'tag'>";               


            var m = Regex.Match(linesFromFile[45], r);
            
            Debug.Log(m.Groups["text"].ToString());
            var floats = m.Groups["text"].ToString().Split(' ');
            
            var output = new Matrix4x4[floats.Length /16];

            
            
            
            for (int i = 0; i < floats.Length-1; i += 16)
            {

                var outputMatrix = new Matrix4x4
                {
                    m00 = float.Parse(floats[i]),
                    m01 = float.Parse(floats[i + 1]),
                    m02 = float.Parse(floats[i + 2]),
                    m03 = float.Parse(floats[i + 3]),
                    m10 = float.Parse(floats[i + 4]),
                    m11 = float.Parse(floats[i + 5]),
                    m12 = float.Parse(floats[i + 6]),
                    m13 = float.Parse(floats[i + 7]),
                    m20 = float.Parse(floats[i + 8]),
                    m21 = float.Parse(floats[i + 9]),
                    m22 = float.Parse(floats[i + 10]),
                    m23 = float.Parse(floats[i + 11]),
                    m30 = float.Parse(floats[i + 12]),
                    m31 = float.Parse(floats[i + 13]),
                    m32 = float.Parse(floats[i + 14]),
                    m33 = float.Parse(floats[i + 15])
                };

                output[i/16] = outputMatrix;
            }
            return output;
        }
    }
}
