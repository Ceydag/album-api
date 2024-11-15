// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Album.Api.Database;
// using Album.Api.Models;

// namespace Album.Api.Controllers
// {
//     public class AlbumController : Controller
//     {
//         private readonly AlbumContext _context;

//         public AlbumController(AlbumContext context)
//         {
//             _context = context;
//         }

//         // GET: Album
//         public async Task<IActionResult> Index()
//         {
//               return _context.Albums != null ? 
//                           View(await _context.Albums.ToListAsync()) :
//                           Problem("Entity set 'AlbumContext.Albums'  is null.");
//         }

//         // GET: Album/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null || _context.Albums == null)
//             {
//                 return NotFound();
//             }

//             var album = await _context.Albums
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (album == null)
//             {
//                 return NotFound();
//             }

//             return View(album);
//         }

//         // GET: Album/Create
//         public IActionResult Create()
//         {
//             return View();
//         }

//         // POST: Album/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Id,Name,Artist,ImageUrl")] Models.Album album)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(album);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(album);
//         }

//         // GET: Album/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null || _context.Albums == null)
//             {
//                 return NotFound();
//             }

//             var album = await _context.Albums.FindAsync(id);
//             if (album == null)
//             {
//                 return NotFound();
//             }
//             return View(album);
//         }

//         // POST: Album/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Artist,ImageUrl")] Models.Album album)
//         {
//             if (id != album.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(album);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!AlbumExists(album.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(album);
//         }

//         // GET: Album/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null || _context.Albums == null)
//             {
//                 return NotFound();
//             }

//             var album = await _context.Albums
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (album == null)
//             {
//                 return NotFound();
//             }

//             return View(album);
//         }

//         // POST: Album/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             if (_context.Albums == null)
//             {
//                 return Problem("Entity set 'AlbumContext.Albums'  is null.");
//             }
//             var album = await _context.Albums.FindAsync(id);
//             if (album != null)
//             {
//                 _context.Albums.Remove(album);
//             }
            
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool AlbumExists(int id)
//         {
//           return (_context.Albums?.Any(e => e.Id == id)).GetValueOrDefault();
//         }
//     }
// }
