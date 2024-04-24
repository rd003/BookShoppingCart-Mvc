using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Namespace.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class StockController : Controller
{
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    public async Task<IActionResult> Index(string sTerm="")
    {
        var stocks = await _stockRepo.GetStocks(sTerm);
        return View(stocks);
    }

    public IActionResult ManangeStock(int bookId)
    {
        var stock = new StockDTO { BookId = bookId };
        return View(stock);
    }

    [HttpPost]
    public async Task<IActionResult> ManangeStock(StockDTO stock)
    {
        if (!ModelState.IsValid)
            return View(stock);
        try
        {
            await _stockRepo.ManageStock(stock.BookId, stock.Quantity);
            TempData["successMessage"] = "Stock is updated successfully.";
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Something went wrong!!";
        }

        return RedirectToAction(nameof(Index));
    }
}