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
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.RaceCannotBeCompleted);
                
            }
            else if (!racerOne.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            else if (!racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable,  racerOne.Username, racerTwo.Username);

            }
            racerOne.Race();
            racerTwo.Race();
            var racerOnebehai = 0.0;
            var racerTwobehai = 0.0;
            if (racerOne.RacingBehavior== "strict")
            {
                racerOnebehai = 1.2;
            }
             if(racerOne.RacingBehavior == "aggressive")
            {
                racerOnebehai = 1.1;
            }
            if (racerTwo.RacingBehavior == "strict")
            {
                racerTwobehai = 1.2;
            }
          if (racerTwo.RacingBehavior == "aggressive")
            {
                racerTwobehai = 1.1;
            }
            var racOne = racerOne.Car.HorsePower * racerOne.DrivingExperience * racerOnebehai;
            var racTwo = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * racerTwobehai;
          
            if (racOne>racTwo)
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
