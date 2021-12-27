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
        private ICar car;
        private string username;
        private string racingBehavior;


        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            this.Username = username;
            this.RacingBehavior = racingBehavior;
            this.DrivingExperience = drivingExperience;
            this.Car = car;
        }

        public string Username
        {
            get { return username; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }
                username = value;
            }
        }


        public string RacingBehavior
        {
            get { return racingBehavior; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }
                racingBehavior = value;
            }
        }

        private int drivingExperience;

        public int DrivingExperience
        {
            get { return drivingExperience; }
            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                }
                drivingExperience = value;
            }
        }


        public ICar Car
        {
            get { return car; }
           private set 
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                }
                this.car = value;
            }
        }


        public bool IsAvailable() => car.FuelAvailable >= car.FuelConsumptionPerRace;
       

        public virtual void Race()
        {
            Car.Drive();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name}: {Username}");
            sb.AppendLine($"--Driving behavior: {RacingBehavior}");
            sb.AppendLine($"--Driving experience: {DrivingExperience}");
            sb.AppendLine($"--Car: {car.Make} {car.Model} ({car.VIN})");

            return sb.ToString().TrimEnd();
        }
    }
}
