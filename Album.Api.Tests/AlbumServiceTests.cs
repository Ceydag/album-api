using Album.Api.Database;
using Album.Api.Models;
using Album.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Album.Api.Tests.Services
{
    public class AlbumServiceTests
    {
        private readonly Mock<AlbumContext> _mockContext;
        private readonly AlbumService _albumService;

        public AlbumServiceTests()
        {
            _mockContext = new Mock<AlbumContext>();
            _albumService = new AlbumService(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllAlbums_ReturnsAllAlbums()
        {
            // Arrange
            var albums = new List<Models.Album>
            {
                new Models.Album { Id = 1, Name = "Album1", Artist = "Artist1", ImageUrl = "Url1" },
                new Models.Album { Id = 2, Name = "Album2", Artist = "Artist2", ImageUrl = "Url2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Models.Album>>();
            mockSet.As<IQueryable<Models.Album>>().Setup(m => m.Provider).Returns(albums.Provider);
            mockSet.As<IQueryable<Models.Album>>().Setup(m => m.Expression).Returns(albums.Expression);
            mockSet.As<IQueryable<Models.Album>>().Setup(m => m.ElementType).Returns(albums.ElementType);
            mockSet.As<IQueryable<Models.Album>>().Setup(m => m.GetEnumerator()).Returns(albums.GetEnumerator());
            mockSet.As<IAsyncEnumerable<Models.Album>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<Models.Album>(albums.GetEnumerator()));

            _mockContext.Setup(c => c.Albums).Returns(mockSet.Object);

            // Act
            var result = await _albumService.GetAllAlbums();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAlbumById_ReturnsAlbum()
        {
            // Arrange
            var album = new Models.Album { Id = 1, Name = "Album1", Artist = "Artist1", ImageUrl = "Url1" };
            var mockSet = new Mock<DbSet<Models.Album>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(album);

            _mockContext.Setup(c => c.Albums).Returns(mockSet.Object);

            // Act
            var result = await _albumService.GetAlbumById(1);

            // Assert
            Assert.Equal(album, result);
        }

        [Fact]
        public async Task CreateAlbum_AddsAlbum()
        {
            // Arrange
            var album = new Models.Album { Id = 1, Name = "Album1", Artist = "Artist1", ImageUrl = "Url1" };
            var mockSet = new Mock<DbSet<Models.Album>>();

            _mockContext.Setup(c => c.Albums).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _albumService.CreateAlbum(album);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Models.Album>()), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            Assert.Equal(album, result);
        }

    
        [Fact]
        public async Task DeleteAlbum_DeletesAlbum()
        {
            // Arrange
            var album = new Models.Album { Id = 1, Name = "Album1", Artist = "Artist1", ImageUrl = "Url1" };
            var mockSet = new Mock<DbSet<Models.Album>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(album);

            _mockContext.Setup(c => c.Albums).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _albumService.DeleteAlbum(1);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Models.Album>()), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            Assert.True(result);
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public T Current => _inner.Current;
    }
}