using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Info
{
    [Serializable()]
    public class AuthorInfo : OIVField.Container
    {
        private String displayName, actionLink, web, facebook, twitter, youtube;

        public event PropertyChangedEventHandler AuthorNameChanged;
        public event PropertyChangedEventHandler AuthorActionLinkChanged;
        public event PropertyChangedEventHandler AuthorWebChanged;
        public event PropertyChangedEventHandler AuthorFacebookChanged;
        public event PropertyChangedEventHandler AuthorTwitterChanged;
        public event PropertyChangedEventHandler AuthorYoutubeChanged;

        [OIVField(true, "Author Display Name")]
        [System.Xml.Serialization.XmlElement("displayName")]
        public String DisplayName
        {
            get
            {
                return displayName;
            }

            set
            {
                displayName = value;
                PropertyChangedEventHandler handler = AuthorNameChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(displayName));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("actionLink")]
        public String ActionLink
        {
            get
            {
                return actionLink;
            }

            set
            {
                actionLink = value;
                PropertyChangedEventHandler handler = AuthorActionLinkChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(actionLink));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("web")]
        public string Web
        {
            get
            {
                return web;
            }

            set
            {
                web = value;
                PropertyChangedEventHandler handler = AuthorWebChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(web));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("facebook")]
        public string Facebook
        {
            get
            {
                return facebook;
            }

            set
            {
                facebook = value;
                PropertyChangedEventHandler handler = AuthorFacebookChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(facebook));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("twitter")]
        public string Twitter
        {
            get
            {
                return twitter;
            }

            set
            {
                twitter = value;
                PropertyChangedEventHandler handler = AuthorTwitterChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(twitter));
                }
            }
        }

        [OIVField(false)]
        [System.Xml.Serialization.XmlElement("youtube")]
        public string Youtube
        {
            get
            {
                return youtube;
            }

            set
            {
                youtube = value;
                PropertyChangedEventHandler handler = AuthorYoutubeChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(youtube));
                }
            }
        }

        public void update(AuthorInfo data)
        {
            this.DisplayName = data.DisplayName;
            this.ActionLink = data.ActionLink;
            this.Web = data.Web;
            this.Facebook = data.Facebook;
            this.Twitter = data.Twitter;
            this.Youtube = data.Youtube;
        }

        public void clear()
        {
            this.ActionLink = "";
            this.DisplayName = "";
            this.Facebook = "";
            this.Twitter = "";
            this.Web = "";
            this.Youtube = "";
        }
    }
}
