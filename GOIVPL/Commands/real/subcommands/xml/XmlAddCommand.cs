using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real.subcommands.xml
{
    [System.Xml.Serialization.XmlRoot("add")]
    public class XmlAddCommand : XmlSubCommand
    {
        public XmlAddCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlAttribute("xpath")]
        public string XPath
        {
            get; set;
        }

        [System.Xml.Serialization.XmlAttribute("append")]
        public string Append
        {
            get; set;
        }

        public override string getName()
        {
            return "XML Add, xpath : " + XPath;
        }


    }
}
