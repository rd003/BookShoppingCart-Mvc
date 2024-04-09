using BookShoppingCartMvcUI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class AdminOperationController : Controller
{
    private readonly IUserOrderRepository _userOrderRepository;
    public AdminOperationController(IUserOrderRepository userOrderRepository)
    {
        _userOrderRepository = userOrderRepository;
    }
    public async Task<IActionResult> AllOrders()
    {
        var orders = await _userOrderRepository.UserOrders(true);
        return Ok(orders);
    }
}
