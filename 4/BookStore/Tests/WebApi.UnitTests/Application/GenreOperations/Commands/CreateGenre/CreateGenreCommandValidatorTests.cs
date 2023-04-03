using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("a")]
    [InlineData("abc")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
        //Arrange
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new();

        //Act
        var result = validator.Validate(command);

        //Assert
        result.Errors.Count.Should().BeGreaterThanOrEqualTo(0);
    }

    [Theory]
    [InlineData("abcd")]
    [InlineData("action")]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string name)
    {
        //Arrange
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

        //Act
        var result = validator.Validate(command);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}