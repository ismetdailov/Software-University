using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
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
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private Mission mission;
        public Controller()
        {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
            this.mission = new Mission();
        }
        int exploredPlanetsCount = 0;

        public string AddAstronaut(string type, string astronautName)
        {
            Astronaut astronaut;
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
            astronauts.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            Planet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planets.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var count = astronauts.Models.Count;
            var missionAstro = astronauts.Models.Where(x => x.Oxygen > 60).ToList();
            if (missionAstro.Count <= 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }
            Planet planet = (Planet)planets.Models.FirstOrDefault(x => x.Name == planetName);
            mission.Explore(planet, missionAstro);
            var cocount = astronauts.Models.Where(x => x.Oxygen == 0).ToList();
            count -= astronauts.Models.Count;
            exploredPlanetsCount++;
            return string.Format(OutputMessages.PlanetExplored, planetName, cocount.Count);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanetsCount} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astro in astronauts.Models)
            {
                sb.AppendLine($"Name: {astro.Name}");
                sb.AppendLine($"Oxygen: {astro.Oxygen}");
                sb.AppendLine($"Bag items: {(astro.Bag.Items.Count > 0 ? string.Join(", ", astro.Bag.Items) : "none")}");
            }
            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronauts.Models.FirstOrDefault(x=>x.Name==astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException($"Astronaut { astronautName } doesn't exists!");
            }
            astronauts.Remove(astronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
