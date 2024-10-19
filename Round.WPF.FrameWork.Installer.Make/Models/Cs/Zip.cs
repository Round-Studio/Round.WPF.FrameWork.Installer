using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.WPF.FrameWork.Installer.Make.Models.Cs
{
    internal class Zip
    {
        public static bool MakeZip(string sourceFolder,string destinationZip)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Source folder does not exist.");
                return false;
            }

            try
            {
                ZipFile.CreateFromDirectory(sourceFolder, destinationZip, CompressionLevel.Optimal, false);
                Console.WriteLine("Folder successfully compressed into zip file.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}
