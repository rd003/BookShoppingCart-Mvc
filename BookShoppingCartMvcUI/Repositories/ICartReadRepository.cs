namespace BookShoppingCartMvcUI.Repositories;

public interface ICartReadRepository
{
    Task<ShoppingCart?> GetUserCart();
    Task<int> GetCartItemCount(string userId = "");
    Task<ShoppingCart?> GetCart(string userId);
}