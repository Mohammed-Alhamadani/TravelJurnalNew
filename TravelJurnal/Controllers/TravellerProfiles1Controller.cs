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
    public class TravellerProfiles1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravellerProfiles1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TravellerProfiles1
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravellerProfile.ToListAsync());
        }

        // GET: TravellerProfiles1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travellerProfile = await _context.TravellerProfile
                .FirstOrDefaultAsync(m => m.TravellerId == id);
            if (travellerProfile == null)
            {
                return NotFound();
            }

            return View(travellerProfile);
        }

        // GET: TravellerProfiles1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravellerProfiles1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TravellerId,TravellerName,TravellerEmail,Description,ProfilePicture")] TravellerProfile travellerProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travellerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travellerProfile);
        }

        // GET: TravellerProfiles1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travellerProfile = await _context.TravellerProfile.FindAsync(id);
            if (travellerProfile == null)
            {
                return NotFound();
            }
            return View(travellerProfile);
        }

        // POST: TravellerProfiles1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravellerId,TravellerName,TravellerEmail,Description,ProfilePicture")] TravellerProfile travellerProfile)
        {
            if (id != travellerProfile.TravellerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travellerProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravellerProfileExists(travellerProfile.TravellerId))
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
            return View(travellerProfile);
        }

        // GET: TravellerProfiles1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travellerProfile = await _context.TravellerProfile
                .FirstOrDefaultAsync(m => m.TravellerId == id);
            if (travellerProfile == null)
            {
                return NotFound();
            }

            return View(travellerProfile);
        }

        // POST: TravellerProfiles1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travellerProfile = await _context.TravellerProfile.FindAsync(id);
            if (travellerProfile != null)
            {
                _context.TravellerProfile.Remove(travellerProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravellerProfileExists(int id)
        {
            return _context.TravellerProfile.Any(e => e.TravellerId == id);
        }
    }
}
