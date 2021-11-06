using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public abstract class Supply
    {
        private double weight;

        public CategoryOfSupply category;

        public Supply(CategoryOfSupply category, double weight, int amount)
        {
            this.category = category;
            Weight = weight;
            Count = amount;
        }

        public double Weight { get => weight; set => weight = value; }
        public int Count { get => count; set => count = value; }
        public string Name { get => name; set => name = value; }

        private int count;

        private string name;
        /// <summary>
        /// Writes all information in the xml file
        /// </summary>
        /// <param name="xml"></param>
        public abstract void WriteXml(XmlWriter xml);
    }
}
