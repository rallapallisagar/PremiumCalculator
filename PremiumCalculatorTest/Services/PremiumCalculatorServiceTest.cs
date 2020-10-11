using NUnit.Framework;
using PremiumCalculator.Services;

namespace PremiumCalculatorTest.Services
{
    [TestFixture]
    public class PremiumCalculatorServiceTest
    {
        private PremiumCalculatorService _premiumCalculatorService;
        [SetUp]
        public void SetUp()
        {
            _premiumCalculatorService = new PremiumCalculatorService();
        }

        [Test]

        public void should_return_correct_premium_rate_for_professionals()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000,"doctoR");
            Assert.AreEqual(calculatedRate, 1.75);
        }

        [Test]
        public void should_return_incorrect_premium_rate_for_professionals()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 10000, "DOCTOR");
            Assert.AreNotEqual(calculatedRate, 1.75);
        }

        [Test]
        public void should_return_zero_premium_rate_for_professionals()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(0, 10000, "doctor");
            Assert.AreEqual(calculatedRate, 0);
        }

        [Test]
        public void should_return_correct_premium_rate_for_whitecollar()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "auTHor");
            Assert.AreEqual(calculatedRate, 2.19);
        }

        [Test]
        public void should_return_incorrect_premium_rate_for_whitecollar()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "Author");
            Assert.AreNotEqual(calculatedRate, 2.17);
        }

        [Test]
        public void should_return_correct_premium_rate_for_lightmanual()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "Florist");
            Assert.AreEqual(calculatedRate, 2.62);
        }

        [Test]
        public void should_return_incorrect_premium_rate_for_lightmanual()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "CLeaner");
            Assert.AreNotEqual(calculatedRate, 2.65);
        }

        [Test]
        public void should_return_correct_premium_rate_for_heavymanual()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "Mechanic");
            Assert.AreEqual(calculatedRate, 3.06);
        }

        [Test]
        public void should_return_incorrect_premium_rate_for_heavymanual()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "Farmer");
            Assert.AreNotEqual(calculatedRate, 3.10);
        }

        [Test]
        public void should_return_zero_premium_rate_for_unknown_professions()
        {
            var calculatedRate = _premiumCalculatorService.GetDeathPremiumAmount(21, 1000, "developer");
            Assert.AreEqual(calculatedRate, 0);
        }
    }
}
