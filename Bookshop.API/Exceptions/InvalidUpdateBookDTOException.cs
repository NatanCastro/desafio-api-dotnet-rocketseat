using System;

namespace Bookshop.API.Exceptions;

public class InvalidUpdateBookDTOException(List<string> errors)
  : Exception($"Invalid values to update a book: {string.Join(", ", errors)}")
{
}
