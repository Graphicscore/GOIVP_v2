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
        //Generic
        private string source;
        private string content;

        //Xml
        private String xpath;
        private String append;
        public const String APPEND_FIRST = "First", APPEND_LAST = "Last";

        public add() : base()
        {
            useType = UseType.Generic;
        }

        public override string getString()
        {
            switch (useType)
            {
                case UseType.TXT:
                    return "add text : " + content;
                case UseType.XML:
                    return "xml add, xpath=" + xpath;
                default:
                case UseType.Generic:
                    return "add, source=" + source + " -> " + content;
            }
            
        }

        public Boolean isXML()
        {
            return content.EndsWith(".xml") || content.EndsWith(".meta");
        }

        [System.Xml.Serialization.XmlText()]
        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

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

        //XML Related
        [System.Xml.Serialization.XmlAttribute("xpath")]
        public string XPath
        {
            get
            {
                return xpath;
            }

            set
            {
                xpath = value;
            }
        }

        [System.Xml.Serialization.XmlAttribute("append")]
        public string Append
        {
            get
            {
                return append;
            }

            set
            {
                append = value;
            }
        }

        public bool ShouldSerializeXPath()
        {
            return isXMLCommandType();
        }

        public bool ShouldSerializeAppend()
        {
            return isXMLCommandType();
        }

        public bool ShouldSerializeSource()
        {
            return isGenericCommandType();
        }
    }
}
