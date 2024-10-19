using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Round.WPF.FrameWork.Installer.Models.Cs.GetInstallConfig;

namespace Round.WPF.FrameWork.Installer.Models.Cs
{
    internal class SystemShell
    {
        public static void CreateShortcut(string installpath)
        {
            foreach (Desktop desk in GetInstallConfig.GetInstallConfigForJson().LinkList) {
                string deskTop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                string dirPath = installpath;
                System.Diagnostics.FileVersionInfo exeInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(installpath+"\\"+desk.RunURL);

                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + desk.LinkTitle+".lnk");
                shortcut.TargetPath = installpath+"\\"+desk.RunURL;         //目标文件
                shortcut.WorkingDirectory = dirPath;    //目标文件夹
                shortcut.WindowStyle = 1;               //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
                shortcut.RelativePath = dirPath;
                shortcut.Description = "Round Installer 安装程序安装";   //描述
                shortcut.IconLocation = installpath+"\\"+desk.LinkIcon;
                shortcut.Save();

                GL.MainWindowConfig.Pathss = installpath;
            }
        }
    }
}
