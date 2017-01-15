using GOIVPL.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL
{
    [System.Xml.Serialization.XmlRoot("package")]
    public class OIVFile : OIVField.Container
    {
        public class TargetTypes { public static String FIVE = "FIVE"; }

        public event PropertyChangedEventHandler ContentChanged;
        public event PropertyChangedEventHandler PictureChanged;

        [System.Xml.Serialization.XmlIgnore]
        private MetaData metadata;
        [System.Xml.Serialization.XmlIgnore]
        private Color color;
        [System.Xml.Serialization.XmlIgnore]
        private Content content;
        [System.Xml.Serialization.XmlIgnore]
        private string id;
        [System.Xml.Serialization.XmlIgnore]
        private string target;
        [System.Xml.Serialization.XmlIgnore]
        private string version;
        [System.Xml.Serialization.XmlIgnore]
        IList<Commands.Command> icommands;

        [System.Xml.Serialization.XmlIgnore]
        private System.Drawing.Bitmap picture;
        [System.Xml.Serialization.XmlIgnore]
        private string contentPath;

        public OIVFile()
        {
            MetaData = new MetaData();
            Color = new Color();
            Content = new Content();
            version = "2.1";
            Target = TargetTypes.FIVE;
            id = "{" + Guid.NewGuid().ToString() + "}";
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("metadata")]
        public MetaData MetaData
        {
            get
            {
                return metadata;
            }

            set
            {
                metadata = value;
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("colors")]
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("content")]
        public Content Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
                PropertyChangedEventHandler handler = ContentChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(null));
                }
            }
        }

        public IList<Commands.Command> ICommands
        {
            get
            {
                return Content.ICommands;
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlAttribute("version")]
        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlAttribute("id")]
        public string ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlAttribute("target")]
        public string Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public System.Drawing.Bitmap Picture
        {
            get
            {
                return this.picture;
            }

            set
            {
                picture = value;
                PropertyChangedEventHandler handler = PictureChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(null));
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public String ContentPath
        {
            get
            {
                return this.contentPath;
            }

            set
            {
                this.contentPath = value;
            }
        }

        public void clear()
        {
            this.MetaData.clear();
        }
    }
}
