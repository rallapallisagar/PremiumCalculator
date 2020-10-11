using PremiumCalculator.Interfaces.CommandResults;
using PremiumCalculator.Responses.CommandResponse;
using System.Collections.Generic;

namespace PremiumCalculator.Handlers.CommandResults
{
    public class GeneratePremiumCommandResult:ICommandResult
    {
        public bool isSuccessful { get; set; }

        public IList<string> ErrorCodes { get; set; }

        public GeneratePremiumCommandResponse GeneratePremiumCommandResponse { get; set; }
    }
}
