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
    public class TractorOneTests
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
        public void CalculateFuelConsumption_ShouldReturn()
        {
            // Arrange
            double expected = 2;
            refrigerator.ConnectTractor(tractorOne);

            // Act
            double actual = tractorOne.CalculateFuelConsumption();

            // Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
