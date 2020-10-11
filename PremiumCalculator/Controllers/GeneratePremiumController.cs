using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PremiumCalculator.Command;
using PremiumCalculator.Handlers.CommandResults;
using PremiumCalculator.Interfaces.ICommandHandlers;
using PremiumCalculator.Models;
using PremiumCalculator.Responses.CommandResponse;

namespace PremiumCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratePremiumController : ControllerBase
    {
        private readonly ILogger<GeneratePremiumController> _logger;
        private readonly ICommandHandler<GeneratePremiumCommand, GeneratePremiumCommandResponse> _commandHandler;
        public GeneratePremiumController(ILogger<GeneratePremiumController> logger, ICommandHandler<GeneratePremiumCommand, GeneratePremiumCommandResponse> commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public PremiumAmountResult GeneratePremiumRate(ProspectiveUserInfo prospectiveUserInfo)
        {
            _logger.LogDebug("Received UserInfo" + prospectiveUserInfo);
            var premiumAmountResult = new PremiumAmountResult();
            
            if (prospectiveUserInfo == null)
                return premiumAmountResult;

            var generatePremiumCommandResult = (GeneratePremiumCommandResult)_commandHandler.ExecuteCommand(new GeneratePremiumCommand { Age = prospectiveUserInfo.Age, Name = prospectiveUserInfo.Name, DeathCoverAmount = prospectiveUserInfo.DeathSumInsured, OccupationalRating = prospectiveUserInfo.Occupation, RequestDateStamp = DateTime.Now });
            double deathPremium;
            if (generatePremiumCommandResult == null || !generatePremiumCommandResult.isSuccessful)
            {
                deathPremium = 0;
            }
            else
            {
                deathPremium = generatePremiumCommandResult.GeneratePremiumCommandResponse != null ?
                               generatePremiumCommandResult.GeneratePremiumCommandResponse.DeathPremium : 0;
            }
            premiumAmountResult.Premium = deathPremium;
            _logger.LogDebug("Calculated Premium Result" + premiumAmountResult);
            return premiumAmountResult;

        }
         
    }
}