using System;

namespace Bookshop.API.DTO;

public class CreateBookDTO(string title, string author, string gerne, decimal price, uint quantity)
{
  public string Title { get; set; } = title;
  public string Author { get; set; } = author;
  public string Gerne { get; set; } = gerne;
  public decimal Price { get; set; } = price;
  public uint Quantity { get; set; } = quantity;
}
