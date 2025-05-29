using System;

namespace Bookshop.API.Exceptions;

public class InvalidCreateBookDTOException(string[] errors)
  : Exception($"Invalid values to create a book: {string.Join(", ", errors)}")
{
}
