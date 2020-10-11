
namespace PremiumCalculator.Interfaces.IQueryHandlers
{
    public interface IQueryHandler<in TQuery,in TResponse> where TQuery:IQueryRequest
        where TResponse:IQueryResponse
    {
        IQueryResult ExecuteQuery(TQuery query);
    }
}
