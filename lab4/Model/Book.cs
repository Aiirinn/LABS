using System.ComponentModel.DataAnnotations;

namespace ConsoleApp4Web.Model;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genres { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Annotation { get; set; }
    public string ISBN { get; set; }
}
