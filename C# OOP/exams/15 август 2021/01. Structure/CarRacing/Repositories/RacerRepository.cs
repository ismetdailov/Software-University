using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private IList<IRacer> racers;

        public IReadOnlyCollection<IRacer> Models { get; private set; }

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }
            racers.Add(model);
        }

        public IRacer FindBy(string property)
        {
            IRacer racer = racers.FirstOrDefault(r => r.Username == property);
            if (racer == null)
            {
                return null;
            }
            return racer;
        }

        public bool Remove(IRacer model)
        {
            IRacer racer = racers.FirstOrDefault(r => r.Username == model.Username);
            if (racer == null)
            {
                return false;
            }
            return true;
        }
    }
}
