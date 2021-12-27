using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        // private List<IBag> bags;
       // private IBag bag;
        public Astronaut(string name, double oxygen)
        {
            this.name = name;
            this.oxygen = oxygen;
            //  this.bags = new List<IBag>();
        }
        private string name;

        public string Name
        {
            get { return name; }
          private  set 
            {
                name = value;
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAstronautName);
                }
            }
        }
        private double oxygen;

        public double Oxygen
        {
            get { return oxygen; }
          protected  set 
            {
                oxygen = value;
                if (oxygen<0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }
            }
        }

        public bool CanBreath =>  Oxygen>0;

        public IBag Bag { get; }

        public virtual void Breath()
        {
            this.Oxygen -= 10;
            if (Oxygen<0)
            {
                Oxygen = 0;
            }
        }
    }
}
