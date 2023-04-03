using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
    public CreateUserCommandValidator()
    {
        RuleFor(command=> command.Model.Password).NotEmpty().MinimumLength(3);
        RuleFor(command=> command.Model.Email).NotEmpty();
        RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(2);
        
    }
        
    }
}
