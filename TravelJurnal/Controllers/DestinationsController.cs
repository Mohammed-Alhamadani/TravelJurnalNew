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
        private readonly ApplicationDbContext _context;

        public DestinationAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DestinationAPI/listDestinations
        [HttpGet(template: "listDestinations")]
        public List<Destination> listDestinations()
        {
            return _context.Destination.ToList();
        }

        // GET: api/DestinationAPI/findDestination/17
        [HttpGet(template: "findDestination/{id}")]
        public Destination FindDestination(int id)
        {
            return _context.Destination.Find(id);
        }

        // PUT: api/DestinationAPI/updateDestination/5
        [HttpPut(template: "updateDestination/{id}")]
        public IActionResult UpdateDestination(int id, Destination destination)
        {
            if (id != destination.DestinationId)
            {
                return BadRequest();
            }
            _context.Destination.Attach(destination);
            _context.Entry(destination).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/DestinationAPI/addDestination
        [HttpPost(template: "addDestination")]
        public ActionResult<Destination> AddDestination(Destination destination)
        {
            _context.Destination.Add(destination);
            _context.SaveChanges();
            return CreatedAtAction("FindDestination", new { id = destination.DestinationId }, destination);
        }

        // DELETE: api/DestinationAPI/deleteDestination/5
        [HttpDelete(template: "deleteDestination/{id}")]
        public IActionResult DeleteDestination(int id)
        {
            var destination = _context.Destination.Find(id);
            if (destination == null)
            {
                return NotFound();
            }
            _context.Destination.Remove(destination);
            _context.SaveChanges();
            return NoContent();
        }

        // Check if destination exists
        private bool DestinationExists(int id)
        {
            return _context.Destination.Any(e => e.DestinationId == id);
        }
    }
}
