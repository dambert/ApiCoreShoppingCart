using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartFinal.Models;

namespace ShoppingCartFinal.Controllers
{
    [Produces("application/json")]
    [Route("api/Genres")]
    public class GenresController : Controller
    {
        private readonly ShoppingCartContext _context;

        public GenresController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public IEnumerable<Genres> GetGenres()
        {
            return _context.Genres;
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenres([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genres = await _context.Genres.SingleOrDefaultAsync(m => m.GenreId == id);

            if (genres == null)
            {
                return NotFound();
            }

            return Ok(genres);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres([FromRoute] int id, [FromBody] Genres genres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genres.GenreId)
            {
                return BadRequest();
            }

            _context.Entry(genres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(id))
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

        // POST: api/Genres
        [HttpPost]
        public async Task<IActionResult> PostGenres([FromBody] Genres genres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genres.Add(genres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenres", new { id = genres.GenreId }, genres);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenres([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genres = await _context.Genres.SingleOrDefaultAsync(m => m.GenreId == id);
            if (genres == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genres);
            await _context.SaveChangesAsync();

            return Ok(genres);
        }

        private bool GenresExists(int id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }
    }
}