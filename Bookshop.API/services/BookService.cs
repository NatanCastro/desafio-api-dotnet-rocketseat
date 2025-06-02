using System;
using Bookshop.API.DTO;
using Bookshop.API.Entity;
using Bookshop.API.Errors;
using Bookshop.API.repositories;
using Bookshop.API.Validators;
using Bookshop.Shared.Functional;
using OneOf;

namespace Bookshop.API.services;

using UpdateBookError = OneOf<NotFoundError, InvalidDataError>;

public class BookService(BookValidador bookValidador, BookRepository bookRepository)
{
    public List<Book> GetAll()
    {
        return bookRepository.GetAll();
    }

    public Result<Book, NotFoundError> Get(Guid id)
    {
        return bookRepository.Get(id).Match(
            book => Result<Book, NotFoundError>.Ok(book),
            () => Result<Book, NotFoundError>.Err(new NotFoundError($"Book with {id} was not found."))
        );
    }

    public Option<InvalidDataError> Create(CreateBookDTO dto)
    {
        Book book = new(dto);

        return BookValidador.Validate(book)
            .Match(
                errors => Option<InvalidDataError>.Some(
                    new InvalidDataError("invalid data to create book.", errors)),
                () =>
                {
                    bookRepository.Add(book);
                    return Option<InvalidDataError>.None();
                }
            );
    }

    public Option<UpdateBookError> Update(Guid id, UpdateBookDTO dto)
    {
        Book book = new(id, dto);
        return BookValidador.Validate(book)
            .Match(
                errors => Option<UpdateBookError>.Some(
                    new InvalidDataError($"Could not update with ID: {id}", errors)),
                () =>
                {
                    bookRepository.Update(book);
                    return Option<UpdateBookError>.None();
                }
            );
    }

    public Option<NotFoundError> Delete(Guid id)
    {
        return bookRepository.Delete(id).Match(
            book => Option<NotFoundError>.None(),
            () => Option<NotFoundError>.Some(new NotFoundError($"Book with {id} was not found."))
        );
    }
}
