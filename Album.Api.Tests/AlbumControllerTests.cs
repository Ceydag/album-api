using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Album.Api.Controllers;
using Album.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Album.Api.Database;

namespace Album.Api.Tests
{
    public class AlbumControllerTests
    {
        private List<Models.Album> GetTestAlbums()
        {
            var albums = new List<Models.Album>
            {
                new Models.Album { Id = 1, Name = "Test Album 1", Artist = "Test Artist 1", ImageUrl = "Test Url 1" },
                new Models.Album { Id = 2, Name = "Test Album 2", Artist = "Test Artist 2", ImageUrl = "Test Url 2" }
            };

            return albums;
        }


        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            // Arrange
            var mockContext = new Mock<AlbumContext>();
            var controller = new AlbumController(mockContext.Object);

            // Act
            var result = await controller.Details(0);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Models.Album>>();
            var mockContext = new Mock<AlbumContext>();
            mockContext.Setup(c => c.Albums).Returns(mockSet.Object);

            var controller = new AlbumController(mockContext.Object);

            // Act
            var result = await controller.Create(new Models.Album { Id = 1, Name = "Test Album", Artist = "Test Artist", ImageUrl = "Test Url" });

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("Details", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsBadRequest_WhenIdDoesNotMatchAlbumId()
        {
            // Arrange
            var mockContext = new Mock<AlbumContext>();
            var controller = new AlbumController(mockContext.Object);

            // Act
            var result = await controller.Edit(1, new Models.Album { Id = 2 });

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            // Arrange
            var mockContext = new Mock<AlbumContext>();
            var controller = new AlbumController(mockContext.Object);

            // Act
            var result = await controller.Delete(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult_WhenIdIsValid()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Models.Album>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(GetTestAlbums().First());

            var mockContext = new Mock<AlbumContext>();
            mockContext.Setup(c => c.Albums).Returns(mockSet.Object);

            var controller = new AlbumController(mockContext.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
