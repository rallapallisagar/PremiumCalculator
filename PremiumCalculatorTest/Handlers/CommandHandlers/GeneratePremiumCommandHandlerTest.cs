using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PremiumCalculator.Command;
using PremiumCalculator.Handlers.CommandHandlers;
using PremiumCalculator.Handlers.CommandResults;
using PremiumCalculator.Services;

namespace PremiumCalculatorTest.Handlers.CommandHandlers
{
    [TestFixture]

    public class GeneratePremiumCommandHandlerTest
    {
        private Mock<ILogger<GeneratePremiumCommandHandler>> _logger;
        private Mock<IPremiumCalculatorService> _premiumCalculatorService;
        private GeneratePremiumCommandHandler _handler;


        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<GeneratePremiumCommandHandler>>();
            _premiumCalculatorService = new Mock<IPremiumCalculatorService>();
            _handler = new GeneratePremiumCommandHandler(_logger.Object, _premiumCalculatorService.Object);
            _premiumCalculatorService.Setup(service => service.GetDeathPremiumAmount(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<string>())).
                Returns(25);

        }

        [Test]
        public void should_returns_success_result()
        {
            GeneratePremiumCommandResult response = (GeneratePremiumCommandResult)_handler.ExecuteCommand(new  GeneratePremiumCommand { Age = 21, DeathCoverAmount = 1000, OccupationalRating = "Doctor", RequestDateStamp = DateTime.Now });
            Assert.IsTrue(response.isSuccessful);
            Assert.IsNull(response.ErrorCodes);
            Assert.AreEqual(response.GeneratePremiumCommandResponse.DeathPremium, 25);
        }
        [Test]
        public void should_returns_failure_for_null_command()
        {
            GeneratePremiumCommandResult response = (GeneratePremiumCommandResult)_handler.ExecuteCommand(null);
            Assert.IsFalse(response.isSuccessful);
            Assert.IsNull(response.ErrorCodes);
            Assert.AreEqual(response.GeneratePremiumCommandResponse.DeathPremium, 0);
        }
        [Test]
        public void should_throw_exception()
        {
            _premiumCalculatorService.Setup(service => service.GetDeathPremiumAmount(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<string>())).
                  Throws(new Exception());
            GeneratePremiumCommandResult response = (GeneratePremiumCommandResult)_handler.ExecuteCommand(new GeneratePremiumCommand { Age = 21, DeathCoverAmount = 1000, OccupationalRating = "Farmer", RequestDateStamp = DateTime.Now });
            Assert.IsFalse(response.isSuccessful);
            Assert.IsNull(response.GeneratePremiumCommandResponse);
        }
    }
}
