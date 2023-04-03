using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery getAuthorsQuery = new GetAuthorsQuery(_context, _mapper);
            return Ok(getAuthorsQuery.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(_context, _mapper);
            AuthorDetailViewModel result;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            getAuthorDetailQuery.AuthorId = id;
            validator.ValidateAndThrow(getAuthorDetailQuery);
            result = getAuthorDetailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newBook)
        {
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context, _mapper);
            createAuthorCommand.Model = newBook;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(createAuthorCommand);
            createAuthorCommand.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedBook)
        {
            UpdateAuthorCommand updateBookCommand = new UpdateAuthorCommand(_context, _mapper);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            updateBookCommand.Model = updatedBook;
            updateBookCommand.Id = id;
            validator.ValidateAndThrow(updateBookCommand);
            updateBookCommand.Handle();

            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand deleteBookCommand = new DeleteAuthorCommand(_context);
            deleteBookCommand.Id = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();

            return Ok();
        }
    }
}