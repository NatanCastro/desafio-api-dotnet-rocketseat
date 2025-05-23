namespace MyApi.dto;


public class NewBookDTO(string title, string author, string genre, uint price)
{
  public string Title { get; set; } = title;
  public string Author { get; set; } = author;
  public string Genre { get; set; } = genre;
  public uint Price { get; set; } = price;
}