using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PremiumCalculator.Command;
using PremiumCalculator.Controllers;
using PremiumCalculator.Handlers.CommandResults;
using PremiumCalculator.Interfaces.ICommandHandlers;
using PremiumCalculator.Models;
using PremiumCalculator.Responses.CommandResponse;

namespace PremiumCalculatorTest.Controllers
{
    [TestFixture]
    public class GeneratePremiumControllerTest
    {
        private Mock<ILogger<GeneratePremiumController>> _logger;
        private Mock<ICommandHandler<GeneratePremiumCommand, GeneratePremiumCommandResponse>> _commandHandler;
        private GeneratePremiumController _generatePremiumController;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<GeneratePremiumController>>();
            _commandHandler = new Mock<ICommandHandler<GeneratePremiumCommand, GeneratePremiumCommandResponse>>();
           
            var response = new GeneratePremiumCommandResponse { DeathPremium = 100, ResponseDateTime = DateTime.Now.AddMinutes(-1) };
            var generatePremiumCommandResult = new GeneratePremiumCommandResult
            {
                isSuccessful = true,
                ErrorCodes = null,
                 GeneratePremiumCommandResponse = response
            };
            _commandHandler.Setup(handler => handler.ExecuteCommand(It.IsAny<GeneratePremiumCommand>())).
                Returns(generatePremiumCommandResult);
            _generatePremiumController = new GeneratePremiumController(_logger.Object, _commandHandler.Object);
        }

        [Test]
        public void should_return_correct_value()
        {
            var usersInfo = new ProspectiveUserInfo { Age = 25, DateofBirth = DateTime.Now.AddYears(-25).ToShortDateString(), DeathSumInsured = 10000, Name = "John Williams" };
            var rate = _generatePremiumController.GeneratePremiumRate(usersInfo);
            Assert.AreEqual(rate.Premium, 100);

        }

        [Test]
        public void should_return_incorrect_value()
        {
            var usersInfo = new ProspectiveUserInfo { Age = 25, DateofBirth = DateTime.Now.AddYears(-25).ToShortDateString(), DeathSumInsured = 10000, Name = "John Williams" };
            var rate = _generatePremiumController.GeneratePremiumRate(usersInfo);
            Assert.AreNotEqual(rate.Premium, 200);
        }


        [Test]
        public void should_return_null_result()
        {
            var usersInfo = new ProspectiveUserInfo { Age = 25, DateofBirth = DateTime.Now.AddYears(-25).ToShortDateString(), DeathSumInsured = 10000, Name = "John Williams" };
            _commandHandler.Setup(handler => handler.ExecuteCommand(It.IsAny<GeneratePremiumCommand>()))
                         .Returns(default(GeneratePremiumCommandResult));
            var rate = _generatePremiumController.GeneratePremiumRate(usersInfo);
            Assert.AreEqual(rate.Premium, 0);
        }

        [Test]
        public void should_return_unsuccessful_result()
        {
            var generatePremiumCommandResult = new GeneratePremiumCommandResult();
            var usersInfo = new ProspectiveUserInfo { Age = 25, DateofBirth = DateTime.Now.AddYears(-25).ToShortDateString(), DeathSumInsured = 10000, Name = "John Williams" };
            _commandHandler.Setup(handler => handler.ExecuteCommand(It.IsAny<GeneratePremiumCommand>()))
                         .Returns(generatePremiumCommandResult);
            var rate = _generatePremiumController.GeneratePremiumRate(usersInfo);
            Assert.AreEqual(rate.Premium, 0);
        }

        [Test]
        public void should_return_empty_result_null_object()
        {
            var rate = _generatePremiumController.GeneratePremiumRate(null);
            Assert.AreEqual(rate.Premium, 0);
        }
    }
}
