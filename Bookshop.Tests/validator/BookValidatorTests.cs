using System;
using Bookshop.API.Entity;
using Bookshop.API.Validators;
using Xunit;

namespace Bookshop.Tests.validator;

public class BookValidatorTests
{
  readonly BookValidador bookValidador = new();

  [Fact(DisplayName = "BookValidator - Should return errors when book is invalid")]
  public void ShouldReturnNoneWhenBookIsValid()
  {
    var book = new Book(Guid.NewGuid(), "title", "author", "gerne", 10, 10);
    var result = bookValidador.Validate(book);

    Assert.True(result.IsNone);
  }

  [Fact(DisplayName = "BookValidator - Should return errors for null values")]
  public void ShouldReturnErrorsForNullValues()
  {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    var book = new Book(Guid.NewGuid(), null, null, null, 10, 10);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    var result = bookValidador.Validate(book);

    Assert.True(result.IsSome);
    Assert.Equal(3, result.Unwrap().Count);
    Assert.Equal("Title cannot be null", result.Unwrap()[0]);
    Assert.Equal("Author cannot be null", result.Unwrap()[1]);
    Assert.Equal("Gerne cannot be null", result.Unwrap()[2]);
  }

  [Fact(DisplayName = "BookValidator - Should return errors for too short values")]
  public void ShouldReturnErrorsForTooShortValues()
  {
    var book = new Book(Guid.NewGuid(), "t", "a", "g", 10, 10);
    var result = bookValidador.Validate(book);

    Assert.True(result.IsSome);
    Assert.Equal(3, result.Unwrap().Count);
    Assert.Equal("Title must be at least 3 characters", result.Unwrap()[0]);
    Assert.Equal("Author must be at least 3 characters", result.Unwrap()[1]);
    Assert.Equal("Gerne must be at least 3 characters", result.Unwrap()[2]);
  }

  [Fact(DisplayName = "BookValidator - Should return errors for negative values")]
  public void ShouldReturnErrorsForNegativeValues()
  {
    var book = new Book(Guid.NewGuid(), "title", "author", "gerne", -10, 0);
    var result = bookValidador.Validate(book);

    Assert.True(result.IsSome);
    Assert.Single(result.Unwrap());
    Assert.Equal("Price must be greater than 0", result.Unwrap()[0]);
  }
}
