//using BookShoppingCartMvcUI.Models.DTOs;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace BookShoppingCartMvcUI.Repositories
{
    public class StockRepository: IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStock(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<Stock?> GetStockById(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task DeleteStock(Stock stock)
        {
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks()
        {
            // It is temperory, we will define it's logic later
            return await Task.FromResult(Enumerable.Empty<StockDisplayModel>());
        }

    }
}
