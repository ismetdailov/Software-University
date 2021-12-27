namespace Robots.Tests
{
    using NUnit.Framework;
    using System;
    using Robots

    public class RobotsTests
    {
        private Robot robots;
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void WhenCellDoesNotExist_ShouldThrowException()
        {
             robots =
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.AddItem("go nqma", item);
            });
            Assert.AreEqual(ex.Message, "Cell doesn't exists!");
        }
    }
}
