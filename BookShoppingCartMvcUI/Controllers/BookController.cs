using BookShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IFileService _fileService;

        public BookController(IBookRepository bookRepo, IGenreRepository genreRepo, IFileService fileService)
        {
            _bookRepo = bookRepo;
            _genreRepo = genreRepo;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookRepo.GetBooks();
            return View(books);
        }

        public async Task<IActionResult> AddBook()
        {
            var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            BookDTO bookToAdd = new() { GenreList = genreSelectList };
            return View(bookToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookToAdd)
        {
            var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            bookToAdd.GenreList = genreSelectList;

            if (!ModelState.IsValid)
                return View(bookToAdd);

            try
            {
                if (bookToAdd.ImageFile != null)
                {
                    if(bookToAdd.ImageFile.Length> 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg",".jpg",".png"];
                    string imageName=await _fileService.SaveFile(bookToAdd.ImageFile, allowedExtensions);
                    bookToAdd.Image = imageName;
                }
                // manual mapping of BookDTO -> Book
                Book book = new()
                {
                    Id = bookToAdd.Id,
                    BookName = bookToAdd.BookName,
                    AuthorName = bookToAdd.AuthorName,
                    Image = bookToAdd.Image,
                    GenreId = bookToAdd.GenreId,
                    Price = bookToAdd.Price
                };
                await _bookRepo.AddBook(book);
                return RedirectToAction(nameof(AddBook));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"]= ex.Message;
                return View(bookToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(bookToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(bookToAdd);
            }
        }

    }
}
