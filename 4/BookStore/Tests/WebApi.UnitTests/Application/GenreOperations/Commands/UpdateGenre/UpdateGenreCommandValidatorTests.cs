using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private UpdateGenreCommandValidator _validator;

    public UpdateGenreCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("1")]
    [InlineData("x")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string name)
    {
        //Arrange
        var model = new UpdateGenreCommand.UpdateGenreModel { Name = name };
        UpdateGenreCommand updateCommand = new(null);
        updateCommand.GenreId = 1;
        updateCommand.Model = model;

        //Act
        var result = _validator.Validate(updateCommand);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("Deneme")]
    [InlineData("Deneme Test")]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError(string name)
    {
        //Arrange
        var model = new UpdateGenreCommand.UpdateGenreModel { Name = name };
        UpdateGenreCommand updateCommand = new(null);
        updateCommand.GenreId = 2;
        updateCommand.Model = model;

        //Act
        var result = _validator.Validate(updateCommand);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}