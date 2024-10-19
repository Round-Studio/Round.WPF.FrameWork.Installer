using Round.WPF.FrameWork.Installer.Models.Cs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Round.WPF.FrameWork.Installer.Models.Pages.InstallPage
{
    /// <summary>
    /// InstallEndPage.xaml 的交互逻辑
    /// </summary>
    public partial class InstallEndPage : Page
    {
        public InstallEndPage()
        {
            InitializeComponent();

            RunCh.Content = RunCh.Content.ToString().Replace("{InstallerTitle}",GetInstallConfig.GetInstallConfigForJson().InstallTitle);
        }

        private void Next1_Click(object sender, RoutedEventArgs e)
        {
            if (RunCh.IsChecked == true) {
                foreach (var Desktop in GetInstallConfig.GetInstallConfigForJson().LinkList)
                {
                    Process.Start(GL.MainWindowConfig.Pathss+"\\"+Desktop.RunURL);
                }
            }
            Thread.Sleep(100);
            System.Environment.Exit(0);
        }
    }
}
