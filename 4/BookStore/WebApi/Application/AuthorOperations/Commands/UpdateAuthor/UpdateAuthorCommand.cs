using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorModel Model { get; set; }
        public int Id { get; set; }

        public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == Id);
            if (author is null)
                throw new InvalidOperationException("Id and author are not matching.");

            author.Name = Model.Name;
            author.Surname = Model.Surname;
            author.DateOfBirth = Model.DateOfBirth;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}