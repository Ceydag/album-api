// using Xunit;
// using Moq;
// using Microsoft.EntityFrameworkCore;
// using Album.Api.Services;
// using Album.Api.Models;
// using System.Collections.Generic;
// using System.Linq;
// using Album.Api.Database;
// using System.Threading.Tasks;

// namespace Album.Api.Tests
// {
//     public class AlbumServiceTests
//     {
//         private Mock<DbSet<Album.Api.Models.Album>> _mockSet;
//         private Mock<AlbumContext> _mockContext;
//         private AlbumService _service;

//        public AlbumServiceTests()
// {
//     var data = new List<Album.Api.Models.Album>
//     {
//         new Album.Api.Models.Album { Id = 1, Name = "Test Album 1", Artist = "Test Artist 1", ImageUrl = "Test Url 1" },
//         new Album.Api.Models.Album { Id = 2, Name = "Test Album 2", Artist = "Test Artist 2", ImageUrl = "Test Url 2" },
//         new Album.Api.Models.Album { Id = 3, Name = "Test Album 3", Artist = "Test Artist 3", ImageUrl = "Test Url 3" },
//     }.AsQueryable();

//     _mockSet = new Mock<DbSet<Album.Api.Models.Album>>();
//     _mockSet.As<IQueryable<Album.Api.Models.Album>>().Setup(m => m.Provider).Returns(data.Provider);
//     _mockSet.As<IQueryable<Album.Api.Models.Album>>().Setup(m => m.Expression).Returns(data.Expression);
//     _mockSet.As<IQueryable<Album.Api.Models.Album>>().Setup(m => m.ElementType).Returns(data.ElementType);
//     _mockSet.As<IQueryable<Album.Api.Models.Album>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

//     _mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).Returns<object[]>(ids => new ValueTask<Album.Api.Models.Album>(data.FirstOrDefault(d => d.Id == (int)ids[0])));

//     _mockSet.As<IAsyncEnumerable<Album.Api.Models.Album>>().Setup(d => d.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Album.Api.Models.Album>(data.GetEnumerator()));
//     _mockSet.As<IQueryable<Album.Api.Models.Album>>().Setup(d => d.Provider).Returns(new TestAsyncQueryProvider<Album.Api.Models.Album>(data.Provider));

//     _mockContext = new Mock<AlbumContext>();
//     _mockContext.Setup(c => c.Albums).Returns(_mockSet.Object);

//     _service = new AlbumService(_mockContext.Object);
// }

//         [Fact]
//         public async Task GetAllAlbums_ReturnsAllAlbums()
//         {
//             var albums = await _service.GetAllAlbums();
//             Assert.Equal(3, albums.Count);
//         }

//         [Fact]
//         public async Task GetAlbumById_ReturnsAlbumWithGivenId()
//         {
//             var album = await _service.GetAlbumById(1);
//             Assert.Equal("Test Album 1", album.Name);
//         }

//         // Continue with the rest of your tests...
//     }
// }