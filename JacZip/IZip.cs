using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacZip
{
    public interface IZip
    {
        void Compress(string FilePath, string ZipPath, string CreateFolder);

        void CompressMultipleFiles(string ZipEntries, string CreateFolder, string DestinationFile);

        void DeCompress(string ZipPath, string DestinationPath);

        void AddFiletoZip(string FilePath, string ZipPath, string FileName);

        string isCompressed(string ZipFilePath);
    }
}
