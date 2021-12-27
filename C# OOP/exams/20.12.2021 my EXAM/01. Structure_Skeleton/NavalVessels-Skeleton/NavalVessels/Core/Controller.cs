using NavalVessels.Core.Contracts;
using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vesselRepository;
        private List<ICaptain> captains;
        public Controller()
        {
            this.vesselRepository = new VesselRepository();
            this.captains = new List<ICaptain>();
        }
        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var capitan = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);
            var vessel = vesselRepository.FindByName(selectedVesselName);
            if (capitan == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }
            if (vessel.Captain!= null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }
            vessel.Captain = capitan;
            capitan.AddVessel(vessel);
            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var atack = vesselRepository.FindByName(attackingVesselName);
            var defend = vesselRepository.FindByName(defendingVesselName);
            if (atack==null )
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }
           else if ( defend == null)
            {
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }
            if (atack.ArmorThickness==0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            else if (defend.ArmorThickness==0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);

            }
            atack.Attack(defend);
            atack.Captain.IncreaseCombatExperience();
            defend.Captain.IncreaseCombatExperience();
            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defend.ArmorThickness);
        }

        public string CaptainReport(string captainFullName)
        {
            var vesel= vesselRepository.Models.Where(x => x.Captain.FullName == captainFullName);
            var capitan = captains.FirstOrDefault(x => x.FullName == captainFullName);
          return  capitan.Report();
            
        }

        public string HireCaptain(string fullName)
        {
           var capitan= captains.FirstOrDefault(x => x.FullName == fullName);
            if (capitan!=null)
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }
            capitan = new Captain(fullName);
            captains.Add(capitan);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
            throw new NotImplementedException();
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
           var vesselForAdd= vesselRepository.FindByName(name);
            if (vesselForAdd!=null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            }
            if (vesselType == "Submarine")
            {
                vesselForAdd = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vesselForAdd = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return string.Format(OutputMessages.InvalidVesselType);
            }
            vesselRepository.Add(vesselForAdd);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string ServiceVessel(string vesselName)
        {
            var vesel = vesselRepository.FindByName(vesselName);
            if (vesel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            vesel.RepairVessel();
            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var vessel = vesselRepository.FindByName(vesselName);
            if (vessel.GetType().Name == "Battleship")
            {
                IBattleship vessel1 =(IBattleship)vesselRepository.FindByName(vesselName);
                vessel1.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);

            }
            if (vessel.GetType().Name == "Submarine")
            {
                IBattleship vessel1 = (IBattleship)vesselRepository.FindByName(vesselName);
                vessel1.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode,vesselName);
            }
            
                return string.Format(OutputMessages.VesselNotFound, vesselName);
        }

        public string VesselReport(string vesselName)
        {
            var vesel = vesselRepository.FindByName(vesselName);
         return  vesel.ToString();
        }
    }
}
