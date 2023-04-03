using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange

        DeleteGenreCommand command = new(_context);
        command.GenreId = 32;

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü Bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        //Arrange
        DeleteGenreCommand command = new(_context);
        command.GenreId = 1;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //Assert
        var genre = _context.Genres.SingleOrDefault(g => g.Id == command.GenreId);
        genre.Should().BeNull();
    }
}