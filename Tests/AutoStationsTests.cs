using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SecondTaskLibrary;

namespace SecondTaskLibraryTests
{
    [TestClass]
    public class AutoStationsTests
    {
        private ThermalCategory thermal;
        private ChemicalCategory chemical;
        private Milk milk;
        private Fish fish;
        private Antifreeze antifreeze;
        private Detergents detergents;
        private TractorOne tractorOne;
        private TractorOne tractorTwo;
        private Refrigerator refrigerator;
        private Awning awning;
        AutoStation auto;

        [TestInitialize]
        public void TestInitialize()
        {
            thermal = new ThermalCategory();
            chemical = new ChemicalCategory();
            milk = new Milk(thermal, (4, 10), 1, 40);
            fish = new Fish(thermal, (-10, -5), 4, 20);
            antifreeze = new Antifreeze(chemical, 1, 20);
            detergents = new Detergents(chemical, 1, 35);
            tractorOne = new TractorOne(new ModelOfTractor(400, 200, "Audi"));
            tractorTwo = new TractorOne(new ModelOfTractor(500, 300, "Mersedes"));
            refrigerator = new Refrigerator(600, 200, thermal);
            awning = new Awning(600, 200,null, chemical);
            auto = new AutoStation();

        }

        [TestMethod]
        public void XmlReads_CheckForAnError()
        {
            // Arrange
            bool expected = true;
            bool actual = false;

            // Act
            auto.XmlReads();
            actual = true;


            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XmlWrites_CheckForAnError()
        {
            // Arrange
            bool expected = true;
            bool actual = false;
            auto.semitrailers.Add(refrigerator);
            auto.categories.Add(thermal);
            auto.tractors.Add(tractorTwo);
            auto.supplies.Add(fish);
            auto.supplies.Add(milk);
            // Act
            auto.XmlWrites();
            actual = true;


            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindSupply_ShouldReturnMilk()
        {
            // Arrange
            auto.supplies.Add(milk);
            string type = "Milk";
            Milk expected = milk;

            // Act
            Supply supply = auto.FindSupply(type);

            // Assert
            Assert.AreEqual(expected, supply);
        }

        [TestMethod]
        public void FindSemitrailer_ShouldReturnRefregerator()
        {
            // Arrange
            auto.semitrailers.Add(refrigerator);
            string type = "Refregerator";
            Refrigerator expected = refrigerator;

            // Act
            AutoStation.AutoPark.Semitrailer actual = auto.FindSemitrailer(type);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FidSemitrailerBySample_ShouldReturnAwning()
        {
            // Arrange
            auto.semitrailers.Add(awning);
            Awning expected = awning;

            // Act
            AutoStation.AutoPark.Semitrailer actual = auto.FindBySample(refrigerator);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UploadCouple_ShouldFullUpload()
        {
            // Arrange
            refrigerator.ConnectTractor(tractorOne);
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(milk, 40);


            // Act
            auto.UploadCuple(refrigerator, tractorOne, thermal, 0);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }

        [TestMethod]
        public void AddLoadCouple_ShouldAddload()
        {
            refrigerator.ConnectTractor(tractorOne);
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(fish, 10);


            // Act
            auto.AddLoadCuple(refrigerator, tractorOne, thermal, 1, 10);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }

        [TestMethod]
        public void UnloadCouple_ShouldUnloadCouple()
        {
            refrigerator.ConnectTractor(tractorOne);
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            refrigerator.Upload(thermal, 0);


            // Act
            auto.UnloadCuple(refrigerator, tractorOne, thermal, 0);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }

        [TestMethod]
        public void PartialUnload_ShouldPartiallyUnload()
        {
            refrigerator.ConnectTractor(tractorOne);
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(milk, 20);
            refrigerator.Upload(thermal, 0);


            // Act
            auto.UnloadCouplePartially(refrigerator, tractorOne, thermal, 0,20);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }
    }
    
}
