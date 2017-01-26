//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GOIVPL.Commands._xml
//{
//    [System.Xml.Serialization.XmlRoot("add")]
//    public class xmladd : Command
//    {
//        private String xpath;

//        private String append;

//        public const String APPEND_FIRST = "First", APPEND_LAST = "Last";

//        [System.Xml.Serialization.XmlAttribute("xpath")]
//        public string XPath
//        {
//            get
//            {
//                return xpath;
//            }

//            set
//            {
//                xpath = value;
//            }
//        }

//        [System.Xml.Serialization.XmlAttribute("append")]
//        public string Append
//        {
//            get
//            {
//                return append;
//            }

//            set
//            {
//                append = value;
//            }
//        }

//        public xmladd() : base()
//        {

//        }

//        public override string getString()
//        {
//            return "xml add, xpath=" + xpath;
//        }
//    }
//}
