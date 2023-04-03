using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;


namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange

        UpdateGenreCommand command = new(_context);
        command.GenreId = 999;

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap türü bulunamadı.");
    }

    [Fact]
    public void WhenGivenGenreNameAlreadyExists_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        UpdateGenreCommand command = new(_context);
        var genre = new Genre { Name = "Deneme" };

        _context.Genres.Add(genre);
        _context.SaveChanges();

        var genre1 = new Genre { Name = "Test" };

        _context.Genres.Add(genre1);
        _context.SaveChanges();

        command.GenreId = genre1.Id;
        UpdateGenreCommand.UpdateGenreModel model = new UpdateGenreCommand.UpdateGenreModel { Name = "Deneme" };
        command.Model = model;

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should().Be("Aynı isimli bir kitap türü zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        //Arrange
        UpdateGenreCommand command = new(_context);
        var genre = new Genre { Name = "Deneme" };

        _context.Genres.Add(genre);
        _context.SaveChanges();

        command.GenreId = genre.Id;
        UpdateGenreCommand.UpdateGenreModel model = new UpdateGenreCommand.UpdateGenreModel { Name = "Yeni Test" };
        command.Model = model;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        //Assert
        var updatedGenre = _context.Genres.SingleOrDefault(g => g.Id == genre.Id);
        updatedGenre.Should().NotBeNull();
        updatedGenre.IsActive.Should().BeTrue();
        updatedGenre.Name.Should().Be(model.Name);
    }
}