using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Round.WPF.FrameWork.Installer.Models.Cs
{
    internal class GetInstallConfig
    {
        public class Desktop
        {
            public string LinkIcon { get; set; }
            public string LinkTitle { get; set; }
            public string RunURL { get; set; }
        }
        public class Config
        {
            public string InstallTitle { get; set; }
            public string InstallArticle { get; set; }
            public string InstallDefaultPath { get; set; }
            public List<Desktop> LinkList { get; set; }
        }
        public static Config GetInstallConfigForJson() {
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("InstallConfig.json"));
            return config;
        }
    }
}
