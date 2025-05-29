using Bookshop.API.Exceptions;

namespace Bookshop.API.records.responses;

public record class BookNotFoundResponse
{
  public string Message { get; }

  public BookNotFoundResponse(BookNotFoundException ex)
  {
    Message = ex.Message;
  }
}
