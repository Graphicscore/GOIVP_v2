using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GOIVPL
{
    public class XmlTools
    {
        public static Object DeserializeFromXmlElement<Object>(Type type, XmlElement element)
        {
            try {
                var serializer = new XmlSerializer(type);

                return (Object)serializer.Deserialize(new XmlNodeReader(element));
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return default(Object);
            }
        }

        public static XmlElement SerializeToXmlElement(object o)
        {
            XmlDocument doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
            }

            return XElementToXML(RemoveAllNamespaces(XElement.Parse(doc.DocumentElement.OuterXml)));
        }

        public static T DeserializeFromXmlElement<T>(XmlElement element)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(new XmlNodeReader(element));
        }

        public static XElement RemoveAllNamespaces(XElement e)
        {
            return new XElement(e.Name.LocalName,
               (from n in e.Nodes()
                select ((n is XElement) ? RemoveAllNamespaces(n as XElement) : n)),
               (e.HasAttributes) ? (from a in e.Attributes()
                                    where (!a.IsNamespaceDeclaration)
                                    select new XAttribute(a.Name.LocalName, a.Value)) : null);
        }

        public static XmlElement XElementToXML(XElement el)
        {
            var doc = new XmlDocument();
            doc.Load(el.CreateReader());
            return doc.DocumentElement;
        }
    }
}
