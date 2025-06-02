using Bookshop.API.Errors;

namespace Bookshop.API.records.responses;

public record class InvalidDataResponse
{
    public string Message { get; }
    public List<string> Errors { get; }

    public InvalidDataResponse(InvalidDataError ex)
    {
        Message = ex.Message;
        Errors = ex.Errors;
    }
}
