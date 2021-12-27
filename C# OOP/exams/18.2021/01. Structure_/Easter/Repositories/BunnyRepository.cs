using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private List<IBunny> bunnies;
        public BunnyRepository()
        {
            this.bunnies = new List<IBunny>();
        }
        public IReadOnlyCollection<IBunny> Models => this.bunnies;

        public void Add(IBunny model)
        {
            bunnies.Add(model);
        }

        public IBunny FindByName(string name)
        {
            IBunny bun = bunnies.FirstOrDefault(b => b.Name == b.Name);
            if (bun!= null)
            {
                return bun;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IBunny model)
        {
            if (bunnies.Remove(model))
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
