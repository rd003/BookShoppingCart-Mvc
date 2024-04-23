using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var stocks = await _stockRepo.GetStocks();
                return View(stocks);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error on retrieving stocks";
                return View();
            }
        }

        public async Task<IActionResult> AddStock()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStock()
        {
            return View();
        }

    }
}
