using BookShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories;

public class CartReadRepository : ICartReadRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartReadRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<ShoppingCart> GetUserCart()
    {
        var userId = CartUtility.GetUserId(_httpContextAccessor, _userManager);
        if (userId == null)
            throw new InvalidOperationException("Invalid userid");
        var shoppingCart = await _db.ShoppingCarts
                              .Include(a => a.CartDetails)
                              .ThenInclude(a => a.Book)
                              .ThenInclude(a => a.Stock)
                              .Include(a => a.CartDetails)
                              .ThenInclude(a => a.Book)
                              .ThenInclude(a => a.Genre)
                              .Where(a => a.UserId == userId).FirstOrDefaultAsync();
        return shoppingCart;

    }
    public async Task<ShoppingCart> GetCart(string userId)
    {
        var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        return cart;
    }

    public async Task<int> GetCartItemCount(string userId = "")
    {
        if (string.IsNullOrEmpty(userId)) // updated line
        {
            userId = CartUtility.GetUserId(_httpContextAccessor, _userManager);
        }
        var data = await (from cart in _db.ShoppingCarts
                          join cartDetail in _db.CartDetails
                          on cart.Id equals cartDetail.ShoppingCartId
                          where cart.UserId == userId // updated line
                          select new { cartDetail.Id }
                    ).ToListAsync();
        return data.Count;
    }

}

