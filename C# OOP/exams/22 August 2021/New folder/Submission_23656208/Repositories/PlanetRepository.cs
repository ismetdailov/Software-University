using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;
        public PlanetRepository()
        {
            this.models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => throw new NotImplementedException();

        public void Add(IPlanet model)
        {
            models.Add(model);

        }

        public IPlanet FindByName(string name)
        {
            IPlanet astro = models.FirstOrDefault(x => x.Name == name);
            if (astro == null)
            {
                return null;
            }
            return astro;
        }

        public bool Remove(IPlanet model)
        {
            IPlanet astro = models.FirstOrDefault(x => x == model);
            if (astro == null)
            {
                return false;
            }
            return true;
        }
    }
}
