using ConsoleApp3.DB;
using ConsoleApp3.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConsoleApp3.Repository;

public class BookRepository : IBookRepository
{
    private readonly string _jsonFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "books.json");

    private BookContext _context = new ();

    public void SaveToDb(List<Book> books)
    {
        _context.Database.EnsureCreated(); // Создать базу данных, если её нет
        _context.Database.ExecuteSqlRaw("DELETE FROM Books");
        foreach (var book in books)
        {
            _context.Books.Add(book);
        }

        _context.SaveChanges();
    }

    public List<Book> LoadFromDb()
    {
        _context.CreateBooksTable();
        return _context.Books.ToList();
    }

    public void SaveToJson(List<Book> books)
    {
        File.WriteAllText(_jsonFilePath, string.Empty);
        var jsonData = JsonConvert.SerializeObject(books, Formatting.Indented);
        File.WriteAllText(_jsonFilePath, jsonData);
    }

    public List<Book> LoadFromJson()
    {
        List<Book> temp=new List<Book>();
        if (!File.Exists(_jsonFilePath))
            File.Create(_jsonFilePath).Close();
        else
        {
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
        }
        return temp;
    }
}
