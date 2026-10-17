
using PentiaCase.Web.ViewModels;

namespace PentiaCase.Web.Services.Interfaces;

public interface IOrderStatisticsService
{
    Task<List<MonthlyOrdersViewModel>> GetMonthlyOrdersAsync(
        CancellationToken cancellationToken = default);
}
