using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {


        private string name;
        private Bag Bag;
        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            this.Name = name;
            this.Health = health;
            this.Armor = armor;
            this.AbilityPoints = abilityPoints;
            this.Bag = bag;
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }
        private double baseHealth;

        public double BaseHealth
        {
            get { return baseHealth; }
            private set
            {
                baseHealth = value;
            }
        }
        private double health;

        public double Health
        {
            get { return health; }
            private set
            {
                if (value > BaseHealth)
                {
                    value = baseHealth;
                }
                if (value < 0)
                {
                    value = 0;
                }
                health = value;
            }
        }

        private double baseArmor;

        public double BaseArmor
        {
            get { return baseArmor; }
            private set
            {

                baseArmor = value;
            }
        }
        private double armor;

        public double Armor
        {
            get { return armor; }
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }
                armor = value;
            }
        }
        private double abilityPoints;

        public double AbilityPoints
        {
            get { return abilityPoints; }
            private set
            {

                abilityPoints = value;
            }
        }



        public bool IsAlive { get; set; } = true;

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {

            }
        }
        public void TakeDamage(double hitPoints)
        {
            if (IsAlive)
            {
                var neshto = Math.Abs(armor -= hitPoints);
                Health -= neshto;
                if (Health < 0)
                {
                    IsAlive = false;
                }
            }


        }
       public void UseItem(Item item)
        {
            
        }
    }
}