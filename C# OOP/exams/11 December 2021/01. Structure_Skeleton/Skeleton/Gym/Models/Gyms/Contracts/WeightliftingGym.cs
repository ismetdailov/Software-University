using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Gyms.Contracts
{
    public class WeightliftingGym : Gym
    {
        public WeightliftingGym(string name) : base(name,20)
        {
        }
    }
}
