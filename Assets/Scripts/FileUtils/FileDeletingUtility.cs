using System.IO;

namespace FileUtils
{
    public static class FileDeletingUtility
    {
        public static void DeleteRecordFile(string fileName) =>
            File.Delete(fileName);
    }
}
