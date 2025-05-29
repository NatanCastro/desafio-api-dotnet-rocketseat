using Bookshop.API.enums.results;
using Bookshop.API.Exceptions;
using Bookshop.Shared.Functional;

namespace Bookshop.API.records.results;

public record class UpdateBookResult
{

  public UpdateBookResultType Type { get; }
  public Option<Exception> Exception { get; }

  public UpdateBookResult()
  {
    Type = UpdateBookResultType.OK;
    Exception = Option<Exception>.None();
  }

  public UpdateBookResult(Option<Exception> exception)
  {
    Exception = exception;
    Type = exception.Match(
      ex => ex switch
      {
        InvalidUpdateBookDTOException => UpdateBookResultType.INVALID_UPDATE_BOOK_DTO,
        BookNotFoundException => UpdateBookResultType.BOOK_NOT_FOUND,
        _ => UpdateBookResultType.OK
      },
      () => UpdateBookResultType.OK
    );
  }
}
