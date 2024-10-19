using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelJurnal.Data;
using TravelJurnal.Models;

namespace TravelJurnal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EntryAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("listEntries")]
        public async Task<ActionResult<List<Entry>>> ListEntries()
        {
            return await _context.Entries.ToListAsync(); // Use Entries
        }

        [HttpGet("FindEntry/{id}")]
        public async Task<ActionResult<Entry>> FindEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id); // Use Entries
            if (entry == null)
            {
                return NotFound();
            }
            return entry;
        }

        [HttpPut("updateEntry/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, Entry entry)
        {
            if (id != entry.EntryId)
            {
                return BadRequest();
            }
            _context.Entries.Attach(entry); // Use Entries
            _context.Entry(entry).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        [HttpPost("addEntry")]
        public async Task<ActionResult<Entry>> AddEntry(Entry entry)
        {
            _context.Entries.Add(entry); // Use Entries
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(FindEntry), new { id = entry.EntryId }, entry);
        }

        [HttpDelete("deleteEntry/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id); // Use Entries
            if (entry == null)
            {
                return NotFound();
            }
            _context.Entries.Remove(entry); // Use Entries
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.EntryId == id); // Use Entries
        }
    }
}
