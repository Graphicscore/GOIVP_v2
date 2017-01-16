using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands._text
{
    [Serializable()]
    [System.Xml.Serialization.XmlType("insertText")]
    public class insert : Command
    {
        private String where, line, condition, text;

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

        [System.Xml.Serialization.XmlAttribute("where")]
        public string Where
        {
            get
            {
                return where;
            }

            set
            {
                where = value;
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

        public insert() : base()
        {

        }

        public override string getString()
        {
            return "text insert, where=" + where + ", line=" + line + ", condition=" + condition + ", text=" + text;
        }
    }
}
