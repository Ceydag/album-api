using Microsoft.EntityFrameworkCore;

namespace Album.Api.Database
{
    public class AlbumContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public AlbumContext() { }

        public AlbumContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public virtual DbSet<Album.Api.Models.Album> Albums { get; set; }
    }
}