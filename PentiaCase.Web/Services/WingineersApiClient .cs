using PentiaCase.Web.DTOs;
using PentiaCase.Web.Services.Interfaces;

namespace PentiaCase.Web.Services;

public class WingineersApiClient : IWingineersApiClient
{
    private readonly HttpClient _httpClient;

    public WingineersApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SalesPersonDto>> GetSalesPersonsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<SalesPersonDto>>(
            "api/v1/SalesPersons",
            cancellationToken) ?? new List<SalesPersonDto>(); ;
    }

    public async Task<List<OrderLineDto>> GetOrderLinesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<OrderLineDto>>(
            "api/v1/Orderlines",
            cancellationToken) ?? new List<OrderLineDto>(); ;
    }
}
