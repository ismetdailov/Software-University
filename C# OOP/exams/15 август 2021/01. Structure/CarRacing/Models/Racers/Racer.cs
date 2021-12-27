using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
  public abstract  class Racer :IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        // private  ICar IRacer.Car car;
       // private ICar car;
        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {

        }
        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }
                this.username = value;
            }
        }  
        public string RacingBehavior
        {
            get
            {
                return this.racingBehavior;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }
                this.racingBehavior = value;
            }
        } 
        public int DrivingExperience
        {
            get
            {
                return this.drivingExperience;
            }
            private set
            {
                if (value<0 || value> 100)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                }
                this.drivingExperience = value;
            }
        }
        //public Car Car
        //{
        //    get
        //    {
        //        return this.car;
        //    }
        //    private set
        //    {
        //        if (car== null)
        //        {
        //            throw new ArgumentException(ExceptionMessages.InvalidCarMake);
        //        }
        //        this.car = value;
        //    }
        //}


        //ICar IRacer.Car
        //{
        //    get
        //    {
        //        return this.car;
        //    }
        //     set
        //    {
        //        if (car == null)
        //        {
        //            throw new ArgumentException(ExceptionMessages.InvalidCarMake);
        //        }
        //        this.car = value;
        //    }
        //}
        public ICar Car
        {
            get { return car; }

            private set
            {
                if (value == null)
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                car = value;
            }
        }


        public bool IsAvailable()
        {
            if (car.FuelAvailable < car.FuelConsumptionPerRace)
            {
                return false;
            }
            return true;
        }

        public virtual void Race()
        {

        }
    }
}
