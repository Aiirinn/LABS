using ConsoleApp4Web.Model;

namespace ConsoleApp4Web.Repository;

public interface IBookRepository
{
    Task Add(Book book);
    Task<List<Book>> Search(string keyword, int mode);

    Task<List<Book>> SearchByKeywords(string keywords);
}
