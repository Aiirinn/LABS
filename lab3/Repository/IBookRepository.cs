using ConsoleApp3.Model;

namespace ConsoleApp3.Repository;

public interface IBookRepository
{
    void SaveToDb(List<Book> books);
    List<Book> LoadFromDb();
    void SaveToJson(List<Book> books);
    List<Book> LoadFromJson();
}