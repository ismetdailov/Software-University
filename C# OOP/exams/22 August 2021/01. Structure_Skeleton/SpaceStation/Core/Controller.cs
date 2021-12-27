using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private IMission mission;
        public Controller()
        {
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
            this.mission = new Mission();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;
            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
            astronautRepository.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planetRepository.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var astroForExplore = astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();
            var planetForExplore = planetRepository.Models.FirstOrDefault(x => x.Name == planetName);
            if (astroForExplore.Count() ==0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }
            
            mission.Explore(planetForExplore, astroForExplore);
            return string.Format(OutputMessages.PlanetExplored, planetName, astronautRepository.Models.Where(x => x.Oxygen == 0).ToList().Count);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{planetRepository.Models.Where(x=>x.Items.Count ==0).ToList().Count} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astro in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {astro.Name}");
                sb.AppendLine($"Oxygen: {astro.Oxygen}");
                sb.AppendLine($"Bag items: {(astro.Bag.Items.Count>0 ? string.Join(", ",astro.Bag.Items) : "none")}");
            }
            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
           var retired= astronautRepository.FindByName(astronautName);
            if (retired == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut,astronautName));
            }
            astronautRepository.Remove(retired);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
