using BookShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories;
public class CheckoutRepository : ICheckoutRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICartReadRepository _cartReadRepository;

    public CheckoutRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager, ICartReadRepository cartReadRepository)
    {
        _db = db;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _cartReadRepository = cartReadRepository;
    }

    public async Task<bool> DoCheckout(CheckoutModel model)
    {
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            // logic
            // move data from cartDetail to order and order detail then we will remove cart detail
            var userId = CartUtility.GetUserId(_httpContextAccessor, _userManager);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged-in");
            var cart = await _cartReadRepository.GetCart(userId);
            if (cart is null)
                throw new InvalidOperationException("Invalid cart");
            var cartDetail = _db.CartDetails
                                .Where(a => a.ShoppingCartId == cart.Id).ToList();
            if (cartDetail.Count == 0)
                throw new InvalidOperationException("Cart is empty");
            var pendingRecord = _db.orderStatuses.FirstOrDefault(s => s.StatusName == "Pending");
            if (pendingRecord is null)
                throw new InvalidOperationException("Order status does not have Pending status");
            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                Name = model.Name,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                PaymentMethod = model.PaymentMethod,
                Address = model.Address,
                IsPaid = false,
                OrderStatusId = pendingRecord.Id
            };
            _db.Orders.Add(order);
            _db.SaveChanges();
            foreach (var item in cartDetail)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                _db.OrderDetails.Add(orderDetail);

                // update stock here

                var stock = await _db.Stocks.FirstOrDefaultAsync(a => a.BookId == item.BookId);
                if (stock == null)
                {
                    throw new InvalidOperationException("Stock is null");
                }

                if (item.Quantity > stock.Quantity)
                {
                    throw new InvalidOperationException($"Only {stock.Quantity} items(s) are available in the stock");
                }
                // decrease the number of quantity from the stock table
                stock.Quantity -= item.Quantity;
            }
            //_db.SaveChanges();

            // removing the cartdetails
            _db.CartDetails.RemoveRange(cartDetail);
            _db.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }
    }
}
