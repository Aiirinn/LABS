using ConsoleApp3.Model;

namespace ConsoleApp3.View;

public class BookView:IBookView
{

    public  void DisplayBooksFull(List<Book> books) // Отображение информации о книгах
    {
        foreach (var book in books)
        {
            Console.WriteLine($"Название: {book.Title}; Автор: {book.Author}; Жанры: {String.Join(", ", book.Genres)}; Дата публикации:" +
                              $" {book.PublicationDate}; Аннотация: {book.Annotation}; ISBN: {book.ISBN}");
        }
    }
    
    public  void DisplayBooksShort(List<Book> books, List<SortedBook> sortedBooks) // Отображение информации о книгах
    {
        foreach (var sortedBook in sortedBooks)
        {
            foreach (var book in books)
            {
                if (book.Title == sortedBook.Title)
                {
                    var annot = "";
                    if (sortedBook.IsKeywordInAnnotation)
                        annot = "Ключевое слово найдено в аннотации!";
                    Console.WriteLine($"Название: {book.Title}; Автор: {book.Author}; Жанры: {book.Genres}; Дата публикации:" +
                                     $" {book.PublicationDate}; ISBN: {book.ISBN}. {annot}");
                }
            }
        }
    }

    public  int GetMenuChoice() // Взаимодействие с пользователем для выбора пунктов меню
    {
        Console.WriteLine(
            "Выберите действие:\n1. Добавление книги в каталог.\n2. Выборка информации по конкретной книге.\n3. Выход.");
        Console.Write("> ");
        var isValid = int.TryParse(Console.ReadLine(), out var n);
        if (!isValid || n < 1 || n > 3)
            throw new Exception("Введено неверное значение");
        return n;
    }

    public  string SearchQuery()
    {
        Console.Write("Введите запрос: ");
        var input = Console.ReadLine() ?? "";
        if (input == "")
            throw new Exception("Пустая строка недопустима.");
        return input;
    }

    public Book BookQuery()
    {
        var book = new Book();
        var tempList = new List<string>();
        
        Console.WriteLine("Введите название: ");
        var input = Console.ReadLine() ?? "";
        if (input == "")
            throw new Exception("Пустая строка недопустима.");
        book.Title = input;

        Console.WriteLine("Введите имя автора: ");
        input = Console.ReadLine() ?? "";
        if (input == "")
            throw new Exception("Пустая строка недопустима.");
        book.Author = input;

        while (true)
        {
            Console.WriteLine("Введите жанры книги: ");
            input = Console.ReadLine() ?? "";
            if (input == "")
                break;
            tempList.Add(input);

        }

        if (!tempList.Any())
            throw new Exception("Пустая строка недопустима.");
        book.Genres = tempList;

        Console.WriteLine("Введите дату публикации: ");
        input = Console.ReadLine() ?? "";
        book.PublicationDate = Convert.ToDateTime(input);

        Console.WriteLine("Введите аннотацию: ");
        input = Console.ReadLine() ?? "";
        if (input == "")
            throw new Exception("Пустая строка недопустима.");
        book.Annotation = input;

        Console.WriteLine("Введите ISBN: ");
        input = Console.ReadLine() ?? "";
        if (input == "")
            throw new Exception("Пустая строка недопустима.");
        book.ISBN = input;

        return book;

    }

    public int SearchChoiceQuery()
    {
        Console.WriteLine(
            "Выберите поиск:\n1. По названию.\n2. По имени автора.\n3. По ISBN.\n4. По ключевым словам.\n5. Выход.");
        Console.Write("> ");
        var isValid = int.TryParse(Console.ReadLine(), out var n);
        if (!isValid || n < 1 || n > 4)
            throw new Exception("Введено неверное значение");
        return n;
    }

    public List<string> KeywordsQuery()
    {
        var tempList = new List<string>();
        while (true)
        {
            Console.WriteLine("Введите ключевые слова: ");
            var input = Console.ReadLine() ?? "";
            if (input == "")
                break;
            tempList.Add(input);
        }

        return tempList;
    }

    public int SearchSaveAndLoadMode()
    {
        Console.WriteLine(
            "Выберите способ загрузки/сохранения данных:\n1. Database.\n2. JSON.");
        Console.Write("> ");
        var isValid = int.TryParse(Console.ReadLine(), out var n);
        if (!isValid || n < 1 || n > 2)
            throw new Exception("Введено неверное значение");
        return n;
    }
}
