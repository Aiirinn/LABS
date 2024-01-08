using System;
using Avalonia.Controls;
using Avalonia.Media;
using ConsoleApp5.Models;
using ConsoleApp5.Views;
using ReactiveUI;

namespace ConsoleApp5.ViewModels;

public class DialogFilterViewModel : ViewModelBase
{
    private string _keyword;
    private int _mode;

    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly DialogFilterWindow _dialog;
    private readonly Button _button;

    public DialogFilterViewModel(MainWindowViewModel mwvm, DialogFilterWindow dialog)
    {
        _mainWindowViewModel = mwvm;
        _dialog = dialog;
        _button = _dialog.FindControl<Button>("OkButton");
    }

    public string Keyword
    {
        get => _keyword;
        set => this.RaiseAndSetIfChanged(ref _keyword, value);
    }

    public int Mode
    {
        get => _mode;
        set => this.RaiseAndSetIfChanged(ref _mode, value);
    }

    public void ConfirmAction()
    {
        _mainWindowViewModel.Filter(Keyword, Mode);
    }
}