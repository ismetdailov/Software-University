using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    [TestFixture]

    public class GymsTests
    {
        [Test]
        public void ConstructorInitializeCorrectly()
        {
            string name = "aname";
            int cap = 1;
            Gym aquarium = new Gym(name, cap);

            Assert.AreEqual(aquarium.Name, name);
            Assert.AreEqual(aquarium.Capacity, cap);
        }
        [Test]
        public void ConstructorInitializy()
        {
            string name = "aname";
            Athlete aquarium = new Athlete(name);

            Assert.AreEqual(aquarium.FullName, name);
            Assert.AreEqual(aquarium.IsInjured, false);
        }
        [Test]
        public void ConstructorInasdasdasditializy()
        {
            string name = "aname";
            Athlete aquarium = new Athlete(name);

            Assert.AreEqual(aquarium.FullName, name);
        }
        [Test]
        public void ConstructorInasdasdasadsadasdditializy()
        {
            string name = "aname";
            Athlete aquarium = new Athlete(name);

            Assert.AreEqual(aquarium.IsInjured, false);

        }
        [Test]
        public void test()
        {
            var gym = new Gym("name", 6);
            Assert.AreEqual(6, gym.Capacity);
        }
        [Test]
        public void testNameBBB()
        {
            Assert.Throws<ArgumentNullException>(() => new Gym("", 1));
        }
        [Test]
        public void testsada()
        {
            var gym = new Gym("name", 6);
            Assert.AreEqual("name", gym.Name);
        }
        [Test]
        public void tessdsadasdasdtsada()
        {
            var gym = new Gym("name", 6);
            Assert.AreEqual(gym.Name,"name");
        }
        [Test]
        public void testssadasdada()
        {
            var gym = new Gym("name", 6);
            Assert.AreEqual(6, gym.Capacity);
        }
        [Test]
        public void testName()
        {
            Assert.Throws<ArgumentNullException>(()=> new Gym(null,1));

        }
        [Test]
        public void testasdasdassadName()
        {
            Assert.Throws<ArgumentNullException>(() => new Gym("", 1));

        }
        [Test]
        public void testNameCr()
        {
            Assert.Throws<ArgumentException>(() => new Gym("name", -5));

        }
        [Test]
        public void testAddAtleteShouldThrowError()
        {
            var gym = new Gym("name", 0);
            var atlet = new Athlete("atlet");
            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(atlet));

        }

        [Test]
        public void testCount()
        {
            var gym = new Gym("name", 2);
            var atlet = new Athlete("atlet");
            gym.AddAthlete(atlet);
            Assert.AreEqual(1, gym.Count);
            Assert.AreEqual(gym.Count,1);
        }
        [Test]
        public void testAddAthletheCount()
        {
            var gym = new Gym("name", 0);
            var atlet = new Athlete("atlet");
            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(atlet));

        }
        [Test]
        public void testRemoveShouldThrowError()
        {
            var gym = new Gym("name", 0);
            var atlet = new Athlete("atlet");
            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete("losh"));
        }
        [Test]
        public void testRemoveShouldTError()
        {
            var gym = new Gym("name", 0);
            var atlet = new Athlete("atlet");
            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete(null));
        }

        [Test]
        public void testRemoveTrue()
        {
            var gym = new Gym("name", 5);
            var atlet = new Athlete("atlet");
            gym.AddAthlete(atlet);
            gym.RemoveAthlete("atlet");
            Assert.AreEqual(0, gym.Count);
        }
        [Test]
        public void testFullNameAthlete()
        {
            var gym = new Gym("name", 5);
            var atlet = new Athlete("atlet");
            gym.AddAthlete(atlet);
            Assert.AreEqual("atlet", atlet.FullName);
        }
        [Test]
        public void testFullInjured()
        {
            var gym = new Gym("name", 5);
            var atlet = new Athlete("atlet");
            gym.AddAthlete(atlet);
            Assert.AreEqual(false, atlet.IsInjured);
        }
        [Test]
        public void testFullInjuredTrue()
        {
            var gym = new Gym("name", 5);
            var atlet = new Athlete("atlet");
            gym.AddAthlete(atlet);
            gym.InjureAthlete("atlet");
            Assert.AreEqual(true, atlet.IsInjured);
        }
        [Test]
        public void testInjureThrowError()
        {
            var gym = new Gym("name", 0);
            var atlet = new Athlete("atlet");

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete("h"));
        }
        [Test]
        public void testInjureError()
        {
            var gym = new Gym("name", 0);
            var atlet = new Athlete("atlet");

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete(null));
        }
        [Test]
        public void testInjureTrue()
        {
            var gym = new Gym("name", 5);
            var atlet = new Athlete("atlet");
            gym.AddAthlete(atlet);
            gym.InjureAthlete("atlet");
            Assert.AreEqual(true, atlet.IsInjured);
        }
        [Test]
        public void testInReportTrue()
        {
            var gym = new Gym("name", 5);
            var atlet = new Athlete("atlet");
            var atlet1 = new Athlete("atlet1");
            var atlet2 = new Athlete("atlet2");
            gym.AddAthlete(atlet);
            gym.AddAthlete(atlet1);
            gym.AddAthlete(atlet2);
            gym.InjureAthlete("atlet2");


           

            Assert.AreEqual("Active athletes at name: atlet, atlet1", gym.Report());
        }
        // Implement unit tests here
    }
}
