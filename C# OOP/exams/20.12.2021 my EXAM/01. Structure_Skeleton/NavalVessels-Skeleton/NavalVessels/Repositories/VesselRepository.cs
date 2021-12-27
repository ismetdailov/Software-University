using NavalVessels.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Models.Contracts
{
    public class VesselRepository : IRepository<IVessel>
    {
        private readonly List<IVessel> vessels;
        public VesselRepository()
        {
            this.vessels = new List<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models =>this.vessels;

        public void Add(IVessel model)
        {
            vessels.Add(model);
        }

        public IVessel FindByName(string name)
        {

            return vessels.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return vessels.Remove(model);
        }
    }
}
