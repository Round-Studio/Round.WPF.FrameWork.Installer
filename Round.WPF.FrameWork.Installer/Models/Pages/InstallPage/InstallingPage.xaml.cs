using Round.WPF.FrameWork.Installer.Models.Cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Round.WPF.FrameWork.Installer.Models.Cs.Zip;

namespace Round.WPF.FrameWork.Installer.Models.Pages.InstallPage
{
    /// <summary>
    /// InstallingPage.xaml 的交互逻辑
    /// </summary>
    public partial class InstallingPage : Page
    {
        public InstallingPage(string Path)
        {
            InitializeComponent();

            Task.Run(() => {
                var extractor = new ZipExtractor();
                extractor.ExtractProgress += (sender, e) => {
                    JDBox.Dispatcher.Invoke(new Action(() => {
                        JDBox.Maximum = e.TotalFiles;
                        JDBox.Value = e.ExtractedFiles;
                        FileName.Content = $"({e.ExtractedFiles}/{e.TotalFiles}):{e.DisName}";
                    }));
                };

                // 获取当前程序集的完整路径
                string assemblyPath = Assembly.GetExecutingAssembly().Location;

                // 获取程序集所在的目录
                string assemblyDirectory = System.IO.Path.GetDirectoryName(assemblyPath);
                extractor.ExtractZip(assemblyDirectory+"\\install.zip", Path);
                SystemShell.CreateShortcut(Path);

                GL.MainWindowConfig.MainFrame.Dispatcher.Invoke(() =>
                {
                    GL.MainWindowConfig.MainFrame.Content = new InstallEndPage();
                });
            });
        }
    }
}
