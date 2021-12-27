using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;
        public Controller()
        {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
        }
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car;
            if (type == "SuperCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == "TunedCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }
            cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);

        }

        public string AddRacer(string type, string username, string carVIN)
        {
            IRacer racer;
            ICar car = cars.FindBy(carVIN);
            if (car ==null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }
            if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username,car);
            }
            else if (type == "StreetRacer")
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }
            racers.Add(racer);
            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            var raceOne = racers.FindBy(racerOneUsername);
            var raceTwo = racers.FindBy(racerTwoUsername);
            if (raceOne == null)
            {
                throw new ArgumentException($"Racer {racerOneUsername} cannot be found!");
            }
            else if (raceTwo == null)
            {
                throw new ArgumentException($"Racer {racerTwoUsername} cannot be found!");
            }
            //raceOne.Race();
            //raceTwo.Race();
            return map.StartRace(raceOne,raceTwo);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            var ordered = racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(u => u.Username).ToList();
            foreach (var racer in ordered)
            {
                sb.AppendLine($"{racer.GetType().Name}: {racer.Username}");
                sb.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                sb.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
