using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GOIVPL.XPath
{
    class XPathFile
    {
        private XmlDocument xml;

        private XPathFile(XmlDocument document) {
            this.xml = document;
        }

        public static XPathFile XPathFileByXML(FileInfo xmlFile)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xmlFile.FullName);
            XPathFile xpath = new XPathFile(document);
            return xpath;
        }

        public static XPathFile XPathFileByXML(String xmlText)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlText);
            XPathFile xpath = new XPathFile(document);
            return xpath;
        }
    }
}
