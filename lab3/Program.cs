using ConsoleApp3.Controller;
using ConsoleApp3.Model;
using ConsoleApp3.Repository;
using ConsoleApp3.View;

namespace ConsoleApp3;
public static class Program
{
    public static void Main(string[] args)
    {
        var bookRepository = new BookRepository();
        var library = new List<Book>();
        var bookView = new BookView();
        var bookController = new BookController(library, bookView, bookRepository);
        bookController.Run();
    }
    
    
}
