using Microsoft.EntityFrameworkCore;

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

            GetAllBooks(db);
            GetBooksAfter2005(db);
            GetBooksByMartinFowler(db);
            GetLongestBook(db);
            HasUnreadBooks(db);
            CountReadBooks(db);
            GetBookTitlesAndYears(db);
            GetSecondPage(db);
            FindBookTwice(db);
            GetUnreadBooksTotalPrice(db);
            SearchAuthors(db);
            
            MarkBookAsRead(db);
            DeleteBook(db);
            
            TestLocalDateTime(db);
            ShowCreateScript(db);
            GroupBooksByAuthor(db);
            
            ShowQueryString(db);
            CompareTracking(db);
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
    
    static void GetAllBooks(LibraryContext db)
    {
        Console.WriteLine("All books ordered by year");

        var books = db.Books
            .OrderBy(b => b.Year)
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Year} - {book.Title} - {book.Author}");
        }
    }
    static void GetBooksAfter2005(LibraryContext db)
    {
        Console.WriteLine("Books published after 2005");

        var books = db.Books
            .Where(b => b.Year > 2005)
            .OrderBy(b => b.Title)
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Year} - {book.Title}");
        }
    }
    
    static void GetBooksByMartinFowler(LibraryContext db)
    {
        Console.WriteLine("Books by Martin Fowler");

        var books = db.Books
            .Where(b => b.Author == "Martin Fowler")
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} - {book.Year}");
        }
    }
    
    static void GetLongestBook(LibraryContext db)
    {
        Console.WriteLine("Longest book");

        var book = db.Books
            .OrderByDescending(b => b.Pages)
            .First();

        Console.WriteLine($"{book.Title} - {book.Pages} pages");
    }
    
    static void HasUnreadBooks(LibraryContext db)
    {
        Console.WriteLine("Has unread books");

        var hasUnread = db.Books.Any(b => !b.IsRead);

        Console.WriteLine(hasUnread);
    }
    
    static void CountReadBooks(LibraryContext db)
    {
        Console.WriteLine("Read books count");

        var count = db.Books.Count(b => b.IsRead);

        Console.WriteLine(count);
    }
    
    static void GetBookTitlesAndYears(LibraryContext db)
    {
        Console.WriteLine("Book titles and years");

        var books = db.Books
            .Select(b => new { b.Title, b.Year })
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} - {book.Year}");
        }

        Console.WriteLine($"Tracked entries: {db.ChangeTracker.Entries().Count()}");
    }
    
    static void GetSecondPage(LibraryContext db)
    {
        Console.WriteLine("Second page");

        var books = db.Books
            .OrderBy(b => b.Title)
            .Skip(3)
            .Take(3)
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine(book.Title);
        }
    }
    
    static void FindBookTwice(LibraryContext db)
    {
        Console.WriteLine("Find book twice");

        var book1 = db.Books.Find(1);
        var book2 = db.Books.First(b => b.BookId == 1);

        Console.WriteLine($"Same object: {ReferenceEquals(book1, book2)}");
    }
    
    static void GetUnreadBooksTotalPrice(LibraryContext db)
    {
        Console.WriteLine("Total price of unread books");

        var totalPrice = db.Books
            .Where(b => b.IsRead == false)
            .Sum(b => b.Price);

        Console.WriteLine($"Total price: {totalPrice}");
    }
    
    static void SearchAuthors(LibraryContext db)
    {
        Console.WriteLine("Search authors with Contains");

        var books1 = db.Books
            .Where(b => b.Author.Contains("martin"))
            .ToList();

        foreach (var book in books1)
        {
            Console.WriteLine(book.Author);
        }

        Console.WriteLine($"Found: {books1.Count}");

        Console.WriteLine("Search authors with ILike");

        var books2 = db.Books
            .Where(b => EF.Functions.ILike(b.Author, "%martin%"))
            .ToList();

        foreach (var book in books2)
        {
            Console.WriteLine(book.Author);
        }

        Console.WriteLine($"Found: {books2.Count}");
    }
    
    static void MarkBookAsRead(LibraryContext db)
    {
        Console.WriteLine("Mark book as read");

        var book = db.Books.First(b => b.Title == "SQL Antipatterns");

        Console.WriteLine($"State before: {db.Entry(book).State}");

        book.IsRead = true;

        Console.WriteLine($"State after: {db.Entry(book).State}");

        db.SaveChanges();
    }
    
    static void DeleteBook(LibraryContext db)
    {
        Console.WriteLine("Delete book");

        var book = db.Books.First(b => b.Title == "The Pragmatic Programmer");

        db.Books.Remove(book);
        db.SaveChanges();

        var count = db.Books.Count();

        Console.WriteLine($"Remaining books: {count}");
    }
    
    static void AddExistingBook(LibraryContext db)
    {
        Console.WriteLine("Add existing book");

        var book = db.Books.First(b => b.Title == "SQL Antipatterns");

        book.IsRead = true;

        db.Books.Add(book);
        db.SaveChanges();
    }
    
    static void TestLocalDateTime(LibraryContext db)
    {
        Console.WriteLine("Test local DateTime");

        var book = new Book
        {
            Title = "Test Book",
            Author = "Test Author",
            Year = 2026,
            Pages = 100,
            Price = 10.00m,
            IsRead = false,
            AddedAt = DateTime.Now
        };

        db.Books.Add(book);

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            db.ChangeTracker.Clear();
        }
    }
    
    static void ShowCreateScript(LibraryContext db)
    {
        Console.WriteLine("Create script");

        Console.WriteLine(db.Database.GenerateCreateScript());
    }
    
    static void GroupBooksByAuthor(LibraryContext db)
    {
        Console.WriteLine("Books grouped by author");

        var authors = db.Books
            .GroupBy(b => b.Author)
            .Select(g => new
            {
                Author = g.Key,
                Count = g.Count(),
                Total = g.Sum(b => b.Price)
            })
            .OrderByDescending(a => a.Count)
            .ToList();

        foreach (var author in authors)
        {
            Console.WriteLine($"{author.Author} - {author.Count} - {author.Total}");
        }
    }
    
    static void ShowQueryString(LibraryContext db)
    {
        Console.WriteLine("Query SQL");

        var query = db.Books
            .Where(b => b.Year > 2005)
            .OrderBy(b => b.Title);

        Console.WriteLine(query.ToQueryString());
    }
    
    static void CompareTracking(LibraryContext db)
    {
        Console.WriteLine("Normal tracking");

        db.ChangeTracker.Clear();

        var books1 = db.Books
            .OrderBy(b => b.Year)
            .ToList();

        Console.WriteLine($"Tracked entries: {db.ChangeTracker.Entries().Count()}");

        db.ChangeTracker.Clear();

        Console.WriteLine("No tracking");

        var books2 = db.Books
            .AsNoTracking()
            .OrderBy(b => b.Year)
            .ToList();

        Console.WriteLine($"Tracked entries: {db.ChangeTracker.Entries().Count()}");
    }
}   