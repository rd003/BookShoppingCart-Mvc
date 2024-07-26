using BookShoppingCartMvcUI.Controllers;
using BookShoppingCartMvcUI.Models;
using BookShoppingCartMvcUI.Models.DTOs;
using BookShoppingCartMvcUI.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BookShoppingCart.Test;

public class BookShoppingCartTests
{
    private readonly IGenreRepository _mockGenreRepo; //mocked dependency
    private readonly GenreController _controller; // system under test

    public BookShoppingCartTests()
    {
        _mockGenreRepo = Substitute.For<IGenreRepository>();
        _controller = new GenreController(_mockGenreRepo);
    }

    [Fact]
    public async Task Index_ReturnsViewWithGenres()
    {
        // Arrange
        List<Genre> genres = new List<Genre>();
        _mockGenreRepo.GetGenres().Returns(genres);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Genre>>(viewResult.ViewData.Model);
        Assert.Equal(genres.Count, model.Count());

    }

    // [Fact]
    // public async Task AddGenre_ValidModel_ReturnsRedirectToAction()
    // {
    //     // Arrange
    //     GenreDTO genreDTO = new GenreDTO { GenreName = "Test Genre", Id = 1 };

    //     // Act
    //     var result = await _controller.AddGenre(genreDTO);

    //     // Assert
    //     var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
    //     Assert.Equal(nameof(GenreController.AddGenre), redirectToActionResult.ActionName);
    //     await _mockGenreRepo.Received(1).AddGenre(Arg.Is<Genre>(g => g.GenreName == genreDTO.GenreName));
    // }

    // [Fact]
    // public async Task UpdateGenre_ValidModel_ReturnsRedirectToAction()
    // {
    //     // Arrange
    //     GenreDTO genreDTO = new GenreDTO { Id = 1, GenreName = "Updated Genre" };

    //     // Act
    //     var result = await _controller.UpdateGenre(genreDTO);

    //     // Assert
    //     var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
    //     Assert.Equal(nameof(GenreController.Index), redirectToActionResult.ActionName);
    //     await _mockGenreRepo.Received(1).UpdateGenre(Arg.Is<Genre>(g => g.Id == genreDTO.Id && g.GenreName == genreDTO.GenreName));
    // }

    [Fact]
    public async Task DeleteGenre_ExistingGenre_ReturnsRedirectToAction()
    {
        // Arrange
        int genreId = 1;
        var genre = new Genre { Id = genreId, GenreName = "Test Genre" };
        _mockGenreRepo.GetGenreById(genreId).Returns(genre);

        // Act
        var result = await _controller.DeleteGenre(genreId);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(GenreController.Index), redirectToActionResult.ActionName);
        await _mockGenreRepo.Received(1).DeleteGenre(Arg.Is<Genre>(g => g.Id == genreId));
    }

}