namespace Bookshop.API.Errors;

public class InvalidDataError(string message, List<string> errors)
{
    public string Message { get; } = message;
    public List<string> Errors { get; } = errors;
}
