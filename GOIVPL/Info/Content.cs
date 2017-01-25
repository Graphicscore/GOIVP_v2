using GOIVPL.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using GOIVPL.Commands.generic;
using GOIVPL.Commands.real;

namespace GOIVPL.Info
{
    [Serializable()]
    public class Content
    {

        public Content()
        {
        }

        [System.Xml.Serialization.XmlElement(typeof(AddCommand), ElementName = "add")]
        [System.Xml.Serialization.XmlElement(typeof(ArchiveCommand), ElementName = "archive")]
        [System.Xml.Serialization.XmlElement(typeof(DefragmentationCommand), ElementName = "defragmentation")]
        [System.Xml.Serialization.XmlElement(typeof(DeleteCommand), ElementName = "delete")]
        [System.Xml.Serialization.XmlElement(typeof(XmlCommand), ElementName = "xml")]
        //TODO TxTCommand
        public List<BaseCommand> ICommands
        {
            get;set;
        }
    }
}
