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
    private readonly ILogger<CheckoutRepository> _logger;

    public CheckoutRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager, ICartReadRepository cartReadRepository, ILogger<CheckoutRepository> logger)
    {
        _db = db;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _cartReadRepository = cartReadRepository;
        _logger = logger;
    }

    public async Task<bool> DoCheckout(CheckoutModel model)
    {
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            // logic
            // move data from cartDetail to order and order detail then we will remove cart detail
            var userId = CartUtility.GetUserId(_httpContextAccessor, _userManager);

            ValidateUser(userId);

            ShoppingCart cart = await GetCart(userId);

            IEnumerable<CartDetail> cartDetail = await GetCartDetail(cart.Id);

            OrderStatus orderStatus = await GetOrderStatus("Pending");

            Order order = await CreateOrderAsync(userId,model, orderStatus.Id);
            
            await CreateCartItems(cartDetail, order);

            _db.CartDetails.RemoveRange(cartDetail);
            await _db.SaveChangesAsync();
            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    private async Task<OrderStatus> GetOrderStatus(string orderStatusValue)
    {
        OrderStatus? orderStatus = await _db.orderStatuses.AsNoTracking().FirstOrDefaultAsync(s => s.StatusName == orderStatusValue);

        if (orderStatus == null)
        {
            throw new InvalidOperationException("Order status does not have Pending status");
        }
        return orderStatus;
    }

    private async Task<IEnumerable<CartDetail>> GetCartDetail(int cartId)
    {
       var cartDetail= await _db.CartDetails
                 .Where(a => a.ShoppingCartId == cartId)
                 .AsNoTracking()
                 .ToListAsync();

        if (cartDetail.Count == 0)
        {
            throw new InvalidOperationException("Cart is empty");
        }
        return cartDetail;
    }

    private async Task CreateCartItems(IEnumerable<CartDetail> cartDetail, Order order)
    {
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

            await UpdateStock(item);
        }
    }

    private async Task UpdateStock(CartDetail item)
    {
        var stock = await _db.Stocks.FirstAsync(a => a.BookId == item.BookId);

        if (item.Quantity > stock.Quantity)
        {
            throw new InvalidOperationException($"Only {stock.Quantity} items(s) are available in the stock");
        }
        // decrease the number of quantity from the stock table
        stock.Quantity -= item.Quantity;
    }

    private async Task<ShoppingCart> GetCart(string userId)
    {
        var cart = await _cartReadRepository.GetCart(userId);
        if (cart is null)
        {
            throw new InvalidOperationException("Invalid cart");
        }
        return cart;
    }

    private async Task<Order> CreateOrderAsync(string userId, CheckoutModel model, int orderStatusId)
    {
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
            OrderStatusId = orderStatusId
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return order;
    }

    private void ValidateUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new UnauthorizedAccessException("User is not logged-in");
        }
    }
}
