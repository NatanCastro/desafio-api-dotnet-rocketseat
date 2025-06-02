using Bookshop.API.DTO;
using Bookshop.API.Entity;
using Bookshop.API.records.responses;
using Bookshop.API.services;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.API.Controller;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class BookController(BookService bookService) : ControllerBase
{
    private readonly BookService _bookService = bookService;

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult FindBooks()
    {
        return Ok(_bookService.GetAll());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public IActionResult FindBookById([FromRoute] Guid id)
    {
        return _bookService.Get(id).Match<IActionResult>(
            Ok,
            ex => NotFound(new NotFoundResponse(ex))
        );
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(InvalidDataResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateBook([FromBody] CreateBookDTO dto)
    {
        return _bookService.Create(dto).Match<IActionResult>(
            ex => BadRequest(new InvalidDataResponse(ex)),
            Created
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InvalidDataException), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] UpdateBookDTO dto)
    {
        return _bookService.Update(id, dto).Match<IActionResult>(
            ex =>
            {
                return ex.Match<IActionResult>(
                    notFound => NotFound(new NotFoundResponse(notFound)),
                    invalidDto => BadRequest(new InvalidDataResponse(invalidDto))
                );
            },
            NoContent
        );
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public ActionResult DeleteBook([FromRoute] Guid id)
    {
        return _bookService.Delete(id).Match<ActionResult>(
            ex => NotFound(new NotFoundResponse(ex)),
            Ok
        );
    }
}
