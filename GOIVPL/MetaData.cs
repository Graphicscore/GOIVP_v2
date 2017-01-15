using GOIVPL.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL
{
    [Serializable()]
    public class MetaData : OIVField.Container
    {

        private VersionInfo versionInfo;
        private Description description;
        private LargeDescription largeDescription = new LargeDescription();
        private Licence license = new Licence();
        private AuthorInfo authorInfo;
        private String name;

        public event PropertyChangedEventHandler NameChanged;

        public MetaData()
        {
            Version = new VersionInfo();
            Description = new Description();
            LargeDescription = new LargeDescription();
            this.License = new Licence();
            Author = new AuthorInfo();
        }

        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("version")]
        public VersionInfo Version
        {
            get
            {
                return versionInfo;
            }

            set
            {
                versionInfo = value;
            }
        }


        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("description")]
        public Description Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }


        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("largeDescription")]
        public LargeDescription LargeDescription
        {
            get
            {
                return largeDescription;
            }

            set
            {
                largeDescription = value;
            }
        }


        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("licence")]
        public Licence License
        {
            get
            {
                return license;
            }

            set
            {
                license = value;
            }
        }


        [OIVField(true)]
        [System.Xml.Serialization.XmlElement("author")]
        public AuthorInfo Author
        {
            get
            {
                return authorInfo;
            }

            set
            {
                authorInfo = value;
            }
        }


        [OIVField(true, "Package Name")]
        [System.Xml.Serialization.XmlElement("name")]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                PropertyChangedEventHandler handler = NameChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }

        public void update(MetaData data)
        {
            this.Author.update(data.Author);
            this.Description.update(data.Description);
            this.LargeDescription.update(data.LargeDescription);
            this.License.update(data.License);
            this.Name = data.Name;
            this.Version.update(data.Version);
        }

        public void clear()
        {
            this.Author.clear();
            this.Description.clear();
            this.LargeDescription.Clear();
            this.License.clear();
            this.Version.clear();
            this.Name = "";

        }
    }
}
