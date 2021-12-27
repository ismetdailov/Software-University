using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> eggs;
        public EggRepository()
        {
            this.eggs = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models => eggs;

        public void Add(IEgg model)
        {
            eggs.Add(model);
        }

        public IEgg FindByName(string name)
        {
            var egg = eggs.FirstOrDefault(x => x.Name == name);
            if (egg == null)
            {
                return null;
            }
            return egg;
        }

        public bool Remove(IEgg model)
        {
            var eggtrue = eggs.Remove(model);
            if (eggtrue == true)
            {
                return true;
            }
            return false;
        }
    }
}
