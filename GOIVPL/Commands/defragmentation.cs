using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class defragmentation : Command
    {
        private string archive;

        [System.Xml.Serialization.XmlAttribute("archive")]
        public string Archive
        {
            get
            {
                return archive;
            }

            set
            {
                archive = value;
            }
        }

        public defragmentation() : base()
        {

        }
    }
}
