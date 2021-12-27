using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> astronauts;

        public AstronautRepository()
        {
            this.astronauts = new List<IAstronaut>();
        }
        // moje da iskat imeto da e models a ne astronauts!!!
        public IReadOnlyCollection<IAstronaut> Models => this.astronauts;

        public void Add(IAstronaut model)
        {
            astronauts.Add(model);
        }

        public IAstronaut FindByName(string name)
        {
            IAstronaut astr = astronauts.FirstOrDefault(x => x.Name == name);
            return astr;
        }

        public bool Remove(IAstronaut model)
        {
            return astronauts.Remove(model);
        }
    }
}
