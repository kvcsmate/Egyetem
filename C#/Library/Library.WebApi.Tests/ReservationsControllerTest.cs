using Library.Persistence;
using Library.Persistence.DTO;
using Library.Persistence.Services;
using Library.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.WebApi.Tests
{
    public class ReservationsControllerTest : IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly LibraryService _service;
        private readonly ReservationsController _reservationsController;
        public ReservationsControllerTest()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase("TestDb3")
                .Options;
            _context = new LibraryDbContext(options);

            TestDbInitializer.Initialize(_context);
            _service = new LibraryService(_context);
            _reservationsController = new ReservationsController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetReservationsTest()
        {
            // Act
            var result = _reservationsController.GetReservations(111);

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<ReservationDto>>(result.Value);
            Assert.Equal(2, content.Count());

        }
        [Theory]
        [InlineData(1111)]
        [InlineData(1112)]
        public void GetReservationByIdTest(int id)
        {
            // Act
            var result = _reservationsController.GetReservation(id);

            // Assert
            var content = Assert.IsAssignableFrom<ReservationDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidReservationTest()
        {
            // Arrange
            int id = 114;

            // Act
            var result = _reservationsController.GetReservation(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

       
    }
}
