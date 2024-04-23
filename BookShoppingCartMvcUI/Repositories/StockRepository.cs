//using BookShoppingCartMvcUI.Models.DTOs;
//using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddStock(int bookId, int quantity)
        {
            throw new NotImplementedException();
            // _context.Stocks.Add(stock);
            // await _context.SaveChangesAsync();
        }

        public async Task<Stock?> GetStockById(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks()
        {
            var stocks = await (from book in _context.Books
                                join stock in _context.Stocks
                                on book.Id equals stock.BookId
                                into book_stock
                                from bookStock in book_stock.DefaultIfEmpty()
                                select new StockDisplayModel
                                {
                                    BookId = book.Id,
                                    BookName = book.BookName,
                                    Quantity = bookStock == null ? 0 : bookStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }

    }

    public interface IStockRepository
    {
        Task<Stock?> GetStockById(int id);
        Task<IEnumerable<StockDisplayModel>> GetStocks();
        Task AddStock(int bookId, int quantity);
    }
}
