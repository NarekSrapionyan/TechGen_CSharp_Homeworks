using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Project;

public class LibraryContext : DbContext
{
    private const string Cs =
        "Host=localhost;Port=5433;Database=library;Username=admin;Password=admin1234";

    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Cs);

        options.LogTo(
            Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information);

        options.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(e =>
        {
            e.Property(b => b.Title).IsRequired().HasMaxLength(200);
            e.Property(b => b.Author).IsRequired().HasMaxLength(120);
            e.Property(b => b.Price).HasPrecision(10, 2);
            e.HasIndex(b => b.Author);
        });
    }
}