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
        // DbContext instance for database operations
        private readonly ApplicationDbContext _context;

        // Constructor initializes DbContext
        public TripController(ApplicationDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Returns a list of all trips in the database.
        // </summary>
        // <returns>List of Trip objects</returns>
        [HttpGet(template: "listTrips")]
        public async Task<ActionResult<List<Trip>>> listTrips()
        {
            // Retrieve all trips from database
            return await _context.Trip.ToListAsync();
        }

        // <summary>
        // Finds a trip by its ID.
        // </summary>
        // <param name="id">Trip ID</param>
        // <returns>Trip object or NotFound result</returns>
        [HttpGet(template: "findTrip/{id}")]
        public async Task<ActionResult<Trip>> FindTrip(int id)
        {
            // Retrieve trip by ID from database
            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return trip;
        }

        // <summary>
        // Updates an existing trip.
        // </summary>
        // <param name="id">Trip ID</param>
        // <param name="trip">Updated Trip object</param>
        // <returns>NoContent result or BadRequest</returns>
        [HttpPut(template: "updateTrip/{id}")]
        public async Task<IActionResult> UpdateTrip(int id, Trip trip)
        {
            // Validate trip ID
            if (id != trip.TripId)
            {
                return BadRequest();
            }
            // Attach trip to DbContext
            _context.Trip.Attach(trip);
            // Set trip state to modified
            _context.Entry(trip).State = EntityState.Modified;
            try
            {
                // Save changes to database
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

        // <summary>
        // Creates a new trip.
        // </summary>
        // <param name="trip">New Trip object</param>
        // <returns>Created Action result</returns>
        [HttpPost(template: "addTrip")]
        public async Task<ActionResult<Trip>> AddTrip(Trip trip)
        {
            // Add trip to DbContext
            _context.Trip.Add(trip);
            // Save changes to database
            await _context.SaveChangesAsync();
            // Return created trip with ID
            return CreatedAtAction("FindTrip", new { id = trip.TripId }, trip);
        }

        // <summary>
        // Deletes a trip by its ID.
        // </summary>
        // <param name="id">Trip ID</param>
        // <returns>NoContent result or NotFound</returns>
        [HttpDelete(template: "deleteTrip/{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            // Retrieve trip by ID from database
            var trip = await _context.Trip.FindAsync(id);
            // Validate trip existence
            if (trip == null)
            {
                return NotFound();
            }
            // Remove trip from DbContext
            _context.Trip.Remove(trip);
            // Save changes to database
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // <summary>
        // Checks if a trip exists.
        // </summary>
        // <param name="id">Trip ID</param>
        // <returns>True if trip exists, false otherwise</returns>
        private bool TripExists(int id)
        {
            // Check if trip exists in database
            return _context.Trip.Any(e => e.TripId == id);
              }
    }
}
