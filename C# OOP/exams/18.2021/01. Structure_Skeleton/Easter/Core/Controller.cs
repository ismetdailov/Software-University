using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
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
        private BunnyRepository bunnies;
        private EggRepository eggs;
        private IWorkshop workshop;
        private List<IBunny> bunnyesReady;
        private bool flag;
        public Controller()
        {
            this.bunnies = new BunnyRepository();
            this.eggs = new EggRepository();
            this.workshop = new Workshop();
            this.bunnyesReady = new List<IBunny>();
            this.flag = false;
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }
            bunnies.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bun = bunnies.Models.FirstOrDefault(x => x.Name == bunnyName);

            if (bun == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            var dye = new Dye(power);
            bun.Dyes.Add(dye);
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            IWorkshop workshop = new Workshop();
            List<IBunny> suitableBunnies =
                bunnies.Models.Where(x => x.Energy >= 50).OrderByDescending(x => x.Energy).ToList();

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
            //if (flag == false)
            //{
            //    this.bunnyesReady = bunnies.Models.Where(x => x.Energy >= 50).OrderByDescending(c => c.Energy).ToList();
            //}

            //flag = true;
            //var eggForColored = eggs.Models.FirstOrDefault(x => x.Name == eggName);
            //if (bunnyesReady.Count == 0)
            //{
            //    throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            //}
            //foreach (var bun in bunnyesReady)
            //{
            //    if (eggForColored.IsDone())
            //    {
            //        break;
            //    }
            //    workshop.Color(eggForColored, bun);
            //    if (bun.Energy <= 0)
            //    {
            //        bunnies.Remove(bun);
            //    }
            //}

            //return $"Egg {eggName} is {(eggForColored.IsDone() ? "done" : "not done")}.";
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{eggs.Models.Count} eggs are done!");
            sb.AppendLine($"Bunnies info:");
            foreach (var bun in bunnies.Models)
            {
                sb.AppendLine($"Name: {bun.Name}");
                sb.AppendLine($"Energy: {bun.Energy}");
                sb.AppendLine($"Dyes: {bun.Dyes.Count(x => !x.IsFinished())} not finished");
               // sb.AppendLine($"Dyes: {bun.Dyes.Where(x => x.Power > 0).ToList().Count} not finished");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
