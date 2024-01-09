using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    
    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange

        DeleteBookCommand command = new(context);
        command.BookId = 25;

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should().Be("Silinecek Kitap Bulunamadı!");
    }  

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        //Arrange
        DeleteBookCommand command = new(context);
        command.BookId = 1;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        //Assert
        var book = context.Books.SingleOrDefault(x => x.Id == command.BookId);
        book.Should().BeNull();
    }
}