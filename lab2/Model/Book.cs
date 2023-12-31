namespace ConsoleApp2.Model;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public List<string> Genres { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Annotation { get; set; }
    public string ISBN { get; set; }
}
