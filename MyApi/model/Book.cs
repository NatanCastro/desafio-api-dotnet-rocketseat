namespace MyApi.model;

using MyApi.dto;


public class Book
{
  public System.Guid Id { get; set; }
  public string Title { get; set; }
  public string Author { get; set; }
  public string Genre { get; set; }
  public uint Price { get; set; }

  public Book(System.Guid id, string title, string author, string genre, uint price)
  {
    Id = id;
    Title = title;
    Author = author;
    Genre = genre;
    Price = price;
  }

  public Book(NewBookDTO newBookDTO)
  {
    Id = System.Guid.NewGuid();
    Title = newBookDTO.Title;
    Author = newBookDTO.Author;
    Genre = newBookDTO.Genre;
    Price = newBookDTO.Price;
  }
}