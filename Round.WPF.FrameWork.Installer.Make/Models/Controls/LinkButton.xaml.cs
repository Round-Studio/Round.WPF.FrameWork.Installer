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
    /// LinkButton.xaml 的交互逻辑
    /// </summary>
    public partial class LinkButton : UserControl
    {
        public LinkButton(string name)
        {
            InitializeComponent();
            Names.Content = name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var name in GL.LinkList)
            {
                if (name.LinkTitle == Names.Content.ToString())
                {
                    GL.LinkList.Remove(name); GL.FlushedFileList(); return;
                }
            }
        }
    }
}
