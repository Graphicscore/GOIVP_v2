using GOIVPL.Commands.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real
{
    [System.Xml.Serialization.XmlRoot("add")]
    public class AddCommand : BaseCommand
    {

        public AddCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlText()]
        public string Name
        {
            get; set;
        }

        [System.Xml.Serialization.XmlAttribute("source")]
        public string Source
        {
            get; set;
        }

        public override string getName()
        {
            return "Add Command, Path " + Name + "->" + Source;
        }

        public Boolean isXML()
        {
            return Name.EndsWith(".xml") || Name.EndsWith(".meta");
        }
    }
}
