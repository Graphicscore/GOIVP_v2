using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Info
{
    public class LargeDescription : Description
    {
        private String displayName;

        public event PropertyChangedEventHandler LargeDescriptioDisplayNameChanged;

        [OIVField(false)]
        [System.Xml.Serialization.XmlAttribute("displayName")]
        public string DisplayName
        {
            get
            {
                return displayName;
            }

            set
            {
                displayName = value;
                PropertyChangedEventHandler handler = LargeDescriptioDisplayNameChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(displayName));
                }
            }
        }

        public void update(LargeDescription data)
        {
            base.update(data);
            DisplayName = data.DisplayName;
        }

        public void Clear()
        {
            base.clear();
            this.DisplayName = "";
        }
    }
}
