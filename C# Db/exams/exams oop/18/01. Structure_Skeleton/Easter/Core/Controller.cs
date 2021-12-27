using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnyRepositories;
        private EggRepository eggRepositories;
    
        public Controller()
        {
            this.bunnyRepositories =new BunnyRepository();
            this.eggRepositories = new EggRepository();
          
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunn=null;
            if (bunnyType == "HappyBunny")
            {
                bunn = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunn = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }
            bunnyRepositories.Add(bunn);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyType);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunnn=bunnyRepositories.FindByName(bunnyName);
         
            if (bunnn == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            var dye = new Dye(power);
            bunnn.AddDye(dye);
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);
            eggRepositories.Add(egg);
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggRepositories.FindByName(eggName);
            IWorkshop workshop = new Workshop();
            List<IBunny> suitableBunnies =
                bunnyRepositories.Models.Where(x => x.Energy >= 50).OrderByDescending(x => x.Energy).ToList();

            if (suitableBunnies.Any() == false)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }
            while (suitableBunnies.Any())
            {
                IBunny currentBunny = suitableBunnies.First();

                while (true)
                {
                    if (currentBunny.Energy == 0 || currentBunny.Dyes.All(x => x.IsFinished()))
                    {
                        suitableBunnies.Remove(currentBunny);
                        break;
                    }

                    workshop.Color(egg, currentBunny);

                    if (egg.IsDone())
                    {
                        break;
                    }
                }

                if (egg.IsDone())
                {
                    break;
                }
            }

            return $"Egg {eggName} is {(egg.IsDone() ? "done" : "not done")}.";
        }

        public string Report()
        {
            var sb = new StringBuilder();
            
        
            sb.AppendLine($"{eggRepositories.Models.Count(x=>x.IsDone())} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach (var item in bunnyRepositories.Models)
            {
                sb.AppendLine($"Name: {item.Name}");
                sb.AppendLine($"Energy: {item.Energy}");
                sb.AppendLine($"Dyes: {item.Dyes.Count} not finished");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
