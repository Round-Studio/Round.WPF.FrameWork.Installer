using Round.WPF.FrameWork.Installer.Models.Cs;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Round.WPF.FrameWork.Installer.Models.Pages
{
    /// <summary>
    /// InstallArticlePage.xaml 的交互逻辑
    /// </summary>
    public partial class InstallArticlePage : Page
    {
        public InstallArticlePage()
        {
            InitializeComponent();

            ArtickeBox.Text = GetInstallConfig.GetInstallConfigForJson().InstallArticle;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (EnabledNext.IsChecked == true)
            {
                Next.IsEnabled = true;
            }
            else
            {
                Next.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GL.MainWindowConfig.MainFrame.Content = new InstallHomePage();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            GL.MainWindowConfig.MainFrame.Content = new InstallChooseInstallPath();
        }
    }
}
