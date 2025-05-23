namespace MyApi.API.controller;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyApi.API.dto;
using MyApi.API.model;
using MyApi.API.service;
using MyApi.API.utils.Functional;

[ApiController]
[Route("book")]
public class BookController : ControllerBase
{
  private readonly IBookService _bookService;

  public BookController(IBookService bookService)
  {
    _bookService = bookService;
  }

  [HttpGet]
  public List<Book> GetAllBooks()
  {
    return _bookService.GetAllBooks();
  }

  [HttpGet("{id}")]
  public Option<Book> GetBookById(Guid id)
  {
    return _bookService.GetBookById(id);
  }

  [HttpPost]
  public Result<Book, Exception> CreateBook([FromBody] NewBookDTO dto)
  {

    var book = new Book(Guid.NewGuid(), dto.Title, dto.Author, dto.Genre, dto.Price);
    return _bookService.CreateBook(book);
  }
  [HttpPut("{id}")]
  public Option<Exception> UpdateBook(Guid id, [FromBody] NewBookDTO dto)
  {
    var book = new Book(id, dto.Title, dto.Author, dto.Genre, dto.Price);
    return _bookService.UpdateBook(book);
  }

  [HttpDelete("{id}")]
  public Option<Exception> DeleteBook(Guid id)
  {
    return _bookService.DeleteBook(id);
  }
}