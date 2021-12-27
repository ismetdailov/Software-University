using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type == "SuperCar")
            {
                this.cars.Add(new SuperCar(make, model, VIN,horsePower));
            }
            else if (type == "TunedCar")
            {
                this.cars.Add(new TunedCar(make, model, VIN, horsePower));
            }
            else
            {
                return "Invalid car type!";
            }
           return string.Format(OutputMessages.SuccessfullyAddedCar ,make,model,VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
          
            if (type == "ProfessionalRacer")
            {
                this.racers.Add(new ProfessionalRacer(username, cars.FindBy(carVIN)));
            }
            else if (type == "StreetRacer")
            {
                this.racers.Add(new ProfessionalRacer(username, cars.FindBy(carVIN)));
            }
            else
            {
                return "Invalid racer type!";
            }
            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);

        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = racers.FindBy(racerOneUsername);
            IRacer racerTwo = racers.FindBy(racerTwoUsername);
         //  ICar car.StartRace(racerOne,racerTwo);
            if (racerOne == null)
            {
                return $"Racer {racerOneUsername} cannot be found!";
            }
            else if (racerTwo == null)
            {
                return $"Racer {racerTwoUsername} cannot be found!";

            }
            return null;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IRacer racer in racers.Models)
            {
                sb.AppendLine($"{racer.GetType()}: {racer.Username}");
                sb.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                sb.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
