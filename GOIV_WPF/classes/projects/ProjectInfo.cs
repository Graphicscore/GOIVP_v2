using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIV_WPF.classes.projects
{
    public class ProjectInfo
    {
        public DirectoryInfo ProjectPath{ get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModified { get; set; }
        public String ProjectName { get; set; }
        public Image Icon{ get; set; }
        public String Version { get; set; }
    }
}
