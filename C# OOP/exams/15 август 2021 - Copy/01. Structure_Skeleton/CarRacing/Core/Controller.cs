using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.IO
{
    public  class Controller : IController
    {
        private IRepository<ICar> cars;
        private IRepository<IRacer> racers;
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
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }
            cars.Add(car);
            return String.Format(OutputMessages.SuccessfullyAddedCar,make,model,VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            IRacer racer;
            ICar car ; 
                car =this.cars.Models.FirstOrDefault(x => x.VIN == carVIN);
            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }
            if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, car);
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
            return String.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne ;
            racerOne= racers.Models.FirstOrDefault(x => x.Username == racerOneUsername);
            IRacer racerTwo ;
            racerTwo= racers.Models.FirstOrDefault(x => x.Username == racerTwoUsername);
           // IMap map ;
            
            if (racerOne == null)
            {
                throw new ArgumentException(ExceptionMessages.RacerCannotBeFound,racerOneUsername);
            }
            else if (racerTwo == null)
            {
                throw new ArgumentException(ExceptionMessages.RacerCannotBeFound,racerTwoUsername);
            }
            else
            {
                racerOne.Race();
                racerTwo.Race();
                return map.StartRace(racerOne, racerTwo);
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            var race =this.racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(y=>y.Username).ToList();
      //      this.racers.Models.OrderBy(x=>x.Username);
            foreach (var racer in race)
            {
                var type = racer.Username.ToString();
                sb.AppendLine($"{type}: {racer.Username}");
                sb.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                sb.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");

            }
            return sb.ToString().TrimEnd();

        }
    }
}
