using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumCalculator.Interfaces.CommandResponse
{
    public interface ICommandResponse
    {
        public DateTime ResponseDateTime { get; set; }
    }
}
