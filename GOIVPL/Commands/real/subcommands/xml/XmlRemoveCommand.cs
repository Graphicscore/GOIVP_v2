using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real.subcommands.xml
{
    [System.Xml.Serialization.XmlRoot("remove")]
    public class XmlRemoveCommand : XmlSubCommand
    {
        public XmlRemoveCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlAttribute("xpath")]
        public string XPath
        {
            get; set;
        }

        public override string getName()
        {
            return "XML Remove, xpath : " + XPath;
        }


    }
}
