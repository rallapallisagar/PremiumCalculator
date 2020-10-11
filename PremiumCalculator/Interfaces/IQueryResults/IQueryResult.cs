using System.Collections.Generic;

namespace PremiumCalculator.Interfaces.IQueryHandlers
{
    public interface IQueryResult
    {
        bool isSuccessful { get; }

        IList<string> ErrorCodes { get; set; }
    }
}
