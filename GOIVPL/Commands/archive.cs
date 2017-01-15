using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class archive : Command
    {
        private string path;
        private string createIfNotExists;
        private string type;

        [System.Xml.Serialization.XmlAttribute("path")]
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        [System.Xml.Serialization.XmlAttribute("createIfNotExist")]
        public string CreateIfNotExists
        {
            get
            {
                return createIfNotExists;
            }

            set
            {
                createIfNotExists = value;
            }
        }

        [System.Xml.Serialization.XmlAttribute("type")]
        public string ArchiveType
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public archive() : base()
        {

        }

        public override string getString()
        {
            return "archive, path=" + path + ", type=" + type;
        }
    }
}
