
using Microsoft.AspNetCore.Mvc;
using PentiaCase.Web.Services.Interfaces;

namespace PentiaCase.Web.Controllers;

public class StatisticsController : Controller
{
    private readonly IOrderStatisticsService _statisticsService;

    public StatisticsController(
        IOrderStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Displays monthly order statistics
    /// <returns>The monthly statistics page</returns>
    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var viewModel = await _statisticsService
            .GetMonthlyOrdersAsync(cancellationToken);

        return View(viewModel);
    }
}
