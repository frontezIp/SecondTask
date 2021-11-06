using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public static class  XmlRead
    {
        /// <summary>
        /// Reads file
        /// </summary>
        /// <param name="auto"></param>
       public static void Xml(AutoStation auto)
        {
            TractorFactory tractorFactory = new TractorFactory();
            CategoryFactory categoryFactory = new CategoryFactory();
            SupplyFactory supplyFactory = new SupplyFactory();
            SemitrailerFactory semitrailerFactory = new SemitrailerFactory();
            using var reader = XmlReader.Create("C:\\Users\\User\\source\\repos\\SecondTask\\SecondTaskLibrary\\xmll.xml");
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Tractor":
                            string typeOfTractor = reader.GetAttribute("type");
                            string typeOfConnector = reader.GetAttribute("Connected");
                            reader.ReadToFollowing("Model");
                            auto.tractors.Add(tractorFactory.create(typeOfTractor, new ModelOfTractor(Convert.ToDouble(reader.GetAttribute("HorsePower")), Convert.ToDouble(reader.GetAttribute("WeightOfTractor")), reader.GetAttribute("Name"))));
                            if (typeOfConnector != "null")
                                auto.tractors[auto.tractors.Count - 1].ConnectSemitrailer(auto.FindSemitrailer(typeOfConnector));
                            break;
                        case "category":
                            string typeOfCategory = reader.GetAttribute("type");
                            auto.categories.Add(categoryFactory.Create(typeOfCategory));
                            break;
                        case "Supply":
                            string typeOfSupply = reader.GetAttribute("type");
                            auto.supplies.Add(supplyFactory.create(typeOfSupply, auto.categories, Convert.ToDouble(reader.GetAttribute("weight")), Convert.ToInt32(reader.GetAttribute("count")), Convert.ToInt32(reader.GetAttribute("tempretureMin")), Convert.ToInt32(reader.GetAttribute("tempretureMax"))));
                            break;
                        case "Semitrailer":
                            string typeOfSemitrailer = reader.GetAttribute("type");
                            auto.semitrailers.Add(semitrailerFactory.Create(typeOfSemitrailer, auto.categories, Convert.ToDouble(reader.GetAttribute("LiftingCapacity")), Convert.ToInt32(reader.GetAttribute("VolumeOfSemitrailer")),Convert.ToInt32(reader.GetAttribute("tempretureMin")),Convert.ToInt32(reader.GetAttribute("tempretureMax"))));
                            break;
                        case "stock":
                            auto.semitrailers[auto.semitrailers.Count - 1].supplies.Add(auto.FindSupply(reader.GetAttribute("type")), 5);
                            break;

                    }
                }
            }
        }


    }
}
