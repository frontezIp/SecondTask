using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    class CategoryFactory:ICategoryFactory
    {
        /// <summary>
        /// Creates categories according to type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public CategoryOfSupply Create(string type)
        {
            switch (type)
            {
                case "Thermal":
                    return new ThermalCategory();
                    break;
                case "Chemical":
                    return new ChemicalCategory();
                    break;
                case "Food":
                    return new FoodCategory();
                    break;
                default:
                    throw new Exception("No such types of category");
            }
        }
    }
}
