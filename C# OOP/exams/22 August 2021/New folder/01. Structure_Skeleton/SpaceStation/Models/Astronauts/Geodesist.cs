﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut
    {
        public Geodesist(string name) : base(name, 50)
        {
        }
        public override void Breath()
        {
           // Oxygen -= 10;
            base.Breath();
        }
    }
}
