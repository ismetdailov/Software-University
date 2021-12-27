using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (!egg.IsDone())
            {
                if (bunny.Energy == 0)
                {
                    break;
                }

                if (bunny.Dyes.All(x => x.IsFinished()))
                {
                    break;
                }
                egg.GetColored();
                bunny.Work();
            }
                //if (bunny.Energy>0|| bunny.Dyes.Count>0)
                //{
                //    while (egg.IsDone() && bunny.Energy>0 && bunny.Dyes.Count>0 )
                //    {
                //        foreach (var dyee in bunny.Dyes)
                //        {
                //            if (dyee.IsFinished())
                //            {
                //                continue;
                //            }
                //          egg.GetColored();

                //        }

                //    }
                //}
            }
    }
}
