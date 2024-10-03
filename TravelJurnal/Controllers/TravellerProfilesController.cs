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
    public class TravellerProfilesController : ControllerBase
    {
        // DbContext instance for database operations
        private readonly ApplicationDbContext _context;

        // Constructor initializes DbContext
        public TravellerProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Returns a list of all traveller profiles in the database.
        // </summary>
        // <returns>List of TravellerProfile objects</returns>
        [HttpGet(template: "listTravellerProfiles")]
        public async Task<ActionResult<List<TravellerProfile>>> listTravellerProfiles()
        {
            // Retrieve all traveller profiles from database
            return await _context.TravellerProfile.ToListAsync();
        }

        // <summary>
        // Finds a traveller profile by its ID.
        // </summary>
        // <param name="id">Traveller ID</param>
        // <returns>TravellerProfile object or NotFound result</returns>
        [HttpGet(template: "findTravellerProfile/{id}")]
        public async Task<ActionResult<TravellerProfile>> FindTravellerProfile(int id)
        {
            // Retrieve traveller profile by ID from database
            var travellerProfile = await _context.TravellerProfile.FindAsync(id);
            if (travellerProfile == null)
            {
                return NotFound();
            }
            return travellerProfile;
        }

        // <summary>
        // Updates an existing traveller profile.
        // </summary>
        // <param name="id">Traveller ID</param>
        // <param name="travellerProfile">Updated TravellerProfile object</param>
        // <returns>NoContent result or BadRequest</returns>
        [HttpPut(template: "updateTravellerProfile/{id}")]
        public async Task<IActionResult> UpdateTravellerProfile(int id, TravellerProfile travellerProfile)
        {
            // Validate traveller ID
            if (id != travellerProfile.TravellerId)
            {
                return BadRequest();
            }
            // Attach traveller profile to DbContext
            _context.TravellerProfile.Attach(travellerProfile);
            // Set traveller profile state to modified
            _context.Entry(travellerProfile).State = EntityState.Modified;
            try
            {
                // Save changes to database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravellerProfileExists(id))
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
        // Creates a new traveller profile.
        // </summary>
        // <param name="travellerProfile">New TravellerProfile object</param>
        // <returns>CreatedAtAction result or BadRequest</returns>
        [HttpPost(template: "addTravellerProfile")]
        public async Task<ActionResult<TravellerProfile>> AddTravellerProfile(TravellerProfile travellerProfile)
        {
            // Add traveller profile to DbContext
            _context.TravellerProfile.Add(travellerProfile);
            // Save changes to database
            await _context.SaveChangesAsync();
            // Return created traveller profile with ID
            return CreatedAtAction("FindTravellerProfile", new { id = travellerProfile.TravellerId }, travellerProfile);
        }

        // <summary>
        // Deletes a traveller profile by its ID.
        // </summary>
        // <param name="id">Traveller ID</param>
        // <returns>NoContent result or NotFound</returns>
        [HttpDelete(template: "deleteTravellerProfile/{id}")]
        public async Task<IActionResult> DeleteTravellerProfile(int id)
        {
            // Retrieve traveller profile by ID from database
            var travellerProfile = await _context.TravellerProfile.FindAsync(id);
            // Validate traveller profile existence
            if (travellerProfile == null)
            {
                return NotFound();
            }
            // Remove traveller profile from DbContext
            _context.TravellerProfile.Remove(travellerProfile);
            // Save changes to database
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // <summary>
        // Checks if a traveller profile exists.
        // </summary>
        // <param name="id">Traveller ID</param>
        // <returns>True if traveller profile exists, false otherwise</returns>
        private bool TravellerProfileExists(int id)
        {
            // Check if traveller profile exists in database
            return _context.TravellerProfile.Any(e => e.TravellerId == id);
        }
    }
}