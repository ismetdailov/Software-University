namespace Presents.Tests
{
    using NUnit.Framework;
    using Presents;
    using System;
    using System.Collections.Generic;
    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
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

        [Test]
        public void CreateThrowsAnExceptionWhenPresentIsAlreadyInTheBag()
        {
            Present present = new Present("Truck", 69);

            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }

        [Test]
        public void CreateAddsPresentsInTheBag()
        {
            Present present = new Present("Truck", 69);

            bag.Create(present);

            Assert.That(present, Is.EqualTo(bag.GetPresent(present.Name)));
        }

        [Test]
        public void CreateReturnsProperMessage()
        {
            Present present = new Present("Truck", 69);

            string expectedMsg = $"Successfully added present {present.Name}.";

            string actual = bag.Create(present);

            Assert.That(expectedMsg, Is.EqualTo(actual));
        }

        [Test]
        public void RemoveMethodRemovesPresentsFromTheBag()
        {
            Present present = new Present("Truck", 69);

            bag.Create(present);

            bag.Remove(present);

            Assert.That(bag.GetPresent(present.Name), Is.Null);
        }

        [Test]
        public void RemoveReturnsBoolean()
        {
            Present present = new Present("Truck", 69);

            Assert.IsFalse(bag.Remove(present));

            bag.Create(present);

            Assert.IsTrue(bag.Remove(present));
        }

        [Test]
        public void GetPresentWithLeastMagicWorks()
        {
            Present truck = new Present("Truck", 69);
            Present bus = new Present("Bus", 100);
            Present leastMagic = new Present("No Magic", 1);

            bag.Create(truck);
            bag.Create(bus);
            bag.Create(leastMagic);

            Assert.That(leastMagic, Is.EqualTo(bag.GetPresentWithLeastMagic()));
        }

        [Test]
        public void GetPresentReturnPresent()
        {
            Present truck = new Present("Truck", 69);
            Present bus = new Present("Bus", 100);

            bag.Create(truck);
            bag.Create(bus);

            Present expectedPresent = bus;
            Present actualPresent = bag.GetPresent(bus.Name);

            Assert.AreEqual(expectedPresent, actualPresent);
        }

        [Test]
        public void GetPresentsReturnsBagAsReadOnlyCollection()
        {
            Assert.That(bag.GetPresents(), Is.InstanceOf<IReadOnlyCollection<Present>>());
        }
    }
    //[TestFixture]
    //public class PresentsTests
    //{
    //    private Bag bag1;
    //    [SetUp]
    //    public void SetUp()
    //    {
    //        this.bag1 = new Bag();
    //    }
    //    [Test]
    //    public void Test()
    //    {
    //        var present = new Present("present", 10.10);
    //        var bag = new Bag();
    //        Assert.AreEqual(present.Name, "present");
    //        Assert.AreEqual(present.Magic, 10.10);
    //    }
    //    [Test]
    //    public void TestCount()
    //    {
    //        var present = new Present("present", 10.10);
    //        bag1.Create(present);
    //        Assert.AreEqual(bag1.GetPresents().Count, 1);
    //    }
    //    [Test]
    //    public void TestBagCreateCorrectly()
    //    {
    //        var present = new Present("present", 10.10);
    //        var bag = new Bag();
    //        Assert.AreEqual(bag.Create(present), $"Successfully added present {present.Name}.");
    //    }
    //    [Test]
    //    public void CreateThrowsAnExceptionWhenPresetIsNull()
    //    {
    //        Present nullPresent = null;

    //        Assert.Throws<ArgumentNullException>(() => bag1.Create(nullPresent));
    //    }

    //    [Test]
    //    public void CreateThrowsAnExceptionWhenPresentIsAlreadyInTheBag()
    //    {
    //        Present present = new Present("Truck", 69);

    //        bag1.Create(present);

    //        Assert.Throws<InvalidOperationException>(() => bag1.Create(present));
    //    }
    //    [Test]
    //    public void CreateThrowsAnExceptionWhenPrremove()
    //    {
    //        Present present = new Present("Truck", 69);
    //        bag1.Create(present);
    //        Assert.AreEqual(bag1.Remove(present), bag1.GetPresents().Count == 0);
    //    }
    //    [Test]
    //    public void CreateThrowsAnExceptionWhenLeast()
    //    {
    //        Present present = new Present("present", 10);
    //        Present present1 = new Present("present", 5);
    //        Present present2 = new Present("present", 2);
    //        bag1.Create(present);
    //        bag1.Create(present1);
    //        bag1.Create(present2);
    //        Assert.AreEqual(bag1.GetPresentWithLeastMagic(), present2);
    //    }
    //    [Test]
    //    public void CreateThrowsAnExceptGetPresent()
    //    {
    //        Present present = new Present("present", 10);
    //        Present present1 = new Present("present1", 5);
    //        Present present2 = new Present("present2", 2);
    //        bag1.Create(present);
    //        bag1.Create(present1);
    //        bag1.Create(present2);
    //        Assert.AreEqual(bag1.GetPresent("present2"), present2);
    //    }
    //}
}
