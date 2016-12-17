using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Info
{
    [Serializable()]
    public class Color : OIVField.Container
    {
        private IconBackground iconBackground;
        private HeaderBackground headerBackground;

        public event PropertyChangedEventHandler IconBackgroundChanged, HeaderBackgroundChagned;

        public Color()
        {
            IconBackground = new IconBackground();
            HeaderBackground = new HeaderBackground();
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("headerBackground")]
        public HeaderBackground HeaderBackground
        {
            get
            {
                return headerBackground;
            }

            set
            {
                headerBackground = value;
                PropertyChangedEventHandler handler = HeaderBackgroundChagned;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(null));
                }
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("iconBackground")]
        public IconBackground IconBackground
        {
            get
            {
                return iconBackground;
            }

            set
            {
                iconBackground = value;
                PropertyChangedEventHandler handler = IconBackgroundChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(null));
                }
            }
        }

        public void update(Color data)
        {
            this.IconBackground = data.IconBackground;
            this.HeaderBackground = data.HeaderBackground;
        }
    }

    [Serializable()]
    public class HeaderBackground : OIVField.Container
    {

        private String useBlackTextColor;
        private String text;

        public HeaderBackground()
        {
            setColor(System.Drawing.Color.FromArgb(255, 59, 89, 152));
            useBlackTextColor = "False";
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlAttribute("useBlackTextColor")]
        public String UseBlackTextColor
        {
            get
            {
                return useBlackTextColor;
            }

            set
            {
                useBlackTextColor = value;
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlText()]
        public String Value
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

        public void setColor(System.Drawing.Color color)
        {
            this.text = System.Drawing.ColorTranslator.ToHtml(color).Replace("#", "$FF");
        }

        public System.Drawing.Color getColor()
        {
            if (Value != null && Value.Length > 0 && Value.Contains("$"))
            {
                return System.Drawing.ColorTranslator.FromHtml(Value.Replace("$", "#"));
            }
            else
            {
                return System.Drawing.Color.FromArgb(255, 59, 89, 152);
            }
        }
    }

    [Serializable()]
    public class IconBackground : OIVField.Container
    {

        private String text;

        public IconBackground()
        {
            setColor(System.Drawing.Color.FromArgb(255, 59, 89, 152));
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlText()]
        public String Value
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

        public void setColor(System.Drawing.Color color)
        {
            this.text = System.Drawing.ColorTranslator.ToHtml(color).Replace("#", "$FF");
        }

        public System.Drawing.Color getColor()
        {
            if (Value != null && Value.Length > 0 && Value.Contains("$"))
            {
                return System.Drawing.ColorTranslator.FromHtml(Value.Replace("$", "#"));
            }
            else
            {
                return System.Drawing.Color.FromArgb(255, 59, 89, 152);
            }
        }
    }
}
