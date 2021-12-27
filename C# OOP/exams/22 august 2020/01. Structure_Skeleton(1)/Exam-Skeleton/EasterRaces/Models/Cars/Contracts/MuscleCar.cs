using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars.Contracts
{
    public class MuscleCar : Car
    {
        public MuscleCar(string model, int horsePower) : base(model, horsePower, 5000, 400, 600)
        {
        }

      
    }
}
