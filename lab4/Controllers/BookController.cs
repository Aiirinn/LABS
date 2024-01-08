using ConsoleApp4Web.Model;
using ConsoleApp4Web.Repository;
using ConsoleApp4Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp4Web.Controllers;

[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    [HttpPost]
    [Route("/add-book")]
    public Task Add([FromBody] string fake, string title, string author, string genres, DateTime publicationDate, string annotation, string isbn)
    {
        var validator = new Validation();
        var storeWord = new StoreBook(validator);
        var book = storeWord.SetBook(title, author, genres, publicationDate, annotation, isbn);

        return _bookRepository.Add(book);
    }
    
    [HttpGet]
    [Route("/search-book")]
    public Task<List<Book>> SearchBook(string keyword, int mode)
    {
        return _bookRepository.Search(keyword, mode);
    }
    
    [HttpGet]
    [Route("/search-book-by-keyword")]
    public Task<List<Book>> SearchBookByKeyword(string keywords)
    {
        return _bookRepository.SearchByKeywords(keywords);
    }
}
