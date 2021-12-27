using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym :IGym
    {
        private  List<IEquipment> equipments;
        private List<IAthlete> athletes;
        public Gym(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.equipments = new List<IEquipment>();
            this.athletes = new List<IAthlete>();
        }
        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
           private set 
            { 

                capacity = value; 
            }
        }


        public double EquipmentWeight => this.equipments.Sum(x=>x.Weight);

        public ICollection<IEquipment> Equipment => this.equipments;

        public ICollection<IAthlete> Athletes => this.athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (Capacity == athletes.Count)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
            athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            equipments.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var atlet in athletes)
            {
                atlet.Exercise();
            }
        }

        public virtual string GymInfo()
        {
            var sb = new StringBuilder();
            var str = "No athletes";
            sb.AppendLine($"{Name} is a {GetType().Name}:");
            sb.AppendLine($"Athletes: {(athletes.Count > 0 ? string.Join(", ", athletes.Select(x => x.FullName).ToList()): str)}");
            sb.AppendLine($"Equipment total count: {equipments.Count}");
            sb.AppendLine($"Equipment total weight: {EquipmentWeight.ToString("F2")} grams");
            return sb.ToString().TrimEnd();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return athletes.Remove(athlete);
        }
    }
}
