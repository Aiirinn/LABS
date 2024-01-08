using ConsoleApp4Web.Model;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4Web.DB;

public sealed class BookContext : DbContext
{
    public DbSet<Book>? Books { get; set; }

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
