using System;
namespace PremiumCalculator.Interfaces.Command
{
    public interface ICommand
    {
        public DateTime RequestDateStamp { get; set; }
    }
}
