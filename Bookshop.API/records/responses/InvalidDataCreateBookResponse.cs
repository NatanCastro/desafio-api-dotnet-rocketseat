using Bookshop.API.Exceptions;

namespace Bookshop.API.records.responses;

public record class InvalidDataCreateBookResponse
{

  public string Message { get; }


  public InvalidDataCreateBookResponse(InvalidCreateBookDTOException ex)
  {
    Message = ex.Message;
  }
}
