
namespace BookShoppingCartMvcUI.Repositories
{
    public interface IStockRepository
    {
        Task AddStock(Stock stock);
        Task DeleteStock(Stock stock);
        Task<Stock?> GetStockById(int id);
        Task<IEnumerable<StockDisplayModel>> GetStocks();
        Task UpdateStock(Stock stock);
    }
}