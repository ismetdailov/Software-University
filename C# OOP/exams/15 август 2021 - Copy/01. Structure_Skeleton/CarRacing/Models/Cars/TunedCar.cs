using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars.Contracts
{
    public  class TunedCar : Car
    {
       public TunedCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, 65,7.5)
        {
            
        }
        public override void Drive()
        {
            FuelAvailable -= FuelConsumptionPerRace;

            var ro = HorsePower * (3 / 100);
            this.HorsePower -=((int)ro); 
         }
    }
}
