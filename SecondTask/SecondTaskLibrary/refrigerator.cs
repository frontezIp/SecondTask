using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class Refrigerator : AutoStation.AutoPark.Semitrailer
    {
        /// <summary>
        /// Contain required tempreture for transportation
        /// </summary>
        private (int, int) rangeOfTemprature;

        ThermalCategory category;

        public Refrigerator(double liftingCapacity,
            int volumeOfSemitrailer, ThermalCategory category, int minTemp= 0, int maxTemp=0
            ) : base(liftingCapacity, volumeOfSemitrailer)
        {
            this.category = category;
            rangeOfTemprature = (minTemp,maxTemp);
        }


        public override bool CheckForFullUpload()
        {
            if (supplies.Count < category.supplies.Count)
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
            if (supply.Weight * supply.Count+CurrentWeight <= LiftingCapacity &&
                supply.Count+CurrentVolume <= VolumeOfSemitrailer
                && supplies.ContainsKey(supply) == false
                )
            {
                if (supplies.Count != 0)
                {
                    if (category.tempretureLimits[supply] == rangeOfTemprature)
                        return true;
                    else
                    {
                        return false;
                    }
                }
                else
                    return true;

            }
            else return false;
        }


        /// <summary>
        /// Validates add load
        /// </summary>
        /// <param name="supply"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CheckForAddLoad(Supply supply,int amount)
        {
            if (supply.Weight * amount + CurrentWeight <= LiftingCapacity &&
                            amount + CurrentVolume <= VolumeOfSemitrailer && 
                            amount <= supply.Count
                            )
            {
                if (supplies.Count != 0)
                {
                    if (rangeOfTemprature == category.tempretureLimits[supply])
                        return true;
                    else return false;
                }
                else return true;
                
                
            }
            return false;
        }


        public override void Upload(CategoryOfSupply category,int index)
        {
            if (this.category == category) 
            {
                if (CheckForUpload(category.supplies[index]))
                {
                    if(supplies.Count == 0)
                    rangeOfTemprature = this.category.tempretureLimits[this.category.supplies[index]];
                    supplies.Add(category.supplies[index], category.supplies[index].Count);
                    CurrentVolume += category.supplies[index].Count;
                    CurrentWeight += category.supplies[index].Count * category.supplies[index].Weight;
                    category.supplies[index].Count = 0;
                }
            }
        }

        public override void AddLoad(CategoryOfSupply category, int index,int amount)
        {
            if (this.category == category)
            {
                if (CheckForAddLoad(category.supplies[index],amount))
                {
                    if(supplies.Count==0)
                        rangeOfTemprature = this.category.tempretureLimits[this.category.supplies[index]];
                    if (supplies.ContainsKey(category.supplies[index]) == false)
                        supplies.Add(category.supplies[index], amount);
                    else
                        supplies[category.supplies[index]] += amount;
                    CurrentVolume += amount;
                    CurrentWeight += amount * category.supplies[index].Weight;
                    category.supplies[index].Count -= amount;
                }
            }
        }

        public override void Unload(CategoryOfSupply category,int index)
        {
            if(this.category == category)
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

        public override void PartialUnload(CategoryOfSupply category, int index, int amount)
        {
            if(this.category == category)
            {
                if (supplies.ContainsKey(category.supplies[index]))
                {
                   if(supplies[category.supplies[index]] < amount)
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

        public override void ConnectTractor(AutoStation.AutoPark.Tractor tractor)
        {
            if (Parent == null)
            {
                Parent = tractor;
                Parent.MaxSemitrailerCapacity = this.LiftingCapacity;
                tractor.ConnectSemitrailer(this);
            }
        }

        public override void UnconnectTractor()
        {
            if (Parent != null)
            {
                Parent.MaxSemitrailerCapacity = 0;
                Parent.UnconnectSemitrailer();
                Parent = null;
            }
            
        }
        
        public override string Type => "Refregerator";

        public override void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("Semitrailer");
            xml.WriteAttributeString("type", $"{Type}");
            xml.WriteAttributeString("LiftingCapacity", $"{LiftingCapacity}");
            xml.WriteAttributeString("VolumeOfSemitrailer", $"{VolumeOfSemitrailer }");
            xml.WriteAttributeString("CurrentWeight", $"{CurrentWeight}");
            xml.WriteAttributeString("CurrentVolume", $"{CurrentVolume}");
            xml.WriteAttributeString("tempretureMin", $"{ rangeOfTemprature.Item1}");
            xml.WriteAttributeString("tempretureMax", $"{ rangeOfTemprature.Item2}");
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
