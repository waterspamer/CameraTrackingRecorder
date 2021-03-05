using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FileUtils
{
    public static class AsyncFileUtils
    {

        private const int DefaultBufferSize = 4096;

        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static Task<string[]> ReadAllLinesAsync(string path)
        {
            return ReadAllLinesAsync(path, Encoding.UTF8);
        }

        public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding)
        {
            var lines = new List<string>();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize,
                DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Debug.Log("New Line");
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}