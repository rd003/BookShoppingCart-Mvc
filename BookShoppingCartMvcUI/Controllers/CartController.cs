﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartRepository cartRepo, ILogger<CartController> logger)
        {
            _cartRepo = cartRepo;
            _logger = logger;
        }
        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            try
            {
                var cartCount = await _cartRepo.AddItem(bookId, qty);
                if (redirect == 0)
                    return Ok(cartCount);
            }
            catch (Exception ex)
            {
                TempData[NotificationType.ErrorMessage] = "Something went wrong!!";
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepo.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool isCheckedOut = await _cartRepo.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
