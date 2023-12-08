using ConsoleApp3.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3.DB;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "bookstore.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);

        optionsBuilder.UseSqlite(connection);
    }

    public void CreateBooksTable()
    {
        using (var context = new BookContext())
        {
            context.Database.OpenConnection();
            context.Database.ExecuteSqlRaw("CREATE TABLE IF NOT EXISTS Books (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Author TEXT, Genre TEXT, PublicationDate DATETIME, Annotation TEXT, ISBN TEXT)");

            context.Database.CloseConnection();
        }
    }
}
