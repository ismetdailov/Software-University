using System;
using System.Collections.Generic;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        public Bunny(string name , int energy)
        {
            this.Name = name;
            this.Energy = energy;

            this.dyes = new List<IDye>();
        }

        private string name;

        private int energy;

        private List<IDye> dyes;

        public string Name
        {
            get
            {
                return this.name;
            }

            private  set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }

                this.name = value;
            }
        }

        public int Energy
        {
            get
            {
                return this.energy;
            }

            protected set
            {
                if (value  < 0)
                {
                   this.energy = 0; ///CHECK  LATER FOR MISTAKE
               }

                else
                {
                    this.energy = value;
               }

            }
        }

        public ICollection<IDye> Dyes => this.dyes;

        public virtual void Work()
        {
            this.Energy -= 10;
            if(this.Energy < 0)
            {
                this.Energy = 0; ///CHECK  LATER FOR MISTAKE
            }
        }

        public void AddDye(IDye dye)
        {
            this.dyes.Add(dye);
        }
    }
}
