using System.Collections;
using System.IO;
using FileUtils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.FileRoutinesTests
{
    public class FileRoutinesTestSuite
    {
        private static string testFileDirectoryFolder => Application.persistentDataPath;

        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }


        [Test]
        public void DeletingUtilityActuallyDeletesFile()
        {
            var path = $"{testFileDirectoryFolder}\\newFile.txt";
            File.Create(path).Dispose();
            FileDeletingUtility.DeleteRecordFile(path);
            Assert.AreEqual(false, File.Exists(path));
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