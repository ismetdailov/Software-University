using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        public TunedCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, 65, 7.5)
        {
        }
        public override void Drive()
        {
            double varia = 0.0;
            HorsePower *= 3;
            HorsePower /= 100;
            varia = HorsePower;
           varia = Math.Round(varia);
            HorsePower = (int)varia;
        }
    }
}
