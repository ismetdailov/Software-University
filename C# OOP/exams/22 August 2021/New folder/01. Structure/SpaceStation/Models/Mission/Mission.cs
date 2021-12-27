using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
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
            
            var bacpack = new Backpack();
            foreach (var astro in astronauts)
            {
                while ((int)astro.Oxygen > 0)
                {
                    if (planet.Items.Count<1)
                    {
                        break;
                    }

                        foreach (var item in planet.Items.ToList())
                        {
                            astro.Breath();
                            astro.Bag.Items.Add(item);
                            planet.Items.Remove(item);
                        if (planet.Items.Count < 1)
                        {
                            break;
                        }
                            if (astro.Oxygen <= 0)
                            {
                                break;
                            }
                        }
                    

                }
            }
        }
    }
}
