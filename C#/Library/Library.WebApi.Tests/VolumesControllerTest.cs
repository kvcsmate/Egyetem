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
   public class VolumesControllerTest:IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly LibraryService _service;
        private readonly VolumesController _volumesController;
        public VolumesControllerTest()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase("TestDb2")
                .Options;
            _context = new LibraryDbContext(options);

            TestDbInitializer.Initialize(_context);
            _service = new LibraryService(_context);
            _volumesController = new VolumesController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetVolumesTest()
        {
            // Act
            var result = _volumesController.GetVolumes(1);

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<VolumeDto>>(result.Value);
            Assert.Equal(3, content.Count());
            
        }
        [Theory]
        [InlineData(111)]
        [InlineData(112)]
        [InlineData(113)]
        public void GetVolumeByIdTest(int id)
        {
            // Act
            var result = _volumesController.GetVolume(id);

            // Assert
            var content = Assert.IsAssignableFrom<VolumeDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidVolumeTest()
        {
            // Arrange
            int id = 114;

            // Act
            var result = _volumesController.GetVolume(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostVolumeTest()
        {
            // Arrange
            var newVolume = new VolumeDto { Id=115, BookId=1 };
            var count = _context.Volumes.Count();

            // Act
            var result = _volumesController.PostVolume(newVolume);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<VolumeDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.Volumes.Count());
        }
        /*
        */
    }
}
