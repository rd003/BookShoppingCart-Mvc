using Microsoft.AspNetCore.Mvc;

namespace Namespace.Controllers;

public class StockController : Controller
{
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    public async Task<IActionResult> Index()
    {
        var stocks = await _stockRepo.GetStocks();
        return View(stocks);
    }
}