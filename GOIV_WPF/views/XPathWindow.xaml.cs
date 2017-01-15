using GOIVPL;
using GOIVPL.Commands;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GOIV_WPF.views
{
    /// <summary>
    /// Interaktionslogik für XPathWindow.xaml
    /// </summary>
    public partial class XPathWindow : MetroWindow
    {
        private xml command;
        private OIVFile oivFile;

        private XmlDocument xmlDocument;
        private XmlDocument codeDocument;

        public XPathWindow(ref OIVFile oivFile, xml command)
        {
            InitializeComponent();
            this.command = command;
            this.oivFile = oivFile;
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            xmlDocument = new XmlDocument();
            codeDocument = new XmlDocument();

            xmlDocument.Load(oivFile.ContentPath + "\\" + command.LocalXmlFilePath);
            xmlBox.Text = PrettyXml(xmlDocument.OuterXml);
            codeDocument.LoadXml(XmlTools.SerializeToXmlElement(command).OuterXml);
            codeBox.Text = PrettyXml(codeDocument.OuterXml);
        }
        private string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        private void xml_TextChanged(object sender, EventArgs e)
        {

        }

        private void code_TextChanged(object sender, EventArgs e)
        {

        }

        private void insertXmlButton_Click(object sender, RoutedEventArgs e)
        {
            switch (combbox.SelectedIndex)
            {
                case 0:
                    codeDocument.DocumentElement.AppendChild(codeDocument.ImportNode(generateAddXmlNode(), true));
                    break;
            }

            updateCodeText();
        }

        private void updateCodeText()
        {
            codeBox.Text = PrettyXml(codeDocument.OuterXml);
        }

        private void updateXmlText()
        {
            xmlBox.Text = PrettyXml(xmlDocument.OuterXml);
        }

        private XmlNode generateAddXmlNode()
        {
            GOIVPL.Commands._xml.add cadd = new GOIVPL.Commands._xml.add();
            cadd.XPath = "";
            XmlElement element = XmlTools.SerializeToXmlElement(cadd);
            element.InnerXml = "<dummy>Test Content</dummy>";
            return element;

        }

        private void textXmlButton_Click(object sender, RoutedEventArgs e)
        {
            codeDocument.LoadXml(codeBox.Text);
            XmlElement rootElement = codeDocument.DocumentElement;
            XmlDocument originalXml = xmlDocument.Clone() as XmlDocument;
            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "add":
                        runTest_Add(childNode);
                        break;
                }
            }
            updateXmlText();
            xmlDocument = originalXml;
        }

        private void runTest_Add(XmlNode childNode)
        {
            GOIVPL.Commands._xml.add cadd = XmlTools.DeserializeFromXmlElement<GOIVPL.Commands._xml.add>(childNode as XmlElement);
            XmlNode node = xmlDocument.SelectSingleNode(cadd.XPath);
            if (node == null)
            {
                MessageBox.Show("Sorry but that XPATH doesn't seem to be valid ...");
            }
            else
            {
                node.AppendChild(xmlDocument.ImportNode(childNode.FirstChild,true));
            }
        }
    }


}
