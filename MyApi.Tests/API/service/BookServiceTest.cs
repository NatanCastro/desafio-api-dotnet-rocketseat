namespace MyApi.Tests.API.service;

using Xunit;
using MyApi.API.service;
using MyApi.API.repository;
using Moq;
using MyApi.API.model;
using MyApi.API.utils.Functional;

public class BookServiceTest
{
  [Fact(DisplayName = "BookService: Should return all books")]
  public void GetAllBooks()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    mockRepository.Setup(r => r.GetAllBooks()).Returns([]);
    var service = new BookService(mockRepository.Object);

    // Act
    var result = service.GetAllBooks();

    // Assert
    Assert.Empty(result);
  }

  [Fact(DisplayName = "BookService: Should return book by id")]
  public void GetBookById()
  {
    // Arrange
    var id = Guid.NewGuid();
    var mockBook = new Book(id, "Title", "Author", "Romance", 100);
    var mockRepository = new Mock<IBookRepository>();
    mockRepository.Setup(r => r.GetBookById(id))
    .Returns(Option<Book>.Some(mockBook));
    var service = new BookService(mockRepository.Object);

    // Act
    var result = service.GetBookById(id);

    // Assert
    Assert.True(result.IsSome);
    Assert.Equal(mockBook, result.Unwrap());
  }

  [Fact(DisplayName = "BookService: Should return none when book not found")]
  public void FailToGetBookById()
  {
    // Arrange
    var id = Guid.NewGuid();
    var mockRepository = new Mock<IBookRepository>();
    mockRepository.Setup(r => r.GetBookById(id))
    .Returns(Option<Book>.None());
    var service = new BookService(mockRepository.Object);

    // Act
    var result = service.GetBookById(id);

    // Assert
    Assert.False(result.IsSome);
  }

  [Fact(DisplayName = "BookService: Should Create book")]
  public void CreateBook()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    var book = new Book(Guid.NewGuid(), "Title", "Author", "Romance", 100);
    mockRepository.Setup(r => r.CreateBook(book))
    .Returns(Result<Book, Exception>.Ok(book));
    var service = new BookService(mockRepository.Object);

    // Act
    var result = service.CreateBook(book);

    // Assert
    Assert.True(result.Success);
    Assert.Equal(book.Id, result.Unwrap().Id);
  }

  [Fact(DisplayName = "BookService: Should fail to create book with existing id")]
  public void FailToCreateBookWithExistingId()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    var book = new Book(Guid.NewGuid(), "Title", "Author", "Romance", 100);
    mockRepository.Setup(r => r.CreateBook(book))
    .Returns(Result<Book, Exception>.Err(new Exception()));
    var service = new BookService(mockRepository.Object);

    // Act
    var result = service.CreateBook(book);

    // Assert
    Assert.False(result.Success);
    Assert.IsType<Exception>(result.UnwrapErr());
  }

  [Fact(DisplayName = "BookService: Should update book")]
  public void UpdateBook()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => mockRepository.Setup(r => r.GetBookById(b.Id))
    .Returns(Option<Book>.Some(b)));
    var bookToUpdate = mockRepository.Setup(r => r.UpdateBook(It.IsAny<Book>()))
    .Returns(Option<Exception>.None());
    var service = new BookService(mockRepository.Object);

    var bookToUpdateToUpdate = books[0];
    bookToUpdateToUpdate.Title = "New Title";

    // Act
    var result = service.UpdateBook(bookToUpdateToUpdate);

    // Assert
    Assert.True(result.IsNone);
  }

  [Fact(DisplayName = "BookService: Should fail to update book with non existing id")]
  public void FailToUpdateBookWithNonExistingId()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => mockRepository.Setup(r => r.GetBookById(b.Id))
    .Returns(Option<Book>.Some(b)));
    var bookToUpdate = mockRepository.Setup(r => r.UpdateBook(It.IsAny<Book>()))
    .Returns(Option<Exception>.Some(new Exception()));
    var service = new BookService(mockRepository.Object);

    var bookToUpdateToUpdate = books[0];
    bookToUpdateToUpdate.Title = "New Title";

    // Act
    var result = service.UpdateBook(bookToUpdateToUpdate);

    // Assert
    Assert.True(result.IsSome);
    Assert.IsType<Exception>(result.Unwrap());
  }

  [Fact(DisplayName = "BookService: Should delete book")]
  public void DeleteBook()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => mockRepository.Setup(r => r.GetBookById(b.Id))
    .Returns(Option<Book>.Some(b)));
    var bookToDelete = mockRepository.Setup(r => r.DeleteBook(It.IsAny<Guid>()))
    .Returns(Option<Exception>.None());
    var service = new BookService(mockRepository.Object);

    var bookToDeleteToDelete = books[0];

    // Act
    var result = service.DeleteBook(bookToDeleteToDelete.Id);

    // Assert
    Assert.True(result.IsNone);
  }

  [Fact(DisplayName = "BookService: Should fail to delete book with non existing id")]
  public void FailToDeleteBookWithNonExistingId()
  {
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    var books = new List<Book>
    {
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
      new(Guid.NewGuid(), "Title", "Author", "Romance", 100),
     };
    books.ForEach(b => mockRepository.Setup(r => r.GetBookById(b.Id))
    .Returns(Option<Book>.Some(b)));
    var bookToDelete = mockRepository.Setup(r => r.DeleteBook(It.IsAny<Guid>()))
    .Returns(Option<Exception>.Some(new Exception()));
    var service = new BookService(mockRepository.Object);

    var bookToDeleteToDelete = books[0];

    // Act
    var result = service.DeleteBook(bookToDeleteToDelete.Id);

    // Assert
    Assert.True(result.IsSome);
    Assert.IsType<Exception>(result.Unwrap());
  }
}