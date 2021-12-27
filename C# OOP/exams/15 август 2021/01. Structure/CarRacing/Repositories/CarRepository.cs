using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using CarRacing.Utilities.Messages;
using System.Linq;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private IList<ICar> models;

        public CarRepository()
        {
        
        }
        public IReadOnlyCollection<ICar> Models { get; private set; }

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }
            models.Add(model);
        }

        public ICar FindBy(string property)
        {
            ICar car = models.FirstOrDefault(m => m.VIN == property);
            if (car == null)
            {
                return null;
            }
            return car;
        }

        public bool Remove(ICar model)
        {
            ICar car = models.FirstOrDefault(m => m.Model == model.Model);
            if (car == null)
            {
                return false;
            }
            return true;
        }
    }
}
