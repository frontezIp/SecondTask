using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class Milk: Supply
    {
        /// <summary>
        /// Returns type of supply
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Milk";
        }

        public string type = "Thermal";

        /// <summary>
        /// Contains required tempreture
        /// </summary>
        (int, int) tempreture;

        public Milk(ThermalCategory category, (int, int) tempreture,double weight, int amount) : base(category,weight,amount)
        {
            this.tempreture = tempreture;
            this.category = category;
            category.tempretureLimits.Add(this, tempreture);
            category.supplies.Add(this);

        }

        /// <summary>
        /// Writes all information in xml file
        /// </summary>
        /// <param name="xml"></param>
        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("Supply");
            xml.WriteAttributeString("type", $"{ToString()}");           
            xml.WriteAttributeString("weight", $"{Weight}");
            xml.WriteAttributeString("count", $"{Count}");
            xml.WriteAttributeString("tempretureMin", $"{tempreture.Item1}");
            xml.WriteAttributeString("tempetureMax", $"{tempreture.Item2}");

        }

    }
}
