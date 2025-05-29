using Bookshop.API.Exceptions;

namespace Bookshop.API.records.responses;

public record class InvalidDataUpdateBookResponse
{
  public string Message { get; }

  public InvalidDataUpdateBookResponse(InvalidUpdateBookDTOException ex)
  {
    Message = ex.Message;
  }
}
