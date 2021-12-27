using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public  class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (bunny.Dyes.Count>0)
            {
                var dyeTouse = bunny.Dyes.First();
                while (dyeTouse.IsFinished())
                {
                    if (egg.IsDone()|| bunny.Energy<=0)
                    {
                        return;
                    }
                    egg.GetColored();
                    bunny.Work();
                    dyeTouse.Use();
                }
                bunny.Dyes.Remove(dyeTouse);
            }
        }
    }
}
