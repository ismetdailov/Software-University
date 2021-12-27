using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private List<string> items;
        public Planet(string name)
        {
            this.name = name;
            this.items = new List<string>();
        }
        public ICollection<string> Items => items;


        private string  name;

        public string  Name
        {
            get { return name; }
          private  set 
            {
                name = value;
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
            }
        }

    }
}
