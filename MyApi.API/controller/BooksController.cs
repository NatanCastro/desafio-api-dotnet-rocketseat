using Microsoft.AspNetCore.Mvc;
using MyApi.API.dto;
using MyApi.API.model;
using MyApi.API.service;

namespace MyApi.API.Controller;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
  private readonly IBookService _bookService;

  public BooksController(IBookService bookService)
  {
    _bookService = bookService;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Book>> GetAll()
  {
    var books = _bookService.GetAllBooks();
    return Ok(books);
  }

  [HttpGet("{id:guid}")]
  public ActionResult<Book> GetById(Guid id)
  {
    var bookOption = _bookService.GetBookById(id);

    return bookOption.Match<ActionResult<Book>>(
        book => Ok(book),
        () => NotFound()
    );
  }

  [HttpPost]
  public ActionResult<Book> Create([FromBody] NewBookDTO dto)
  {
    var book = new Book(Guid.NewGuid(), dto.Title, dto.Author, dto.Genre, dto.Price);
    var result = _bookService.CreateBook(book);

    return result.Match<ActionResult<Book>>(
        b => CreatedAtAction(nameof(GetById), new { id = b.Id }, b),
        ex => Problem(detail: ex.Message)
    );
  }

  [HttpPut("{id:guid}")]
  public IActionResult Update(Guid id, [FromBody] NewBookDTO dto)
  {
    var book = new Book(id, dto.Title, dto.Author, dto.Genre, dto.Price);
    var result = _bookService.UpdateBook(book);

    return result.Match<IActionResult>(
        ex => Problem(detail: ex.Message),
        () => NoContent()
    );
  }

  [HttpDelete("{id:guid}")]
  public IActionResult Delete(Guid id)
  {
    var result = _bookService.DeleteBook(id);

    return result.Match<IActionResult>(
        ex => Problem(detail: ex.Message),
        () => NoContent()
    );
  }
}
