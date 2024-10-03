using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // <summary>
        // Returns a list of all entries in the database.
        // </summary>
        // <returns>List of Entry objects</returns>
        [HttpGet(template: "listEntries")]
        public async Task<ActionResult<List<Entry>>> ListEntries()
        {
            return await _context.Entry.ToListAsync();
        }

        // <summary>
        // Finds an entry by its ID.
        // </summary>
        // <param name="id">Entry ID</param>
        // <returns>Entry object or NotFound result</returns>
        [HttpGet(template: "FindEntry/{id}")]
        public async Task<ActionResult<Entry>> FindEntry(int id)
        {
            var entry = await _context.Entry.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return entry;
        }

        // <summary>
        // Updates an existing entry.
        // </summary>
        // <param name="id">Entry ID</param>
        // <param name="entry">Updated Entry object</param>
        // <returns>NoContent result or BadRequest</returns>
        [HttpPut(template: "updateEntry/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, Entry entry)
        {
            if (id != entry.EntryId)
            {
                return BadRequest();
            }
            _context.Entry.Attach(entry);
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

        // <summary>
        // Creates a new entry.
        // </summary>
        // <param name="entry">New Entry object</param>
        // <returns>CreatedAtAction result or BadRequest</returns>
        [HttpPost(template: "addEntry")]
        public async Task<ActionResult<Entry>> AddEntry(Entry entry)
        {
            _context.Entry.Add(entry);
            await _context.SaveChangesAsync();
            return CreatedAtAction("FindEntry", new { id = entry.EntryId }, entry);
        }

        // <summary>
        // Deletes an entry by its ID.
        // </summary>
        // <param name="id">Entry ID</param>
        // <returns>will delete it if found and NoContent result or NotFound</returns>
        [HttpDelete(template: "deleteEntry/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _context.Entry.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            _context.Entry.Remove(entry);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // <summary>
        // Checks if an entry exists.
        // </summary>
        // <param name="id">Entry ID</param>
        // <returns>True if entry exists, false otherwise</returns>
        private bool EntryExists(int id)
        {
            return _context.Entry.Any(e => e.EntryId == id);
        }
    }
}

