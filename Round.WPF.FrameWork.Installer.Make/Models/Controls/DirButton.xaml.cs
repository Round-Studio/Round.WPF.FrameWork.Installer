using Round.WPF.FrameWork.Installer.Make.Models.Cs;
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

namespace Round.WPF.FrameWork.Installer.Make.Models.Controls
{
    /// <summary>
    /// DirButton.xaml 的交互逻辑
    /// </summary>
    public partial class DirButton : UserControl
    {
        public DirButton(string path)
        {
            InitializeComponent();
            for (int i = 0; i < path.Length; i++)
            {
                if (i % 90 == 0 && i != 0)
                {
                    Names.Content += "\n" + path[i];
                }
                else
                {
                    Names.Content = Names.Content.ToString() + path[i];
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var name in GL.DirList)
            {
                if (name == Names.Content.ToString())
                {
                    GL.DirList.Remove(name);
                    GL.FlushedFileList();
                    return;
                }
            }
        }
    }
}
