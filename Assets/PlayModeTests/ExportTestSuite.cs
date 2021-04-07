using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using FileUtils;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TestTools;
using  UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace PlayModeTests
{
    public class ExportTestSuite
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ExportTestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        
        
        [UnityTest]
        public IEnumerator ExportingFilesRunsUniqueThreadForEachFile()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuMaterialUI");
            yield return new WaitForSeconds(3f);
            var selectorObject = GameObject.FindObjectOfType<UIElementsSelector>();
            var selectedObjects = GameObject.FindObjectsOfType(typeof(UIElementManager));
            foreach (var obj in selectedObjects)
            {
                selectorObject.selectedElements.Add(obj as UIElementManager);
            }
            var exporterObject = GameObject.FindObjectOfType<NativeAndroidExporter>();
            exporterObject.RepackRecordsAndShare();
            Assert.AreEqual(selectorObject.selectedElements.Count, exporterObject.threadPool.Count);
            yield return null;
        }
        
        
        
        [UnityTest]
        public IEnumerator ExportTestSuiteWithEnumeratorPasses()
        {
            
            
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
