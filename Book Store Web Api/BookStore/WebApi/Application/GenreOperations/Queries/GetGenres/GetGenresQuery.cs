using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _context;

        public readonly IMapper _mappper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mappper)
        {
            _context = context;
            _mappper = mappper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mappper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

}