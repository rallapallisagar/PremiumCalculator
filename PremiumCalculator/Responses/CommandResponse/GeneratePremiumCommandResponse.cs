using PremiumCalculator.Interfaces.CommandResponse;
using System;
namespace PremiumCalculator.Responses.CommandResponse
{
    public class GeneratePremiumCommandResponse:ICommandResponse
    {
        public double DeathPremium { get; set; }
        public DateTime ResponseDateTime { get; set; }
    }
}
