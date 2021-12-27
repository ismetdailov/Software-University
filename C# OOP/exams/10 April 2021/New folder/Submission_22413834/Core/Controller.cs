using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private  List<IDecoration> decorations;
        private List<IAquarium> aquariums;
        public Controller()
        {
            decorations = new List<IDecoration>();
            aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium = null;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium( aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
            aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decorat = null;
            if (decorationType == "Ornament")
            {
                decorat = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decorat = new Plant();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }
            decorations.Add(decorat);
            return String.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish desiredFish = null;
            IAquarium desirAquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            if (fishType == "FreshwaterFish")
            {
                desiredFish = new FreshwaterFish(fishName, fishSpecies, price);
                if (desirAquarium.GetType().Name != nameof(FreshwaterAquarium))
                {
                    return OutputMessages.UnsuitableWater;
                }
            }
            else if (fishType == "SaltwaterFish")
            {
                desiredFish = new SaltwaterFish(fishName, fishSpecies, price);
                if (desirAquarium.GetType().Name != nameof(SaltwaterAquarium))
                {
                    return OutputMessages.UnsuitableWater;
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }
            desirAquarium.AddFish(desiredFish);
            return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            decimal sumOfDecoration = aquarium.Decorations.Sum(x =>x.Price);
            decimal sumOfFishes = aquarium.Fish.Sum(x => x.Price);
            decimal totalPrice = sumOfDecoration + sumOfFishes;
            return string.Format(OutputMessages.AquariumValue, aquariumName, totalPrice);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            aquarium.Feed();
            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium desireaquar = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            IDecoration decor = decorations.FirstOrDefault(x=>x.GetType().Name == decorationType);
            if (decor is null)
            {
                throw new InvalidOperationException(String.Format( ExceptionMessages.InexistentDecoration, decorationType));
            }
            decorations.Add(decor);
            desireaquar.AddDecoration(decor);
            decorations.Remove(decor);
            return String.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IAquarium aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
