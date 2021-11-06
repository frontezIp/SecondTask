using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class ModelOfTractor
    {
        private double horsePower;
        private double weightOfTractor;
        private string name;
        public double HorsePower { get => horsePower; set => horsePower = value; }
        public double WeightOfTractor { get => weightOfTractor; set => weightOfTractor = value; }
        public string Name { get => name; set => name = value; }

        public ModelOfTractor(double power, double weight, string name)
        {
            HorsePower = power;
            weightOfTractor = weight;
            Name = name;
        }

        /// <summary>
        /// Writes all information in xml file
        /// </summary>
        /// <param name="xml"></param>
        public void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("Model");
            xml.WriteAttributeString("HorsePower", $"{HorsePower}");
            xml.WriteAttributeString("WeightOfTractor", $"{WeightOfTractor}");
            xml.WriteAttributeString("Name", $"{name}");
            xml.WriteEndElement();
        }

    }
}

