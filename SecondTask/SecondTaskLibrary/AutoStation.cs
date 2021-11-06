using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecondTaskLibrary
{
    public class AutoStation
    {
       public List<AutoPark.Semitrailer> semitrailers;
       public List<AutoPark.Tractor> tractors;
       public List<CategoryOfSupply> categories;
       public List<Supply> supplies;
        /// <summary>
        /// creates collections for everything in stock
        /// </summary>
        public AutoStation()
       {
            semitrailers = new List<AutoPark.Semitrailer>();
            tractors = new List<AutoPark.Tractor>();
            categories = new List<CategoryOfSupply>();
            supplies = new List<Supply>();
       }
       
        /// <summary>
        /// Reads information from xml file using xml class
        /// </summary>
        public void XmlReads()
        {
            XmlRead.Xml(this);
        }

        /// <summary>
        /// Writes information in xml file using xml class
        /// </summary>
        public void XmlWrites()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;
            XmlWriter xml = XmlWriter.Create("C:\\Users\\User\\source\\repos\\SecondTask\\SecondTaskLibrary\\xmll.xml", settings);
            xml.WriteStartDocument();
            xml.WriteStartElement("AutoStation");
            foreach (CategoryOfSupply category in categories)
                category.WriteXml(xml);
            foreach (AutoPark.Semitrailer semitrailer in semitrailers)
                semitrailer.WriteXml(xml);
            foreach (AutoPark.Tractor tractor in tractors)
                tractor.WriteXml(xml);
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Close();
            
        }
        /// <summary>
        /// Finding required supply according to given type of supply
        /// </summary>
        /// <param name="type">type of required supply</param>
        /// <returns></returns>
        public Supply FindSupply(string type)
        {
            foreach(Supply supply in supplies)
            {
                if (supply.ToString() == type)
                    return supply;
            }
            return null;
        }

        /// <summary>
        /// Finds semitrailer according to given type
        /// </summary>
        /// <param name="type">type of semitrailer</param>
        /// <returns></returns>
        public AutoPark.Semitrailer FindSemitrailer(string type)
        {
            foreach (AutoPark.Semitrailer semitrailer in semitrailers)
            {
                if (semitrailer.Type == type)
                    return semitrailer;
            }
            return null;
        }

        /// <summary>
        /// Finds semitrailer by given sample
        /// </summary>
        /// <param name="semitrailer">sample of semitrailer</param>
        /// <returns></returns>
        public AutoPark.Semitrailer FindBySample(AutoPark.Semitrailer semitrailer)
        {
            foreach (AutoPark.Semitrailer semitr in semitrailers)
            {
                if (semitr.LiftingCapacity == semitrailer.LiftingCapacity && semitr.VolumeOfSemitrailer == semitrailer.VolumeOfSemitrailer && semitr.GetHashCode() != semitrailer.GetHashCode())
                    return semitr;
            }
            return null;
        }

        /// <summary>
        /// Finds all couples Tractor-Semitrailer
        /// </summary>
        public IEnumerable<(AutoPark.Semitrailer, AutoPark.Tractor)> FindAllCouples
        {
            get
            {
                foreach (AutoPark.Semitrailer semitrailer in semitrailers)
                {
                    if (semitrailer.Parent != null)
                        yield return (semitrailer, semitrailer.Parent);
                }
            }
        }

        /// <summary>
        /// Finds all couples that can be  add loaded
        /// </summary>
        public IEnumerable<(AutoPark.Semitrailer, AutoPark.Tractor)> FindAllCouplesThatCanBeLoaded
        {
            get
            {
                foreach ((AutoPark.Semitrailer,AutoPark.Tractor) couple in FindAllCouples)
                {
                    if(couple.Item1.CurrentWeight!= couple.Item1.LiftingCapacity && couple.Item1.CurrentVolume != couple.Item1.VolumeOfSemitrailer)
                    {
                        yield return couple;
                    }
                }
            }
        }

        /// <summary>
        /// Finds all couples that can be full loaded
        /// </summary>
        public IEnumerable<(AutoPark.Semitrailer,AutoPark.Tractor)> FindAllCouplesThatCanBeFullLoaded
        {
            get
            {
                foreach((AutoPark.Semitrailer,AutoPark.Tractor) couple in FindAllCouples)
                {
                    if (couple.Item1.CheckForFullUpload() == true)
                        yield return couple;
                }
            }
        } 

        /// <summary>
        /// Upload required couple
        /// </summary>
        /// <param name="semitrailer"></param>
        /// <param name="tractor"></param>
        /// <param name="category">Category of supply</param>
        /// <param name="index">index of supply that will be uploaded</param>
        public void UploadCuple(AutoPark.Semitrailer semitrailer, AutoPark.Tractor tractor,CategoryOfSupply category, int index)
        {
            if(semitrailer.Parent == tractor)
            {
                semitrailer.Upload(category, index);
            }
        }

        /// <summary>
        /// Add load couple
        /// </summary>
        /// <param name="semitrailer"></param>
        /// <param name="tractor"></param>
        /// <param name="category">Category of supply</param>
        /// <param name="index">index of supply</param>
        /// <param name="amount"></param>
        public void AddLoadCuple(AutoPark.Semitrailer semitrailer, AutoPark.Tractor tractor, CategoryOfSupply category, int index,int amount)
        {
            if(semitrailer.Parent == tractor)
            {
                semitrailer.AddLoad(category, index, amount);
            }
        }

        /// <summary>
        /// Unload couple 
        /// </summary>
        /// <param name="semitrailer"></param>
        /// <param name="tractor"></param>
        /// <param name="category">Category of supply</param>
        /// <param name="index">index of supply</param>
        public void UnloadCuple(AutoPark.Semitrailer semitrailer, AutoPark.Tractor tractor, CategoryOfSupply category, int index)
        {
            if(semitrailer.Parent == tractor)
            {
                semitrailer.Unload(category, index);
            }
        }

        /// <summary>
        /// Unload couple partially
        /// </summary>
        /// <param name="semitrailer"></param>
        /// <param name="tractor"></param>
        /// <param name="category">Category of supplt</param>
        /// <param name="index">index of supply</param>
        /// <param name="amount"></param>
        public void UnloadCouplePartially(AutoPark.Semitrailer semitrailer, AutoPark.Tractor tractor, CategoryOfSupply category, int index, int amount)
        {
            if (semitrailer.Parent == tractor)
            {
                semitrailer.PartialUnload(category, index, amount);
            }
        }

        public class Logists
        {
            AutoStation control;
            /// <summary>
            /// Gives control for logists to Station
            /// </summary>
            public Logists()
            {
                control = new AutoStation();
            }

            /// <summary>
            /// Upload chosen semitrailer with supply
            /// </summary>
            /// <param name="semitrailer"></param>
            /// <param name="category">Category of supply</param>
            /// <param name="index">index of supply</param>
            public void Upload(AutoPark.Semitrailer semitrailer, CategoryOfSupply category, int index)
            {
                semitrailer.Upload(category, index);
            }

            /// <summary>
            /// Add load chosen semitrailer
            /// </summary>
            /// <param name="semitrailer"></param>
            /// <param name="category">Category of supply</param>
            /// <param name="index">index of supplt</param>
            /// <param name="amount"></param>
            public void AddLoad(AutoPark.Semitrailer semitrailer, CategoryOfSupply category, int index, int amount)
            {
                semitrailer.AddLoad(category, index, amount);
            }

            /// <summary>
            /// Unload chosen semitrailer
            /// </summary>
            /// <param name="semitrailer"></param>
            /// <param name="category">Category of supply</param>
            /// <param name="index">index of supply that need to be unloaded</param>
            public void Unload(AutoPark.Semitrailer semitrailer, CategoryOfSupply category, int index)
            {
                semitrailer.Unload(category, index);
            }

            /// <summary>
            /// Partially unload chosen semitrailer 
            /// </summary>
            /// <param name="semitrailer"></param>
            /// <param name="category">Category of supply</param>
            /// <param name="index">index of supply</param>
            /// <param name="amount"></param>
            public void PartialUnload(AutoPark.Semitrailer semitrailer, CategoryOfSupply category, int index, int amount)
            {
                semitrailer.PartialUnload(category, index, amount);
            }

            /// <summary>
            /// Connect chosen tractor and semitrailer in one couple
            /// </summary>
            /// <param name="tractor"></param>
            /// <param name="semitrailer"></param>
            public void CreateCouple(AutoPark.Tractor tractor, AutoPark.Semitrailer semitrailer)
            {
                semitrailer.ConnectTractor(tractor);
            }

            /// <summary>
            /// Disconnect semitrailer from its tractor
            /// </summary>
            /// <param name="semitrailer"></param>
            public void UnconnectCouple(AutoPark.Semitrailer semitrailer)
            {
                semitrailer.UnconnectTractor();
            }

        }

        public class AutoPark
        {
            public abstract class Tractor
            {
                /// <summary>
                /// Connected semitrailer
                /// </summary>
                private Semitrailer child;

                /// <summary>
                /// Model of tractor
                /// </summary>
                private ModelOfTractor model;

                private double maxSemitrailerCapacity;

                public double MaxSemitrailerCapacity { get => maxSemitrailerCapacity; set => maxSemitrailerCapacity = value; }
                public ModelOfTractor Model { get => model; set => model = value; }
                public Semitrailer Child { get => child; set => child = value; }

                public Tractor(ModelOfTractor model)
                {
                    this.model = model;
                }

                /// <summary>
                /// Disconnects semitrailer
                /// </summary>
                public abstract void UnconnectSemitrailer();

                /// <summary>
                /// Calculates fuel consumption
                /// </summary>
                /// <returns></returns>
                public abstract double CalculateFuelConsumption();

                /// <summary>
                /// Conects chosen semitrailer
                /// </summary>
                /// <param name="semitrailer"></param>
                public abstract void ConnectSemitrailer(Semitrailer semitrailer);

                /// <summary>
                /// Writes all data to xml file
                /// </summary>
                /// <param name="xml"></param>
                public abstract void WriteXml(XmlWriter xml);
            }

            public abstract class Semitrailer
            {
                private int volumeOfSemitrailer;

                private double liftingCapacity;

                private int currentVolume;

                private double currentWeight;

                /// <summary>
                /// Type of supply and what amount is stored in semitrailer
                /// </summary>
                public Dictionary<Supply, int> supplies;

                /// <summary>
                /// Connected tractor
                /// </summary>
                private Tractor parent;

                public double LiftingCapacity { get => liftingCapacity; set => liftingCapacity = value; }
                public Tractor Parent { get => parent; set => parent = value; }
                public int VolumeOfSemitrailer { get => volumeOfSemitrailer; set => volumeOfSemitrailer = value; }
                public double CurrentWeight { get => currentWeight; set => currentWeight = value; }
                public int CurrentVolume { get => currentVolume; set => currentVolume = value; }

                public Semitrailer(double liftingCapacity, int volumeOfSemitrailer = 0)
                {
                    LiftingCapacity = liftingCapacity;
                    VolumeOfSemitrailer = volumeOfSemitrailer;
                    supplies = new Dictionary<Supply, int>();
                }
                /// <summary>
                /// Type of semitrailer
                /// </summary>
                public abstract string Type { get; }

                /// <summary>
                /// Validate full upload
                /// </summary>
                /// <returns></returns>
                public abstract bool CheckForFullUpload();

                /// <summary>
                /// Uplpoads chosen category of supply and index of supply
                /// </summary>
                /// <param name="category"></param>
                /// <param name="index"></param>
                public abstract void Upload(CategoryOfSupply category,int index);

                /// <summary>
                /// Add load supply
                /// </summary>
                /// <param name="category"></param>
                /// <param name="index"></param>
                /// <param name="amount"></param>
                public abstract void AddLoad(CategoryOfSupply category, int index, int amount);

                /// <summary>
                /// Unload supply
                /// </summary>
                /// <param name="category"></param>
                /// <param name="index"></param>
                public abstract void Unload(CategoryOfSupply category, int index);

                /// <summary>
                /// Partially unload supply
                /// </summary>
                /// <param name="category"></param>
                /// <param name="index"></param>
                /// <param name="amount"></param>
                public abstract void PartialUnload(CategoryOfSupply category, int index, int amount);

                /// <summary>
                /// Connects chosen tractor
                /// </summary>
                /// <param name="tractor"></param>
                public abstract void ConnectTractor(Tractor tractor);

                /// <summary>
                /// Disconects current tractor
                /// </summary>
                public abstract void UnconnectTractor();

                /// <summary>
                /// Writes all its data in xml file
                /// </summary>
                /// <param name="xml"></param>
                public abstract void WriteXml(XmlWriter xml);

            }
        }
    }
}