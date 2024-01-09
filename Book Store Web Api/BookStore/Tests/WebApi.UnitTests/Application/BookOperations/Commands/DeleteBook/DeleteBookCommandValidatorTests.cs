using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;


namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private DeleteBookCommandValidator _validator;

    public DeleteBookCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenBookIdLessThanOrEqualZero_ValidationShouldReturnError(int bookId)
    {
        //Arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = bookId;

        //Act
        var result = _validator.Validate(command);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenBookIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        //Arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = 25;

        //Act
        var result = _validator.Validate(command);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}