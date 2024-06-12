// note: namespace AlbumApi renamed from Album.Api to fix model Album namespace error.

using Album.Api.Database;
using Album.Api.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
		options.AddDefaultPolicy(builder => {
			builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
		}); 
	});
	

var Configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.Build();


//Adding the database
builder.Services.AddDbContext<Album.Api.Database.AlbumContext>(options =>
{
	options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

app.UseCors(policy => policy
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowAnyOrigin());
				
				
				
app.MapHealthChecks("/health");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


				
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<Album.Api.Database.AlbumContext>();
	DBInitializer.Initialize(dbContext);
}



app.Run();