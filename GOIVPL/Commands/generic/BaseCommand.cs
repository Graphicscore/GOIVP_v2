using GOIVPL.Commands.real;
using GOIVPL.Commands.real.subcommands.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.generic
{
    public abstract class BaseCommand : InterfaceCommand
    {
        [System.Xml.Serialization.XmlElement(typeof(AddCommand), ElementName = "add")]
        [System.Xml.Serialization.XmlElement(typeof(ArchiveCommand), ElementName = "archive")]
        [System.Xml.Serialization.XmlElement(typeof(DefragmentationCommand), ElementName = "defragmentation")]
        [System.Xml.Serialization.XmlElement(typeof(DeleteCommand), ElementName = "delete")]
        [System.Xml.Serialization.XmlElement(typeof(XmlCommand), ElementName = "xml")]
        public List<BaseCommand> SubCommands { get; set; }

        public BaseCommand()
        {
            SubCommands = new List<BaseCommand>();
        }

        [System.Xml.Serialization.XmlIgnore]
        public String CommandName
        {
            get
            {
                return getName();
            }
        }

        public abstract string getName();
    }
}
