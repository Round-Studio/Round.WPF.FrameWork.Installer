using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Round.WPF.FrameWork.Installer.Make.Models.Cs
{
    internal class MakeCore
    {
        public static void MakeInstallerMain()
        {
            WriteJsonFile();
        }
        public static void WriteJsonFile()
        {
            GL.Root.LinkList = GL.LinkList;

            var Json = GL.Root;
            string jsonstr = JsonConvert.SerializeObject(Json);
            if (Directory.Exists("bin\\MakeCore")) Directory.Delete("bin\\MakeCore",true);
            Directory.CreateDirectory("bin\\MakeCore");
            File.WriteAllText("bin\\MakeCore\\InstallConfig.json", jsonstr);
            File.WriteAllBytes("bin\\WinRAR.zip", Properties.Resources.WinRAR);
            ExtractZipFile("bin\\WinRAR.zip", "bin");

            if (File.Exists("bin\\Temp\\temp.zip")) File.Delete("bin\\Temp\\temp.zip");
            if (Directory.Exists("bin\\Temp\\Make")) Directory.Delete("bin\\Temp\\Make",true);

            Directory.CreateDirectory("bin\\Temp\\Make");
            foreach (var item in GL.DirList)
            {
                CopyFolder(item, "bin\\Temp\\Make");
            }
            GL.JDBar.Dispatcher.Invoke(() =>
            {
                GL.JDBar.Maximum = GL.FileList.Count();
                GL.JDBar.Value = 0;
            });
            foreach (var item in GL.FileList)
            {
                CopyFIle(item, "bin\\Temp\\Make\\"+Path.GetFileName(item));
                GL.JDBar.Dispatcher.Invoke(() =>
                {
                    GL.JDBar.Value++;
                });
            }
            if (File.Exists("bin\\Temp\\install.zip")) File.Delete("bin\\Temp\\install.zip");
            Zip.MakeZip("bin\\Temp\\Make", "bin\\Temp\\install.zip");
            GL.JDBar.Dispatcher.Invoke(() =>
            {
                GL.JDBar.Maximum = 100;
                GL.JDBar.Value = 0;
            });
            File.WriteAllBytes("bin\\Lib.zip", global::Round.WPF.FrameWork.Installer.Make.Properties.Resources.Lib);
            File.WriteAllBytes("bin\\icon.ico",Properties.Resources.briefcase);

            CopyFIle("bin\\Temp\\install.zip", "bin\\MakeCore\\install.zip");
            ExtractZipFile("bin\\Lib.zip", "bin\\MakeCore");
            GL.JDBar.Dispatcher.Invoke(() =>
            {
                GL.JDBar.Value = 70;
            });
            ByExe();
            GL.JDBar.Dispatcher.Invoke(() =>
            {
                GL.JDBar.Value = 100;
            });
        }
        /*public static void ByExe()
        {
            string rcstr = "";
            foreach (var item in Directory.GetFiles("bin\\MakeCore"))
            {
                rcstr += "IDR_" + Path.GetFileName(item).Replace(".", "_") + " LYSHARK " + item + "\n";
            }
            File.WriteAllText("bin\\Resource.rc", rcstr);
            File.WriteAllText("bin\\Main.cpp", "#define _CRT_SECURE_NO_WARNINGS\r\n#include <Windows.h>\r\n#include <iostream>\r\n#include <WinUser.h>\r\n#include <cstdio>\r\n#include <cstdlib>\r\n#include <string>\r\n\r\nusing namespace std;\r\n\r\nBOOL UseCustomResource(const string& resourceName)\r\n{\r\n    HMODULE hModule = GetModuleHandle(NULL);\r\n    if (hModule == NULL)\r\n    {\r\n        cerr << \"错误：获取模块句柄失败。\" << endl;\r\n        return FALSE;\r\n    }\r\n\r\n    HRSRC hRsrc = FindResource(hModule, resourceName.c_str(), \"LYSHARK\");\r\n    if (hRsrc == NULL)\r\n    {\r\n        cerr << \"错误：无法找到资源 \" << resourceName << \".\" << endl;\r\n        return FALSE;\r\n    }\r\n\r\n    DWORD dwSize = SizeofResource(hModule, hRsrc);\r\n    if (dwSize == 0)\r\n    {\r\n        cerr << \"错误：资源 \" << resourceName << \" 的大小无效。\" << endl;\r\n        return FALSE;\r\n    }\r\n\r\n    HGLOBAL hGlobal = LoadResource(hModule, hRsrc);\r\n    if (hGlobal == NULL)\r\n    {\r\n        cerr << \"错误：无法加载资源 \" << resourceName << \".\" << endl;\r\n        return FALSE;\r\n    }\r\n\r\n    LPVOID lpVoid = LockResource(hGlobal);\r\n    if (lpVoid == NULL)\r\n    {\r\n        cerr << \"错误：无法锁定资源 \" << resourceName << \".\" << endl;\r\n        FreeResource(hGlobal);\r\n        return FALSE;\r\n    }\r\n\r\n    char tempPath[MAX_PATH];\r\n    if (GetTempPath(MAX_PATH, tempPath) == 0)\r\n    {\r\n        cerr << \"错误：获取临时路径失败。\" << endl;\r\n        return FALSE;\r\n    }\r\n\r\n    // 替换文件名中的下划线为点\r\n    string fileName = resourceName;\r\n    for (size_t i = 0; i < fileName.size(); ++i)\r\n    {\r\n        if (fileName[i] == '_')\r\n        {\r\n            fileName[i] = '.';\r\n        }\r\n    }\r\n\r\n    string filePath = tempPath;\r\n    filePath += \"\\\\\\\\\" + fileName;\r\n\r\n    FILE* fp = fopen(filePath.c_str(), \"wb\");\r\n    if (fp == NULL)\r\n    {\r\n        cerr << \"错误：无法创建或打开文件 \" << filePath << \".\" << endl;\r\n        return FALSE;\r\n    }\r\n\r\n    fwrite(lpVoid, sizeof(char), dwSize, fp);\r\n    fclose(fp);\r\n    return TRUE;\r\n}\r\n\r\nint main(int argc, char* argv[])\r\n{\r\n    string resources[] = {\r\n        \"IDR_ControlzEx_dll\",\r\n        \"IDR_Costura_dll\",\r\n        \"IDR_ICSharpCode_SharpZipLib_dll\",\r\n        \"IDR_install_zip\",\r\n        \"IDR_InstallConfig_json\",\r\n        \"IDR_MahApps_Metro_dll\",\r\n        \"IDR_Microsoft_Xaml_Behaviors_dll\",\r\n        \"IDR_Newtonsoft_Json_dll\",\r\n        \"IDR_Round_WPF_FrameWork_Installer_exe\",\r\n        \"IDR_Round_WPF_FrameWork_Installer_exe_config\",\r\n        \"IDR_Round_WPF_FrameWork_Installer_exe_manifest\",\r\n        \"IDR_System_Buffers_dll\",\r\n        \"IDR_System_Diagnostics_DiagnosticSource_dll\",\r\n        \"IDR_System_Memory_dll\",\r\n        \"IDR_System_Numerics_Vectors_dll\",\r\n        \"IDR_System_Runtime_CompilerServices_Unsafe_dll\",\r\n        \"IDR_System_Threading_Tasks_Extensions_dll\"\r\n    };\r\n\r\n    size_t resourceCount = sizeof(resources) / sizeof(resources[0]);\r\n\r\n    for (size_t i = 0; i < resourceCount; ++i)\r\n    {\r\n        BOOL ref = UseCustomResource(resources[i]);\r\n        cout << \"资源 \" << resources[i] << \" 释放状态: \" << ref << endl;\r\n    }\r\n\r\n    int result = system(\"Round.WPF.FrameWork.Installer.exe\");\r\n\r\n    return 0;\r\n}");

            string commandstr = "/c windres bin\\Resource.rc -O coff -o bin\\Resource.o&g++ -c bin\\Main.cpp -o bin\\Main.o&g++ bin\\Main.o bin\\Resource.o -o output.exe";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = commandstr
            };
            process.Start();
        }*/

        public static void ByExe()
        {
            File.WriteAllText("Config.txt", "TempMode\r\nSilent=1\r\nOverwrite=2\r\n\r\nSetup=Round.WPF.FrameWork.Installer.exe");
            File.WriteAllText("bin\\Run.bat", "@echo off\r\ncd bin/MakeCore\r\nset softname=../../Installer.exe\r\n\r\n..\\WinRAR\\WinRAR.exe a -sfx -iicon../icon.ico \"%softname%\" ./\r\n\r\n..\\WinRAR\\Rar.exe c -z../../config.txt \"%softname%\"");
            Process.Start("cmd.exe", "/c bin\\Run.bat").StartInfo.CreateNoWindow = true;
            //Process.Start("cmd.exe", "/c cd bin/MakeCore&set softname=../../Installer.exe&..\\WinRAR\\WinRAR.exe a -sfx \"%softname%\" ./&..\\WinRAR\\Rar.exe c -z../../config.txt \"%softname%\"");
        }
        public static void CopyFIle(string sourceFile,string destinationFile)
        {
            File.Copy(sourceFile, destinationFile, true);
        }

        public static void CopyFolder(string sourceDir, string destinationDir)
        {
            // 确保源文件夹存在
            if (!Directory.Exists(sourceDir))
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDir);
            }

            try
            {
                // 如果目标文件夹不存在，则创建它
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                GL.JDBar.Dispatcher.Invoke(() =>
                {
                    GL.JDBar.Maximum = Directory.GetFiles(sourceDir).Count();
                    GL.JDBar.Value = 0;
                });

                // 复制文件
                foreach (string file in Directory.GetFiles(sourceDir))
                {
                    string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                    try
                    {
                        File.Copy(file, destFile, true); // true 表示如果目标文件已存在，则覆盖它
                        GL.JDBar.Dispatcher.Invoke(() =>
                        {
                            GL.JDBar.Value++;
                        });
                    }
                    catch { }
                }

                // 递归复制子文件夹
                foreach (string folder in Directory.GetDirectories(sourceDir))
                {
                    string destFolder = Path.Combine(destinationDir, Path.GetFileName(folder));
                    CopyFolder(folder, destFolder);
                }
            }
            catch { }
        }
        public static void ExtractZipFile(string zipFilePath, string extractionPath)
        {
            try
            {
                // 确保ZIP文件存在
                if (!File.Exists(zipFilePath))
                {
                    throw new FileNotFoundException("The ZIP file was not found.", zipFilePath);
                }

                // 创建目标目录（如果不存在）
                Directory.CreateDirectory(extractionPath);

                // 解压ZIP文件
                ZipFile.ExtractToDirectory(zipFilePath, extractionPath);
            }catch { }
        }
    }
}
