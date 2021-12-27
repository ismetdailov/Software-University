using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
       // private RaceEntry raceEntry;
        private UnitCar  unitCar;
        private UnitDriver unitDriver;
        [SetUp]
        public void Setup()
        {
           // raceEntry = new RaceEntry();
            //unitCar = new UnitCar();
           // unitDriver = new UnitDriver();

        }

        [Test]
        public void TestOne()
        {
            var raceEntry = new RaceEntry();
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(null));
        } 
        [Test]
        public void TestTwo()
        {
            Assert.Throws<InvalidOperationException>(() =>
               {
                   var raceEntry = new RaceEntry();
                   var unitCar = new UnitCar("bmw", 231, 3000);
                   var unitDriver = new UnitDriver("Pesho", unitCar);
                   raceEntry.AddDriver(unitDriver);
                   raceEntry.AddDriver(unitDriver);
               });
        }  [Test]
        public void TestThree()
        {
            
                   var raceEntry = new RaceEntry();
                   var unitCar = new UnitCar("bmw", 231, 3000);
                   var unitDriver = new UnitDriver("Pesho", unitCar);

            var result = raceEntry.AddDriver(unitDriver);
            Assert.AreEqual("Driver Pesho added in race.", result);
            Assert.AreEqual(1, raceEntry.Counter);
        }
    }
}