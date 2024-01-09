using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;


namespace WebApi;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExitsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        var genre = new Genre()
        {
            Name = "Deneme",
            IsActive = true
        };
        context.Genres.Add(genre);
        context.SaveChanges();

        CreateGenreCommand command = new(context);
        command.Model = new CreateGenreCommand.CreateGenreModel { Name = genre.Name };

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap türü zaten mevcut.");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        //Arrange
        CreateGenreCommand command = new(context);
        CreateGenreCommand.CreateGenreModel model = new CreateGenreCommand.CreateGenreModel()
        {
            Name = "Test test",
        };

        command.Model = model;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        //Assert
        var genre = context.Genres.SingleOrDefault(g => g.Name == model.Name);
        genre.Should().NotBeNull();
        genre.IsActive.Should().BeTrue();
    }
}