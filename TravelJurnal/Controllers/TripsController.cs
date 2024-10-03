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

        // GET: api/Trip/listTrips
        [HttpGet(template: "listTrips")]
        public List<Trip> listTrips()
        {
            return _context.Trip.ToList();
        }

        // GET: api/Trip/findTrip/17
        [HttpGet(template: "findTrip/{id}")]
        public Trip FindTrip(int id)
        {
            return _context.Trip.Find(id);
        }

        // PUT: api/Trip/updateTrip/5
        [HttpPut(template: "updateTrip/{id}")]
        public IActionResult UpdateTrip(int id, Trip trip)
        {
            if (id != trip.TripId)
            {
                return BadRequest();
            }
            _context.Trip.Attach(trip);
            _context.Entry(trip).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/Trip/addTrip
        [HttpPost(template: "addTrip")]
        public ActionResult<Trip> AddTrip(Trip trip)
        {
            _context.Trip.Add(trip);
            _context.SaveChanges();
            return CreatedAtAction("FindTrip", new { id = trip.TripId }, trip);
        }

        // DELETE: api/Trip/deleteTrip/5
        [HttpDelete(template: "deleteTrip/{id}")]
        public IActionResult DeleteTrip(int id)
        {
            var trip = _context.Trip.Find(id);
            if (trip == null)
            {
                return NotFound();
            }
            _context.Trip.Remove(trip);
            _context.SaveChanges();
            return NoContent();
        }

        // Check if trip exists
        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.TripId == id);
        }
    }
}

