/* using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;


namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExitsAuthorFullNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        var author = new Author()
        {
            Name = "Necip Fazil",
            Surname = "Short Shovel",
            DateOfBirth = new DateTime(1987, 18, 27)
        };
        _context.Authors.Add(author);
        _context.SaveChanges();

        CreateAuthorCommand command = new(_context, _mapper);
        command.Model = new CreateAuthorCommand.CreateAuthorModel { Name = author.Name, Surname = author.Surname, DateOfBirth = author.DateOfBirth };

        //Act and assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("That Author already exists");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        //Arrange
        CreateAuthorCommand command = new(_context, _mapper);
        CreateAuthorCommand.CreateAuthorModel model = new CreateAuthorCommand.CreateAuthorModel()
        {
            Name = "Ahmet",
            Surname = "His Same is Here",
            DateOfBirth = new DateTime(1977, 17, 27)
        };

        command.Model = model;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        //Assert
        var author = _context.Authors.SingleOrDefault(g => g.Name == model.Name);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
        author.DateOfBirth.Should().Be(model.DateOfBirth);
    }
} */