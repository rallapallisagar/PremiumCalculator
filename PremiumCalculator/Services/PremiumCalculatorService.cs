using PremiumCalculator.Constants;
using PremiumCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PremiumCalculator.Services
{
    public class PremiumCalculatorService : IPremiumCalculatorService
    {
        private static readonly Dictionary<Rating, double> OccupationalRatingFactor = new Dictionary<Rating, double>(); 
        private static readonly List<ProfessionRatings>   ProfessionRatingsList = new List<ProfessionRatings>();

        public double GetDeathPremiumAmount(int age, double deathSumInsured, string occupationalRating)
        {
            if (age <= 0 || deathSumInsured <= 0)
                return 0;

            return CalculatePremium(age, deathSumInsured, occupationalRating); 
        }

        private double CalculatePremium(int age, double deathSumInsured,string occupationalRating)
        {
            var deathPremium = age * deathSumInsured * GetOccupationalRatingFactor(occupationalRating);

            deathPremium /= (PremiumCalculationDataRates.RateFactor * PremiumCalculationDataRates.TotalMonthsInAYear);

            deathPremium = deathPremium > 0 ? Math.Round(deathPremium, PremiumCalculationDataRates.RatePrecision) : 0.00;

            return deathPremium;
        }

        private double GetOccupationalRatingFactor(string occupation)
        { 
            double occupationalRatingFactor = 0;
            if(ProfessionRatingsList.Count==0)
            {
                PopulateProfessionRatings();
            }
            if(OccupationalRatingFactor.Count==0)
            {
                PopulateDictionaryWithFactors();
            }

            var occupationalRating = ProfessionRatingsList.Where(userProfession => userProfession.Profession.Equals(occupation,StringComparison.OrdinalIgnoreCase)).Select(profession => profession.ProfessionRatingFactor).FirstOrDefault();
            if (OccupationalRatingFactor.ContainsKey(occupationalRating))
            {
               occupationalRatingFactor= OccupationalRatingFactor.Where(pair => pair.Key == occupationalRating).Select(pair => pair.Value).FirstOrDefault();
            }

            return occupationalRatingFactor;
        }
        private void PopulateProfessionRatings()
        {
           
            ProfessionRatingsList.Add(new ProfessionRatings { Profession = "Cleaner", ProfessionRatingFactor = Rating.LightManual });
            ProfessionRatingsList.Add(new ProfessionRatings { Profession = "Doctor", ProfessionRatingFactor = Rating.Professional });
            ProfessionRatingsList.Add(new ProfessionRatings { Profession = "Author", ProfessionRatingFactor = Rating.WhiteCollar });
            ProfessionRatingsList.Add(new ProfessionRatings { Profession = "Farmer", ProfessionRatingFactor = Rating.HeavyManual }); 
            ProfessionRatingsList.Add(new ProfessionRatings { Profession = "Mechanic", ProfessionRatingFactor = Rating.HeavyManual });
            ProfessionRatingsList.Add(new ProfessionRatings { Profession = "Florist", ProfessionRatingFactor = Rating.LightManual });

        }
        private void PopulateDictionaryWithFactors()
        {
            OccupationalRatingFactor.Add(Rating.Professional, PremiumCalculationDataRates.ProfessionalFactorRate);
            OccupationalRatingFactor.Add(Rating.WhiteCollar, PremiumCalculationDataRates.WhiteCollarFactorRate);
            OccupationalRatingFactor.Add(Rating.HeavyManual, PremiumCalculationDataRates.HeavyManualFactorRate);
            OccupationalRatingFactor.Add(Rating.LightManual, PremiumCalculationDataRates.LightManualFactorRate);
        }
    }
    
}
