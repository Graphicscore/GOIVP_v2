using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands
{
    [Serializable()]
    public class delete : Command
    {
        //Generic
        private string content;

        //TXT
        private String condition;

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

        public delete() : base()
        {

        }

        public override string getString()
        {
            switch (useType)
            {
                case UseType.TXT:
                    return "text remove, text=" + content + ", condition=" + condition;
                default:
                    return "delete command, " + content;
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

        public bool ShouldSerializeCondition()
        {
            return isTXTCommandType();
        }
    }
}
