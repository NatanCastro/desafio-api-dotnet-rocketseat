using System;
using Bookshop.API.DTO;

namespace Bookshop.API.Entity;

public class Book
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Author { get; set; }

  public string Gerne { get; set; }

  public decimal Price { get; set; }

  public uint Quantity { get; set; }

  public Book(Guid id, string title, string author, string gerne, decimal price, uint quantity)
  {
    Id = id;
    Title = title;
    Author = author;
    Gerne = gerne;
    Price = price;
    Quantity = quantity;
  }

  public Book(CreateBookDTO dto)
  {
    Id = Guid.NewGuid();
    Title = dto.Title;
    Author = dto.Author;
    Gerne = dto.Gerne;
    Price = dto.Price;
    Quantity = dto.Quantity;
  }

  public Book(Guid id, UpdateBookDTO dto)
  {
    Id = id;
    Title = dto.Title;
    Author = dto.Author;
    Gerne = dto.Gerne;
    Price = dto.Price;
    Quantity = dto.Quantity;
  }
}
