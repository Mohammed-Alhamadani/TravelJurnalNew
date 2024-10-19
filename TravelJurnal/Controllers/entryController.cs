using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelJurnal.Data;
using TravelJurnal.Models;

namespace TravelJurnal.Controllers
{
    public class EntryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: entry
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Entries.Include(e => e.Destination).Include(e => e.Trip); // Change to Entries
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: entry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries // Change to Entries
                .Include(e => e.Destination)
                .Include(e => e.Trip)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: entry/Create
        public IActionResult Create()
        {
            ViewData["DestinationId"] = new SelectList(_context.Destination, "DestinationId", "DestinationId"); // Change to Destinations
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId"); // Change to Trips
            return View();
        }

        // POST: entry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryId,Description,TripId,DestinationId")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinationId"] = new SelectList(_context.Destination, "DestinationId", "DestinationId", entry.DestinationId); // Change to Destinations
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId", entry.TripId); // Change to Trips
            return View(entry);
        }

        // GET: entry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries.FindAsync(id); // Change to Entries
            if (entry == null)
            {
                return NotFound();
            }
            ViewData["DestinationId"] = new SelectList(_context.Destination, "DestinationId", "DestinationId", entry.DestinationId); // Change to Destinations
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId", entry.TripId); // Change to Trips
            return View(entry);
        }

        // POST: entry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,Description,TripId,DestinationId")] Entry entry)
        {
            if (id != entry.EntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryExists(entry.EntryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinationId"] = new SelectList(_context.Destination, "DestinationId", "DestinationId", entry.DestinationId); // Change to Destinations
            ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TripId", entry.TripId); // Change to Trips
            return View(entry);
        }

        // GET: entry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries // Change to Entries
                .Include(e => e.Destination)
                .Include(e => e.Trip)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: entry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _context.Entries.FindAsync(id); // Change to Entries
            if (entry != null)
            {
                _context.Entries.Remove(entry); // Change to Entries
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.EntryId == id); // Change to Entries
        }
    }
}
