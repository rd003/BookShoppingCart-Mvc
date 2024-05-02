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
        public async Task<IActionResult> AddGenre(GenreDTO genre)
        {
            if(!ModelState.IsValid)
            {
                return View(genre);
            }
            try
            {
                var genreToAdd = new Genre { GenreName = genre.GenreName, Id = genre.Id };
                await _genreRepo.AddGenre(genreToAdd);
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
            var genreToUpdate = new GenreDTO
            {
                Id = genre.Id,
                GenreName = genre.GenreName
            };
            return View(genreToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGenre(GenreDTO genreToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(genreToUpdate);
            }
            try
            {
                var genre = new Genre { GenreName = genreToUpdate.GenreName, Id = genreToUpdate.Id };
                await _genreRepo.UpdateGenre(genre);
                TempData["successMessage"] = "Genre is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Genre could not updated!";
                return View(genreToUpdate);
            }

        }

        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _genreRepo.GetGenreById(id);
            if (genre is null)
                throw new InvalidOperationException($"Genre with id: {id} does not found");
            await _genreRepo.DeleteGenre(genre);
            return RedirectToAction(nameof(Index));

        }

    }
}
