
using PentiaCase.Web.Models;

namespace PentiaCase.Web.Services.Interfaces;

public interface ISalesPersonService
{
    Task<List<SalesPerson>> GetSalesPersonsAsync(
        CancellationToken cancellationToken = default);

    Task<SalesPerson?> GetSalesPersonByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}
