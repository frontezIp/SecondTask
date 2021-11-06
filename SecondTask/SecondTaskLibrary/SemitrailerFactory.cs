using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTaskLibrary
{
     public class SemitrailerFactory:ISemitrailerFactory
    {
        public AutoStation.AutoPark.Semitrailer Create(string type,List<CategoryOfSupply> categories ,double liftingCapacity, int volumeOfSemitrailer,int tempMin =0 , int tempMax =0)
        {
            switch (type)
            {
                case "Refregerator":
                    ThermalCategory thermalForRefregerator = new ThermalCategory();
                    foreach (CategoryOfSupply category in categories)
                    {
                        if (category.GetType() == typeof(ThermalCategory))
                            thermalForRefregerator = (ThermalCategory)category;
                    }
                    return new Refrigerator(liftingCapacity, volumeOfSemitrailer, thermalForRefregerator,tempMin,tempMax);
                    break;
                case "Awning":
                   ChemicalCategory chemical = new ChemicalCategory();
                    foreach (CategoryOfSupply category in categories)
                    {
                        if (category.GetType() == typeof(ChemicalCategory))
                            chemical = (ChemicalCategory)category;
                    }
                    FoodCategory food = new FoodCategory();
                    foreach (FoodCategory category in categories)
                    {
                        if (category.GetType() == typeof(FoodCategory))
                            food = (FoodCategory)category;
                    }
                    return new Awning(liftingCapacity, volumeOfSemitrailer, food,chemical);
                default:
                    throw new Exception();
            }
        }
    }
}
