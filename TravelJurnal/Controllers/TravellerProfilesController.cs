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
        private readonly ApplicationDbContext _context;

        public TravellerProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TravellerProfiles/listTravellerProfiles
        [HttpGet(template: "listTravellerProfiles")]
        public List<TravellerProfile> listTravellerProfiles()
        {
            return _context.TravellerProfile.ToList();
        }

        // GET: api/TravellerProfiles/findTravellerProfile/17
        [HttpGet(template: "findTravellerProfile/{id}")]
        public TravellerProfile FindTravellerProfile(int id)
        {
            return _context.TravellerProfile.Find(id);
        }

        // PUT: api/TravellerProfiles/updateTravellerProfile/5
        [HttpPut(template: "updateTravellerProfile/{id}")]
        public IActionResult UpdateTravellerProfile(int id, TravellerProfile travellerProfile)
        {
            if (id != travellerProfile.TravellerId)
            {
                return BadRequest();
            }
            _context.TravellerProfile.Attach(travellerProfile);
            _context.Entry(travellerProfile).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/TravellerProfiles/addTravellerProfile
        [HttpPost(template: "addTravellerProfile")]
        public ActionResult<TravellerProfile> AddTravellerProfile(TravellerProfile travellerProfile)
        {
            _context.TravellerProfile.Add(travellerProfile);
            _context.SaveChanges();
            return CreatedAtAction("FindTravellerProfile", new { id = travellerProfile.TravellerId }, travellerProfile);
        }

        // DELETE: api/TravellerProfiles/deleteTravellerProfile/5
        [HttpDelete(template: "deleteTravellerProfile/{id}")]
        public IActionResult DeleteTravellerProfile(int id)
        {
            var travellerProfile = _context.TravellerProfile.Find(id);
            if (travellerProfile == null)
            {
                return NotFound();
            }
            _context.TravellerProfile.Remove(travellerProfile);
            _context.SaveChanges();
            return NoContent();
        }

        // Check if traveller profile exists
        private bool TravellerProfileExists(int id)
        {
            return _context.TravellerProfile.Any(e => e.TravellerId == id);
        }
    }
}
