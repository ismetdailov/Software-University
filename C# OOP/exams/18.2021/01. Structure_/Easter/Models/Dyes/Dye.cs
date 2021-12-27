using Easter.Models.Dyes.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Dyes
{
    public  class Dye : IDye
    {

        private int power;
        public Dye(int power)
        {
            this.Power = power;
        }
        public int Power
        {
            get 
            { 
                return power; 
            }
           private set
            {
                if (power<0)
                {
                    power = 0;
                }
                power = value; 
            }
        }


        public bool IsFinished()
        {
            if (power==0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public void Use()
        {
            power -= 10;
            if (power<0)
            {
                power = 0;
            }
        }
    }
}
