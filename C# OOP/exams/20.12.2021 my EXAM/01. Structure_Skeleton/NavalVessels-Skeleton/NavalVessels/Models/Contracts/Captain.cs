using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Models.Contracts
{
    public class Captain : ICaptain
    {
        private string fullName;
        private List<IVessel> vessels;
        public Captain(string fullName )
        {
            this.FullName = fullName;
            this.vessels = new List<IVessel>();
            this.CombatExperience = combatExperience;
        }

        public string FullName
        {
            get { return fullName; }
           private set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                }
                fullName = value;
            }
        }

        private int combatExperience=0;
        public int CombatExperience
        {
            get { return combatExperience; }
           private set 
            {
                this.combatExperience +=value;
            }
        }


        public ICollection<IVessel> Vessels => this.vessels;

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
            }
            vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            //todo
            CombatExperience += 10;
          
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {vessels.Count} vessels.");
            var capi = vessels.FirstOrDefault(x => x.Captain.FullName == fullName);
            if (capi!=null)
            {
              return sb.ToString().TrimEnd() +  capi.ToString();
            }
            return sb.ToString().TrimEnd();
        }
    }
}
