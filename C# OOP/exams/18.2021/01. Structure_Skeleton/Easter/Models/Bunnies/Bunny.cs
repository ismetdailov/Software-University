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
        private List<IDye> dyes;
        public Bunny(string name, int energy)
        {
            this.dyes = new List<IDye>();
            this.Name = name;
            this.Energy = energy;
        }
        public string Name
        {
            get { return name; }
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
            get { return energy; }
            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }
                energy = value;
            }
        }



        public ICollection<IDye> Dyes => this.dyes;

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public virtual void Work()
        {
            Energy -= 10;
            if (Energy<0)
            {
                Energy = 0;
            }
            //Energy -= 10;
            //if (Energy < 0)
            //{
            //    Energy = 0;
            //}
            //else
            //{
            //    while (Dyes.Any())
            //    {
            //        if (Dyes.First().IsFinished() == false)
            //        {
            //            Dyes.First().Use();
            //            break;
            //        }

            //        Dyes.Remove(Dyes.First());
            //    }
           // }
        }
    }
}
