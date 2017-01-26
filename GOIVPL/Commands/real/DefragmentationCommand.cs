using GOIVPL.Commands.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real
{
    [System.Xml.Serialization.XmlRoot("defragmentation")]
    public class DefragmentationCommand : BaseCommand
    {
        public DefragmentationCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlAttribute("archive")]
        public string Archive
        {
            get; set;
        }

        public override string getName()
        {
            return "Defragmentation Command";
        }
    }
}
