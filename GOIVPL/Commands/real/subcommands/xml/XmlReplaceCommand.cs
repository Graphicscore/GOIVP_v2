using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real.subcommands.xml
{
    [System.Xml.Serialization.XmlRoot("replace")]
    public class XmlReplaceCommand : XmlSubCommand
    {
        public XmlReplaceCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlAttribute("xpath")]
        public string XPath
        {
            get; set;
        }

        public override string getName()
        {
            return "XML Replace, xpath : " + XPath;
        }


    }
}
