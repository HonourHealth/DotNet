using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;


namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private DeleteGenreCommandValidator _validator;

    public DeleteGenreCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenGenreIdLessThanOrEqualZero_ValidationShouldReturnError(int genreId)
    {
        //Arrange
        DeleteGenreCommand command = new(null);
        command.GenreId = genreId;

        //Act
        var result = _validator.Validate(command);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        //Arrange
        DeleteGenreCommand command = new(null);
        command.GenreId = 32;

        //Act
        var result = _validator.Validate(command);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}