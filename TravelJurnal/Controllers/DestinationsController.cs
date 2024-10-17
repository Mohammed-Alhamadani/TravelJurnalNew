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
    public class DestinationAPIController : ControllerBase
    {
        // DbContext instance for database operations
        private readonly ApplicationDbContext _context;

        // Constructor initializes DbContext
        public DestinationAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Returns a list of all destinations in the database.
        // </summary>
        // <returns>List of Destination objects</returns>
        [HttpGet(template: "listDestinations")]
        public async Task<ActionResult<List<Destination>>> listDestinations()
        {
            // Retrieve all destinations from database
            return await _context.Destination.ToListAsync();
        }

        // <summary>
        // Finds a destination by its ID.
        // </summary>
        // <param name="id">Destination ID</param>
        // <returns>Destination object</returns>
        [HttpGet(template: "findDestination/{id}")]
        public async Task<ActionResult<Destination>> FindDestination(int id)
        {
            // Retrieve destination by ID from database
            var destination = await _context.Destination.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            return destination;
        }

        // <summary>
        // Updates an existing destination.
        // </summary>
        // <param name="id">Destination ID</param>
        // <param name="destination">Updated Destination object</param>
        // <returns>if no id NoContent result or BadRequest</returns>
        [HttpPut(template: "updateDestination/{id}")]
        public async Task<IActionResult> UpdateDestination(int id, Destination destination)
        {
            // Validate destination ID
            if (id != destination.DestinationId)
            {
                return BadRequest();
            }
            // Attach destination to DbContext
            _context.Destination.Attach(destination);
            // Set destination state to modified
            _context.Entry(destination).State = EntityState.Modified;
            try
            {
                // Save changes to database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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
        // Creates a new destination.
        // </summary>
        // <param name="destination">New Destination object</param>
        // <returns>Created Action result or BadRequest</returns>
        [HttpPost(template: "addDestination")]
        public async Task<ActionResult<Destination>> AddDestination(Destination destination)
        {
            // Add destination to DbContext
            _context.Destination.Add(destination);
            // Save changes to database
            await _context.SaveChangesAsync();
            // Return created destination with ID
            return CreatedAtAction("FindDestination", new { id = destination.DestinationId }, destination);
        }

        // <summary>
        // Deletes a destination by its ID.
        // </summary>
        // <param name="id">Destination ID</param>
        // <returns>NoContent result or NotFound</returns>
        [HttpDelete(template: "deleteDestination/{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            // Retrieve destination by ID from database
            var destination = await _context.Destination.FindAsync(id);
            // Validate destination existence
            if (destination == null)
            {
                return NotFound();
            }
            // Remove destination from DbContext
            _context.Destination.Remove(destination);
            // Save changes to database
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // <summary>
        // Checks if a destination exists.
        // </summary>
        // <param name="id">Destination ID</param>
        // <returns>True if destination exist</returns>
        private bool DestinationExists(int id)
        {
            // Check if destination exists in database
            return _context.Destination.Any(e => e.DestinationId == id);
        }
    }
}

