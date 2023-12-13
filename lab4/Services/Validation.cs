using ConsoleApp4Web.Model;

namespace ConsoleApp4Web.Services;

public class Validation : IValidation
{
    public void ValidateBook(Book book)
    {
        if (book.Title=="" || book.Author=="" || book.Genres=="" || book.PublicationDate.ToString() == "" ||
            book.Annotation=="" || book.ISBN=="")
            throw new Exception("Empty fields are not allowed");
    }
}
