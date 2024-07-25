using BookShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories;

public class ManageCartRepository : IManageCartRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICartReadRepository _cartReadRepository;
    private readonly ILogger<ManageCartRepository> _logger;

    public ManageCartRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager, ICartReadRepository cartReadRepository, ILogger<ManageCartRepository> logger)
    {
        _db = db;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _cartReadRepository = cartReadRepository;
        _logger = logger;
    }

    public async Task<int> AddItem(int bookId, int quantity)
    {
        string userId = CartUtility.GetUserId(_httpContextAccessor, _userManager);
        ValidateUser(userId);
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            ShoppingCart cart = await GetUsersCart(userId);

            CartDetail? cartItem = GetCartItemByBookId(cart.Id, bookId);

            if (cartItem == null)
            {
                await CreateCartItem(bookId, cart.Id, quantity);
            }
            else
            {
                await IncrementCartQuantity(cartItem, quantity);
            }
            transaction.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
        }
        var cartItemCount = await _cartReadRepository.GetCartItemCount(userId);
        return cartItemCount;
    }

    private async Task IncrementCartQuantity(CartDetail cartItem, int quantity)
    {
        cartItem.Quantity += quantity;
        _db.CartDetails.Update(cartItem);
        await _db.SaveChangesAsync();
    }

    private async Task<ShoppingCart> GetUsersCart(string userId)
    {
        ShoppingCart? cart = await _cartReadRepository.GetCart(userId);
        return cart ?? await CreateCart(userId);
    }

    private async Task<ShoppingCart> CreateCart(string userId)
    {
        ShoppingCart cart = new ShoppingCart
        {
            UserId = userId
        };
        _db.ShoppingCarts.Add(cart);
        await _db.SaveChangesAsync();
        return cart;
    }

    private async Task CreateCartItem(int bookId, int cartId, int quantiy)
    {
        Book book = GetBook(bookId);

        CartDetail cartItem = new CartDetail
        {
            BookId = bookId,
            ShoppingCartId = cartId,
            Quantity = quantiy,
            UnitPrice = book.Price
        };
        _db.CartDetails.Add(cartItem);
        await _db.SaveChangesAsync();
    }

    private Book GetBook(int bookId)
    {
        var book = _db.Books.Find(bookId);
        if (book == null)
        {
            throw new InvalidOperationException("Book is null");
        }
        return book;
    }

    private void ValidateUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new UnauthorizedAccessException("user is not logged-in");
    }

    private CartDetail? GetCartItemByBookId(int cartId,int bookId)
    {
        var cartItem = _db.CartDetails.AsNoTracking()
                             .FirstOrDefault(a => a.ShoppingCartId == cartId && a.BookId == bookId);
        return cartItem;
    }

    public async Task<int> RemoveItem(int bookId)
    {
        //using var transaction = _db.Database.BeginTransaction();
        string userId = CartUtility.GetUserId(_httpContextAccessor, _userManager);
        try
        {
            ValidateUser(userId);

            var cart = await _cartReadRepository.GetCart(userId);
            if (cart == null)
            {
                throw new InvalidOperationException("Invalid cart");
            }

            var cartItem = GetCartItemByBookId(cart.Id, bookId);
            if (cartItem == null)
            {
                throw new InvalidOperationException("Cart is empty");
            }
             
            if (cartItem.Quantity == 1)
            {
                _db.CartDetails.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = cartItem.Quantity - 1;
                _db.CartDetails.Update(cartItem);
            }
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        int cartItemCount = await _cartReadRepository.GetCartItemCount(userId);
        return cartItemCount;
    }

}