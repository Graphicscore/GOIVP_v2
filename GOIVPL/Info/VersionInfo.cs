using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Info
{
    [Serializable()]
    public class VersionInfo : OIVField.Container
    {
        private int major = -1, minor = -1;
        private String tag;

        public event PropertyChangedEventHandler VersionMajorChanged, VersionMinorChanged, VersionTagChanged;

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("major")]
        public int Major
        {
            get
            {
                return major;
            }

            set
            {
                major = value;
                PropertyChangedEventHandler handler = VersionMajorChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(major.ToString()));
                }
            }
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("minor")]
        public int Minor
        {
            get
            {
                return minor;
            }

            set
            {
                minor = value;
                PropertyChangedEventHandler handler = VersionMinorChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(minor.ToString()));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("tag")]
        public String Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
                PropertyChangedEventHandler handler = VersionTagChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(tag));
                }
            }
        }

        public void update(VersionInfo data)
        {
            this.Minor = data.Minor;
            this.Major = data.Major;
            this.Tag = data.Tag;
        }

        public void clear()
        {
            this.Minor = 1;
            this.Major = 1;
            this.Tag = "";
        }
    }
}
