using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
       private List<IDecoration> decorations;
       private List<IFish> fish;
        private string name;
        public Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            decorations = new List<IDecoration>();
            fish = new List<IFish>();
        }
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishName);
                }
                name = value;
            }
        }
        public int Capacity { get; }
        public int Comfort => Decorations.Sum(x => x.Comfort);
        public ICollection<IDecoration> Decorations => this.decorations;

        public ICollection<IFish> Fish => fish;

        public void AddFish(IFish fish)
        {
            if (Fish.Count == Capacity )
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            Fish.Add(fish);

        }

        public bool RemoveFish(IFish fish) => Fish.Remove(fish);


        public void AddDecoration(IDecoration decoration) => Decorations.Add(decoration);


        public void Feed() => fish.ForEach(x => x.Eat());
        

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} ({Name}):");
            sb.AppendLine($"Fish: {(Fish.Any() ? String.Join(" ", GetFishNames()) : "none")}");
            sb.AppendLine($"Comfort: {Comfort}");
            return sb.ToString().TrimEnd();
        }
        private List<string> GetFishNames()
        {
            List<string> list = new List<string>();

            foreach (var fish in Fish)
            {
                list.Add(fish.Name);
            }

            return list;
        }
    }
}
