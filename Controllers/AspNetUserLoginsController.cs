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
    [Route("api/AspNetUserLogins")]
    public class AspNetUserLoginsController : Controller
    {
        private readonly ShoppingCartContext _context;

        public AspNetUserLoginsController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/AspNetUserLogins
        [HttpGet]
        public IEnumerable<AspNetUserLogins> GetAspNetUserLogins()
        {
            return _context.AspNetUserLogins;
        }

        // GET: api/AspNetUserLogins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAspNetUserLogins([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetUserLogins = await _context.AspNetUserLogins.SingleOrDefaultAsync(m => m.LoginProvider == id);

            if (aspNetUserLogins == null)
            {
                return NotFound();
            }

            return Ok(aspNetUserLogins);
        }

        // PUT: api/AspNetUserLogins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserLogins([FromRoute] string id, [FromBody] AspNetUserLogins aspNetUserLogins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetUserLogins.LoginProvider)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUserLogins).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserLoginsExists(id))
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

        // POST: api/AspNetUserLogins
        [HttpPost]
        public async Task<IActionResult> PostAspNetUserLogins([FromBody] AspNetUserLogins aspNetUserLogins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AspNetUserLogins.Add(aspNetUserLogins);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserLoginsExists(aspNetUserLogins.LoginProvider))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAspNetUserLogins", new { id = aspNetUserLogins.LoginProvider }, aspNetUserLogins);
        }

        // DELETE: api/AspNetUserLogins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUserLogins([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetUserLogins = await _context.AspNetUserLogins.SingleOrDefaultAsync(m => m.LoginProvider == id);
            if (aspNetUserLogins == null)
            {
                return NotFound();
            }

            _context.AspNetUserLogins.Remove(aspNetUserLogins);
            await _context.SaveChangesAsync();

            return Ok(aspNetUserLogins);
        }

        private bool AspNetUserLoginsExists(string id)
        {
            return _context.AspNetUserLogins.Any(e => e.LoginProvider == id);
        }
    }
}