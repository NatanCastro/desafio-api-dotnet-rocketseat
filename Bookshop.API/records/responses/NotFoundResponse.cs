using Bookshop.API.Errors;

namespace Bookshop.API.records.responses;

public record class NotFoundResponse
{
    public string Message { get; }

    public NotFoundResponse(NotFoundError ex)
    {
        Message = ex.Message;
    }
}
