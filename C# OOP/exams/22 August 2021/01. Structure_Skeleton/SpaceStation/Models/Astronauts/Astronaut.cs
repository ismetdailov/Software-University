using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private Backpack backPack;
            public Astronaut(string name, double oxygen)
        {
            this.Name = name;
            this.Oxygen = oxygen;
            this.backPack = new Backpack();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }
                name = value;
            }
        }

        private double oxygen;

        public double Oxygen
        {
            get { return oxygen; }
           protected set
            {
                if (value<0)
                {
                    throw new ArithmeticException(ExceptionMessages.InvalidOxygen);
                }
                oxygen = value;
            }
        }


        public bool CanBreath => Oxygen>0;

        public IBag Bag => this.backPack;

        public virtual void Breath()
        {
            Oxygen -= 10;
            if (Oxygen<0)
            {
                Oxygen = 0;
            }
        }
    }
}
