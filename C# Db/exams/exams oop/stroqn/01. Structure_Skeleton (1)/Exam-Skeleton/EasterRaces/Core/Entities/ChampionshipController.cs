using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private readonly IRepository<IDriver> driverRepository;
        private readonly IRepository<ICar> carRepository;
        private readonly IRepository<IRace> raceRepository;
        public ChampionshipController()
        {
            this.driverRepository = new DriverRepository();
            this.carRepository = new CarRepository();
            this.raceRepository = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            var car = this.carRepository.GetByName(carModel);
            var driver = this.driverRepository.GetByName(driverName);

            if (driver== null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            if (car == null)
            {
                throw new InvalidOperationException($"Car {carModel} could not be found.");
            }
            driver.AddCar(car);
            this.carRepository.Remove(car);
            return $"Driver {driverName} received car {carModel}.";
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var race = this.raceRepository.GetByName(raceName);
            var driver = this.driverRepository.GetByName(driverName);

            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            race.AddDriver(driver);
            return $"Driver {driverName} added in {raceName} race.";
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            var carExist = this.carRepository.GetByName(model);
            if (carExist  != null)
            {
                throw new ArgumentException($"Car {model} is already created.");
            }
            
            ICar car = null;
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if (type == "Sports" )
            {
                car = new SportsCar(model, horsePower);
            }
            this.carRepository.Add(car);
            return $"{car.GetType().Name} {model} is created.";
        }

        public string CreateDriver(string driverName)
        {
            var driverExist = this.driverRepository.GetByName(driverName);
            if (driverExist != null)
            {
                throw new ArgumentException($"Driver {driverName} is already created.");
            }
            var driver = new Driver(driverName);
            this.driverRepository.Add(driver);

            return $"Driver {driverName} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            if (this.raceRepository.GetByName(name) != null)
            {
                throw new InvalidCastException($"Race {name} is already create.");
            }
            var race = new Race(name, laps);
            this.raceRepository.Add(race);
            return $"Race {name} is created.";
        }

        public string StartRace(string raceName)
        {
            var race = this.raceRepository.GetByName(raceName);

            if (race == null)
            {
                throw new InvalidCastException($"Race {raceName} could not be found.");
            }
            if (race.Drivers.Count < 3)
            {
                throw new InvalidCastException($"Race {raceName} cannot start with less than 3 participants.");
            }
            var drivers = race.Drivers
                 .OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps))
                 .ToList();
            var first = drivers[0];
            var second = drivers[1];
            var third = drivers[2];

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Driver {first.Name} wins {raceName} race.");
            sb.AppendLine($"Driver {second.Name} is second in {raceName} race.");
            sb.AppendLine($"Driver {third.Name} is third in {raceName} race.");
          
            var result = sb.ToString().TrimEnd();
            this.raceRepository.Remove(race);
            return result;
        }
    }
}
