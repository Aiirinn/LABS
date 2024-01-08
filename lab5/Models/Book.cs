using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp5.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    public string Annotation { get; set; }
    public string ISBN { get; set; }
}
