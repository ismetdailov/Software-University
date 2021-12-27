using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public  class RacerRepository : IRepository<IRacer>
    {
        private readonly List<IRacer> racers;
        public RacerRepository()
        {
            this.racers = new List<IRacer>();
        }
        public IReadOnlyCollection<IRacer> Models => racers;

        public void Add(IRacer model)
        {
            var racer = racers.Contains(model);
            if (model==null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }
            racers.Add(model);
        }

        public IRacer FindBy(string property)
        {
            IRacer racer = racers.FirstOrDefault(x => x.Username == property); 
            throw new NotImplementedException();
        }

        public bool Remove(IRacer model)
        {
            var racer = racers.Contains(model);
            
            return racer;
        }

    }
}
