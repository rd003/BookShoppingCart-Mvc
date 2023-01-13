namespace BookShoppingCartMvcUI.Repositories
{
    public interface ICartRepository
    {
        Task<bool> AddItem(int bookId, int qty);
        Task<bool> RemoveItem(int bookId);
        Task<IEnumerable<ShoppingCart>> GetUserCart();
    }
}
