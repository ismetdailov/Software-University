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
        private List<IEgg> egg;
        public EggRepository()
        {
            egg = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models => this.egg;

        public void Add(IEgg model)
        {
            egg.Add(model);
        }

        public IEgg FindByName(string name)
        {
            IEgg eggs = egg.FirstOrDefault(e => e.Name == e.Name);
            if (eggs!= null)
            {
                return eggs;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IEgg model)
        {
            if (egg.Remove(model))
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
