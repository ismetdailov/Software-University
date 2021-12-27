using Gym.Models.Athletes.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Athletes
{
    public abstract class Athlete : IAthlete
    {

        private string fullName;

        public Athlete(string fullName, string motivation, int numberOfMedals, int stamina)
        {
            this.FullName = fullName;
            this.Motivation = motivation;
            this.NumberOfMedals = numberOfMedals;
            this.Stamina = stamina;
        }
        public string FullName
        {
            get { return fullName; }
          private  set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteName);
                }
                fullName = value; 
            }
        }

        private string motivation;

        public string Motivation
        {
            get { return motivation; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteMotivation);
                }
                motivation = value;
            }
        }
        private int stamina;

        public int Stamina
        {
            get { return stamina; }
          protected  set 
            {

                stamina = value; 
            }
        }
        private int numberOfMedals;

        public int NumberOfMedals
        {
            get { return numberOfMedals; }
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteMedals);
                }
                numberOfMedals = value;
            }
        }
      
        public virtual void Exercise()
        {
            if (Stamina>100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
