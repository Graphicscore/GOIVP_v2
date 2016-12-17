using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class delete : Command
    {
        private string name;

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

        public delete() : base()
        {

        }
    }
}
