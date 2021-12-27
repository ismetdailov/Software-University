using System;
using Easter.Models.Dyes.Contracts;

namespace Easter.Models.Dyes
{
    public class Dye : IDye
    {
        public Dye(int power)
        {
            this.Power = power;
        }

        private int power;

        public int Power
        {
            get
            {
                return this.power;
            }

            private set
            {
                if (value < 0)
                {
                    this.power = 0;
                }

                else
                {
                    this.power = value; ///CHECK  LATER FOR MISTAKE
               }
            }
        }

        public bool IsFinished()
        {
           if(power == 0 )
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
            this.Power -= 10;  ///CHECK  LATER FOR MISTAKE
            if(this.Power < 0)
            {
               this.Power = 0;
            }
        }
    }
}
