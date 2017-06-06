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
    [Route("api/MigrationHistories")]
    public class MigrationHistoriesController : Controller
    {
        private readonly ShoppingCartContext _context;

        public MigrationHistoriesController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/MigrationHistories
        [HttpGet]
        public IEnumerable<MigrationHistory> GetMigrationHistory()
        {
            return _context.MigrationHistory;
        }

        // GET: api/MigrationHistories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMigrationHistory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var migrationHistory = await _context.MigrationHistory.SingleOrDefaultAsync(m => m.MigrationId == id);

            if (migrationHistory == null)
            {
                return NotFound();
            }

            return Ok(migrationHistory);
        }

        // PUT: api/MigrationHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMigrationHistory([FromRoute] string id, [FromBody] MigrationHistory migrationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != migrationHistory.MigrationId)
            {
                return BadRequest();
            }

            _context.Entry(migrationHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MigrationHistoryExists(id))
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

        // POST: api/MigrationHistories
        [HttpPost]
        public async Task<IActionResult> PostMigrationHistory([FromBody] MigrationHistory migrationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MigrationHistory.Add(migrationHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MigrationHistoryExists(migrationHistory.MigrationId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMigrationHistory", new { id = migrationHistory.MigrationId }, migrationHistory);
        }

        // DELETE: api/MigrationHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMigrationHistory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var migrationHistory = await _context.MigrationHistory.SingleOrDefaultAsync(m => m.MigrationId == id);
            if (migrationHistory == null)
            {
                return NotFound();
            }

            _context.MigrationHistory.Remove(migrationHistory);
            await _context.SaveChangesAsync();

            return Ok(migrationHistory);
        }

        private bool MigrationHistoryExists(string id)
        {
            return _context.MigrationHistory.Any(e => e.MigrationId == id);
        }
    }
}