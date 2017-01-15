using GOIVPL.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;

namespace GOIVPL.Info
{
    [Serializable()]
    public class Content
    {
        [System.Xml.Serialization.XmlIgnore]
        private IList<Command> commands;

        public Content()
        {
            commands = new List<Command>();
        }

        [System.Xml.Serialization.XmlIgnore]
        public IList<Command> ICommands
        {
            get
            {
                return commands;
            }
            set
            {
                commands = value;
            }
        }

        [System.Xml.Serialization.XmlAnyElement]
        public XmlElement[] Elements
        {
            get
            {
                List<XmlElement> xmls = new List<XmlElement>();
                foreach (Command c in commands)
                {
                    xmls.Add(XmlTools.SerializeToXmlElement(c));
                }
                return xmls.ToArray();
            }

            set
            {
                ICommands.Clear();
                Command[] cmds = OIVPManager.commandFromXml(value);
                foreach(Command c in cmds)
                {
                    ICommands.Add(c);
                }
            }
        }
        /*public void setCommands(List<Command> commands)
        {
            List<XmlElement> xmls = new List<XmlElement>();
            foreach(Command c in commands)
            {
                xmls.Add(XmlTools.SerializeToXmlElement(c));
            }
            this.Elements = xmls.ToArray();
        }*/
    }
}
