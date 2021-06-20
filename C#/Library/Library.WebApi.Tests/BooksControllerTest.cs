using Library.Persistence;
using Library.Persistence.DTO;
using Library.Persistence.Services;
using Library.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.WebApi.Tests
{
    public class BooksControllerTest : IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly LibraryService _service;
        private readonly BooksController _booksController;
        public BooksControllerTest()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _context = new LibraryDbContext(options);

            TestDbInitializer.Initialize(_context);
            _service = new LibraryService(_context);
            _booksController = new BooksController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetBooksTest()
        {
            // Act
            var result = _booksController.GetBooks();

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<BookDto>>(result.Value);
            Assert.Equal(4, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetBookByIdTest(int id)
        {
            // Act
            var result = _booksController.GetBook(id);

            // Assert
            var content = Assert.IsAssignableFrom<BookDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidBookTest()
        {
            // Arrange
            var id = 5;

            // Act
            var result = _booksController.GetBook(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostBookTest()
        {
            // Arrange
            var newBook = new BookDto { Name = "New book" };
            var count = _context.Books.Count();

            // Act
            var result = _booksController.PostBook(newBook);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<BookDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.Books.Count());
        }

    }
}
