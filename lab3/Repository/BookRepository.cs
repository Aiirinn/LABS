using ConsoleApp3.DB;
using ConsoleApp3.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConsoleApp3.Repository;

public class BookRepository : IBookRepository
{
    private readonly string _jsonFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "books.json");

    private readonly BookContext _context = new();

    public void SaveToDb(List<Book> books)
    {
        _context.Database.ExecuteSqlRaw("DELETE FROM Books");
        _context.Books.AddRange(books);

        _context.SaveChanges();
    }

    public List<Book> LoadFromDb()
    {
        _context.Database.EnsureCreated();
        return _context.Books.ToList();
    }

    public void SaveToJson(List<Book> books)
    {
        var jsonData = JsonConvert.SerializeObject(books, Formatting.Indented);
        File.WriteAllText(_jsonFilePath, jsonData);
    }

    public List<Book> LoadFromJson()
    {
        List<Book> temp = new List<Book>();
        File.Create(_jsonFilePath).Close();

        var jsonData = File.ReadAllText(_jsonFilePath);
        var data = JsonConvert.DeserializeObject<List<Book>>(jsonData) ??
                   new List<Book>();
        foreach (var book in data)
        {
            temp.Add(new Book
            {
                Title = book.Title,
                Author = book.Author,
                Genres = book.Genres,
                PublicationDate = book.PublicationDate,
                Annotation = book.Annotation,
                ISBN = book.ISBN
            });
        }

        return temp;
    }
}
