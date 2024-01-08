using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConsoleApp5.DB;
using ConsoleApp5.Models;

namespace ConsoleApp5.Repository;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;
    
    public BookRepository(BookContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    public void SaveToDb(ObservableCollection<Book> tracks)
    {
        _context.Books?.RemoveRange(_context.Books);
        _context.SaveChanges();

        _context.Books?.AddRange(tracks);
        _context.SaveChanges();
    }

    public List<Book> LoadFromDb()
    {
        return _context.Books!.ToList();
    }
}
