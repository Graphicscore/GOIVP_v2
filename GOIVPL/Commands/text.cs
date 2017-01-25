//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;

//namespace GOIVPL.Commands
//{
//    [Serializable()]
//    public class text : Command
//    {
//        private string path;
//        private string createIfNotExists;

//        [System.Xml.Serialization.XmlAttribute("path")]
//        public string Path
//        {
//            get
//            {
//                return path;
//            }

//            set
//            {
//                path = value;
//            }
//        }

//        [System.Xml.Serialization.XmlAttribute("createIfNotExist")]
//        public string CreateIfNotExists
//        {
//            get
//            {
//                return createIfNotExists;
//            }

//            set
//            {
//                createIfNotExists = value;
//            }
//        }

//        public text() : base()
//        {

//        }

//        public override Command[] getCommands()
//        {
//            //OIVPManager mgr = new OIVPManager();
//            //List<Command> cmds = new List<Command>();

//            /*foreach (XmlElement element in elements)
//            {
//                Assembly assem = typeof(GOIVPL.OIVPManager).Assembly;
//                Type type = assem.GetType(typeof(Command).Namespace + "._text." + element.Name);
//                Command cmd = XmlTools.DeserializeFromXmlElement<Command>(type, element);
//                if (cmd.Elements != null && cmd.Elements.Count > 0)
//                {
//                    cmd.addSubCommand(cmd.getCommands());
//                }
//                cmds.Add(cmd);
//            }*/

//            return ICommands.ToArray();
//        }

//        public override string getString()
//        {
//            return "text, path=" + path;
//        }
//    }
//}
