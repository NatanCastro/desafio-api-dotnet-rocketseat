namespace MyApi.API.service;

using System;
using System.Collections.Generic;
using MyApi.API.model;
using MyApi.API.repository;
using MyApi.API.utils.Functional;

public class BookService(IBookRepository repository) : IBookService
{
  private readonly IBookRepository _repository = repository;

  public Result<Book, Exception> CreateBook(Book book)
  {
    return _repository.CreateBook(book);
  }

  public Option<Exception> DeleteBook(Guid id)
  {
    return _repository.DeleteBook(id);
  }

  public List<Book> GetAllBooks()
  {
    return _repository.GetAllBooks();
  }

  public Option<Book> GetBookById(Guid id)
  {
    return _repository.GetBookById(id);
  }

  public Option<Exception> UpdateBook(Book book)
  {
    return _repository.UpdateBook(book);
  }
}