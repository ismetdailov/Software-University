using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter
{
   public  class Bunny :IBunny
    {
        private List<IDye> dyes;
        public Bunny(string name, int energy)
        {
            this.Name = name;
            this.Energy = energy;
            dyes = new List<IDye>();
        }
        private string name;

        public string Name

        {
            get { return name; }
           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                name = value; 
            }
        }
        private int energy;

        public int Energy
        {
            get { return energy; }
           protected set 
            {
                if (energy<0)
                {
                    energy = 0;
                }
                energy = value; 
            }
        }

        public ICollection<IDye> Dyes => this.dyes;

        //public ICollection<IDye> Dyes
        //{
        //    get;
        //    private set;
        //}

        public virtual void Work()
        {
            energy -= 10;
            if (energy<0)
            {
                energy = 0;
            }
        }

        public void AddDye(IDye dye)
        {
            this.dyes.Add(dye);
        }
    }
}
