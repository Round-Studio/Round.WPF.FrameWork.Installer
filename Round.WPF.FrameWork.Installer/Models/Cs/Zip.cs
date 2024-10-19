using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.WPF.FrameWork.Installer.Models.Cs
{
    internal class Zip
    {
        public class ZipExtractor
        {
            public event EventHandler<ExtractProgressEventArgs> ExtractProgress;

            public void ExtractZip(string zipFilePath, string outputDirectory)
            {
                int fileCount = 0;
                using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry entry;
                    int extractedCount = 0;

                    // 首先遍历ZIP文件，统计文件数量
                    while ((entry = zipInputStream.GetNextEntry()) != null)
                    {
                        fileCount++;
                    }
                    zipInputStream.Close();
                }
                using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry entry;
                    int extractedCount = 0;

                    // 再次遍历ZIP文件，提取文件
                    while ((entry = zipInputStream.GetNextEntry()) != null)
                    {
                        extractedCount++;
                        string outputPath = Path.Combine(outputDirectory, entry.Name);

                        if (entry.IsDirectory)
                        {
                            Directory.CreateDirectory(outputPath);
                        }
                        else
                        {
                            string directoryName = Path.GetDirectoryName(outputPath);
                            Directory.CreateDirectory(directoryName);

                            byte[] buffer = new byte[4096];
                            using (FileStream streamWriter = File.Create(outputPath))
                            {
                                StreamUtils.Copy(zipInputStream, streamWriter, buffer);
                            }
                            OnExtractProgress(new ExtractProgressEventArgs
                            {
                                TotalFiles = fileCount,
                                ExtractedFiles = extractedCount,
                                DisName = entry.Name
                            });
                        }
                    }
                }
            }

            protected virtual void OnExtractProgress(ExtractProgressEventArgs e)
            {
                ExtractProgress?.Invoke(this, e);
            }
        }

        public class ExtractProgressEventArgs : EventArgs
        {
            public int TotalFiles { get; set; }
            public int ExtractedFiles { get; set; }
            public string DisName { get; set; }
        }
    }
}
