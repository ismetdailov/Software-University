using System;
using System.Collections.Generic;
using System.Linq;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        public BunnyRepository()
        {
            this.bunnies = new List<IBunny>();
        }

        private List<IBunny> bunnies;

        public IReadOnlyCollection<IBunny> Models => this.bunnies;

        public void Add(IBunny model)
        {
            this.bunnies.Add(model);
        }

        public IBunny FindByName(string name)
        {
            IBunny bunnyToReturn = this.bunnies.FirstOrDefault(x => x.Name == name);
            return bunnyToReturn;
        }

        public bool Remove(IBunny model)
        {
            if(this.bunnies.Remove(model))

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
