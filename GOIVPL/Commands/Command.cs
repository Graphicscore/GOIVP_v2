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

        public enum UseType { Generic, XML, TXT }

        protected UseType useType = UseType.Generic;

        [System.Xml.Serialization.XmlIgnore]
        private List<Command> subCommands = new List<Command>();

        [System.Xml.Serialization.XmlElement(typeof(_text.insert))]
        [System.Xml.Serialization.XmlElement(typeof(_text.replace))]
        [System.Xml.Serialization.XmlElement(typeof(_xml.replace))]
        [System.Xml.Serialization.XmlElement(typeof(_xml.remove))]
        [System.Xml.Serialization.XmlElement(typeof(add))]
        [System.Xml.Serialization.XmlElement(typeof(archive))]
        [System.Xml.Serialization.XmlElement(typeof(Command))]
        [System.Xml.Serialization.XmlElement(typeof(defragmentation))]
        [System.Xml.Serialization.XmlElement(typeof(delete))]
        [System.Xml.Serialization.XmlElement(typeof(text))]
        [System.Xml.Serialization.XmlElement(typeof(xml))]
        public List<Command> ICommands
        {
            get
            {
                return subCommands;
            }
        }


        public virtual String getString()
        {
            return "Command";
        }

        public Command()
        {

        }

        public Command(UseType type)
        {
            useType = type;
        }

        public String CommandName
        {
            get
            {
                return getString();
            }
        }

        protected bool isXMLCommandType()
        {
            return useType == UseType.XML;
        }
        protected bool isGenericCommandType()
        {
            return useType == UseType.Generic;
        }
        protected bool isTXTCommandType()
        {
            return useType == UseType.TXT;
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
            /*OIVPManager mgr = new OIVPManager();
            List<Command> cmds = new List<Command>();

            foreach (XmlElement element in Elements)
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

            return cmds.ToArray<Command>();*/
            return ICommands.ToArray();
        }

        /*internal void addSubCommand(Command[] command)
        {
            foreach(Command cmd in command)
            {
                subCommands.AddRange(command);
            }
        }

        public void addSubCommand(List<Command> command)
        {
            foreach (Command c in command)
            {
                addSubCommand(c);
            }
        }

        public void addSubCommand(Command command)
        {
                subCommands.Add(command);
        }

        public void removeSubCommandAt(int index)
        {
            subCommands.RemoveAt(index);
        }

        public void insertSubCommandAt(int index, Command cmd)
        {
            subCommands.Insert(index,cmd);
        }*/
    }
}
