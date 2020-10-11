using PremiumCalculator.Interfaces.Command;
using PremiumCalculator.Interfaces.CommandResponse;
using PremiumCalculator.Interfaces.CommandResults;

namespace PremiumCalculator.Interfaces.ICommandHandlers
{
    public interface ICommandHandler<in TQuery, in TResponse> where TQuery : ICommand
       where TResponse : ICommandResponse
    {
            ICommandResult ExecuteCommand(TQuery query);
        }
     
}
