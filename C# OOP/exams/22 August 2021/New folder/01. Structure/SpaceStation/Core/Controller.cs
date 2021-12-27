using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private IRepository<IAstronaut> astronauts;
        private IRepository<IPlanet> planets;
        private IMission mision;
        private List<IPlanet> explored;

        public Controller()
        {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
            this.mision = new Mission();
            this.explored = new List<IPlanet>();
        }
        IAstronaut astro;
        public string AddAstronaut(string type, string astronautName)
        {

            if (type == "Biologist")
            {
                astro = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astro = new Geodesist(astronautName);

            }
            else if (type == "Meteorologist")
            {
                astro = new Meteorologist(astronautName);

            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
            astronauts.Add(astro);
            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet;
            planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planets.Add(planet);
            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            List<IAstronaut> astuts;
            astuts = astronauts.Models.Where(x => x.Oxygen > 60).ToList();
            if (astuts.Count<1)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }
            explored.Add(planet);
            mision.Explore(planet, astuts);
            var deadh = astuts.Where(x => x.Oxygen <= 0).ToList();
            return String.Format(OutputMessages.PlanetExplored, planetName, deadh.Count);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{explored.Count} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astro in astronauts.Models)
            {
                sb.AppendLine($"Name: {astro.Name}");
                sb.AppendLine($"Oxygen: {astro.Oxygen}");
                if (astro.Bag.Items.Count > 0)
                {
                    sb.AppendLine($"Bag items: {String.Join(", ", astro.Bag.Items.ToList())}");
                }
                else
                {
                    sb.AppendLine($"Bag items: none");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut asro = astronauts.FindByName(astronautName);
            if (asro == null)
            {
                throw new InvalidOperationException($"Astronaut { astronautName } doesn't exists!");
            }
            else if (asro != null)
            {
                astronauts.Remove(asro);
            }
            return String.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
