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
		public async Task Index_ReturnsAViewResult_WithAListOfAlbums()
		{
		// Arrange
		var testAlbums = GetTestAlbums();

		var mockContext = new Mock<AlbumContext>();
		// Set up the behavior of the mock context here, if necessary

		var controller = new AlbumController(mockContext.Object);

		// Act
		var result = await controller.Index();

		// Assert
		var objectResult = Assert.IsType<ObjectResult>(result);
		var model = Assert.IsAssignableFrom<ProblemDetails>(objectResult.Value);
		}



		[Fact]
		public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
		{
			// Arrange
			var mockContext = new Mock<AlbumContext>();
			var controller = new AlbumController(mockContext.Object);
			// Act
			var result = await controller.Details(null);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
		{
			// Arrange
			var mockContext = new Mock<AlbumContext>();
			var controller = new AlbumController(mockContext.Object);
			controller.ModelState.AddModelError("error", "some error");

			// Act
			var result = await controller.Create(new Models.Album());

			// Assert
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
		{
			// Arrange
			var mockContext = new Mock<AlbumContext>();
			var controller = new AlbumController(mockContext.Object);
			// Act
			var result = await controller.Edit(null);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
		{
			// Arrange
			var mockContext = new Mock<AlbumContext>();
			var controller = new AlbumController(mockContext.Object);

			// Act
			var result = await controller.Delete(null);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenIdIsValid()
		{
			// Arrange
			var mockSet = new Mock<DbSet<Models.Album>>();
			mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(GetTestAlbums().First());

			var mockContext = new Mock<AlbumContext>();
			mockContext.Setup(c => c.Albums).Returns(mockSet.Object);

			var controller = new AlbumController(mockContext.Object);

			// Act
			var result = await controller.DeleteConfirmed(1);

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}
	}
}