using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var astro in astronauts)
            {
                while (astro.Oxygen>0&& planet.Items.Count>0)
                {
                    foreach (var item in planet.Items.ToList())
                    {
                        astro.Breath();
                        astro.Bag.Items.Add(item);
                        planet.Items.Remove(item);
                        if (!astro.CanBreath)
                        {
                            break;
                        }
                       
                    }
                   
                }
            }
        }
    }
}
