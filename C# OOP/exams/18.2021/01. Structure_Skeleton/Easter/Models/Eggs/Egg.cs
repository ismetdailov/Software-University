﻿using Easter.Models.Eggs.Contracts;
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
            get { return name; }
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
          private  set 
            {
                if (value<0)
                {
                    value = 0;
                }
                energyRequired = value;
            }
        }


        public void GetColored()
        {
            EnergyRequired -= 10;
            if (EnergyRequired<0)
            {
                EnergyRequired = 0;
            }
        }

        public bool IsDone()
        {
            return EnergyRequired == 0;
        }
    }
}
