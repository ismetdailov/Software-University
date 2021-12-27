using Easter.Models.Eggs.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Eggs
{
    public class Egg : IEgg
    {
        public Egg(string name, int energyRequired)
        {
            this.Name = name;
            this.EnergyRequired = energyRequired;
        }
        private string name;
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
                    throw new ArgumentException(ExceptionMessages.InvalidEggName);
                }
                name = value;
            }
        }
        private int energyRequired;

        public int EnergyRequired
        {
            get { return energyRequired; }
           private set 
            {
                energyRequired = value;
                if (energyRequired<0)
                {
                    energyRequired = 0;
                }
            }
        }


        public void GetColored()
        {
            EnergyRequired -= 10;
            if (EnergyRequired<0)
            {
                energyRequired = 0;
            }
        }

        public bool IsDone()
        {
            if (EnergyRequired ==0)
            {
                return true;
            }
            return false;
        }
    }
}
