namespace Robots.Tests
{
    using NUnit.Framework;
    using System;
    [TestFixture]
    public class RobotsTests
    {
        [Test]
        public void testCtor()
        {
            var robo = new Robot("name", 10);
            Assert.AreEqual(robo.Name ,"name");

        }
        [Test]
        public void RobotBatery()
        {
            var robo = new Robot("name", 10);
            Assert.AreEqual(robo.MaximumBattery, 10);

        }
        [Test]
        public void Batery()
        {
            var robo = new Robot("name", 10);
            robo.Battery = 5;
            Assert.AreEqual(robo.Battery, 5);

        }
        [Test]
        public void ConstructorInitializeCorrectly()
        {
            string name = "aname";
            int cap = 1;
            Robot robot = new Robot(name, cap);

            Assert.AreEqual(robot.Name, name);
            Assert.AreEqual(robot.MaximumBattery, cap);
        }
        [Test]
        public void ConstructorRobotManager()
        {
            int cap = 1;
            RobotManager robot = new RobotManager(cap);

            Assert.AreEqual(robot.Capacity, cap);
        }
        //[Test]
        //public void ConstructorManager()
        //{
        //    int cap = 1;
        //    RobotManager robot = new RobotManager(cap);

        //    Assert.AreEqual(robot.Count, 1);
        //}
        [Test]
        public void shouldThrowEroorcapacity()
        {
            int cap = 1;
            RobotManager robot = new RobotManager(cap);

            Assert.Throws<ArgumentException>(()=>new RobotManager(-6));
        }
        [Test]
        public void shouldThrowErrorWhenAddTwoIdentityRobots()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            Robot robot1 = new Robot("name2", 5);
            Robot robot2 = new Robot("name2", 5);
            robot.Add(robot1);
            Assert.Throws<InvalidOperationException>(() =>robot.Add(robot2), $"There is already a robot with name {robot2.Name}!");
        }
        [Test]
        public void shouldThrowErrorWhenAddTwoIdentityRobotsCount()
        {
            int cap = 1;
            RobotManager robot = new RobotManager(cap);
            Robot robot1 = new Robot("name2", 5);
            Robot robot2 = new Robot("name2", 5);
            robot.Add(robot1);
            Assert.Throws<InvalidOperationException>(() => robot.Add(robot2), "Not enough capacity!");
        }
        [Test]
        public void shoulCorrectAddRobotsCount()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            Robot robot1 = new Robot("name2", 5);
            Robot robot2 = new Robot("name3", 5);
            robot.Add(robot1);
            robot.Add(robot2);
            Assert.AreEqual(robot.Count,2);
        }
        [Test]
        public void shoulCorrectAddRobotsCountName()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            Robot robot1 = new Robot("name2", 5);
            Robot robot2 = new Robot("name3", 5);
            robot.Add(robot1);
            robot.Add(robot2);
            robot.Remove("name3");
            Assert.AreEqual(robot.Count, 1);
        }
        [Test]
        public void shoulCorrecRemoveCountName()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            Assert.Throws<InvalidOperationException>(() => robot.Remove(null), $"Robot with the name {null} doesn't exist!");
        }
        [Test]
        public void workShouwdThrowError()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            Assert.Throws<InvalidOperationException>(() => robot.Work(null,null,0), $"Robot with the name {null} doesn't exist!");
        }
        [Test]
        public void workShouwdThrowErrorwhenBattery()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            var rob = new Robot("batery", 5);
            Assert.Throws<InvalidOperationException>(() => robot.Work("batery", "job", 6), $"{rob.Name} doesn't have enough battery!");
        }
        [Test]
        public void workShouwdThrowErrorwhenBatteryCorrect()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            var rob = new Robot("batery", 5);
            robot.Add(rob);
            robot.Work("batery", "job", 4);
            Assert.AreEqual(rob.Battery,1);
        }
        [Test]
        public void workShouwdThrowErrorwhenBatteryCharge()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            var rob = new Robot("batery", 5);
            robot.Add(rob);
            robot.Charge("batery");
            Assert.AreEqual(rob.Battery, 5);
        }
        [Test]
        public void workShouwdThrowErrorwhenBatteryChargeCorr()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            var rob = new Robot("batery", 5);
            robot.Add(rob);
            robot.Charge("batery");
            Assert.AreEqual(rob.Battery, rob.MaximumBattery);
        }
        [Test]
        public void workShouwdThrowErrorwhenBatteryChargeCr()
        {
            int cap = 3;
            RobotManager robot = new RobotManager(cap);
            var rob = new Robot("batery", 5);
            robot.Add(rob);
            robot.Charge("batery");
            Assert.Throws<InvalidOperationException>(() => robot.Charge(null), $"Robot with the name {null} doesn't exist!");

        }
    }
}
