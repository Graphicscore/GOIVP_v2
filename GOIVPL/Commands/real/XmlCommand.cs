using GOIVPL.Commands.generic;
using GOIVPL.Commands.real.subcommands.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace GOIVPL.Commands.real
{
    [System.Xml.Serialization.XmlRoot("xml")]
    public class XmlCommand : BaseCommand, System.Xml.Serialization.IXmlSerializable
    {
        public XmlCommand() : base()
        {
            XmlSubCommands = new List<XmlSubCommand>();
        }

        [System.Xml.Serialization.XmlIgnore]
        public String LocalXmlFilePath;

        [System.Xml.Serialization.XmlIgnore]
        public List<XmlSubCommand> XmlSubCommands { get; set; }

        [System.Xml.Serialization.XmlAttribute("path")]
        public string Path
        {
            get; set;
        }

        public override string getName()
        {
            return "XML, Path : " + Path;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Path = reader.GetAttribute("path");
            while(reader.NodeType == XmlNodeType.Element)
            {
                reader.ReadStartElement();
                switch (reader.Name)
                {
                    case "add":
                        XmlSubCommands.Add((XmlAddCommand) new System.Xml.Serialization.XmlSerializer(typeof(XmlAddCommand)).Deserialize(reader));
                        break;
                    default:
                        Console.WriteLine("Unknown Doc Name " + reader.Name);
                     break;
                }
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("path", Path);

            foreach(XmlSubCommand xmlCommand in XmlSubCommands)
            {
               new System.Xml.Serialization.XmlSerializer(xmlCommand.GetType()).Serialize(writer, xmlCommand);
            }        
        }
    }
}
