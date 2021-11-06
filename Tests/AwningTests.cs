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
    public class AwningTests
    {
        private ThermalCategory thermal;
        private ChemicalCategory chemical;
        private Milk milk;
        private Fish fish;
        private Antifreeze antifreeze;
        private Detergents detergents;
        private TractorOne tractorOne;
        private TractorOne tractorTwo;
        private Awning awning;

        [TestInitialize]
        public void TestInitialize()
        {
            thermal = new ThermalCategory();
            chemical = new ChemicalCategory();
            milk = new Milk(thermal, (4, 10), 1, 40);
            fish = new Fish(thermal, (4, 10), 4, 20);
            antifreeze = new Antifreeze(chemical, 1, 20);
            detergents = new Detergents(chemical, 1, 35);
            tractorOne = new TractorOne(new ModelOfTractor(400, 200, "Audi"));
            awning = new Awning(600, 200,null, chemical);
        }

        [TestMethod]
        public void CheckForFullUpload_ShouldReturnTrue()
        {
            bool expected = true;
            
            awning.Upload(chemical, 0);

            // Act
            bool actual = awning.CheckForFullUpload();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForFullUpload_ShouldReturnFalse()
        {
            bool expected = false;
            awning.Upload(chemical, 0);
            awning.Upload(chemical, 1);

            // Act
            bool actual = awning.CheckForFullUpload();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForUpload_ShouldReturnTrue()
        {
            bool expected = true;

            // Act
            bool actual = awning.CheckForUpload(fish);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForUpload_ShouldReturnFalse()
        {
            bool expected = false;
            fish.Count += 600;

            // Act
            bool actual = awning.CheckForUpload(fish);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForAddload_ShouldReturnTrue()
        {
            bool expected = true;

            // Act
            bool actual = awning.CheckForAddLoad(antifreeze, 10);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForAddload_ShouldReturnFalse()
        {
            bool expected = false;

            // Act
            bool actual = awning.CheckForAddLoad(fish, 60000);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddLoad_ShouldAddload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(detergents, 10);


            // Act
            awning.AddLoad(chemical, 1, 10);

            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }

        [TestMethod]
        public void Unload_ShouldUnload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            awning.Upload(chemical, 0);


            // Act
            awning.Unload(chemical, 0);

            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }

        [TestMethod]
        public void PartialUnload_ShouldPartiallyUnload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(antifreeze, 5);
            awning.Upload(chemical, 0);


            // Act
            awning.PartialUnload(chemical, 0, 15);

            // Assert
            CollectionAssert.AreEqual(expected, awning.supplies);
        }


    }

}