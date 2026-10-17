
using AutoMapper;
using PentiaCase.Web.Models;
using PentiaCase.Web.Services.Interfaces;

namespace PentiaCase.Web.Services;

public class SalesPersonService : ISalesPersonService
{
    private readonly IWingineersApiClient _apiClient;
    private readonly IMapper _mapper;

    public SalesPersonService(
        IWingineersApiClient apiClient,
        IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    public async Task<List<SalesPerson>> GetSalesPersonsAsync(
        CancellationToken cancellationToken = default)
    {
        // FEtch both datasets from API
        var salesPersonDtos = await _apiClient
            .GetSalesPersonsAsync(cancellationToken);

        var orderLineDtos = await _apiClient
            .GetOrderLinesAsync(cancellationToken);

        //DTOs to models
        var salesPersons = _mapper.Map<List<SalesPerson>>(
            salesPersonDtos);

        var orderLines = _mapper.Map<List<OrderLine>>(
            orderLineDtos);

        // Group order lines by salesperson ID
        var ordersBySalesPerson = orderLines.ToLookup(
            order => order.SalesPersonId);

        // Associate orders with their salesperson
        foreach (var person in salesPersons)
        {
            person.Orders = ordersBySalesPerson[person.Id]
                .OrderByDescending(order => order.OrderDate)
                .ToList();
        }

        return salesPersons
            .OrderBy(person => person.Name)
            .ToList();
    }

    public async Task<SalesPerson?> GetSalesPersonByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var salesPersons = await GetSalesPersonsAsync(
            cancellationToken);

        return salesPersons.FirstOrDefault(
            person => person.Id == id);
    }
}
