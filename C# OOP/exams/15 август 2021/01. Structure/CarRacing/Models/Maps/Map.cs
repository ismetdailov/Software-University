using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {

        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                racerOne.Race();
                racerTwo.Race();
                double racerOneWin = ChanceOFWinnig(racerOne);
                double racerTwoWin = ChanceOFWinnig(racerTwo);
                if (racerOneWin> racerTwoWin)
                {
                    return $"{racerOne.Username} has just raced against {racerTwo.Username}! {racerOne.Username} is the winner!";
                }
                else
                {
                    return $"{racerOne.Username} has just raced against {racerTwo.Username}! {racerTwo.Username} is the winner!";

                }
            }
            else if (racerOne.IsAvailable())
            {
                return $"{racerOne.Username} wins the race! {racerTwo.Username} was not available to race!";
            }
            else if (racerTwo.IsAvailable())
            {
                return $"{racerTwo.Username} wins the race! {racerOne.Username} was not available to race!";
                
            }
            else
            {
                return "Race cannot be completed because both racers are not available!";
            }
             double ChanceOFWinnig(IRacer racer)
            {
                double chanceOfWinn=0.0;

                if (racer.RacingBehavior == "strict")
                {
                    chanceOfWinn = racer.Car.HorsePower * racer.DrivingExperience * 1.2;
                }
                else if (racer.RacingBehavior == "aggressive")
                {
                chanceOfWinn = racer.Car.HorsePower * racer.DrivingExperience * 1.1;
                }
                 return chanceOfWinn;

            }
        }
            
        }
    }

