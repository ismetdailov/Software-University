using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    { //todo
        private List<IBunny> bunnies;
        public BunnyRepository()
        {
            this.bunnies = new List<IBunny>();
        }
        public IReadOnlyCollection<IBunny> Models =>this.bunnies;

        public void Add(IBunny model)
        {
            bunnies.Add(model);
        }

        public IBunny FindByName(string name)
        {
            var bunn = bunnies.FirstOrDefault(x => x.Name == name);
            if (bunn == null)
            {
                return null;
            }
            return bunn;
        }

        public bool Remove(IBunny model)
        {
            var bunn =bunnies.Remove(model);
            if (bunn == true)
            {
                return true;
            }
            return false;
        }
    }
}
