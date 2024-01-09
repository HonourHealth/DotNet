using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int Id { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == Id);
            if (author is null)
                throw new InvalidOperationException("Author and id are not matching.");

            if (_context.Books.Any())
                throw new InvalidOperationException("Author can not be deleted because of having published book");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

    }
}