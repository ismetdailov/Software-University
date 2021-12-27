using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        public Bunny(string name, int energy)
        {
            this.Name = name;
            this.Energy = energy;
            this.Dyes = new List<IDye>();
        }
        public string Name
        {
            get 
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                name = value;
            }
        }

        private int energy;

        public int Energy
        {
            get 
            {
                return energy;
            }
            protected set 
            {
                energy = value; 
                if (energy<0)
                {
                    energy = 0;
                }
            }
        }

       // private List<IDye> dyes;
        public ICollection<IDye> Dyes { get; }

        public void AddDye(IDye dye)
        {
            Dyes.Add(dye);
        }

        public virtual void Work()
        {
            Energy -= 10;
            if (Energy<0)
            {
                Energy = 0;
            }
        }
    }
}
