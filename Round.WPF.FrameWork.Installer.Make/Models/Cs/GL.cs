using Round.WPF.FrameWork.Installer.Make.Models.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Round.WPF.FrameWork.Installer.Make.Models.Cs
{
    internal class GL
    {
        public static List<string> DirList = new List<string>();
        public static List<string> FileList = new List<string>();
        public static List<EntityClasses.LinkConfig> LinkList = new List<EntityClasses.LinkConfig>();
        public static EntityClasses.Root Root = new EntityClasses.Root();
        public static ListBox FileListBox = new ListBox();
        public static ListBox LinkListBox = new ListBox();
        public static ProgressBar JDBar = new ProgressBar();
        public static void FlushedFileList()
        {
            FileListBox.Items.Clear();
            LinkListBox.Items.Clear();
            foreach (var dir in GL.DirList)
            {
                FileListBox.Items.Add(new DirButton(dir));
            }
            foreach (var file in GL.FileList)
            {
                FileListBox.Items.Add(new FileButton(file));
            }
            foreach (var link in GL.LinkList)
            {
                LinkListBox.Items.Add(new LinkButton(link.LinkTitle));
            }
        }
    }
}
