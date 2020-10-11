using System;

namespace PremiumCalculator.Interfaces.IQueryHandlers
{
    public interface IQueryRequest
    {
        public DateTime RequestDateStamp { get; set; }
    }
}
