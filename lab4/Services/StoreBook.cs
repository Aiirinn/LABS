using ConsoleApp4Web.Model;

namespace ConsoleApp4Web.Services;

public class StoreBook : IStoreBook
{
    private readonly IValidation _validator;
    public StoreBook(IValidation validator)
    {
        _validator = validator;
    }
    
    public Book SetBook(string title, string author, string genres, DateTime publicationDate, string annotation, string isbn)
    {
        var book = new Book
        {
            Title = title,
            Author = author,
            Genres = genres,
            PublicationDate = publicationDate,
            Annotation = annotation,
            ISBN = isbn
        };

        _validator.ValidateBook(book);
        return book;

    }
}
