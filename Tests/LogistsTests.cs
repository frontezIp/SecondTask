using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SecondTaskLibrary;
namespace SecondTaskLibraryTests
{
    [TestClass]
    public class LogistsTests
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
        AutoStation.Logists logists;

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
            awning = new Awning(600, 200, null, chemical);
            logists = new AutoStation.Logists();

        }

        [TestMethod]
        public void Upload_ShouldFullUpload()
        {
            // Arrange
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(antifreeze, 20);


            // Act
            logists.Upload(awning, chemical, 0);

            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }

        [TestMethod]
        public void AddLoad_ShouldAddload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(detergents, 15);


            // Act
            logists.AddLoad(awning, chemical, 1,15);

            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }

        [TestMethod]
        public void Unload_ShouldUnload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            awning.Upload(chemical, 0);


            // Act
            logists.Unload(awning,chemical,0);

            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }

        [TestMethod]
        public void PartialUnload_ShouldPartiallyUnload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(antifreeze, 10);
            awning.Upload(chemical, 0);


            // Act
            logists.PartialUnload(awning, chemical, 0, 10);


            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }

        [TestMethod]
        public void CreateCouple_ShouldConnectTractorWithSemitrailer()
        {
            // Arrange and act
            logists.CreateCouple(tractorOne, awning);

            // Assert
            Assert.AreEqual(tractorOne, awning.Parent);

        }
        
        [TestMethod]
        public void DisconnectCouple_ShouldDisconnectTractor()
        {
            // Arrange and act
            logists.CreateCouple(tractorOne, awning);
            logists.UnconnectCouple(awning);

            // Assert
            Assert.AreEqual(null, awning.Parent);
        }
    }
}
