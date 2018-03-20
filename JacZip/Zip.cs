using System;
using System.IO;
using System.IO.Compression;

namespace JacZip
{
    public class Zip : IZip
    {
        #region Compress One File Byte To Byte 
        public void Compress(string FilePath, string ZipPath, string CreateFolder) // FilePath is the path's which file you want to compress
                                                                                   // ZipPath is the path to where you want to compress your file
        {                                                                            //createFolder is the path to if you don't have the folder on the path.    
            try
            {
                byte[] read = new byte[4096];
                int readByte = 0;

                MemoryStream _mStream = new MemoryStream();
                ZipArchive archive = new ZipArchive(_mStream, ZipArchiveMode.Create, true);
                ZipArchiveEntry fileArchive = archive.CreateEntry(FilePath);
                var OpenFileinArchive = fileArchive.Open();
                FileStream _fsReader = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

                while (_fsReader.Position != _fsReader.Length)
                {
                    readByte = _fsReader.Read(read, 0, read.Length);
                    OpenFileinArchive.Write(read, 0, readByte);
                }

                _fsReader.Dispose();
                OpenFileinArchive.Close();
                archive.Dispose();
                if (!Directory.Exists(CreateFolder))
                {
                    Directory.CreateDirectory(CreateFolder);
                    if (!Directory.Exists(ZipPath))
                    {
                        using (var _fs = new FileStream(ZipPath, FileMode.Create))
                        {
                            _mStream.Seek(0, SeekOrigin.Begin);
                            _mStream.CopyTo(_fs);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The folder already exists. Change the name and try again.");
                        Console.ReadLine();
                    }
                }

                else
                {
                    if (!File.Exists(ZipPath))
                    {
                        using (var _fs = new FileStream(ZipPath, FileMode.Create))
                        {
                            _mStream.Seek(0, SeekOrigin.Begin);
                            _mStream.CopyTo(_fs);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The file already exists. Change the name and try again.");
                        Console.ReadLine();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadLine();
            }
        }
        #endregion

        #region Compress Multi Files
        public void CompressMultipleFiles(string ZipEntries, string CreateFolder, string DestinationFile)
        {
            try
            {
                if (!Directory.Exists(CreateFolder))
                {
                    Directory.CreateDirectory(CreateFolder);
                    if (!File.Exists(DestinationFile))
                    {
                        using (ZipArchive zipArchive = ZipFile.Open(DestinationFile, ZipArchiveMode.Create))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(ZipEntries);
                            FileInfo[] FilestoArchive = dirInfo.GetFiles();
                            if (FilestoArchive != null && FilestoArchive.Length > 0)
                            {
                                foreach (FileInfo item in FilestoArchive)
                                {
                                    zipArchive.CreateEntryFromFile(item.FullName, item.Name, CompressionLevel.Optimal);
                                }
                            }
                        }
                        Console.WriteLine("Compress has successfully completed.\nPress enter to exit");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("The file already exist. Change the name and try again");
                    }
                }
                else
                {
                    if (!File.Exists(DestinationFile))
                    {
                        using (ZipArchive zipArchive = ZipFile.Open(DestinationFile, ZipArchiveMode.Create))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(ZipEntries);
                            FileInfo[] FilestoArchive = dirInfo.GetFiles();
                            if (FilestoArchive != null && FilestoArchive.Length > 0)
                            {
                                foreach (FileInfo item in FilestoArchive)
                                {
                                    zipArchive.CreateEntryFromFile(item.FullName, item.Name, CompressionLevel.Optimal);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The file already exists. \nChange the name and try again");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message, "\nPress enter to exit");
                Console.ReadLine();
            }

        }
        #endregion

        #region Decompress
        public void DeCompress(string ZipPath, string DestinationPath)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(ZipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {

                        if (!Directory.Exists(DestinationPath))
                        {
                            Directory.CreateDirectory(DestinationPath);
                            entry.ExtractToFile(Path.Combine(DestinationPath, Path.GetFileName(entry.FullName)));
                        }
                        else
                        {
                            entry.ExtractToFile(Path.Combine(DestinationPath, Path.GetFileName(entry.FullName)));
                        }

                    }
                    Console.WriteLine("Decompress has successfully done.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadLine();
            }

        }

        #endregion

        #region Add File to Existing Zip

        public void AddFiletoZip(string FilePath, string ZipPath, string FileName)
        {
            try
            {
                if (!File.Exists(ZipPath) || !File.Exists(FilePath))
                {
                    Console.WriteLine("Zip file or other file that which you want to add the zip file is not exist.");
                    Console.ReadLine();
                }
                else
                {
                    using (var zipArchive = ZipFile.Open(ZipPath, ZipArchiveMode.Update))
                    {
                        var info = new FileInfo(FilePath);
                        zipArchive.CreateEntryFromFile(Path.GetFullPath(info.FullName), FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0} ", ex.Message);
                throw;
            }
        }

        #endregion

        #region IsCompressed

        public string isCompressed(string ZipFilePath)
        {
            string message = "";
            try
            {
                if (File.Exists(ZipFilePath))
                {
                    using (var archive = ZipFile.OpenRead(ZipFilePath))  // Open zip file and control 
                    {                                           // if we can open the file that means it's a real zipped file.
                        if (archive.Entries.Count > 0)          // check it if it have entries .
                        {
                            message = "This is a compressed (.zip) file.\nPress enter to exit.";
                        }
                        else if (archive.Entries.Count == 0)  // check it if it doesn't have entries.
                        {
                            message = "This is a compressed (.zip) file but it's empty.\nPress enter to exit.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("This is not a compressed (.zip) file or " + ex.Message + ".\nPress enter to exit."); // if we can't read the file then it 
                Console.ReadLine();                                                                                 //means this is not a zipped file
            }                                                                                                        // only its extention is .zip
            return message;
        }

        #endregion
    }
}