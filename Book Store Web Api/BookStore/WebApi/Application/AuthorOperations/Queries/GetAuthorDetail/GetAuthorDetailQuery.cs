using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("The Author is not found");

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author); 
            return vm;
        }

    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}