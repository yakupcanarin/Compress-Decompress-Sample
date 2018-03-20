using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacZip
{
    class WorkSpace
    {
        private static IZip _functionInterface = new Zip();

        static void Main(string[] args)
        {
            //ZipMultipleFiles();
            //ZipFiles();
            //ModifyZip();
            //ExtractFiles();
            //isCompressed();
        }

        protected void ZipFiles()
        {
            _functionInterface.Compress(@"C:\Users\ykpcn\Desktop\aaaa.txt", @"C:\Users\ykpcn\Desktop\ziplenecek\deneme.zip", @"C:\Users\ykpcn\Desktop\ziplenecek");
        }

        protected static void ZipMultipleFiles()
        {
            string folder = @"C:\Users\ykpcn\Desktop\ziplenecek";
            _functionInterface.CompressMultipleFiles(folder, @"C:\Users\ykpcn\Desktop\MultiZip", @"C:\Users\ykpcn\Desktop\MultiZip\deniyoruz.zip");
        }

        protected static void ExtractFiles()
        {
            _functionInterface.DeCompress(@"C:\Users\ykpcn\Desktop\MultiZip\deniyoruz.zip", @"C:\Users\ykpcn\Desktop\çıkar");
        }

        protected static void ModifyZip()
        {
            _functionInterface.AddFiletoZip(@"C:\Users\ykpcn\Desktop\aaaa.txt", @"C:\Users\ykpcn\Desktop\deneme.zip", Path.GetFileName(@"C:\Users\ykpcn\Desktop\aaaa.txt"));
        }

        protected static void isCompressed()
        {
            Console.WriteLine(_functionInterface.isCompressed(@"C:\Users\ykpcn\Desktop\dene.zip"));
            Console.ReadLine();
        }
    }
}
