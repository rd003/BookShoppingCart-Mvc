using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories;

public interface IGenreRepository
{
    Task AddGenre(Genre genre);
    Task UpdateGenre(Genre genre);
    Task<Genre?> GetGenreById(int id);
    Task DeleteGenre(Genre genre);
    Task<IEnumerable<Genre>> GetGenres();
}
public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext _context;
    public GenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddGenre(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateGenre(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenre(Genre genre)
    {
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }

    
}
