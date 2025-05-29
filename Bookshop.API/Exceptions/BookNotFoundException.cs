using System;

namespace Bookshop.API.Exceptions;

public class BookNotFoundException(Guid id) : Exception($"Book with id {id} not found")
{
}
