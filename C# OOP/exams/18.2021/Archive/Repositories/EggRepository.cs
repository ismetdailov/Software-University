using System;
using System.Collections.Generic;
using System.Linq;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        public EggRepository()
        {
            this.eggs = new List<IEgg>();
        }

        private List<IEgg> eggs;

        public IReadOnlyCollection<IEgg> Models => this.eggs;

        public void Add(IEgg model)
        {
            this.eggs.Add(model);
        }

        public IEgg FindByName(string name)
        {
            IEgg eggToReturn = this.eggs.FirstOrDefault(x => x.Name == name);
            return eggToReturn;
        }

        public bool Remove(IEgg model)
        {
            if (this.eggs.Remove(model))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
