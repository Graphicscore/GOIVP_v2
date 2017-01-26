using GOIVPL.Commands.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.real
{

    [System.Xml.Serialization.XmlRoot("delete")]
    public class DeleteCommand : BaseCommand
    {
        public DeleteCommand() : base()
        {

        }

        [System.Xml.Serialization.XmlText()]
        public string Name
        {
            get; set;
        }

        public override string getName()
        {
            return "Delete Command";
        }
    }
}
