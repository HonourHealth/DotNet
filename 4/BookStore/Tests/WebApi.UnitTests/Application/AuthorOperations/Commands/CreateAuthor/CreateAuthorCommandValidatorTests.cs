/* using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("a")]
    public void WhenNameIsInvalid_Validator_ShouldReturnError(string name)
    {
        //Arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel { Name = name, Surname = "Test", DateOfBirth = new DateTime(1991, 2, 2) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        //Act
        var result = validator.Validate(command);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("a")]
    
    public void WhenSurnameHasLessThan4Characters_Validator_ShouldReturnError(string surname)
    {
        //Arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel { Name = "Deneme", Surname = surname, DateOfBirth = new DateTime(1990, 1, 1) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        //Act
        var result = validator.Validate(command);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenBirthdayIsAfterToday_Validator_ShouldReturnError()
    {
        //Arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel { Name = "Deneme", Surname = "Test", DateOfBirth = DateTime.Now.AddDays(1) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        //Act
        var result = validator.Validate(command);

        //Assert
        result.Errors.Should().ContainSingle();
    }

    [Fact]
    public void WhenModelIsValid_Validator_ShouldNotReturnError()
    {
        //Arrange
        var command = new CreateAuthorCommand(null, null);
        var Model = new CreateAuthorModel { Name = "Deneme", Surname = "Test", DateOfBirth = new DateTime(1990, 1, 1) };

        command.Model = Model;

        var validator = new CreateAuthorCommandValidator();

        //Act
        var result = validator.Validate(command);

        //Assert
        result.Errors.Should().BeEmpty();
    }
}
 */