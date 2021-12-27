namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        [Test]
        public void ConstructorInitializeCorrectly()
        {
            string name = "aname";
            int cap = 1;
            Aquarium aquarium = new Aquarium(name, cap);
            Assert.AreEqual(aquarium.Name, name);
            Assert.AreEqual(aquarium.Capacity, cap);
        }
        [Test]
        public void SetNameThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 1));
            Assert.Throws<ArgumentNullException>(() => new Aquarium(string.Empty, 1));
        }
        [Test]
        public void CapacityThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("a", -1));
        }
        [Test]
        public void CountIsWorketTrue()
        {
            Aquarium aquarium = new Aquarium("A", 1);
            int co = 1;
            aquarium.Add(new Fish("A"));
            Assert.AreEqual(co, aquarium.Count);
        }
        [Test]
        public void AddShouldThrowException()
        {
            Aquarium aquarium = new Aquarium("Testa", 0);
            Assert.Throws<InvalidOperationException>(() => aquarium.Add(new Fish("fishy")));
        }
        [Test]
        public void RemoveThrowException()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(null));
        }
        //[Test]
        //public void Remove()
        //{
        //    Aquarium aquarium = new Aquarium("a", 1);
        //    aquarium.Add(new Fish("alabala"));
        //    aquarium.RemoveFish("alabala");
        //    Assert.AreEqual(aquarium.Count, 0);
        //}
        [Test]
        public void SellFishShouldThrowException()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish(null));
        }
        [Test]
        public void SellFish()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            aquarium.Add(new Fish("alabala"));
            Fish fish = aquarium.SellFish("alabala");
            Assert.AreEqual(fish.Name, "alabala");
            Assert.AreEqual(fish.Available, false);
        }
        [Test]
        public void Report()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            aquarium.Add(new Fish("alabala"));
            string expectedMessage = $"Fish available at a: alabala";
            Assert.AreEqual(expectedMessage, aquarium.Report());
        }
    }
}
