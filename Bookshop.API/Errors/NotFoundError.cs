using System;

namespace Bookshop.API.Errors;

public class NotFoundError(string message)
{
    public string Message { get; } = message;
}
