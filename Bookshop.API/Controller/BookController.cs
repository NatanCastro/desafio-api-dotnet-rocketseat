using Bookshop.API.DTO;
using Bookshop.API.Entity;
using Bookshop.API.enums.results;
using Bookshop.API.Exceptions;
using Bookshop.API.records.responses;
using Bookshop.API.services;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.API.Controller;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class BookController(BookService bookService) : ControllerBase
{
    private readonly BookService bookService = bookService;

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult FindBooks()
    {
        return Ok(bookService.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BookNotFoundResponse), StatusCodes.Status404NotFound)]
    public IActionResult FindBookById([FromRoute] Guid id)
    {
        return bookService.Get(id).Match<IActionResult>(
            book => Ok(book),
            ex => NotFound(new BookNotFoundResponse(ex))
        );
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(InvalidDataCreateBookResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateBook([FromBody] CreateBookDTO dto)
    {
        return bookService.Create(dto).Match<IActionResult>(
                ex => BadRequest(new InvalidDataCreateBookResponse(ex)),
                () => Created()
            );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BookNotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InvalidDataUpdateBookResponse), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] UpdateBookDTO dto)
    {
        var result = bookService.Update(id, dto);

        return result.Type switch
        {
            UpdateBookResultType.OK => Ok(),
            UpdateBookResultType.BOOK_NOT_FOUND => NotFound(
                new BookNotFoundResponse(
                    (result.Exception.Unwrap() as BookNotFoundException)!
                    )
                ),
            UpdateBookResultType.INVALID_UPDATE_BOOK_DTO => BadRequest(
                new InvalidDataUpdateBookResponse(
                    (result.Exception.Unwrap() as InvalidUpdateBookDTOException)!
                    )
                ),
            _ => throw new NotImplementedException()
        };
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BookNotFoundResponse), StatusCodes.Status404NotFound)]
    public ActionResult DeleteBook([FromRoute] Guid id)
    {
        return bookService.Delete(id).Match<ActionResult>(
            ex => NotFound(new BookNotFoundResponse(ex)),
            () => Ok()
        );
    }
}