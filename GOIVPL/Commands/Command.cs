using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class Command
    {
        protected List<XmlElement> elements;

        [System.Xml.Serialization.XmlAnyElement]
        public List<XmlElement> Elements
        {
            get
            {
                return elements;
            }

            set
            {
                elements = value;
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public List<Command> subCommands = new List<Command>();

        public IList<Command> ICommands
        {
            get
            {
                return subCommands;
            }
        }

        public String CommandName
        {
            get
            {
                return getString();
            }
        }

        public virtual String getString()
        {
            return "Command";
        }

        public Command()
        {
            elements = new List<XmlElement>();
        }

       /* public virtual Command[] getSubCommands()
        {
            List<Command> nodes = new List<Command>();

            foreach (XmlElement element in elements)
            {
                Assembly assem = Assembly.GetAssembly(typeof(GOIVPL.OIVFile));
                Type type;
                type = assem.GetType(GetType().Namespace + "." + element.Name);
                Command cmd = XmlTools.DeserializeFromXmlElement<Command>(type, element);
                cmd.getSubCommands();
                nodes.Add(cmd);
            }

            return nodes.ToArray<Command>();
        }*/

        public virtual Command[] getCommands(){
            OIVPManager mgr = new OIVPManager();
            List<Command> cmds = new List<Command>();

            foreach (XmlElement element in elements)
            {
                Assembly assem = typeof(GOIVPL.OIVPManager).Assembly;
                Type type = assem.GetType(typeof(Command).Namespace + "." + element.Name);
                Command cmd = XmlTools.DeserializeFromXmlElement<Command>(type, element);
                if (cmd != null && cmd.Elements != null && cmd.Elements.Count > 0)
                {
                    cmd.addSubCommand(cmd.getCommands());
                }
                if (cmd != null)
                {
                    cmds.Add(cmd);
                }
            }

            return cmds.ToArray<Command>();
        }

        internal void addSubCommand(Command[] command)
        {
            foreach(Command cmd in command)
            {
                elements.Add(XmlTools.SerializeToXmlElement(cmd));
                subCommands.AddRange(command);
            }
        }

        public void addSubCommand(Command command)
        {
            elements.Add(XmlTools.SerializeToXmlElement(command));
            subCommands.Add(command);
        }

        public void addSubCommand(List<Command> command)
        {
            foreach (Command c in command)
            {
                elements.Add(XmlTools.SerializeToXmlElement(c));
                subCommands.Add(c);
            }
        }
    }
}
