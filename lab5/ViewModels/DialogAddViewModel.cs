using System;
using Avalonia.Controls;
using Avalonia.Media;
using ConsoleApp5.Models;
using ConsoleApp5.ViewModels;
using ConsoleApp5.Views;
using ReactiveUI;

namespace ConsoleApp5.ViewModels;

public class DialogAddViewModel : ViewModelBase
{
    private string _title;
    private string _author;
    private string _genre;
    private int _publicationYear;
    private string _annotation;
    private string _isbn;
    private int _mode;

    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly DialogAddWindow _dialog;
    private readonly Button _button;

    public DialogAddViewModel(MainWindowViewModel mwvm, DialogAddWindow dialog)
    {
        _mainWindowViewModel = mwvm;
        _dialog = dialog;
        _button = _dialog.FindControl<Button>("OkButton");
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string Author
    {
        get => _author;
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }

    public string Genre
    {
        get => _genre;
        set => this.RaiseAndSetIfChanged(ref _genre, value);
    }

    public int PublicationYear
    {
        get => _publicationYear;
        set => this.RaiseAndSetIfChanged(ref _publicationYear, value);
    }

    public string Annotation
    {
        get => _annotation;
        set => this.RaiseAndSetIfChanged(ref _annotation, value);
    }

    public string ISBN
    {
        get => _isbn;
        set => this.RaiseAndSetIfChanged(ref _isbn, value);
    }

    private bool IsInputValid()
    {
        return Author != "" && Title != "" && Genre != "" && Annotation != "" && ISBN != "";
    }

    public async void ConfirmAction()
    {
        if (IsInputValid())
        {
            _button.Background = Brushes.Chartreuse;
            await _mainWindowViewModel.AddBook(new Book
            {
                Title = Title, Author = Author, PublicationYear = PublicationYear, Genre = Genre,
                Annotation = Annotation, ISBN = ISBN
            });
            _dialog.Close();
        }
        else
        {
            _button.Background = Brushes.Red;
        }
    }
}
