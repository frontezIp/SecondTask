using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public abstract class CategoryOfSupply
    {
        /// <summary>
        /// Contains all supplies of the category
        /// </summary>
        public List<Supply> supplies;

        public CategoryOfSupply()
        {
            supplies = new List<Supply>();
        }

        /// <summary>
        /// Writes all information about category
        /// </summary>
        /// <param name="xml"></param>
        public abstract void WriteXml(XmlWriter xml);

    }
}