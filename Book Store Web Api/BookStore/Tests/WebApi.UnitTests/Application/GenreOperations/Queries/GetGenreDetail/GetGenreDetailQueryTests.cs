using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
    {
        //Arrange
        GetGenreDetailQuery query = new(_context, _mapper);
        var GenreId = query.GenreId = 1;

        var genre = _context.Genres.Where(g => g.Id == GenreId).SingleOrDefault();

        //Act
        GenreDetailViewModel vm = query.Handle();

        //Assert
        vm.Should().NotBeNull();
        vm.Name.Should().Be(genre.Name);
    }

    [Fact]
    public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        int genreId = 22;

        GetGenreDetailQuery query = new(_context, _mapper);
        query.GenreId = genreId;

        //Assert
        query.Invoking(x => x.Handle())
             .Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("Kitap türü bulunamadı.");
    }
}