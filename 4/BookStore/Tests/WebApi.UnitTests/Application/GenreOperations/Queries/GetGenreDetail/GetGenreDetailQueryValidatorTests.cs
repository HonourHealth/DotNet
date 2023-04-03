using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;


namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private GetGenreDetailQueryValidator _validator;

    public GetGenreDetailQueryValidatorTests()
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
        GetGenreDetailQuery query = new(null, null);
        query.GenreId = genreId;

        //Act
        var result = _validator.Validate(query);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        //Arrange
        GetGenreDetailQuery query = new(null, null);
        query.GenreId = 22;

        //Act
        var result = _validator.Validate(query);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}