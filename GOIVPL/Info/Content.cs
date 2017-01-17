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
