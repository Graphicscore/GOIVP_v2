using GOIVPL.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GOIVPL.Info
{
    [Serializable()]
    public class Content
    {
        private XmlElement[] elements;

        public Content()
        {

        }

        [System.Xml.Serialization.XmlAnyElement]
        public XmlElement[] Elements
        {
            get
            {
                return elements;
            }

            set
            {
                elements = value;
            }
        }

        public Command[] getCommands()
        {
            return OIVPManager.getCommands(Elements);
        }

        public void setCommands(List<Command> commands)
        {
            List<XmlElement> xmls = new List<XmlElement>();
            foreach(Command c in commands)
            {
                xmls.Add(XmlTools.SerializeToXmlElement(c));
            }
            this.Elements = xmls.ToArray();
        }
    }
}
