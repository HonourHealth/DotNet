using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;

        public readonly IMapper _mappper;
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mappper)
        {
            _context = context;
            _mappper = mappper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if(genre == null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            }
            return _mappper.Map<GenreDetailViewModel>(genre);
        }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    }
}