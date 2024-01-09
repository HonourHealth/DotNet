using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestsSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
        context.Authors.AddRange(
            new Author
            {
                Name = "Montaigne",
                Surname = "Tries Again",
                DateOfBirth = new DateTime(1962, 04, 11)
            },
            new Author
            {
                Name = "Arhur Conan",
                Surname = "Wildwest",
                DateOfBirth = new DateTime(1770, 17, 21)
            },
            new Author
            {
                Name = "Connor",
                Surname = "Android Sent By Cyberlife",
                DateOfBirth = new DateTime(1865, 10, 30)
            }
        );
    }
}