using System.Text.RegularExpressions;
using ConsoleApp3.Model;
using ConsoleApp3.Repository;
using ConsoleApp3.View;

namespace ConsoleApp3.Controller;

public class BookController
{
    private List<Book> _books;
    private readonly IBookView _bookView;
    private readonly IBookRepository _bookRepository;

    public BookController(List<Book> books, IBookView bookView, IBookRepository bookRepository)
    {
        _books = books;
        _bookView = bookView;
        _bookRepository = bookRepository;
    }

    public List<Book> Search(string keyword, int mode)
    {
        var tempBooks = new List<Book>();
        foreach (var book in _books)
        {
            switch (mode)
            {
                case 1:
                    if (book.Title.ToLower() == keyword.ToLower())
                    {
                        tempBooks.Add(book);
                    }

                    break;
                case 2:
                    if (book.Author.ToLower() == keyword.ToLower())
                    {
                        tempBooks.Add(book);
                    }

                    break;
                case 3:
                    if (book.ISBN.ToLower() == keyword.ToLower())
                    {
                        tempBooks.Add(book);
                    }

                    break;
            }
        }

        if (tempBooks.Count == 0)
            Console.WriteLine("По вашему запросу ничего не найдено");
        return tempBooks;
    }


    public List<SortedBook> SearchByKeywords(List<string> keywords) // Реализация поиска по ключевым словам
    {
        var tempArray = new List<SortedBook>(); // вспомогательный список
        foreach (var book in _books)
        {
            var sortedBook = new SortedBook(); // объект для сортировки
            var counter = 0; // счетчик ключевых слов
            foreach (var keyword in keywords)
            {
                var regex = new Regex($@"{keyword}(\w*)"); // регулярное выражение
                counter += regex.Matches(book.Title?.ToLower() ?? "").Count;

                if (regex.Matches(book.Annotation?.ToLower() ?? "").Count > 0) // если слово найдено в аннотации
                {
                    counter += regex.Matches(book.Annotation?.ToLower() ?? "").Count;
                    sortedBook.IsKeywordInAnnotation = true;
                }

                if (counter > 0)
                {
                    sortedBook.Counter = counter;
                    sortedBook.Title = book.Title;
                }
            }

            tempArray.Add(sortedBook);
        }

        if (tempArray.Count==0)
            Console.WriteLine("По вашему запросу ничего не найдено");
        else
            tempArray.Sort((b1, b2) => b2.Counter.CompareTo(b1.Counter));
        
        return tempArray;
    }

    private void AddBook(Book book) // добавление книги
    {
        _books.Add(book);
    }

    public void Run()
    {
        var mode = _bookView.SearchSaveAndLoadMode();
        switch (mode)
        {
            case 1:
                _books = _bookRepository.LoadFromDb();
                break;
            case 2:
                _books = _bookRepository.LoadFromJson();
                break;
        }

        var running = true;
        while (true)
        {
            var menu = _bookView.GetMenuChoice();
            switch (menu)
            {
                case 1:
                    AddBook(_bookView.BookQuery());
                    break;
                case 2:
                    int search = _bookView.SearchChoiceQuery();
                    string query;
                    switch (search)
                    {
                        case 1:
                            query = _bookView.SearchQuery();
                            _bookView.DisplayBooksFull(Search(query, 1));
                            break;
                        case 2:
                            query = _bookView.SearchQuery();
                            _bookView.DisplayBooksFull(Search(query, 2));
                            break;
                        case 3:
                            query = _bookView.SearchQuery();
                            _bookView.DisplayBooksFull(Search(query, 3));
                            break;
                        case 4:
                            List<string> queryList = _bookView.KeywordsQuery();
                            _bookView.DisplayBooksShort(_books, SearchByKeywords(queryList));
                            break;
                        case 5:
                            break;
                    }

                    break;
                case 3:
                    running = !running;
                    break;
            }

            if (!running)
            {
                switch (mode)
                {
                    case 1:
                        _bookRepository.SaveToDb(_books);
                        break;
                    case 2:
                        _bookRepository.SaveToJson(_books);
                        break;
                }

                break;
            }
        }
    }
}
