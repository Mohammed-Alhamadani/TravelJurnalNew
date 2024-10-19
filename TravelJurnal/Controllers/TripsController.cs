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
    public class TripController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TripController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(template: "listTrips")]
        public async Task<ActionResult<List<Trip>>> listTrips()
        {
            return await _context.Trips.ToListAsync(); // Changed to 'Trips'
        }

        [HttpGet(template: "findTrip/{id}")]
        public async Task<ActionResult<Trip>> FindTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id); // Changed to 'Trips'
            if (trip == null)
            {
                return NotFound();
            }
            return trip;
        }

        [HttpPut(template: "updateTrip/{id}")]
        public async Task<IActionResult> UpdateTrip(int id, Trip trip)
        {
            if (id != trip.TripId)
            {
                return BadRequest();
            }
            _context.Trips.Attach(trip); // Changed to 'Trips'
            _context.Entry(trip).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
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

        [HttpPost(template: "addTrip")]
        public async Task<ActionResult<Trip>> AddTrip(Trip trip)
        {
            _context.Trips.Add(trip); // Changed to 'Trips'
            await _context.SaveChangesAsync();
            return CreatedAtAction("FindTrip", new { id = trip.TripId }, trip);
        }

        [HttpDelete(template: "deleteTrip/{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id); // Changed to 'Trips'
            if (trip == null)
            {
                return NotFound();
            }
            _context.Trips.Remove(trip); // Changed to 'Trips'
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id); // Changed to 'Trips'
        }
    }
}
