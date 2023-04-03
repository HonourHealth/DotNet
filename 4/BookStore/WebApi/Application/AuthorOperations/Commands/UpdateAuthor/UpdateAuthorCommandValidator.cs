using FluentValidation;
using System;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(2);
            RuleFor(command => command.Model.Surname).MinimumLength(2);
            RuleFor(command => command.Model.DateOfBirth.Date).LessThan(DateTime.Today.Date);
        }
    }
}