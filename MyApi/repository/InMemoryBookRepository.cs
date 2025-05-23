namespace MyApi.repository;

using System;
using MyApi.model;
using MyApi.utils.Functional;

public class InMemoryBookRepository : IBookRepository
{
  private readonly List<Book> _books = new();

  public List<Book> GetAllBooks()
  {
    return _books;
  }

  public Option<Book> GetBookById(Guid id)
  {
    var book = _books.Find(b => b.Id == id);
    return book == null ? Option<Book>.None() : Option<Book>.Some(book);
  }

  public Result<Book, Exception> CreateBook(Book book)
  {
    if (book.Id == Guid.Empty)
    {
      book.Id = Guid.NewGuid();
    }

    if (_books.Exists(b => b.Id == book.Id))
    {
      return Result<Book, Exception>.Err(new Exception("Book already exists"));
    }

    _books.Add(book);
    return Result<Book, Exception>.Ok(book);
  }

  public Option<Exception> UpdateBook(Book book)
  {
    var bookToUpdate = _books.Find(b => b.Id == book.Id);

    if (bookToUpdate == null)
    {
      return Option<Exception>.Some(new Exception("Book not found"));
    }

    bookToUpdate.Title = book.Title;
    bookToUpdate.Author = book.Author;
    bookToUpdate.Genre = book.Genre;
    bookToUpdate.Price = book.Price;

    return Option<Exception>.None();
  }

  public Option<Exception> DeleteBook(Guid id)
  {
    var bookToDelete = _books.Find(b => b.Id == id);

    if (bookToDelete == null)
    {
      return Option<Exception>.Some(new Exception("Book not found"));
    }

    _books.Remove(bookToDelete);
    return Option<Exception>.None();
  }
}