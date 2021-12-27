using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public class Planet : IPlanet
    {
        private List<string> items;
        public Planet(string name)
        {
            this.items = new List<string>();
            this.Name = name;
        }
        private string name;

        public string Name
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        public ICollection<string> Items =>this.items;
    }
}
