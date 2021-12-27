using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public  class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars;
        public CarRepository()
        {
            this.cars = new List<ICar>();
        }
        public IReadOnlyCollection<ICar> Models => cars;

        public void Add(ICar model)
        {
            //ICar mar = cars.Find(x => x.Model == model.Model);
            ICar car = cars.FirstOrDefault(x => x.Model == model.Model);
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }
            cars.Add(model);
        }

        public ICar FindBy(string property)
        {
            ICar vin = cars.FirstOrDefault(x => x.VIN == property);
            if (vin == null)
            {
                return null;
            }
            return vin;
        }

        public bool Remove(ICar model)
        {
            var car = cars.Contains(model);
            return car;
        }
    }
}
