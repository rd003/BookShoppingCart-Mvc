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

        public async Task ManageStock(int bookId, int quantity)
        {
            // if there is no stock for given book id, then add new record
            // if there is already stock for given book id, update stock's quantity
            var existingStock = await GetStockById(bookId);
            if (existingStock is null)
            {
                var stock = new Stock { BookId = bookId, Quantity = quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Stock?> GetStockById(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm="")
        {
            var stocks = await (from book in _context.Books
                                join stock in _context.Stocks
                                on book.Id equals stock.BookId
                                into book_stock
                                from bookStock in book_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) ||            book.BookName.ToLower().Contains(sTerm.ToLower())
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
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm="");
        Task ManageStock(int bookId, int quantity);
    }
}
