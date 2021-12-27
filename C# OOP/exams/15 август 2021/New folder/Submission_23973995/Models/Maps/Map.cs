using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {

            if (!racerOne.IsAvailable()&& !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.RaceCannotBeCompleted);
            }
            else if (!racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }
            else if (!racerOne.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            racerOne.Car.Drive();
            racerTwo.Car.Drive();
            var behaiv = 0.0;
            if (racerOne.RacingBehavior == "strict")
            {
                behaiv = 1.2;
            }
            else if (racerOne.RacingBehavior == "aggressive")
            {
                behaiv = 1.1;
            }
            var oneRacer = racerOne.Car.HorsePower * racerOne.DrivingExperience * behaiv;
            var behai1 = 0.0;
            if (racerTwo.RacingBehavior == "strict")
            {
                behai1 = 1.2;
            }
            else if (racerTwo.RacingBehavior == "aggressive")
            {
                behai1 = 1.1;
            }
            var twoRacer = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * behai1;
            if (oneRacer>twoRacer)
            {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerOne.Username);
            }
            else 
            {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerTwo.Username);
            }
        }
    }
}
