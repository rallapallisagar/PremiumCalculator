using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumCalculator.Interfaces.CommandResults
{
    public interface ICommandResult
    {
        bool isSuccessful { get; }

        IList<string> ErrorCodes { get; set; }
    }
}
