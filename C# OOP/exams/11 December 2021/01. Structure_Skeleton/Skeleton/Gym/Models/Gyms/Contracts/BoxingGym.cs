using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Gyms.Contracts
{
    public class BoxingGym : Gym
    {
        public BoxingGym(string name) : base(name, 15)
        {
        }
    }
}
