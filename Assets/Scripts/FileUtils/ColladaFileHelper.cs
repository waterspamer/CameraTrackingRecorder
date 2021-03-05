using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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




        public static async Task SetMatricesFromFileToPersistent(string fileName)
        {
            var ms = GetUnityMatricesFromFile(fileName);
            SettingsManager.GetInstance().CurrentReplayMatrixArray = await ms;
        }



        public struct DataJob : IJob
        {
            public void Execute()
            {
                throw new NotImplementedException();
            }
        }
        
        public static async Task<Matrix4x4[]> GetUnityMatricesFromFile(string fileName)

        {
            var path = Path.Combine(Application.persistentDataPath, fileName);
            var linesFromFile = await AsyncFileUtils.ReadAllLinesAsync(fileName);
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
                    m00 = (float) Convert.ToDouble(floats[i]),
                    m01 = (float) Convert.ToDouble(floats[i + 1]),
                    m02 = (float) Convert.ToDouble(floats[i + 2]),
                    m03 = (float) Convert.ToDouble(floats[i + 3]),
                    m10 = (float) Convert.ToDouble(floats[i + 4]),
                    m11 = (float) Convert.ToDouble(floats[i + 5]),
                    m12 = (float) Convert.ToDouble(floats[i + 6]),
                    m13 = (float) Convert.ToDouble(floats[i + 7]),
                    m20 = (float) Convert.ToDouble(floats[i + 8]),
                    m21 = (float) Convert.ToDouble(floats[i + 9]),
                    m22 = (float) Convert.ToDouble(floats[i + 10]),
                    m23 = (float) Convert.ToDouble(floats[i + 11]),
                    m30 = (float) Convert.ToDouble(floats[i + 12]),
                    m31 = (float) Convert.ToDouble(floats[i + 13]),
                    m32 = (float) Convert.ToDouble(floats[i + 14]),
                    m33 = (float) Convert.ToDouble(floats[i + 15])
                };

                output[i/16] = outputMatrix;
            }
            return output;
        }


        
        
        
    }
}
