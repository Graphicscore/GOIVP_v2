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
    public class xml : Command
    {
        private string path;

        [System.Xml.Serialization.XmlIgnore]
        private String localXmlFilePath;

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

        [System.Xml.Serialization.XmlIgnore]
        public string LocalXmlFilePath
        {
            get
            {
                return localXmlFilePath;
            }

            set
            {
                localXmlFilePath = value;
            }
        }
        public xml() : base()
        {

        }

        public override string getString()
        {
            return "xml, path=" + path;
        }
    }
}
