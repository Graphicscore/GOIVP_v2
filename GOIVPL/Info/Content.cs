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
        private List<Command> commands;

        public Content()
        {
            commands = new List<Command>();
        }

        [System.Xml.Serialization.XmlElement(typeof(Commands._text.add))]
        [System.Xml.Serialization.XmlElement(typeof(Commands._text.delete))]
        [System.Xml.Serialization.XmlElement(typeof(Commands._text.insert))]
        [System.Xml.Serialization.XmlElement(typeof(Commands._text.replace))]
        [System.Xml.Serialization.XmlElement(typeof(Commands._xml.add))]
        [System.Xml.Serialization.XmlElement(typeof(Commands._xml.replace))]
        [System.Xml.Serialization.XmlElement(typeof(Commands._xml.remove))]
        [System.Xml.Serialization.XmlElement(typeof(add))]
        [System.Xml.Serialization.XmlElement(typeof(archive))]
        [System.Xml.Serialization.XmlElement(typeof(Command))]
        [System.Xml.Serialization.XmlElement(typeof(defragmentation))]
        [System.Xml.Serialization.XmlElement(typeof(delete))]
        [System.Xml.Serialization.XmlElement(typeof(text))]
        [System.Xml.Serialization.XmlElement(typeof(xml))]
        public List<Command> ICommands
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
    }
}
