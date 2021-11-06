using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
    class SupplyFactory:ISupplyFactory
    {
        public Supply create(string type,List<CategoryOfSupply> categories, double weight , int count , int tempretureMin, int tempretureMax)
        {
            switch (type)
            {
                case "Fish":
                    ThermalCategory thermalForFish = new ThermalCategory();
                    foreach(CategoryOfSupply category in categories)
                    {
                        if (category.GetType() == typeof(ThermalCategory))
                            thermalForFish = (ThermalCategory)category;
                    }
                    return new Fish(thermalForFish, (tempretureMin, tempretureMax), weight, count);
                    break;
                case "Milk":
                    ThermalCategory thermalForMilk = new ThermalCategory();
                    foreach (CategoryOfSupply category in categories)
                    {
                        if (category.GetType() == typeof(ThermalCategory))
                            thermalForMilk = (ThermalCategory)category;
                    }
                    return new Milk(thermalForMilk, (tempretureMin, tempretureMax), weight, count);
                    break;
                case "Antifreeze":
                    ChemicalCategory chemical = new ChemicalCategory();
                    foreach (CategoryOfSupply category in categories)
                    {
                        if (category.GetType() == typeof(ChemicalCategory))
                            chemical = (ChemicalCategory)category;
                    }
                    return new Antifreeze(chemical, weight, count);
                    break;
                case "Detergents":
                    ChemicalCategory chemicalForDet = new ChemicalCategory();
                    foreach (CategoryOfSupply category in categories)
                    {
                        if (category.GetType() == typeof(ChemicalCategory))
                            chemicalForDet = (ChemicalCategory)category;
                    }
                    return new Detergents(chemicalForDet, weight, count);
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}
