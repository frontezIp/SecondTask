using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class TractorOne:AutoStation.AutoPark.Tractor
    {
        public TractorOne(ModelOfTractor model) : base(model)
        {

        }

        public override double CalculateFuelConsumption()
        {
            if (Child == null)
            {
                return Model.WeightOfTractor / Model.HorsePower;
            }
            else
            {
                return ((Model.WeightOfTractor + MaxSemitrailerCapacity+Child.CurrentWeight) / Model.HorsePower);
            }
        }

        public override void ConnectSemitrailer(AutoStation.AutoPark.Semitrailer semitrailer)
        {
            Child = semitrailer;   
        }

        public override string ToString()
        {
            return "TractorOne";
        }

        public override void UnconnectSemitrailer()
        {
            Child = null;
        }

        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("Tractor");
            xml.WriteAttributeString("type", "TractorOne");
            xml.WriteAttributeString("MaxTrailerCapacity", $"{MaxSemitrailerCapacity}");
            if (Child != null)
                xml.WriteAttributeString("Connected", $"{Child.Type}");
            else
                xml.WriteAttributeString("Connected", $"null");
            Model.WriteXml(xml);
            xml.WriteEndElement();
        }
    }
}
