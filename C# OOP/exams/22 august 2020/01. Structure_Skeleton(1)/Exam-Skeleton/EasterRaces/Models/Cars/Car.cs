using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars
{
   public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;
        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
        }
        public string Model
        {
            get { return this.model; }
           private set 
            {
                if (string.IsNullOrEmpty(value)|| value.Length<4)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidModel, model);
                }
               this.model = value;
            }
        }

      
        public  int HorsePower 
        {
            get 
            {
                return this.HorsePower;
            }
            private set
            {
                if (value>=400 && value<=600)
                {
                    this.horsePower = value;
                }
                else
                {
                    throw new ArgumentException(ExceptionMessages.InvalidHorsePower, model);
                }
            }
        } 

        public  double CubicCentimeters { get; }


        public double CalculateRacePoints(int laps)
        {
            return this.CubicCentimeters / HorsePower * laps;
        }
    }
}
