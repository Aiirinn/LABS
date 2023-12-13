using System.Linq.Expressions;
using ConsoleApp4Web.DB;
using ConsoleApp4Web.Model;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4Web.Repository;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;

    public BookRepository(BookContext context)
    {
        _context = context;
    }

    public Task Add(Book book)
    {
        if (!_context.Books!.Any(b => b.Title == book.Title && b.Author == book.Author && b.Genres == book.Genres &&
                                      b.PublicationDate == book.PublicationDate && b.Annotation == book.Annotation &&
                                      b.ISBN == book.ISBN))
        {
            _context.Books?.Add(book);
            return _context.SaveChangesAsync();
        }

        throw new Exception("Книга уже существует.");
    }

    public Task<List<Book>> Search(string keyword, int mode)
    {
        return mode switch
        {
            1 => _context.Books!.Where(w => w.Title.ToLower() == keyword.ToLower()).ToListAsync(),
            2 => _context.Books!.Where(w => w.Author.ToLower() == keyword.ToLower()).ToListAsync(),
            3 => _context.Books!.Where(w => w.ISBN.ToLower() == keyword.ToLower()).ToListAsync(),
            _ => throw new Exception("Неизвестный режим / книга не найдена.")
        };
    }

    public async Task<List<Book>> SearchByKeywords(string keywords) // Реализация поиска по ключевым словам
    {
        var keywordList = keywords.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


        var parameter = Expression.Parameter(typeof(Book), "book");
        var titleContains = BuildContainsExpression(parameter, "Title", keywordList);
        var annotationContains = BuildContainsExpression(parameter, "Annotation", keywordList);

        Expression<Func<Book, bool>> combinedExpression = Expression.Lambda<Func<Book, bool>>(
            Expression.OrElse(titleContains.Body, annotationContains.Body), parameter);

        var result = await _context.Books!.Where(combinedExpression).ToListAsync();

        return result;
    }
    
    private Expression<Func<Book, bool>> BuildContainsExpression(ParameterExpression parameter, 
        string propertyName, string[] values)
    {
        var property = Expression.Property(parameter, propertyName);
        var nullCheck = Expression.NotEqual(property, Expression.Constant(null));
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        Expression body = null;
        foreach (var value in values)
        {
            var valueExpression = Expression.Constant(value, typeof(string));
            var containsCall = Expression.Call(property, containsMethod, valueExpression);
            var valueIsNotNull = Expression.AndAlso(nullCheck, 
                Expression.NotEqual(property, Expression.Constant(string.Empty)));
            var containsWithCheck = Expression.AndAlso(valueIsNotNull, containsCall);
            body = body == null ? containsWithCheck : Expression.Or(body, containsWithCheck);
        }

        return Expression.Lambda<Func<Book, bool>>(body, parameter);
    }
}
