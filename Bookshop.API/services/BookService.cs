using System;
using Bookshop.API.DTO;
using Bookshop.API.Entity;
using Bookshop.API.Exceptions;
using Bookshop.API.records.results;
using Bookshop.API.repositories;
using Bookshop.API.Validators;
using Bookshop.Shared.Functional;

namespace Bookshop.API.services;

public class BookService(BookValidador bookValidador, BookRepository bookRepository)
{
  public List<Book> GetAll() => bookRepository.GetAll();

  public Result<Book, BookNotFoundException> Get(Guid id)
  {
    return bookRepository.Get(id).Match(
      book => Result<Book, BookNotFoundException>.Ok(book),
      () => Result<Book, BookNotFoundException>.Err(new BookNotFoundException(id))
    );
  }

  public Option<InvalidCreateBookDTOException> Create(CreateBookDTO dto)
  {
    Book book = new(dto);

    return bookValidador
      .Validate(book)
      .Match(
        errors => Option<InvalidCreateBookDTOException>.Some(new InvalidCreateBookDTOException(errors)),
        () =>
        {
          bookRepository.Add(book);
          return Option<InvalidCreateBookDTOException>.None();
        }
      );
  }

  public UpdateBookResult Update(Guid id, UpdateBookDTO dto)
  {
    Book book = new(id, dto);
    return bookValidador
      .Validate(book)
      .Match(
        errors => new UpdateBookResult(Option<Exception>.Some(new InvalidUpdateBookDTOException(errors))),
        () =>
        {
          bookRepository.Update(book);
          return new UpdateBookResult();
        }
      );
  }

  public Option<BookNotFoundException> Delete(Guid id)
  {
    return bookRepository.Delete(id).Match(
      book => Option<BookNotFoundException>.None(),
      () => Option<BookNotFoundException>.Some(new BookNotFoundException(id))
    );
  }
}
