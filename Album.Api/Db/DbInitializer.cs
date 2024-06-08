using System.Linq;
using Album.Api.Database;

namespace Album.Api.Database
{
	public static class DBInitializer
	{
		public static void Initialize(Database.AlbumContext context)
		{
			// Ensure the database is created
			try{
			context.Database.EnsureCreated();}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}

			// Check if the Albums table is empty
			if (!context.Albums.Any())
			{
				// Add initial data to the Albums table
				var albums = new[]
				{
					new Models.Album { Id = 1, Name = "The Tortured Poets Department", Artist = "Taylor Swift", ImageUrl = "https://dims.apnews.com/dims4/default/e64a886/2147483647/strip/true/crop/3000x3000+0+0/resize/599x599!/quality/90/?url=https%3A%2F%2Fassets.apnews.com%2F7c%2F48%2F5e5528ec5aa61e904ef58f832672%2F47b5e4628207415c899d0f94987626c2" },
					new Models.Album { Id = 2, Name = "RevolverDeluxeEdition", Artist = "TheBeatles", ImageUrl = "https://m.media-amazon.com/images/I/91ffeWzPNpL._UF1000,1000_QL80_.jpg" }
				};

				context.Albums.AddRange(albums);
				context.SaveChanges();
			}
		}
	}
}