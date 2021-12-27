using System;
using Easter.Models.Eggs.Contracts;
using Easter.Utilities.Messages;

namespace Easter.Models.Eggs
{
    public class Egg : IEgg
    {
        public Egg(string name , int energyRequired)
        {
            this.Name = name;
            this.EnergyRequired = energyRequired;
        }

        private string name;

        private int energyRequired;

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEggName);
                }

                this.name = value;
            }
        }
        public int EnergyRequired

        {
            get
            {
                return this.energyRequired;
            }

            private set
            {
                if (value < 0)
                {
                    this.energyRequired = 0; ///CHECK  LATER FOR MISTAKE
                }

                else
                {
                    this.energyRequired = value;
                }

            }
        }

        public void GetColored()
        {
            this.EnergyRequired -= 10;
            if(this.EnergyRequired < 0) 
            {
                this.EnergyRequired = 0;  ///CHECK  LATER FOR MISTAKE
            }
        }

        public bool IsDone()
        {
            if(this.EnergyRequired ==  0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
