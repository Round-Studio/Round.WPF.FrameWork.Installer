using Microsoft.Win32;
using Round.WPF.FrameWork.Installer.Models.Cs;
using Round.WPF.FrameWork.Installer.Models.Pages.InstallPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Round.WPF.FrameWork.Installer.Models.Pages
{
    /// <summary>
    /// InstallChooseInstallPath.xaml 的交互逻辑
    /// </summary>
    public partial class InstallChooseInstallPath : Page
    {
        public InstallChooseInstallPath()
        {
            InitializeComponent();

            PathBox.Text=GetInstallConfig.GetInstallConfigForJson().InstallDefaultPath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GL.MainWindowConfig.MainFrame.Content = new InstallArticlePage();
        }

        private void Next1复制__C__Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // 显示对话框
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;

                PathBox.Text = selectedFolderPath;
            }
        }

        private void Next1_Click(object sender, RoutedEventArgs e)
        {
            GL.MainWindowConfig.MainFrame.Content = new InstallingPage(PathBox.Text);
        }
    }
}
