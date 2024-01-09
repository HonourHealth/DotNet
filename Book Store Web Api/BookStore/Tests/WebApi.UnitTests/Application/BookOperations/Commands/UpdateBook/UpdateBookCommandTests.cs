using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange

        UpdateBookCommand command = new(context);
        command.BookId = 1000;

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Bulunamadı!");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        //Arrange
        UpdateBookCommand command = new(context);
        var book = new Book { Title = "Book Name", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2021, 5, 8) };

        context.Books.Add(book);
        context.SaveChanges();

        command.BookId = book.Id;
        UpdateBookCommand.UpdateBookModel model = new UpdateBookCommand.UpdateBookModel { Title = "Updated Title", GenreId = 3 };
        command.Model = model;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        //Assert
        var updatedBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
        updatedBook.Should().NotBeNull();
        updatedBook.PageCount.Should().Be(book.PageCount);
        updatedBook.PublishDate.Should().Be(book.PublishDate);
        updatedBook.Title.Should().Be(model.Title);
        updatedBook.GenreId.Should().Be(model.GenreId);
    }
}