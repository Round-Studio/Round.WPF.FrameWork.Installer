using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.WPF.FrameWork.Installer.Make.Models.Cs
{
    internal class EntityClasses
    {
        public class LinkConfig
        {
            public string LinkIcon { get; set; }
            public string LinkTitle { get; set; }
            public string RunURL { get; set; }
        }
        public class Root
        {
            public string InstallTitle { get; set; }
            public string InstallArticle { get; set; }
            public string InstallDefaultPath { get; set; }
            public List<LinkConfig> LinkList { get; set; }
        }
    }
}
