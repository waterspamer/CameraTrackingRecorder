using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Antilatency.UnityPrototypingTools {

    public static class UnityEditorScreenshot {

        [MenuItem("Antilatency/Take Unity Editor window screenshot %#k")]
        private static void Screenshot() {
            // Heavily basen on https://stackoverflow.com/a/54543469.

            var activeWindow = EditorWindow.focusedWindow;
            var timestamp = System.DateTime.Now;

            var vec2Position = activeWindow.position.position;
            var sizeX = activeWindow.position.width;
            var sizeY = activeWindow.position.height;

            var colors = InternalEditorUtility.ReadScreenPixel(vec2Position, (int)sizeX, (int)sizeY);
            var result = new Texture2D((int)sizeX, (int)sizeY);
            result.SetPixels(colors);

            var bytes = result.EncodeToPNG();
            Object.DestroyImmediate(result);

            var screenshotsDir = Path.Combine(UnityEditorUtils.GetProjectRoot(), "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var fileName = string.Format("{0:yyyy-MM-dd-HHmmss}.png", timestamp);
            var filePath = Path.Combine(screenshotsDir, fileName);

            File.WriteAllBytes(filePath, bytes);

            Debug.Log($"Screenshot taken: {fileName}");
        }
    }
}
