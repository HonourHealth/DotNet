using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;


namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private GetBookDetailQueryValidator _validator;

    public GetBookDetailQueryValidatorTests()
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
        GetBookDetailQuery query = new(null, null);
        query.BookId = bookId;

        //Act
        var result = _validator.Validate(query);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenBookIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        //Arrange
        GetBookDetailQuery query = new(null, null);
        query.BookId = 15;

        //Act
        var result = _validator.Validate(query);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}