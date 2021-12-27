using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private List<string> targets;
        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            this.Name = name;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armorThickness;
            this.targets = new List<string>();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }
                name = value;
            }
        }

        private double armorThickness;

        public double ArmorThickness
        {
            get { return armorThickness; }
            set
            {
                armorThickness = value;
            }
        }
        private ICaptain captain;

        public ICaptain Captain
        {
            get { return captain; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
                }
                captain = value;
            }
        }

        private double mainWeaponCaliber;

        public double MainWeaponCaliber
        {
            get { return mainWeaponCaliber; }
            set { mainWeaponCaliber = value; }
        }



        public double Speed { get; set; }

        public ICollection<string> Targets => this.targets;

        public void Attack(IVessel target)
        {

           
                if (target==null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidTarget);
                }
                 target.ArmorThickness -= MainWeaponCaliber;
                if (target.ArmorThickness<0)
                {
                  target.ArmorThickness = 0;
                }
                target.Targets.Add(target.Name);
            targets.Add(target.Name);
            

            
        }

        public virtual void RepairVessel()
        {

        }
        public override string ToString()
        {
            var sb = new StringBuilder();
          
            sb.AppendLine($"- {name}");
            sb.AppendLine($"*Type: {GetType().Name}");
            sb.AppendLine($"*Armor thickness: {armorThickness}");
            sb.AppendLine($"*Main weapon caliber: {mainWeaponCaliber}");
            sb.AppendLine($"*Speed: {Speed} knots");
            sb.AppendLine($"*Targets: {(targets.Count>0 ? string.Join(", ",targets.ToArray()): "None")}"+Environment.NewLine+"");
            sb.AppendLine(" ");

            return sb.ToString().TrimEnd();
        }
    }
}
