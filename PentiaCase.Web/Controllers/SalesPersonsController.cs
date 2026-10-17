
using Microsoft.AspNetCore.Mvc;
using PentiaCase.Web.Services.Interfaces;
using PentiaCase.Web.ViewModels;

namespace PentiaCase.Web.Controllers;

public class SalesPersonsController : Controller
{
    private readonly ISalesPersonService _salesPersonService;

    public SalesPersonsController(
        ISalesPersonService salesPersonService)
    {
        _salesPersonService = salesPersonService;
    }

    /// <summary>
    /// Displays all salespeople with their total order counts
    /// </summary>
    /// <param name="cancellationToken">
    /// </param>
    /// <returns>
    /// The salesperson overview page.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var salesPersons = await _salesPersonService
            .GetSalesPersonsAsync(cancellationToken);

        var viewModel = salesPersons
            .Select(person => new SalesPersonListItemViewModel
            {
                Id = person.Id,
                Name = person.Name,
                OrderCount = person.Orders.Count
            })
            .ToList();

        return View(viewModel);
    }
    /// <summary>
    /// Displays information and orders for a specific salesperson.
    /// </summary>
    /// <param name="id">
    /// Id of salesperson
    /// </param>
    /// <returns>
    /// The salesperson details page, or 404 if not found.
    /// </returns>
    public async Task<IActionResult> Details(
        int id,
        CancellationToken cancellationToken)
    {
        var salesPerson = await _salesPersonService
            .GetSalesPersonByIdAsync(id, cancellationToken);

        if (salesPerson == null)
            return NotFound();

        var viewModel = new SalesPersonDetailsViewModel
        {
            SalesPerson = salesPerson
        };

        return View(viewModel);
    }
}
