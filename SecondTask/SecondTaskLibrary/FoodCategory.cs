using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class FoodCategory:CategoryOfSupply
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "Food";
        }

        public FoodCategory() : base()
        {

        }

        /// <summary>
        /// Writes all information in xml file
        /// </summary>
        /// <param name="xml"></param>
        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("category");
            xml.WriteAttributeString("type", $"{ToString()}");
            foreach (Supply supply in supplies)
            {
                supply.WriteXml(xml);
            }
            xml.WriteEndElement();
        }

    }
}
