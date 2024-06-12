using Album.Api.Database;
using Album.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Album.Api.Models;



namespace Album.Api.Services{
public class AlbumService : IAlbumService
{
	
	private readonly AlbumContext _context;
	
	public AlbumService(AlbumContext context)
	{
		_context = context;
	}
	
	public async Task<List<Album.Api.Models.Album>> GetAllAlbums()
	{
		if (_context.Albums != null)
		{
			return await _context.Albums.ToListAsync();
		}
		return new List<Album.Api.Models.Album>();
	}
	
	public async Task<Album.Api.Models.Album> GetAlbumById(int id)
	{
		return await _context.Albums.FindAsync(id);
	}
	
	public async Task<Album.Api.Models.Album> CreateAlbum(Album.Api.Models.Album album)
	{
		_context.Albums.Add(album);
		await _context.SaveChangesAsync();
		return album;
	}
	
	public async Task<Album.Api.Models.Album> UpdateAlbum(int id, Album.Api.Models.Album album)
	{
		_context.Entry(album).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return album;
	}
	
	public async Task<bool> DeleteAlbum(int id)
	{
		var album = await _context.Albums.FindAsync(id);
		if (album == null)
		{
			return false;
		}
		_context.Albums.Remove(album);
		await _context.SaveChangesAsync();
		return true;
	}
}
}


namespace Album.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumContext _context;

        public AlbumController(AlbumContext context)
        {
            _context = context;
        }

        // GET: api/album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Album>>> Index()
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'AlbumContext.Albums' is null.");
            }
            return await _context.Albums.ToListAsync();
        }

        // GET: api/album/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Album>> Details(int id)
        {
            if (_context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        // POST: api/album
        [HttpPost]
        public async Task<ActionResult<Models.Album>> Create([FromBody] Models.Album album)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'AlbumContext.Albums' is null.");
            }

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Details), new { id = album.Id }, album);
        }

        // PUT: api/album/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Models.Album album)
        {
            if (id != album.Id)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/album/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(int id)
        {
            return (_context.Albums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

