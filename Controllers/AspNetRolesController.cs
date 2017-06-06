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
    [Route("api/AspNetRoles")]
    public class AspNetRolesController : Controller
    {
        private readonly ShoppingCartContext _context;

        public AspNetRolesController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/AspNetRoles
        [HttpGet]
        public IEnumerable<AspNetRoles> GetAspNetRoles()
        {
            return _context.AspNetRoles;
        }

        // GET: api/AspNetRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAspNetRoles([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetRoles = await _context.AspNetRoles.SingleOrDefaultAsync(m => m.Id == id);

            if (aspNetRoles == null)
            {
                return NotFound();
            }

            return Ok(aspNetRoles);
        }

        // PUT: api/AspNetRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetRoles([FromRoute] string id, [FromBody] AspNetRoles aspNetRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetRoles.Id)
            {
                return BadRequest();
            }

            _context.Entry(aspNetRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRolesExists(id))
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

        // POST: api/AspNetRoles
        [HttpPost]
        public async Task<IActionResult> PostAspNetRoles([FromBody] AspNetRoles aspNetRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AspNetRoles.Add(aspNetRoles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAspNetRoles", new { id = aspNetRoles.Id }, aspNetRoles);
        }

        // DELETE: api/AspNetRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetRoles([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetRoles = await _context.AspNetRoles.SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }

            _context.AspNetRoles.Remove(aspNetRoles);
            await _context.SaveChangesAsync();

            return Ok(aspNetRoles);
        }

        private bool AspNetRolesExists(string id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id);
        }
    }
}