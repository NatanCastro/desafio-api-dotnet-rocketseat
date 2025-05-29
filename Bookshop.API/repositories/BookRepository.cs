using System;
using Bookshop.API.Entity;
using Bookshop.Shared.Functional;

namespace Bookshop.API.repositories;

public class BookRepository
{
  List<Book> books = [];

  public List<Book> GetAll() => books;
  public Option<Book> Get(Guid id)
  {
    var book = books.Find(b => b.Id == id);
    return book != null ? Option<Book>.Some(book) : Option<Book>.None();
  }

  public void Add(Book book)
  {
    books.Add(book);
  }

  public Option<Book> Update(Book book)
  {
    var index = books.FindIndex(b => b.Id == book.Id);

    if (index == -1)
      return Option<Book>.None();

    books[index] = book;
    return Option<Book>.Some(book);
  }
  public Option<Book> Delete(Guid id)
  {
    var index = books.FindIndex(b => b.Id == id);

    if (index == -1)
      return Option<Book>.None();

    var book = books[index];
    books.RemoveAt(index);

    return Option<Book>.Some(book);
  }
}
