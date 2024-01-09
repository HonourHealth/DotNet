using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Name == Model.Name
            && author.Surname == Model.Surname && author.DateOfBirth == Model.DateOfBirth);

            if (author is not null)
                throw new InvalidOperationException("Author is already exist.");

            author = new Author
            {
                Name = Model.Name,
                Surname = Model.Surname,
                DateOfBirth = Model.DateOfBirth
            };

            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }

    
}