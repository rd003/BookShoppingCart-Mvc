namespace BookShoppingCartMvcUI.Repositories;

public interface IManageCartRepository
{
    Task<int> AddItem(int bookId, int qty);
    Task<int> RemoveItem(int bookId);
}
