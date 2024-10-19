using Round.WPF.FrameWork.Installer.Make.Models.Cs;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using static Round.WPF.FrameWork.Installer.Make.Models.Cs.EntityClasses;

namespace Round.WPF.FrameWork.Installer.Make.Models.Windows
{
    /// <summary>
    /// ChooseLinkFileWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseLinkFileWindow : Window
    {
        public ChooseLinkFileWindow()
        {
            InitializeComponent();

            foreach (var item in GL.FileList)
            {
                ComListBox.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择图标文件";
            openFileDialog.Filter = "图标文件 (*.ico)|*.ico";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                foreach (var item in GL.FileList)
                {
                    if (selectedFile == item)
                    {
                        IconPathBox.Text = selectedFile;
                        return;
                    }
                }

                if(System.Windows.MessageBox.Show("包含文件内无此图标文件，请问是否添加？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    GL.FileList.Add(selectedFile);
                    IconPathBox.Text = selectedFile;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                LinkConfig linkConfig = new LinkConfig();
                linkConfig.LinkIcon = System.IO.Path.GetFileName(IconPathBox.Text);
                if (ComListBox.SelectedItem == null)
                {
                    throw new Exception("未选择文件");
                }
                linkConfig.RunURL = System.IO.Path.GetFileName(ComListBox.SelectedItem.ToString());
                linkConfig.LinkTitle = NameBox.Text;

                GL.LinkList.Add(linkConfig);
            }
            catch
            {
                System.Windows.MessageBox.Show("未选择文件！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }
    }
}
