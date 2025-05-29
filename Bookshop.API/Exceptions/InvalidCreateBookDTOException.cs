using System;

namespace Bookshop.API.Exceptions;

public class InvalidCreateBookDTOException(List<string> errors)
  : Exception($"Invalid values to create a book: {string.Join(", ", errors)}")
{
}
