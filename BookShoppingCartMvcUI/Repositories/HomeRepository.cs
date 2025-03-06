using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
        {

            var bookQuery = _db.Books
                           .AsNoTracking()
                           .Include(x => x.Genre)
                           .Include(x => x.Stock)
                           .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sTerm))
            {
                bookQuery = bookQuery.Where(b => b.BookName.StartsWith(sTerm.ToLower()));
            }

            if (genreId > 0)
            {
                bookQuery = bookQuery.Where(b => b.GenreId == genreId);
            }

            var books = await bookQuery
                .AsNoTracking()
                .Select(book => new Book
                {
                    Id = book.Id,
                    Image = book.Image,
                    AuthorName = book.AuthorName,
                    BookName = book.BookName,
                    GenreId = book.GenreId,
                    Price = book.Price,
                    GenreName = book.Genre.GenreName,
                    Quantity = book.Stock == null ? 0 : book.Stock.Quantity
                }).ToListAsync();

            return books;

        }
    }
}
