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

        //<summary>
        // This Api returns List of the Entries in the data base.
        // /api/EntryAPI/listEntries
        [HttpGet(template:"listEntries")]

        
        public List<Entry> listEntries()
        {
            return _context.Entry.ToList();
        }

        // /api/EntryAPI/findentry/17

        [HttpGet(template:"FindEntry/{id}")]
        public Entry FindEntry(int id) {

            return _context.Entry.Find(id);
        }

        // PUT: api/EntryAPI/updateEntry/5
        [HttpPut(template: "updateEntry/{id}")]
        public IActionResult UpdateEntry(int id, Entry entry)
        {
            if (id != entry.EntryId)
            {
                return BadRequest();
            }
            _context.Entry.Attach(entry);
            _context.Entry(entry).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/EntryAPI/addEntry
        [HttpPost(template: "addEntry")]
        public ActionResult<Entry> AddEntry(Entry entry)
        {
            _context.Entry.Add(entry);
            _context.SaveChanges();
            return CreatedAtAction("FindEntry", new { id = entry.EntryId }, entry);
        }

        // DELETE: api/EntryAPI/deleteEntry/5
        [HttpDelete(template: "deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var entry = _context.Entry.Find(id);
            if (entry == null)
            {
                return NotFound();
            }
            _context.Entry.Remove(entry);
            _context.SaveChanges();
            return NoContent();
        }

        // Check if entry exists
        private bool EntryExists(int id)
        {
            return _context.Entry.Any(e => e.EntryId == id);
        }

    }
}
