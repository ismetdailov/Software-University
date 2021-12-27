    using Presents;
namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;
        private Present present;

        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
            present = new Present("name",12.5);
        }

        [Test]
        public void CtorInitializesCollectionOfPresents()
        {
            Assert.That(bag, Is.Not.Null);
        }

        [Test]
        public void CreateThrowsAnExceptionWhenPresetIsNull()
        {
            Present nullPresent = null;

            Assert.Throws<ArgumentNullException>(() => bag.Create(nullPresent));
        }
        public void CreatebagCreate()
        {
            var list = new List<Present>();
            var prez = new Present("ko", 5.2);
            list.Add(present);
            list.Add(prez);
            bag.Create(present);
            Assert
            Assert.Throws<ArgumentNullException>(() => bag.Create(nullPresent));
        }
    }

}
