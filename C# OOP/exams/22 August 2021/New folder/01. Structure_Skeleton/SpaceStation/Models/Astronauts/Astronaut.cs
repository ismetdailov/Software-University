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
        private Backpack backpack;
        public Astronaut(string name, double oxygen)
        {
            this.Name = name;
            this.Oxygen = oxygen;
            backpack = new Backpack();
        }
        private string name;

        public string Name
        {
            get { return name; }
          private  set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAstronautName);
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
                oxygen = value;
                if (oxygen < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }
            }
        }



        public bool CanBreath => oxygen > 0;
        public IBag Bag =>this.backpack;
        //to do 
        public virtual void Breath()
        {
            Oxygen -= 10;
            if (Oxygen < 0)
            {
                Oxygen = 0;
            }
        }
    }
}
