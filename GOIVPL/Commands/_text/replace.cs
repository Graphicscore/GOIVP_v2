using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands._text
{
    public class replace : Command
    {
        private String line, condition,text;

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

        public replace() : base()
        {

        }

        public override string getString()
        {
            return "text replace, line=" + line + ", condition=" + condition + ",text=" + text;
        }
    }
}
