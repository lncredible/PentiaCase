
using System.Globalization;
using PentiaCase.Web.Services.Interfaces;
using PentiaCase.Web.ViewModels;

namespace PentiaCase.Web.Services;

public class OrderStatisticsService : IOrderStatisticsService
{
    private readonly IWingineersApiClient _apiClient;

    public OrderStatisticsService(IWingineersApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<MonthlyOrdersViewModel>> GetMonthlyOrdersAsync(
        CancellationToken cancellationToken = default)
    {
        var orders = await _apiClient.GetOrderLinesAsync(cancellationToken);

        var dates = orders
            .Select(order =>
                DateTime.TryParse(
                    order.OrderDate,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var date)
                    ? (DateTime?)date
                    : null)
            .Where(date => date.HasValue)
            .Select(date => date.Value)
            .ToList();

        if (dates.Count == 0)
            return new List<MonthlyOrdersViewModel>();

        var counts = dates
            .GroupBy(date => (date.Year, date.Month))
            .ToDictionary(
                group => group.Key,
                group => group.Count());

        var firstDate = dates.Min();
        var lastDate = dates.Max();

        var firstMonth = new DateTime(
            firstDate.Year, firstDate.Month, 1);

        var lastMonth = new DateTime(
            lastDate.Year, lastDate.Month, 1);

        var result = new List<MonthlyOrdersViewModel>();

        for (var month = firstMonth;
             month <= lastMonth;
             month = month.AddMonths(1))
        {
            counts.TryGetValue(
                (month.Year, month.Month),
                out var count);

            result.Add(new MonthlyOrdersViewModel
            {
                Year = month.Year,
                Month = month.Month,
                OrderCount = count
            });
        }

        return result;
    }
}
