using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class Fish:Supply
    {
        public override string ToString()
        {
            return "Fish";
        }

        /// <summary>
        /// Contain required tempreture
        /// </summary>
        (int, int) tempreture;

        public Fish(ThermalCategory category,(int,int) tempreture,double weight,int amount) : base(category,weight,amount) 
        {
            this.tempreture = tempreture;
            category.tempretureLimits.Add(this,tempreture);
            this.category = category;
            category.supplies.Add(this);

        }

        /// <summary>
        /// Writes all infomation in xml file
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
