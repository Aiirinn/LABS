using ConsoleApp3.Controller;
using ConsoleApp3.Model;
using ConsoleApp3.Repository;
using ConsoleApp3.View;
using Moq;

namespace ConsoleApp3.Test;

public class UnitTest1
{
    [Fact]
    public void SearchByKeywordsTest()
    {
        var mock = new Mock<IBookRepository>();
        mock.Setup(r => r.LoadFromDb()).Returns(new List<Book>());
        mock.Setup(j => j.LoadFromJson()).Returns(new List<Book>());
        var library = new List<Book>
        {
            new()
            {

                Title = "Shadow Fight",
                Author = "Chuck Palanik",
                Genres = new List<string>{"Thriller", "Rofls"},
                PublicationDate = new DateTime(1234, 12, 13),
                Annotation = "jfaiofaiowfnioajf",
                ISBN = "93290173"
            },
            new()
            {
                Title = "Fight Club",
                Author = "Chuck Palanik",
                Genres = new List<string>{"Thriller"},
                PublicationDate = new DateTime(1234, 12, 13),
                Annotation = "bro fight jfaiofaiowfnioajf",
                ISBN = "93290173"
            }
        };

        var view = new BookView();
        var controller = new BookController(library, view, mock.Object);
        var actual = controller.SearchByKeywords(new List<string> { "fight", "bro" });
        var expected = new List<SortedBook>
        {
            new()
            {
                Title = "Fight Club",
                Counter = 3,
                IsKeywordInAnnotation = true
            },
            new()
            {
                Title = "Shadow Fight",
                Counter = 1,
                IsKeywordInAnnotation = false
            }
        };
        
        // так как Assert выдавал ошибку при сравнении двух листов, несмотря на то, что они правильные
        // мне пришлось написать все отдельно
        Assert.Equal(expected[0].Counter, actual[0].Counter);
        Assert.Equal(expected[0].IsKeywordInAnnotation, actual[0].IsKeywordInAnnotation);
        Assert.Equal(expected[1].Counter, actual[1].Counter);
        Assert.Equal(expected[1].IsKeywordInAnnotation, actual[1].IsKeywordInAnnotation);

    }
    
    [Fact]
    public void VoidSearchByTitleTest()
    {
        var mock = new Mock<IBookRepository>();
        mock.Setup(r => r.LoadFromDb()).Returns(new List<Book>());
        mock.Setup(j => j.LoadFromJson()).Returns(new List<Book>());
        var library = new List<Book>
        {
            new()
            {

                Title = "Shadow Fight",
                Author = "Chuck Palanik",
                Genres = new List<string>{"Psycho"},
                PublicationDate = new DateTime(1234, 12, 13),
                Annotation = "jfaiofaiowfnioajf",
                ISBN = "93290173"
            },
            new()
            {
                Title = "Fight Club",
                Author = "Chuck Palanik",
                Genres = new List<string>{"Thriller"},
                PublicationDate = new DateTime(1234, 12, 13),
                Annotation = "bro fight jfaiofaiowfnioajf",
                ISBN = "93290173"
            }
        };

        var view = new BookView();
        var controller = new BookController(library, view, mock.Object);

        var actual = controller.Search("Fight Club",1);
        
        Assert.Equal(actual[0].Title, library[1].Title);
        
    }
    
}
