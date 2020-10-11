using PremiumCalculator.Interfaces.Command;
using PremiumCalculator.Models;
using System;
namespace PremiumCalculator.Command
{
    public class GeneratePremiumCommand: ICommand
    {
        public int Age { get; set; }

        public double DeathCoverAmount { get; set; }

        public DateTime RequestDateStamp { get; set; }

        public string OccupationalRating { get; set; }

        public string Name { get; set; }

    }
}
