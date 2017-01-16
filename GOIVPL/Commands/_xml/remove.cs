using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands._xml
{
    [System.Xml.Serialization.XmlType("removeXml")]
    public class remove : Command
    {
        private String xpath;

        [System.Xml.Serialization.XmlAttribute("xpath")]
        public string XPath
        {
            get
            {
                return xpath;
            }

            set
            {
                xpath = value;
            }
        }

        public remove() : base()
        {

        }

        public override string getString()
        {
            return "xml remove, xpath=" + xpath;
        }
    }
}
