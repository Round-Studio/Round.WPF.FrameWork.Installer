using Round.WPF.FrameWork.Installer.Models.Cs;
using System;
using System.Collections.Generic;
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

namespace Round.WPF.FrameWork.Installer.Models.Pages
{
    /// <summary>
    /// LoadingPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            InitializeComponent();

            Task.Run(() => {
                Thread.Sleep(800);

                GL.MainWindowConfig.MainFrame.Dispatcher.Invoke(() =>
                {
                    GL.MainWindowConfig.MainFrame.Content = new InstallHomePage();
                });
            });
        }
    }
}
