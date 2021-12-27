using Easter.Models.Dyes.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
                return power;
            }
            private set
            {
                power = value;
                if (power < 0)
                {
                    power = 0;
                }
            }
        }

        public bool IsFinished()
        {
            if (Power ==0)
            {
                return true;
            }
            return false;
        }

        public virtual void Use()
        {
            Power -= 10;
            if (Power<0)
            {
                Power = 0;
            }
        }
    }
}
