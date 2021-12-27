using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Aquariums.Contracts
{
    public class FreshwaterAquarium : Aquarium
    {
        public FreshwaterAquarium(string name) : base(name, 50)
        {
        }
    }
}
