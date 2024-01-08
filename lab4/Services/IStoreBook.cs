using ConsoleApp4Web.Model;

namespace ConsoleApp4Web.Services;

public interface IStoreBook
{
    Book SetBook(string title, string author, string genres, DateTime publicationDate, string annotation, string isbn);
}
