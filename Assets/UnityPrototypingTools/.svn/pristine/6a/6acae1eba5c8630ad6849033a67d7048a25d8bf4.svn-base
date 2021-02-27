using System;
using System.IO;

namespace Antilatency.UnityPrototypingTools {

    public static class UnityEditorUtils {

        public static string GetProjectRoot() {

        #if !UNITY_EDITOR
            throw new NotSupportedException("Only supported on the Unity Editor platform");
        #else
            string dataPath = UnityEngine.Application.dataPath;

            const string trailingPathComponent = "/Assets";
            if (!dataPath.EndsWith(trailingPathComponent))
                throw new Exception("Unexpected Application.dataPath value");

            return dataPath.Substring(0, dataPath.Length - trailingPathComponent.Length);
        #endif
        }

        public static string GetProjectRelativePath(string path) {

            if (Path.IsPathRooted(path))
                return path;

            var root = GetProjectRoot();
            return Path.Combine(root, path);
        }
    }
}
