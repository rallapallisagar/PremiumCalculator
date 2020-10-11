
namespace PremiumCalculator.Services
{
    public interface IPremiumCalculatorService
    {
        double  GetDeathPremiumAmount(int Age,double DeathCoverAmount,string occupationalRating);
    }
}
