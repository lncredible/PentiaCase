using PentiaCase.Web.DTOs;

namespace PentiaCase.Web.Services.Interfaces;

public interface IWingineersApiClient
{
    Task<List<SalesPersonDto>> GetSalesPersonsAsync(
        CancellationToken cancellationToken = default);

    Task<List<OrderLineDto>> GetOrderLinesAsync(
        CancellationToken cancellationToken = default);
}