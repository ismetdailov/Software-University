using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private BankVault vault;
        private Item item;
        [SetUp]
        public void Setup()
        {
            vault = new BankVault();
            item = new Item("me", "1");
        }

        [Test]
        public void WhenCellDoesNotExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.AddItem("go nqma", item);
            });
            Assert.AreEqual(ex.Message, "Cell doesn't exists!");
        } 
        [Test]
        public void WhenCellIsAlreadyTaken_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.AddItem("A2", item);
                vault.AddItem("A2", new Item ("pesho", "3"));
            });
            Assert.AreEqual(ex.Message, "Cell is already taken!");
        }
        [Test]
        public void WhenItemAlreadyExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() =>
            {
                vault.AddItem("A2", item);
                vault.AddItem("B3", item);
            });
            Assert.AreEqual(ex.Message, "Item is already in cell!");
        } 
        [Test]
        public void WhenItemIsAddet_ShouldThrowException()
        {
            
             string result = vault.AddItem("A2", item);
            
            Assert.AreEqual(result, $"Item:{item.ItemId} saved successfully!");
        }
        [Test]
        public void WhenItemIsAddet_ShouldSetItemToCell()
        {
            
             string result = vault.AddItem("A2", item);
            
            Assert.AreEqual(item, vault.VaultCells["A2"]);
        }
        [Test]
        public void WhenRemoveCalledAndCellDoesNotExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.RemoveItem("go nqma", item);
            });
            Assert.AreEqual(ex.Message, "Cell doesn't exists!");
        }
        [Test]
        public void WhenRemoveCalledAndItemDoesNotExist_ShouldThrowException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                vault.RemoveItem("A2", item);
            });
            Assert.AreEqual(ex.Message, $"Item in that cell doesn't exists!");
        }
        [Test]
        public void WhenItemIsRemoved_ShouldThrowException()
        {
              vault.AddItem("A2", item);
            string result = vault.RemoveItem("A2", item);
            Assert.AreEqual(result, $"Remove item:{item.ItemId} successfully!");
        } 
        [Test]
        public void WhenItemIsRemoved_ShouldException()
        {
              vault.AddItem("A2", item);
            //Item savedItem = vault.VaultCells["A2"];

            string result = vault.RemoveItem("A2", item);

            Assert.AreEqual(null, vault.VaultCells["A2"]);
        }
    }
}