using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuerry
    {
        private readonly IBookStoreDbContext _dpContext;
        private readonly IMapper _mapper;
        public GetBooksQuerry(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dpContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dpContext.Books.Include(x=>x.Genre).OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList); //new List<BooksViewModel>();
            /* foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount
                });
            } */
            return vm;
        }

    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; } // DateTime is original version
        public string Genre { get; set; } // Genre is was int
    }

}