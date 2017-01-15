using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands._text
{
    public class add : Command
    {

        private string text;

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

        public add() : base()
        {

        }

        public override string getString()
        {
            return "add text : " + text;
        }
    }
}
