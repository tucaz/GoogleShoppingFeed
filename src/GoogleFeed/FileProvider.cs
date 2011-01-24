using System.IO;

namespace GoogleFeed
{
    public class FileProvider : IFileProvider
    {
        private const int BUFFER_SIZE = 4096;
        
        private string _cacheDir;
        private string _cacheFileName;

        public FileProvider(string cacheDir, string cacheFileName)
        {
            this._cacheDir = cacheDir;
            this._cacheFileName = cacheFileName;
        }

        public FileProvider()
        {
            this._cacheDir = @"C:\temp";
            this._cacheFileName = "google_products.xml";
        }

        public Stream GetWriter(out bool isNewFile)
        {
            var cacheFilePath = Path.Combine(_cacheDir, _cacheFileName);

            if (!File.Exists(cacheFilePath))
            {
                isNewFile = true;
                var newFile = File.Create(cacheFilePath, BUFFER_SIZE, FileOptions.SequentialScan);                
                return newFile;
            }
            else
            {
                isNewFile = false;
                var existingFile = File.Open(cacheFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return existingFile;
            }
        }
    }

    public interface IFileProvider
    {
        Stream GetWriter(out bool isNewFile);
    }
}
