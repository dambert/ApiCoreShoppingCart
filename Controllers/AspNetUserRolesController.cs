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
    [Route("api/AspNetUserRoles")]
    public class AspNetUserRolesController : Controller
    {
        private readonly ShoppingCartContext _context;

        public AspNetUserRolesController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/AspNetUserRoles
        [HttpGet]
        public IEnumerable<AspNetUserRoles> GetAspNetUserRoles()
        {
            return _context.AspNetUserRoles;
        }

        // GET: api/AspNetUserRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAspNetUserRoles([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetUserRoles = await _context.AspNetUserRoles.SingleOrDefaultAsync(m => m.UserId == id);

            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            return Ok(aspNetUserRoles);
        }

        // PUT: api/AspNetUserRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserRoles([FromRoute] string id, [FromBody] AspNetUserRoles aspNetUserRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetUserRoles.UserId)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUserRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserRolesExists(id))
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

        // POST: api/AspNetUserRoles
        [HttpPost]
        public async Task<IActionResult> PostAspNetUserRoles([FromBody] AspNetUserRoles aspNetUserRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AspNetUserRoles.Add(aspNetUserRoles);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserRolesExists(aspNetUserRoles.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAspNetUserRoles", new { id = aspNetUserRoles.UserId }, aspNetUserRoles);
        }

        // DELETE: api/AspNetUserRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUserRoles([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aspNetUserRoles = await _context.AspNetUserRoles.SingleOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            _context.AspNetUserRoles.Remove(aspNetUserRoles);
            await _context.SaveChangesAsync();

            return Ok(aspNetUserRoles);
        }

        private bool AspNetUserRolesExists(string id)
        {
            return _context.AspNetUserRoles.Any(e => e.UserId == id);
        }
    }
}