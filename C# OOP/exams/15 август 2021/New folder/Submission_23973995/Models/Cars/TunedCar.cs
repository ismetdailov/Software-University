using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        public TunedCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, 65.0, 7.5)
        {
        }
        public override void Drive()
        {
            var something = Math.Round((double)HorsePower * (3 / 100));
            HorsePower = (int)something;
            base.Drive();
        }
    }
}
