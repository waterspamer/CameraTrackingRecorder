using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using FileUtils;
using NUnit.Framework;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.TestTools;
using  UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PlayModeTests
{
    public class RuntimeUITestSuite
    {
        
        const string uiElementObject = "Assets/Prefabs/RecordUIPrefab.prefab";
        GameObject m_PrefabRoot;

        const string kPrefabPath = "Assets/Resources/ButtonPrefab.prefab";
        
        
        
        
        
        
        // A Test behaves as an ordinary method
        [Test]
        public void ExportTestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.

        [UnityTest]
        public IEnumerator ShortPressOnObjectCausesOnPointerClickEvent()
        {
            var items  = GameObject.FindObjectsOfType<UIElementManager>();
            var button = (Button)((UIElementManager) items[0]).gameObject.GetComponentInChildren(typeof(Button));
            bool called = false;
            button.onClick.AddListener(() => { called = true; });
            button.OnPointerClick(new PointerEventData(GameObject.FindObjectOfType<EventSystem>()) { button = PointerEventData.InputButton.Left });
            Assert.True(called);
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