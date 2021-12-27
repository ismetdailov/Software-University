using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Athletes.Contracts
{
    public class Weightlifter : Athlete
    {
        public Weightlifter(string fullName, string motivation, int numberOfMedals) : base(fullName, motivation, numberOfMedals, 50)
        {
        }
        public override void Exercise()
        {
            Stamina += 10;
            base.Exercise();
        }
    }
}
