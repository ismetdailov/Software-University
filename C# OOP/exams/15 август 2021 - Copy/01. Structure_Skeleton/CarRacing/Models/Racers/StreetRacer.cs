using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public  class StreetRacer : Racer
    {
        public StreetRacer(string username, ICar car) : base(username, "aggressive", 10, car)
        {
        }
        public override bool IsAvailable()
        {
           
            return base.IsAvailable();
        }
        public override void Race()
        {

            this.Car.Drive();

            if (IsAvailable() == true)
            {
                this.DrivingExperience += 5;

            }

        }
    }
}
