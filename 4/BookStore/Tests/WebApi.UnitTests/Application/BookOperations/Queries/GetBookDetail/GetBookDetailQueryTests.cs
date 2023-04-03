using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;


namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
    {
        //Arrange
        GetBookDetailQuery query = new(context, mapper);
        var BookId = query.BookId = 1;

        var book = context.Books.Include(x => x.Genre).Where(x => x.Id == BookId).SingleOrDefault();

        //Act
        BookDetailViewModel vm = query.Handle();

        //Assert
        vm.Should().NotBeNull();
        vm.Title.Should().Be(book.Title);
        vm.PageCount.Should().Be(book.PageCount);
        vm.Genre.Should().Be(book.Genre.Name);
        vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
    }

    [Fact]
    public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        int bookId = 18;

        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
        query.BookId = bookId;

        //Assert
        query.Invoking(x => x.Handle())
             .Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("Kitap Bulunamadı");
    }
}