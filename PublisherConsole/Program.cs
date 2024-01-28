// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

Console.WriteLine("Hello, World!");
using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

//GetAuthors();
AddAuthor();
//GetAuthors();
AddAuthorWithBook();
GetAuthorsWithBooks();

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}

void AddAuthor()
{
    var author = new Author { FirstName = "Josie", LastName = "Newf" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework",
        PublishDate = DateTime.SpecifyKind(new DateTime(2010, 8, 1), DateTimeKind.Utc)
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = DateTime.SpecifyKind(new DateTime(2010, 8, 1), DateTimeKind.Utc)
    });

    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach(var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach(var book in author.Books)
        {
            Console.WriteLine("*" + book.Title);
        }
    }
}