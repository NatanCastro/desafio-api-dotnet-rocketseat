namespace MyApi.API.service;

using System;
using MyApi.API.model;
using MyApi.API.utils.Functional;

public interface IBookService
{
  public List<Book> GetAllBooks();
  public Option<Book> GetBookById(Guid id);
  public Result<Book, Exception> CreateBook(Book book);
  public Option<Exception> UpdateBook(Book book);
  public Option<Exception> DeleteBook(Guid id);
}