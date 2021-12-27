using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private List<string> items;
        public Planet(string name)
        {
            this.Name = name;
            this.items = new List<string>();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        public ICollection<string> Items => this.items;

    }
}
