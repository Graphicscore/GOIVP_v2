using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class text : Command
    {
        private string path;
        private string createIfNotExists;

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

        public text() : base()
        {

        }

        public override string getString()
        {
            return "text, path=" + path;
        }
    }
}
