using AutoMapper;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }

    }        
}