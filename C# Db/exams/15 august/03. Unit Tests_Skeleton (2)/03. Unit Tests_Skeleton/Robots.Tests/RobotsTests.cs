namespace Robots.Tests
{
    using NUnit.Framework;
    using Robots;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class RobotsTests
    {
       // private Robot robot;
        [SetUp]
        public void SetUp()
        {
          //  this.robot = new Robot("kvo", 12);
        }

        [Test]
        public void CtorInitializesCollectionOfPresents()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var maneger = new RobotManager(-1);
            });
        }
        [Test]
        public void CtorInitialisadasdzesCollectionOfPresents()
        {
            var maneger = new RobotManager(10);
            Assert.AreEqual(10, 10);
        }
        [Test]
        public void addRobot()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var robot = new Robot("joni", 12);
                var robot1 = new Robot("joni", 12);
                var ro = new RobotManager(3);
                ro.Add(robot);
                ro.Add(robot1);
            });
        }
        [Test]
        public void addRobsdasadot()
        {
         
            Assert.Throws<InvalidOperationException>(() =>
            {
                var robot = new Robot("joni", 12);
                var robot1 = new Robot("joi", 12);
                var robot12 = new Robot("ni", 12);
                var ro = new RobotManager(1);
                ro.Add(robot);
                ro.Add(robot1);
            });
        }
        [Test]
        public void adRobot()
        {
                var robot = new Robot("joni", 12);
                var ro = new RobotManager(3);
                ro.Add(robot);
            Assert.AreEqual(1, ro.Count);
        }
        [Test]
        public void removeRobot()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var robot = new Robot("joni", 12);
                var ro = new RobotManager(1);
                ro.Remove("joniu");
            });
        }
        [Test]
        public void remofsdfsdfveRobot()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var ro = new RobotManager(1);
                ro.Remove(null);
            });
        }
        [Test]
        public void removeWoirk()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var ro = new RobotManager(1);
                ro.Work("joniu","job",12);
            });
        }
        [Test]
        public void removeWfdsdfoirk()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var ro = new RobotManager(1);
                var rob = new Robot("godjo", 6);
                ro.Add(rob);
                ro.Work("go", "bob", 8);
            });
        }
        [Test]
        public void removeWok()
        {
        
            var ro = new RobotManager(1);
            var rob = new Robot("godjo", 10);
            ro.Add(rob);
            ro.Work("godjo", "bob", 8);
            var res = rob.Battery -= 8;
            Assert.AreEqual(res, rob.Battery);

        }
        [Test]
        public void charge()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var ro = new RobotManager(1);
                ro.Charge(null);
            });
        }
        [Test]
        public void chargenot()
        {
                var ro = new RobotManager(1);
                var rob = new Robot("godjo", 10);
            ro.Add(rob);
            ro.Charge("godjo");
            Assert.AreEqual(10, rob.Battery);
           
        }
        [Test]
        public void ctor()
        {
            var ro = new RobotManager(3);
            var rob = new Robot("godjo", 10);
            var rob2 = new Robot("god", 6);
            List<Robot> robots;
             robots = new List<Robot>();
            ro.Add(rob2);
            ro.Add(rob);
            robots.Add(rob2);
            robots.Add(rob);
            Assert.AreEqual(2, ro.Count);

        }
        [Test]
        public void ctosr()
        {
            var ro = new RobotManager(1);
            var rob2 = new Robot("god", 6);
            var robots = new List<Robot>();
            ro.Add(rob2);
            Assert.AreEqual(1,ro.Capacity);

        }
        [Test]
        public void ctosssr()
        {
            var ro = new RobotManager(1);
            var rob2 = new Robot("god", 6);
            var robots = new List<Robot>();
            ro.Add(rob2);
            Assert.AreEqual(6, rob2.MaximumBattery);

        }
        [Test]
        public void ctosssfasfasfasfasfr()
        {
            var ro = new RobotManager(1);
            var rob2 = new Robot("god", 6);
            var robots = new List<Robot>();
            ro.Add(rob2);
            Assert.AreEqual("god", rob2.Name);

        }
        [Test]
        public void CtorInitializesCollecsadasdtionOfPresents()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var maneger = new RobotManager(-6);

            });
            var maneger = new RobotManager(10);

            Assert.AreEqual(10, maneger.Capacity);
        }
        [Test]
        public void removsdasdeRobot()
        {
            var ro = new RobotManager(4);
            var bo = new Robot("ko", 6);
            var so = new Robot("mo", 4);
            ro.Add(bo);
            ro.Add(so);
            ro.Remove("ko");
            Assert.AreEqual(1, ro.Count);
        }
        [Test]
        public void removsdasdasdasdeRobot()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var ro = new RobotManager(4);
                ro.Remove("kodsd");
            });
          
        }
    }
}
