using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class Detergents:Supply
    {
        public override string ToString()
        {
            return "Detergents";
        }

        public string type = "Chemical";

        public Detergents(ChemicalCategory category, double weight, int amount) : base(category, weight, amount)
        {
            this.category.supplies.Add(this);
        }
        /// <summary>
        /// Writes all its data in xml file
        /// </summary>
        /// <param name="xml"></param>
        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("Supply");
            xml.WriteAttributeString("type", $"{ToString()}");
            xml.WriteAttributeString("weight", $"{Weight}");
            xml.WriteAttributeString("count", $"{Count}");
        }
    }
}
