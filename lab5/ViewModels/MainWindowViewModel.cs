using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
using ConsoleApp5.DB;
using ConsoleApp5.Models;
using ConsoleApp5.Repository;
using ConsoleApp5.Views;
using DynamicData;
using ReactiveUI;

namespace ConsoleApp5.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IBookRepository _repository;

    public MainWindowViewModel()
    {
        var context = new BookContext();
        _repository = new BookRepository(context);
        var books = _repository.LoadFromDb();
        BookList.AddRange(books);
        BookList = new ObservableCollection<Book>(_repository.LoadFromDb());
        FilteredList = new ObservableCollection<Book>(BookList);
    }

    private int _mode;

    public int Mode
    {
        get => _mode;
        set => this.RaiseAndSetIfChanged(ref _mode, value);
    }

    private string _title;

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private string _author;

    public string Author
    {
        get => _author;
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }

    private string _genre;

    public string Genre
    {
        get => _genre;
        set => this.RaiseAndSetIfChanged(ref _genre, value);
    }

    private int _publicationYear;

    public int PublicationYear
    {
        get => _publicationYear;
        set => this.RaiseAndSetIfChanged(ref _publicationYear, value);
    }

    private string _annotation;

    public string Annotation
    {
        get => _annotation;
        set => this.RaiseAndSetIfChanged(ref _annotation, value);
    }

    private string _isbn;

    public string ISBN
    {
        get => _isbn;
        set => this.RaiseAndSetIfChanged(ref _isbn, value);
    }

    public ObservableCollection<Book> BookList { get; set; } = [];

    public ObservableCollection<Book> FilteredList { get; set; }


    public void ShowAddDialog()
    {
        var dialog = new DialogAddWindow();
        dialog.DataContext = new DialogAddViewModel(this, dialog);

        dialog.Show();
    }

    public void ShowFilterDialog()
    {
        var dialog = new DialogFilterWindow();
        dialog.DataContext = new DialogFilterViewModel(this, dialog);

        dialog.Show();
    }

    public void UpdateFiltered(ObservableCollection<Book> books)
    {
        FilteredList.Clear();
        FilteredList.AddRange(books);
    }

    public async Task AddBook(Book books)
    {
        await Task.Delay(1000);

        BookList.Add(books);
        UpdateFiltered(BookList);
        _repository.SaveToDb(BookList);
    }
    
    public void Filter(string filterWord, int mode)
    {
        FilteredList.Clear();
        var regex = new Regex($@"{filterWord}(\w*)");
        if (filterWord == "")
            FilteredList.AddRange(BookList);
        else 
        {
            switch (mode)
            {
                case 0:
                    foreach (var book in BookList)
                    {
                        if (regex.Matches(book.Title).Count > 0)
                            FilteredList.Add(book);
                    }
                    break;
                case 1:
                    foreach (var book in BookList)
                    {
                        if (regex.Matches(book.Author).Count > 0)
                            FilteredList.Add(book);
                    }
                    break;
                case 2:
                    foreach (var book in BookList)
                    {
                        if (regex.Matches(book.ISBN).Count > 0)
                            FilteredList.Add(book);
                    }
                    break;
                case 3:
                    foreach (var book in BookList)
                    {
                        if (regex.Matches(book.Annotation).Count > 0 || regex.Matches(book.Title).Count > 0)
                            FilteredList.Add(book);
                    }
                    break;
            }
            
        }
    }

    public void Exit()
    {
        Environment.Exit(0);
    }
}