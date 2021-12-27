using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public  class ProfessionalRacer : Racer
    {
        public ProfessionalRacer(string username,  ICar car) : base(username, "strict", 30, car)
        {

        }
        public override bool IsAvailable()
        {
            return base.IsAvailable();
        }
        public override void Race()
        {
            Car.Drive();
            if (IsAvailable() == true)
            {
                this.DrivingExperience += 10;

            }
        }
        
    }
}
