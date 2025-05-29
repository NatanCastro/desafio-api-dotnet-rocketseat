using System;

namespace Bookshop.API.Exceptions;

public class InvalidUpdateBookDTOException(string[] errors)
  : Exception($"Invalid values to update a book: {string.Join(", ", errors)}")
{
}
