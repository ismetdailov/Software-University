using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models.Contracts
{
    public class Submarine : Vessel, ISubmarine
    {
        private bool submergeMode;
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 200)
        {
            this.submergeMode = false;
        }

        public bool SubmergeMode { get; }

        public void ToggleSubmergeMode()
        {
            if (submergeMode == false)
            {
                submergeMode = true;
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else if (submergeMode == true)
            {
                submergeMode = false;
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }
        public override void RepairVessel()
        {
            base.RepairVessel();
            ArmorThickness = 200;
        }
        public override string ToString()
        {
            var str = "";
            if (submergeMode == false)
            {

               str="*Submerge mode: OFF";
            }
            else if (submergeMode == true)
            {
                str= "*Submerge mode: ON";

            }
           // return base.ToString();
            return base.ToString() +Environment.NewLine+ str;
        }
    }
}
