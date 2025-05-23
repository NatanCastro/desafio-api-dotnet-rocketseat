namespace MyApi.Tests.API.repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using MyApi.API.repository;
using MyApi.API.model;
using MyApi.API.dto;


public class InMemoryBookRepositoryTest
{
  [Fact(DisplayName = "Should return all books")]
  public Task GetAllBooks()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    // Act
    var result = repository.GetAllBooks();

    // Assert
    Assert.Equal(3, result.Count);
    Assert.Equal(books[0].Id, result[0].Id);
    Assert.Equal(books[1].Id, result[1].Id);
    Assert.Equal(books[2].Id, result[2].Id);
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should return book by id")]
  public Task GetBookById()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    // Act
    var result = repository.GetBookById(books[0].Id);

    // Assert
    Assert.Equal(books[0].Id, result.Unwrap().Id);
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should return none when book not found")]
  public Task FailToGetBookById()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    // Act
    var result = repository.GetBookById(Guid.NewGuid());

    // Assert
    Assert.False(result.IsSome);
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should Create book")]
  public Task CreateBook()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var book = new Book(Guid.NewGuid(), "Title", "Author", "Romance", 100);

    // Act
    var result = repository.CreateBook(book);

    // Assert
    Assert.True(result.Success);
    Assert.Equal(book.Id, result.Unwrap().Id);
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should fail to create book with existing id")]
  public Task FailToCreateBookWithExistingId()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var book = new Book(Guid.NewGuid(), "Title", "Author", "Romance", 100);
    repository.CreateBook(book);

    // Act
    var result = repository.CreateBook(book);

    // Assert
    Assert.False(result.Success);
    Assert.IsType<Exception>(result.UnwrapErr());
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should update book")]
  public Task UpdateBook()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    var bookToUpdate = repository.GetBookById(books[0].Id).Unwrap();
    bookToUpdate.Title = "New Title";

    // Act
    var result = repository.UpdateBook(bookToUpdate);

    // Assert
    Assert.True(result.IsNone);
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should fail to update book with non existing id")]
  public Task FailToUpdateBookWithNonExistingId()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    var bookToUpdate = new Book(Guid.NewGuid(), "Title", "Author", "Romance", 100);

    // Act
    var result = repository.UpdateBook(bookToUpdate);

    // Assert
    Assert.True(result.IsSome);
    Assert.IsType<Exception>(result.Unwrap());
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should delete book")]
  public Task DeleteBook()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    var bookToDelete = repository.GetBookById(books[0].Id).Unwrap();

    // Act
    var result = repository.DeleteBook(bookToDelete.Id);

    // Assert
    Assert.True(result.IsNone);
    return Task.CompletedTask;
  }

  [Fact(DisplayName = "Should fail to delete book with non existing id")]
  public Task FailToDeleteBookWithNonExistingId()
  {
    // Arrange
    var repository = new InMemoryBookRepository();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => repository.CreateBook(b));

    var bookToDelete = new Book(Guid.NewGuid(), "Title", "Author", "Romance", 100);

    // Act
    var result = repository.DeleteBook(bookToDelete.Id);

    // Assert
    Assert.True(result.IsSome);
    Assert.IsType<Exception>(result.Unwrap());
    return Task.CompletedTask;
  }
}