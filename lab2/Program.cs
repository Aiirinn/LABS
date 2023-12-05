using ConsoleApp2.Controller;
using ConsoleApp2.Model;
using ConsoleApp2.View;

namespace ConsoleApp2;
public static class Program
{
    public static void Main(string[] args)
    {
        var library = new List<Book>();
        var bookView = new BookView();
        var bookController = new BookController(library, bookView);
        bookController.Run();
    }
    
    
}
