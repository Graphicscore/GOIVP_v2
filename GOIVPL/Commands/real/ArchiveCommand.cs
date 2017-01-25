using GOIVPL.Commands.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real
{
    [System.Xml.Serialization.XmlRoot(ElementName = "archive")]
    public class ArchiveCommand : BaseCommand
    {
        public ArchiveCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlAttribute("path")]
        public string Path
        {
            get; set;
        }

        [System.Xml.Serialization.XmlAttribute("createIfNotExist")]
        public string CreateIfNotExists
        {
            get; set;
        }

        [System.Xml.Serialization.XmlAttribute("type")]
        public string ArchiveType
        {
            get; set;
        }

        public override string getName()
        {
            return "Archive, Path : " + Path;
        }
    }
}
