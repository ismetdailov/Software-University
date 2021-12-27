using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums.Contracts
{
    public abstract class Aquarium : IAquarium
    {
        private readonly List<IDecoration> listDecoration;
        private readonly List<IFish> listOfFish;
        private string name;
        private int capacity;
        public Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            listDecoration = new List<IDecoration>();
            listOfFish = new List<IFish>();
        }
        public string Name
        {
            get
            {
                return this.name;
            }
          private  set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Fish name cannot be null or empty.");
                }
                this.name = value;
            }
        }
        public int Capacity { get; }

        public int Comfort => Decorations.Sum(x => x.Comfort);

        public ICollection<IDecoration> Decorations => this.listDecoration;

        public ICollection<IFish> Fish => this.listOfFish;

        public void AddDecoration(IDecoration decoration) => Decorations.Add(decoration);

        public void AddFish(IFish fish)
        {
            if (Fish.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            Fish.Add(fish);
        }

        public void Feed()
        {
            foreach (var item in this.listOfFish)
            {
                item.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} ({Name}):");
            sb.AppendLine($"Fish: {(Fish.Any() ? String.Join(" ", GetFishNames()) : "none")}");
            sb.AppendLine($"Comfort: {Comfort}");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish) => Fish.Remove(fish);
        private List<string> GetFishNames()
        {
            List<string> list = new List<string>();
            foreach (var item in Fish)
            {
                list.Add(item.Name);
            }
            return list;
        }
    }
}
