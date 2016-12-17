using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GOIVPL.Info
{
    [Serializable()]
    public class Description : OIVField.Container
    {
        [System.Xml.Serialization.XmlIgnore]
        private String footerLink, footerLinkTitle, text;
        public event PropertyChangedEventHandler DescriptionFooterLinkChanged;
        public event PropertyChangedEventHandler DescriptionFooterTextChanged;
        public event PropertyChangedEventHandler DescriptionTextChanged;

        [OIVField(false)]
        [System.Xml.Serialization.XmlAttribute("footerLink")]
        public string FooterLink
        {
            get
            {
                return footerLink;
            }

            set
            {
                footerLink = value;
                PropertyChangedEventHandler handler = DescriptionFooterLinkChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(footerLink));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlAttribute("footerLinkTitle")]
        public string FooterLinkTitle
        {
            get
            {
                return footerLinkTitle;
            }

            set
            {
                footerLinkTitle = value;
                PropertyChangedEventHandler handler = DescriptionFooterTextChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(footerLinkTitle));
                }
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlText()]
        public XmlNode[] Text
        {
            get
            {
                var dummy = new XmlDocument();
                return new XmlNode[] { dummy.CreateCDataSection(text) };
            }

            set
            {
                text = value[0].Value;
            }
        }

        public void setText(String text)
        {
            this.text = text;
            PropertyChangedEventHandler handler = DescriptionTextChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(text));
            }
        }

        public void update(Description data)
        {
            this.FooterLink = data.FooterLink;
            this.FooterLinkTitle = data.FooterLinkTitle;
            this.Text = data.Text;
            setText(text);
        }

        public void clear()
        {
            this.FooterLink = "";
            this.FooterLinkTitle = "";
            this.setText("");
        }
    }
}
