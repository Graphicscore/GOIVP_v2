using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands._text
{
    public class delete : Command
    {
        private String condition, text;

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

        public delete() : base()
        {

        }

        public override string getString()
        {
            return "text replace, text=" + text + ", condition=" + condition;
        }
    }
}
