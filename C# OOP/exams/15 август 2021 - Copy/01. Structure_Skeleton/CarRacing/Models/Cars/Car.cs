using CarRacing.Models.Cars.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        public Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            this.Make = make;
            this.Model = model;
            this.VIN = VIN;
            this.HorsePower = horsePower;
            this.FuelAvailable = fuelAvailable;
            this.FuelConsumptionPerRace = fuelConsumptionPerRace;
        }
        private string make;

        public string Make
        {
            get 
            {
                return make; 
            }
           private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarMake);
                }
                make = value; 
            }
        }
        private string model;

        public string Model
        {
            get
            {
                return model;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarModel);
                }
                model = value;
            }
        }
        private string vin;

        public string VIN
        {
            get
            {
                return vin;
            }
            private set
            {
                char[] arr = new char[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    arr[i] = value[i];
                }

                if (value.Length<17|| value.Length>17)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarVIN);
                }
                vin = value;
            }
        }
        private int horsePower;

        public int HorsePower
        {
            get 
            {
                return horsePower;
            }
           protected set 
            {
                horsePower = value;
                if (horsePower<0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarHorsePower);

                }
            }
        }
        private double fuelAvailable;

        public double FuelAvailable
        {
            get
            {
                return fuelAvailable;
            }
            protected set
            {
                fuelAvailable = value;
                if (fuelAvailable < 0)
                {
                    this.fuelAvailable = 0;
                }
            }
        }
        private double fuelConsumtpionPerRace ;

        public double FuelConsumptionPerRace
        {
            get
            {
                return fuelConsumtpionPerRace;
            }
            private set
            {
                if (fuelConsumtpionPerRace < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarFuelConsumption);
                }
                fuelConsumtpionPerRace = value;
            }
        }

        //public double FuelConsumptionPerRace => throw new NotImplementedException();

        public abstract void Drive();
        
    }
}
