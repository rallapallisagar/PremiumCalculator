using Microsoft.Extensions.Logging;
using PremiumCalculator.Command;
using PremiumCalculator.Handlers.CommandResults;
using PremiumCalculator.Interfaces.CommandResults;
using PremiumCalculator.Responses.CommandResponse;
using PremiumCalculator.Services;
using System;
using PremiumCalculator.Interfaces.ICommandHandlers;

namespace PremiumCalculator.Handlers.CommandHandlers
{
    public class GeneratePremiumCommandHandler : ICommandHandler<GeneratePremiumCommand, GeneratePremiumCommandResponse>
    {
        private readonly ILogger<GeneratePremiumCommandHandler> _logger;
        private readonly IPremiumCalculatorService _premiumCalculatorService;

        public GeneratePremiumCommandHandler(ILogger<GeneratePremiumCommandHandler> logger, IPremiumCalculatorService premiumCalculatorService)
        {
            _logger = logger;
            _premiumCalculatorService = premiumCalculatorService;

        }
        public ICommandResult ExecuteCommand(GeneratePremiumCommand command)
        {
            var commandResult = new GeneratePremiumCommandResult();
            var response = new GeneratePremiumCommandResponse();
            try
            {
                if (command == null)
                {
                    commandResult.GeneratePremiumCommandResponse = response;
                    return commandResult;
                }
                response.DeathPremium = _premiumCalculatorService.GetDeathPremiumAmount(command.Age, command.DeathCoverAmount, command.OccupationalRating);
                commandResult.GeneratePremiumCommandResponse = response;
                commandResult.isSuccessful = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Not able to retrieve premium details", ex.Message);

            }

            return commandResult;
        }
    }
}
