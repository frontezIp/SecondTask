using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class Awning : AutoStation.AutoPark.Semitrailer
    {
        /// <summary>
        /// Indicate that which category of supply is occupy semitrailer
        /// </summary>
        CategoryOfSupply currentCategory;

        ChemicalCategory chemical;

        FoodCategory food;



        public Awning(double liftingCapacity,
            int volumeOfSemitrailer, FoodCategory food = null,
            ChemicalCategory chemical = null
            ) : base(liftingCapacity, volumeOfSemitrailer)
        {
            if (food != null)
                this.food = food;
            else if (chemical != null)
                this.chemical = chemical;
        }


        /// <summary>
        /// Validates full upload
        /// </summary>
        /// <returns></returns>
        public override bool CheckForFullUpload()
        {
            if (supplies.Count < currentCategory.supplies.Count)
                return true;
            else return false;
        }

        /// <summary>
        /// Validates upload
        /// </summary>
        /// <param name="supply"></param>
        /// <returns></returns>
        public bool CheckForUpload(Supply supply)
        {
            if (supply.Weight * supply.Count + CurrentWeight <= LiftingCapacity &&
                supply.Count + CurrentVolume <= VolumeOfSemitrailer
                && supplies.ContainsKey(supply) == false
                )
            {
                if (supplies.Count != 0)
                {
                    if (supplies.ContainsKey(supply) == true)
                        return false;
                    else return true;
                }
                else
                {
                    return true;
                }

            }
            else return false;
        }
        /// <summary>
        /// validates add load
        /// </summary>
        /// <param name="supply"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CheckForAddLoad(Supply supply, int amount)
        {
            if (supply.Weight * amount + CurrentWeight <= LiftingCapacity &&
                            amount + CurrentVolume <= VolumeOfSemitrailer &&
                            amount <= supply.Count
                            )
            {
                if (currentCategory != null)
                {
                    if (supply.category == currentCategory)
                        return true;
                    else return false;
                }
                else return true;
            }
            else return false;
        }

        /// <summary>
        /// upload supply
        /// </summary>
        /// <param name="category"></param>
        /// <param name="index"></param>
        public override void Upload(CategoryOfSupply category, int index)
        {
            if (chemical == category || food == category)
            {
                if (CheckForUpload(category.supplies[index]))
                {
                    if (supplies.Count == 0)
                        currentCategory = category;
                    supplies.Add(category.supplies[index], category.supplies[index].Count);
                    CurrentVolume += category.supplies[index].Count;
                    CurrentWeight += category.supplies[index].Count * category.supplies[index].Weight;
                    category.supplies[index].Count = 0;
                }
            }
        }

        /// <summary>
        /// Add load supply
        /// </summary>
        /// <param name="category"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        public override void AddLoad(CategoryOfSupply category, int index, int amount)
        {
            if (chemical == category || food == category)
            {
                if (CheckForAddLoad(category.supplies[index], amount))
                {
                    if (supplies.Count == 0)
                        currentCategory = category.supplies[index].category;
                    if (supplies.ContainsKey(category.supplies[index]) == false)
                        supplies.Add(category.supplies[index], amount);
                    else
                        supplies[category.supplies[index]] += amount;
                    CurrentVolume += category.supplies[index].Count;
                    CurrentWeight += category.supplies[index].Count * category.supplies[index].Weight;
                    category.supplies[index].Count -= amount;
                }
            }
        }
        /// <summary>
        /// Unload supply
        /// </summary>
        /// <param name="category"></param>
        /// <param name="index"></param>
        public override void Unload(CategoryOfSupply category, int index)
        {
            if (chemical == category || food == category)
            {
                if (supplies.ContainsKey(category.supplies[index]))
                {
                    category.supplies[index].Count += this.supplies[category.supplies[index]];
                    CurrentVolume -= supplies[category.supplies[index]];
                    CurrentWeight -= supplies[category.supplies[index]] * category.supplies[index].Weight;
                    this.supplies.Remove(category.supplies[index]);
                }
            }
        }

        /// <summary>
        /// Partial unload supply
        /// </summary>
        /// <param name="category"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        public override void PartialUnload(CategoryOfSupply category, int index, int amount)
        {
            if (chemical == category || food == category)
            {
                if (supplies.ContainsKey(category.supplies[index]))
                {
                    if (supplies[category.supplies[index]] < amount)
                    {
                        category.supplies[index].Count += supplies[category.supplies[index]];
                        CurrentVolume -= supplies[category.supplies[index]];
                        CurrentWeight -= supplies[category.supplies[index]] * category.supplies[index].Weight;
                        this.supplies.Remove(category.supplies[index]);
                    }
                    else
                    {
                        category.supplies[index].Count += amount;
                        CurrentVolume -= supplies[category.supplies[index]];
                        CurrentWeight -= amount * category.supplies[index].Weight;
                        supplies[category.supplies[index]] -= amount;
                    }


                }
            }
        }
        /// <summary>
        /// Connect chosen tractor
        /// </summary>
        /// <param name="tractor"></param>
        public override void ConnectTractor(AutoStation.AutoPark.Tractor tractor)
        {
            if (Parent == null)
            {
                Parent = tractor;
                Parent.MaxSemitrailerCapacity = this.LiftingCapacity;
                tractor.ConnectSemitrailer(this);
            }
        }

        /// <summary>
        /// Disconnect current tractor
        /// </summary>
        public override void UnconnectTractor()
        {
            if (Parent != null)
            {
                Parent.MaxSemitrailerCapacity = 0;
                Parent.UnconnectSemitrailer();
                Parent = null;
            }
        }

        /// <summary>
        /// Contain type of semitrailer
        /// </summary>
        public override string Type => "Awning";

        /// <summary>
        /// Writes all information about semitrailer in xml file
        /// </summary>
        /// <param name="xml"></param>
        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("Semitrailer");
            xml.WriteAttributeString("type", $"{Type}");
            xml.WriteAttributeString("LiftingCapacity", $"{LiftingCapacity}");
            xml.WriteAttributeString("VolumeOfSemitrailer", $"{VolumeOfSemitrailer }");
            xml.WriteAttributeString("CurrentWeight", $"{CurrentWeight}");
            xml.WriteAttributeString("CurrentVolume", $"{CurrentVolume}");
            if (supplies.Count != 0)
            {
                xml.WriteStartElement("stocks");
                foreach (KeyValuePair<Supply, int> supply in supplies)
                {
                    xml.WriteStartElement("stock");
                    xml.WriteAttributeString("type", $"{supply.Key.ToString()}");
                    xml.WriteAttributeString("amount", $"{supply.Value}");
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }
    }
}
