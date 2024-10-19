using Microsoft.Win32;
using Round.WPF.FrameWork.Installer.Make.Models.Controls;
using Round.WPF.FrameWork.Installer.Make.Models.Cs;
using Round.WPF.FrameWork.Installer.Make.Models.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Round.WPF.FrameWork.Installer.Make
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GL.LinkListBox = LinkList;
            GL.FileListBox = FileList;
            GL.JDBar = JDBar;
        }

        private void FlushedFileList()
        {
            FileList.Items.Clear();
            LinkList.Items.Clear();
            foreach (var dir in GL.DirList)
            {
                FileList.Items.Add(new DirButton(dir));
            }
            foreach (var file in GL.FileList)
            {
                FileList.Items.Add(new FileButton(file));
            }
            foreach (var link in GL.LinkList)
            {
                LinkList.Items.Add(new LinkButton(link.LinkTitle));
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "所有文件 (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                GL.FileList.Add(selectedFile);
                FlushedFileList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "选择文件夹";

                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    GL.DirList.Add(selectedFolder);
                    FlushedFileList();
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChooseLinkFileWindow chooseLinkFileWindow = new ChooseLinkFileWindow();
            chooseLinkFileWindow.ShowDialog();
            FlushedFileList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GL.Root.InstallTitle = titbox.Text;
            GL.Root.InstallDefaultPath = defpathbox.Text + "\\" + titbox.Text;
            GL.Root.InstallArticle = MultilineTextBox.Text;

            Task.Run(() =>
            {
                MakeCore.MakeInstallerMain();
                this.Dispatcher.Invoke(() =>
                {
                    System.Windows.MessageBox.Show("文件已打包至当前目录下的 Installer.exe!","提示",MessageBoxButton.OK,MessageBoxImage.Information);
                    GL.JDBar.Value = 0;
                });
            });
        }
    }
}
