using Gym.Core.Contracts;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private readonly EquipmentRepository equipmentRepository;
        private List<IGym> gyms;
        public Controller()
        {
            this.equipmentRepository = new EquipmentRepository();
            this.gyms = new List<IGym>();

        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete;
            var gygy = gyms.FirstOrDefault(x => x.Name == gymName);

            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);

            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
            if (athleteType == "Boxer" && gygy.GetType().Name != "BoxingGym")
            {
                return string.Format(OutputMessages.InappropriateGym);
            }
            else if (athleteType == "Weightlifter" && gygy.GetType().Name != "WeightliftingGym")
            {
                return string.Format(OutputMessages.InappropriateGym);

            }
            else
            {
                gygy.AddAthlete(athlete);
                return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
            }

           
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment;
            if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }
            equipmentRepository.Add(equipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gymForAdd;
            if (gymType == "BoxingGym")
            {
                gymForAdd = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gymForAdd = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }
            gyms.Add(gymForAdd);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = gyms.FirstOrDefault(x => x.Name == gymName);
            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight.ToString("F2"));
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {

            var enqi = equipmentRepository.FindByType(equipmentType);
            var gy = gyms.FirstOrDefault(x => x.Name == gymName);
            if (enqi == null)
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }
            gy.Equipment.Add(enqi);
            gyms.Add(gy);
            equipmentRepository.Remove(enqi);
            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            foreach (var gym in gyms.Distinct())
            {
                sb.AppendLine(gym.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            var gym = gyms.FirstOrDefault(x => x.Name == gymName);
            foreach (var atlet in gym.Athletes)
            {
                atlet.Exercise();
            }
            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }
    }
}
