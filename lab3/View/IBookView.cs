using ConsoleApp3.Model;

namespace ConsoleApp3.View;

public interface IBookView
{
    void DisplayBooksFull(List<Book> books);
    void DisplayBooksShort(List<Book> books, List<SortedBook> sortedBooks);
    int GetMenuChoice();
    string SearchQuery();
    Book BookQuery();
    int SearchChoiceQuery();
    List<string> KeywordsQuery();
    int SearchSaveAndLoadMode();
}