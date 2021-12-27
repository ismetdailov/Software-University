using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipments;
        public EquipmentRepository()
        {
            this.equipments = new List<IEquipment>();
        }
        public IReadOnlyCollection<IEquipment> Models => this.equipments;

        public void Add(IEquipment model)
        {
            equipments.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            return equipments.FirstOrDefault(x => x.GetType().Name.ToString() == type);
        }

        public bool Remove(IEquipment model)
        {
            return equipments.Remove(model);
        }
    }
}
