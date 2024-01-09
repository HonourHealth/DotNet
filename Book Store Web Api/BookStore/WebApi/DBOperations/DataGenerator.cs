using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator{
        public static void Initialize(IServiceProvider serviceProvider){
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()){
                    return;
                }
                context.Authors.AddRange(
                    new Author {
                        Name = "Arnould",
                        Surname = "Hangs Around",
                        DateOfBirth = new DateTime(1987,02,21)
                    },
                    new Author {
                        Name = "Michael",
                        Surname = "Dancerson",
                        DateOfBirth = new DateTime(1979,12,01) 
                    },
                    new Author {
                        Name = "Montaigne",
                        Surname = "Nice Try",
                        DateOfBirth = new DateTime(1967,06,15) 
                    });

                context.Genres.AddRange(
                    new Genre 
                    {
                        Name = "Personal Growth"
                    },
                    new Genre 
                    {
                        Name = "Science Fiction"
                    },
                    new Genre 
                    {
                        Name = "Romance"
                    }

                );


                context.Books.AddRange(
                    new Book {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, //Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12) 
                    },
                    new Book {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010,05,23) 
                    },
                    new Book {
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2, //Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2001,12,21) 
                    });

                    context.SaveChanges();
            }
        }

    }


}