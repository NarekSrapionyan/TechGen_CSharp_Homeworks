namespace Project;

class Program
{
    static void Main(string[] args)
    {
        using (var db = new LibraryContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Seed(db);
        }
    }

    static void Seed(LibraryContext db)
    {
        var book1 = new Book
        {
            Title = "Clean Code",
            Author = "Robert Martin",
            Year = 2008,
            Pages = 464,
            Price = 42.50m,
            IsRead = true,
            AddedAt = DateTime.UtcNow
        };

        var book2 = new Book
        {
            Title = "The Pragmatic Programmer",
            Author = "Andrew Hunt",
            Year = 1999,
            Pages = 352,
            Price = 38.00m,
            IsRead = true,
            AddedAt = DateTime.UtcNow
        };

        var book3 = new Book
        {
            Title = "Designing Data-Intensive Applications",
            Author = "Martin Kleppmann",
            Year = 2017,
            Pages = 616,
            Price = 55.90m,
            IsRead = false,
            AddedAt = DateTime.UtcNow
        };

        var book4 = new Book
        {
            Title = "Refactoring",
            Author = "Martin Fowler",
            Year = 2018,
            Pages = 448,
            Price = 47.25m,
            IsRead = false,
            AddedAt = DateTime.UtcNow
        };

        var book5 = new Book
        {
            Title = "Code Complete",
            Author = "Steve McConnell",
            Year = 2004,
            Pages = 960,
            Price = 51.00m,
            IsRead = true,
            AddedAt = DateTime.UtcNow
        };

        var book6 = new Book
        {
            Title = "SQL Antipatterns",
            Author = "Bill Karwin",
            Year = 2010,
            Pages = 328,
            Price = 34.75m,
            IsRead = false,
            AddedAt = DateTime.UtcNow
        };

        db.Books.AddRange(book1, book2, book3, book4, book5, book6);

        Console.WriteLine($"BookId before SaveChanges: {book1.BookId}");

        db.SaveChanges();

        Console.WriteLine($"BookId after SaveChanges: {book1.BookId}");
    }
}