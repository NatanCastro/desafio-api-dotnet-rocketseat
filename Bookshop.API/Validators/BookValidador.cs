using System;
using Bookshop.API.DTO;
using Bookshop.API.Entity;
using Bookshop.API.Exceptions;
using Bookshop.Shared.Functional;

namespace Bookshop.API.Validators;

public class BookValidador
{

  public Option<List<string>> Validate(Book book)
  {
    var errors = new List<string>();

    if (book.Title == null)
    {
      errors.Add("Title cannot be null");
    }

    if (book.Title != null && book.Title.Length < 3)
    {
      errors.Add("Title must be at least 3 characters");
    }

    if (book.Author == null)
    {
      errors.Add("Author cannot be null");
    }

    if (book.Author != null && book.Author.Length < 3)
    {
      errors.Add("Author must be at least 3 characters");
    }

    if (book.Price < 0)
    {
      errors.Add("Price must be greater than 0");
    }

    if (book.Gerne == null)
    {
      errors.Add("Gerne cannot be null");
    }

    if (book.Gerne != null && book.Gerne.Length < 3)
    {
      errors.Add("Gerne must be at least 3 characters");
    }

    if (book.Quantity < 0)
    {
      errors.Add("Quantity must be greater than or equal to 0");
    }

    return errors.Count != 0 ? Option<List<string>>.Some(errors) : Option<List<string>>.None();
  }
}
