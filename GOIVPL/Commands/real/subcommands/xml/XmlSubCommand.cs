using GOIVPL.Commands.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GOIVPL.Commands.real.subcommands.xml
{
    public abstract class XmlSubCommand : SubCommand
    {
        [System.Xml.Serialization.XmlAnyElement]
        public List<XmlElement> Elements { get; set; }

        public XmlSubCommand()
        {
            Elements = new List<XmlElement>();
        }
    }
}
