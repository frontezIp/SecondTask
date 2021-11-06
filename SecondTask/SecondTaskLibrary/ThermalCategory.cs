using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class ThermalCategory : CategoryOfSupply
    {

        public Dictionary<Supply,(int,int)> tempretureLimits;

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
            return "Thermal";
        }
        public ThermalCategory() : base()
        {
            tempretureLimits = new Dictionary<Supply, (int, int)>();
        }

        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("category");
            xml.WriteAttributeString("type", $"{ToString()}");
            foreach(Supply supply in supplies)
            {
                supply.WriteXml(xml);
            }
            xml.WriteEndElement();
        }

    }
}
