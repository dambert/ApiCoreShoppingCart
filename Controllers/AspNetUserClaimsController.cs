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
    [Route("api/AspNetUserClaims")]
    public class AspNetUserClaimsController : Controller
    {
        private readonly ShoppingCartContext _context;

        public AspNetUserClaimsController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/AspNetUserClaims
        [HttpGet]
        public IEnumerable<AspNetUserClaims> GetAspNetUserClaims()
        {
            return _context.AspNetUserClaims;
        }

        // GET: api/AspNetUserClaims/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAspNetUserClaims([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetUserClaims = await _context.AspNetUserClaims.SingleOrDefaultAsync(m => m.Id == id);

            if (aspNetUserClaims == null)
            {
                return NotFound();
            }

            return Ok(aspNetUserClaims);
        }

        // PUT: api/AspNetUserClaims/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserClaims([FromRoute] int id, [FromBody] AspNetUserClaims aspNetUserClaims)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetUserClaims.Id)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUserClaims).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserClaimsExists(id))
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

        // POST: api/AspNetUserClaims
        [HttpPost]
        public async Task<IActionResult> PostAspNetUserClaims([FromBody] AspNetUserClaims aspNetUserClaims)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AspNetUserClaims.Add(aspNetUserClaims);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAspNetUserClaims", new { id = aspNetUserClaims.Id }, aspNetUserClaims);
        }

        // DELETE: api/AspNetUserClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUserClaims([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetUserClaims = await _context.AspNetUserClaims.SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUserClaims == null)
            {
                return NotFound();
            }

            _context.AspNetUserClaims.Remove(aspNetUserClaims);
            await _context.SaveChangesAsync();

            return Ok(aspNetUserClaims);
        }

        private bool AspNetUserClaimsExists(int id)
        {
            return _context.AspNetUserClaims.Any(e => e.Id == id);
        }
    }
}