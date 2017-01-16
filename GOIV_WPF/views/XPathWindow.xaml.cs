using GOIVPL;
using GOIVPL.Commands;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        public struct Chunk
        {
            public int startpos;
            public int length;
            public Color BackColor;
        }

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

        private XmlNode generateReplaceXmlNode()
        {
            GOIVPL.Commands._xml.replace cadd = new GOIVPL.Commands._xml.replace();
            cadd.XPath = "";
            XmlElement element = XmlTools.SerializeToXmlElement(cadd);
            element.InnerXml = "<dummy>Test Content</dummy>";
            return element;
        }

        private XmlNode generateRemoveXmlNode()
        {
            GOIVPL.Commands._xml.remove cadd = new GOIVPL.Commands._xml.remove();
            cadd.XPath = "";
            XmlElement element = XmlTools.SerializeToXmlElement(cadd);
            return element;
        }


        private void textXmlButton_Click(object sender, RoutedEventArgs e)
        {
            codeDocument.LoadXml(codeBox.Text);
            XmlElement rootElement = codeDocument.DocumentElement;
            XmlDocument originalXml = xmlDocument.CloneNode(true) as XmlDocument;

            XmlNode curNode = null;

            try
            {
                foreach (XmlNode childNode in rootElement.ChildNodes)
                {
                    curNode = childNode;
                    switch (childNode.Name)
                    {
                        case "add":
                            runTest_Add(childNode);
                            break;
                        case "replace":
                            runTest_Replace(childNode);
                            break;
                        case "remove":
                            runTest_Remove(childNode);
                            break;
                    }

                }

                DiffView v = new DiffView(PrettyXml(originalXml.OuterXml), PrettyXml(xmlDocument.OuterXml));
                v.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync(TryFindResource("STRING_XPATH_ERROR_TITLE") as String, TryFindResource("STRING_XPATH_ERROR_MESSAGE") as String + Environment.NewLine + PrettyXml(curNode.OuterXml), MessageDialogStyle.Affirmative);
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
                throw new Exception(cadd.XPath);
            }
            else
            {
                node.AppendChild(xmlDocument.ImportNode(childNode.FirstChild, true));
            }
        }

        private void runTest_Replace(XmlNode childNode)
        {
            GOIVPL.Commands._xml.replace creplace = XmlTools.DeserializeFromXmlElement<GOIVPL.Commands._xml.replace>(childNode as XmlElement);
            XmlNode node = xmlDocument.SelectSingleNode(creplace.XPath);
            if (node == null)
            {
                throw new Exception(creplace.XPath);
            }
            else
            {
                node.ParentNode.ReplaceChild(xmlDocument.ImportNode(childNode.FirstChild, true), node);
            }
        }

        private void runTest_Remove(XmlNode childNode)
        {
            GOIVPL.Commands._xml.remove cremove = XmlTools.DeserializeFromXmlElement<GOIVPL.Commands._xml.remove>(childNode as XmlElement);
            XmlNode node = xmlDocument.SelectSingleNode(cremove.XPath);
            if (node == null)
            {
                throw new Exception(cremove.XPath);
            }
            else
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        private void codebox_context_add_Click(object sender, RoutedEventArgs e)
        {
            codeDocument.DocumentElement.AppendChild(codeDocument.ImportNode(generateAddXmlNode(), true));
            updateCodeText();

        }

        private void codebox_context_replace_Click(object sender, RoutedEventArgs e)
        {
            codeDocument.DocumentElement.AppendChild(codeDocument.ImportNode(generateReplaceXmlNode(), true));
            updateCodeText();

        }

        private void codebox_context_remove_Click(object sender, RoutedEventArgs e)
        {
            codeDocument.DocumentElement.AppendChild(codeDocument.ImportNode(generateRemoveXmlNode(), true));
            updateCodeText();
        }
    }

}
