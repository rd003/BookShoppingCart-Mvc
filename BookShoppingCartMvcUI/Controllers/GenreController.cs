using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepo;

        public GenreController(IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepo.GetGenres();
            return View(genres);
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View(genre);
            }
            try
            {
                await _genreRepo.AddGenre(genre);
                TempData["successMessage"] = "Genre added successfully";
                return RedirectToAction(nameof(AddGenre));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = "Genre could not added!";
                return View(genre);
            }

        }

        public async Task<IActionResult> UpdateGenre(int id)
        {
            var genre = await _genreRepo.GetGenreById(id);
            if (genre is null)
                throw new InvalidOperationException($"Genre with id: {id} does not found");
            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            try
            {
                await _genreRepo.UpdateGenre(genre);
                TempData["successMessage"] = "Genre is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Genre could not updated!";
                return View(genre);
            }

        }

        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _genreRepo.GetGenreById(id);
            if (genre is null)
                throw new InvalidOperationException($"Genre with id: {id} does not found");
            await _genreRepo.DeleteGenre(genre);
            return View(genre);
        }

    }
}
