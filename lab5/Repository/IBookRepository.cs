
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ConsoleApp5.Models;

namespace ConsoleApp5.Repository;

public interface IBookRepository
{
    void SaveToDb(ObservableCollection<Book> books);
    List<Book> LoadFromDb();
}
