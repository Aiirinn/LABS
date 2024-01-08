using ConsoleApp5.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp5.DB;

public class BookContext : DbContext
{
    public DbSet<Book>? Books { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=BookStore.db");
    }
        
    
}
