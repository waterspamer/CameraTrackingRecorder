using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UITestSuite
    {



        private static string testFileDirectoryFolder => Application.persistentDataPath;
        
        
        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }



        [Test]
        public void ClickingOnFileInvokesReplayWindowAnimatorActivation()
        {
            
        }

        [Test]
        public void LongClickOnElementInvokesInstrumentPanelActivation()
        {
            
        }

        
        
        

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}