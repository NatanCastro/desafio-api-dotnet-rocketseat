namespace MyApi.repository;

using System;
using MyApi.model;
using MyApi.utils.Functional;

public interface IBookRepository
{
  public List<Book> GetAllBooks();
  public Option<Book> GetBookById(Guid id);
  public Result<Book, Exception> CreateBook(Book book);
  public Option<Exception> UpdateBook(Book book);
  public Option<Exception> DeleteBook(Guid id);
}
