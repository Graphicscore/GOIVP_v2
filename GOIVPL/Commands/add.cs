using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class add : Command
    {

        private string source;
        private string name;

        [System.Xml.Serialization.XmlAttribute("source")]
        public string Source
        {
            get
            {
                return source;
            }

            set
            {
                source = value;
            }
        }

        [System.Xml.Serialization.XmlText()]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public add() : base()
        {

        }

        public override string getString()
        {
            return "add, source=" + source + " -> " + name;
        }
    }
}
