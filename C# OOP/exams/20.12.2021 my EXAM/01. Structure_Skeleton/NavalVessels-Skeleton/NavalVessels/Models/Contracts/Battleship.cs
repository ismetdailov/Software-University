using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models.Contracts
{
    public class Battleship : Vessel , IBattleship
    {
        private bool sonarMode;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 300)
        {
            sonarMode = false;
        }

        public bool SonarMode { get; }

        bool IBattleship.SonarMode { get; }

        public override void RepairVessel()
        {
            ArmorThickness = 300;
            base.RepairVessel();
        }
      

       void IBattleship.ToggleSonarMode()
        {
            if (sonarMode == false)
            {
                sonarMode = true;
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else if (sonarMode == true)
            {
                sonarMode = false;
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
            
        }
        public override string ToString()
        {
            var str = "";
            if (sonarMode == false)
            {
                str= "*Sonar mode: OFF";
            }
            else
            {
                str= "*Sonar mode: ON";
            }
           // return base.ToString();
            return base.ToString() + Environment.NewLine + str;

        }
    }
}
