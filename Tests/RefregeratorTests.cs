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
    public class RefregeratorTests
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
            refrigerator = new Refrigerator(600, 200, thermal);
        }

        [TestMethod]
        public void CheckForFullUpload_ShouldReturnTrue()
        {
            bool expected = true;
            refrigerator.Upload(thermal, 0);

            // Act
            bool actual = refrigerator.CheckForFullUpload();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForFullUpload_ShouldReturnFalse()
        {
            bool expected = false;
            refrigerator.Upload(thermal, 0);
            refrigerator.Upload(thermal, 1);

            // Act
            bool actual = refrigerator.CheckForFullUpload();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] 
        public void CheckForUpload_ShouldReturnTrue()
        {
            bool expected = true;

            // Act
            bool actual = refrigerator.CheckForUpload(fish);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForUpload_ShouldReturnFalse()
        {
            bool expected = false;
            fish.Count += 600;

            // Act
            bool actual = refrigerator.CheckForUpload(fish);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForAddload_ShouldReturnTrue()
        {
            bool expected = true;

            // Act
            bool actual = refrigerator.CheckForAddLoad(fish,10);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForAddload_ShouldReturnFalse()
        {
            bool expected = false;

            // Act
            bool actual = refrigerator.CheckForAddLoad(fish,60000);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddLoad_ShouldAddload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(fish, 10);


            // Act
            refrigerator.AddLoad(thermal, 1, 10);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }

        [TestMethod]
        public void Unload_ShouldUnload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            refrigerator.Upload(thermal, 0);


            // Act
            refrigerator.Unload(thermal,0);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }

        [TestMethod]
        public void PartialUnload_ShouldPartiallyUnload()
        {
            Dictionary<Supply, int> expected = new Dictionary<Supply, int>();
            expected.Add(milk, 20);
            refrigerator.Upload(thermal, 0);


            // Act
            refrigerator.PartialUnload(thermal, 0, 20);

            // Assert
            CollectionAssert.AreEqual(expected, refrigerator.supplies);
        }


    }

}