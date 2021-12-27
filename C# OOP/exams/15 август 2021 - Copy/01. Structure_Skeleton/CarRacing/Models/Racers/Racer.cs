using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            this.username = username;
            this.racingBehavior = racingBehavior;
            this.drivingExperience = drivingExperience;
            this.Car = car;
        }

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }
                username = value;
            }
        }
        private string racingBehavior;

        public string RacingBehavior
        {
            get
            {
                return racingBehavior;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }
                racingBehavior = value;
            }
        }
        private int drivingExperience;

        public int DrivingExperience
        {
            get
            {
                return drivingExperience;
            }
            protected set
            {
                drivingExperience = value;
                if (drivingExperience < 0 || drivingExperience >100)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);

                }
            }
        }



        public ICar Car { get; private set; }
        //{
        //    get
        //    {
        //        return this.Car;
        //    }
        //  private  set
        //    {
        //        if(value == null)
        //        {
        //            throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
        //        }
        //    }
        //}

        public virtual bool IsAvailable()
        {
           int nesto = (int)Car.FuelAvailable;
            if ((int)Car.FuelAvailable <=0 )
            {
                return false;
            }
            return true;
        }

        public abstract void Race();
        
        
            
        
    }
}
