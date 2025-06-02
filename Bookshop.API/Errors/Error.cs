namespace Bookshop.API.Errors;

public class Error(string message)
{
    public string Message { get; set; } = message;
}
