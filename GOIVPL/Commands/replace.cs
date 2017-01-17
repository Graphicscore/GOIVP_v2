using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands._text
{
    [System.Xml.Serialization.XmlType("replaceText")]
    public class replace : Command
    {
        //TXT
        private String line;
        private String condition;
        private String text;

        //XML
        private String xpath;

        public override string getString()
        {
            switch (useType)
            {
                case UseType.XML:
                    return "xml replace, xpath=" + xpath;
                case UseType.TXT:
                    return "text replace, line=" + line + ", condition=" + condition + ",text=" + text;
                default:
                case UseType.Generic:
                    return "this should never happen replace";
            }
            
        }

       

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

        [System.Xml.Serialization.XmlText()]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        [System.Xml.Serialization.XmlAttribute("line")]
        public string Line
        {
            get
            {
                return line;
            }

            set
            {
                line = value;
            }
        }

        [System.Xml.Serialization.XmlAttribute("condition")]
        public string Condition
        {
            get
            {
                return condition;
            }

            set
            {
                condition = value;
            }
        }

        public bool ShouldSerializeXPath()
        {
            return isXMLCommandType();
        }

        public bool ShouldSerializeText()
        {
            return isTXTCommandType();
        }

        public bool ShouldSerializeLine()
        {
            return isTXTCommandType();
        }

        public bool ShouldSerializeCondition()
        {
            return isTXTCommandType();
        }
    }
}
